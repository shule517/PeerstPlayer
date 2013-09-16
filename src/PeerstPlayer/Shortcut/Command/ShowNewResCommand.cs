
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

		public void Execute(CommandArgs commandArgs)
		{
			// TODO バルーンの表示方法を考える
			/*
			string newRes = statusBar.ReadNewRes();
			Help.ShowPopup(form, newRes, Control.MousePosition);
			 */
		}

		public string Detail
		{
			get { return "新着レスを表示"; }
		}
	}
}
