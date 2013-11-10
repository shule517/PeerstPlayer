using System;
using System.Drawing;
using System.Windows.Forms;
using PeerstLib.Bbs;
using PeerstLib.Util;
using System.Linq;
using PeerstPlayer.Forms.ThreadSelect;
using System.ComponentModel;

namespace PeerstPlayer.Controls.WriteField
{
	//-------------------------------------------------------------
	// 概要：書き込み欄コントロール
	//-------------------------------------------------------------
	public partial class WriteFieldControl : UserControl
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// 選択スレッドURL
		public string SelectThreadUrl
		{
			get { return operationBbs.ThreadUrl; }
			set
			{
				// TODO 別メソッド移動
				// TODO ChangeUrlをBackGroundで実行させる
				threadSelectView.ThreadUrl = value;
				operationBbs.ChangeUrl(value);
			}
		}

		// 高さ変更
		public event EventHandler HeightChanged = delegate { };

		// 右クリックイベント
		public event EventHandler RightClick = delegate { };

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// 掲示板操作クラス
		private OperationBbs operationBbs = new OperationBbs();

		// スレッド選択画面
		private ThreadSelectView threadSelectView = new ThreadSelectView();

		// 新着レス取得Worker
		BackgroundWorker showNewResWorker = new BackgroundWorker();

		// レス書き込みWorker
		BackgroundWorker writeResWorker = new BackgroundWorker();

		//-------------------------------------------------------------
		// 定義
		//-------------------------------------------------------------

		// 選択スレッドのレス数の更新間隔
		const int UpdateThreadResCount = 60000;

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：イベントの設定
		//-------------------------------------------------------------
		public WriteFieldControl()
		{
			InitializeComponent();

			// 選択スレッドのクリックイベント
			selectThreadLabel.MouseDown += (sender, e) =>
			{
				if (e.Button == MouseButtons.Left)
				{
					// 選択スレッド画面を開く
					threadSelectView.Open();
				}
				else if (e.Button == MouseButtons.Right)
				{
					RightClick(sender, e);
				}
			};

			// 新着レス表示処理
			string newRes = "";
			showNewResWorker.WorkerSupportsCancellation = true; // キャンセル処理を許可
			showNewResWorker.DoWork += (sender, e) =>
			{
				newRes = ReadNewRes();
			};
			showNewResWorker.RunWorkerCompleted += (sender, e) =>
			{
				newResToolTip.SetToolTip(selectThreadLabel, newRes);
			};

			// マウスオーバー時に新着レス表示
			selectThreadLabel.MouseHover += (sender, e) =>
			{
				// 新着レス表示の実行
				if (!showNewResWorker.IsBusy)
				{
					showNewResWorker.RunWorkerAsync();
				}
			};

			// レス書き込み処理
			writeResWorker.WorkerSupportsCancellation = true;
			ArgumentException exception = new ArgumentException();
			string message = writeFieldTextBox.Text;
			writeResWorker.DoWork += (sender, e) =>
			{
				e.Result = true;

				// レス書き込み
				try
				{
					WriteRes();
				}
				catch (ArgumentException ex)
				{
					exception = ex;
					e.Result = false;
					Logger.Instance.ErrorFormat("レス書き込み：失敗 [エラー内容{0} 掲示板:{1} スレッド:{2}, 本文:{3}]", ex.Message, operationBbs.BbsInfo.BbsName, operationBbs.SelectThread.ThreadTitle, message);
				}
			};
			// 書き込み完了処理
			writeResWorker.RunWorkerCompleted += (sender, e) =>
			{
				if ((bool)e.Result)
				{
					writeFieldTextBox.Text = "";
				}
				else
				{
					writeFieldTextBox.Text = message;
					MessageBox.Show(
						string.Format("{0}\n\n掲示板：{1}\nスレッド：{2}\n本文：{3}", exception.Message, operationBbs.BbsInfo.BbsName, operationBbs.SelectThread.ThreadTitle, message),
						"Error!!");
				}
			};

			// 書き込みボタン押下
			writeButton.Click += (sender, e) =>
			{
				// レス書き込み
				if (!writeResWorker.IsBusy)
				{
					writeResWorker.RunWorkerAsync();
				}

				// 書き込み欄をフォーカス
				writeFieldTextBox.Focus();
			};

			// スレッド一覧情報更新イベント
			threadSelectView.ThreadListChange += (sender, e) => operationBbs.ChangeUrl(threadSelectView.ThreadUrl);
			// スレッド変更イベント
			threadSelectView.ThreadChange += (sender, e) =>
			{
				// スレッド情報の更新
				ThreadSelectEventArgs args = (ThreadSelectEventArgs)e;
				operationBbs = args.OperationBbs;
				UpdateThreadTitle();
			};
			// スレッドタイトルの更新
			operationBbs.ThreadListChange += (sender, e) => UpdateThreadTitle();

			// 高さ自動調節
			writeFieldTextBox_TextChanged(this, new EventArgs());

			// 選択スレッドのレス数の更新
			Timer timer = new Timer();
			timer.Interval = UpdateThreadResCount;
			timer.Tick += (sender, e) =>
			{
				operationBbs.UpdateThreadList();
				UpdateThreadTitle();
			};
			timer.Start();
		}

