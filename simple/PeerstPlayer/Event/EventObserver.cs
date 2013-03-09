using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstPlayer.Event
{
	/// <summary>
	/// イベントオブザーバ
	/// </summary>
	interface EventObserver
	{
		void OnEvent(Events events, Object param);
	}

	/// <summary>
	/// イベントの種類
	/// </summary>
	enum Events
	{
		None = 0,

		// マウス
		LeftClick,
		RightClick,
		MiddleClick,

		// ダブルクリック
		DoubleLeftClick,
		DoubleRightClick,
		DoubleMiddleClick,

		// マウスホイール
		WheelDown,
		WheelUp,

		// 右クリック＋マウスホイール
		RightClick_WheelDown,
		RightClick_WheelUp,

		// 左クリック＋マウスホイール
		LeftClick_WheelDown,
		LeftClick_WheelUp,

		// 右→左クリック
		RightToLeftClick,

		// 左→右クリック
		LeftToRightClick,
	}
}
