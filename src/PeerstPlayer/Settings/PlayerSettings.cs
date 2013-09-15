
using System;
using PeerstLib.Controls;
using PeerstLib.Util;
using PeerstPlayer.Settings;
namespace PeerstPlayer.Forms.Player
{
	/// <summary>
	/// プレイヤーの設定クラス
	/// </summary>
	static class PlayerSettings
	{
		/// <summary>
		/// 設定情報
		/// </summary>
		private static PlayerSettingsInfo info = new PlayerSettingsInfo();

		/// <summary>
		/// 保存ファイルパス
		/// </summary>
		private static string filePath = string.Format("{0}/PeerstPlayer.xml", FormUtility.GetExeFolderPath());

		/// <summary>
		/// 終了時にリレー切断をするか
		/// </summary>
		public static bool DisconnectRealyOnClose
		{
			get { return info.DisconnectRealyOnClose; }
			set { info.DisconnectRealyOnClose = value; }
		}

		/// <summary>
		/// ウィンドウスナップ有効
		/// </summary>
		public static bool WindowSnapEnable
		{
			get { return info.WindowSnapEnable; }
			set { info.WindowSnapEnable = value; }
		}

		static PlayerSettings()
		{
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		public static void Load()
		{
			Object data;
			if (Serializer.TryLoad(filePath, typeof(PlayerSettingsInfo), out data))
			{
				info = (PlayerSettingsInfo)data;
				return;
			}

			// 読み込みに失敗したら設定を初期化
			info = new PlayerSettingsInfo();
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
