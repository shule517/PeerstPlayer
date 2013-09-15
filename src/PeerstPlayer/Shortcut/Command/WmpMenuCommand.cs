using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeerstLib.Controls;
using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// WMPメニュー表示コマンド
	/// </summary>
	class WmpMenuCommand : IShortcutCommand
	{
		private PecaPlayerControl pecaPlayer;

		public WmpMenuCommand(PecaPlayerControl pecaPlayer)
		{
			this.pecaPlayer = pecaPlayer;
		}

		public void Execute(CommandArgs commandArgs)
		{
			pecaPlayer.EnableContextMenu = true;
			FormUtility.ShowContextMenu(this.pecaPlayer.WMPHandle, Cursor.Position);
			pecaPlayer.EnableContextMenu = false;
		}

		public string Detail
		{
			get { return "WMPメニュー表示"; }
		}
	}
}
