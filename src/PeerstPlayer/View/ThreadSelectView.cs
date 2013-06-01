using System;
using System.Drawing;
using System.Windows.Forms;
using PeerstLib.Bbs;
using PeerstPlayer.ViewModel;

namespace PeerstPlayer.View
{
	//-------------------------------------------------------------
	// 概要：スレッド選択画面表示クラス
	//-------------------------------------------------------------
	public partial class ThreadSelectView : Form
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// TODO 選択URLの変更イベントを作成する
		// TODO このイベント時に選択スレッド名を表示する
		public string ThreadUrl
		{
			get { return viewModel.ThreadUrl; }
			set { urlTextBox.Text = value; }
		}

		// スレッド一覧の変更
		public event EventHandler ThreadListChange
		{
			add { viewModel.ThreadListChange += value; }
			remove { viewModel.ThreadListChange -= value; }
		}

		// スレッド変更
		public event EventHandler ThreadChange = delegate { };

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------
		private ThreadSelectViewModel viewModel = new ThreadSelectViewModel();

		//-------------------------------------------------------------
		// 概要：コンストラタ
		//-------------------------------------------------------------
		public ThreadSelectView()
		{
			InitializeComponent();

			viewModel.ThreadListChange += (sender, e) =>
			{
				// TODO 変更前URLがチラッと見えてしまう
				urlTextBox.Text = viewModel.ThreadUrl;

				// スレッド一覧の描画
				DrawThreadList();
			};
			// TODO 初期フォーカス時にURLテストトボックスを全選択する
		}

		//-------------------------------------------------------------
		// 概要：ウィンドウを開く
		//-------------------------------------------------------------
		public void Open()
		{
			Visible = true;
			Activate();
			updateButton_Click(this, new EventArgs());
		}

		//-------------------------------------------------------------
		// 概要：スレッド一覧の描画
		//-------------------------------------------------------------
		private void DrawThreadList()
		{
			threadListView.Items.Clear();

			// データ追加：開始
			threadListView.BeginUpdate();

			// スレッド一覧の行追加
			int index = 0;
			foreach (ThreadInfo info in viewModel.ThreadList)
			{
				index++;

				// ストップしたスレッドを表示するか判定
				if (!checkBox.Checked)
				{
					// ストップしたスレッドはスルー
					if (info.ResCount >= 1000)
					{
						continue;
					}
				}

				string[] items = { String.Format("{0, 4}", index), info.ThreadTitle, info.ResCount.ToString(), String.Format("{0,6:F1}", info.ThreadSpeed) };
				ListViewItem item = new ListViewItem(items);
				item.Tag = info.ThreadNo;
				if (threadListView.Items.Count % 2 == 0) { item.BackColor = Color.FromArgb(200, 235, 203); }

				threadListView.Items.Add(item);
			}

			// TODO カラムダブルクリックでソートを行う

			// データ追加：終了
			threadListView.EndUpdate();

			// 幅を自動調整
			threadListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			this.ClientSize = new Size(threadListView.PreferredSize.Width + 30, ClientSize.Height);
		}

		//-------------------------------------------------------------
		// 概要：更新ボタン押下
		// 詳細：スレッド一覧の更新を行う
		//-------------------------------------------------------------
		private void updateButton_Click(object sender, EventArgs e)
		{
			// スレッド更新
			viewModel.Update(urlTextBox.Text);
		}

		//-------------------------------------------------------------
		// 概要：スレッド表示切り替えイベント
		// 詳細：スレッド一覧の再描画を行う
		//-------------------------------------------------------------
		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			// スレッド一覧の描画
			DrawThreadList();
		}

		//-------------------------------------------------------------
		// 概要：スレッド終了前イベント
		// 詳細：ウィンドウを非表示にする
		//-------------------------------------------------------------
		private void ThreadSelectView_FormClosing(object sender, FormClosingEventArgs e)
		{
			// 終了せずに、非表示とする
			Visible = false;
			e.Cancel = true;
		}

		//-------------------------------------------------------------
		// 概要：ダブルクリックイベント
		// 詳細：スレッド変更を行う
		//-------------------------------------------------------------
		private void threadListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			// 未選択チェック
			if (threadListView.SelectedItems.Count == 0)
			{
				return;
			}

			// 左クリック
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				// スレッド変更
				string selectThreadNo = threadListView.SelectedItems[0].Tag.ToString();
				viewModel.ChangeThread(selectThreadNo);
				urlTextBox.Text = viewModel.ThreadUrl;
				ThreadChange(sender, e);

				// 非表示
				Visible = false;
			}
		}

		//-------------------------------------------------------------
		// 概要：ダブルクリックイベント
		// 詳細：スレッド変更を行う
		//-------------------------------------------------------------
		private void urlTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				// スレッド更新
				viewModel.Update(urlTextBox.Text);
			}
		}
	}
}
