using AxWMPLib;
using PeerstPlayer.Event;
using Shule.Peerst.Form;
using Shule.Peerst.Observer;
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

		public event FormEvent FormEvent;

		// マウスジェスチャ
		MouseGesture mouseGesture = new MouseGesture();

		/// <summary>
		/// WMPイベントマネージャ
		/// </summary>
		/// <param name="wmp"></param>
		public WmpEventManager(AxWindowsMediaPlayer wmp)
		{
			this.wmp = wmp;

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
			// 左ダブルクリック
			if (e.nButton == (short)Keys.LButton)
			{
				Notify(FormEvents.DoubleLeftClick);
			}
			// 右ダブルクリック
			else if (e.nButton == (short)Keys.RButton)
			{
				Notify(FormEvents.DoubleRightClick);
			}
			// 中ダブルクリック
			else if (e.nButton == (short)Keys.MButton)
			{
				Notify(FormEvents.DoubleMiddleClick);
			}
		}

		/// <summary>
		/// マウスダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void wmp_MouseDownEvent(object sender, _WMPOCXEvents_MouseDownEvent e)
		{
			// 左クリック
			if (e.nButton == (short)Keys.LButton)
			{
				// TODO 右クリック->左クリック

				// 左クリック
				Notify(FormEvents.LeftClick);
			}
			// 右クリック
			else if (e.nButton == (short)Keys.RButton)
			{
				// TODO 左クリック->右クリック

				// 右クリック
				Notify(FormEvents.RightClick);
			}
			// 中クリック
			else if (e.nButton == (short)Keys.MButton)
			{
				Notify(FormEvents.MiddleClick);
			}
		}

		/// <summary>
		/// 通知
		/// </summary>
		/// <param name="events"></param>
		private void Notify(FormEvents events)
		{
			List<Keys> keys = FormUtility.GetModifyKeys();

			if (FormEvent != null)
			{
				FormEvent(new FormEventArgs(events, keys));
			}
		}
	}
}
