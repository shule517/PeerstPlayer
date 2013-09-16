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

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			int height = (int)(pecaPlayer.ImageHeight * 0.1);
			form.Width -= (int)(height * pecaPlayer.AspectRate);
			form.Height -= height;
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			int size = (int)(pecaPlayer.ImageHeight * 0.1);
			int width = form.Width - (int)(size * pecaPlayer.AspectRate);
			int height = form.Height - size;
			return string.Format("サイズ-10%");
		}
	}
}
