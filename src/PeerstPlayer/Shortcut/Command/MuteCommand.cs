using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// ミュート切り替えコマンド
	/// </summary>
	class MuteCommand : IShortcutCommand
	{
		private PecaPlayerControl pecaPlayer;

		public MuteCommand(PecaPlayerControl pecaPlayer)
		{
			this.pecaPlayer = pecaPlayer;
		}

		public void Execute()
		{
			pecaPlayer.Mute = !pecaPlayer.Mute;
		}

		public string Detail
		{
			get { return "ミュート"; }
		}
	}
}
