using AxWMPLib;
using PeerstPlayer.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		/// ダブルクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void wmp_DoubleClickEvent(object sender, _WMPOCXEvents_DoubleClickEvent e)
		{
			eventObserver.OnEvent(Events.DoubleClick, e);
		}

		/// <summary>
		/// マウスダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void wmp_MouseDownEvent(object sender, _WMPOCXEvents_MouseDownEvent e)
		{
			eventObserver.OnEvent(Events.MouseDown, e);
		}
	}
}
