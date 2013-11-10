
namespace PeerstViewer.Settings
{
	/// <summary>
	/// Viewerの設定情報
	/// </summary>
	class ViewerSettingsInfo
	{
		/// <summary>
		/// 初期位置X
		/// </summary>
		public int X = 0;

		/// <summary>
		/// 初期位置Y
		/// </summary>
		public int Y = 0;

		/// <summary>
		/// 初期幅
		/// </summary>
		public int Wdith = 800;

		/// <summary>
		/// 初期高さ
		/// </summary>
		public int Height = 600;

		/// <summary>
		/// 最前列表示
		/// </summary>
		public bool TopMost = false;

		/// <summary>
		/// 自動更新をするか
		/// </summary>
		public bool AutoReload = true;

		/// <summary>
		/// 自動更新時間
		/// </summary>
		public int AutoReloadInterval = 7;

		/// <summary>
		/// 書き込み欄を表示するか
		/// </summary>
		public bool WriteView = false;

		/// <summary>
		/// 初期スレッド更新時に最下位までスクロールすか
		/// </summary>
		public bool ScrollBottom = true;

		/// <summary>
		/// フォント名
		/// </summary>
		public string FontName = @"<body bgcolor=""#E6EEF3"" style=""font-family:'※※※','ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:16px;line-height:18px;"" >";

		/// <summary>
		/// 書き込み欄の高さ
		/// </summary>
		public int WriteHeight = 215;

		/// <summary>
		/// 折り返し表示
		/// </summary>
		public bool NoBR = false;

		/// <summary>
		/// 終了時に位置を保存
		/// </summary>
		public bool SaveLocationOnClose = false;

		/// <summary>
		/// 終了時にサイズを保存
		/// </summary>
		public bool SaveSizeOnClose = false;
	}
}
