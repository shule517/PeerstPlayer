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
		// ウィンドウサイズ変更用の枠サイズ
		private const int FrameSize = 15;

		private readonly VlcControl parent;
		private readonly IntPtr parentHandle;
		private readonly HookProcedureDelegate mouseHookDelegate;
		private readonly BackgroundWorker worker = new BackgroundWorker();
		private IntPtr hook = IntPtr.Zero;
		private Point startClickPoint;
		private Point startWindowPoint;
		private bool isClick;
		private bool clickScaleMode;
		private bool isDoubleClick;
		private HitArea startArea;
		private RECT startWindowSize;
		private int parentWidth;
		private int parentHeight;

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
		[DllImport("kernel32.dll")]
		static extern uint FormatMessage(
		  uint dwFlags, IntPtr lpSource,
		  uint dwMessageId, uint dwLanguageId,
		  StringBuilder lpBuffer, int nSize,
		  IntPtr Arguments);

		private const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
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
				RECT windowRect;
				var rootHandle = Win32API.GetAncestor(parentHandle, Win32API.GA_ROOT);
				Win32API.ScreenToClient(Win32API.GetAncestor(parentHandle, Win32API.GA_ROOT), ref clientClickPoint);
				switch ((WindowsMessage)wp)
				{
				case WindowsMessage.WM_LBUTTONDOWN:
					// 枠なしのときだけ処理を続ける
					if ((ExStyle)Win32API.GetWindowLong(rootHandle, GWLIndexes.GWL_EXSTYLE) == ExStyle.WS_EX_WINDOWEDGE)
					{
						MouseDownEvent(this, new MouseEventArgs(MouseButtons.Left, 0, clientClickPoint.x, clientClickPoint.y, 0));
						break;
					}
					startClickPoint = new Point(mouseHookStruct.pt.x, mouseHookStruct.pt.y);
					WINDOWPLACEMENT placement;
					Win32API.GetWindowPlacement(rootHandle, out placement);
					startWindowPoint = new Point(placement.normalPosition.left, placement.normalPosition.top);
					startWindowSize = placement.normalPosition;
					var hitArea = GetHitArea(FrameSize, clientClickPoint.x, clientClickPoint.y, parent.Width, parent.Height);
					if (!Win32API.IsZoomed(rootHandle) && hitArea != HitArea.HTNONE)
					{
						startArea = hitArea;
						clickScaleMode = true;
						parentWidth = parent.Width;
						parentHeight = parent.Height;
					}
					else
					{
						isClick = true;
						MouseDownEvent(this, new MouseEventArgs(MouseButtons.Left, 0, clientClickPoint.x, clientClickPoint.y, 0));
					}

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
						clickScaleMode = false;
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
					var deltaX = mouseHookStruct.pt.x - startClickPoint.X;
					var deltaY = mouseHookStruct.pt.y - startClickPoint.Y;
					if (isClick)
					{
						// 最大化されていたら最大化戻す
						if (Win32API.IsZoomed(rootHandle))
						{
							Win32API.ShowWindow(rootHandle, ShowCmd.RESTORE);
						}
						Win32API.GetWindowPlacement(rootHandle, out placement);
						var width = placement.normalPosition.right - placement.normalPosition.left;
						var height = placement.normalPosition.bottom - placement.normalPosition.top;
						var rect = new Rectangle(startWindowPoint.X + deltaX, startWindowPoint.Y + deltaY, width, height);
						rect = SnapScreen(rect);
						rect = SnapWindow(rect);
						Win32API.MoveWindow(rootHandle, rect.Left, rect.Top, width, height, true);

					}
					if (clickScaleMode)
					{
						var rect = startWindowSize;

						if (startArea == HitArea.HTTOPLEFT || startArea == HitArea.HTLEFT || startArea == HitArea.HTBOTTOMLEFT)
						{
							rect.left += deltaX;
						}
						if (startArea == HitArea.HTTOP || startArea == HitArea.HTTOPLEFT || startArea == HitArea.HTTOPRIGHT)
						{
							rect.top += deltaY;
						}

						if (startArea == HitArea.HTTOPRIGHT || startArea == HitArea.HTRIGHT || startArea == HitArea.HTBOTTOMRIGHT)
						{
							rect.right += deltaX;
						}
						if (startArea == HitArea.HTBOTTOMLEFT || startArea == HitArea.HTBOTTOM || startArea == HitArea.HTBOTTOMRIGHT)
						{
							rect.bottom += deltaY;
						}

						if (startArea == HitArea.HTLEFT || startArea == HitArea.HTRIGHT)
						{
							rect.bottom = rect.top + (startWindowSize.bottom - startWindowSize.top - parentHeight) + (int)(parent.Width / parent.AspectRatio);
						}
						if (startArea == HitArea.HTTOP || startArea == HitArea.HTBOTTOM)
						{
							rect.right = rect.left + (int)((parent.Height) * parent.AspectRatio);
						}

						Win32API.MoveWindow(rootHandle, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, true);
						SetCursor(startArea);
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

		private void SetCursor(HitArea area)
		{
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
		}

		/// <summary>
		/// マウスとウィンドウ枠の当たり判定
		/// </summary>
		private HitArea GetHitArea(int frameSize, int fX, int fY, int width, int height)
		{
			// 斜め判定（上
			if (fY <= frameSize)
			{
				// 左上
				if (fX <= frameSize)
				{
					return HitArea.HTTOPLEFT;
				}
				// 右上
				else if (fX > (width - frameSize))
				{
					return HitArea.HTTOPRIGHT;
				}
			}
			// 斜め判定（下
			else if (fY >= (height - frameSize))
			{
				// 左下
				if (fX <= frameSize)
				{
					return HitArea.HTBOTTOMLEFT;
				}
				// 右下
				else if (fX > (width - frameSize))
				{
					return HitArea.HTBOTTOMRIGHT;
				}
			}

			// 上
			if (fY <= frameSize)
			{
				return HitArea.HTTOP;
			}
			// 下
			else if (fY >= (height - frameSize))
			{
				return HitArea.HTBOTTOM;
			}

			// 左
			if (fX <= frameSize)
			{
				return HitArea.HTLEFT;
			}
			// 右
			else if (fX > (width - frameSize))
			{
				return HitArea.HTRIGHT;
			}

			return HitArea.HTNONE;
		}
	}
}
