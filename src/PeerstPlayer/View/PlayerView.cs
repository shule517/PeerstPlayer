using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PeerstLib.Control;
using PeerstLib.Form;
using PeerstLib.PeerCast;
using PeerstLib.Utility;
using WMPLib;

namespace PeerstPlayer
{
	//-------------------------------------------------------------
	// 概要：動画プレイヤー画面表示クラス
	//-------------------------------------------------------------
	public partial class PlayerView : Form
	{
		// イベント定義
		enum Event
		{
			WheelUp,				// ホイールUp
			WheelDown,				// ホイールDown
			Mute,					// ミュートボタン押下
			DoubleClick,			// ダブルクリック
			StatusbarRightClick,	// ステータスバー右クリック
			StatusbarLeftClick,
		};

		// コマンド実行
		enum Command
		{
			VolumeUp,			// 音量Up
			VolumeDown,			// 音量Down
			Mute,				// ミュート切り替え
			WindowMaximize,		// ウィンドウ最大化
			WindowMinimization,	// ウィンドウ最小化
			Close,				// 閉じる
			OpenPeerstViewer,	// ビューワを開く
			VisibleStatusBar,	// ステータスバーの表示切り替え
		};

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// コマンドMap (コマンド -> 実行処理)
		private Dictionary<Command, Action> commandMap = new Dictionary<Command, Action>();

		// イベントMap (イベント -> コマンド)
		private Dictionary<Event, Command> eventMap = new Dictionary<Event, Command>();

		// マウスジェスチャーMap (ジェスチャー -> コマンド)
		private Dictionary<string, Command> gestureMap = new Dictionary<string, Command>();

