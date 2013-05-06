using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeerstPlayer.View;

namespace PeerstPlayer.Control
{
	// 書き込み欄コントロール
	public partial class WriteField : UserControl
	{
		// 選択スレッドURL
		public string SelectThreadUrl
		{
			get { return selectThreadLabel.Text; }
			set { selectThreadLabel.Text = value; }
		}

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
				PlayerSettingView view = new PlayerSettingView();
				view.Show();
			};

		}

		#region 非公開プロパティ

		//-------------------------------------------------------------
		// 概要：文字入力イベント
		// 詳細：コントロールの高さ調節
		//-------------------------------------------------------------
		private void writeFieldTextBox_TextChanged(object sender, EventArgs e)
		{
			writeFieldTextBox.Height = writeFieldTextBox.PreferredSize.Height;
			Height = selectThreadLabel.Height + writeFieldTextBox.PreferredSize.Height;
		}

		#endregion
	}
}
