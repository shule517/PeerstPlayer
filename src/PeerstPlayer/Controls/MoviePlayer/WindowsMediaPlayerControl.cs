using PeerstLib.Util;
using PeerstPlayer.Controls.PecaPlayer;
using System;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using WMPLib;

namespace PeerstPlayer.Controls.MoviePlayer
{
	/// <summary>
	/// WMPコントロール
	/// </summary>
	public partial class WindowsMediaPlayerControl : UserControl, IMoviePlayer
	{
		public WindowsMediaPlayerControl()
		{
			InitializeComponent();

			// 初期設定
			wmp.uiMode = "none";
			wmp.stretchToFit = true;
			wmp.enableContextMenu = false;

			// ダブルクリックイベント
			var wmpNativeWindow = new WmpNativeWindow(wmp);
			wmpNativeWindow.DoubleClick += (sender, e) => doubleClickEvent(sender, e);
			wmpNativeWindow.MouseDown += (sender, e) => mouseDownEvent(sender, e);

			// チャンネル自動リトライ
			new ChannelAutoRetry(wmp);

			// WMPフルスクリーンを無効化
			wmp.MouseDownEvent += (sender, e) =>
			{
				if (wmp.fullScreen)
				{
					wmp.fullScreen = false;
				}
			};

			wmp.PreviewKeyDown += (sender, args) =>
			{
				int state = 0;
				state += args.Shift ? 1 : 0;
				state += args.Control ? 1 << 1 : 0;
				state += args.Alt ? 1 << 2 : 0;
				keyDownEvent(this, new AxWMPLib._WMPOCXEvents_KeyDownEvent((short)args.KeyData, (short)state));
			};
		}

		/// <summary>
		/// 音量
		/// </summary>
		int IMoviePlayer.Volume
		{
			get { return wmp.settings.volume; }
			set
			{
				wmp.settings.volume = value;
				volumeChange(this, new EventArgs());
			}
		}

		/// <summary>
		/// 音量バランス
		/// </summary>
		int IMoviePlayer.VolumeBalance
		{
			get { return wmp.settings.balance; }
			set
			{
				wmp.settings.balance = value;
				volumeChange(this, new EventArgs());
			}
		}

		/// <summary>
		/// ミュート状態
		/// WMPの仕様で、動画切替時にミュートが解除されるための対応
		bool isMute = false;

		/// <summary>
		/// ミュート
		/// </summary>
		bool IMoviePlayer.Mute
		{
			get { return wmp.settings.mute; }
			set
			{
				wmp.settings.mute = value;
				isMute = value;
				volumeChange(this, new EventArgs());
			}
		}

		/// <summary>
		/// 再生時間
		/// </summary>
		string IMoviePlayer.Duration
		{
			get
			{
				string position = wmp.Ctlcontrols.currentPositionString;
				if (position.Length == 5)
				{
					position = "00:" + position;
				}

				return position;
			}
		}

		/// <summary>
		/// バッファー率
		/// </summary>
		int IMoviePlayer.BufferingProgress
		{
			get { return wmp.network.bufferingProgress; }
		}

		/// <summary>
		/// 再生状態
		/// </summary>
		WMPPlayState IMoviePlayer.PlayState
		{
			get { return wmp.playState; }
		}

		/// <summary>
		/// 動画再生状態
		/// </summary>
		WMPOpenState IMoviePlayer.OpenState
		{
			get { return wmp.openState; }
		}

		/// <summary>
		/// アスペクト比
		/// </summary>
		float IMoviePlayer.AspectRate
		{
			get { return (float)((IMoviePlayer)this).ImageWidth / (float)((IMoviePlayer)this).ImageHeight; }
		}

		/// <summary>
		/// 動画の幅
		/// </summary>
		int IMoviePlayer.ImageWidth
		{
			get { return ((wmp.currentMedia == null) || (wmp.currentMedia.imageSourceWidth == 0)) ? 800 : wmp.currentMedia.imageSourceWidth; }
		}

		/// <summary>
		/// 動画の高さ
		/// </summary>
		int IMoviePlayer.ImageHeight
		{
			get { return ((wmp.currentMedia == null) || (wmp.currentMedia.imageSourceHeight == 0)) ? 600 : wmp.currentMedia.imageSourceHeight; }
		}

		/// <summary>
		/// 音量変更イベント
		/// </summary>
		event EventHandler IMoviePlayer.VolumeChange
		{
			add { volumeChange += value; }
			remove { volumeChange -= value; }
		}
		event EventHandler volumeChange = delegate { };

