using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeerstLib.Forms;

namespace PeerstLib.Controls.Glow
{
	partial class GlowParts : Form
	{
		public int GlowSize { get; private set; }

		public Color GlowColor { get; private set; }

		public Color GlowAlphaColor { get; private set; }

		public Color DeactiveColor { get; private set; }

		protected Bitmap surface;
		protected bool isGlowing;

		public GlowParts(Form owner)
		{
			InitializeComponent();

			Owner = owner;
			GlowSize = 8;

			Owner.LocationChanged += UpdatePosition;
			Owner.SizeChanged += Update;
			Owner.Activated += Update;
			Owner.Deactivate += Update;
			Owner.VisibleChanged += Update;
			Owner.Closing += (sender, args) =>
			{
				Close();
			};
			FormClosed += (sender, args) =>
			{
				Owner.LocationChanged -= UpdatePosition;
				Owner.SizeChanged -= Update;
				Owner.Activated -= Update;
				Owner.Deactivate -= Update;
				Owner.VisibleChanged -= Update;
			};
			int color;
			bool opaque;
			Win32API.DwmGetColorizationColor(out color, out opaque);
			GlowColor = Color.FromArgb(color);
			GlowAlphaColor = Color.FromArgb(64, GlowColor.R, GlowColor.G, GlowColor.B);
			DeactiveColor = Color.FromArgb(43, 60, 77);
			//GlowColor = Color.FromArgb(1, 121, 203);
			//GlowAlphaColor = Color.FromArgb(64, 1, 121, 203);
		}

		private void Update(object sender, EventArgs e)
		{
			UpdatePosition(sender, e);
			isGlowing = ActiveForm != null && ClientSize.Width != 0 && ClientSize.Height != 0;
			CreateGlow();
			SetLayeredWindow(surface);
		}

		private void UpdatePosition(object sender, EventArgs e)
		{
			var left = GetLeft(Owner.Left, Owner.Width);
			var top = GetTop(Owner.Top, Owner.Height);
			var width = GetWidth(Owner.Left, Owner.Width);
			var height = GetHeight(Owner.Top, Owner.Height);
			Win32API.SetWindowPos(Handle, Owner.Handle, left, top, width, height, (uint)SWP.NOACTIVATE);
		}

		protected virtual void CreateGlow()
		{
			surface = new Bitmap(Width, Height);
		}

		protected void SetLayeredWindow(Bitmap srcBitmap)
		{
			const byte AC_SRC_OVER = 0;
			const byte AC_SRC_ALPHA = 1;
			const int ULW_ALPHA = 2;

			using (var graphicsScreen = Graphics.FromHwnd(IntPtr.Zero))
			using (var graphicsBitmap = Graphics.FromImage(srcBitmap))
			{
				var hdcScreen = graphicsScreen.GetHdc();
				var hdcBitmap = graphicsBitmap.GetHdc();
				var old = Win32API.SelectObject(hdcBitmap, srcBitmap.GetHbitmap(Color.FromArgb(0)));

				var blend = new BLENDFUNCTION
				{
					BlendOp = AC_SRC_OVER,
					BlendFlags = 0,
					SourceConstantAlpha = 255,
					AlphaFormat = AC_SRC_ALPHA
				};
				var point = new POINT
				{
					x = Left,
					y = Top
				};

				var surfaceSize = new Size(Width, Height);
				var surfacePoint = new POINT();
				Win32API.UpdateLayeredWindow(Handle, hdcScreen, ref point, ref surfaceSize,
					hdcBitmap, ref surfacePoint, 0, ref blend, ULW_ALPHA);

				Win32API.DeleteObject(Win32API.SelectObject(hdcBitmap, old));
				graphicsScreen.ReleaseHdc(hdcScreen);
				graphicsBitmap.ReleaseHdc(hdcBitmap);
			}
		}

		virtual protected int GetLeft(int ownerLeft, int ownerWidth)
		{
			return 0;
		}

		virtual protected int GetTop(int ownerTop, int ownerHeight)
		{
			return 0;
		}

		virtual protected int GetWidth(int ownerLeft, int ownerWidth)
		{
			return 0;
		}

		virtual protected int GetHeight(int ownerTop, int ownerHeight)
		{
			return 0;
		}

		protected override CreateParams CreateParams
		{
			get
			{
				var cp = base.CreateParams;
				cp.ExStyle |= (int)WSEX.TOOLWINDOW | (int)WSEX.LAYERED;
				return cp;
			}
		}

		protected override void WndProc(ref Message m)
		{
			switch ((WindowsMessage)m.Msg)
			{
			case WindowsMessage.WM_LBUTTONDOWN:
				Win32API.SendMessage(Owner.Handle, (int)WindowsMessage.WM_NCLBUTTONDOWN, (IntPtr)GetHitTest(LParamToPoint(m.LParam), Width, Height), IntPtr.Zero);
				break;
			case WindowsMessage.WM_MOUSEMOVE:
				Win32API.ReleaseCapture();
				break;
			case WindowsMessage.WM_NCHITTEST:
				Cursor = GetCursor(PointToClient(LParamToPoint(m.LParam)), Width, Height);
				break;
			case WindowsMessage.WM_DWMCOLORIZATIONCOLORCHANGED:
				var color = (long)m.WParam;
				GlowColor = Color.FromArgb((byte)(color >> 24), (byte)(color >> 16), (byte)(color >> 8), (byte)(color));
				GlowAlphaColor = Color.FromArgb(64, GlowColor.R, GlowColor.G, GlowColor.B);
				Update(this, null);
				break;
			/*
			なぜか動かない
			case WindowsMessage.WM_LBUTTONDBLCLK:
				if (this is GlowTop)
				{
					Win32API.SendMessage(Owner.Handle, (int)WindowsMessage.WM_NCLBUTTONDBLCLK, (IntPtr)HitTest.Top, IntPtr.Zero);
				}
				else if (this is GlowBottom)
				{
					Win32API.SendMessage(Owner.Handle, (int)WindowsMessage.WM_NCLBUTTONDBLCLK, (IntPtr)HitTest.Bottom, IntPtr.Zero);
				}
				break;
			*/
            }
			base.WndProc(ref m);
		}

		protected virtual HitTest GetHitTest(Point point, int width, int height)
		{
			return HitTest.Caption;
		}

		protected virtual Cursor GetCursor(Point point, int width, int height)
		{
			return Cursors.Arrow;
		}

		protected Point LParamToPoint(IntPtr lp)
		{
			return new Point((short)((uint)lp & 0xFFFF), (short)((uint)lp >> 16));
		}
	}
}
