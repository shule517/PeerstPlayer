using log4net;
using PeerstLib.Util;
using PeerstViewer.ThreadViewer;
using System;
using System.Reflection;
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

			Logger.Instance.Info("END:PeerstViewer");
		}
	}
}
