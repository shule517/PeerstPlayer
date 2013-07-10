using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeerstLib.Utility;
using PeerstPlayer.Control;

namespace PeerstPlayer.Model.Shortcut
{
	//-------------------------------------------------------------
	// 概要：ショートカットコマンドクラス
	//-------------------------------------------------------------
	class ShortcutManager
	{
		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// イベントMap (イベント -> コマンド)
		private Dictionary<ShortcutEvents, ShortcutCommands> eventMap = new Dictionary<ShortcutEvents, ShortcutCommands>();

		// コマンドMap (コマンド -> 実行処理)
		private Dictionary<ShortcutCommands, Action> commandMap = new Dictionary<ShortcutCommands, Action>();

		// マウスジェスチャーMap (ジェスチャー -> コマンド)
		private Dictionary<string, ShortcutCommands> gestureMap = new Dictionary<string, ShortcutCommands>();

		//-------------------------------------------------------------
		// 概要：初期化
		//-------------------------------------------------------------
		public void Init(PlayerView playerView, PecaPlayer pecaPlayer, Control.StatusBar statusBar)
		{
			CreateCommand(playerView, pecaPlayer, statusBar);
			CreateEvent();
			CreateGesture();
		}

		//-------------------------------------------------------------
		// 概要：マウスジェスチャー実行
		//-------------------------------------------------------------
		public void ExecGesture(string gesture)
		{
			ShortcutCommands commandId;
			if (gestureMap.TryGetValue(gesture, out commandId))
			{
				Logger.Instance.InfoFormat("マウスジェスチャー実行 [ジェスチャー:{0}, コマンドID:{1}]", gesture, commandId);
				ExecCommand(commandId);
			}
		}

		//-------------------------------------------------------------
		// 概要：イベント実行
		//-------------------------------------------------------------
		public void RaiseEvent(ShortcutEvents eventId)
		{
			Logger.Instance.InfoFormat("イベント実行 [イベントID:{0}]", eventId);
			ShortcutCommands commandId = eventMap[eventId];
			ExecCommand(commandId);
		}

		//-------------------------------------------------------------
		// 概要：ジェスチャーの詳細を取得
		//-------------------------------------------------------------
		public string GetGestureDetail(string gesture)
		{
			ShortcutCommands commandId;
			if (gestureMap.TryGetValue(gesture, out commandId))
			{
				return commandId.ToString();
			}

			return String.Empty;
		}
	
		//-------------------------------------------------------------
		// 概要：イベント登録
		//-------------------------------------------------------------
		private void CreateEvent()
		{
			// TODO 設定によって切り替えを行う
			eventMap.Add(ShortcutEvents.WheelUp,				ShortcutCommands.VolumeUp);
			eventMap.Add(ShortcutEvents.WheelDown,				ShortcutCommands.VolumeDown);
			eventMap.Add(ShortcutEvents.Mute,					ShortcutCommands.Mute);
			eventMap.Add(ShortcutEvents.DoubleClick,			ShortcutCommands.WindowMaximize);
			eventMap.Add(ShortcutEvents.StatusbarRightClick,	ShortcutCommands.OpenPeerstViewer);
			eventMap.Add(ShortcutEvents.StatusbarLeftClick,		ShortcutCommands.VisibleStatusBar);
			eventMap.Add(ShortcutEvents.MinButtonClick,			ShortcutCommands.WindowMinimization);
			eventMap.Add(ShortcutEvents.MaxButtonClick,			ShortcutCommands.WindowMaximize);
			eventMap.Add(ShortcutEvents.CloseButtonClick,		ShortcutCommands.Close);
			eventMap.Add(ShortcutEvents.ThreadTitleRightClick,	ShortcutCommands.OpenPeerstViewer);
		}

		//-------------------------------------------------------------
		// 概要：マウスジェスチャーの設定作成
		//-------------------------------------------------------------
		private void CreateGesture()
		{
			// TODO 設定によって切り替えを行う
			gestureMap.Add("↓→",	ShortcutCommands.Close);
			gestureMap.Add("↓",	ShortcutCommands.OpenPeerstViewer);
			gestureMap.Add("↓↑",	ShortcutCommands.UpdateChannelInfo);
		}

		//-------------------------------------------------------------
		// 概要：コマンド作成
		//-------------------------------------------------------------
		private void CreateCommand(Form form, PecaPlayer pecaPlayer, PeerstPlayer.Control.StatusBar statusBar)
		{
			// 音量UP
			commandMap.Add(ShortcutCommands.VolumeUp, () =>
			{
				if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
				{
					pecaPlayer.Volume += 1;
				}
				else if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
				{
					pecaPlayer.Volume += 5;
				}
				else
				{
					pecaPlayer.Volume += 10;
				}
			});
			// 音量UP
			commandMap.Add(ShortcutCommands.VolumeDown, () =>
			{
				if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
				{
					pecaPlayer.Volume -= 1;
				}
				else if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
				{
					pecaPlayer.Volume -= 5;
				}
				else
				{
					pecaPlayer.Volume -= 10;
				}
			});
			// ミュート切替
			commandMap.Add(ShortcutCommands.Mute, () => pecaPlayer.Mute = !pecaPlayer.Mute);
			// ウィンドウを最小化
			commandMap.Add(ShortcutCommands.WindowMinimization, () => form.WindowState = FormWindowState.Minimized);
			// 閉じる
			commandMap.Add(ShortcutCommands.Close, () =>
			{
				Application.Exit();
			});
			// ステータスバーの表示切り替え
			commandMap.Add(ShortcutCommands.VisibleStatusBar, () =>
			{
				statusBar.WriteFieldVisible = !statusBar.WriteFieldVisible;
				if (statusBar.WriteFieldVisible)
				{
					statusBar.Focus();
				}
			});
			// ウィンドウを最大化
			commandMap.Add(ShortcutCommands.WindowMaximize, () =>
			{
				if (form.WindowState == FormWindowState.Normal)
				{
					form.WindowState = FormWindowState.Maximized;
				}
				else
				{
					form.WindowState = FormWindowState.Normal;
				}
			});
			// PeerstViewerを開く
			commandMap.Add(ShortcutCommands.OpenPeerstViewer, () =>
			{
				// スレッド選択しているスレッドURLを開く
				string viewerExePath = Path.Combine(Environment.CurrentDirectory, "PeerstViewer.exe");
				string param = statusBar.SelectThreadUrl;
				Logger.Instance.InfoFormat("PeerstViewer起動 [viewerExePath:{0} param:{1}]", viewerExePath, param);
				Process.Start(viewerExePath, param);
			});
			// チャンネル情報更新
			commandMap.Add(ShortcutCommands.UpdateChannelInfo, () =>
			{
				pecaPlayer.UpdateChannelInfo();
			});
			// 新着レス表示
			commandMap.Add(ShortcutCommands.ShowNewRes, () =>
			{
				// TODO 新着レス表示の実装
			});
		}

		//-------------------------------------------------------------
		// 概要：コマンド実行
		//-------------------------------------------------------------
		private void ExecCommand(ShortcutCommands commandId)
		{
			Logger.Instance.InfoFormat("コマンド実行 [コマンドID:{0}]", commandId);
			commandMap[commandId]();
		}
	}
}
