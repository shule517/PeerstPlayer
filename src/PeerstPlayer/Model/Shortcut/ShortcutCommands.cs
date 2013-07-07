using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstPlayer.Model.Shortcut
{
	// コマンド実行
	public enum ShortcutCommands
	{
		VolumeUp,			// 音量Up
		VolumeDown,			// 音量Down
		Mute,				// ミュート切り替え
		WindowMaximize,		// ウィンドウ最大化
		WindowMinimization,	// ウィンドウ最小化
		Close,				// 閉じる
		OpenPeerstViewer,	// ビューワを開く
		VisibleStatusBar,	// ステータスバーの表示切り替え
		UpdateChannelInfo,	// チャンネル情報更新
	};
}
