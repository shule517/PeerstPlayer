using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstLib.Controls.Glow
{
	class GlowRight : GlowParts
	{
		public GlowRight(Form owner) : base(owner)
		{
		}

		protected override void CreateGlow()
		{
			base.CreateGlow();

			var glowColor = isGlowing ? GlowColor : DeactiveColor;
			using (var g = Graphics.FromImage(surface))
			using (var pen = new Pen(glowColor, 1.0f))
			{
				var rect = new Rectangle(0, GlowSize - 1, GlowSize, Height - GlowSize * 2 + 1);

				if (isGlowing)
				{
					DrawGlowCorner(g);
					var linearGradientBrush = new LinearGradientBrush(rect,
						GlowAlphaColor, Color.Transparent, LinearGradientMode.Horizontal);
					g.FillRectangle(linearGradientBrush, rect);
					linearGradientBrush.Dispose();
				}
				g.DrawLine(pen, rect.X, rect.Y, 0, Height - GlowSize);
			}
		}

		private void DrawGlowCorner(Graphics g)
		{
			using (var graphicsPath = new GraphicsPath())
			using (var graphicsPath2 = new GraphicsPath())
			{
				var rect = new Rectangle(-GlowSize, -1, GlowSize * 2, GlowSize * 2);
				var rect2 = new Rectangle(-GlowSize, Owner.Height, GlowSize * 2, GlowSize * 2);
				graphicsPath.AddEllipse(rect);
				graphicsPath2.AddEllipse(rect2);
				using (var pathGradientBrush = new PathGradientBrush(graphicsPath))
				using (var pathGradientBrush2 = new PathGradientBrush(graphicsPath2))
				{
					pathGradientBrush.CenterColor = GlowAlphaColor;
					pathGradientBrush.SurroundColors = new[] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
					pathGradientBrush2.CenterColor = GlowAlphaColor;
					pathGradientBrush2.SurroundColors = new[] { Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent };
					g.FillPie(pathGradientBrush, rect, 270, 90);
					g.FillPie(pathGradientBrush2, rect2, 0, 90);
				}
			}
		}

		protected override int GetLeft(int ownerLeft, int ownerWidth)
		{
			return ownerLeft + ownerWidth;
		}

		protected override int GetTop(int ownerTop, int ownerHeight)
		{
			return ownerTop - GlowSize;
		}

		protected override int GetWidth(int ownerLeft, int ownerWidth)
		{
			return GlowSize;
		}

		protected override int GetHeight(int ownerTop, int ownerHeight)
		{
			return ownerHeight + GlowSize * 2;
		}

		protected override HitTest GetHitTest(Point point, int width, int height)
		{
			if (point.X <= GlowSize * 2 && point.Y <= GlowSize * 2)
			{
				return HitTest.TopRight;
			}
			if (point.X >= Width - GlowSize * 2 && point.Y >= Height - GlowSize * 2)
			{
				return HitTest.BottomRight;
			}
			return HitTest.Right;
		}

		protected override Cursor GetCursor(Point point, int width, int height)
		{
			if (point.X <= GlowSize * 2 && point.Y <= GlowSize * 2)
			{
				return Cursors.SizeNESW;
			}
			if (point.X >= Width - GlowSize * 2 && point.Y >= Height - GlowSize * 2)
			{
				return Cursors.SizeNWSE;
			}
			return Cursors.SizeWE;
		}
	}
}
