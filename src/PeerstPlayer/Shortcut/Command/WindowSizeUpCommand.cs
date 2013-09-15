using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// ウィンドウサイズUPコマンド
	/// </summary>
	class WindowSizeUpCommand : IShortcutCommand
	{
		private Form form;
		private PecaPlayerControl pecaPlayer;

		public WindowSizeUpCommand(Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;
		}

		public void Execute(CommandArgs commandArgs)
		{
			int height = (int)(pecaPlayer.ImageHeight * 0.1);
			form.Width += (int)(height * pecaPlayer.AspectRate);
			form.Height += height;
		}

		public string Detail
		{
			get { return "ウィンドウサイズを大きく"; }
		}
	}
}
