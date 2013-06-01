using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using log4net;

namespace PeerstPlayer
{
	static class Program
	{
		// log4netの初期化
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			PlayerView playerView = new PlayerView();
			
			// TODO デバッグ開始

			// 動画再生
			if (Environment.GetCommandLineArgs().Count() > 1)
			{
				string url = Environment.GetCommandLineArgs()[1];
				playerView.Open(url);
			}

			// TODO デバッグ終了

			Application.Run(playerView);
		}
	}
}
