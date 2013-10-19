
using PeerstPlayer.Shortcut;
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
		/// ウィンドウスナップ有効
		/// </summary>
		public bool WindowSnapEnable = true;

		/// <summary>
		/// 初期最前列表示
		/// </summary>
		public bool TopMost = false;

		/// <summary>
		/// アスペクト比固定
		/// </summary>
		public bool AspectRateFix = true;

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

		// - ステータスバーフォント : Font
		// - ステータスバー表示項目 : List
		// - 終了時位置保存 : boolean
		// - 終了時音量保存 : boolean
		// - 初期表示フレーム表示 : boolean
		// - 初期表示ステータスバー表示 : boolean
		// - 初期音量ミュート : boolean
		// - 書き込み欄(Ctrl+Enter) : boolean
		// - レスが1000のスレを表示するか : boolean
	};
}