		/// <summary>
		/// 動画再生開始イベント
		/// </summary>
		event EventHandler IMoviePlayer.MovieStart
		{
			add { movieStart += value; }
			remove { movieStart -= value; }
		}
		event EventHandler movieStart = delegate { };

		/// <summary>
		/// マウス押下イベント
		/// </summary>
		event AxWMPLib._WMPOCXEvents_MouseDownEventHandler IMoviePlayer.MouseDownEvent
		{
			add { mouseDownEvent += value; }
			remove { mouseDownEvent -= value; }
		}
		event AxWMPLib._WMPOCXEvents_MouseDownEventHandler mouseDownEvent = delegate { };

		/// <summary>
		///  マウスアップイベント
		/// </summary>
		event AxWMPLib._WMPOCXEvents_MouseUpEventHandler IMoviePlayer.MouseUpEvent
		{
			add { wmp.MouseUpEvent += value; }
			remove { wmp.MouseUpEvent -= value; }
		}

		/// <summary>
		///  マウス移動イベント
		/// </summary>
		event AxWMPLib._WMPOCXEvents_MouseMoveEventHandler IMoviePlayer.MouseMoveEvent
		{
			add { wmp.MouseMoveEvent += value; }
			remove { wmp.MouseMoveEvent -= value; }
		}

		/// <summary>
		/// ダブルクリックイベント
		/// </summary>
		event AxWMPLib._WMPOCXEvents_DoubleClickEventHandler IMoviePlayer.DoubleClickEvent
		{
			add { doubleClickEvent += value; }
			remove { doubleClickEvent -= value; }
		}
		event AxWMPLib._WMPOCXEvents_DoubleClickEventHandler doubleClickEvent = delegate { };

		/// <summary>
		/// キー押下イベント
		/// </summary>
		event AxWMPLib._WMPOCXEvents_KeyDownEventHandler IMoviePlayer.KeyDownEvent
		{
			add { keyDownEvent += value; }
			remove { keyDownEvent -= value; }
		}
		event AxWMPLib._WMPOCXEvents_KeyDownEventHandler keyDownEvent = delegate { };

		/// <summary>
		/// WMPのハンドル
		/// </summary>
		IntPtr IMoviePlayer.WMPHandle { get { return wmp.Handle; } }

		/// <summary>
		/// コンテキストメニューの有効
		/// </summary>
		bool IMoviePlayer.EnableContextMenu
		{
			get { return wmp.enableContextMenu; }
			set { try { wmp.enableContextMenu = value; } catch { } }
		}

		/// <summary>
		/// 現在のフレームレート
		/// </summary>
		int IMoviePlayer.NowFrameRate { get { try { return wmp.network.frameRate / 100; } catch { return 0; } } }

		/// <summary>
		/// フレームレート
		/// </summary>
		int IMoviePlayer.FrameRate { get { return wmp.network.encodedFrameRate; } }

		/// <summary>
		/// 現在のビットレート
		/// </summary>
		int IMoviePlayer.NowBitrate { get { return wmp.network.bandWidth / 1000; } }

		/// <summary>
		/// ビットレート
		/// </summary>
		int IMoviePlayer.Bitrate { get { return wmp.network.bitRate / 1000; } }

		/// <summary>
		/// 動画コントロール
		/// </summary>
		Control IMoviePlayer.MovieControl { get { return wmp; } }

		/// <summary>
		/// 初回ファイルオープンフラグ(MovieStartに使用)
		/// </summary>
		private bool isFirstMediaOpen = true;

		/// <summary>
		/// 動画再生
		/// </summary>
		/// <param name="streamUrl">ストリームURL</param>
		void IMoviePlayer.PlayMoive(string streamUrl)
		{
			wmp.URL = streamUrl;

			// 再生変更イベント
			wmp.OpenStateChange += (sender, e) =>
			{
				Logger.Instance.Debug(String.Format("OpenStateChange [{0}]", wmp.openState.ToString()));
				
				// 動画切替時に、ミュートが解除されるための対応
				((IMoviePlayer)this).Mute = isMute;

				// 動画再生開始イベント
				if ((wmp.openState == WMPOpenState.wmposMediaOpen) && isFirstMediaOpen)
				{
					isFirstMediaOpen = false;
					movieStart(this, new EventArgs());
				}
			};
		}

		/// <summary>
		/// 再接続(プレイヤー)
		/// </summary>
		void IMoviePlayer.Retry()
		{
			wmp.URL = wmp.URL;
		}
	}
}
