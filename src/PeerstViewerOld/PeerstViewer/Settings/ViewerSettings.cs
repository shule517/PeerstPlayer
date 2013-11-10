using PeerstLib.Controls;
using PeerstLib.Util;
using System;

namespace PeerstViewer.Settings
{
	/// <summary>
	/// Viewerの設定クラス
	/// </summary>
	class ViewerSettings
	{
		/// <summary>
		/// 設定情報
		/// </summary>
		private static ViewerSettingsInfo info = new ViewerSettingsInfo();

		/// <summary>
		/// 保存ファイルパス
		/// </summary>
		private static string filePath = string.Format("{0}/PeerstViewer.xml", FormUtility.GetExeFolderPath());

		/// <summary>
		/// 初期位置X
		/// </summary>
		public static int X
		{
			get { return info.X; }
			set { info.X = value; }
		}

		/// <summary>
		/// 初期位置Y
		/// </summary>
		public static int Y
		{
			get { return info.Y; }
			set { info.Y = value; }
		}

		/// <summary>
		/// 初期幅
		/// </summary>
		public static int Width
		{
			get { return info.Width; }
			set { info.Width = value; }
		}

		/// <summary>
		/// 初期高さ
		/// </summary>
		public static int Height
		{
			get { return info.Height; }
			set { info.Height = value; }
		}

		/// <summary>
		/// 最前列表示
		/// </summary>
		public static bool TopMost
		{
			get { return info.TopMost; }
			set { info.TopMost = value; }
		}

		/// <summary>
		/// 自動更新をするか
		/// </summary>
		public static bool AutoReload
		{
			get { return info.AutoReload; }
			set { info.AutoReload = value; }
		}

		/// <summary>
		/// 自動更新時間
		/// </summary>
		public static int AutoReloadInterval
		{
			get { return info.AutoReloadInterval; }
			set { info.AutoReloadInterval = value; }
		}

		/// <summary>
		/// 書き込み欄を表示するか
		/// </summary>
		public static bool WriteView
		{
			get { return info.WriteView; }
			set { info.WriteView = value; }
		}

		/// <summary>
		/// 初期スレッド更新時に最下位までスクロールすか
		/// </summary>
		public static bool ScrollBottom
		{
			get { return info.ScrollBottom; }
			set { info.ScrollBottom = value; }
		}

		/// <summary>
		/// フォント名
		/// </summary>
		public static string FontName
		{
			get { return info.FontName; }
			set { info.FontName = value; }
		}

		/// <summary>
		/// 書き込み欄の高さ
		/// </summary>
		public static int WriteHeight
		{
			get { return info.WriteHeight; }
			set { info.WriteHeight = value; }
		}

		/// <summary>
		/// 折り返し表示
		/// </summary>
		public static bool NoBR
		{
			get { return info.NoBR; }
			set { info.NoBR = value; }
		}

		/// <summary>
		/// 終了時に位置を保存
		/// </summary>
		public static bool SaveLocationOnClose
		{
			get { return info.SaveLocationOnClose; }
			set { info.SaveLocationOnClose = value; }
		}

		/// <summary>
		/// 終了時にサイズを保存
		/// </summary>
		public static bool SaveSizeOnClose
		{
			get { return info.SaveSizeOnClose; }
			set { info.SaveSizeOnClose = value; }
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		public static void Load()
		{
			Object data;
			if (Serializer.TryLoad(filePath, typeof(ViewerSettingsInfo), out data))
			{
				info = (ViewerSettingsInfo)data;
				return;
			}

			// 読み込みに失敗したら設定を初期化
			info = new ViewerSettingsInfo();
		}

		/// <summary>
		/// 設定を保存する
		/// </summary>
		public static void Save()
		{
			Serializer.Save(filePath, info);
		}
	}
}
