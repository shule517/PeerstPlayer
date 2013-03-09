using AxWMPLib;
using PeerstPlayer.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer
{
	class WmpEventManager
	{
		AxWindowsMediaPlayer wmp;
		EventObserver eventObserver;

		// マウスジェスチャ
		MouseGesture mouseGesture = new MouseGesture();

		public WmpEventManager(AxWindowsMediaPlayer wmp, EventObserver eventObserver)
		{
			this.wmp = wmp;
			this.eventObserver = eventObserver;

			wmp.DoubleClickEvent += wmp_DoubleClickEvent;	// ダブルクリック
			wmp.MouseDownEvent += wmp_MouseDownEvent;		// マウスダウン
		}

		/// <summary>
		/// Shift+Control+Altを取得
		/// </summary>
		/// <returns></returns>
		string GetModifiers()
		{
			// ジェスチャー
			byte[] keyState = new byte[256];
			Win32API.GetKeyboardState(keyState);

			string modifiers = "";

			if ((keyState[(int)Keys.ShiftKey] & 128) != 0)
			{
				modifiers += "Shift+";
			}

			if ((keyState[(int)Keys.ControlKey] & 128) != 0)
			{
				modifiers += "Control+";
			}

			if ((keyState[(int)Keys.Menu] & 128) != 0)
			{
				modifiers += "Alt+";
			}

			return modifiers;
		}

		/// <summary>
		/// ダブルクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void wmp_DoubleClickEvent(object sender, _WMPOCXEvents_DoubleClickEvent e)
		{
			// 左ダブルクリック
			if (e.nButton == 1)
			{
				eventObserver.OnEvent(Events.DoubleLeftClick, e);
			}
			// 右ダブルクリック
			else if (e.nButton == 2)
			{
				eventObserver.OnEvent(Events.DoubleRightClick, e);
			}
			// 中ダブルクリック
			else if (e.nButton == 4)
			{
				eventObserver.OnEvent(Events.DoubleMiddleClick, e);
			}
		}

		const int KeyLeft = 1;		// 左クリック
		const int KeyRight = 2;		// 右クリック

		/// <summary>
		/// マウスダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void wmp_MouseDownEvent(object sender, _WMPOCXEvents_MouseDownEvent e)
		{
			// 左クリック
			if (e.nButton == 1)
			{
				// 右クリック->左クリック
				if (Win32API.GetAsyncKeyState(KeyRight) < 0)
				{
					eventObserver.OnEvent(Events.RightToLeftClick, e);
				}
				// 左クリック
				else
				{
					eventObserver.OnEvent(Events.LeftClick, e);
				}
			}
			// 右クリック
			else if (e.nButton == 2)
			{
				// 左クリック->右クリック
				if (Win32API.GetAsyncKeyState(KeyRight) < 0)
				{
					eventObserver.OnEvent(Events.LeftToRightClick, e);
				}
				// 右クリック
				else
				{
					eventObserver.OnEvent(Events.RightClick, e);
				}
			}
			// 中クリック
			else if (e.nButton == 3)
			{
				eventObserver.OnEvent(Events.MiddleClick, e);
			}
		}
	}
}
