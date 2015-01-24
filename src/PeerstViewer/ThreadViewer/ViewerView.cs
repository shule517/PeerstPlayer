using PeerstLib.Util;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PeerstLib.Controls;
using PeerstViewer.Settings;

namespace PeerstViewer.ThreadViewer
{
	/// <summary>
	/// スレッドビューワ画面
	/// </summary>
	public partial class ThreadViewerView : Form
	{
		private ThradViewerViewModel viewModel = new ThradViewerViewModel();

		public ThreadViewerView()
		{
			Logger.Instance.Debug("ThreadViewerView[]");
			InitializeComponent();

			Init();

			// 設定の読み込み
			LoadSetting();

			//---------------------------------------------------
			// データバインド設定
			//---------------------------------------------------

			textBoxUrl.DataBindings.Add("Text", viewModel, "ThreadUrl");
			textBoxMessage.DataBindings.Add("Text", viewModel, "Message");

			//---------------------------------------------------
			// ボタン押下
			//---------------------------------------------------

			// 更新ボタン押下
			toolStripButtonUpdate.Click += (sender, e) => viewModel.UpdateThread();

			// スクロールボタン押下
			toolStripButtonTop.Click += (sender, e) => threadViewer.ScrollToTop();
			toolStripButtonBottom.Click += (sender, e) => threadViewer.ScrollToBottom();

			// スレッド一覧表示ボタン押下
			toolStripButtonThreadList.MouseDown += (sender, e) => ToggleThreadList();

			// 書き込み欄表示ボタン押下
			toolStripButtonWriteField.MouseDown += (sender, e) => ToggleWriteField();

			// 設定ボタン押下
			toolStripButtonSetting.MouseDown += (sender, e) =>
			{
				var view = new ViewerSettingView();
				view.TopMost = TopMost;
				view.ShowDialog();
			};

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
			threadViewer.DocumentCompleted += (sender, e) =>
			{
				threadViewer.DocumentScroll += (sender_, e_) =>
				{
					// スクロール位置が最上位か
					toolStripButtonTop.Checked = threadViewer.IsScrollTop;

					// スクロールイ位置が最下位か
					toolStripButtonBottom.Checked = threadViewer.IsScrollBottom;
				};
			};

			// 初期化
			Load += (sender, args) =>
			{
				// 起動時にウィンドウサイズを復帰する
				if (ViewerSettings.ReturnSizeOnStart)
				{
					Width = ViewerSettings.ReturnSize.Width;
					Height = ViewerSettings.ReturnSize.Height;
				}

				// ここでやらないとウィンドウ位置が復帰できない?
				// 起動時にウィンドウ位置を復帰する
				if (ViewerSettings.ReturnPositionOnStart)
				{
					Location = ViewerSettings.ReturnPosition;
				}
			};

			// 終了処理
			FormClosed += (sender, e) =>
			{
				WINDOWPLACEMENT placement;
				Win32API.GetWindowPlacement(Handle, out placement);

				// 終了時の位置とサイズを保存
				if (ViewerSettings.SaveReturnPositionOnClose)
				{
					ViewerSettings.ReturnPosition = new Point(placement.normalPosition.left,
						placement.normalPosition.top);
				}
				if (ViewerSettings.SaveReturnSizeOnClose)
				{
					ViewerSettings.ReturnSize = new Size(
						placement.normalPosition.right - placement.normalPosition.left,
						placement.normalPosition.bottom - placement.normalPosition.top);

				}
				ViewerSettings.Save();
			};

			// URLの設定
			textBoxUrl.Text = viewModel.ThreadUrl;

			// スレッド一覧更新
			viewModel.UpdateThreadList();
		}

		/// <summary>
		/// フォームの初期化
		/// </summary>
		private void Init()
		{
			Logger.Instance.Debug("Init[]");
			splitContainerThreadList.Panel1Collapsed = true;
			splitContainerWriteField.Panel2Collapsed = true;
			toolStrip.CanOverflow = true;
		}

		/// <summary>
		/// 設定の読み込み
		/// </summary>
		private void LoadSetting()
		{
			// 設定の読み込み
			ViewerSettings.Load();
		}

		/// <summary>
		/// 書き込み欄表示切り替え
		/// </summary>
		private void ToggleWriteField()
		{
			Logger.Instance.Debug("ToggleWriteField[]");
			toolStripButtonWriteField.Checked = !toolStripButtonWriteField.Checked;
			splitContainerWriteField.Panel2Collapsed = !toolStripButtonWriteField.Checked;

			if (toolStripButtonBottom.Checked)
			{
				threadViewer.ScrollToBottom();
			}
		}

		/// <summary>
		/// スレッド一覧表示切り替え
		/// </summary>
		private void ToggleThreadList()
		{
			Logger.Instance.Debug("ToggleThreadList[]");
			toolStripButtonThreadList.Checked = !toolStripButtonThreadList.Checked;
			splitContainerThreadList.Panel1Collapsed = !toolStripButtonThreadList.Checked;
		}

		/// <summary>
		/// データ変更イベント
		/// </summary>
		private void PropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
		{
			Logger.Instance.DebugFormat("PropertyChanged[PropertyName:{0}]", e.PropertyName);

			switch (e.PropertyName)
			{
				case "DocumentText":
					threadViewer.DocumentText = viewModel.DocumentText;
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
