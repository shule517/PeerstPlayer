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
		SelectThreadObserver selectThreadObserver = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="operationBbs">MainFormのOperationBbs</param>
		public ThreadSelectForm(SelectThreadObserver selectThreadObserver)
		{
			InitializeComponent();
			this.selectThreadObserver = selectThreadObserver;
		}

		/// <summary>
		/// スレッド一覧更新
		/// </summary>
		/// <param name="threadUrl"></param>
		public void Update(string threadUrl)
		{
			textBoxThreadUrl.Text = threadUrl;
			UpdateThreadList(threadUrl);
		}

		/// <summary>
		/// 更新ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			UpdateThreadList(textBoxThreadUrl.Text);
		}

		/// <summary>
		/// スレッド一覧を更新
		/// </summary>
		/// <param name="threadUrl">スレッドURL</param>
		private void UpdateThreadList(string threadUrl)
		{
			operationBbs.ChangeUrl(threadUrl);
			List<ThreadInfo> threadList = operationBbs.GetThreadList();

			listViewThread.Items.Clear();
			int no = 1;
			foreach (ThreadInfo info in threadList)
			{
				string[] items = { no.ToString(), info.Title, info.ResCount, ((int)info.ThreadSpeed).ToString() };
				ListViewItem item = new ListViewItem(items);
				item.Tag = info.ThreadNo;
				listViewThread.Items.Add(item);

				// 指定スレッドを選択
				if (info.ThreadNo == operationBbs.GetThreadNo())
				{
					listViewThread.Items[listViewThread.Items.Count - 1].BackColor = Color.Orange;
					listViewThread.Select();
					listViewThread.EnsureVisible(listViewThread.Items.Count - 1);
				}
				no++;
			}

			// 幅を自動調整
			listViewThread.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}

		/// <summary>
		/// ダブルクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewThread_DoubleClick(object sender, EventArgs e)
		{
			// 未選択チェック
			if (listViewThread.SelectedItems.Count == 0)
			{
				return;
			}

			// MainFormのスレッド変更
			string selectThreadNo = listViewThread.SelectedItems[0].Tag.ToString();

			// スレッドURL更新通知
			selectThreadObserver.UpdateThreadUrl(operationBbs.GetUrl(), selectThreadNo);

			// 非表示
			Visible = false;
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
	}
}
