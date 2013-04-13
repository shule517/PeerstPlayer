using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
			// アプリーケーション設定
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//// Formの表示
			Application.Run(new PlayerView());
		}
	}
}