		//-------------------------------------------------------------
		// コンストラクタ
		//-------------------------------------------------------------
		public PlayerView()
		{
			InitializeComponent();

			// TODO デバッグ開始

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

				// 最小化ボタン
				minToolStripButton.Click += (sender, e) => ExecCommand(Command.WindowMinimization);
				// 最大化ボタン
				maxToolStripButton.Click += (sender, e) => ExecCommand(Command.WindowMaximize);
				// 閉じるボタン
				closeToolStripButton.Click += (sender, e) => ExecCommand(Command.Close);
				// 音量クリック
				statusBar.VolumeClick += (sender, e) => RaiseEvent(Event.Mute);
				// ダブルクリック
				pecaPlayer.DoubleClickEvent += (sender, e) => RaiseEvent(Event.DoubleClick);
				// マウスホイール
				MouseWheel += (sender, e) =>
				{
					// 音量変更
					if (e.Delta > 0)
					{
						RaiseEvent(Event.WheelUp);
					}
					else if (e.Delta < 0)
					{
						RaiseEvent(Event.WheelDown);
					}
				};

				// チャンネル情報更新
				bool isFirst = true;
				pecaPlayer.ChannelInfoChange += (sender, e) =>
				{
					ChannelInfo info = pecaPlayer.ChannelInfo;
					// TODO 文字が空の場合は、空白を開けない
					statusBar.ChannelDetail = String.Format("{0} {1}{2} {3}", info.Name, string.IsNullOrEmpty(info.Genre) ? "" : string.Format("[{0}] ", info.Genre), info.Desc, info.Comment);

					// TODO 初回だけコンタクトURLを設定する
					if (isFirst)
					{
						statusBar.SelectThreadUrl = info.Url;
						isFirst = false;
					}
				};

				// ステータスバーのサイズ変更
				statusBar.HeightChanged += (sender, e) =>
				{
					this.ClientSize = new Size(ClientSize.Width, pecaPlayer.Height + statusBar.Height);
				};

				// ステータスバーのクリックイベント
				statusBar.ChannelDetailClick += (sender, e) =>
				{
					if (e.Button == MouseButtons.Left)
					{
						RaiseEvent(Event.StatusbarLeftClick);
					}
					else if (e.Button == MouseButtons.Right)
					{
						RaiseEvent(Event.StatusbarRightClick);
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
				pecaPlayer.VolumeChange += (sender, e) =>
					statusBar.Volume = pecaPlayer.Mute ? "-" : pecaPlayer.Volume.ToString();

				// スレッドタイトル右クリックイベント
				statusBar.ThreadTitleRightClick += (sender, e) => ExecCommand(Command.OpenPeerstViewer);

				// マウスジェスチャー
				MouseGesture mouseGesture = new MouseGesture();
				mouseGesture.Interval = 10;
				bool isGesturing = false;

				// タイマーイベント
				Timer timer = new Timer();
				timer.Interval = 500;
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
							case WMPPlayState.wmppsUndefined: statusBar.MovieStatus = ""; break;
							case WMPPlayState.wmppsStopped: statusBar.MovieStatus = "停止"; break;
							case WMPPlayState.wmppsPaused: statusBar.MovieStatus = "一時停止"; break;
							case WMPPlayState.wmppsPlaying: statusBar.MovieStatus = "再生中"; break;
							case WMPPlayState.wmppsScanForward: statusBar.MovieStatus = "早送り"; break;
							case WMPPlayState.wmppsScanReverse: statusBar.MovieStatus = "巻き戻し"; break;
							case WMPPlayState.wmppsBuffering: statusBar.MovieStatus = string.Format("バッファ{0}%", pecaPlayer.BufferingProgress); break;
							case WMPPlayState.wmppsWaiting: statusBar.MovieStatus = "接続待機"; break;
							case WMPPlayState.wmppsMediaEnded: statusBar.MovieStatus = "再生完了"; break;
							case WMPPlayState.wmppsTransitioning: statusBar.MovieStatus = "準備中"; break;
							case WMPPlayState.wmppsReady: statusBar.MovieStatus = "準備完了"; break;
							case WMPPlayState.wmppsReconnecting: statusBar.MovieStatus = "再接続"; break;
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
							statusBar.ChannelDetail = String.Format("{0} {1}{2} {3}", info.Name, string.IsNullOrEmpty(info.Genre) ? "" : string.Format("[{0}] ", info.Genre), info.Desc, info.Comment);
						}

						// マウスジェスチャーが実行されていなければ
						string gesture = mouseGesture.ToString();
						if (string.IsNullOrEmpty(gesture))
						{
							// コンテキストメニュー表示
							pecaPlayer.EnableContextMenu = true;
							FormUtility.ShowContextMenu(this.pecaPlayer.WMPHandle, MousePosition);
							pecaPlayer.EnableContextMenu = false;
						}
						else if (isGesturing)
						{
							// マウスジェスチャー実行
							ExecGesture(gesture);
						}

						isGesturing = false;
					}
				};

