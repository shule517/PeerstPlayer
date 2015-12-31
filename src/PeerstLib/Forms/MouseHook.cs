using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using PeerstLib.Controls;

namespace PeerstLib.Forms
{
	
	public class MouseHook : IDisposable
	{
		public class MouseHookEventArgs : EventArgs
		{
			public IntPtr Handle { get; private set; }
			public WindowsMessage WindowsMessage { get; private set; }
			public int X { get; private set; }
			public int Y { get; private set; }

			public MouseHookEventArgs(IntPtr handle, WindowsMessage windowsMessage, int x, int y)
			{
				Handle = handle;
				WindowsMessage = windowsMessage;
				X = x;
				Y = y;
			}

			public MouseHookEventArgs(IntPtr handle, WindowsMessage windowsMessage, POINT point)
			{
				Handle = handle;
				WindowsMessage = windowsMessage;
				X = point.x;
				Y = point.y;
			}
		}

		private readonly IntPtr hook;
		private readonly HookProcedureDelegate hookDelegate;

		public event Func<MouseHookEventArgs, bool> OnMouseHook = delegate(MouseHookEventArgs args) { return false; };

		public MouseHook(IntPtr hwnd)
		{
			// デリゲートを変数に入れて参照しないとGCに回収されることがある?
			hookDelegate = MouseHookDelegate;

			uint processId;
			uint threadId = Win32API.GetWindowThreadProcessId(hwnd, out processId);
			hook = Win32API.SetWindowsHookEx(HookType.WH_MOUSE, hookDelegate, IntPtr.Zero, (int)threadId);
		}

		private IntPtr MouseHookDelegate(int code, IntPtr wp, IntPtr lp)
		{
			if (code == 0)
			{
				var mouseHookStruct = (MOUSEHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(MOUSEHOOKSTRUCT));
				var eventArgs = new MouseHookEventArgs(mouseHookStruct.hwnd, (WindowsMessage)wp, mouseHookStruct.pt);
				var result = false;
				foreach (var f in OnMouseHook.GetInvocationList())
				{
					if ((bool)f.DynamicInvoke(eventArgs))
					{
						result = true;
					}
				}
				if (result)
				{
					return new IntPtr(1);
				}
			}
			return Win32API.CallNextHookEx(hook, code, wp, lp);
		}

		~MouseHook()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected void Dispose(bool disposing)
		{
			if (hook != IntPtr.Zero)
			{
				Win32API.UnhookWindowsHookEx(hook);
			}
			GC.SuppressFinalize(this);
		}
	}
}
