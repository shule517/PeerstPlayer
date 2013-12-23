using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstViewer.ThreadViewer
{
	class ThreadViewerPresenter
	{
		private System.Windows.Forms.WebBrowser webBrowser;
		private PeerstLib.Controls.BufferedListView threadListView;
		private System.Windows.Forms.ToolStripButton toolStripButtonWriteField;
		private System.Windows.Forms.SplitContainer splitContainerWriteField;
		private System.Windows.Forms.ToolStripButton toolStripButtonBottom;
		private System.Windows.Forms.ToolStripButton toolStripButtonThreadList;
		private System.Windows.Forms.SplitContainer splitContainerThreadList;
		private System.Windows.Forms.ToolStrip toolStrip;

		public ThreadViewerPresenter(System.Windows.Forms.WebBrowser webBrowser, PeerstLib.Controls.BufferedListView threadListView, System.Windows.Forms.ToolStripButton toolStripButtonWriteField, System.Windows.Forms.SplitContainer splitContainerWriteField, System.Windows.Forms.ToolStripButton toolStripButtonBottom, System.Windows.Forms.ToolStripButton toolStripButtonThreadList, System.Windows.Forms.SplitContainer splitContainerThreadList, System.Windows.Forms.ToolStrip toolStrip)
		{
			// TODO: Complete member initialization
			this.webBrowser = webBrowser;
			this.threadListView = threadListView;
			this.toolStripButtonWriteField = toolStripButtonWriteField;
			this.splitContainerWriteField = splitContainerWriteField;
			this.toolStripButtonBottom = toolStripButtonBottom;
			this.toolStripButtonThreadList = toolStripButtonThreadList;
			this.splitContainerThreadList = splitContainerThreadList;
			this.toolStrip = toolStrip;
		}

		public void Init()
		{

			// 初期表示設定
			webBrowser.DocumentText = @"<head>
<style type=""text/css"">
<!--
U
{
	color: #0000FF;
}

ul
{
	margin: 1px 1px 1px 30px;
}

TT
{
	color: #0000FF;
	text-decoration:underline;
}
-->
</style>
</head>
<body bgcolor=""#E6EEF3"" style=""font-family:'※※※','ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:16px;line-height:18px;"" >
読み込み中...";

			splitContainerThreadList.Panel1Collapsed = true;
			splitContainerWriteField.Panel2Collapsed = true;

			toolStrip.CanOverflow = true;
		}

		/// <summary>
		/// 書き込み欄表示切り替え
		/// </summary>
		public void ToggleWriteField()
		{
			toolStripButtonWriteField.Checked = !toolStripButtonWriteField.Checked;
			splitContainerWriteField.Panel2Collapsed = !toolStripButtonWriteField.Checked;

			if (toolStripButtonBottom.Checked)
			{
				ScrollToBottom();
			}
		}

		/// <summary>
		/// スレッド一覧表示切り替え
		/// </summary>
		public void ToggleThreadList()
		{
			toolStripButtonThreadList.Checked = !toolStripButtonThreadList.Checked;
			splitContainerThreadList.Panel1Collapsed = !toolStripButtonThreadList.Checked;
		}

		/// <summary>
		/// 最上位へスクロール
		/// </summary>
		public void ScrollToTop()
		{
			webBrowser.Document.Window.ScrollTo(0, 0);
		}

		/// <summary>
		/// 最下位へスクロール
		/// </summary>
		public void ScrollToBottom()
		{
			webBrowser.Document.Window.ScrollTo(0, webBrowser.Document.Body.ScrollRectangle.Bottom);
		}
	}
}
