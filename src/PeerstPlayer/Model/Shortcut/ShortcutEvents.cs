using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstPlayer.Model.Shortcut
{
	// イベント定義
	public enum ShortcutEvents
	{
		WheelUp,				// ホイールUp
		WheelDown,				// ホイールDown
		MiddleClick,			// 中クリック
		Mute,					// ミュートボタン押下
		DoubleClick,			// ダブルクリック
		StatusbarRightClick,	// ステータスバー右クリック
		StatusbarLeftClick,		// ステータスバー左クリック
		MaxButtonClick,			// 最大化ボタン押下
		MinButtonClick,			// 最小化ボタン押下
		CloseButtonClick,		// 閉じるボタン押下
		ThreadTitleRightClick,	// スレッドタイトルを右クリック
		StatusbarHover,			// ステータスバーをマウスホバー
	};
}
