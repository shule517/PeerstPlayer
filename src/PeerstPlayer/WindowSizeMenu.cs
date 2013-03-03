using System;
using System.Collections.Generic;
using System.Text;

namespace PeerstPlayer
{
	class WindowSizeMenu
	{
		/// <summary>
		/// ウィンドウサイズ管理
		/// </summary>
		WindowSizeManager windowSizeManager = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="windowSizeManager">ウィンドウサイズ管理</param>
		public WindowSizeMenu(WindowSizeManager windowSizeManager)
		{
			this.windowSizeManager = windowSizeManager;
		}

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

		#region サイズ指定（拡大率）

		public void toolStripMenuItem50Per_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetScale(50);
		}

		public void toolStripMenuItem75Per_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetScale(75);
		}

		public void toolStripMenuItem100Per_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetScale(100);
		}

		public void toolStripMenuItem150_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetScale(150);
		}

		public void toolStripMenuItem200Per_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetScale(200);
		}

		#endregion

		#region サイズ指定（幅×高さ）

		public void x120ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetSize(160, 120);
		}

		public void x240ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetSize(320, 240);
		}

		public void x360ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetSize(480, 360);
		}

		public void x480ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetSize(640, 480);
		}

		public void x600ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetSize(800, 600);
		}

		#endregion

		#region サイズ指定（高さ）

		public void 高さ120ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetHeight(120);
		}

		public void 高さ240ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetHeight(240);
		}

		public void 高さ360ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetHeight(360);
		}

		public void 高さ480ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetHeight(480);
		}

		public void 高さ600ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.SetHeight(600);
		}

		#endregion

		#region 画面分割メニュー（高さx幅）

		public void 分割5X5ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplit(5);
		}

		public void 分割4X4ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplit(4);
		}

		public void 分割3X3ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplit(3);
		}

		public void 分割2X2ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplit(2);
		}

		public void 分割1X1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplit(1);
		}

		#endregion

		#region 画面分割メニュー（幅）

		public void 幅5分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplitWidth(5);
		}

		public void 幅4分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplitWidth(4);
		}

		public void 幅3分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplitWidth(3);
		}

		public void 幅2分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplitWidth(2);
		}

		public void 幅1分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplitWidth(1);
		}

		#endregion

		#region 画面分割メニュー（高さ）

		public void 高さ5分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplitHeight(5);
		}

		public void 高さ4分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplitHeight(4);
		}

		public void 高さ3分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplitHeight(3);
		}

		public void 高さ2分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplitHeight(2);
		}

		public void 高さ1分の1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			windowSizeManager.ScreenSplitHeight(1);
		}

		#endregion
	}
}
