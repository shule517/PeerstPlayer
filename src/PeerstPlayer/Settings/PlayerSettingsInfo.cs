
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
		
		// - ステータスバーフォント : Font
		// - ステータスバー表示項目 : List
		// - 終了時位置保存 : boolean
		// - 終了時音量保存 : boolean
		// - 初期表示フレーム表示 : boolean
		// - 初期表示ステータスバー表示 : boolean
		// - 初期音量ミュート : boolean
		// - 初期音量 : int
		// - 書き込み欄(Ctrl+Enter) : boolean
		// - レスが1000のスレを表示するか : boolean

		// TODO 別クラスに持つ？
		// - マウスジェスチャー : List
		// - ショートカット : List
	};
}
