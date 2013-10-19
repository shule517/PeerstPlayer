using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;
using PeerstPlayer.Forms.Player;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 音量Upコマンド
	/// </summary>
	class VolumeUpCommand : IShortcutCommand
	{
		private PecaPlayerControl pecaPlayer;

		public VolumeUpCommand(PecaPlayerControl pecaPlayer)
		{
			this.pecaPlayer = pecaPlayer;
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
			{
				pecaPlayer.Volume += PlayerSettings.VolumeChangeShift;
			}
			else if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
			{
				pecaPlayer.Volume += PlayerSettings.VolumeChangeCtrl;
			}
			else
			{
				pecaPlayer.Volume += PlayerSettings.VolumeChangeNone;
			}
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "音量 (上げる)";
		}
	}
}
