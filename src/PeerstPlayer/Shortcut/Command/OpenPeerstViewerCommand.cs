using System;
using System.Diagnostics;
using System.IO;
using PeerstLib.Controls;
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
			string folderPath = FormUtility.GetExeFolderPath();

			// スレッド選択しているスレッドURLを開く
			string viewerExePath = Path.Combine(folderPath, "PeerstViewer.exe");
			string param = statusBar.SelectThreadUrl;
			Logger.Instance.InfoFormat("PeerstViewer起動 [viewerExePath:{0} param:{1}]", viewerExePath, param);

			try
			{
				Process.Start(viewerExePath, param);
			}
			catch
			{
				Logger.Instance.Error("PeerstViewer起動失敗");
			}
		}

		public string Detail
		{
			get { return "PeerstViewerを開く"; }
		}
	}
}
