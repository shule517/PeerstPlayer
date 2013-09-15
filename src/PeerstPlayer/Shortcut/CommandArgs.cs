
using System.Drawing;
namespace PeerstPlayer.Shortcut
{
	/// <summary>
	/// コマンド引数
	/// </summary>
	public class CommandArgs
	{
		public static readonly CommandArgs Empty = new CommandArgs();
	}

	/// <summary>
	/// ウィンドウ拡大率コマンドの引数
	/// </summary>
	public class WindowScaleCommandArgs : CommandArgs
	{
		/// <summary>
		/// 拡大率
		/// </summary>
		public float Scale;
		public WindowScaleCommandArgs(float scale)
		{
			this.Scale = scale;
		}
	}

	/// <summary>
	/// ウィンドウサイズコマンドの引数
	/// </summary>
	public class WindowSizeCommandArgs : CommandArgs
	{
		/// <summary>
		/// ウィンドウサイズ
		/// </summary>
		public Size Size;
		public WindowSizeCommandArgs(int width, int height)
		{
			this.Size = new Size(width, height);
		}
	}
}
