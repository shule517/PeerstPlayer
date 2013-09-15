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

		public void Execute(CommandArgs commandArgs)
		{
			WindowSizeCommandArgs args = (WindowSizeCommandArgs)commandArgs;
			Size size = args.Size;
			form.Size = new Size(size.Width, size.Height);
		}

		public string Detail
		{
			get { return "サイズ"; }
		}
	}
}
