using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstLib.Controls.Glow
{
	public class GlowForm : Form
	{
		readonly GlowParts top, bottom, left, right;

		public GlowForm()
		{
			if (IsWindowsTen())
			{
				top = new GlowTop(this);
				bottom = new GlowBottom(this);
				left = new GlowLeft(this);
				right = new GlowRight(this);
			}
		}

		public void ShowGlow()
		{
			if (IsWindowsTen())
			{
				top.Show();
				bottom.Show();
				left.Show();
				right.Show();
			}
		}

		public void HideGlow()
		{
			if (IsWindowsTen())
			{
				top.Hide();
				bottom.Hide();
				left.Hide();
				right.Hide();
			}
		}

		public bool GlowVisible()
		{
			return top.Visible;
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			// フォーム非表示にした場合は、Glowも消す
			base.OnVisibleChanged(e);
			if (!Visible)
			{
				HideGlow();
			}
		}

		protected bool IsWindowsTen()
		{
			return Environment.OSVersion.Version.Major >= 10;
		}
	}
}
