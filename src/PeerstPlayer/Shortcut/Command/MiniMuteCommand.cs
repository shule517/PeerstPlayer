using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 最小化ミュートコマンド
	/// </summary>
	class MiniMuteCommand : IShortcutCommand
	{
		private Form form;
		private PecaPlayerControl pecaPlayer;

		public MiniMuteCommand(Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;
		}

		public void Execute(CommandArgs commandArgs)
		{
			form.WindowState = FormWindowState.Minimized;
			pecaPlayer.Mute = true;
		}

		public string Detail
		{
			get { return "最小化ミュート"; }
		}
	}
}
