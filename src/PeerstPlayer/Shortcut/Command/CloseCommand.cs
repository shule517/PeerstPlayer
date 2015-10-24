using System;
using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;
using PeerstPlayer.Forms.Player;
using System.Drawing;
using PeerstLib.Util;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 閉じるコマンド
	/// </summary>
	class CloseCommand : IShortcutCommand
	{
		private Form form;
		private PecaPlayerControl pecaPlayer;

		public CloseCommand(Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;
		}

		public void Execute(CommandArgs commandArgs)
		{
			// 非表示ミュート
			form.Visible = false;
			pecaPlayer.Mute = true;

			// 終了時のリレー切断
			if (PlayerSettings.DisconnectRealyOnClose)
			{
				pecaPlayer.DisconnectRelay();
			}

			// 終了時の位置とサイズを保存
			if (PlayerSettings.SaveReturnPositionOnClose)
			{
				PlayerSettings.ReturnPosition = form.Location;
			}
			if (PlayerSettings.SaveReturnSizeOnClose)
			{
				PlayerSettings.ReturnSize = pecaPlayer.Size;
			}

			try
			{
				PlayerSettings.Save();
			}
			catch (Exception e)
			{
				Logger.Instance.Error(e.Message);
			}

			Application.Exit();
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "終了する";
		}
	}
}
