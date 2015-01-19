using PeerstLib.Controls;
using PeerstLib.PeerCast.Data;
using PeerstLib.Util;
using PeerstPlayer.Controls.PecaPlayer;
using PeerstPlayer.Controls.StatusBar;
using PeerstPlayer.Forms.Player;
using PeerstPlayer.Shortcut;
using System;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;

namespace PeerstPlayer.Forms.Setting
{
	//-------------------------------------------------------------
	// 概要：動画プレイヤー画面表示クラス
	//-------------------------------------------------------------
	public partial class PlayerView : Form
	{
		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// ショートカットコマンド
		ShortcutManager shortcut = new ShortcutManager();

		//-------------------------------------------------------------
		// 定義
		//-------------------------------------------------------------
		
		// 動画ステータスの更新間隔
		const int MovieStatusUpdateInterval = 500;

		//-------------------------------------------------------------
		// コンストラクタ
		//-------------------------------------------------------------
		public PlayerView()
		{
			InitializeComponent();

			// Toastの初期化
			ToastMessage.Init(this.statusBar);

			// コマンドライン引数のログ出力
			string[] commandLine = Environment.GetCommandLineArgs();
			foreach (string arg in commandLine)
			{
				Logger.Instance.InfoFormat("コマンドライン引数[{0}]", arg);
			}

			// チャンネル名表示
			if (Environment.GetCommandLineArgs().Length > 3)
			{
				string name = commandLine[3];
				Logger.Instance.InfoFormat("チャンネル名:{0}", name);
				statusBar.ChannelDetail = name;
				Win32API.SetWindowText(Handle, String.Format("{0} - PeerstPlayer", name));
			}
			else
			{
				// タイトル設定
				Win32API.SetWindowText(Handle, "PeerstPlayer");
			}

			// イベントの初期化
			InitEvent();

			// 設定の読み込み
			LoadSetting();

			Shown += (senderObject, eventArg) =>
			{
				Logger.Instance.InfoFormat("画面表示 - Shownイベント開始");

				// 設定の反映
				// 書き込み欄の非表示
				statusBar.WriteFieldVisible = PlayerSettings.WriteFieldVisible;
				if (statusBar.WriteFieldVisible)
				{
					statusBar.Focus();
				}

				// チャンネル名設定後、画面表示
				Application.DoEvents();

				// プレイヤーをフォーカス
				pecaPlayer.Focus();

				// 動画再生
				if (Environment.GetCommandLineArgs().Length > 1)
				{
					string url = commandLine[1];
					Logger.Instance.InfoFormat("動画再生[url:{0}]", url);
					Open(url);
				}

				// ショートカットの初期化
				shortcut.Init(this, pecaPlayer, statusBar);

				// ウィンドウスナップ
				new WindowSnap(this, pecaPlayer);

				// アスペクト比維持
				new AspectRateKeepWindow(this, pecaPlayer);

				// WMPを使用していなければ「WMPメニュー」を非表示にする
				if (!pecaPlayer.UsedWMP)
				{
					wmpMenuToolStripMenuItem.Visible = false;
				}
				// Flashを使用していなければFlash用のメニューを非表示にする
				if (!pecaPlayer.UsedFlash)
				{
					showDebugToolStripMenuItem.Visible = false;
				}
			};
		}

		/// <summary>
		/// 設定の読み込み
		/// </summary>
		private void LoadSetting()
		{
			// 設定の読み込み
			PlayerSettings.Load();

			// ウィンドウ枠を消す
			if (PlayerSettings.FrameInvisible)
			{
				FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			}

			// 起動時にウィンドウサイズを復帰する
			if (PlayerSettings.ReturnSizeOnStart)
			{
				pecaPlayer.SetSize(PlayerSettings.ReturnSize.Width, PlayerSettings.ReturnSize.Height);
			}

			// 起動時にウィンドウ位置を復帰する
			if (PlayerSettings.ReturnPositionOnStart)
			{
				Location = PlayerSettings.ReturnPosition;
			}

			// 最前列表示
			TopMost = PlayerSettings.TopMost;

			// 初期音量設定
			pecaPlayer.Volume = PlayerSettings.InitVolume;
		}

		//-------------------------------------------------------------
		// 概要：URLを開く
		//-------------------------------------------------------------
		public void Open(string url)
		{
			Logger.Instance.DebugFormat("Open(url:{0})", url);
			pecaPlayer.Open(url);
		}

