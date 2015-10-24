using System;
using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 再接続(プレイヤー)コマンド
	/// </summary>
	class RetryPlayerCommand : IShortcutCommand
	{
		private PecaPlayerControl pecaPlayer;

		public RetryPlayerCommand(PecaPlayerControl pecaPlayer)
		{
			this.pecaPlayer = pecaPlayer;
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			pecaPlayer.RetryPlayer();
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "再接続(プレイヤー)する";
		}
	}
}
