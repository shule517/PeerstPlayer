using PeerstViewer.Settings;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PeerstLib.Bbs;
using PeerstLib.Bbs.Data;

namespace PeerstViewer
{
	public partial class ThreadViewer : Form
	{
		/// <summary>
		/// 掲示板操作クラス
		/// </summary>
		PeerstLib.Bbs.OperationBbs operationBbs = new PeerstLib.Bbs.OperationBbs();

		/// <summary>
		/// 現在取得している次のレス番号
		/// </summary>
		int ResNum = 1;

		/// <summary>
		/// スクロール位置X
		/// </summary>
		int ScrollPosX = 0;

		/// <summary>
		/// スクロール位置Y
		/// </summary>
		int ScrollPosY = 0;

		/// <summary>
		/// １番下までスクロールするか
		/// </summary>
		bool IsScrollBottom = true;

		/// <summary>
		/// OuterText
		/// </summary>
		string OuterText = "";

		/// <summary>
		/// 現在のブラウザのHTML
		/// </summary>
		string DocumentText = "";

		/// <summary>
		/// レスのデータリスト
		/// </summary>
		List<string> ResList = new List<string>();

		/// <summary>
		/// Panel2の高さ：書き込み欄表示時のスクロール用
		/// </summary>
		int Panel2Height = 0;

		/// <summary>
		/// チャンネル名
		/// </summary>
		string ChannelName = "";

