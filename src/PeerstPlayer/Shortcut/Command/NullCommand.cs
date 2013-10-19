using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 空コマンド
	/// </summary>
	class NullCommand : IShortcutCommand
	{
		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "処理なし";
		}
	}
}
