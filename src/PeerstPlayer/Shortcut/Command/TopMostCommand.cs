
namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 最前列表示切り替えコマンド
	/// </summary>
	class TopMostCommand : IShortcutCommand
	{
		private System.Windows.Forms.Form form;

		public TopMostCommand(System.Windows.Forms.Form form)
		{
			this.form = form;
		}

		public void Execute()
		{
			form.TopMost = !form.TopMost;
		}

		public string Detail
		{
			get { return "最前列表示を切り替える"; }
		}
	}
}
