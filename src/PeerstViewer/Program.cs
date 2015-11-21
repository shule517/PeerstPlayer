using log4net;
using PeerstLib.Util;
using PeerstViewer.ThreadViewer;
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace PeerstViewer
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
			Logger.Instance.Info("START:PeerstViewer");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ThreadViewerView());

			// メインスレッドの未処理例外
			Application.ThreadException += (sender, e) => Logger.Instance.Fatal(e.Exception);

			// メインスレッド以外の未処理例外
			Thread.GetDomain().UnhandledException += (sender, e) => Logger.Instance.Fatal(e.ExceptionObject as Exception);

			Logger.Instance.Info("END:PeerstViewer");
		}
	}
}
