using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PeerstLib.Control
{
	// ToolStripクラス
	// フォーカスが当たっていない状態でのボタン押下に対応
	public class ToolStripEx : ToolStrip
	{
		public ToolStripEx()
		{
		}

		const uint WM_MOUSEACTIVATE = 0x21;
		const uint MA_ACTIVATE = 1;
		const uint MA_ACTIVATEANDEAT = 2;

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if ((m.Msg == WM_MOUSEACTIVATE) && (m.Result == (IntPtr)MA_ACTIVATEANDEAT))
			{
				m.Result = (IntPtr)MA_ACTIVATE;
			}
		}
	}
}