		/// <summary>
		/// 書き込み欄の表示
		/// </summary>
		bool IsWriteView
		{
			get
			{
				return toolStripButtonWriteView.Checked;
			}
			set
			{
				// チェック
				toolStripButtonWriteView.Checked = value;
				// 表示 / 非表示
				splitContainer.Panel2Collapsed = !value;

				try
				{
					if (value)
					{
						Panel2Height = splitContainer.Panel2.Height;
						webBrowser.Document.Window.ScrollTo(ScrollPosX, ScrollPosY + splitContainer.Panel2.Height);
					}
					else if (webBrowser.Document != null)
					{
						webBrowser.Document.Window.ScrollTo(ScrollPosX, ScrollPosY - Panel2Height);
					}
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// オートリロード
		/// </summary>
		bool IsAutoReload
		{
			get
			{
				return toolStripButtonAutoReload.Checked;
			}
			set
			{
				// 自動更新ストップ
				if (!value)
				{
					// チェックをはずす
					toolStripButtonAutoReload.Checked = false;
					// 自動更新秒数
					toolStripComboBoxReloadTime.Enabled = true;
					// 自動更新タイマーストップ
					timerAutoReload.Stop();
				}
				// 自動更新スタート
				else
				{
					// チェックする
					toolStripButtonAutoReload.Checked = true;
					// 自動更新秒数
					toolStripComboBoxReloadTime.Enabled = false;
					try
					{
						// 自動更新タイマースタート
						int reload_time = int.Parse(toolStripComboBoxReloadTime.Text.ToString()) * 1000;
						timerAutoReload.Interval = reload_time;
						timerAutoReload.Start();
					}
					catch
					{
						// チェックをはずす
						toolStripButtonAutoReload.Checked = false;
						// 自動更新秒数
						toolStripComboBoxReloadTime.Enabled = true;
					}
				}
			}
		}

		/// <summary>
		/// 画像ビューワ
		/// </summary>
		ImageViewer imageViewer = new ImageViewer();

		/// <summary>
		/// オートリロードタイマー
		/// </summary>
		Timer timerAutoReload;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ThreadViewer()
		{
			InitializeComponent();
			Show();
			
			// オートリロードタイマー
			timerAutoReload = new Timer();
			timerAutoReload.Tick += new EventHandler(timerAutoReload_Tick);

			// コンボボックスを選択
			toolStripComboBoxReloadTime.SelectedIndex = 0;

			// 書き込み欄を非表示
			IsWriteView = false;

			// オートリロード開始
			IsAutoReload = true;

			// 起動時に最下位にスクロールさせるか
			IsScrollBottom = true;

			// 初期化ファイル読み込み
			LoadSettings();

			// チャンネル名を取得
			if (Environment.GetCommandLineArgs().Length > 2)
			{
				ChannelName = Environment.GetCommandLineArgs()[2].ToString();
			}

			// コマンドライン入力
			if (Environment.GetCommandLineArgs().Length > 1)
			{
				// スレッドURL
				String url = Environment.GetCommandLineArgs()[1].ToString();

				// コンタクトURL設定有り
				InitDocumentText();
				operationBbs.ChangeUrl(url);

				// スレ一覧更新
				ThreadListUpdate();
			}
			else
			{
				// コンタクトURL設定なし
				//operationBbs.ChangeUrl("本スレ");

				// ブラウザのフォント、背景色を設定
				webBrowser.DocumentText = @"<body bgcolor=""#E6EEF3"" style=""font-family:'ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:14px;line-height:16px;"" ><br>↑スレッド(板)URLを入力してください。";
			}
		}

		/// <summary>
		/// スレッドビューワを開く
		/// </summary>
		public void OpenUrl()
		{
			try
			{
				InitDocumentText();
				IsScrollBottom = ViewerSettings.ScrollBottom;

				ResList.Clear();
				resList.Clear();

				// スレッド更新
				Reload(false);
			}
			catch
			{
			}
		}

		/// <summary>
		/// ドキュメントの初期化
		/// </summary>
		private void InitDocumentText()
		{
			ResNum = 1;

			// ブラウザのフォント、背景色を設定
			DocumentText = @"<head>
<style type=""text/css"">
<!--
U
{
color: #0000FF;
}

ul
{
margin: 1px 1px 1px 30px;
}

TT
{
color: #0000FF;
text-decoration:underline;
}
-->
</style>
</head>
" + ViewerSettings.FontName;
		}

		/// <summary>
		/// 更新
		/// </summary>
		void Reload(bool UseThread)
		{
			// 動作中の場合キャンセル
			if (backgroundWorkerReload.IsBusy)
			{
				return;
			}

			// 実行
			comboBox.Enabled = false;
			backgroundWorkerReload.RunWorkerAsync();
		}

		/// <summary>
		/// ID抽出
		/// </summary>
		private string GetIDRes(string id)
		{
			string text = "";

			for (int i = 0; i < ResList.Count; i++)
			{
				int index = ResList[i].IndexOf(id);
				if (index != -1)
				{
					if (text != "")
					{
						text += "\n\n";
					}

					text += ResList[i];
				}
			}

			return text;
		}

		/// <summary>
		/// 書き込み後の改行対策
		/// </summary>
		bool WriteCheck = false;

		/// <summary>
		/// コンボボックスに入力されたURLを更新 / 現在の板のスレッド一覧を取得
		/// </summary>
		void ThreadListUpdate()
		{
			PeerstLib.Bbs.Data.BbsInfo bbsInfo = operationBbs.BbsInfo;

			try
			{
				// 取得できていなかったら、URLを直接ブラウザで開く
				if ((bbsInfo.BbsServer == BbsServer.UnSupport) || (bbsInfo.BoardGenre == "") || (bbsInfo.BoardNo == ""))
				{
					string url = comboBox.Text;
					if (isEnableUrl(url))
					{
						webBrowser.Url = new Uri(url);
					}
					else
					{
						// 初期表示
						webBrowser.DocumentText = @"<body bgcolor=""#E6EEF3"" style=""font-family:'ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:14px;line-height:16px;"" ><br>↑スレッド(板)URLを入力してください。";
					}
				}
				// データ更新
				else
				{
					// タイトルバーに板名表示
					Text = bbsInfo.BbsName;

					// スレッド一覧更新
					if ((bbsInfo.BbsServer != BbsServer.UnSupport) && (bbsInfo.BoardGenre != "") && (bbsInfo.BoardNo != ""))
					{
						// スレッド一覧を取得
						operationBbs.UpdateThreadList();

						// コンボボックスにセット
						comboBox.Items.Clear();
						for (int i = 0; i < operationBbs.ThreadList.Count; i++)
						{
							// スレタイ(レス数)
							comboBox.Items.Add(operationBbs.ThreadList[i].ThreadTitle + " (" + operationBbs.ThreadList[i].ResCount + ")");
						}
					}

					// 指定スレッドを選択する
					if ((bbsInfo.BbsServer != BbsServer.UnSupport) && (bbsInfo.BoardGenre != "") && (bbsInfo.BoardNo != "") && (bbsInfo.ThreadNo != ""))
					{
						// コンボボックスのスレッドを選択
						int index = 0;

						// 指定スレッドのindexを取得
						for (int i = 0; i < operationBbs.ThreadList.Count; i++)
						{
							if (bbsInfo.ThreadNo == operationBbs.ThreadList[i].ThreadNo)
							{
								index = i;
								break;
							}
						}

						// スレッドを選択
						if (comboBox.Items.Count > index)
						{
							comboBox.SelectedIndex = index;
						}
					}
					// スレッドの指定なし 
					else
					{
						// １番上を選択
						if (comboBox.Items.Count > 0)
						{
							comboBox.SelectedIndex = 0;
						}
					}
				}
			}
			catch
			{
			}
		}

		/// <summary>
		/// 初期設定ロード
		/// </summary>
		private void LoadSettings()
		{
			// 設定の読み込み
			ViewerSettings.Load();

			// ウィンドウ設定
			Left = ViewerSettings.X;
			Top = ViewerSettings.Y;
			Width = ViewerSettings.Width;
			Height = ViewerSettings.Height;
			TopMost = ViewerSettings.TopMost;

			// 自動更新設定
			switch (ViewerSettings.AutoReloadInterval)
			{
				case 7:
					toolStripComboBoxReloadTime.SelectedIndex = 0;
					break;
				case 10:
					toolStripComboBoxReloadTime.SelectedIndex = 1;
					break;
				case 15:
					toolStripComboBoxReloadTime.SelectedIndex = 2;
					break;
				case 20:
					toolStripComboBoxReloadTime.SelectedIndex = 3;
					break;
				case 25:
					toolStripComboBoxReloadTime.SelectedIndex = 4;
					break;
				case 30:
					toolStripComboBoxReloadTime.SelectedIndex = 5;
					break;
			}

			// 書き込み欄の高さ設定
			try
			{
				splitContainer.SplitterDistance = ViewerSettings.WriteHeight;
			}
			catch
			{
			}

			// 自動更新
			IsAutoReload = ViewerSettings.AutoReload;

			// 書き込み欄表示
			IsWriteView = ViewerSettings.WriteView;
		}
	}
}
