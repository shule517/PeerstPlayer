using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PeerstLib.Control
{
	//-------------------------------------------------------------
	// 概要：ListViewの拡張クラス
	// 概要：描画のチラつきを軽減
	//-------------------------------------------------------------
	public class BufferedListView : ListView
	{
		// ダブルバッファ
		protected override bool DoubleBuffered { get { return true; } set { } }
	}
}
