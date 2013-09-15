using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;
using PeerstPlayer.Forms.Player;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 閉じるコマンド
	/// </summary>
	class CloseCommand : IShortcutCommand
	{
		private Form form;
		private PecaPlayerControl pecaPlayer;

		public CloseCommand(Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;
		}

		public void Execute(CommandArgs commandArgs)
		{
			// 非表示ミュート
			form.Visible = false;
			pecaPlayer.Mute = true;

			// 終了時のリレー切断
			if (PlayerSettings.DisconnectRealyOnClose)
			{
				pecaPlayer.DisconnectRelay();
			}

			Application.Exit();
		}

		public string Detail
		{
			get { return "PeerstPlayerを終了"; }
		}
	}
}
