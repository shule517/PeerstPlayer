using System;
using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// リレー切断コマンド
	/// </summary>
	class DisconnectRelayCommand : IShortcutCommand
	{
		private PecaPlayerControl pecaPlayer;
		private Form form;

		public DisconnectRelayCommand(Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;
		}

		public void Execute()
		{
			// リレー切断に時間がかかるため非表示にする
			form.Visible = false;
			pecaPlayer.Mute = true;

			// リレー切断＆終了
			pecaPlayer.DisconnectRelay();
			Application.Exit();
		}

		public string Detail
		{
			get { return "リレー切断"; }
		}
	}
}
