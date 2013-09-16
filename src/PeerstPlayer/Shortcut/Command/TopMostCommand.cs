
using System.Windows.Forms;
namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 最前列表示切り替えコマンド
	/// </summary>
	class TopMostCommand : IShortcutCommand
	{
		private Form form;

		public TopMostCommand(Form form)
		{
			this.form = form;
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			form.TopMost = !form.TopMost;
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return string.Format("最前列表示切り替え"); 
		}
	}
}
