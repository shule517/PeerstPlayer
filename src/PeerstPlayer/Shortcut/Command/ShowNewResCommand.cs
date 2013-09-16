
using System.Windows.Forms;
using PeerstPlayer.Controls.StatusBar;
namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 新着レス表示コマンド
	/// </summary>
	class ShowNewResCommand : IShortcutCommand
	{
		private StatusBarControl statusBar;
		private Form form;

		public ShowNewResCommand(Form form, StatusBarControl statusBar)
		{
			this.form = form;
			this.statusBar = statusBar;
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			// TODO バルーンの表示方法を考える
			/*
			string newRes = statusBar.ReadNewRes();
			Help.ShowPopup(form, newRes, Control.MousePosition);
			 */
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "新着レス表示";
		}
	}
}
