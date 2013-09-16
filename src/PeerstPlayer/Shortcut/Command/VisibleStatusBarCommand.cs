using System.Windows.Forms;
using PeerstPlayer.Controls.StatusBar;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// ステータスーバー表示切り替えコマンド
	/// </summary>
	class VisibleStatusBarCommand : IShortcutCommand
	{
		private Form form;
		private StatusBarControl statusBar;

		public VisibleStatusBarCommand(Form form, StatusBarControl statusBar)
		{
			this.form = form;
			this.statusBar = statusBar;
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			// ウィンドウ最大化時は一度通常に戻す
			if (form.WindowState == FormWindowState.Maximized)
			{
				form.WindowState = FormWindowState.Normal;
				statusBar.WriteFieldVisible = !statusBar.WriteFieldVisible;
				form.WindowState = FormWindowState.Maximized;
			}
			else
			{
				statusBar.WriteFieldVisible = !statusBar.WriteFieldVisible;
			}

			// ステータスバーにフォーカス
			if (statusBar.WriteFieldVisible)
			{
				statusBar.Focus();
			}
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "ステータスバー表示";
		}
	}
}
