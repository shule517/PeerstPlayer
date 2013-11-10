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

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			// ウィンドウ状態が通常の場合にのみ実行する
			if (form.WindowState != FormWindowState.Normal)
			{
				return;
			}

			int height = (int)(pecaPlayer.ImageHeight * 0.1);
			form.Width += (int)(height * pecaPlayer.AspectRate);
			form.Height += height;
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			int size = (int)(pecaPlayer.ImageHeight * 0.1);
			int width = form.Width + (int)(size * pecaPlayer.AspectRate);
			int height = form.Height + size;
			return string.Format("サイズ (+10%)");
		}
	}
}
