using PeerstPlayer.Shortcut.Command;

namespace PeerstPlayer.Shortcut
{
	/// <summary>
	/// ショートカットコマンドクラス
	/// </summary>
	public class ShortcutCommand
	{
		/// <summary>
		/// コマンド
		/// </summary>
		public IShortcutCommand command;

		/// <summary>
		/// コマンドの引数
		/// </summary>
		public CommandArgs commandArgs;

		public ShortcutCommand(IShortcutCommand command, CommandArgs commandArgs)
		{
			this.command = command;
			this.commandArgs = commandArgs;
		}

		/// <summary>
		/// コマンドの実行
		/// </summary>
		public void Execute()
		{
			command.Execute(commandArgs);
		}

		/// <summary>
		/// コマンドの詳細
		/// </summary>
		public string Detail
		{
			get { return command.GetDetail(commandArgs); }
		}
	}
}
