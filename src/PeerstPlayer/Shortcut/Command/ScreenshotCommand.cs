using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using PeerstLib.PeerCast.Data;
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
				pecaPlayer.Invoke((MethodInvoker)(() =>
				{
					// // SSのディレクトリが存在していなければ作る
					if (!Directory.Exists(PlayerSettings.ScreenshotFolder))
					{
						Directory.CreateDirectory(PlayerSettings.ScreenshotFolder);
					}
					var fileName = Format(PlayerSettings.ScreenshotFormat, pecaPlayer.ChannelInfo);
					// 書式にディレクトリが含まれているかもしれないので確認しておく
					if (!Directory.Exists(PlayerSettings.ScreenshotFolder + Path.GetDirectoryName(fileName)))
					{
						Directory.CreateDirectory(PlayerSettings.ScreenshotFolder + Path.GetDirectoryName(fileName));
					}

					// SS撮影
					using (var image = new Bitmap(pecaPlayer.Width, pecaPlayer.Height, PixelFormat.Format24bppRgb))
					using (var graphics = Graphics.FromImage(image))
					{
						var location = pecaPlayer.Location;
						var screenPoint = pecaPlayer.PointToScreen(location);
						graphics.CopyFromScreen(screenPoint.X, screenPoint.Y, 0, 0, image.Size);

						var format = GetImageFormat(PlayerSettings.ScreenshotExtension);
						var name = string.Format("{0}/{1}.{2}",
							PlayerSettings.ScreenshotFolder, fileName, format.ToString().ToLower());
						try
						{
							image.Save(name, format);
						}
						catch (ExternalException)
						{
							MessageBox.Show(pecaPlayer, "スクリーンショットの保存に失敗しました。\n" +
								"保存フォルダが間違っている、既にファイルが開かれている、\n" +
								"ドライブの空き容量が無い可能性があります。",
								"ERROR!",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}));
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

		private ImageFormat GetImageFormat(string extension)
		{
			switch (extension)
			{
				case "bmp":
					return ImageFormat.Bmp;
				case "jpg":
				case "jpeg":
					return ImageFormat.Jpeg;
				case "gif":
					return ImageFormat.Gif;
				case "png":
					return ImageFormat.Png;
				default:
					return ImageFormat.Png;
			}
		}

		public static string Format(string format, ChannelInfo info)
		{
			// 書式が空の場合、設定デフォルト値を使いたい
			if (string.IsNullOrEmpty(format))
			{
				format = "yyyyMMdd_HHmmss";
			}
			// 時刻
			var buffer = DateTime.Now.ToString(format);
			// チャンネル名
			buffer = buffer.Replace("$0", info != null ? info.Name : "チャンネル名");
			// ファイル名として使えない文字を消す
			buffer = buffer.Replace("\"", "");
			buffer = buffer.Replace(":", "");
			buffer = buffer.Replace("*", "");
			buffer = buffer.Replace("?", "");
			buffer = buffer.Replace("<", "");
			buffer = buffer.Replace(">", "");
			buffer = buffer.Replace("|", "");
			buffer = buffer.Replace(";", "");
			buffer = buffer.Replace(",", "");
			return buffer;
		}
	}
}
