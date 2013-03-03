using System;
using System.Collections.Generic;
using System.Text;

namespace PeerstPlayer
{
	class WindowSizeMenu
	{
		// TODO 
		WindowSizeManager windowSizeManager = null;

		#region サイズ指定（幅）

		public void 幅160ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetWidth(160);
		}

		public void 幅320ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetWidth(320);
		}

		public void 幅480ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetWidth(480);
		}

		public void 幅640ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetWidth(640);
		}

		public void 幅800ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetWidth(800);
		}

		#endregion
	}
}
