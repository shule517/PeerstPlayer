using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using PeerstPlayer;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Shule.Peerst.Util;
using Shule.Peerst.BBS;
using Shule.Peerst.Form;

namespace PeerstViewer
{
	public partial class ThreadViewer : Form
	{
		/// <summary>
		/// 掲示板操作クラス
		/// </summary>
		OperationBbs operationBbs = new OperationBbs();

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
		/// 初期リロード時に最下位までスクロール
		/// </summary>
		bool ScrollBottomOnOpen = true;

		/// <summary>
		/// 折りかえり表示
		/// </summary>
		bool NoBR = true;

		/// <summary>
		/// 終了時に位置保存するか
		/// </summary>
		bool SaveLocationOnClose = false;

		/// <summary>
		/// 終了時にサイズ保存するか
		/// </summary>
		bool SaveSizeOnClose = false;

		/// <summary>
		/// フォント名
		/// </summary>
		string FontName = @"<body bgcolor=""#E6EEF3"" style=""font-family:'メイリオ','ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:16px;line-height:18px;"" >";

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
			LoadIni();

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
				operationBbs.ChangeUrl("本スレ");

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
				IsScrollBottom = ScrollBottomOnOpen;

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
" + FontName;
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
			BbsInfo bbsUrl = operationBbs.BbsInfo;

