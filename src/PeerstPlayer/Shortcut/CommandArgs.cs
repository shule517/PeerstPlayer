
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

	/// <summary>
	/// 画面分割コマンドの引数
	/// </summary>
	[DataContract(Name = "ScreenSplitCommandArgs")]
	public class ScreenSplitCommandArgs : CommandArgs
	{
		/// <summary>
		/// 画面分割数
		/// </summary>
		[DataMember]
		public int WidthNum;	// 幅分割数
		public int HeightNum;	// 高さ分割数
		public ScreenSplitCommandArgs(int widthNum, int heightNum)
		{
			this.WidthNum = widthNum;
			this.HeightNum = heightNum;
		}
	}
}
