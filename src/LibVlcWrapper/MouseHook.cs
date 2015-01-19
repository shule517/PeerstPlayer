using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LibVlcWrapper
{
	public class MouseHook
	{
		public MouseEventHandler MouseDownEvent = delegate { };
		public MouseEventHandler MouseUpEvent = delegate { };
		public MouseEventHandler DoubleClickEvent = delegate { };
		public MouseEventHandler MouseMoveEvent = delegate { };

		// スクリーン吸着距離
		private const int ScreenMagnetDockDist = 20;
		
		private readonly VlcControl parent;
		private readonly IntPtr parentHandle;
		private readonly HookProcedureDelegate mouseHookDelegate;
		private readonly BackgroundWorker worker = new BackgroundWorker();
		private IntPtr hook = IntPtr.Zero;
		private Point startClickPoint;
		private Point startWindowPoint;
		private bool isClick = false;
		private bool isDoubleClick = false;

		public MouseHook(VlcControl parent)
		{
			this.parent = parent;
			parentHandle = parent.Handle;
			// デリゲートを変数に入れて参照しないとGCに回収されることがある?
			mouseHookDelegate = MouseHookDelegate;

			worker.DoWork += (sender, args) =>
			{
				// 直ちに動画を表示するコントロールが生成されるわけではないので繰り返す
				for (;;)
				{
					if (hook != IntPtr.Zero) break;

					Win32API.EnumChildWindows((IntPtr)args.Argument, (handle, param) =>
					{
						var stringBuilder = new StringBuilder(256);
						Win32API.GetClassName(handle, stringBuilder, stringBuilder.Capacity);
						if (stringBuilder.ToString().IndexOf("VLC MSW") != -1)
						{
							uint processId;
							uint threadId = Win32API.GetWindowThreadProcessId(handle, out processId);
							parent.Invoke(new Action(() =>
							{
								hook = Win32API.SetWindowsHookEx(HookType.WH_MOUSE, mouseHookDelegate, IntPtr.Zero, (int)threadId);
								if (hook == IntPtr.Zero)
								{
									Trace.WriteLine("SetWindowsHookEx failed.");
								}
							}));
							return false;
						}
						return true;
					}, IntPtr.Zero);
					Thread.Sleep(100);
				}
			};
		}

		~MouseHook()
		{
			Unhook();
		}

		public void Hook()
		{
			// 既にフックしていたら解除する
			if (hook != IntPtr.Zero) Unhook();

			parent.Invoke(new Action(() => worker.RunWorkerAsync(parent.Handle)));
		}

		public void Unhook()
		{
			if (hook == IntPtr.Zero) return;
			if (Win32API.UnhookWindowsHookEx(hook))
			{
				Trace.WriteLine("UnhookWindowsHookEx failed");
			}
		}

		private IntPtr MouseHookDelegate(int code, IntPtr wp, IntPtr lp)
		{
			if (code >= 0)
			{
				var mouseHookStruct = (MOUSEHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(MOUSEHOOKSTRUCT));
				var clientClickPoint = new POINT()
				{
					x = mouseHookStruct.pt.x,
					y = mouseHookStruct.pt.y,
				};
				Win32API.ScreenToClient(Win32API.GetAncestor(parentHandle, Win32API.GA_ROOT), ref clientClickPoint);
				switch ((WindowsMessage)wp)
				{
				case WindowsMessage.WM_LBUTTONDOWN:
					Win32API.SetFocus(parentHandle);
					isClick = true;
					startClickPoint = new Point(mouseHookStruct.pt.x, mouseHookStruct.pt.y);
					RECT windowRect;
					Win32API.GetWindowRect(Win32API.GetAncestor(parentHandle, Win32API.GA_ROOT), out windowRect);
					startWindowPoint = new Point(windowRect.left, windowRect.top);
					MouseDownEvent(this, new MouseEventArgs(MouseButtons.Left, 0, clientClickPoint.x, clientClickPoint.y, 0));
					break;
				case WindowsMessage.WM_RBUTTONDOWN:
					MouseDownEvent(this, new MouseEventArgs(MouseButtons.Right, 0, clientClickPoint.x, clientClickPoint.y, 0));
					break;
				case WindowsMessage.WM_MBUTTONDOWN:
					MouseDownEvent(this, new MouseEventArgs(MouseButtons.Middle, 0, clientClickPoint.x, clientClickPoint.y, 0));
					break;
				case WindowsMessage.WM_LBUTTONUP:
					if (isDoubleClick)
					{
						isDoubleClick = false;
						DoubleClickEvent(this, new MouseEventArgs(MouseButtons.Left, 0, clientClickPoint.x, clientClickPoint.y, 0));
					}
					else
					{
						isClick = false;
						MouseUpEvent(this, new MouseEventArgs(MouseButtons.Left, 0, clientClickPoint.x, clientClickPoint.y, 0));						
					}

					break;
				case WindowsMessage.WM_RBUTTONUP:
					MouseUpEvent(this, new MouseEventArgs(MouseButtons.Right, 0, clientClickPoint.x, clientClickPoint.y, 0));
					break;
				case WindowsMessage.WM_LBUTTONDBLCLK:
					isDoubleClick = true;
					break;
				case WindowsMessage.WM_MOUSEMOVE:
					if (isClick)
					{
						var deltaX = mouseHookStruct.pt.x - startClickPoint.X;
						var deltaY = mouseHookStruct.pt.y - startClickPoint.Y;
						var rootHandle = Win32API.GetAncestor(parentHandle, Win32API.GA_ROOT);
						Win32API.GetWindowRect(rootHandle, out windowRect);
						var width = windowRect.right - windowRect.left;
						var height = windowRect.bottom - windowRect.top;
						var rect = new Rectangle(startWindowPoint.X + deltaX, startWindowPoint.Y + deltaY, width, height);
						rect = SnapScreen(rect);
						rect = SnapWindow(rect);
						Win32API.MoveWindow(rootHandle, rect.Left, rect.Top, width, height, true);					
					}
					MouseMoveEvent(this, new MouseEventArgs(MouseButtons.None, 0, clientClickPoint.x, clientClickPoint.y, 0));
					break;
				}
			}
			return Win32API.CallNextHookEx(hook, code, wp, lp);
		}

		private Rectangle SnapScreen(Rectangle rect)
		{
			var left = rect.Left;
			var top = rect.Top;

			var screen = Screen.GetBounds(new Point(rect.Left, rect.Top));

			if (Math.Abs(left - screen.Left) <= ScreenMagnetDockDist)
			{
				left = screen.Left;
			}
			if (Math.Abs(rect.Top) <= ScreenMagnetDockDist)
			{
				top = screen.Top;
			}
			if (Math.Abs(rect.Right - screen.Width) <= ScreenMagnetDockDist)
			{
				left = screen.Right - (rect.Right - rect.Left);
			}
			if (Math.Abs(rect.Bottom - screen.Height) <= ScreenMagnetDockDist)
			{
				top = screen.Bottom - (rect.Bottom - rect.Top);
			}

			return new Rectangle(left, top, rect.Width, rect.Height);
		}

		private Rectangle SnapWindow(Rectangle rect)
		{
			var left = rect.Left;
			var top = rect.Top;

			POINT leftPoint;
			leftPoint.x = rect.Left - ScreenMagnetDockDist;
			leftPoint.y = rect.Top + rect.Height / 2;
			IntPtr leftHandle = Win32API.WindowFromPoint(leftPoint);

			POINT topPoint;
			topPoint.x = rect.Left + rect.Width / 2;
			topPoint.y = rect.Top - ScreenMagnetDockDist;
			IntPtr topHandle = Win32API.WindowFromPoint(topPoint);

			POINT rightPoint;
			rightPoint.x = rect.Left + rect.Width + ScreenMagnetDockDist;
			rightPoint.y = rect.Top + rect.Height / 2;
			IntPtr rightHandle = Win32API.WindowFromPoint(rightPoint);

			POINT bottomPoint;
			bottomPoint.x = topPoint.x;
			bottomPoint.y = rect.Top + rect.Height + ScreenMagnetDockDist;
			IntPtr bottomHandle = Win32API.WindowFromPoint(bottomPoint);

			// 左
			if (leftHandle != IntPtr.Zero)
			{
				RECT rect2;
				IntPtr handle = Win32API.GetAncestor(leftHandle, Win32API.GA_ROOT);

				if (Win32API.GetWindowRect(handle, out rect2))
				{
					if (Math.Abs(left - rect2.right) <= ScreenMagnetDockDist)
					{
						left = rect2.right;
					}
				}
			}

			// 上
			if (topHandle != IntPtr.Zero)
			{
				RECT rect2;
				IntPtr handle = Win32API.GetAncestor(topHandle, Win32API.GA_ROOT);

				if (Win32API.GetWindowRect(handle, out rect2))
				{
					if (Math.Abs(top - rect2.bottom) <= ScreenMagnetDockDist)
					{
						top = rect2.bottom;
					}
				}
			}

			// 右
			if (rightHandle != IntPtr.Zero)
			{
				RECT rect2;
				IntPtr handle = Win32API.GetAncestor(rightHandle, Win32API.GA_ROOT);

				if (Win32API.GetWindowRect(handle, out rect2))
				{
					if (Math.Abs(rect.Right - rect2.left) <= ScreenMagnetDockDist)
					{
						left = rect2.left - rect.Width;
					}
				}
			}

			// 下
			if (bottomHandle != IntPtr.Zero)
			{
				RECT rect2;
				IntPtr handle = Win32API.GetAncestor(bottomHandle, Win32API.GA_ROOT);

				if (Win32API.GetWindowRect(handle, out rect2))
				{
					if (Math.Abs(rect.Bottom - rect2.top) <= ScreenMagnetDockDist)
					{
						top = rect2.top - rect.Height;
					}
				}
			}

			return new Rectangle(left, top, rect.Width, rect.Height);
		}
	}
}
