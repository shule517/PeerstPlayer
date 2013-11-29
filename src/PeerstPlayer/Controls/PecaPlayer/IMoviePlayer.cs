
using System;
using WMPLib;
namespace PeerstPlayer.Controls.PecaPlayer
{
	/// <summary>
	/// 動画プレイヤーインターフェイス
	/// </summary>
	interface IMoviePlayer
	{
		/// <summary>
		/// 音量
		/// </summary>
		int Volume { get; set; }

		/// <summary>
		/// 音量バランス
		/// </summary>
		int VolumeBalance { get; set; }

		/// <summary>
		/// ミュート
		/// </summary>
		bool Mute { get; set; }

		/// <summary>
		/// 再生時間
		/// </summary>
		string Duration { get; set; }

		/// <summary>
		/// バッファー率
		/// </summary>
		int BufferingProgress { get; set; }

		/// <summary>
		/// 再生状態
		/// </summary>
		WMPPlayState PlayState { get; set; }

		/// <summary>
		/// 動画再生状態
		/// </summary>
		WMPOpenState OpenState { get; set; }

		/// <summary>
		/// アスペクト比
		/// </summary>
		float AspectRate { get; set; }

		/// <summary>
		/// 動画の幅
		/// </summary>
		int ImageWidth { get; set; }

		/// <summary>
		/// 動画の高さ
		/// </summary>
		int ImageHeight { get; set; }
		
		/// <summary>
		/// マウス押下イベント
		/// </summary>
		event AxWMPLib._WMPOCXEvents_MouseDownEventHandler MouseDownEvent;

		/// <summary>
		///  マウスアップイベント
		/// </summary>
		event AxWMPLib._WMPOCXEvents_MouseUpEventHandler MouseUpEvent;

		/// <summary>
		///  マウス移動イベント
		/// </summary>
		event AxWMPLib._WMPOCXEvents_MouseMoveEventHandler MouseMoveEvent;

		/// <summary>
		/// キー押下イベント
		/// </summary>
		event AxWMPLib._WMPOCXEvents_KeyDownEventHandler KeyDownEvent;

		/// <summary>
		/// WMPのハンドル
		/// </summary>
		IntPtr WMPHandle { get; set; }

		/// <summary>
		/// コンテキストメニューの有効
		/// </summary>
		bool EnableContextMenu { get; set; }

		/// <summary>
		/// 現在のフレームレート
		/// </summary>
		int NowFrameRate { get; set; }

		/// <summary>
		/// フレームレート
		/// </summary>
		int FrameRate { get; set; }

		/// <summary>
		/// 現在のビットレート
		/// </summary>
		int NowBitrate { get; set; }

		/// <summary>
		/// ビットレート
		/// </summary>
		int Bitrate { get; set; }
	}
}
