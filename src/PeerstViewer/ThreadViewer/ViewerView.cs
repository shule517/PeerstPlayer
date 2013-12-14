using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PeerstViewer.ThreadViewer
{
	/// <summary>
	/// スレッドビューワ画面
	/// </summary>
	public partial class ThreadViewerView : Form
	{
		private ThradViewerViewModel viewModel = new ThradViewerViewModel();

		private Point ScrollPos = new Point();

		public ThreadViewerView()
		{
			InitializeComponent();

			// データバインド設定
			// TODO データバインドするとブラウザにフォーカスを当てた時に更新が走ってしまう
			//webBrowser.DataBindings.Add("DocumentText", viewModel, "DocumentText");
			textBoxUrl.DataBindings.Add("Text", viewModel, "ThreadUrl");
			textBoxMessage.DataBindings.Add("Text", viewModel, "Message");

			viewModel.PropertyChanged += (sender, e) =>
			{
				switch (e.PropertyName)
				{
					case "DocumentText":
						// 更新音を出さないために描画時は非表示にする
						webBrowser.Visible = false;
						webBrowser.DocumentText = viewModel.DocumentText;
						webBrowser.Visible = true;
						break;
					case "ThreadList":
						threadListView.Items.Clear();
						foreach (var thread in viewModel.ThreadList.Where(x => (x.ResCount < x.MaxResCount)).Select((v, i) => new { Index = i, Value = v }))
						{
							ListViewItem item = new ListViewItem((thread.Index + 1).ToString());
							item.SubItems.Add(thread.Value.ThreadTitle);
							item.SubItems.Add(thread.Value.ResCount.ToString());
							item.SubItems.Add(thread.Value.ThreadSpeed.ToString("0.0"));
							item.SubItems.Add(thread.Value.ThreadSince.ToString("0.0"));
							item.Tag = thread.Value.ThreadNo;

							threadListView.Items.Add(item);
						}
						break;
				}
			};

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

			// 自動更新
			autoUpdateTimer.Tick += (sender, e) => viewModel.UpdateThread();

			// スレッド一覧表示ボタン押下
			toolStripButtonThreadList.MouseDown += (sender, e) =>
			{
				toolStripButtonThreadList.Checked = !toolStripButtonThreadList.Checked;
				splitContainerThreadList.Panel1Collapsed = !toolStripButtonThreadList.Checked;
			};

			// 書き込み欄表示ボタン押下
			toolStripButtonWriteField.MouseDown += (sender, e) =>
			{
				toolStripButtonWriteField.Checked = !toolStripButtonWriteField.Checked;
				splitContainerWriteField.Panel2Collapsed = !toolStripButtonWriteField.Checked;

				if (toolStripButtonBottom.Checked)
				{
					ScrollToBottom();
				}
			};

			splitContainerThreadList.Panel1Collapsed = true;
			splitContainerWriteField.Panel2Collapsed = true;

			toolStrip.CanOverflow = true;

			// スレッド一覧更新
			viewModel.UpdateThreadList();

			// 更新ボタン押下
			toolStripButtonUpdate.Click += (sender, e) => viewModel.UpdateThread();

			// スクロールボタン押下
			toolStripButtonTop.Click += (sender, e) => ScrollToTop();
			toolStripButtonBottom.Click += (sender, e) => ScrollToBottom();

			// URL欄キー押下
			textBoxUrl.KeyDown += (sender, e) =>
			{
				if (e.KeyCode == Keys.Enter)
				{
					viewModel.ChangeUrl(textBoxUrl.Text);
				}
			};

			// スレッド選択変更
			threadListView.SelectedIndexChanged += (sender, e) =>
			{
				if (threadListView.SelectedItems.Count <= 0)
				{
					return;
				}
				viewModel.ChangeThread(threadListView.Items[threadListView.SelectedItems[0].Index].Tag as string);
			};

			// URLの設定
			textBoxUrl.Text = viewModel.ThreadUrl;

			// 書き込みボタン押下
			buttonWrite.Click += (sender, e) => viewModel.WriteRes(textBoxName.Text, textBoxMail.Text, textBoxMessage.Text);
			// レス書き込み
			textBoxMessage.KeyDown += (sender, e) =>
			{
				if (((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Enter)) ||
					((e.Modifiers == Keys.Shift) && (e.KeyCode == Keys.Enter)))
				{
					e.SuppressKeyPress = true;
					viewModel.WriteRes(textBoxName.Text, textBoxMail.Text, textBoxMessage.Text);
				}
			};

			webBrowser.DocumentCompleted += (sender, e) =>
			{
				if (toolStripButtonBottom.Checked)
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
					// スクロール位置が最上位か
					bool isTop = (webBrowser.Document.Body.ScrollRectangle.Top == 0);
					toolStripButtonTop.Checked = isTop;

					// スクロールイ位置が最下位か
					bool isBottom = (webBrowser.Document.Body.ScrollRectangle.Height - webBrowser.Document.Body.ScrollRectangle.Top <= webBrowser.Height + 4);
					toolStripButtonBottom.Checked = isBottom;

					// スクロール位置を保存
					ScrollPos.X = webBrowser.Document.Body.ScrollRectangle.X;
					ScrollPos.Y = webBrowser.Document.Body.ScrollRectangle.Y;
				};
			};
		}

		/// <summary>
		/// 最上位へスクロール
		/// </summary>
		private void ScrollToTop()
		{
			webBrowser.Document.Window.ScrollTo(0, 0);
		}

		/// <summary>
		/// 最下位へスクロール
		/// </summary>
		private void ScrollToBottom()
		{
			webBrowser.Document.Window.ScrollTo(0, webBrowser.Document.Body.ScrollRectangle.Bottom);
		}
	}
}
