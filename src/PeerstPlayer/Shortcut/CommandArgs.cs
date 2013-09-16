
using System.Drawing;
using System.Runtime.Serialization;
namespace PeerstPlayer.Shortcut
{
	/// <summary>
	/// コマンド引数
	/// </summary>
	[DataContract(Name = "CommandArgs")]
	[KnownType(typeof(WindowScaleCommandArgs))]
	[KnownType(typeof(WindowSizeCommandArgs))]
	public class CommandArgs
	{
		public static readonly CommandArgs Empty = new CommandArgs();
	}

	/// <summary>
	/// ウィンドウ拡大率コマンドの引数
	/// </summary>
	[DataContract(Name = "WindowScaleCommandArgs")]
	public class WindowScaleCommandArgs : CommandArgs
	{
		/// <summary>
		/// 拡大率
		/// </summary>
		[DataMember]
		public float Scale;
		public WindowScaleCommandArgs(float scale)
		{
			this.Scale = scale;
		}
	}

	/// <summary>
	/// ウィンドウサイズコマンドの引数
	/// </summary>
	[DataContract(Name = "WindowSizeCommandArgs")]
	public class WindowSizeCommandArgs : CommandArgs
	{
		/// <summary>
		/// ウィンドウサイズ
		/// </summary>
		[DataMember]
		public Size Size;
		public WindowSizeCommandArgs(int width, int height)
		{
			this.Size = new Size(width, height);
		}
	}
}
