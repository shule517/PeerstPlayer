using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer.Event
{
	/// <summary>
	/// フォームイベント引数
	/// </summary>
	public class FormEventArgs : EventArgs
	{
		public FormEventArgs(FormEvents events, List<Keys> modifyKeys)
		{
			this.Event = events;
			this.ModifyKeys = modifyKeys;
		}

		public FormEvents Event { get; private set; }
		public List<Keys> ModifyKeys { get; private set; }
	}

	/// <summary>
	/// イベントの種類
	/// </summary>
	public enum FormEvents
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
