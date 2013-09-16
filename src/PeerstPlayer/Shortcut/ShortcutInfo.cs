
using System.Runtime.Serialization;
namespace PeerstPlayer.Shortcut
{
	/// <summary>
	/// ショートカット情報クラス
	/// </summary>
	[DataContract(Name = "ShortcutInfo")]
	public class ShortcutInfo
	{
		/// <summary>
		/// コマンド
		/// </summary>
		[DataMember]
		public ShortcutCommands command;

		/// <summary>
		/// コマンドの引数
		/// </summary>
		[DataMember]
		public CommandArgs args;

		public ShortcutInfo(ShortcutCommands command, CommandArgs args)
		{
			this.command = command;
			this.args = args;
		}
	}
}
