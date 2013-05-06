using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer.Control
{
	// ステータスバーコントロール
	public partial class StatusBar : UserControl
	{
		// チャンネル詳細
		public string ChannelDetail {
			get { return ChannelDetailLabel.Text; }
			set { ChannelDetailLabel.Text = value; }
		}

		// 選択スレッドのクリックイベント
		public event EventHandler SelectThreadClick;

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：イベント登録
		//-------------------------------------------------------------
		public StatusBar()
		{
			InitializeComponent();

			// サイズ変更イベント登録
			writeField.SizeChanged += writeField_SizeChanged;
			writeField.SelectThreadClick += (sender, e) =>
			{
				if (SelectThreadClick != null) SelectThreadClick(sender, e);
			};
		}

		#region 非公開プロパティ

		//-------------------------------------------------------------
		// 概要：サイズ変更イベント
		// 詳細：書き込み欄のサイズ自動調節
		//-------------------------------------------------------------
		private void writeField_SizeChanged(object sender, EventArgs e)
		{
			Height = writeField.Height + ChannelDetailLabel.Height;
			ChannelDetailLabel.Top = writeField.Height;
		}

		#endregion
	}
}
