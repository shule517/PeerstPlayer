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
		public StatusBar()
		{
			InitializeComponent();

			writeField.SizeChanged += writeField_SizeChanged;
		}

		// 書き込み欄のサイズ自動調節
		void writeField_SizeChanged(object sender, EventArgs e)
		{
			Height = writeField.Height + ChannelDetailLabel.Height;
			ChannelDetailLabel.Top = writeField.Height;
		}
	}
}
