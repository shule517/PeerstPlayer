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

		public void Execute()
		{
			form.WindowState = FormWindowState.Minimized;
		}

		public string Detail
		{
			get { return "ウィンドウを最小化"; }
		}
	}
}
