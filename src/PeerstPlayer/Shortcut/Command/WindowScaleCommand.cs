using System.Drawing;
using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// ウィンドウ拡大率指定コマンド
	/// </summary>
	class WindowScaleCommand : IShortcutCommand
	{
		private Form form;
		private PecaPlayerControl pecaPlayer;

		public WindowScaleCommand(Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;
		}

		public void Execute(CommandArgs commandArgs)
		{
			WindowScaleCommandArgs args = (WindowScaleCommandArgs)commandArgs;
			int width = (int)(pecaPlayer.ImageWidth * args.Scale);
			int height = (int)(pecaPlayer.ImageHeight * args.Scale);
			form.Size = new Size(width, height);
		}

		public string Detail
		{
			get { return string.Format("ウィンドウサイズ変更({0}x{1})", pecaPlayer.ImageWidth, pecaPlayer.ImageHeight); }
		}
	}
}
