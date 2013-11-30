
using System;
using System.Windows.Forms;
using WMPLib;
namespace PeerstPlayer.Controls.MoviePlayer
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
		/// 音量変更イベント
		/// </summary>
		event EventHandler VolumeChange;

		/// <summary>
		/// 動画再生開始イベント
		/// </summary>
		event EventHandler MovieStart;

		/// <summary>
		/// ミュート
		/// </summary>
		bool Mute { get; set; }

		/// <summary>
		/// 再生時間
		/// </summary>
		string Duration { get; }

		/// <summary>
		/// バッファー率
		/// </summary>
		int BufferingProgress { get; }

		/// <summary>
		/// 再生状態
		/// </summary>
		WMPPlayState PlayState { get; }

		/// <summary>
		/// 動画再生状態
		/// </summary>
		WMPOpenState OpenState { get; }

		/// <summary>
		/// アスペクト比
		/// </summary>
		float AspectRate { get; }

		/// <summary>
		/// 動画の幅
		/// </summary>
		int ImageWidth { get; }

		/// <summary>
		/// 動画の高さ
		/// </summary>
		int ImageHeight { get; }
		
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
		/// ダブルクリックイベント
		/// </summary>
		event EventHandler DoubleClickEvent;

		/// <summary>
		/// キー押下イベント
		/// </summary>
		event AxWMPLib._WMPOCXEvents_KeyDownEventHandler KeyDownEvent;

		/// <summary>
		/// WMPのハンドル
		/// </summary>
		IntPtr WMPHandle { get; }

		/// <summary>
		/// コンテキストメニューの有効
		/// </summary>
		bool EnableContextMenu { get; set; }

		/// <summary>
		/// 現在のフレームレート
		/// </summary>
		int NowFrameRate { get; }

		/// <summary>
		/// フレームレート
		/// </summary>
		int FrameRate { get; }

		/// <summary>
		/// 現在のビットレート
		/// </summary>
		int NowBitrate { get; }

		/// <summary>
		/// ビットレート
		/// </summary>
		int Bitrate { get; }

		/// <summary>
		/// 動画コントロール
		/// </summary>
		Control MovieControl { get; }

		/// <summary>
		/// 動画再生
		/// </summary>
		/// <param name="streamUrl">ストリームURL</param>
		void PlayMoive(string streamUrl);
	}
}
