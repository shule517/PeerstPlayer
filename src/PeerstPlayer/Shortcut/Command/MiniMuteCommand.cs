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

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			form.WindowState = FormWindowState.Minimized;
			pecaPlayer.Mute = true;
			form.Resize += form_Resize;
		}

		/// <summary>
		/// 最小化を解除したらミュートを解除する
		/// </summary>
		void form_Resize(object sender, System.EventArgs e)
		{
			if (form.WindowState == FormWindowState.Normal)
			{
				pecaPlayer.Mute = false;
			}

			form.Resize -= form_Resize;
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "最小化ミュート";
		}
	}
}
