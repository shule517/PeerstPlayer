using System;
using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// Bumpコマンド
	/// </summary>
	class BumpCommand : IShortcutCommand
	{
		private PecaPlayerControl pecaPlayer;

		public BumpCommand(PecaPlayerControl pecaPlayer)
		{
			this.pecaPlayer = pecaPlayer;
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			pecaPlayer.Bump();
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "再接続(Bump)する";
		}
	}
}
