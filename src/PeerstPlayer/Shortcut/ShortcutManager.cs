using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PeerstLib.Util;
using PeerstPlayer.Controls.PecaPlayer;
using PeerstPlayer.Controls.StatusBar;
using PeerstPlayer.Forms.Setting;
using PeerstPlayer.Shortcut.Command;

namespace PeerstPlayer.Shortcut
{
	//-------------------------------------------------------------
	// 概要：ショートカットコマンドクラス
	//-------------------------------------------------------------
	class ShortcutManager
	{
		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// コマンドMap (コマンドID -> コマンドクラスを取得)
		private Dictionary<ShortcutCommands, IShortcutCommand> commandMap = new Dictionary<ShortcutCommands, IShortcutCommand>();

		// イベントMap (イベントID -> コマンドIDを取得)
		private Dictionary<ShortcutEvents, ShortcutInfo> eventMap = new Dictionary<ShortcutEvents, ShortcutInfo>();

		// マウスジェスチャーMap (ジェスチャー -> コマンドIDを取得)
		private Dictionary<string, ShortcutInfo> gestureMap = new Dictionary<string, ShortcutInfo>();

		// キー入力Map (キー入力 -> コマンドIDを取得)
		private Dictionary<Keys, ShortcutInfo> keyMap = new Dictionary<Keys, ShortcutInfo>();

		//-------------------------------------------------------------
		// 概要：初期化
		//-------------------------------------------------------------
		public void Init(PlayerView playerView, PecaPlayerControl pecaPlayer, StatusBarControl statusBar)
		{
			CreateCommand(playerView, pecaPlayer, statusBar);

			// キー設定
			SettingEvent();
			SettingGesture();
			SettingKey();
		}

		//-------------------------------------------------------------
		// 概要：イベント実行
		//-------------------------------------------------------------
		public void RaiseEvent(ShortcutEvents eventId)
		{
			Logger.Instance.InfoFormat("イベント実行 [イベントID:{0}]", eventId);
			ShortcutInfo shortcut;
			if (eventMap.TryGetValue(eventId, out shortcut))
			{
				ExecCommand(shortcut);
			}
			else
			{
				Logger.Instance.ErrorFormat("イベントに紐付くコマンドがありません [eventid : {0}]", eventId);
			}
		}

		//-------------------------------------------------------------
		// 概要：キー押下イベント実行
		//-------------------------------------------------------------
		public void RaiseKeyEvent(AxWMPLib._WMPOCXEvents_KeyDownEvent e)
		{
			// 入力値をKeysへ変換
			Keys keyCode = (Keys)e.nKeyCode;
			Keys modifierKey = (Keys)(e.nShiftState << 16);
			Keys key = keyCode | modifierKey;

			Logger.Instance.InfoFormat("キー押下イベント実行 [イベントID:{0}, keyCode{0}, modifierKey{1}]", keyCode, modifierKey);

			// コマンド実行
			ShortcutInfo shortcut;
			if (keyMap.TryGetValue(key, out shortcut))
			{
				ExecCommand(shortcut);
			}
		}

		//-------------------------------------------------------------
		// 概要：マウスジェスチャー実行
		//-------------------------------------------------------------
		public void ExecGesture(string gesture)
		{
			ShortcutInfo shortcut;
			if (gestureMap.TryGetValue(gesture, out shortcut))
			{
				Logger.Instance.InfoFormat("マウスジェスチャー実行 [ジェスチャー:{0}, コマンドID:{1}]", gesture, shortcut);
				ExecCommand(shortcut);
			}
		}

		//-------------------------------------------------------------
		// 概要：ジェスチャーの詳細を取得
		//-------------------------------------------------------------
		public string GetGestureDetail(string gesture)
		{
			ShortcutInfo shortcut;
			if (gestureMap.TryGetValue(gesture, out shortcut))
			{
				return commandMap[shortcut.command].Detail;
			}

			return String.Empty;
		}

		//-------------------------------------------------------------
		// 概要：コマンド実行
		//-------------------------------------------------------------
		public void ExecCommand(ShortcutInfo shortcut)
		{
			Logger.Instance.InfoFormat("コマンド実行 [コマンドID:{0}]", shortcut.command);
			commandMap[shortcut.command].Execute(shortcut.args);
		}

