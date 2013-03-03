using Shule.Peerst.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PeerstPlayer
{
	class Settings
	{
		/// <summary>
		/// ショートカットリスト
		/// </summary>
		public List<string[]> ShortcutList = new List<string[]>();

		/// <summary>
		/// ブラウザアドレス
		/// </summary>
		public string BrowserAddress = "";

		/// <summary>
		/// スレッドブラウザアドレス
		/// </summary>
		public string ThreadBrowserAddress = "";

		/// <summary>
		/// レスボックスの操作
		/// true（Enter：改行 / Shift+Enter：書き込み）
		/// false（Enter：書き込み / Shift+Enter：改行）
		/// </summary>
		public bool ResBoxType = true;

		/// <summary>
		/// レスボックスを自動的に隠すか
		/// </summary>
		public bool ResBoxAutoVisible = false;

		/// <summary>
		/// 終了時にリレーを切断する
		/// </summary>
		public bool RlayCutOnClose = true;

		/// <summary>
		/// クリックした時レスボックスを閉じるか
		/// </summary>
		public bool ClickToResBoxClose = true;

		/// <summary>
		/// 終了時に位置を保存するか
		/// </summary>
		public bool SaveLocationOnClose = false;

		/// <summary>
		/// 終了時にサイズを保存するか
		/// </summary>
		public bool SaveSizeOnClose = false;

		/// <summary>
		/// アスペクト比を維持
		/// </summary>
		public bool AspectRate = true;

		/// <summary>
		/// 書き込み時にレスボックスを閉じる
		/// </summary>
		public bool CloseResBoxOnWrite = true;

		/// <summary>
		/// バックスペースでレスボックスを閉じるか
		/// </summary>
		public bool CloseResBoxOnBackSpace = false;

		/// <summary>
		/// 最小化ミュート時
		/// </summary>
		public bool MiniMute = false;

		/// <summary>
		/// デフォルト拡大率
		/// </summary>
		public int DefaultScale = -1;

		/// <summary>
		/// 起動時に動画サイズを合わせる
		/// </summary>
		public bool FitSizeMovie = false;

		/// <summary>
		/// スクリーン吸着距離
		/// </summary>
		public int ScreenMagnetDockDist = 20;

		/// <summary>
		/// 終了時に一緒にビューワも終了するか
		/// </summary>
		public bool CloseViewerOnClose = false;

		/// <summary>
		/// 終了時のボリュームを保存するか
		/// </summary>
		public bool SaveVolumeOnClose = false;

		/// <summary>
		/// スクリーン吸着をするか
		/// </summary>
		public bool UseScreenMagnet = true;

		/// <summary>
		/// INIファイルの読み込み
		/// </summary>
		// TODO iniFileを内部で生成
		public void LoadIniFile(IniFile iniFile, string iniFileName)
		{
			// INIファイルが存在しなければ生成
			if (!System.IO.File.Exists(iniFileName))
			{
				CreateIniFile(iniFileName);
			}

			// 設定読み込み
			LoadPlayerSettings(iniFile);

			// ショートカット読み込み
			LoadShortcut(iniFile);
		}

		// TODO iniFileをSettings内で生成するように修正
		/// <summary>
		/// INIファイルから設定読み込み
		/// </summary>
		private void LoadPlayerSettings(IniFile iniFile)
		{
			// デフォルト
			string[] keys = iniFile.GetKeys("Player");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Player", keys[i]);
				switch (keys[i])
				{
					/*
					// レスボックス
					case "ResBox":
						panelResBox.Visible = (data == "True");
						break;

					// ステータスラベル
					case "StatusLabel":
						panelStatusLabel.Visible = (data == "True");
						break;

					// 最前列表示
					case "TopMost":
						TopMost = (data == "True");
						break;
					 */

					case "AspectRate":
						AspectRate = (data == "True");
						break;

					// レスボックスの操作方法
					case "ResBoxType":
						ResBoxType = (data == "True");
						break;

					// レスボックスを自動表示
					case "ResBoxAutoVisible":
						ResBoxAutoVisible = (data == "True");
						break;

					// 終了時にリレーを終了
					case "RlayCutOnClose":
						RlayCutOnClose = (data == "True");
						break;

					//　書き込み後にレスボックスを閉じる
					case "CloseResBoxOnWrite":
						CloseResBoxOnWrite = (data == "True");
						break;

					case "UseScreenMagnet":
						UseScreenMagnet = (data == "True");
						break;

					// 終了時に一緒にビューワも終了するか
					case "CloseViewerOnClose":
						CloseViewerOnClose = (data == "True");
						break;

					// バックスペースでレスボックスを閉じるか
					case "CloseResBoxOnBackSpace":
						CloseResBoxOnBackSpace = (data == "True");
						break;

					// クリックした時にレスボックスを閉じるか
					case "ClickToResBoxClose":
						ClickToResBoxClose = (data == "True");
						break;

					// 終了時に位置を保存するか
					case "SaveLocationOnClose":
						SaveLocationOnClose = (data == "True");
						break;

					// 終了時にボリュームを保存するか
					case "SaveVolumeOnClose":
						SaveVolumeOnClose = (data == "True");
						break;

					// 終了時にサイズを保存するか
					case "SaveSizeOnClose":
						SaveSizeOnClose = (data == "True");
						break;

					// 再生時に動画サイズに合わせる
					case "FitSizeMovie":
						FitSizeMovie = (data == "True");
						break;

					/*
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


					// ボリューム
					case "Volume":
						try
						{
							wmp.Volume = int.Parse(data);
						}
						catch
						{
						}
						break;
					 */

					// 拡大率
					case "Scale":
						try
						{
							if (data == "")
								DefaultScale = -1;

							DefaultScale = int.Parse(data);

							if (DefaultScale < 0)
								DefaultScale = -1;
						}
						catch
						{
						}
						break;

					/*
					// フォント名
					case "FontName":
						SetFont(data, labelDetail.Font.Size);
						break;

					// フォントのサイズ
					case "FontSize":
						try
						{
							SetFont(labelDetail.Font.Name, float.Parse(data));
						}
						catch
						{
						}
						break;

					case "FontColorR":
						try
						{
							int R = int.Parse(data);
							Color color = Color.FromArgb(255, R, labelDetail.ForeColor.G, labelDetail.ForeColor.B);
							labelDetail.ForeColor = color;
							labelDuration.ForeColor = color;
							labelVolume.ForeColor = color;
						}
						catch
						{
						}
						break;

					case "FontColorG":
						try
						{
							int G = int.Parse(data);
							Color color = Color.FromArgb(255, labelDetail.ForeColor.R, G, labelDetail.ForeColor.B);
							labelDetail.ForeColor = color;
							labelDuration.ForeColor = color;
							labelVolume.ForeColor = color;
						}
						catch
						{
						}
						break;

					case "FontColorB":
						try
						{
							int B = int.Parse(data);
							Color color = Color.FromArgb(255, labelDetail.ForeColor.R, labelDetail.ForeColor.G, B);
							labelDetail.ForeColor = color;
							labelDuration.ForeColor = color;
							labelVolume.ForeColor = color;
						}
						catch
						{
						}
						break;
					 */

					case "ScreenMagnetDockDist":
						try
						{
							ScreenMagnetDockDist = int.Parse(data);
						}
						catch
						{
						}
						break;

					/*
					case "MouseGestureInterval":
						try
						{
							wmp.mouseGesture.Interval = int.Parse(data);
						}
						catch
						{
						}
						break;
					 */

					case "BrowserAddress":
						BrowserAddress = data;
						break;

					case "ThreadBrowserAddress":
						ThreadBrowserAddress = data;
						break;
				}
			}
		}


		// TODO iniFileをSettings内で生成するように修正
		/// <summary>
		/// ショートカットの読み込み
		/// </summary>
		private void LoadShortcut(IniFile iniFile)
		{
			// ショートカット
			string[] keys = iniFile.GetKeys("PlayerShortcut");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("PlayerShortcut", keys[i]);

				string[] shortcut = new string[2];
				shortcut[0] = keys[i];
				shortcut[1] = data;
				ShortcutList.Add(shortcut);
			}
		}

		// TODO iniFileをSettings内で生成するように修正
		/// <summary>
		/// INIファイルの生成
		/// </summary>
		private void CreateIniFile(string iniFileName)
		{
			string str = @"/**************************************************************************/
/* ここからは、Ｐｌａｙｅｒの設定です
/* 全ての値は、デフォルト値となります
/**************************************************************************/
[Player]

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
Frame =True

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
RlayCutOnClose =Flase

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
Escape = Close
T = TopMost
A = ResBox
S = StatusLabel
D = Frame
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
Right->LeftClick = Mute
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
	}
}
