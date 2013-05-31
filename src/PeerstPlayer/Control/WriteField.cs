using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeerstPlayer.View;
using PeerstLib.Bbs;

namespace PeerstPlayer.Control
{
	//-------------------------------------------------------------
	// 概要：書き込み欄コントロール
	//-------------------------------------------------------------
	public partial class WriteField : UserControl
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// 選択スレッドURL
		public string SelectThreadUrl
		{
			get { return threadSelectView.ThreadUrl; }
			set
			{
				// TODO 別メソッド移動
				// TODO ChangeUrlをBackGroundで実行させる
				threadSelectView.ThreadUrl = value;
				operationBbs.ChangeUrl(value);
			}
		}

		// 高さ変更
		public event EventHandler HeightChanged = delegate { };

		// 右クリックイベント
		public event EventHandler RightClick = delegate { };

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// 掲示板操作クラス
		private OperationBbs operationBbs = new OperationBbs();

		// スレッド選択画面
		private ThreadSelectView threadSelectView = new ThreadSelectView();

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：イベントの設定
		//-------------------------------------------------------------
		public WriteField()
		{
			InitializeComponent();

			// 選択スレッドのクリックイベント
			selectThreadLabel.MouseDown += (sender, e) =>
			{
				if (e.Button == MouseButtons.Left)
				{
					// 選択スレッド画面を開く
					threadSelectView.Open();
				}
				else if (e.Button == MouseButtons.Right)
				{
					RightClick(sender, e);
				}
			};
			// スレッド一覧情報更新イベント
			threadSelectView.ThreadListChange += (sender, e) => operationBbs.ChangeUrl(threadSelectView.ThreadUrl);
			// スレッド変更イベント
			threadSelectView.ThreadChange += (sender, e) => operationBbs.ChangeUrl(threadSelectView.ThreadUrl);
			// スレッドタイトルの更新
			operationBbs.ThreadListChange += (sender, e) => UpdateThreadTitle();
		}

		//-------------------------------------------------------------
		// 概要：スレッドタイトルの更新
		//-------------------------------------------------------------
		private void UpdateThreadTitle()
		{
			if (operationBbs.Enabled)
			{
				// スレッドタイトル表示
				EditThreadTitle();
			}
			else
			{
				// スレッドタイトル表示
				if (string.IsNullOrEmpty(operationBbs.ThreadUrl))
				{
					selectThreadLabel.Text = "URLが指定されていません";
				}
				else
				{
					selectThreadLabel.Text = "未対応URLです";
				}
			}

			// 書き込み欄の有効/無効
			writeFieldTextBox.Enabled = operationBbs.ThreadSelected;
		}

		//-------------------------------------------------------------
		// 概要：スレッドタイトルの編集
		//-------------------------------------------------------------
		private void EditThreadTitle()
		{
			if (operationBbs.ThreadSelected)
			{
				try
				{
					selectThreadLabel.Text = string.Format("スレッド[ {0} ] ({1})", operationBbs.SelectThread.ThreadTitle, operationBbs.SelectThread.ResCount);
				}
				catch
				{
					selectThreadLabel.Text = string.Format("掲示板[ {0} ] 指定されたスレッドが存在しません", operationBbs.BbsInfo.BbsName);
				}
			}
			else
			{
				selectThreadLabel.Text = string.Format("掲示板[ {0} ] スレッドを選択してください", operationBbs.BbsInfo.BbsName);
			}
		}

		//-------------------------------------------------------------
		// 概要：文字入力イベント
		// 詳細：コントロールの高さ調節
		//-------------------------------------------------------------
		private void writeFieldTextBox_TextChanged(object sender, EventArgs e)
		{
			writeFieldTextBox.Height = writeFieldTextBox.PreferredSize.Height;
			Height = selectThreadLabel.Height + writeFieldTextBox.PreferredSize.Height;

			// 高さの変更
			HeightChanged(sender, e);
		}

		//-------------------------------------------------------------
		// 概要：書き込み欄のキー押下
		// 詳細：レス書き込み
		//-------------------------------------------------------------
		private void writeFieldTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Enter))
			{
				// TODO レス書き込み
				operationBbs.Write("", "sage", writeFieldTextBox.Text);
				writeFieldTextBox.Text = "";
			}
		}
	}
}
