
namespace PeerstPlayer.Shortcut
{
	/// <summary>
	/// ショートカット情報クラス
	/// </summary>
	public class ShortcutInfo
	{
		/// <summary>
		/// コマンド
		/// </summary>
		public ShortcutCommands command;

		/// <summary>
		/// コマンドの引数
		/// </summary>
		public CommandArgs args;

		public ShortcutInfo(ShortcutCommands command, CommandArgs args)
		{
			this.command = command;
			this.args = args;
		}
	}
}
