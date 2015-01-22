
using System.ComponentModel;
using PeerstLib.Controls;
using PeerstLib.Util;
using PeerstPlayer.Settings;
using PeerstPlayer.Shortcut;
using System;
using System.Drawing;
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
		/// 起動時にウィンドウサイズを保存するか
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
		/// 起動時にウィンドウ位置を保存するか
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
		/// ウィンドウ：ウィンドウスナップ有効
		/// </summary>
		public static bool WindowSnapEnable
		{
			get { return info.WindowSnapEnable; }
			set { info.WindowSnapEnable = value; }
		}

		/// <summary>
		/// ウィンドウ：アスペクト比固定
		/// </summary>
		public static bool AspectRateFix
		{
			get { return info.AspectRateFix; }
			set { info.AspectRateFix = value; }
		}

		/// <summary>
		/// ウィンドウ：ウィンドウ枠を消す
		/// </summary>
		public static bool FrameInvisible
		{
			get { return info.FrameInvisible; }
			set { info.FrameInvisible = value; }
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
		/// 書き込み欄表示
		/// </summary>
		public static bool WriteFieldVisible
		{
			get { return info.WriteFieldVisible; }
			set { info.WriteFieldVisible = value; }
		}

		/// <summary>
		/// マウスジェスチャーの感度
		/// </summary>
		public static int MouseGestureInterval
		{
			get { return info.MouseGestureInterval; }
			set { info.MouseGestureInterval = value; }
		}

		/// <summary>
		/// FPS表示
		/// </summary>
		public static bool DisplayFps
		{
			get { return info.DisplayFps; }
			set { info.DisplayFps = value; }
		}

		/// <summary>
		/// ビットレート表示
		/// </summary>
		public static bool DisplayBitrate
		{
			get { return info.DisplayBitrate; }
			set { info.DisplayBitrate = value; }
		}

		/// <summary>
		/// リスナー数表示
		/// </summary>
		public static bool DisplayListenerNumber
		{
			get { return info.DisplayListenerNumber; }
			set { info.DisplayListenerNumber = value; }
		}

		/// <summary> 
		/// フルスクリーン時にステータスバーを非表示するか
		/// </summary>
		public static bool HideStatusBarOnFullscreen
		{
			get { return info.HideStatusBarOnFullscreen; }
			set { info.HideStatusBarOnFullscreen = value; }
		}

		/// <summary>
		/// 初期音量
		/// </summary>
		public static int InitVolume
		{
			get { return info.InitVolume; }
			set { info.InitVolume = value; }
		}

		/// <summary>
		/// 音量変更：キー押下なし
		/// </summary>
		public static int VolumeChangeNone
		{
			get { return info.VolumeChangeNone; }
			set { info.VolumeChangeNone = value; }
		}

		/// <summary>
		/// 音量変更：Ctrlキー押下
		/// </summary>
		public static int VolumeChangeCtrl
		{
			get { return info.VolumeChangeCtrl; }
			set { info.VolumeChangeCtrl = value; }
		}

		/// <summary>
		/// 音量変更：Shiftキー押下
		/// </summary>
		public static int VolumeChangeShift
		{
			get { return info.VolumeChangeShift; }
			set { info.VolumeChangeShift = value; }
		}

		/// <summary>
		/// 動画再生開始時に実行するコマンド
		/// </summary>
		public static Commands MovieStartCommand
		{
			get { return info.MovieStartCommand; }
			set { info.MovieStartCommand = value; }
		}

		/// <summary>
		/// スクリーンショットを保存するフォルダ
		/// </summary>
		public static string ScreenshotFolder
		{
			get { return info.ScreenshotFolder; }
			set { info.ScreenshotFolder = value; }
		}

		/// <summary>
		/// スクリーンショットの保存形式
		/// </summary>
		public static string ScreenshotExtension
		{
			get { return info.ScreenshotExtension; }
			set { info.ScreenshotExtension = value; }
		}

		/// <summary>
		/// スクリーンショットファイル名の書式
		/// </summary>
		public static string ScreenshotFormat
		{
			get { return info.ScreenshotFormat; }
			set { info.ScreenshotFormat = value; }
		}

		public static bool AutoReadThread
		{
			get { return info.AutoReadThread; }
			set { info.AutoReadThread = value; }
		}

		/// <summary>
		/// Player終了時にViewerも終了させる
		/// </summary>
		public static bool ExitedViewerClose
		{
			get { return info.ExitedViewerClose; }
			set { info.ExitedViewerClose = value; }
		}

		/// <summary>
		/// FLVで再生支援を使うか
		/// </summary>
		public static bool Gpu
		{
			get { return info.Gpu; }
			set
			{
				if (info.Gpu != value)
				{
					info.Gpu = value;
					RaisePropertyChanged("Gpu");
				}
			}
		}

		public static event Action<string> Changed;

		private static void RaisePropertyChanged(string property)
		{
			if (Changed != null)
			{
				Changed(property);
			}
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
