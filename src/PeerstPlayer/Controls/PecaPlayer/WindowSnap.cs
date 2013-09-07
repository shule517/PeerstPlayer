using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PeerstLib.Controls;

namespace PeerstPlayer.Controls.PecaPlayer
{
	//-------------------------------------------------------------
	// 概要：ウィンドウスナップクラス
	//-------------------------------------------------------------
	public class WindowSnap : NativeWindow
	{
		System.Windows.Forms.Form form;
		PecaPlayerControl pecaPlayer;

		// スクリーン吸着距離
		private const int ScreenMagnetDockDist = 20;

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		//-------------------------------------------------------------
		public WindowSnap(System.Windows.Forms.Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;

			// サブクラスウィンドウの設定
			AssignHandle(form.Handle);
		}

		//-------------------------------------------------------------
		// 概要：ウィンドウプロシージャ
		//-------------------------------------------------------------
		protected override void WndProc(ref Message m)
		{
			if ((WindowMessage)m.Msg == WindowMessage.WM_MOVING)
			{
				SnapScreen(m);
				SnapWindow(m);
			}

			base.WndProc(ref m);
		}

		//-------------------------------------------------------------
		// 概要：ディスプレイ枠にスナップする
		//-------------------------------------------------------------
		private void SnapScreen(Message m)
		{
			int left = Marshal.ReadInt32(m.LParam, 0);
			int top = Marshal.ReadInt32(m.LParam, 4);

			// マウスポジションの取得
			Rectangle screen = System.Windows.Forms.Screen.GetBounds(form);
			int mouseX = System.Windows.Forms.Control.MousePosition.X;
			int mouseY = System.Windows.Forms.Control.MousePosition.Y;

			// 左枠
			if (Math.Abs((mouseX - pecaPlayer.ClickPoint.X) - screen.Left) <= ScreenMagnetDockDist)
			{
				left = screen.Left;
			}

			// 上枠
			if (Math.Abs(mouseY - pecaPlayer.ClickPoint.Y) <= ScreenMagnetDockDist)
			{
				top = screen.Top;
			}

			// 右枠
			if (Math.Abs((mouseX + form.Width) - (pecaPlayer.ClickPoint.X + screen.Width) - screen.X) <= ScreenMagnetDockDist)
			{
				left = screen.Right - form.Width;
			}

			// 下枠
			if (Math.Abs((mouseY + form.Height) - (pecaPlayer.ClickPoint.Y + screen.Height)) <= ScreenMagnetDockDist)
			{
				top = screen.Bottom - form.Height;
			}

			Marshal.WriteInt32(m.LParam, 0, left);
			Marshal.WriteInt32(m.LParam, 4, top);
			Marshal.WriteInt32(m.LParam, 8, left + form.Width);
			Marshal.WriteInt32(m.LParam, 12, top + form.Height);
		}

		//-------------------------------------------------------------
		// 概要：ウィンドウ枠にスナップする
		//-------------------------------------------------------------
		private void SnapWindow(Message m)
		{
			int left = Marshal.ReadInt32(m.LParam, 0);
			int top = Marshal.ReadInt32(m.LParam, 4);

			// マウスポジションの取得
			Rectangle screen = System.Windows.Forms.Screen.GetBounds(form);
			int mouseX = System.Windows.Forms.Control.MousePosition.X;
			int mouseY = System.Windows.Forms.Control.MousePosition.Y;

			// 左
			POINT leftPoint;
			leftPoint.x = left - ScreenMagnetDockDist;
			leftPoint.y = top + form.Height / 2;
			IntPtr leftHandle = Win32API.WindowFromPoint(leftPoint);

			// 上
			POINT topPoint;
			topPoint.x = left + form.Width / 2;
			topPoint.y = top - ScreenMagnetDockDist;
			IntPtr topHandle = Win32API.WindowFromPoint(topPoint);

			// 右
			POINT rightPoint;
			rightPoint.x = left + form.Width + ScreenMagnetDockDist;
			rightPoint.y = top + form.Height / 2;
			IntPtr rightHandle = Win32API.WindowFromPoint(rightPoint);

			// 下
			POINT bottomPoint;
			bottomPoint.x = topPoint.x;
			bottomPoint.y = top + form.Height + ScreenMagnetDockDist;
			IntPtr bottomHandle = Win32API.WindowFromPoint(bottomPoint);

			// 左
			if (leftHandle != IntPtr.Zero)
			{
				RECT rect;
				IntPtr handle = Win32API.GetAncestor(leftHandle, Win32API.GA_ROOT);

				if (Win32API.GetWindowRect(handle, out rect))
				{
					if (Math.Abs(mouseX - pecaPlayer.ClickPoint.X - rect.right) <= ScreenMagnetDockDist)
					{
						left = rect.right;
					}
				}
			}

			// 上
			if (topHandle != IntPtr.Zero)
			{
				RECT rect;
				IntPtr handle = Win32API.GetAncestor(topHandle, Win32API.GA_ROOT);

				if (Win32API.GetWindowRect(handle, out rect))
				{
					if (Math.Abs(mouseY - pecaPlayer.ClickPoint.Y - rect.bottom) <= ScreenMagnetDockDist)
					{
						top = rect.bottom;
					}
				}
			}

			// 右
			if (rightHandle != IntPtr.Zero)
			{
				RECT rect;
				IntPtr handle = Win32API.GetAncestor(rightHandle, Win32API.GA_ROOT);

				if (Win32API.GetWindowRect(handle, out rect))
				{
					if (Math.Abs(mouseX + form.Width - (pecaPlayer.ClickPoint.X + rect.left)) <= ScreenMagnetDockDist)
					{
						left = rect.left - form.Width;
					}
				}
			}
	
			// 下
			if (bottomHandle != IntPtr.Zero)
			{
				RECT rect;
				IntPtr handle = Win32API.GetAncestor(bottomHandle, Win32API.GA_ROOT);

				if (Win32API.GetWindowRect(handle, out rect))
				{
					if (Math.Abs((mouseY + form.Height) - (pecaPlayer.ClickPoint.Y + rect.top)) <= ScreenMagnetDockDist)
					{
						top = rect.top - form.Height;
					}
				}
			}

			Marshal.WriteInt32(m.LParam, 0, left);
			Marshal.WriteInt32(m.LParam, 4, top);
			Marshal.WriteInt32(m.LParam, 8, left + form.Width);
			Marshal.WriteInt32(m.LParam, 12, top + form.Height);
		}
	}
}
