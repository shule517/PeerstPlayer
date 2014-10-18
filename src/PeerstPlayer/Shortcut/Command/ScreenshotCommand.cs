using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using PeerstPlayer.Controls.PecaPlayer;
using PeerstPlayer.Forms.Player;

namespace PeerstPlayer.Shortcut.Command
{
	class ScreenshotCommand : IShortcutCommand
	{
		private readonly PecaPlayerControl pecaPlayer;
		private readonly BackgroundWorker worker = new BackgroundWorker();

		public ScreenshotCommand(PecaPlayerControl pecaPlayer)
		{
			this.pecaPlayer = pecaPlayer;

			worker.DoWork += (sender, args) =>
			{
				// コンテキストメニューから呼ばれた時用に少し待機する
				Thread.Sleep(50);

				// // SSのディレクトリが存在していなければ作る
				if (!Directory.Exists(PlayerSettings.ScreenshotFolder))
				{
					Directory.CreateDirectory(PlayerSettings.ScreenshotFolder);
				}

				// SS撮影
				using (var image = new Bitmap(pecaPlayer.Width, pecaPlayer.Height, PixelFormat.Format24bppRgb))
				using (var graphics = Graphics.FromImage(image))
				{
					var location = pecaPlayer.Location;
					var screenPoint = pecaPlayer.PointToScreen(location);
					graphics.CopyFromScreen(screenPoint.X, screenPoint.Y, 0, 0, image.Size);
					var name = string.Format("{0}/{1}.png",
						PlayerSettings.ScreenshotFolder, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
					image.Save(name);
				}
			};
		}

		public void Execute(CommandArgs commandArgs)
		{
			worker.RunWorkerAsync();
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "スクリーンショット";
		}
	}
}
