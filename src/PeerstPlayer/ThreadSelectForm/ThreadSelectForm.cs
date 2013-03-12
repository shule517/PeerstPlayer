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
			List<ThreadInfo> threadList = operationBbs.ThreadList;

			listViewThread.Items.Clear();
			int no = 1;
			foreach (ThreadInfo info in threadList)
			{
				string[] items = { no.ToString(), info.ThreadTitle, info.ResCount, ((int)info.ThreadSpeed).ToString() };
				ListViewItem item = new ListViewItem(items);
				item.Tag = info.ThreadNo;
				listViewThread.Items.Add(item);

				// 指定スレッドを選択
				if (info.ThreadNo == operationBbs.BbsInfo.ThreadNo)
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

		/// <summary>
		/// ダブルクリックイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewThread_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			// 未選択チェック
			if (listViewThread.SelectedItems.Count == 0)
			{
				return;
			}

			// 左クリック
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				// MainFormのスレッド変更
				string selectThreadNo = listViewThread.SelectedItems[0].Tag.ToString();

				// スレッドURL更新通知
				selectThreadObserver.UpdateThreadUrl(operationBbs.ThreadUrl, selectThreadNo);

				// 非表示
				Visible = false;
			}
		}

		/// <summary>
		/// スレッドURLテキストボックスでキー押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxThreadUrl_KeyDown(object sender, KeyEventArgs e)
		{
			// エンター押下
			if (e.KeyCode == Keys.Enter)
			{
				// チャンネル更新
				UpdateThreadList(textBoxThreadUrl.Text);
			}
		}
	}
}
