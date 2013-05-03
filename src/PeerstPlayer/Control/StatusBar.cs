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
		//--------------------------------------------
		// 公開プロパティ
		//--------------------------------------------

		// チャンネル詳細
		public string ChannelDetail {
			get { return ChannelDetailLabel.Text; }
			set { ChannelDetailLabel.Text = value; }
		}

		public StatusBar()
		{
			InitializeComponent();

			// サイズ変更イベント登録
			writeField.SizeChanged += writeField_SizeChanged;
		}

		//--------------------------------------------
		// 非公開プロパティ
		//--------------------------------------------

		// 書き込み欄のサイズ自動調節
		private void writeField_SizeChanged(object sender, EventArgs e)
		{
			Height = writeField.Height + ChannelDetailLabel.Height;
			ChannelDetailLabel.Top = writeField.Height;
		}
	}
}
