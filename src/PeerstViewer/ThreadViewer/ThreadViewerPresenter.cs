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

		public ThreadViewerPresenter(System.Windows.Forms.WebBrowser webBrowser, PeerstLib.Controls.BufferedListView threadListView)
		{
			// TODO: Complete member initialization
			this.webBrowser = webBrowser;
			this.threadListView = threadListView;
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
