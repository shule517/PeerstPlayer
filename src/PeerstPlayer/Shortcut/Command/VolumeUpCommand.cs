using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;

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

		public void Execute(CommandArgs commandArgs)
		{
			if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
			{
				pecaPlayer.Volume += 1;
			}
			else if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
			{
				pecaPlayer.Volume += 5;
			}
			else
			{
				pecaPlayer.Volume += 10;
			}
		}

		public string Detail
		{
			get { return "音量を上げる"; }
		}
	}
}
