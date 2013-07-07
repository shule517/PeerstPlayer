using System;
using System.Drawing;
using System.Windows.Forms;
using PeerstLib.Bbs;
using PeerstLib.Utility;
using PeerstPlayer.View;

namespace PeerstPlayer.Control
{
	//-------------------------------------------------------------
	// 概要：書き込み欄コントロール
	//-------------------------------------------------------------
	public partial class WriteField : UserControl
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

		//-------------------------------------------------------------
		// 定義
		//-------------------------------------------------------------

		// 選択スレッドのレス数の更新間隔
		const int UpdateThreadResCount = 60000;

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：イベントの設定
		//-------------------------------------------------------------
		public WriteField()
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

			// 書き込みボタン押下
			writeButton.Click += (sender, e) =>
			{
				// レス書き込み
				WriteRes();

				// 書き込み欄をフォーカス
				writeFieldTextBox.Focus();
			};

			// スレッド一覧情報更新イベント
			threadSelectView.ThreadListChange += (sender, e) => operationBbs.ChangeUrl(threadSelectView.ThreadUrl);
			// スレッド変更イベント
			threadSelectView.ThreadChange += (sender, e) => operationBbs.ChangeUrl(threadSelectView.ThreadUrl);
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
				WriteRes();
			}
		}

		//-------------------------------------------------------------
		// 概要：レス書き込み
		//-------------------------------------------------------------
		private void WriteRes()
		{
			string message = writeFieldTextBox.Text;
			Logger.Instance.InfoFormat("レス書き込み [掲示板:{0} スレッド:{1}, 本文:{2}]", operationBbs.BbsInfo.BbsName, operationBbs.SelectThread.ThreadTitle, message);

			// レス書き込み
			try
			{
				operationBbs.Write("", "sage", writeFieldTextBox.Text);
				Logger.Instance.InfoFormat("レス書き込み：成功 [掲示板:{0} スレッド:{1}, 本文:{2}]", operationBbs.BbsInfo.BbsName, operationBbs.SelectThread.ThreadTitle, message);
				writeFieldTextBox.Text = "";
			}
			catch
			{
				Logger.Instance.ErrorFormat("レス書き込み：失敗 [掲示板:{0} スレッド:{1}, 本文:{2}]", operationBbs.BbsInfo.BbsName, operationBbs.SelectThread.ThreadTitle, message);
				MessageBox.Show(
					string.Format("レス書き込みに失敗しました。\n\n掲示板：{0}\nスレッド：{1}\n本文：{2}", operationBbs.BbsInfo.BbsName, operationBbs.SelectThread.ThreadTitle, message),
					"Error!!");
				writeFieldTextBox.Text = message;
			}
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
