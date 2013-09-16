
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
		public Commands command;

		/// <summary>
		/// コマンドの引数
		/// </summary>
		[DataMember]
		public CommandArgs args;

		public ShortcutInfo(Commands command, CommandArgs args)
		{
			this.command = command;
			this.args = args;
		}
	}
}