				// マウスムーズイベント
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
						Command commandId;
						if (gestureMap.TryGetValue(gesture, out commandId))
						{
							statusBar.ChannelDetail = string.Format("マウスジェスチャ：{0}({1})", gesture, commandId);
							return;
						}
						statusBar.ChannelDetail = string.Format("マウスジェスチャ：{0}", gesture);
					}
				};

				// 終了処理
				FormClosed += (sender, e) =>
				{
					Visible = false;
					Logger.Instance.Debug("FormClosed");
					//mouseHook.Dispose();
					pecaPlayer.Close();
					statusBar.Close();
				};

				// ウィンドウスナップ
				new SnapWindow(this, pecaPlayer);

				// 書き込み欄の非表示
				statusBar.WriteFieldVisible = false;

				// コマンド作成
				CreateEvent();
				CreateCommand();
				CreateGesture();
			};

			// TODO デバッグ終了
		}

		//-------------------------------------------------------------
		// 概要：マウスジェスチャーの設定作成
		//-------------------------------------------------------------
		private void CreateGesture()
		{
			gestureMap.Add("↓→", Command.Close);
			gestureMap.Add("↓", Command.OpenPeerstViewer);
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
		// 概要：イベント実行
		//-------------------------------------------------------------
		private void RaiseEvent(Event eventId)
		{
			Logger.Instance.InfoFormat("イベント実行 [イベントID:{0}]", eventId);
			Command commandId = eventMap[eventId];
			ExecCommand(commandId);
		}

		//-------------------------------------------------------------
		// 概要：マウスジェスチャー実行
		//-------------------------------------------------------------
		private void ExecGesture(string gesture)
		{
			Command commandId;
			if (gestureMap.TryGetValue(gesture, out commandId))
			{
				Logger.Instance.InfoFormat("マウスジェスチャー実行 [ジェスチャー:{0}, コマンドID:{1}]", gesture, commandId);
				ExecCommand(commandId);
			}
		}

		//-------------------------------------------------------------
		// 概要：コマンド実行
		//-------------------------------------------------------------
		private void ExecCommand(Command commandId)
		{
			Logger.Instance.InfoFormat("コマンド実行 [コマンドID:{0}]", commandId);
			commandMap[commandId]();
		}

		//-------------------------------------------------------------
		// 概要：イベント登録
		//-------------------------------------------------------------
		private void CreateEvent()
		{
			// TODO 設定によって切り替えを行う
			eventMap.Add(Event.WheelUp, Command.VolumeUp);
			eventMap.Add(Event.WheelDown, Command.VolumeDown);
			eventMap.Add(Event.Mute, Command.Mute);
			eventMap.Add(Event.DoubleClick, Command.WindowMaximize);
			eventMap.Add(Event.StatusbarRightClick, Command.OpenPeerstViewer);
			eventMap.Add(Event.StatusbarLeftClick, Command.VisibleStatusBar);
		}

		//-------------------------------------------------------------
		// 概要：コマンド作成
		//-------------------------------------------------------------
		private void CreateCommand()
		{
			// 音量UP
			commandMap.Add(Command.VolumeUp, () =>
			{
				if (ModifierKeys == Keys.Shift)
				{
					pecaPlayer.Volume += 1;
				}
				else if (ModifierKeys == Keys.Control)
				{
					pecaPlayer.Volume += 5;
				}
				else
				{
					pecaPlayer.Volume += 10;
				}
			});
			// 音量UP
			commandMap.Add(Command.VolumeDown, () =>
			{
				if (ModifierKeys == Keys.Shift)
				{
					pecaPlayer.Volume -= 1;
				}
				else if (ModifierKeys == Keys.Control)
				{
					pecaPlayer.Volume -= 5;
				}
				else
				{
					pecaPlayer.Volume -= 10;
				}
			});
			// ミュート切替
			commandMap.Add(Command.Mute, () => pecaPlayer.Mute = !pecaPlayer.Mute);
			// ウィンドウを最小化
			commandMap.Add(Command.WindowMinimization, () => WindowState = FormWindowState.Minimized);
			// 閉じる
			commandMap.Add(Command.Close, () => Close());
			// ステータスバーの表示切り替え
			commandMap.Add(Command.VisibleStatusBar, () =>
			{
				statusBar.WriteFieldVisible = !statusBar.WriteFieldVisible;
				if (statusBar.WriteFieldVisible)
				{
					statusBar.Focus();
				}
			});
			// ウィンドウを最大化
			commandMap.Add(Command.WindowMaximize, () =>
			{
				if (WindowState == FormWindowState.Normal)
				{
					WindowState = FormWindowState.Maximized;
				}
				else
				{
					WindowState = FormWindowState.Normal;
				}
			});
			// PeerstViewerを開く
			commandMap.Add(Command.OpenPeerstViewer, () =>
			{
				// TODO スレッド選択しているスレッドURLを指定する
				string viewerExePath = Path.Combine(Environment.CurrentDirectory, "PeerstViewer.exe");
				string param = statusBar.SelectThreadUrl;
				Logger.Instance.InfoFormat("PeerstViewer起動 [viewerExePath:{0} param:{1}]", viewerExePath, param);
				Process.Start(viewerExePath, param);
			});
		}
	}
}
