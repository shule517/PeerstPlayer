using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace PeerstPlayer
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new MainForm());
		}
	}
}
