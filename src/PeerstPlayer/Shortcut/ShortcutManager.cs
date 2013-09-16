using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Forms;
using PeerstLib.Util;
using PeerstPlayer.Controls.PecaPlayer;
using PeerstPlayer.Controls.StatusBar;
using PeerstPlayer.Forms.Setting;
using PeerstPlayer.Shortcut.Command;

namespace PeerstPlayer.Shortcut
{
	/// <summary>
	/// ショートカット設定
	/// </summary>
	[DataContract(Name = "ShortcutSettings")]
	public class ShortcutSettings
	{
		// イベントMap (イベントID -> コマンドIDを取得)
		[DataMember]
		public Dictionary<ShortcutEvents, ShortcutInfo> eventMap = new Dictionary<ShortcutEvents,ShortcutInfo>();

		// マウスジェスチャーMap (ジェスチャー -> コマンドIDを取得)
		[DataMember]
		public Dictionary<string, ShortcutInfo> gestureMap = new Dictionary<string,ShortcutInfo>();
	
		// キー入力Map (キー入力 -> コマンドIDを取得)
		[DataMember]
		public Dictionary<KeyInput, ShortcutInfo> keyMap = new Dictionary<KeyInput,ShortcutInfo>();
	}

	//-------------------------------------------------------------
	// 概要：ショートカットコマンドクラス
	//-------------------------------------------------------------
	public class ShortcutManager
	{
		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// ショートカット設定
		/// </summary>
		ShortcutSettings settings = new ShortcutSettings(); 
		public ShortcutSettings Settings
		{
			get { return settings; }
			set { settings = value; }
		}

		/// <summary>
		/// コマンドMap (コマンドID -> コマンドクラスを取得)
		/// </summary>
		private Dictionary<ShortcutCommands, IShortcutCommand> commandMap = new Dictionary<ShortcutCommands, IShortcutCommand>();

		//-------------------------------------------------------------
		// 概要：初期化
		//-------------------------------------------------------------
		public void Init(PlayerView playerView, PecaPlayerControl pecaPlayer, StatusBarControl statusBar)
		{
			// コマンド作成
			CreateCommand(playerView, pecaPlayer, statusBar);

			// ショートカット設定の読み込み
			try
			{
				settings = SettingSerializer.LoadSettings<ShortcutSettings>("ShortcutSettings.xml");
			}
			catch
			{
				Logger.Instance.Error("ショートカット設定ファイルの読み込みに失敗したため、デフォルト設定を読み込みます。");
				SettingEvent();
				SettingGesture();
				SettingKey();
				SettingSerializer.SaveSettings<ShortcutSettings>("ShortcutSettings.xml", settings);
			}
		}

