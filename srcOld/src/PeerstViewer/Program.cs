using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

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
			Application.Run(new ThreadViewer());
		}
	}
}
