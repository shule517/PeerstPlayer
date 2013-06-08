using System;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using log4net;
using PeerstLib.Utility;

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
			Logger.Instance.Info("START:PeerstPlayer");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// プロトコル違反しているPeerCastに対応
			WebUtil.SettingDisableResponseError();

			// メインスレッドの未処理例外
			Application.ThreadException += (sender, e) => Logger.Instance.Fatal(e.Exception);

			// メインスレッド以外の未処理例外
			Thread.GetDomain().UnhandledException += (sender, e) => Logger.Instance.Fatal(e.ExceptionObject as Exception);

			PlayerView playerView = new PlayerView();
			
			string[] commandLine = Environment.GetCommandLineArgs();
			foreach (string arg in commandLine)
			{
				Logger.Instance.InfoFormat("コマンドライン引数[{0}]", arg);
			}

			// 動画再生
			if (Environment.GetCommandLineArgs().Count() > 1)
			{
				string url = commandLine[1];
				Logger.Instance.InfoFormat("動画再生[url:{0}]", url);
				playerView.Open(url);
			}

			Application.Run(playerView);
			Logger.Instance.Info("END:PeerstPlayer");
		}
	}
}
