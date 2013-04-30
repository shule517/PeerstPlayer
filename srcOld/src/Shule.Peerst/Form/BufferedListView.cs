using System;
using System.Collections.Generic;
using System.Text;

namespace Shule.Peerst.Form
{
	public class BufferedListView : System.Windows.Forms.ListView
	{
		protected override bool DoubleBuffered { get { return true; } set { } }
	}
}