			try
			{
				// 取得できていなかったら、URLを直接ブラウザで開く
				if ((bbsUrl.BBSServer == BbsServer.UnSupport) || (bbsUrl.BoadGenre == "") || (bbsUrl.BoadNo == ""))
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
					Text = operationBbs.BbsName;

					// スレッド一覧更新
					if ((bbsUrl.BBSServer != BbsServer.UnSupport) && (bbsUrl.BoadGenre != "") && (bbsUrl.BoadNo != ""))
					{
						// スレッド一覧を取得
						operationBbs.UpdateThreadInfo();

						// コンボボックスにセット
						comboBox.Items.Clear();
						for (int i = 0; i < operationBbs.ThreadList.Count; i++)
						{
							// スレタイ(レス数)
							comboBox.Items.Add(operationBbs.ThreadList[i].ThreadTitle + " (" + operationBbs.ThreadList[i].ResCount + ")");
						}
					}

					// 指定スレッドを選択する
					if ((bbsUrl.BBSServer != BbsServer.UnSupport) && (bbsUrl.BoadGenre != "") && (bbsUrl.BoadNo != "") && (bbsUrl.ThreadNo != ""))
					{
						// コンボボックスのスレッドを選択
						int index = 0;

						// 指定スレッドのindexを取得
						for (int i = 0; i < operationBbs.ThreadList.Count; i++)
						{
							if (bbsUrl.ThreadNo == operationBbs.ThreadList[i].ThreadNo)
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
		private void LoadIni()
		{
			string iniFileName = FormUtility.GetExeFileDirectory() + "\\PeerstPlayer.ini";
			IniFile iniFile = new IniFile(iniFileName);

			if (!System.IO.File.Exists(iniFileName))
			{

				string str = @"/**************************************************************************/
/* ここからは、Ｐｌａｙｅｒの設定です
/* 全ての値は、デフォルト値となります
/**************************************************************************/
[Player]

// スレッド内容を字幕表示するか
// True ： 表示
// False ： 表示しない
ThreadCaption =True

// タイトルバー
// True ： 表示
// False ： 表示しない
TitleBar =False

// レスボックスの表示
// True ： 表示
// False ： 表示しない
ResBox =False

// ステータスバーの表示
// True ： 表示
// False ： 表示しない
StatusLabel =True

// フレームの表示
// True ： 表示
// False ： 表示しない
Frame =False

// アスペクト比を維持
// True : 維持する
// False : 維持しない
AspectRate =True

// 最前列表示
// True ： 最前列表示 ON
// False ： 最前列表示 OFF
TopMost =False

// ボリュームの値
Volume =50

// レスボックスの操作
// True（Enter：改行 / Shift+Enter：書き込み）
// False（Enter：書き込み / Shift+Enter：改行）
ResBoxType =True

// レスボックスを自動に隠すか
// True ： 自動に隠れる
// False ： ステータスバークリックで表示
ResBoxAutoVisible =False

// レスボックスの表示 / 非表示
// （スーテタスラベルをクリックした時の動作）
// True ： レスボックスを閉じる
// False ： レスボックスを閉じない
ClickToResBoxClose =False

// 終了時にリレーを切断するか
// True　：　終了時にリレー切断
// False ： 終了時にリレー切断しない
RlayCutOnClose =True

// 終了時に位置を保存
// True ： 保存する
// False ： 保存しない
SaveLocationOnClose =False

// 起動時の位置（X, Y）
// 指定しない場合は空白「X =」「Y =」
X =
Y =

// 起動時のサイズ（Width, Height）
// 指定しない場合は空白「Width =」「Height =」
Width =
Height =

// 起動時の拡大率
Scale =100

// 起動時に動画サイズを合わせる
FitSizeMovie =True

// 終了時にサイズを保存
// True ： 保存する
// False ： 保存しない
SaveSizeOnClose =False

// 終了時に音量を保存
// True ： 保存する
// False ： 保存しない
SaveVolumeOnClose =False

// ステータスラベルのフォント指定
FontName =MS UI Gothic

// フォントの大きさ(pt)
FontSize =9

// フォントの色(RGB)
FontColorR =0
FontColorG =255
FontColorB =128

// 書き込み時にレスボックスを閉じる
// True ： 閉じる
// False ： 閉じない
CloseResBoxOnWrite =False

// バックスペースでレスボックスを閉じる
CloseResBoxOnBackSpace =False

// マウスジェスチャーを使うか
// falseにすると、ショートカットにマウスジェスチャーが指定されていても反応しません
// True ： 使う
// False ： 使わない
UseMouseGesture =True

// スクリーン吸着をするか
// True ： する
// False ： しない
UseScreenMagnet =True

// プレイヤーを閉じたときにビューワも閉じるか
// True ： 閉じる
// False ： 閉じない
CloseViewerOnClose=True

// スクリーン吸着の感度（指定したドット範囲内だと吸着します）
ScreenMagnetDockDist=20

// マウスジェスチャー感度
MouseGestureInterval =10

/// ブラウザアドレス(exeの場所を入力してください)
BrowserAddress =

// スレッドブラウザアドレス(exeの場所を入力してください)
ThreadBrowserAddress =

/**************************************************************************/
/* ここからは、Viewerの設定です
/**************************************************************************/
[Viewer]
// 起動時の位置（X, Y）
// 指定しない場合は空白「X =」「Y =」
X =
Y =

// 起動時のサイズ（Width, Height）
// 指定しない場合は空白「Width =」「Height =」
Width =
Height =

// 最前列表示にするか
// True : 最前列表示
// False : 最前列表示にしない
TopMost =False

// 自動更新をするか
// True : 自動更新をする
// False : 自動行進をしない
AutoReload =True

// 自動更新時間（秒）（7、10、15、20、25、30）
AutoReloadInterval =7

// 書き込み欄を表示するか
// True : 表示
// False : 非表示
WriteView =False

// フォント指定
// AAがずれないフォント：<body style=""font-family:'ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:16px;line-height:18px;"" >
// htmlのbodyタグを埋め込んでフォントを変えています。 いろいろいじってみてね！
// FontName = <body bgcolor=""背景色"" style=""font-family:'フォント名';font-size:サイズpx;line-height:高さpx;"" >

FontName =<body bgcolor=""#E6EEF3"" style=""font-family:'※※※','ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:16px;line-height:18px;"" >

// 書き込み欄の高さ
WriteHeight =223

// スレッドを開いた時に最下位まで移動させるか
// True : スクロールする
// False : スクロールしない
ScrollBottom =True
/**************************************************************************/
/* ここからは、ショートカットの設定です
/**************************************************************************/
[PlayerShortcut]
↓ = OpenThreadViewer
↓↑ = ChannelInfoUpdate
↑↓ = ThreadListUpdate
↓→ = Close
← = balance-10
→ = balance+10
MiddleClick = Mini&Mute
Shift+WheelUp = Volume+1
Shift+WheelDown = Volume-1
Control+WheelUp = Volume+5
Control+WheelDown = Volume-5
WheelUp = Volume+10
WheelDown = Volume-10
Up = Volume+10
Down = Volume-10
Alt+B = Bump
Alt+X = Close&RelayCut
Right = VolumeBalance+10
Left = VolumeBalance-10
Alt+Left = VolumeBalanceLeft
Alt+Right = VolumeBalanceRight
Alt+Down = VolumeBalanceMiddle
RightClick+WheelUp = Size-10%
RightClick+WheelDown = Size+10%
Shift+Up = Size-10%
Shift+Down = Size+10%
Control+Up = Size-10
Control+Down = Size+10
Space = SelectResBox
O = OpenThreadViewer
U = ThreadListUpdate
Escape = Close
T = TopMost
A = ResBox
S = StatusLabel
D = Frame
F = TitleBar
Z = AspectRate
Return = OpenContextMenu
Delete = Mute
Alt+Return = FullScreen
D1 = Size=50%
D2 = Size=75%
D3 = Size=100%
D4 = Size=150%
D5 = Size=200%
Alt+D1 = ScreenSplit=5
Alt+D2 = ScreenSplit=4
Alt+D3 = ScreenSplit=3
Alt+D4 = ScreenSplit=2
Q = ScreenSplitWidth=5
W = ScreenSplitWidth=4
E = ScreenSplitWidth=3
R = ScreenSplitWidth=2
Alt+Q = ScreenSplitHeight=5
Alt+W = ScreenSplitHeight=4
Alt+E = ScreenSplitHeight=3
Alt+R = ScreenSplitHeight=2
Alt+LeftClick = Frame
Right->LeftClick = Frame
P = ScreenShot
↑ = ScreenShot
L = OpenScreenShotFolder
G = FitSizeMovie
Control+V = OpenClipBoard
K = OpenFile
H = Retry
";
				FileStream writer = new FileStream(iniFileName,
									   FileMode.Create,  // 上書き
									   FileAccess.Write);

				byte[] bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(str);
				for (int i = 0; i < bytes.Length; i++)
				{
					writer.WriteByte(bytes[i]);
				}
				writer.Close();
			}

			#region 初期設定

			// デフォルト
			string[] keys = iniFile.GetKeys("Viewer");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Viewer", keys[i]);
				switch (keys[i])
				{
					// 初期位置X
					case "X":
						try
						{
							Left = int.Parse(data);
						}
						catch
						{
						}
						break;

					// 初期位置Y
					case "Y":
						try
						{
							Top = int.Parse(data);
						}
						catch
						{
						}
						break;

					// 初期Width
					case "Width":
						try
						{
							Width = int.Parse(data);
						}
						catch
						{
						}
						break;

					// 初期Height
					case "Height":
						try
						{
							Height = int.Parse(data);
						}
						catch
						{
						}
						break;

					// 最前列表示
					case "TopMost":
						TopMost = (data == "True");
						break;

					// 自動更新をするか
					case "AutoReload":
						IsAutoReload = (data == "True");
						break;

					// 自動更新時間
					case "AutoReloadInterval":
						switch (data)
						{
							case "7":
								toolStripComboBoxReloadTime.SelectedIndex = 0;
								break;
							case "10":
								toolStripComboBoxReloadTime.SelectedIndex = 1;
								break;
							case "15":
								toolStripComboBoxReloadTime.SelectedIndex = 2;
								break;
							case "20":
								toolStripComboBoxReloadTime.SelectedIndex = 3;
								break;
							case "25":
								toolStripComboBoxReloadTime.SelectedIndex = 4;
								break;
							case "30":
								toolStripComboBoxReloadTime.SelectedIndex = 5;
								break;
						}
						IsAutoReload = IsAutoReload;
						break;

					// 書き込み欄を表示するか
					case "WriteView":
						IsWriteView = (data == "True");
						break;

					// 初期スレッド更新時に最下位までスクロールすか
					case "ScrollBottom":
						ScrollBottomOnOpen = (data == "True");
						break;

					// フォント名
					case "FontName":
						FontName = data;
						break;

					// 書き込み欄表示
					case "WriteHeight":
						try
						{
							splitContainer.SplitterDistance = int.Parse(data);
						}
						catch
						{
						}
						break;

					// 折り返し表示
					case "NoBR":
						NoBR = (data == "True");
						break;

					// 終了時に位置を保存
					case "SaveLocationOnClose":
						SaveLocationOnClose = (data == "True");
						break;

					// 終了時にサイズを保存
					case "SaveSizeOnClose":
						SaveSizeOnClose = (data == "True");
						break;
				}
			}

			// 最前列表示
			iniFile.Write("Viewer", "TopMost", TopMost.ToString());

			// 自動更新をするか
			iniFile.Write("Viewer", "AutoReload", IsAutoReload.ToString());

			// 自動更新時間
			int time = 7;
			switch (toolStripComboBoxReloadTime.SelectedIndex)
			{
				case 0:
					time = 7;
					break;
				case 1:
					time = 10;
					break;
				case 2:
					time = 15;
					break;
				case 3:
					time = 20;
					break;
				case 4:
					time = 25;
					break;
				case 5:
					time = 30;
					break;
			}
			iniFile.Write("Viewer", "AutoReloadInterval", time.ToString());

			// 書き込み欄を表示するか
			iniFile.Write("Viewer", "WriteView", IsWriteView.ToString());

			// 初期スレッド更新時に最下位までスクロールすか
			iniFile.Write("Viewer", "ScrollBottom", ScrollBottomOnOpen.ToString());

			// フォント名
			iniFile.Write("Viewer", "FontName", FontName);

			// 書き込み欄の高さ
			iniFile.Write("Viewer", "WriteHeight", splitContainer.SplitterDistance.ToString());

			#endregion
		}
	}
}
