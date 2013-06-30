using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PeerstLib.Control;
using PeerstPlayer.Control;

namespace PeerstLib.Form
{
	//-------------------------------------------------------------
	// 概要：ウィンドウスナップクラス
	//-------------------------------------------------------------
	public class SnapWindow : NativeWindow
	{
		System.Windows.Forms.Form form;
		PecaPlayer pecaPlayer;

		// スクリーン吸着距離
		private const int ScreenMagnetDockDist = 20;

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		//-------------------------------------------------------------
		public SnapWindow(System.Windows.Forms.Form form, PecaPlayer pecaPlayer)
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

			Rectangle screen = System.Windows.Forms.Screen.GetBounds(form);

			int mouseX = ((Cursor.Position.X < 0) ? (Cursor.Position.X + screen.Width) : Cursor.Position.X);
			int mouseY = Cursor.Position.Y;

			// 上枠
			if (Math.Abs(mouseY - pecaPlayer.ClickPoint.Y) <= ScreenMagnetDockDist)
			{
				top = screen.Top;
			}

			// 下枠
			if (Math.Abs((mouseY + form.Height) - (pecaPlayer.ClickPoint.Y + screen.Height)) <= ScreenMagnetDockDist)
			{
				top = screen.Bottom - form.Height;
			}

			// 左枠
			if (Math.Abs(mouseX - pecaPlayer.ClickPoint.X) <= ScreenMagnetDockDist)
			{
				left = screen.Left;
			}

			// 右枠
			int a = Cursor.Position.X + form.Width;
			if (Math.Abs((mouseX + form.Width) - (pecaPlayer.ClickPoint.X + screen.Width)) <= ScreenMagnetDockDist)
			{
				left = screen.Right - form.Width;
			}

			Marshal.WriteInt32(m.LParam, 0, left);
			Marshal.WriteInt32(m.LParam, 4, top);
			Marshal.WriteInt32(m.LParam, 8, left + form.Width);
			Marshal.WriteInt32(m.LParam, 12, top + form.Height);
		}
	}
}
