using Shule.Peerst.BBS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer
{
	public partial class ThreadSelectForm : Form
	{
		// スレッド操作
		OperationBbs operationBbs = new OperationBbs();

		// スレッド選択監視
		ThreadSelectObserver selectThreadObserver = null;

		// スレッド一覧更新用Worker
		BackgroundWorker updateThreadWorker = new BackgroundWorker();

		// TODO スレッドストップスレの表示/非表示を切替(チェックボックス)
		// TODO カラムクリックでソートを行う

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="operationBbs">MainFormのOperationBbs</param>
		public ThreadSelectForm(ThreadSelectObserver selectThreadObserver)
		{
			InitializeComponent();
			this.selectThreadObserver = selectThreadObserver;
			updateThreadWorker.DoWork += updateThreadWorker_DoWork;
			updateThreadWorker.RunWorkerCompleted += updateThreadWorker_RunWorkerCompleted;
		}

		/// <summary>
		/// スレッド一覧更新
		/// </summary>
		/// <param name="threadUrl">スレッドURL</param>
		public void Update(string threadUrl)
		{
			threadListView.Items.Clear();
			textBoxThreadUrl.Text = threadUrl;
			updateThreadWorker.RunWorkerAsync(threadUrl);
		}

		#region スレッド一覧更新Worker

		/// <summary>
		/// スレッド一覧更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">スレッドURL</param>
		void updateThreadWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			operationBbs.ChangeUrl((string)e.Argument);
		}

		/// <summary>
		/// スレッド一覧更新完了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void updateThreadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			DrawListView();
		}

		/// <summary>
		/// スレッド一覧の描画
		/// </summary>
		private void DrawListView()
		{
			threadListView.Items.Clear();
			threadListView.BeginUpdate();
			int no = 1;
			foreach (ThreadInfo info in operationBbs.ThreadList)
			{
				string[] items = { no.ToString(), info.ThreadTitle, info.ResCount.ToString(), ((int)info.ThreadSpeed).ToString() };
				ListViewItem item = new ListViewItem(items);
				item.Tag = info.ThreadNo;
				threadListView.Items.Add(item);

				// 指定スレッドを選択
				if (info.ThreadNo == operationBbs.BbsInfo.ThreadNo)
				{
					threadListView.Items[threadListView.Items.Count - 1].BackColor = Color.Orange;
					threadListView.Select();
					threadListView.EnsureVisible(threadListView.Items.Count - 1);
				}
				no++;
			}

			// 幅を自動調整
			threadListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			threadListView.EndUpdate();
		}

		#endregion

		#region GUIイベント

		/// <summary>
		/// 更新ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			// スレッド一覧更新
			updateThreadWorker.RunWorkerAsync(textBoxThreadUrl.Text);
		}

		/// <summary>
		/// スレッドURLテキストボックス：キー押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxThreadUrl_KeyDown(object sender, KeyEventArgs e)
		{
			// エンター押下
			if (e.KeyCode == Keys.Enter)
			{
				// チャンネル更新
				updateThreadWorker.RunWorkerAsync(textBoxThreadUrl.Text);
			}
		}

		/// <summary>
		/// スレッドURLテキストボックス：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxThreadUrl_Click(object sender, EventArgs e)
		{
			// 全選択する
			textBoxThreadUrl.SelectAll();
		}

		/// <summary>
		/// スレッド一覧：ダブルクリックイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
				// MainFormのスレッド変更
				string selectThreadNo = threadListView.SelectedItems[0].Tag.ToString();

				// スレッドURL更新通知
				selectThreadObserver.UpdateThreadUrl(operationBbs.ThreadUrl, selectThreadNo);

				// 非表示
				Visible = false;
			}
		}

		/// <summary>
		/// フォームを終了する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ThreadSelectForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// 終了せずに、非表示とする
			Visible = false;
			e.Cancel = true;
		}

		#endregion
	}
}
