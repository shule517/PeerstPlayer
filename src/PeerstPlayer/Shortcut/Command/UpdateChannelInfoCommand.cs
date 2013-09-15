using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// チャンネル情報更新コマンド
	/// </summary>
	class UpdateChannelInfoCommand : IShortcutCommand
	{
		private PecaPlayerControl pecaPlayer;

		public UpdateChannelInfoCommand(PecaPlayerControl pecaPlayer)
		{
			this.pecaPlayer = pecaPlayer;
		}

		public void Execute(CommandArgs commandArgs)
		{
			pecaPlayer.UpdateChannelInfo();
		}

		public string Detail
		{
			get { return "チャンネル情報を更新"; }
		}
	}
}
