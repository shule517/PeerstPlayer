using System.Runtime.InteropServices;
using PeerstLib.Controls;
using PeerstLib.Forms;
using System;
using System.Windows.Forms;
using AxShockwaveFlashObjects;
using PeerstPlayer.Forms.Player;

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

		private readonly AxShockwaveFlash flash;
		// ウィンドウサイズ変更用の枠サイズ
		private const int frameSize = 15;
	
		public FlashNativeWindow(AxShockwaveFlash parent)
		{
			flash = parent;
			AssignHandle(parent.Handle);
		}

		private bool isDoubleClick = false;
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case (int)WindowsMessage.WM_LBUTTONDOWN:
					// 枠なしのときだけ処理を実行する
					if (!PlayerSettings.FrameInvisible)
					{
						MouseDownEvent(this, new AxWMPLib._WMPOCXEvents_MouseDownEvent((short)Keys.LButton, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
						return;
					}
					HitArea area = FormUtility.GetHitArea(frameSize, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16, flash.Width, flash.Height);
					if (area != HitArea.HTNONE)
					{
						Win32API.SendMessage(flash.Parent.Parent.Parent.Handle, (int)WindowsMessage.WM_NCLBUTTONDOWN, new IntPtr((int)area), new IntPtr(0));
					}
					else
					{
						MouseDownEvent(this, new AxWMPLib._WMPOCXEvents_MouseDownEvent((short)Keys.LButton, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
					}	
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
					// 枠なしのときだけ処理を実行する
					if (!PlayerSettings.FrameInvisible)
					{
						MouseMoveEvent(this, new AxWMPLib._WMPOCXEvents_MouseMoveEvent((short)Keys.None, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
						return;
					}

					area = FormUtility.GetHitArea(frameSize, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16, flash.Width, flash.Height);
					if (area == HitArea.HTNONE)
					{
						MouseMoveEvent(this, new AxWMPLib._WMPOCXEvents_MouseMoveEvent((short)Keys.None, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
						return;
					}

					switch (area)
					{
					case HitArea.HTTOP:
					case HitArea.HTBOTTOM:
						Cursor.Current = Cursors.SizeNS;
						break;
					case HitArea.HTLEFT:
					case HitArea.HTRIGHT:
						Cursor.Current = Cursors.SizeWE;
						break;
					case HitArea.HTTOPLEFT:
					case HitArea.HTBOTTOMRIGHT:
						Cursor.Current = Cursors.SizeNWSE;
						break;
					case HitArea.HTTOPRIGHT:
					case HitArea.HTBOTTOMLEFT:
						Cursor.Current = Cursors.SizeNESW;
						break;
					}
					break;
				case (int)WindowsMessage.WM_KEYDOWN:
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
