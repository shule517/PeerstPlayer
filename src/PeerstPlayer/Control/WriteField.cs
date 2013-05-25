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
	// 書き込み欄コントロール
	public partial class WriteField : UserControl
	{
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

				// スレッドタイトルの更新
				UpdateThreadTitle();
			}
		}

		// 高さ変更
		public event EventHandler HeightChanged = delegate { };

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
			selectThreadLabel.Click += (sender, e) =>
			{
				// 選択スレッド画面を開く
				threadSelectView.Open();
			};
			// スレッド一覧情報更新イベント
			threadSelectView.ThreadListChange += (sender, e) =>
			{
				operationBbs.ChangeUrl(threadSelectView.ThreadUrl);
				UpdateThreadTitle();
			};
			// スレッド変更イベント
			threadSelectView.ThreadChange += (sender, e) =>
			{
				operationBbs.ChangeUrl(threadSelectView.ThreadUrl);
				UpdateThreadTitle();
			};
		}

		// スレッドタイトルの更新
		private void UpdateThreadTitle()
		{
			if (operationBbs.Enabled)
			{
				// スレッドタイトル表示
				selectThreadLabel.Text = operationBbs.ThreadSelected
					? string.Format("スレッド[ {0} ] ({1})", operationBbs.SelectThread.ThreadTitle, operationBbs.SelectThread.ResCount)
					: string.Format("掲示板[ {0} ] スレッドを選択してください", operationBbs.BbsInfo.BbsName);
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
