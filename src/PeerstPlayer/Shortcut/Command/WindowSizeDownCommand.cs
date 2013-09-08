using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// ウィンドウサイズDOWNコマンド
	/// </summary>
	class WindowSizeDownCommand : IShortcutCommand
	{
		private Form form;
		private PecaPlayerControl pecaPlayer;

		public WindowSizeDownCommand(Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;
		}

		public void Execute()
		{
			int height = (int)(pecaPlayer.ImageHeight * 0.1);
			form.Width -= (int)(height * pecaPlayer.AspectRate);
			form.Height -= height;
		}

		public string Detail
		{
			get { return "ウィンドウサイズを小さく"; }
		}
	}
}
