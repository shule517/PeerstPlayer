using System;
using System.Diagnostics;
using System.IO;
using PeerstLib.Util;
using PeerstPlayer.Controls.StatusBar;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// PeerstViewerを開くコマンド
	/// </summary>
	class OpenPeerstViewerCommand : IShortcutCommand
	{
		private StatusBarControl statusBar;

		public OpenPeerstViewerCommand(StatusBarControl statusBar)
		{
			this.statusBar = statusBar;
		}

		public void Execute()
		{
			if (Environment.GetCommandLineArgs().Length <= 0)
			{
				Logger.Instance.Error("PeerstViewer起動失敗 [コマンドラインが空]");
				return;
			}

			// exeの親フォルダパス取得
			string exePath = Environment.GetCommandLineArgs()[0];
			DirectoryInfo dirInfo = Directory.GetParent(exePath);

			// スレッド選択しているスレッドURLを開く
			string viewerExePath = Path.Combine(dirInfo.FullName, "PeerstViewer.exe");
			string param = statusBar.SelectThreadUrl;
			Logger.Instance.InfoFormat("PeerstViewer起動 [viewerExePath:{0} param:{1}]", viewerExePath, param);
			Process.Start(viewerExePath, param);
		}

		public string Detail
		{
			get { return "PeerstViewerを開く"; }
		}
	}
}
