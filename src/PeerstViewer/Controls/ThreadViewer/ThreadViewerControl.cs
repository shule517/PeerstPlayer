using PeerstLib.Util;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace PeerstViewer.Controls.ThreadViewer
{
	/// <summary>
	/// スレッドビューワコントロール
	/// </summary>
	public partial class ThreadViewerControl : UserControl
	{
		/// <summary>
		/// スクロール可能な領域
		/// </summary>
		public Rectangle ScrollRectangle
		{
			get
			{
				try
				{
					if (webBrowser.Document == null)
					{
						return new Rectangle();
					}
					return webBrowser.Document.Body.ScrollRectangle;
				}
				catch
				{
					return new Rectangle();
				}
			}
		}

		/// <summary>
		/// スクロール位置が最上位か
		/// </summary>
		public bool IsScrollTop
		{
			get { return (ScrollRectangle.Top == 0); }
		}

		/// <summary>
		/// スクロール位置が最下位か
		/// </summary>
		public bool IsScrollBottom
		{
			get { return ((ScrollRectangle.Height - ScrollRectangle.Top) <= (webBrowser.Height + 1)); }
		}

		/// <summary>
		/// 更新前に最下位か
		/// </summary>
		private bool IsScrollBottomLast = false;

		/// <summary>
		/// 表示する内容
		/// </summary>
		public string DocumentText
		{
			get { return webBrowser.DocumentText; }
			set
			{
				// 更新前のスクロール位置が最下位か
				IsScrollBottomLast = IsScrollBottom;

				// 更新音を出さないために描画時は非表示にする
				webBrowser.Visible = false;
				webBrowser.DocumentText = value;
				webBrowser.Visible = true;
			}
		}

		/// <summary>
		/// 更新完了イベント
		/// </summary>
		public event WebBrowserDocumentCompletedEventHandler DocumentCompleted
		{
			add { webBrowser.DocumentCompleted += value; }
			remove { webBrowser.DocumentCompleted -= value; }
		}

		/// <summary>
		/// スクロールイベント
		/// </summary>
		public event HtmlElementEventHandler DocumentScroll
		{
			add { webBrowser.Document.Window.Scroll += value; }
			remove { webBrowser.Document.Window.Scroll -= value; }
		}

		/// <summary>
		/// スクロール位置を保存
		/// </summary>
		private Point ScrollPos = new Point();

		public ThreadViewerControl()
		{
			Logger.Instance.Debug("ThreadViewerControl()");
			InitializeComponent();

			webBrowser.DocumentCompleted += (sender, e) =>
			{
				Logger.Instance.Debug("webBrowser.DocumentCompleted()");
				if (IsScrollBottomLast)
				{
					// スレッドの最下位へ移動
					ScrollToBottom();
				}
				else
				{
					// スクロール位置を復元
					webBrowser.Document.Window.ScrollTo(ScrollPos.X, ScrollPos.Y);
				}

				webBrowser.Document.Window.Scroll += (sender_, e_) =>
				{
					// スクロール位置を保存
					ScrollPos.X = ScrollRectangle.X;
					ScrollPos.Y = ScrollRectangle.Y;
				};
			};
		}

		/// <summary>
		/// 最上位へスクロール
		/// </summary>
		public void ScrollToTop()
		{
			Logger.Instance.Debug("ScrollToTop()");
			webBrowser.Document.Window.ScrollTo(0, 0);
		}

		/// <summary>
		/// 最下位へスクロール
		/// </summary>
		public void ScrollToBottom()
		{
			Logger.Instance.Debug("ScrollToBottom()");
			webBrowser.Document.Window.ScrollTo(0, ScrollRectangle.Bottom);
		}

		private void webBrowser_NewWindow3(object sender, WebBrowserNewWindow3EventArgs e)
		{
			Process.Start(e.bstrUrl);
			e.Cancel = true;
		}
	}
}
