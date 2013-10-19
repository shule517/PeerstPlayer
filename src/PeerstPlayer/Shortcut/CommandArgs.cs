
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
	[KnownType(typeof(ScreenSplitCommandArgs))]
	[KnownType(typeof(VolumeCommandArgs))]
	[KnownType(typeof(VolumeBalanceCommandArgs))]
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
		/// 画面分割数(幅)
		/// </summary>
		[DataMember]
		public int WidthNum;

		/// <summary>
		/// 画面分割数(高さ)
		/// </summary>
		[DataMember]
		public int HeightNum;

		public ScreenSplitCommandArgs(int widthNum, int heightNum)
		{
			this.WidthNum = widthNum;
			this.HeightNum = heightNum;
		}
	}

	/// <summary>
	/// 音量変更コマンド
	/// </summary>
	[DataContract(Name = "VolumeCommandArgs")]
	public class VolumeCommandArgs : CommandArgs
	{
		/// <summary>
		/// 音量
		/// </summary>
		[DataMember]
		public int Volume;
		public VolumeCommandArgs(int volume)
		{
			this.Volume = volume;
		}
	}

	/// <summary>
	/// 音量バランス変更コマンド
	/// </summary>
	[DataContract(Name = "VolumeBaranceCommandArgs")]
	public class VolumeBalanceCommandArgs : CommandArgs
	{
		public const int BalanceLeft = -100;
		public const int BalanceMiddle = 0;
		public const int BalanceRight = 100;

		/// <summary>
		/// 音量バランス
		/// </summary>
		[DataMember]
		public int VolumeBalance;
		public VolumeBalanceCommandArgs(int volumeBalance)
		{
			this.VolumeBalance = volumeBalance;
		}
	}
}