		//-------------------------------------------------------------
		// 概要：コマンド作成
		//-------------------------------------------------------------
		private void CreateCommand(Form form, PecaPlayerControl pecaPlayer, StatusBarControl statusBar)
		{
			commandMap = new Dictionary<ShortcutCommands, IShortcutCommand>()
			{
				{	ShortcutCommands.VolumeUp,			new VolumeUpCommand(pecaPlayer)					}, // 音量UP
				{	ShortcutCommands.VolumeDown,		new VolumeDownCommand(pecaPlayer)				}, // 音量DOWN
				{	ShortcutCommands.Mute,				new MuteCommand(pecaPlayer)						}, // ミュート切替
				{	ShortcutCommands.WindowMinimize,	new WindowMinimize(form)						}, // ウィンドウを最小化
				{	ShortcutCommands.WindowMaximize,	new WindowMaximize(form)						}, // ウィンドウを最大化
				{	ShortcutCommands.MiniMute,			new MiniMuteCommand(form, pecaPlayer)			}, // 最小化ミュート
				{	ShortcutCommands.Close,				new CloseCommand(form, pecaPlayer)				}, // 閉じる
				{	ShortcutCommands.VisibleStatusBar,	new VisibleStatusBarCommand(form, statusBar)	}, // ステータスバーの表示切り替え
				{	ShortcutCommands.OpenPeerstViewer,	new OpenPeerstViewerCommand(statusBar)			}, // PeerstViewerを開く
				{	ShortcutCommands.UpdateChannelInfo,	new UpdateChannelInfoCommand(pecaPlayer)		}, // チャンネル情報更新
				{	ShortcutCommands.ShowNewRes,		new ShowNewResCommand()							}, // 新着レス表示
				{	ShortcutCommands.TopMost,			new TopMostCommand(form)						}, // 最前列表示切り替え
				{	ShortcutCommands.WindowSizeUp,		new WindowSizeUpCommand(form, pecaPlayer)		}, // ウィンドウサイズUP
				{	ShortcutCommands.WindowSizeDown,	new WindowSizeDownCommand(form, pecaPlayer)		}, // ウィンドウサイズDOWN
				{	ShortcutCommands.DisconnectRelay,	new DisconnectRelayCommand(form, pecaPlayer)	}, // リレー切断
				{	ShortcutCommands.Bump,				new BumpCommand(pecaPlayer)						}, // Bump
				{	ShortcutCommands.WindowSize,		new WindowSizeCommand(form, pecaPlayer)			}, // ウィンドウサイズ指定
				{	ShortcutCommands.WindowScale,		new WindowScaleCommand(form, pecaPlayer)		}, // ウィンドウサイズ拡大率指定
				{	ShortcutCommands.WmpMenu,			new WmpMenuCommand(pecaPlayer)					}, // WMPメニュー表示
				// TODO 画面分割		{	ShortcutCommands.ScreenSplit,	new ScreenSplitWidthCommand(form, pecaPlayer)	}, // 画面分割
				// TODO 動画にフィット	{	ShortcutCommands.FitMovieSize,	new FitMovieSizeCommand(form, pecaPlayer)		}, // 黒枠を消す
			};
		}

