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
	/// コマンド実行イベント引数
	/// </summary>
	class CommnadExecutedEventArgs : EventArgs
	{
		/// <summary>
		/// 実行コマンド
		/// </summary>
		public Commands Command;

		/// <summary>
		/// 実行コマンド内容
		/// </summary>
		public string Detail;
	}

	/// <summary>
	/// ショートカット設定
	/// </summary>
	[DataContract(Name = "ShortcutSettings")]
	public class ShortcutSettings
	{
		// イベントMap (イベントID -> コマンドIDを取得)
		[DataMember]
		public Dictionary<ShortcutEvents, Commands> EventMap = new Dictionary<ShortcutEvents, Commands>();

		// マウスジェスチャーMap (ジェスチャー -> コマンドIDを取得)
		[DataMember]
		public Dictionary<string, Commands> GestureMap = new Dictionary<string, Commands>();
	
		// キー入力Map (キー入力 -> コマンドIDを取得)
		[DataMember]
		public Dictionary<KeyInput, Commands> KeyMap = new Dictionary<KeyInput, Commands>();
	}

	//-------------------------------------------------------------
	// 概要：ショートカットコマンドクラス
	//-------------------------------------------------------------
	public class ShortcutManager
	{
		/// <summary>
		/// コマンド実行イベント
		/// </summary>
		public event EventHandler CommandExecuted = delegate { };

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
		private Dictionary<Commands, ShortcutCommand> commandMap = new Dictionary<Commands, ShortcutCommand>();
		public Dictionary<Commands, ShortcutCommand> CommandMap
		{
			get { return commandMap; }
		}

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
				LoadDefaultSettings();
			}

			// 設定ファイルが壊れている場合
			if ((settings.EventMap == null) || (settings.GestureMap == null) || (settings.KeyMap == null))
			{
				Logger.Instance.Error("ショートカット設定ファイルが壊れているため、デフォルト設定を読み込みます。");
				LoadDefaultSettings();
			}
		}

		//-------------------------------------------------------------
		// 概要：イベント実行
		//-------------------------------------------------------------
		public void RaiseEvent(ShortcutEvents eventId)
		{
			Logger.Instance.InfoFormat("イベント実行 [イベントID:{0}]", eventId);
			Commands commands;
			if (settings.EventMap.TryGetValue(eventId, out commands))
			{
				ExecCommand(commands);
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
			Commands commands;
			if (settings.KeyMap.TryGetValue(new KeyInput(modifierKey, keyCode), out commands))
			{
				ExecCommand(commands);
			}
		}

		//-------------------------------------------------------------
		// 概要：マウスジェスチャー実行
		//-------------------------------------------------------------
		public void ExecGesture(string gesture)
		{
			Commands commands;
			if (settings.GestureMap.TryGetValue(gesture, out commands))
			{
				Logger.Instance.InfoFormat("マウスジェスチャー実行 [ジェスチャー:{0}, コマンドID:{1}]", gesture, commands);
				ExecCommand(commands);
			}
		}

		//-------------------------------------------------------------
		// 概要：ジェスチャーの詳細を取得
		//-------------------------------------------------------------
		public string GetGestureDetail(string gesture)
		{
			Commands commands;
			if (settings.GestureMap.TryGetValue(gesture, out commands))
			{
				return commandMap[commands].Detail;
			}

			return String.Empty;
		}

		//-------------------------------------------------------------
		// 概要：コマンド実行
		//-------------------------------------------------------------
		public void ExecCommand(Commands commands)
		{
			Logger.Instance.InfoFormat("コマンド実行 [コマンドID:{0}]", commands);

			// コマンド実行
			ShortcutCommand command = commandMap[commands];
			command.Execute();

			// コマンド実行イベント -> 実行内容を通知
			CommandExecuted(this, new CommnadExecutedEventArgs { Command = commands, Detail = command.Detail });
		}

		//-------------------------------------------------------------
		// 概要：コマンド作成
		//-------------------------------------------------------------
		private void CreateCommand(Form form, PecaPlayerControl pecaPlayer, StatusBarControl statusBar)
		{
			commandMap = new Dictionary<Commands, ShortcutCommand>()
			{
				{	Commands.VolumeUp,				new ShortcutCommand(new VolumeUpCommand(pecaPlayer), new CommandArgs())				}, // 音量UP
				{	Commands.VolumeDown,			new ShortcutCommand(new VolumeDownCommand(pecaPlayer), new CommandArgs())			}, // 音量DOWN
				{	Commands.Mute,					new ShortcutCommand(new MuteCommand(pecaPlayer), new CommandArgs())					}, // ミュート切替
				{	Commands.WindowMinimize,		new ShortcutCommand(new WindowMinimize(form), new CommandArgs())					}, // ウィンドウを最小化
				{	Commands.WindowMaximize,		new ShortcutCommand(new WindowMaximize(form), new CommandArgs())					}, // ウィンドウを最大化
				{	Commands.MiniMute,				new ShortcutCommand(new MiniMuteCommand(form, pecaPlayer), new CommandArgs())		}, // 最小化ミュート
				{	Commands.Close,					new ShortcutCommand(new CloseCommand(form, pecaPlayer), new CommandArgs())			}, // 閉じる
				{	Commands.VisibleStatusBar,		new ShortcutCommand(new VisibleStatusBarCommand(form, statusBar), new CommandArgs())}, // ステータスバーの表示切り替え
				{	Commands.OpenPeerstViewer,		new ShortcutCommand(new OpenPeerstViewerCommand(statusBar), new CommandArgs())		}, // PeerstViewerを開く
				{	Commands.UpdateChannelInfo,		new ShortcutCommand(new UpdateChannelInfoCommand(pecaPlayer), new CommandArgs())	}, // チャンネル情報更新
				{	Commands.ShowNewRes,			new ShortcutCommand(new ShowNewResCommand(form, statusBar), new CommandArgs())		}, // 新着レス表示
				{	Commands.TopMost,				new ShortcutCommand(new TopMostCommand(form), new CommandArgs())					}, // 最前列表示切り替え
				{	Commands.WindowSizeUp,			new ShortcutCommand(new WindowSizeUpCommand(form, pecaPlayer), new CommandArgs())	}, // ウィンドウサイズUP
				{	Commands.WindowSizeDown,		new ShortcutCommand(new WindowSizeDownCommand(form, pecaPlayer), new CommandArgs())	}, // ウィンドウサイズDOWN
				{	Commands.DisconnectRelay,		new ShortcutCommand(new DisconnectRelayCommand(form, pecaPlayer), new CommandArgs())}, // リレー切断
				{	Commands.Bump,					new ShortcutCommand(new BumpCommand(pecaPlayer), new CommandArgs())					}, // Bump
				{	Commands.WmpMenu,				new ShortcutCommand(new WmpMenuCommand(pecaPlayer), new CommandArgs())				}, // WMPメニュー表示
				{	Commands.FitMovieSize,			new ShortcutCommand(new FitMovieSizeCommand(form, pecaPlayer), new CommandArgs())	}, // 黒枠を消す
				{	Commands.WindowSize160x120,		new ShortcutCommand(new WindowSizeCommand(form, pecaPlayer), new WindowSizeCommandArgs(160, 120))	}, // ウィンドウサイズ指定
				{	Commands.WindowSize320x240,		new ShortcutCommand(new WindowSizeCommand(form, pecaPlayer), new WindowSizeCommandArgs(320, 240))	}, // ウィンドウサイズ指定
				{	Commands.WindowSize480x360,		new ShortcutCommand(new WindowSizeCommand(form, pecaPlayer), new WindowSizeCommandArgs(480, 360))	}, // ウィンドウサイズ指定
				{	Commands.WindowSize640x480,		new ShortcutCommand(new WindowSizeCommand(form, pecaPlayer), new WindowSizeCommandArgs(640, 480))	}, // ウィンドウサイズ指定
				{	Commands.WindowSize800x600,		new ShortcutCommand(new WindowSizeCommand(form, pecaPlayer), new WindowSizeCommandArgs(800, 600))	}, // ウィンドウサイズ指定
				{	Commands.WindowScale50Per,		new ShortcutCommand(new WindowScaleCommand(form, pecaPlayer), new WindowScaleCommandArgs(0.5f))		}, // ウィンドウサイズ拡大率指定
				{	Commands.WindowScale75Per,		new ShortcutCommand(new WindowScaleCommand(form, pecaPlayer), new WindowScaleCommandArgs(0.75f))	}, // ウィンドウサイズ拡大率指定
				{	Commands.WindowScale100Per,		new ShortcutCommand(new WindowScaleCommand(form, pecaPlayer), new WindowScaleCommandArgs(1.0f))		}, // ウィンドウサイズ拡大率指定
				{	Commands.WindowScale150Per,		new ShortcutCommand(new WindowScaleCommand(form, pecaPlayer), new WindowScaleCommandArgs(1.5f))		}, // ウィンドウサイズ拡大率指定
				{	Commands.WindowScale200Per,		new ShortcutCommand(new WindowScaleCommand(form, pecaPlayer), new WindowScaleCommandArgs(2.0f))		}, // ウィンドウサイズ拡大率指定
				{	Commands.ScreenSplitWidthx5,	new ShortcutCommand(new ScreenSplitCommand(form, pecaPlayer), new ScreenSplitCommandArgs(5, -1))	}, // 画面分割
				{	Commands.ScreenSplitWidthx4,	new ShortcutCommand(new ScreenSplitCommand(form, pecaPlayer), new ScreenSplitCommandArgs(4, -1))	}, // 画面分割
				{	Commands.ScreenSplitWidthx3,	new ShortcutCommand(new ScreenSplitCommand(form, pecaPlayer), new ScreenSplitCommandArgs(3, -1))	}, // 画面分割
				{	Commands.ScreenSplitWidthx2,	new ShortcutCommand(new ScreenSplitCommand(form, pecaPlayer), new ScreenSplitCommandArgs(2, -1))	}, // 画面分割
				{	Commands.ScreenSplitWidthx1,	new ShortcutCommand(new ScreenSplitCommand(form, pecaPlayer), new ScreenSplitCommandArgs(1, -1))	}, // 画面分割
				{	Commands.ScreenSplitHeightx5,	new ShortcutCommand(new ScreenSplitCommand(form, pecaPlayer), new ScreenSplitCommandArgs(-1, 5))	}, // 画面分割
				{	Commands.ScreenSplitHeightx4,	new ShortcutCommand(new ScreenSplitCommand(form, pecaPlayer), new ScreenSplitCommandArgs(-1, 4))	}, // 画面分割
				{	Commands.ScreenSplitHeightx3,	new ShortcutCommand(new ScreenSplitCommand(form, pecaPlayer), new ScreenSplitCommandArgs(-1, 3))	}, // 画面分割
				{	Commands.ScreenSplitHeightx2,	new ShortcutCommand(new ScreenSplitCommand(form, pecaPlayer), new ScreenSplitCommandArgs(-1, 2))	}, // 画面分割
				{	Commands.ScreenSplitHeightx1,	new ShortcutCommand(new ScreenSplitCommand(form, pecaPlayer), new ScreenSplitCommandArgs(-1, 1))	}, // 画面分割
			};
		}

		//-------------------------------------------------------------
		// 概要：イベントの設定(デフォルト)
		//-------------------------------------------------------------
		private void SettingEvent()
		{
			settings.EventMap.Add(ShortcutEvents.WheelUp, Commands.VolumeUp);
			settings.EventMap.Add(ShortcutEvents.WheelDown, Commands.VolumeDown);
			settings.EventMap.Add(ShortcutEvents.MiddleClick, Commands.MiniMute);
			settings.EventMap.Add(ShortcutEvents.Mute, Commands.Mute);
			settings.EventMap.Add(ShortcutEvents.DoubleClick, Commands.WindowMaximize);
			settings.EventMap.Add(ShortcutEvents.StatusbarRightClick, Commands.OpenPeerstViewer);
			settings.EventMap.Add(ShortcutEvents.StatusbarLeftClick, Commands.VisibleStatusBar);
			settings.EventMap.Add(ShortcutEvents.MinButtonClick, Commands.WindowMinimize);
			settings.EventMap.Add(ShortcutEvents.MaxButtonClick, Commands.WindowMaximize);
			settings.EventMap.Add(ShortcutEvents.CloseButtonClick, Commands.Close);
			settings.EventMap.Add(ShortcutEvents.ThreadTitleRightClick, Commands.OpenPeerstViewer);
			settings.EventMap.Add(ShortcutEvents.RightClickWheelUp, Commands.WindowSizeUp);
			settings.EventMap.Add(ShortcutEvents.RightClickWheelDown, Commands.WindowSizeDown);
			settings.EventMap.Add(ShortcutEvents.MovieStart, Commands.WindowScale100Per);
			// settings.EventMap.Add(ShortcutEvents.StatusbarHover, Commands.ShowNewRes);
		}

		//-------------------------------------------------------------
		// 概要：マウスジェスチャーの設定(デフォルト)
		//-------------------------------------------------------------
		private void SettingGesture()
		{
			settings.GestureMap.Add("↓→", Commands.Close);
			settings.GestureMap.Add("↓", Commands.OpenPeerstViewer);
			settings.GestureMap.Add("↓↑", Commands.UpdateChannelInfo);
			settings.GestureMap.Add("↑", Commands.Bump);
		}

		//-------------------------------------------------------------
		// 概要：キー入力の設定(デフォルト)
		//-------------------------------------------------------------
		private void SettingKey()
		{
			settings.KeyMap.Add(new KeyInput(Keys.T), Commands.TopMost);
			settings.KeyMap.Add(new KeyInput(Keys.F), Commands.FitMovieSize);
			settings.KeyMap.Add(new KeyInput(Keys.Alt, Keys.B), Commands.Bump);
			settings.KeyMap.Add(new KeyInput(Keys.Alt, Keys.X), Commands.DisconnectRelay);
			settings.KeyMap.Add(new KeyInput(Keys.Up), Commands.VolumeUp);
			settings.KeyMap.Add(new KeyInput(Keys.Down), Commands.VolumeDown);
			settings.KeyMap.Add(new KeyInput(Keys.Delete), Commands.Mute);
			settings.KeyMap.Add(new KeyInput(Keys.Enter), Commands.VisibleStatusBar);
			settings.KeyMap.Add(new KeyInput(Keys.Escape), Commands.Close);
			settings.KeyMap.Add(new KeyInput(Keys.D1), Commands.WindowScale50Per);
			settings.KeyMap.Add(new KeyInput(Keys.D2), Commands.WindowScale75Per);
			settings.KeyMap.Add(new KeyInput(Keys.D3), Commands.WindowScale100Per);
			settings.KeyMap.Add(new KeyInput(Keys.D4), Commands.WindowScale150Per);
			settings.KeyMap.Add(new KeyInput(Keys.D5), Commands.WindowScale200Per);
			settings.KeyMap.Add(new KeyInput(Keys.Alt, Keys.D1), Commands.WindowSize160x120);
			settings.KeyMap.Add(new KeyInput(Keys.Alt, Keys.D2), Commands.WindowSize320x240);
			settings.KeyMap.Add(new KeyInput(Keys.Alt, Keys.D3), Commands.WindowSize480x360);
			settings.KeyMap.Add(new KeyInput(Keys.Alt, Keys.D4), Commands.WindowSize640x480);
			settings.KeyMap.Add(new KeyInput(Keys.Alt, Keys.D5), Commands.WindowSize800x600);
			settings.KeyMap.Add(new KeyInput(Keys.Q), Commands.ScreenSplitWidthx5);
			settings.KeyMap.Add(new KeyInput(Keys.W), Commands.ScreenSplitWidthx4);
			settings.KeyMap.Add(new KeyInput(Keys.E), Commands.ScreenSplitWidthx3);
			settings.KeyMap.Add(new KeyInput(Keys.R), Commands.ScreenSplitWidthx2);
			settings.KeyMap.Add(new KeyInput(Keys.Alt, Keys.Q), Commands.ScreenSplitHeightx5);
			settings.KeyMap.Add(new KeyInput(Keys.Alt, Keys.W), Commands.ScreenSplitHeightx4);
			settings.KeyMap.Add(new KeyInput(Keys.Alt, Keys.E), Commands.ScreenSplitHeightx3);
			settings.KeyMap.Add(new KeyInput(Keys.Alt, Keys.R), Commands.ScreenSplitHeightx2);
		}

		/// <summary>
		/// ショートカットのデフォルト設定読み込み
		/// </summary>
		private void LoadDefaultSettings()
		{
			settings = new ShortcutSettings();

			SettingEvent();
			SettingGesture();
			SettingKey();
			SettingSerializer.SaveSettings<ShortcutSettings>("ShortcutSettings.xml", settings);
		}
	}
}
