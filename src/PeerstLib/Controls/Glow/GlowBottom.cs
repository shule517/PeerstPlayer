using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstLib.Controls.Glow
{
	class GlowBottom : GlowParts
	{
		public GlowBottom(Form owner) : base(owner)
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
						GlowAlphaColor, Color.Transparent,
						LinearGradientMode.Vertical);
					g.FillRectangle(linearGradientBrush, ClientRectangle);
					linearGradientBrush.Dispose();
				}
				g.DrawLine(pen, 0, 0, Owner.Width, 0);
			}
		}

		protected override int GetLeft(int ownerLeft, int ownerWidth)
		{
			return ownerLeft;
		}

		protected override int GetTop(int ownerTop, int ownerHeight)
		{
			return ownerTop + ownerHeight;
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
				return HitTest.BottomLeft;
			}
			if (point.X >= Width - GlowSize && point.Y >= Height - GlowSize)
			{
				return HitTest.BottomRight;
			}
			return HitTest.Bottom;
		}

		protected override Cursor GetCursor(Point point, int width, int height)
		{
			if (point.X <= GlowSize && point.Y <= GlowSize)
			{
				return Cursors.SizeNESW;
			}
			if (point.X >= Width - GlowSize && point.Y >= Height - GlowSize)
			{
				return Cursors.SizeNWSE;
			}
			return Cursors.SizeNS;
		}
	}
}
