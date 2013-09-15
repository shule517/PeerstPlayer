using System.Windows.Forms;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// ウィンドウ最大化コマンド
	/// </summary>
	class WindowMaximize : IShortcutCommand
	{
		private Form form;

		public WindowMaximize(Form form)
		{
			this.form = form;
		}

		public void Execute(CommandArgs commandArgs)
		{
			if (form.WindowState == FormWindowState.Normal)
			{
				form.WindowState = FormWindowState.Maximized;
			}
			else
			{
				form.WindowState = FormWindowState.Normal;
			}
		}

		public string Detail
		{
			get { return "ウィンドウを最大化"; }
		}
	}
}
