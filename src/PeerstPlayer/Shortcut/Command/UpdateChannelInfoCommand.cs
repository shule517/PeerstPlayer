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

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			pecaPlayer.UpdateChannelInfo();
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "チャンネル情報を更新";
		}
	}
}
