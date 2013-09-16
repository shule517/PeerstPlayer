using System;
using System.Drawing;
using System.Windows.Forms;
using PeerstLib.Controls;
using PeerstLib.PeerCast.Data;
using PeerstLib.Util;
using PeerstPlayer.Controls.PecaPlayer;
using PeerstPlayer.Forms.Player;
using PeerstPlayer.Shortcut;
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

		// マウスジェスチャーの感度
		const int MouseGestureInterval = 10;
		
		// 動画ステータスの更新間隔
		const int MovieStatusUpdateInterval = 500;

		//-------------------------------------------------------------
		// コンストラクタ
		//-------------------------------------------------------------
		public PlayerView()
		{
			InitializeComponent();

			Shown += (senderObject, eventArg) =>
			{
				// コマンドライン引数のログ出力
				string[] commandLine = Environment.GetCommandLineArgs();
				foreach (string arg in commandLine)
				{
					Logger.Instance.InfoFormat("コマンドライン引数[{0}]", arg);
				}

				// チャンネル名表示
				if (Environment.GetCommandLineArgs().Length > 2)
				{
					string name = commandLine[2];
					Logger.Instance.InfoFormat("チャンネル名:{0}", name);
					statusBar.ChannelDetail = name;
					Text = name;
				}

				// チャンネル名設定後、画面表示
				Application.DoEvents();

				// 動画再生
				if (Environment.GetCommandLineArgs().Length > 1)
				{
					string url = commandLine[1];
					Logger.Instance.InfoFormat("動画再生[url:{0}]", url);
					Open(url);
				}

				// ショートカットの初期化
				shortcut.Init(this, pecaPlayer, statusBar);

				// イベントの初期化
				InitEvent();

				// 設定の読み込み
				LoadSetting();

				// ウィンドウスナップ
				new WindowSnap(this, pecaPlayer);
			};
		}

		/// <summary>
		/// 設定の読み込み
		/// </summary>
		private void LoadSetting()
		{
			// 設定の読み込み
			PlayerSettings.Load();

			// 書き込み欄の非表示
			statusBar.WriteFieldVisible = PlayerSettings.WriteFieldVisible;
			if (statusBar.WriteFieldVisible)
			{
				statusBar.Focus();
			}

			// 最前列表示
			TopMost = PlayerSettings.TopMost;
		}

		//-------------------------------------------------------------
		// 概要：URLを開く
		//-------------------------------------------------------------
		public void Open(string url)
		{
			Logger.Instance.DebugFormat("Open(url:{0})", url);
			Text = string.Empty;
			pecaPlayer.Open(url);
		}

		//-------------------------------------------------------------
		// イベントの初期化
		//-------------------------------------------------------------
		private void InitEvent()
		{
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
				statusBar.ChannelDetail = String.Format("{0} {1}{2} {3}", info.Name, (string.IsNullOrEmpty(info.Genre) ? "" : string.Format("[{0}] ", info.Genre)), info.Desc, info.Comment);

				// 初回のみの設定
				if (isFirst)
				{
					// コンタクトURL設定
					// TODO コンタクトURLが変更されたら、通知後にURL変更
					statusBar.SelectThreadUrl = info.Url;
					isFirst = false;

					// タイトル設定
					Text = info.Name;
				}
			};

			// ステータスバーのサイズ変更
			statusBar.HeightChanged += (sender, e) =>
			{
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
				// 幅
				pecaPlayer.Width = ClientSize.Width;
				statusBar.Width = ClientSize.Width;

				// 高さ
				pecaPlayer.Height = ClientSize.Height - statusBar.Height;
				statusBar.Top = pecaPlayer.Bottom;
			};

			// 音量変更イベント
			pecaPlayer.VolumeChange += (sender, e) => statusBar.Volume = (pecaPlayer.Mute ? "-" : pecaPlayer.Volume.ToString());

			// スレッドタイトル右クリックイベント
			statusBar.ThreadTitleRightClick += (sender, e) => shortcut.RaiseEvent(ShortcutEvents.ThreadTitleRightClick);

			// マウスジェスチャー
			MouseGesture mouseGesture = new MouseGesture();
			mouseGesture.Interval = MouseGestureInterval;
			bool isGesturing = false;

			// タイマーイベント
			Timer timer = new Timer();
			timer.Interval = MovieStatusUpdateInterval;
			timer.Tick += (sender, e) =>
			{
				if (!RectangleToScreen(ClientRectangle).Contains(MousePosition))
				{
					// 自動表示ボタンの表示切り替え
					if (toolStrip.Visible)
					{
						toolStrip.Visible = false;
					}

					// 画面外に出たらマウスジェスチャー解除
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
			};
			timer.Start();

			// マウスダウンイベント
			pecaPlayer.MouseDownEvent += (sender, e) =>
			{
				if (e.nButton == (short)Keys.LButton)
				{
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
						statusBar.ChannelDetail = String.Format("{0} {1}{2} {3}", info.Name, (string.IsNullOrEmpty(info.Genre) ? "" : string.Format("[{0}] ", info.Genre)), info.Desc, info.Comment);
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
				if (!toolStrip.Visible)
				{
					toolStrip.Visible = true;
				}

				if (isGesturing)
				{
					// ジェスチャー表示
					mouseGesture.Moving(new Point(e.fX, e.fY));

					string gesture = mouseGesture.ToString();
					string detail = shortcut.GetGestureDetail(gesture);
					if (!String.IsNullOrEmpty(gesture))
					{
						statusBar.ChannelDetail = string.Format("マウスジェスチャ： {0} {1}", gesture, (String.IsNullOrEmpty(detail) ? "" : "(" + detail + ")"));
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

			//-----------------------------------------------------
			// コンテキストメニュー
			//-----------------------------------------------------

			// 設定メニュー押下
			settingToolStripMenuItem.Click += (sender, e) =>
			{
				PlayerSettingView view = new PlayerSettingView();
				view.ShowDialog();
			};

			// WMPメニュー押下
			wmpMenuToolStripMenuItem.Click += (sender, e) =>
			{
				shortcut.ExecCommand(new ShortcutInfo(ShortcutCommands.WmpMenu, new CommandArgs()));
			};
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
				param.Style &= ~WS_CAPTION;
				return param;
			}
		}
	}
}
