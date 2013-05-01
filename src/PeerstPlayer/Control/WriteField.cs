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
	// 書き込み欄コントロール
	public partial class WriteField : UserControl
	{
		public WriteField()
		{
			InitializeComponent();
		}

		// 文字入力イベント
		private void writeFieldTextBox_TextChanged(object sender, EventArgs e)
		{
			writeFieldTextBox.Height = writeFieldTextBox.PreferredSize.Height;
			Height = selectThreadLabel.Height + writeFieldTextBox.PreferredSize.Height;
		}
	}
}
