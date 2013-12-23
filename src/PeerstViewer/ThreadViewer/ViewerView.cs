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
		private ThreadViewerPresenter presenter;

		private Point ScrollPos = new Point();

		public ThreadViewerView()
		{
			InitializeComponent();

			presenter = new ThreadViewerPresenter(webBrowser, threadListView, toolStripButtonWriteField, splitContainerWriteField, toolStripButtonBottom, toolStripButtonThreadList, splitContainerThreadList, toolStrip);

			//---------------------------------------------------
			// データバインド設定
			//---------------------------------------------------

			// TODO データバインドするとブラウザにフォーカスを当てた時に更新が走ってしまう
			//webBrowser.DataBindings.Add("DocumentText", viewModel, "DocumentText");
			textBoxUrl.DataBindings.Add("Text", viewModel, "ThreadUrl");
			textBoxMessage.DataBindings.Add("Text", viewModel, "Message");

			presenter.Init();

			//---------------------------------------------------
			// ボタン押下
			//---------------------------------------------------

			// 更新ボタン押下
			toolStripButtonUpdate.Click += (sender, e) => viewModel.UpdateThread();

			// スクロールボタン押下
			toolStripButtonTop.Click += (sender, e) => presenter.ScrollToTop();
			toolStripButtonBottom.Click += (sender, e) => presenter.ScrollToBottom();

			// スレッド一覧表示ボタン押下
			toolStripButtonThreadList.MouseDown += (sender, e) => presenter.ToggleThreadList();
	
			// 書き込み欄表示ボタン押下
			toolStripButtonWriteField.MouseDown += (sender, e) => presenter.ToggleWriteField();

			// 書き込みボタン押下
			buttonWrite.Click += (sender, e) => viewModel.WriteRes(textBoxName.Text, textBoxMail.Text, textBoxMessage.Text);

			//---------------------------------------------------
			// イベント登録
			//---------------------------------------------------

			viewModel.PropertyChanged += (sender, e) => PropertyChanged(e);

			// 自動更新
			autoUpdateTimer.Tick += (sender, e) => viewModel.UpdateThread();

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

			// 起動時に最下位にスクロールする
			toolStripButtonBottom.Checked = true;
			webBrowser.DocumentCompleted += (sender, e) =>
			{
				if (toolStripButtonBottom.Checked)
				{
					// スレッドの最下位へ移動
					presenter.ScrollToBottom();
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

			// URLの設定
			textBoxUrl.Text = viewModel.ThreadUrl;

			// スレッド一覧更新
			viewModel.UpdateThreadList();
		}


		private void PropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
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
		}
	}
}
