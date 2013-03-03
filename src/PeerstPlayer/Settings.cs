using System;
using System.Collections.Generic;
using System.Text;

namespace PeerstPlayer
{
	class Settings
	{
		/// <summary>
		/// ブラウザアドレス
		/// </summary>
		public string BrowserAddress = "";

		/// <summary>
		/// スレッドブラウザアドレス
		/// </summary>
		public string ThreadBrowserAddress = "";

		/// <summary>
		/// レスボックスの操作
		/// true（Enter：改行 / Shift+Enter：書き込み）
		/// false（Enter：書き込み / Shift+Enter：改行）
		/// </summary>
		public bool ResBoxType = true;

		/// <summary>
		/// レスボックスを自動的に隠すか
		/// </summary>
		public bool ResBoxAutoVisible = false;

		/// <summary>
		/// 終了時にリレーを切断する
		/// </summary>
		public bool RlayCutOnClose = true;

		/// <summary>
		/// クリックした時レスボックスを閉じるか
		/// </summary>
		public bool ClickToResBoxClose = true;

		/// <summary>
		/// 終了時に位置を保存するか
		/// </summary>
		public bool SaveLocationOnClose = false;

		/// <summary>
		/// 終了時にサイズを保存するか
		/// </summary>
		public bool SaveSizeOnClose = false;

		/// <summary>
		/// アスペクト比を維持
		/// </summary>
		public bool AspectRate = true;

		/// <summary>
		/// 書き込み時にレスボックスを閉じる
		/// </summary>
		public bool CloseResBoxOnWrite = true;

		/// <summary>
		/// バックスペースでレスボックスを閉じるか
		/// </summary>
		public bool CloseResBoxOnBackSpace = false;

		/// <summary>
		/// 最小化ミュート時
		/// </summary>
		public bool MiniMute = false;

		/// <summary>
		/// デフォルト拡大率
		/// </summary>
		public int DefaultScale = -1;

		/// <summary>
		/// 起動時に動画サイズを合わせる
		/// </summary>
		public bool FitSizeMovie = false;

		/// <summary>
		/// スクリーン吸着距離
		/// </summary>
		public int ScreenMagnetDockDist = 20;

		/// <summary>
		/// 終了時に一緒にビューワも終了するか
		/// </summary>
		public bool CloseViewerOnClose = false;

		/// <summary>
		/// 終了時のボリュームを保存するか
		/// </summary>
		public bool SaveVolumeOnClose = false;

		/// <summary>
		/// スクリーン吸着をするか
		/// </summary>
		public bool UseScreenMagnet = true;
	}
}
