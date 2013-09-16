using System.Windows.Forms;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// ウィンドウ最小化コマンド
	/// </summary>
	class WindowMinimize : IShortcutCommand
	{
		private Form form;

		public WindowMinimize(Form form)
		{
			this.form = form;
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			form.WindowState = FormWindowState.Minimized;
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "ウィンドウを最小化";
		}
	}
}
