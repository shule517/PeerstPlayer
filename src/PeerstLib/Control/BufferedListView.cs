using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PeerstLib.Control
{
	// ListViewクラス
	// チラつきを軽減
	public class BufferedListView : ListView
	{
		protected override bool DoubleBuffered { get { return true; } set { } }
	}
}
