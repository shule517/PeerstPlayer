using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PeerstLib.Control
{
	//-------------------------------------------------------------
	// 概要：ToolStripクラス
	// 責務：フォーカスの調節
	//-------------------------------------------------------------
	public class ToolStripEx : ToolStrip
	{
		const uint WM_MOUSEACTIVATE = 0x21;
		const uint MA_ACTIVATE = 1;
		const uint MA_ACTIVATEANDEAT = 2;

		//-------------------------------------------------------------
		// 概要：ウィンドウプロシージャ
		//-------------------------------------------------------------
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
