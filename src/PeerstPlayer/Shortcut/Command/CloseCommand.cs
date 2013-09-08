using System.Windows.Forms;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 閉じるコマンド
	/// </summary>
	class CloseCommand : IShortcutCommand
	{
		public void Execute()
		{
			/*
			// TODO 終了時のリレー切断
			if (PlayerSettings.DisconnectRealyOnClose)
			{
				PeerCastOperate.DisconnectRelay();
			}
			 */

			Application.Exit();
		}

		public string Detail
		{
			get { return "PeerstPlayerを終了"; }
		}
	}
}
