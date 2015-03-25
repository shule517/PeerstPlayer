using System.Drawing;

namespace PeerstViewer.Settings
{
	public class ViewerSettingsInfo
	{
		/// <summary>
		/// 起動時にウィンドウサイズを復帰するか
		/// </summary>
		public bool ReturnSizeOnStart = false;

		/// <summary>
		/// 復帰サイズ
		/// </summary>
		public Size ReturnSize = new Size(784, 472);

		/// <summary>
		/// 起動時にウィンドウ位置を復帰するか
		/// </summary>
		public bool ReturnPositionOnStart = false;

		/// <summary>
		/// 復帰サイズ
		/// </summary>
		public Point ReturnPosition = new Point(0, 0);

		/// <summary>
		/// 終了時にウィンドウ位置を保存するか
		/// </summary>
		public bool SaveReturnPositionOnClose = false;

		/// <summary>
		/// 終了時にウィンドウサイズを保存するか
		/// </summary>
		public bool SaveReturnSizeOnClose = false;

		/// <summary>
		/// リンクをブラウザで開くか
		/// </summary>
		public bool OpenLinkBrowser = true;

		/// <summary>
		/// 書き込みウィンドウの高さ
		/// </summary>
		public int WriteFieldDistance = 271;
	}
}