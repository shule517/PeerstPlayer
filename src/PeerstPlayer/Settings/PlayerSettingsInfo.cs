
using PeerstPlayer.Shortcut;
using System.Drawing;
namespace PeerstPlayer.Settings
{
	/// <summary>
	/// プレイヤーの設定情報クラス
	/// </summary>
	public class PlayerSettingsInfo
	{
		/// <summary>
		/// 終了時にリレー切断をするか
		/// </summary>
		public bool DisconnectRealyOnClose = true;

		/// <summary>
		/// ウィンドウ：ウィンドウスナップ有効
		/// </summary>
		public bool WindowSnapEnable = true;

		/// <summary>
		/// ウィンドウ：アスペクト比固定
		/// </summary>
		public bool AspectRateFix = true;

		/// <summary>
		/// ウィンドウ：ウィンドウ枠を消す
		/// </summary>
		public bool FrameInvisible = false;

		/// <summary>
		/// 初期最前列表示
		/// </summary>
		public bool TopMost = false;

		/// <summary>
		/// 書き込み欄表示
		/// </summary>
		public bool WriteFieldVisible = true;

		/// <summary>
		/// マウスジェスチャーの感度
		/// </summary>
		public int MouseGestureInterval = 10;

		/// <summary>
		/// FPS表示
		/// </summary>
		public bool DisplayFps = true;

		/// <summary>
		/// リスナー数表示
		/// </summary>
		public bool DisplayListenerNumber = true;

		/// <summary>
		/// ビットレート表示
		/// </summary>
		public bool DisplayBitrate = true;

		/// <summary> 
		/// フルスクリーン時にステータスバーを非表示するか
		/// </summary>
		public bool HideStatusBarOnFullscreen = false;

		/// <summary>
		/// 初期音量
		/// </summary>
		public int InitVolume = 50;

		/// <summary>
		/// 音量変更：キー押下なし
		/// </summary>
		public int VolumeChangeNone = 10;

		/// <summary>
		/// 音量変更：Ctrlキー押下
		/// </summary>
		public int VolumeChangeCtrl = 5;

		/// <summary>
		/// 音量変更：Shiftキー押下
		/// </summary>
		public int VolumeChangeShift = 1;

		/// <summary>
		/// 動画再生開始時に実行するコマンド
		/// </summary>
		public Commands MovieStartCommand = Commands.FitMovieSize;

		/// <summary>
		/// 起動時にウィンドウサイズを復帰するか
		/// </summary>
		public bool ReturnSizeOnStart = false;

		/// <summary>
		/// 復帰サイズ
		/// </summary>
		public Size ReturnSize = new Size(480, 360);

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
		/// スクリーンショットを保存するフォルダ
		/// </summary>
		public string ScreenshotFolder = "Screenshot";

		/// <summary>
		/// スクリーンショットの保存形式
		/// </summary>
		public string ScreenshotExtension = "png";

		/// <summary>
		/// スクリーンショットファイル名の書式
		/// </summary>
		public string ScreenshotFormat = "yyyyMMdd_HHmmss";

		/// <summary>
		/// 自動スレ移動
		/// </summary>
		public bool AutoReadThread = false;

		/// <summary>
		/// Player終了時にViewerも終了させる
		/// </summary>
		public bool ExitedViewerClose = false;

		/// <summary>
		/// FLVで再生支援を使うか
		/// </summary>
		public bool Gpu = true;

		/// <summary>
		/// VLCのフォルダ
		/// </summary>
		public string VlcFolder;

		// - ステータスバーフォント : Font
		// - ステータスバー表示項目 : List
		// - 終了時位置保存 : boolean
		// - 終了時音量保存 : boolean
		// - 初期音量ミュート : boolean
		// - 書き込み欄(Ctrl+Enter) : boolean
		// - レスが1000のスレを表示するか : boolean
	};
}
