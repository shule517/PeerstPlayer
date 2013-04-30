using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer
{
	public class ToolStripEx : ToolStrip
	{
		public ToolStripEx()
		{
			MouseLeave += new EventHandler(ToolStripEx_MouseLeave);
		}

		void ToolStripEx_MouseLeave(object sender, EventArgs e)
		{
			Visible = false;
		}

		const uint WM_MOUSEACTIVATE = 0x21;
		const uint MA_ACTIVATE = 1;
		const uint MA_ACTIVATEANDEAT = 2;

		private bool enableClickThrough = true;

		public bool EnableClickThrough
		{
			get { return this.enableClickThrough; }
			set { this.enableClickThrough = value; }
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (this.enableClickThrough
				&& m.Msg == WM_MOUSEACTIVATE && m.Result == (IntPtr)MA_ACTIVATEANDEAT)
			{
				m.Result = (IntPtr)MA_ACTIVATE;
			}
		}
	}
}
