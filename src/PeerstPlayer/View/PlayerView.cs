using HongliangSoft.Utilities.Gui;
using PeerstLib.Control;
using PeerstLib.PeerCast;
using PeerstPlayer.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WMPLib;
using PeerstLib.Form;

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
			WheelUp,		// ホイールUp
			WheelDown,		// ホイールDown
			Mute,			// ミュートボタン押下
			DoubleClick,	// ダブルクリック
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
			VisibleStatusBar,		// ステータスバーの表示切り替え
		};

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// コマンドMap (コマンド -> 実行処理)
		private Dictionary<Command, Action> commandMap = new Dictionary<Command, Action>();

		// イベントMap (イベント -> コマンド)
		private Dictionary<Event, Command> eventMap = new Dictionary<Event, Command>();

		//-------------------------------------------------------------
		// コンストラクタ
		//-------------------------------------------------------------
		public PlayerView()
		{
			InitializeComponent();

			// TODO デバッグ開始

			// 最小化ボタン
			minToolStripButton.Click += (sender, e) => ExecCommand(Command.WindowMinimization);
			// 最大化ボタン
			maxToolStripButton.Click += (sender, e) => ExecCommand(Command.WindowMaximize);
			// 閉じるボタン
			closeToolStripButton.Click += (sender, e) => ExecCommand(Command.Close);
			// 音量クリック
			statusBar.VolumeClick += (sender, e) => OnEvent(Event.Mute);
			// ダブルクリック
			pecaPlayer.DoubleClickEvent += (sender, e) => OnEvent(Event.DoubleClick);
			// マウスホイール
			MouseWheel += (sender, e) =>
			{
				// 音量変更
				if (e.Delta > 0)
				{
					OnEvent(Event.WheelUp);
				}
				else if (e.Delta < 0)
				{
					OnEvent(Event.WheelDown);
				}
			};

			// チャンネル情報更新
			bool isFirst = true;
			pecaPlayer.ChannelInfoChange += (sender, e) =>
			{
				ChannelInfo info = pecaPlayer.ChannelInfo;
				// TODO 文字が空の場合は、空白を開けない
				statusBar.ChannelDetail = String.Format("{0} [{1}] {2} {3}", info.Name, info.Genre, info.Desc, info.Comment);

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
					OnEvent(Event.StatusbarLeftClick);
				}
				else if (e.Button == MouseButtons.Right)
				{
					OnEvent(Event.StatusbarRightClick);
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

			// タイマーイベント
			Timer timer = new Timer();
			timer.Interval = 1000;
			timer.Tick += (sender, e) =>
			{
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
						case WMPPlayState.wmppsBuffering: statusBar.MovieStatus = "バッファ中"; break;
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

			// マウスジェスチャー
			MouseGesture mouseGesture = new MouseGesture();
			bool isGesturing = false;

			// マウスドラッグ
			pecaPlayer.MouseDownEvent += (sender, e) =>
			{
				if (e.nButton == (short)Keys.LButton)
				{
					FormUtility.WindowDragStart(this.Handle);
				}
				else
				{
					mouseGesture.Start();
					isGesturing = true;
				}
			};

			MouseHook mouseHook = new MouseHook(MouseHook.HookType.GlobalHook);
			mouseHook.MouseHooked += (sender, e) =>
			{
				if (e.Message == MouseMessage.Move)
				{
					if (isGesturing)
					{
						// ジェスチャー表示
						mouseGesture.Moving(e.Point);
						statusBar.ChannelDetail = mouseGesture.ToString();
					}

					// 自動表示ボタンの表示切り替え
					if (RectangleToScreen(ClientRectangle).Contains(MousePosition))
					{
						toolStrip.Visible = true;
					}
					else
					{
						toolStrip.Visible = false;
					}
				}
				else if (e.Message == MouseMessage.RUp)
				{
					// TODO ジェスチャーを実行：statusBar.ChannelDetail = mouseGesture.ToString();
					ChannelInfo info = pecaPlayer.ChannelInfo;
					if (info != null)
					{
						statusBar.ChannelDetail = string.Format("{0} [{1}] {2}", info.Name, info.Genre, info.Desc);
					}

					if (mouseGesture.ToString() == "↓→")
					{
						ExecCommand(Command.Close);
					}
					else if (mouseGesture.ToString() == "↓")
					{
						ExecCommand(Command.OpenPeerstViewer);
					}

					isGesturing = false;
				}
			};

			// アスペクト比維持
			new AspectRateKeepWindow(this.Handle);

			// 書き込み欄の非表示
			statusBar.WriteFieldVisible = false;

			// コマンド作成
			createEvent();
			createCommand();

			// TODO デバッグ終了
		}

		//-------------------------------------------------------------
		// 概要：URLを開く
		//-------------------------------------------------------------
		public void Open(string url)
		{
			pecaPlayer.Open(url);
		}

		//-------------------------------------------------------------
		// 概要：イベント実行
		//-------------------------------------------------------------
		private void OnEvent(Event eventId)
		{
			Command commandId = eventMap[eventId];
			ExecCommand(commandId);
		}

		//-------------------------------------------------------------
		// 概要：コマンド実行
		//-------------------------------------------------------------
		private void ExecCommand(Command commandId)
		{
			commandMap[commandId]();
		}

		//-------------------------------------------------------------
		// 概要：イベント登録
		//-------------------------------------------------------------
		private void createEvent()
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
		private void createCommand()
		{
			// 音量UP
			commandMap.Add(Command.VolumeUp, () => pecaPlayer.Volume += 10);
			// 音量UP
			commandMap.Add(Command.VolumeDown, () => pecaPlayer.Volume -= 10);
			// ミュート切替
			commandMap.Add(Command.Mute, () => pecaPlayer.Mute = !pecaPlayer.Mute);
			// ウィンドウを最小化
			commandMap.Add(Command.WindowMinimization, () => WindowState = FormWindowState.Minimized);
			// 閉じる
			commandMap.Add(Command.Close, () => Close());
			// ステータスバーの表示切り替え
			commandMap.Add(Command.VisibleStatusBar, () => statusBar.WriteFieldVisible = !statusBar.WriteFieldVisible);
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
				ChannelInfo info = pecaPlayer.ChannelInfo;
				if (info == null)
				{
					return;
				}

				// TODO スレッド選択しているスレッドURLを指定する
				Process.Start(@"C:\Users\Shule517\Desktop\研究用_リムーバブルディスク\姉ちゃん\Tool\PeerstPlayer Pocket 0.13\PeerstViewer.exe",
					info.Url);
			});
		}
	}
}