		//-------------------------------------------------------------
		// イベントの初期化
		//-------------------------------------------------------------
		private void InitEvent()
		{
			// スレッドビューワを開く
			openViewerToolStripButton.Click += (sender, e) => shortcut.ExecCommand(Commands.OpenPeerstViewer);
			// 最小化ボタン
			minToolStripButton.Click += (sender, e) => shortcut.RaiseEvent(ShortcutEvents.MinButtonClick);
			// 最大化ボタン
			maxToolStripButton.Click += (sender, e) => shortcut.RaiseEvent(ShortcutEvents.MaxButtonClick);
			// 閉じるボタン
			closeToolStripButton.Click += (sender, e) => shortcut.RaiseEvent(ShortcutEvents.CloseButtonClick);
			// 音量クリック
			statusBar.VolumeClick += (sender, e) => shortcut.RaiseEvent(ShortcutEvents.Mute);
			// ダブルクリック
			pecaPlayer.DoubleClickEvent += (sender, e) => shortcut.RaiseEvent(ShortcutEvents.DoubleClick);
			// マウスホイール
			MouseWheel += (sender, e) =>
			{
				if ((Control.MouseButtons & MouseButtons.Right) == MouseButtons.Right)
				{
					// 右クリックマウスホイール
					if (e.Delta > 0)
					{
						shortcut.RaiseEvent(ShortcutEvents.RightClickWheelUp);
					}
					else if (e.Delta < 0)
					{
						shortcut.RaiseEvent(ShortcutEvents.RightClickWheelDown);
					}
				}
				else
				{
					// マウスホイール
					if (e.Delta > 0)
					{
						shortcut.RaiseEvent(ShortcutEvents.WheelUp);
					}
					else if (e.Delta < 0)
					{
						shortcut.RaiseEvent(ShortcutEvents.WheelDown);
					}
				}
			};

			// チャンネル情報更新
			bool isFirst = true;
			pecaPlayer.ChannelInfoChange += (sender, e) =>
			{
				ChannelInfo info = pecaPlayer.ChannelInfo;
				// TODO 文字が空の場合は、スペースを空けない
				UpdateChannelDetail(info);

				// タイトル設定
				string chName = getChannelName(info);
				Win32API.SetWindowText(Handle, String.Format("{0} - PeerstPlayer", chName));

				// 初回のみの設定
				if (isFirst)
				{
					// コンタクトURL設定
					// TODO コンタクトURLが変更されたら、通知後にURL変更
					statusBar.SelectThreadUrl = info.Url;
					isFirst = false;
				}
			};

			// ステータスバーのサイズ変更
			statusBar.HeightChanged += (sender, e) =>
			{
				// 最大化時はサイズ変更しない
				if (WindowState == FormWindowState.Maximized)
				{
					return;
				}
				this.ClientSize = new Size(ClientSize.Width, (pecaPlayer.Height + statusBar.Height));
			};

			// ステータスバーのクリックイベント
			statusBar.ChannelDetailClick += (sender, e) =>
			{
				if (e.Button == MouseButtons.Left)
				{
					shortcut.RaiseEvent(ShortcutEvents.StatusbarLeftClick);
				}
				else if (e.Button == MouseButtons.Right)
				{
					shortcut.RaiseEvent(ShortcutEvents.StatusbarRightClick);
				}
			};

			// サイズ変更
			SizeChanged += (sender, e) =>
			{
				if ((ClientSize.Width) == 0 || (ClientSize.Height == 0))
				{
					return;
				}

				// 幅
				pecaPlayer.Width = ClientSize.Width;
				statusBar.Width = ClientSize.Width;

				// 高さ
				pecaPlayer.Height = ClientSize.Height - statusBar.Height;
				statusBar.Top = pecaPlayer.Bottom;
			};

			// 動画サイズ変更
			pecaPlayer.MovieSizeChange += (sender, e) =>
			{
				// Formサイズ変更
				ClientSize = new Size(pecaPlayer.Width, pecaPlayer.Height + statusBar.Height);

				// 幅
				statusBar.Width = pecaPlayer.Width;

				// 高さ
				statusBar.Top = pecaPlayer.Bottom;
			};

			// 音量変更イベント
			pecaPlayer.VolumeChange += (sender, e) => statusBar.Volume = (pecaPlayer.Mute ? "-" : pecaPlayer.Volume.ToString());

			// スレッドタイトル右クリックイベント
			statusBar.ThreadTitleRightClick += (sender, e) => shortcut.RaiseEvent(ShortcutEvents.ThreadTitleRightClick);

			// マウスジェスチャー
			MouseGesture mouseGesture = new MouseGesture();
			mouseGesture.Interval = PlayerSettings.MouseGestureInterval;
			bool isGesturing = false;

			// タイマーイベント
			Timer timer = new Timer();
			timer.Interval = MovieStatusUpdateInterval;
			timer.Tick += (sender, e) =>
			{
				// 自動表示ボタンの表示切り替え
				if (toolStrip.Visible && !IsVisibleToolStrip(PointToClient(MousePosition)))
				{
					toolStrip.Visible = false;
				}

				// 画面外に出たらマウスジェスチャー解除
				if (!RectangleToScreen(ClientRectangle).Contains(MousePosition))
				{
					isGesturing = false;
				}

				// 動画ステータスを表示
				if (pecaPlayer.Duration.Length == 0)
				{
					switch (pecaPlayer.PlayState)
					{
						case WMPPlayState.wmppsUndefined: statusBar.MovieStatus		= "";			break;
						case WMPPlayState.wmppsStopped: statusBar.MovieStatus		= "停止";		break;
						case WMPPlayState.wmppsPaused: statusBar.MovieStatus		= "一時停止";	break;
						case WMPPlayState.wmppsPlaying: statusBar.MovieStatus		= "再生中";		break;
						case WMPPlayState.wmppsScanForward: statusBar.MovieStatus	= "早送り";		break;
						case WMPPlayState.wmppsScanReverse: statusBar.MovieStatus	= "巻き戻し";	break;
						case WMPPlayState.wmppsWaiting: statusBar.MovieStatus		= "接続待機";	break;
						case WMPPlayState.wmppsMediaEnded: statusBar.MovieStatus	= "再生完了";	break;
						case WMPPlayState.wmppsTransitioning: statusBar.MovieStatus = "準備中";		break;
						case WMPPlayState.wmppsReady: statusBar.MovieStatus			= "準備完了";	break;
						case WMPPlayState.wmppsReconnecting: statusBar.MovieStatus	= "再接続";		break;
						case WMPPlayState.wmppsBuffering: statusBar.MovieStatus		= string.Format("バッファ{0}%", pecaPlayer.BufferingProgress); break;
					}
				}
				// 再生時間を表示
				else
				{
					statusBar.MovieStatus = pecaPlayer.Duration;
				}

				// 動画情報を更新
				ChannelInfo info = pecaPlayer.ChannelInfo ?? new ChannelInfo();
				statusBar.UpdateMovieInfo(
					new MovieInfo()
					{
						NowFps = pecaPlayer.NowFrameRate,
						Fps = pecaPlayer.FrameRate,
						NowBitrate = pecaPlayer.NowBitrate,
						Bitrate = pecaPlayer.Bitrate,
						ListenerNumber = info.Listeners,
						RelayNumber = info.Relays,
						Status = info.Status,
						StreamType = info.Type,
					});
			};
			timer.Start();

			// マウスダウンイベント
			pecaPlayer.MouseDownEvent += (sender, e) =>
			{
				if (e.nButton == (short)Keys.LButton)
				{
					// VLCだとコンテキストメニューが自動で閉じないので手動で閉じる
					contextMenuStrip.Close();
					// マウスドラッグ
					FormUtility.WindowDragStart(this.Handle);
				}
				else if (e.nButton == (short)Keys.RButton)
				{
					// マウスジェスチャー開始
					mouseGesture.Start();
					isGesturing = true;
				}
				else if (e.nButton == (short)Keys.MButton)
				{
					// 中クリック
					shortcut.RaiseEvent(ShortcutEvents.MiddleClick);
				}
			};

			// マウスアップイベント
			pecaPlayer.MouseUpEvent += (sender, e) =>
			{
				if (e.nButton == (short)Keys.RButton)
				{
					// チャンネル詳細を再描画
					ChannelInfo info = pecaPlayer.ChannelInfo;
					if (info != null)
					{
						UpdateChannelDetail(info);
					}

					// マウスジェスチャーが実行されていなければ
					string gesture = mouseGesture.ToString();
					if (string.IsNullOrEmpty(gesture))
					{
						// コンテキストメニュー表示
						contextMenuStrip.Show(MousePosition);
					}
					else if (isGesturing)
					{
						// マウスジェスチャー実行
						shortcut.ExecGesture(gesture);
					}

					isGesturing = false;
				}
			};

			// マウス移動イベント
			pecaPlayer.MouseMoveEvent += (sender, e) =>
			{
				// 自動表示ボタンの表示切り替え
				if (toolStrip.Visible != IsVisibleToolStrip(new Point(e.fX, e.fY)))
				{
					toolStrip.Visible = !toolStrip.Visible;
				}

				if (isGesturing)
				{
					// ジェスチャー表示
					mouseGesture.Moving(new Point(e.fX, e.fY));

					string gesture = mouseGesture.ToString();
					string detail = shortcut.GetGestureDetail(gesture);
					if (!String.IsNullOrEmpty(gesture))
					{
						string message = string.Format("ジェスチャ： {0} {1}", gesture, (String.IsNullOrEmpty(detail) ? "" : "(" + detail + ")"));
						ToastMessage.Show(message);
					}
				}
			};

			// キー押下イベント
			pecaPlayer.KeyDownEvent += (sender, e) => shortcut.RaiseKeyEvent(e);

			// ステータスバー マウスホバーイベント
			statusBar.MouseHoverEvent += (sender, e) => shortcut.RaiseEvent(ShortcutEvents.StatusbarHover);

			// 終了処理
			FormClosed += (sender, e) =>
			{
				Visible = false;
				Logger.Instance.Debug("FormClosed");
				pecaPlayer.Close();
				statusBar.Close();
			};

			// 動画再生イベント
			pecaPlayer.MovieStart += (sender, e) =>
			{
				pecaPlayer.UpdateChannelInfo();
				shortcut.ExecCommand(PlayerSettings.MovieStartCommand);
			};

			// コマンド実行内容の表示
			shortcut.CommandExecuted += (sender, e) =>
			{
				CommnadExecutedEventArgs args = (CommnadExecutedEventArgs)e;

				// ステータスバー表示切り替えはメッセージを出さない
				if (args.Command != Commands.VisibleStatusBar)
				{
					ToastMessage.Show(string.Format("コマンド： {0}", args.Detail));
				}
			};

			//-----------------------------------------------------
			// コンテキストメニュー
			//-----------------------------------------------------

			// 拡大率
			scale25PerToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WindowScale25Per);
			scale50PerToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WindowScale50Per);
			scale75PerToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WindowScale75Per);
			scale100PerToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WindowScale100Per);
			scale150PerToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WindowScale150Per);
			scale200PerToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WindowScale200Per);

			// サイズ変更
			size160x120ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WindowSize160x120);
			size320x240ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WindowSize320x240);
			size480x360ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WindowSize480x360);
			size640x480ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WindowSize640x480);
			size800x600ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WindowSize800x600);
			fitMovieSizeToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.FitMovieSize);

			// 画面分割
			screenSplitWidthx5ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.ScreenSplitWidthx5);
			screenSplitWidthx4ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.ScreenSplitWidthx4);
			screenSplitWidthx3ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.ScreenSplitWidthx3);
			screenSplitWidthx2ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.ScreenSplitWidthx2);
			screenSplitHeightx5ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.ScreenSplitHeightx5);
			screenSplitHeightx4ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.ScreenSplitHeightx4);
			screenSplitHeightx3ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.ScreenSplitHeightx3);
			screenSplitHeightx2ToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.ScreenSplitHeightx2);

			// 機能
			topMostToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.TopMost);
			updateChannelInfoToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.UpdateChannelInfo);
			bumpToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.Bump);
			functionToolStripMenuItem.DropDownOpening += (sender, e) => topMostToolStripMenuItem.Checked = TopMost;
			screenshotToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.Screenshot);
			openScreenshotFolderToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.OpenScreenshotFolder);
			// ファイルから開く
			openFromFileToolStripMenuItem.Click += (sender, e) =>
			{
				var dialog = new OpenFileDialog();
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					// TODO スレッド選択を解除
					// TODO ステータスバーを変更
					Open(dialog.FileName);
				}
			};
			// URLから開く
			openFromUrlToolStripTextBox.KeyDown += (sender, e) =>
			{
				if (e.KeyCode == Keys.Return)
				{
					// TODO FLV or WMVの判定をして、PecaPlayerコントロールを再初期化する
					// TODO スレッド選択を解除　＋　新しいスレッドへ移動
					// TODO ステータスバーを変更
					Open(((ToolStripTextBox)sender).Text);
					contextMenuStrip.Close();
				}
			};
			// クリップボードから開く
			openFromClipboardToolStripMenuItem.Click += (sender, e) =>
			{
				try
				{
					if (Clipboard.ContainsText())
					{
						// TODO FLV or WMVの判定をして、PecaPlayerコントロールを再初期化する
						// TODO スレッド選択を解除　＋　新しいスレッドへ移動
						// TODO ステータスバーを変更
						Open(Clipboard.GetText());
					}
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					MessageBox.Show("クリップボードのオープンに失敗しました");
				}
			};

			// 音量
			volumeUpToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.VolumeUp);
			volumeDownToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.VolumeDown);
			muteToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.Mute);
			volumeBalanceLeftToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.VolumeBalanceLeft);
			volumeBalanceMiddleToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.VolumeBalanceMiddle);
			volumeBalanceRightToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.VolumeBalanceRight);
			volumeToolStripMenuItem.DropDownOpening += (sender, e) => muteToolStripMenuItem.Checked = pecaPlayer.Mute;
			volumeToolStripMenuItem.DropDownOpening += (sender, e) => volumeBalanceLeftToolStripMenuItem.Checked = (pecaPlayer.VolumeBalance == VolumeBalanceCommandArgs.BalanceLeft);
			volumeToolStripMenuItem.DropDownOpening += (sender, e) => volumeBalanceMiddleToolStripMenuItem.Checked = (pecaPlayer.VolumeBalance == VolumeBalanceCommandArgs.BalanceMiddle);
			volumeToolStripMenuItem.DropDownOpening += (sender, e) => volumeBalanceRightToolStripMenuItem.Checked = (pecaPlayer.VolumeBalance == VolumeBalanceCommandArgs.BalanceRight);

			// 設定メニュー押下
			settingToolStripMenuItem.Click += (sender, e) =>
			{
				PlayerSettingView view = new PlayerSettingView(shortcut);
				view.TopMost = TopMost;
				view.ShowDialog();
			};

			// WMPメニュー押下
			wmpMenuToolStripMenuItem.Click += (sender, e) => shortcut.ExecCommand(Commands.WmpMenu);
			// 動画情報表示押下
			showDebugToolStripMenuItem.Click += (sender, e) => pecaPlayer.ShowDebug();
		}

		/// <summary>
		/// 自動表示ボタンの表示判定
		/// </summary>
		private bool IsVisibleToolStrip(Point mousePosition)
		{
			// 右上（幅３分の１・高さ３分の１）にマウスがきたら表示する
			return new Rectangle((pecaPlayer.Width / 3 * 2), 0, (pecaPlayer.Width / 3), (pecaPlayer.Height / 3)).Contains(mousePosition);
		}

		/// <summary>
		/// チャンネル詳細を更新
		/// </summary>
		private void UpdateChannelDetail(ChannelInfo info)
		{
			string chName = getChannelName(info);
			statusBar.ChannelDetail = String.Format("{0} {1}{2} {3}", chName, (string.IsNullOrEmpty(info.Genre) ? "" : string.Format("[{0}] ", info.Genre)), info.Desc, info.Comment);
		}

		/// <summary>
		/// チャンネル名を取得
		/// </summary>
		private string getChannelName(ChannelInfo info)
		{
			// 1.チャンネル情報を優先
			if (info.Name != "")
			{
				return info.Name;
			}

			// 2.コマンドライン引数
			if (Environment.GetCommandLineArgs().Length > 3)
			{
				var name = Environment.GetCommandLineArgs()[3];
				return name;
			}

			// 3.データなし→空
			return "";
		}

		/// <summary>
		/// タイトルバー非表示
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				const int WS_CAPTION = 0x00C00000;
				CreateParams param = base.CreateParams;
				param.Style &= ~WS_CAPTION;			// タイトルバー非表示
				return param;
			}
		}
	}
}
