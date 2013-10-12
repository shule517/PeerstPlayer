using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 音量Downコマンド
	/// </summary>
	class VolumeDownCommand : IShortcutCommand
	{
		private PecaPlayerControl pecaPlayer;

		public VolumeDownCommand(PecaPlayerControl pecaPlayer)
		{
			this.pecaPlayer = pecaPlayer;
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
			{
				pecaPlayer.Volume -= 1;
			}
			else if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
			{
				pecaPlayer.Volume -= 5;
			}
			else
			{
				pecaPlayer.Volume -= 10;
			}
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			// TODO return string.Format("音量：{}", commandArgs.);
			return "音量 (下げる)";
		}
	}
}
