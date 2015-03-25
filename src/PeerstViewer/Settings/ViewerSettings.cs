
using System;
using System.Drawing;
using System.IO;
using PeerstLib.Controls;
using PeerstLib.Util;

namespace PeerstViewer.Settings
{
	/// <summary>
	/// ビューワーの設定クラス
	/// </summary>
	static class ViewerSettings
	{
		/// <summary>
		/// 設定情報
		/// </summary>
		private static ViewerSettingsInfo info = new ViewerSettingsInfo();

		/// <summary>
		///  保存ファイルパス
		/// </summary>
		private static string filePath = string.Format("{0}/PeerstViewer.xml", FormUtility.GetExeFolderPath());

		/// <summary>
		/// 起動時にウィンドウサイズを復帰するか
		/// </summary>
		public static bool ReturnSizeOnStart
		{
			get { return info.ReturnSizeOnStart; }
			set { info.ReturnSizeOnStart = value; }
		}

		/// <summary>
		/// 復帰ウィンドウサイズ
		/// </summary>
		public static Size ReturnSize
		{
			get { return info.ReturnSize; }
			set { info.ReturnSize = value; }
		}

		/// <summary>
		/// 起動時にウィンドウ位置を復帰するか
		/// </summary>
		public static bool ReturnPositionOnStart
		{
			get { return info.ReturnPositionOnStart; }
			set { info.ReturnPositionOnStart = value; }
		}

		/// <summary>
		/// 復帰ウィンドウ位置
		/// </summary>
		public static Point ReturnPosition
		{
			get { return info.ReturnPosition; }
			set { info.ReturnPosition = value; }
		}

		/// <summary>
		/// 終了時にウィンドウ位置を保存するか
		/// </summary>
		public static bool SaveReturnPositionOnClose
		{
			get { return info.SaveReturnPositionOnClose; }
			set { info.SaveReturnPositionOnClose = value; }
		}

		/// <summary>
		/// 終了時にウィンドウサイズを保存するか
		/// </summary>
		public static bool SaveReturnSizeOnClose
		{
			get { return info.SaveReturnSizeOnClose; }
			set { info.SaveReturnSizeOnClose = value; }
		}

		/// <summary>
		/// リンクをブラウザで開くか
		/// </summary>
		public static bool OpenLinkBrowser
		{
			get { return info.OpenLinkBrowser; }
			set { info.OpenLinkBrowser = value; }
		}

		/// <summary>
		/// 書き込みウィンドウの高さ
		/// </summary>
		public static int WriteFieldDistance
		{
			get { return info.WriteFieldDistance; }
			set { info.WriteFieldDistance = value; }
		}

		static ViewerSettings()
		{
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
			try
			{
				Serializer.Save(filePath, info);
			}
			catch (IOException e)
			{
                Logger.Instance.Error(e.Message);
			}
		}
	}
}