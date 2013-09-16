
namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// ショートカットコマンドインターフェイス
	/// </summary>
	public interface IShortcutCommand
	{
		/// <summary>
		/// コマンドを実行
		/// </summary>
		void Execute(CommandArgs commandArgs);

		/// <summary>
		/// コマンドの詳細
		/// </summary>
		string GetDetail(CommandArgs commandArgs);
	}
}
