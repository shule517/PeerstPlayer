using System;
using System.Drawing;
using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// ウィンドウサイズ指定コマンド
	/// </summary>
	class WindowSizeCommand : IShortcutCommand
	{
		private Form form;
		private PecaPlayerControl pecaPlayer;

		public WindowSizeCommand(Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;
		}

		public void Execute()
		{
			form.Size = new Size(800, 600);
		}

		public string Detail
		{
			get { throw new NotImplementedException(); }
		}
	}
}