		//-------------------------------------------------------------
		// 概要：イベント実行
		//-------------------------------------------------------------
		public void RaiseEvent(ShortcutEvents eventId)
		{
			Logger.Instance.InfoFormat("イベント実行 [イベントID:{0}]", eventId);
			ShortcutInfo shortcut;
			if (settings.eventMap.TryGetValue(eventId, out shortcut))
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

			Logger.Instance.InfoFormat("キー押下イベント実行 [イベントID:{0}, keyCode{0}, modifierKey{1}]", keyCode, modifierKey);

			// コマンド実行
			ShortcutInfo shortcut;
			if (settings.keyMap.TryGetValue(new KeyInput(modifierKey, keyCode), out shortcut))
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
			if (settings.gestureMap.TryGetValue(gesture, out shortcut))
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
			if (settings.gestureMap.TryGetValue(gesture, out shortcut))
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
		// 概要：イベントの設定(デフォルト)
		//-------------------------------------------------------------
		private void SettingEvent()
		{
			settings.eventMap.Add(ShortcutEvents.WheelUp, new ShortcutInfo(ShortcutCommands.VolumeUp, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.WheelDown, new ShortcutInfo(ShortcutCommands.VolumeDown, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.MiddleClick, new ShortcutInfo(ShortcutCommands.MiniMute, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.Mute, new ShortcutInfo(ShortcutCommands.Mute, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.DoubleClick, new ShortcutInfo(ShortcutCommands.WindowMaximize, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.StatusbarRightClick, new ShortcutInfo(ShortcutCommands.OpenPeerstViewer, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.StatusbarLeftClick, new ShortcutInfo(ShortcutCommands.VisibleStatusBar, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.MinButtonClick, new ShortcutInfo(ShortcutCommands.WindowMinimize, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.MaxButtonClick, new ShortcutInfo(ShortcutCommands.WindowMaximize, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.CloseButtonClick, new ShortcutInfo(ShortcutCommands.Close, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.ThreadTitleRightClick, new ShortcutInfo(ShortcutCommands.OpenPeerstViewer, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.StatusbarHover, new ShortcutInfo(ShortcutCommands.ShowNewRes, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.RightClickWheelUp, new ShortcutInfo(ShortcutCommands.WindowSizeDown, CommandArgs.Empty));
			settings.eventMap.Add(ShortcutEvents.RightClickWheelDown, new ShortcutInfo(ShortcutCommands.WindowSizeUp, CommandArgs.Empty));
		}

		//-------------------------------------------------------------
		// 概要：マウスジェスチャーの設定(デフォルト)
		//-------------------------------------------------------------
		private void SettingGesture()
		{
			settings.gestureMap.Add("↓→", new ShortcutInfo(ShortcutCommands.Close, CommandArgs.Empty));
			settings.gestureMap.Add("↓", new ShortcutInfo(ShortcutCommands.OpenPeerstViewer, CommandArgs.Empty));
			settings.gestureMap.Add("↓↑", new ShortcutInfo(ShortcutCommands.UpdateChannelInfo, CommandArgs.Empty));
			settings.gestureMap.Add("↑", new ShortcutInfo(ShortcutCommands.Bump, CommandArgs.Empty));
		}

		//-------------------------------------------------------------
		// 概要：キー入力の設定(デフォルト)
		//-------------------------------------------------------------
		private void SettingKey()
		{
			settings.keyMap.Add(new KeyInput(Keys.T), new ShortcutInfo(ShortcutCommands.TopMost, CommandArgs.Empty));
			settings.keyMap.Add(new KeyInput(Keys.Alt, Keys.B), new ShortcutInfo(ShortcutCommands.Bump, CommandArgs.Empty));
			settings.keyMap.Add(new KeyInput(Keys.Alt, Keys.X), new ShortcutInfo(ShortcutCommands.DisconnectRelay, CommandArgs.Empty));
			settings.keyMap.Add(new KeyInput(Keys.Up), new ShortcutInfo(ShortcutCommands.VolumeUp, CommandArgs.Empty));
			settings.keyMap.Add(new KeyInput(Keys.Down), new ShortcutInfo(ShortcutCommands.VolumeDown, CommandArgs.Empty));
			settings.keyMap.Add(new KeyInput(Keys.Delete), new ShortcutInfo(ShortcutCommands.Mute, CommandArgs.Empty));
			settings.keyMap.Add(new KeyInput(Keys.Enter), new ShortcutInfo(ShortcutCommands.VisibleStatusBar, CommandArgs.Empty));
			settings.keyMap.Add(new KeyInput(Keys.Escape), new ShortcutInfo(ShortcutCommands.Close, CommandArgs.Empty));
			settings.keyMap.Add(new KeyInput(Keys.D1), new ShortcutInfo(ShortcutCommands.WindowScale, new WindowScaleCommandArgs(0.5f)));
			settings.keyMap.Add(new KeyInput(Keys.D2), new ShortcutInfo(ShortcutCommands.WindowScale, new WindowScaleCommandArgs(0.75f)));
			settings.keyMap.Add(new KeyInput(Keys.D3), new ShortcutInfo(ShortcutCommands.WindowScale, new WindowScaleCommandArgs(1.0f)));
			settings.keyMap.Add(new KeyInput(Keys.D4), new ShortcutInfo(ShortcutCommands.WindowScale, new WindowScaleCommandArgs(1.5f)));
			settings.keyMap.Add(new KeyInput(Keys.D5), new ShortcutInfo(ShortcutCommands.WindowScale, new WindowScaleCommandArgs(2.0f)));
			settings.keyMap.Add(new KeyInput(Keys.Alt, Keys.D1), new ShortcutInfo(ShortcutCommands.WindowSize, new WindowSizeCommandArgs(160, 120)));
			settings.keyMap.Add(new KeyInput(Keys.Alt, Keys.D2), new ShortcutInfo(ShortcutCommands.WindowSize, new WindowSizeCommandArgs(320, 240)));
			settings.keyMap.Add(new KeyInput(Keys.Alt, Keys.D3), new ShortcutInfo(ShortcutCommands.WindowSize, new WindowSizeCommandArgs(480, 360)));
			settings.keyMap.Add(new KeyInput(Keys.Alt, Keys.D4), new ShortcutInfo(ShortcutCommands.WindowSize, new WindowSizeCommandArgs(640, 480)));
			settings.keyMap.Add(new KeyInput(Keys.Alt, Keys.D5), new ShortcutInfo(ShortcutCommands.WindowSize, new WindowSizeCommandArgs(800, 600)));
		}
	}
}
