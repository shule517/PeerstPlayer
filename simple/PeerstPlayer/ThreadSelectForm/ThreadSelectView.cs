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
	public partial class ThreadSelectView : Form
	{
		// プロパティ名
		public class Property
		{
			public const string ThreadUrl = "ThreadUrl";
		};

		// スレッドURL
		public string ThreadUrl { get { return modelView.BbsInfo.ThreadUrl; } set { textBoxThreadUrl.Text = value; modelView.Update(value); } }

		// スレッドURL変更イベント
		public event PropertyChangedEventHandler ThreadUrlChanged;

		// モデルビュー
		private ThreadSelectModelView modelView = new ThreadSelectModelView();

		// TODO スレッドストップスレの表示/非表示を切替(チェックボックス)
		// TODO カラムクリックでソートを行う

		// コンストラクタ
		public ThreadSelectView()
		{
			InitializeComponent();

			// プロパティ変更イベント
			modelView.PropertyChanged += modelView_PropertyChanged;
		}

		// プロパティ変更イベント
		private void modelView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			// スレッド一覧変更
			if (e.PropertyName == ThreadSelectModelView.Property.ThreadList)
			{
				// スレッド一覧の描画
				DrawThreadList();
			}
			else if (e.PropertyName == ThreadSelectModelView.Property.ThreadUrl)
			{
				// スレッド変更通知
				NotifyThreadUrlChanged();
			}
		}

		// スレッドURL変更通知
		private void NotifyThreadUrlChanged()
		{
			if (ThreadUrlChanged == null) { return; }
			ThreadUrlChanged(this, new PropertyChangedEventArgs(Property.ThreadUrl));
		}

		#region スレッド一覧の描画

		/// <summary>
		/// スレッド一覧の描画
		/// </summary>
		private void DrawThreadList()
		{
			threadListView.Items.Clear();

			// データ追加：開始
			threadListView.BeginUpdate();

			// スレッド一覧の行追加
			for (int i = 0; i < modelView.ThreadList.Count; i++)
			{
				// １行データを追加
				AddThreadListLine(i);
			}

			// 幅を自動調整
			threadListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

			// データ追加：終了
			threadListView.EndUpdate();
		}

		// スレッド一覧へ１行データ追加
		private void AddThreadListLine(int index)
		{
			ThreadInfo info = modelView.ThreadList[index];

			string[] items = { String.Format("{0, 4}", index + 1), info.ThreadTitle, info.ResCount.ToString(), String.Format("{0,6:F1}", info.ThreadSpeed) };
			ListViewItem item = new ListViewItem(items);
			item.Tag = info.ThreadNo;

			// スレッドストップの場合は、文字色を変更
			if (info.ResCount >= modelView.ThreadStopResNum) { item.ForeColor = Color.Gray; }
			if (index % 2 == 0) { item.BackColor = Color.FromArgb(239, 239, 255); }

			// 一覧へ追加
			threadListView.Items.Add(item);

			// 指定スレッドを選択
			if (info.ThreadNo == modelView.BbsInfo.ThreadNo)
			{
				threadListView.Items[threadListView.Items.Count - 1].BackColor = Color.Orange;
				threadListView.Select();
				threadListView.EnsureVisible(threadListView.Items.Count - 1);
			}
		}

		#endregion

		#region GUIイベント

		// 更新ボタン：マウス押下
		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			// スレッド一覧更新
			modelView.Update(textBoxThreadUrl.Text);
		}

		// スレッドURLテキストボックス：キー押下
		private void textBoxThreadUrl_KeyDown(object sender, KeyEventArgs e)
		{
			// エンター押下
			if (e.KeyCode == Keys.Enter)
			{
				// スレッド一覧更新
				modelView.Update(textBoxThreadUrl.Text);
			}
		}

		// スレッドURLテキストボックス：クリック
		private void textBoxThreadUrl_Click(object sender, EventArgs e)
		{
			// 全選択する
			textBoxThreadUrl.SelectAll();
		}

		// スレッド一覧：ダブルクリックイベント
		private void threadListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			// 未選択チェック
			if (threadListView.SelectedItems.Count == 0) { return; }

			// 左クリック
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				// スレッド変更
				string selectThreadNo = threadListView.SelectedItems[0].Tag.ToString();
				modelView.ChangeThread(selectThreadNo);

				// 非表示
				Visible = false;
			}
		}

		// フォーム：終了イベント
		private void ThreadSelectForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// 終了せずに、非表示とする
			Visible = false;
			e.Cancel = true;
		}

		#endregion
	}
}