		//-------------------------------------------------------------
		// 概要：イベントの設定
		//-------------------------------------------------------------
		private void SettingEvent()
		{
			// TODO 設定によって切り替えを行う
			eventMap.Add(ShortcutEvents.WheelUp,				new ShortcutInfo(ShortcutCommands.VolumeUp, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.WheelDown,				new ShortcutInfo(ShortcutCommands.VolumeDown, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.MiddleClick,			new ShortcutInfo(ShortcutCommands.MiniMute, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.Mute,					new ShortcutInfo(ShortcutCommands.Mute, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.DoubleClick,			new ShortcutInfo(ShortcutCommands.WindowMaximize, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.StatusbarRightClick,	new ShortcutInfo(ShortcutCommands.OpenPeerstViewer, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.StatusbarLeftClick,		new ShortcutInfo(ShortcutCommands.VisibleStatusBar, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.MinButtonClick,			new ShortcutInfo(ShortcutCommands.WindowMinimize, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.MaxButtonClick,			new ShortcutInfo(ShortcutCommands.WindowMaximize, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.CloseButtonClick,		new ShortcutInfo(ShortcutCommands.Close, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.ThreadTitleRightClick,	new ShortcutInfo(ShortcutCommands.OpenPeerstViewer, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.StatusbarHover,			new ShortcutInfo(ShortcutCommands.ShowNewRes, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.RightClickWheelUp,		new ShortcutInfo(ShortcutCommands.WindowSizeDown, CommandArgs.Empty));
			eventMap.Add(ShortcutEvents.RightClickWheelDown,	new ShortcutInfo(ShortcutCommands.WindowSizeUp, CommandArgs.Empty));
		}

		//-------------------------------------------------------------
		// 概要：マウスジェスチャーの設定
		//-------------------------------------------------------------
		private void SettingGesture()
		{
			// TODO 設定によって切り替えを行う
			gestureMap.Add("↓→",			new ShortcutInfo(ShortcutCommands.Close, CommandArgs.Empty));
			gestureMap.Add("↓",			new ShortcutInfo(ShortcutCommands.OpenPeerstViewer, CommandArgs.Empty));
			gestureMap.Add("↓↑",			new ShortcutInfo(ShortcutCommands.UpdateChannelInfo, CommandArgs.Empty));
			gestureMap.Add("↑",			new ShortcutInfo(ShortcutCommands.Bump, CommandArgs.Empty));
		}

		//-------------------------------------------------------------
		// 概要：キー入力の設定
		//-------------------------------------------------------------
		private void SettingKey()
		{
			// TODO 設定によって切り替えを行う
			keyMap.Add(Keys.T,				new ShortcutInfo(ShortcutCommands.TopMost, CommandArgs.Empty));
			keyMap.Add(Keys.Alt | Keys.B,	new ShortcutInfo(ShortcutCommands.Bump, CommandArgs.Empty));
			keyMap.Add(Keys.Alt | Keys.X,	new ShortcutInfo(ShortcutCommands.DisconnectRelay, CommandArgs.Empty));
			keyMap.Add(Keys.Up,				new ShortcutInfo(ShortcutCommands.VolumeUp, CommandArgs.Empty));
			keyMap.Add(Keys.Down,			new ShortcutInfo(ShortcutCommands.VolumeDown, CommandArgs.Empty));
			keyMap.Add(Keys.Delete,			new ShortcutInfo(ShortcutCommands.Mute, CommandArgs.Empty));
			keyMap.Add(Keys.Enter,			new ShortcutInfo(ShortcutCommands.VisibleStatusBar, CommandArgs.Empty));
			keyMap.Add(Keys.Escape,			new ShortcutInfo(ShortcutCommands.Close, CommandArgs.Empty));
			keyMap.Add(Keys.D1,				new ShortcutInfo(ShortcutCommands.WindowScale, new WindowScaleCommandArgs(0.5f)));
			keyMap.Add(Keys.D2,				new ShortcutInfo(ShortcutCommands.WindowScale, new WindowScaleCommandArgs(0.75f)));
			keyMap.Add(Keys.D3,				new ShortcutInfo(ShortcutCommands.WindowScale, new WindowScaleCommandArgs(1.0f)));
			keyMap.Add(Keys.D4,				new ShortcutInfo(ShortcutCommands.WindowScale, new WindowScaleCommandArgs(1.5f)));
			keyMap.Add(Keys.D5,				new ShortcutInfo(ShortcutCommands.WindowScale, new WindowScaleCommandArgs(2.0f)));
			keyMap.Add(Keys.Alt | Keys.D1,	new ShortcutInfo(ShortcutCommands.WindowSize, new WindowSizeCommandArgs(160, 120)));
			keyMap.Add(Keys.Alt | Keys.D2,	new ShortcutInfo(ShortcutCommands.WindowSize, new WindowSizeCommandArgs(320, 240)));
			keyMap.Add(Keys.Alt | Keys.D3,	new ShortcutInfo(ShortcutCommands.WindowSize, new WindowSizeCommandArgs(480, 360)));
			keyMap.Add(Keys.Alt | Keys.D4,	new ShortcutInfo(ShortcutCommands.WindowSize, new WindowSizeCommandArgs(640, 480)));
			keyMap.Add(Keys.Alt | Keys.D5,	new ShortcutInfo(ShortcutCommands.WindowSize, new WindowSizeCommandArgs(800, 600)));
		}
	}
}
