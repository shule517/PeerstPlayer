using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstLib.Controls.Glow
{
	class GlowTop : GlowParts
	{
		public GlowTop(Form owner) : base(owner)
		{
		}

		protected override void CreateGlow()
		{
			base.CreateGlow();

			var glowColor = isGlowing ? GlowColor : DeactiveColor;
			using (var g = Graphics.FromImage(surface))
			using (var pen = new Pen(glowColor, 1.0f))
			{
				if (isGlowing)
				{
					var linearGradientBrush = new LinearGradientBrush(
						ClientRectangle,
						Color.Transparent, GlowAlphaColor,
						LinearGradientMode.Vertical);
					g.FillRectangle(linearGradientBrush, ClientRectangle);
					linearGradientBrush.Dispose();
				}
				g.DrawLine(pen, 0, GlowSize - 1, Owner.Width, GlowSize - 1);
			}
		}

		protected override int GetLeft(int ownerLeft, int ownerWidth)
		{
			return ownerLeft;
		}

		protected override int GetTop(int ownerTop, int ownerHeight)
		{
			return ownerTop - GlowSize;
		}

		protected override int GetWidth(int ownerLeft, int ownerWidth)
		{
			return ownerWidth;
		}

		protected override int GetHeight(int ownerTop, int ownerHeight)
		{
			return GlowSize;
		}

		protected override HitTest GetHitTest(Point point, int width, int height)
		{
			if (point.X <= GlowSize && point.Y <= GlowSize)
			{
				return HitTest.TopLeft;
			}
			if (point.X >= Width - GlowSize && point.Y >= Height - GlowSize)
			{
				return HitTest.TopRight;
			}
			return HitTest.Top;
		}

		protected override Cursor GetCursor(Point point, int width, int height)
		{
			if (point.X <= GlowSize && point.Y <= GlowSize)
			{
				return Cursors.SizeNWSE;
			}
			if (point.X >= Width - GlowSize && point.Y >= Height - GlowSize)
			{
				return Cursors.SizeNESW;
			}
			return Cursors.SizeNS;
		}
	}
}
