
namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// ショートカットコマンドインターフェイス
	/// </summary>
	interface IShortcutCommand
	{
		/// <summary>
		/// コマンドを実行
		/// </summary>
		void Execute();

		/// <summary>
		/// コマンドの詳細
		/// </summary>
		string Detail { get; }
	}
}
