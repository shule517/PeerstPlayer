using Shule.Peerst.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer.Event
{
	// WMPウィンドウのサブクラス化
	class WmpNativeWindow : NativeWindow
	{
		// フォームイベント
		public event FormEvent FormEvent;

		// コンストラクタ
		public WmpNativeWindow(IntPtr handle)
		{
			// サブクラスウィンドウの設定
			AssignHandle(handle);
		}

		// 通知
		private void Notify(FormEventArgs args)
		{
			if (FormEvent != null)
			{
				FormEvent(args);
			}
		}

		// ウィンドウプロシージャ
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
					Notify(new FormEventArgs(FormEvents.DoubleLeftClick, new List<Keys>()));
					break;

				default:
					break;
			}

			base.WndProc(ref m);
		}
	}
}
