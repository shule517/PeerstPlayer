using System.Runtime.InteropServices;
using PeerstLib.Controls;
using PeerstLib.Forms;
using System;
using System.Windows.Forms;

namespace PeerstPlayer.Controls.MoviePlayer
{
	/// <summary>
	/// Flashをフックする
	/// </summary>
	class FlashNativeWindow : NativeWindow
	{
		public event AxWMPLib._WMPOCXEvents_MouseDownEventHandler MouseDownEvent = delegate { };
		public event AxWMPLib._WMPOCXEvents_MouseUpEventHandler MouseUpEvent = delegate { };
		public event AxWMPLib._WMPOCXEvents_MouseMoveEventHandler MouseMoveEvent = delegate { };
		public event AxWMPLib._WMPOCXEvents_DoubleClickEventHandler DoubleClickEvent = delegate { };
		public event AxWMPLib._WMPOCXEvents_KeyDownEventHandler KeyDownEvent = delegate { };
	
		public FlashNativeWindow(IntPtr handle)
		{
			AssignHandle(handle);
		}

		private bool isDoubleClick = false;
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case (int)WindowsMessage.WM_LBUTTONDOWN:
					MouseDownEvent(this, new AxWMPLib._WMPOCXEvents_MouseDownEvent((short)Keys.LButton, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
					return;
				case (int)WindowsMessage.WM_RBUTTONDOWN:
					MouseDownEvent(this, new AxWMPLib._WMPOCXEvents_MouseDownEvent((short)Keys.RButton, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
					return;
				case (int)WindowsMessage.WM_MBUTTONDOWN:
					MouseDownEvent(this, new AxWMPLib._WMPOCXEvents_MouseDownEvent((short)Keys.MButton, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
					return;
				case (int)WindowsMessage.WM_LBUTTONUP:
					if (isDoubleClick)
					{
						DoubleClickEvent(this, new AxWMPLib._WMPOCXEvents_DoubleClickEvent((short)Keys.LButton, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
						isDoubleClick = false;
					}
					else
					{
						MouseUpEvent(this, new AxWMPLib._WMPOCXEvents_MouseUpEvent((short)Keys.LButton, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
					}
					break;
				case (int)WindowsMessage.WM_RBUTTONUP:
					MouseUpEvent(this, new AxWMPLib._WMPOCXEvents_MouseUpEvent((short)Keys.RButton, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
					return;
				case (int)WindowsMessage.WM_LBUTTONDBLCLK:
					// ここで処理すると2回目のLBUTTONDOWN時に処理されてしまい、
					// 挙動が少し変わってしまうのでフラグを立ててWM_LBUTTONUPで処理する
					isDoubleClick = true;
					break;
				case (int)WindowsMessage.WM_MOUSEMOVE:
					MouseMoveEvent(this, new AxWMPLib._WMPOCXEvents_MouseMoveEvent((short)Keys.None, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
					return;
				case (int)WindowsMessage.WM_KEYDOWN:
					short state = 0;
					if (Win32API.GetKeyState(0x10) < 0)
					{
						state += 1;
					}
					if (Win32API.GetKeyState(0x11) < 0)
					{
						state += 1 << 1;
					}
					if (Win32API.GetKeyState(0x12) < 0)
					{
						state += 1 << 2;
					}
					KeyDownEvent(this, new AxWMPLib._WMPOCXEvents_KeyDownEvent((short)m.WParam, state));
					return;
				case (int)WindowsMessage.WM_SYSKEYDOWN:
					int shift	= ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) ? 1 : 0;
					int control = ((Control.ModifierKeys & Keys.Control) == Keys.Control) ? 1 : 0;
					int alt = ((Control.ModifierKeys & Keys.Alt) == Keys.Alt) ? 1 : 0;
					short shiftState = (short)(shift + (control << 1) + (alt << 2));
					KeyDownEvent(this, new AxWMPLib._WMPOCXEvents_KeyDownEvent((short)m.WParam, shiftState));
					return;
			}
			base.WndProc(ref m);
		}
	}
}
