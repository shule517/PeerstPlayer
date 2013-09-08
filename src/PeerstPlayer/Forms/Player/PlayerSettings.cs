
namespace PeerstPlayer.Forms.Player
{
	//-------------------------------------------------------------
	// 概要：動画プレイヤーの設定情報クラス
	//-------------------------------------------------------------
	static class PlayerSettings
	{
		/// <summary>
		/// 終了時にリレー切断をするか
		/// </summary>
		static public bool DisconnectRealyOnClose { get; set; }

		// - ステータスバーフォント : Font
		// - ステータスバー表示項目 : List
		// - 終了時位置保存 : boolean
		// - 終了時音量保存 : boolean
		// - 初期表示書き込み欄表示 : boolean
		// - 初期表示フレーム表示 : boolean
		// - 初期表示ステータスバー表示 : boolean
		// - 初期音量ミュート : boolean
		// - 初期音量 : int
		// - 初期最前列表示 : boolean
		// - アスペクト比固定 : boolean
		// - ウィンドウ吸着 : boolean
		// - 書き込み欄(Ctrl+Enter) : boolean
		// - レスが1000のスレを表示するか : boolean

		// TODO 別クラスに持つ？
		// - マウスジェスチャー : List
		// - ショートカット : List

		static PlayerSettings()
		{
			DisconnectRealyOnClose = true;
		}

		static void Load()
		{
			// TODO 設定の読み込み
		}

		static void Save()
		{
			// TODO 設定の保存
		}
	}
}
