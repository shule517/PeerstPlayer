using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using PeerstLib.Controls;
using PeerstLib.Util;
using PeerstPlayer.Controls.StatusBar;
using PeerstPlayer.Forms.Player;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// PeerstViewerを開くコマンド
	/// </summary>
	class OpenPeerstViewerCommand : IShortcutCommand
	{
		private StatusBarControl statusBar;
		private List<Process> processes = new List<Process>(); 

		public OpenPeerstViewerCommand(StatusBarControl statusBar)
		{
			this.statusBar = statusBar;
		}

		~OpenPeerstViewerCommand()
		{
			if (PlayerSettings.ExitedViewerClose)
			{
				foreach (var process in processes.Where(process => !process.HasExited))
				{
					process.CloseMainWindow();
				}
			}
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			string folderPath = FormUtility.GetExeFolderPath();

			// スレッド選択しているスレッドURLを開く
			string viewerExePath = Path.Combine(folderPath, "PeerstViewer.exe");
			string param = statusBar.SelectThreadUrl;
			Logger.Instance.InfoFormat("PeerstViewer起動 [viewerExePath:{0} param:{1}]", viewerExePath, param);

			try
			{
				processes.Add(Process.Start(viewerExePath, param));
			}
			catch
			{
				Logger.Instance.Error("PeerstViewer起動失敗");
			}
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "スレッドビューワを開く";
		}
	}
}
