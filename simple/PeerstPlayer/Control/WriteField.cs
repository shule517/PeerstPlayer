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
	public partial class WriteField : UserControl
	{
		public WriteField()
		{
			InitializeComponent();
		}

		private void writeFieldTextBox_TextChanged(object sender, EventArgs e)
		{
			writeFieldTextBox.Height = writeFieldTextBox.PreferredSize.Height;
			Height = writeFieldTextBox.PreferredSize.Height;
		}
	}
}
