using Shule.Peerst.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer.Event
{
	class WmpNativeWindow : NativeWindow
	{
		Observable observable = new Observable();

		public WmpNativeWindow(IntPtr handle, Observer observer)
		{
			AssignHandle(handle);				// サブクラスウィンドウの設定
			observable.AddObserver(observer);	// オブザーバの設定
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case Win32API.WM_SETFOCUS:
					break;

				case Win32API.WM_KEYDOWN:
					break;

				case Win32API.WM_MOUSEHOVER:
					break;

				case Win32API.WM_MOUSEMOVE:
					break;

				case Win32API.WM_RBUTTONDOWN:
					break;

				case Win32API.WM_LBUTTONDBLCLK:
					break;

				case Win32API.WM_LBUTTONUP:
					observable.NotifyObservers(new FormEventArgs(FormEvents.DoubleLeftClick, new List<Keys>()));
					break;

				default:
					break;
			}

			base.WndProc(ref m);
		}
	}
}
