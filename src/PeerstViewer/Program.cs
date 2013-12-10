using PeerstViewer.ThreadViewer;
using System;
using System.Windows.Forms;

namespace PeerstViewer
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ThreadViewerView());
		}
	}
}
