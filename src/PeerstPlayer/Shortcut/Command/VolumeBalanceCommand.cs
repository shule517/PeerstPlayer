using PeerstPlayer.Controls.PecaPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 音量バランス変更コマンド
	/// </summary>
	class VolumeBalanceCommand : IShortcutCommand
	{
		private PecaPlayerControl pecaPlayer;

		public VolumeBalanceCommand(PecaPlayerControl pecaPlayer)
		{
			this.pecaPlayer = pecaPlayer;
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			VolumeBalanceCommandArgs args = (VolumeBalanceCommandArgs)commandArgs;
			pecaPlayer.VolumeBalance = args.VolumeBalance;
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			VolumeBalanceCommandArgs args = (VolumeBalanceCommandArgs)commandArgs;
			if (args.VolumeBalance == VolumeBalanceCommandArgs.BalanceMiddle)
			{
				return "音量バランス：中央";
			}
			else if (args.VolumeBalance == VolumeBalanceCommandArgs.BalanceLeft)
			{
				return "音量バランス：左";
			}
			else if (args.VolumeBalance == VolumeBalanceCommandArgs.BalanceRight)
			{
				return "音量バランス：右";
			}

			return "音量バランス";
		}
	}
}