		//-------------------------------------------------------------
		// 概要：新着レス取得
		//-------------------------------------------------------------
		public string ReadNewRes()
		{
			operationBbs.ReadThread();

			string message = "";
			foreach (var res in operationBbs.ResList.Select((v, i) => new { v, i }))
			{
				if ((operationBbs.ResList.Count - res.i) <= 5)
				{
					string text = res.v.Message.Replace("<br>", "\n         ");
					message += String.Format("{0, 4} : {1}\n", res.v.ResNo, WebUtil.DeleteHtmlTag(text));
				}
			}

			return message;
		}

		//-------------------------------------------------------------
		// 概要：スレッドタイトルの更新
		//-------------------------------------------------------------
		private void UpdateThreadTitle()
		{
			if (operationBbs.Enabled)
			{
				// スレッドタイトル表示
				EditThreadTitle();
			}
			else
			{
				// スレッドタイトル表示
				if (string.IsNullOrEmpty(operationBbs.ThreadUrl))
				{
					selectThreadLabel.Text = "URLが指定されていません";
				}
				else
				{
					selectThreadLabel.Text = "未対応URLです";
				}
			}

			// 書き込み欄の有効/無効
			writeFieldTextBox.Enabled = operationBbs.ThreadSelected;
		}

		//-------------------------------------------------------------
		// 概要：スレッドタイトルの編集
		//-------------------------------------------------------------
		private void EditThreadTitle()
		{
			if (operationBbs.ThreadSelected)
			{
				try
				{
					selectThreadLabel.Text = string.Format("スレッド[ {0} ] ({1})", operationBbs.SelectThread.ThreadTitle, operationBbs.SelectThread.ResCount);

					// スレッドストップ時の色設定
					if (operationBbs.SelectThread.IsStopThread)
					{
						selectThreadLabel.ForeColor = Color.Red;
					}
					else
					{
						selectThreadLabel.ForeColor = Color.SpringGreen;
					}
				}
				catch
				{
					selectThreadLabel.Text = string.Format("掲示板[ {0} ] 指定されたスレッドが存在しません", operationBbs.BbsInfo.BbsName);
				}
			}
			else
			{
				selectThreadLabel.Text = string.Format("掲示板[ {0} ] スレッドを選択してください", operationBbs.BbsInfo.BbsName);
			}
		}

		//-------------------------------------------------------------
		// 概要：文字入力イベント
		// 詳細：コントロールの高さ調節
		//-------------------------------------------------------------
		private void writeFieldTextBox_TextChanged(object sender, EventArgs e)
		{
			// 書き込み欄の高さ自動調節
			writeFieldTextBox.Height = writeFieldTextBox.PreferredSize.Height;
			writeButton.Height = writeFieldTextBox.Height + 2;
			Height = selectThreadLabel.Height + writeFieldTextBox.PreferredSize.Height;

			// 高さの変更通知
			HeightChanged(sender, e);
		}

		//-------------------------------------------------------------
		// 概要：書き込み欄のキー押下
		// 詳細：レス書き込み
		//-------------------------------------------------------------
		private void writeFieldTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.A))
			{
				Logger.Instance.Debug("全選択");
				writeFieldTextBox.SelectAll();
			}
			else if (((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Enter)) ||	// Ctrl + Enter
					((e.Modifiers == Keys.Shift) && (e.KeyCode == Keys.Enter)))			// Shift + Enter
			{
				// 書き込み後に改行が入らないようにする
				e.SuppressKeyPress = true;

				// レス書き込み
				if (!writeResWorker.IsBusy)
				{
					writeResWorker.RunWorkerAsync();
				}
			}
		}

		//-------------------------------------------------------------
		// 概要：レス書き込み
		//-------------------------------------------------------------
		private void WriteRes()
		{
			string message = writeFieldTextBox.Text;
			Logger.Instance.InfoFormat("レス書き込み [掲示板:{0} スレッド:{1}, 本文:{2}]", operationBbs.BbsInfo.BbsName, operationBbs.SelectThread.ThreadTitle, message);

			operationBbs.Write("", "sage", message);
			Logger.Instance.InfoFormat("レス書き込み：成功 [掲示板:{0} スレッド:{1}, 本文:{2}]", operationBbs.BbsInfo.BbsName, operationBbs.SelectThread.ThreadTitle, message);
		}

		//-------------------------------------------------------------
		// 概要：終了処理
		//-------------------------------------------------------------
		public void Close()
		{
			Logger.Instance.Debug("Close()");
			operationBbs.Close();
			threadSelectView.Kill();
		}
	}
}
