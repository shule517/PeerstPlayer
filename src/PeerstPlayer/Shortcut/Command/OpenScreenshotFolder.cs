using System.Diagnostics;
using System.IO;
using PeerstPlayer.Forms.Player;

namespace PeerstPlayer.Shortcut.Command
{
	class OpenScreenshotFolderCommand : IShortcutCommand
	{
		public OpenScreenshotFolderCommand()
		{
		}

		public void Execute(CommandArgs commandArgs)
		{
			// SSのディレクトリが存在していなければ作る
			if (!Directory.Exists(PlayerSettings.ScreenshotFolder))
			{
				Directory.CreateDirectory(PlayerSettings.ScreenshotFolder);
			}
			Process.Start(PlayerSettings.ScreenshotFolder);
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "スクリーンショットフォルダを開く";
		}
	}
}
