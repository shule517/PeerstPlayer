using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PeerstLib.Controls;
using PeerstPlayer.Controls.PecaPlayer;
using PeerstPlayer.Forms.Player;
using WMPLib;
using PeerstLib.Util;
using System.Text;

namespace PeerstPlayer.Controls.MoviePlayer
{
	/// <summary>
	/// Flash動画プレイヤーコントロール
	/// </summary>
	public partial class FlashMoviePlayerControl : UserControl, IMoviePlayer
	{
		private FlashMoviePlayerManager flashManager = null;

		public FlashMoviePlayerControl(PecaPlayerControl parent)
		{
			try
			{
				InitializeComponent();
			}
			catch (COMException)
			{
				MessageBox.Show("Flash Playerがインストールされていません。\nInternet ExplorerからFlash Playerをインストールする必要があります。",
					"ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
			Dock = DockStyle.Fill;

            // FlashManagerの初期化
            flashManager = new FlashMoviePlayerManager(axShockwaveFlash);
            flashManager.Initialized += (sender, args) =>
            {
                flashManager.EnableGpu(PlayerSettings.Gpu);
            };
            flashManager.OpenStateChange += (sender, args) =>
            {
                if (isFirstMediaOpen)
                {
                    var width = ((IMoviePlayer)this).ImageWidth;
                    var height = ((IMoviePlayer)this).ImageHeight;
                    axShockwaveFlash.Width = width;
                    axShockwaveFlash.Height = height;
                    movieStart(this, new EventArgs());
                    isFirstMediaOpen = false;
                }
                flashManager.ChangeVolume(volume);
            };
            // プレイヤーからBump要求のイベント
			flashManager.RequestBump += (sender, args) => parent.Bump();
			// 再生支援を使う設定が変更されたら
			PlayerSettings.Changed += (s) =>
			{
				if (s == "Gpu")
				{
					flashManager.EnableGpu(PlayerSettings.Gpu);
				}
			};
			// Flashウィンドウをフックする
			FlashNativeWindow flash = new FlashNativeWindow(axShockwaveFlash);
			flash.MouseDownEvent += (sender, e) =>
			{
				mouseDownEvent(this, e);
				Focus();
			};
			flash.MouseUpEvent += (sender, e) => mouseUpEvent(this, e);
			flash.MouseMoveEvent += (sender, e) => mouseMoveEvent(this, e);
			flash.DoubleClickEvent += (sender, e) => doubleClickEvent(this, e);
			flash.KeyDownEvent += (sender, e) => keyDownEvent(this, e);
		}

		// 動画情報を表示
		public void ShowDebug()
		{
			flashManager.ShowDebug();
		}

		int volume = 0;
		int IMoviePlayer.Volume
		{
			get { return volume; }
			set
			{
				if (value < 0)
				{
					volume = 0;
				}
				else if (100 < value)
				{
					volume = 100;
				}
				else
				{
					volume = value;
				}

				// 音量変更したらミュートを解除
				mute = false;

				flashManager.ChangeVolume(volume);
				volumeChange(this, new EventArgs());
			}
		}

		private int volumeBalance = 0;
		int IMoviePlayer.VolumeBalance
		{
			get { return volumeBalance; }
			set
			{
				if (value < -100)
				{
					volumeBalance = -100;
				}
				else if (100 < value)
				{
					volumeBalance = 100;
				}
				else
				{
					volumeBalance = value;
				}

				flashManager.ChangePan(volumeBalance);
			}
		}

		/// <summary>
		/// 音量変化イベント
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

		private bool mute = false;
		bool IMoviePlayer.Mute
		{
			get { return mute; }
			set
			{
				mute = value;
				if (mute)
				{
					flashManager.ChangeVolume(0);
				}
				else
				{
					flashManager.ChangeVolume(volume);
				}
				volumeChange(this, new EventArgs());
			}
		}

		string IMoviePlayer.Duration
		{
			get { return flashManager.GetDurationString(); }
		}

		int IMoviePlayer.BufferingProgress
		{
			get { return 0; }
		}

		WMPLib.WMPPlayState IMoviePlayer.PlayState
		{
			get { return WMPPlayState.wmppsUndefined; }
		}

		WMPLib.WMPOpenState IMoviePlayer.OpenState
		{
			get { return WMPOpenState.wmposUndefined; }
		}

		float IMoviePlayer.AspectRate
		{
			get { return (float)((IMoviePlayer)this).ImageWidth / (float)((IMoviePlayer)this).ImageHeight; }
		}

		int IMoviePlayer.ImageWidth
		{
			get { return flashManager.GetVideoWidth() == 0 ? 800 : flashManager.GetVideoWidth(); }
		}

		int IMoviePlayer.ImageHeight
		{
			get { return flashManager.GetVideoHeight() == 0 ? 600 : flashManager.GetVideoHeight(); }
		}

		event AxWMPLib._WMPOCXEvents_MouseDownEventHandler mouseDownEvent = delegate { };
		event AxWMPLib._WMPOCXEvents_MouseDownEventHandler IMoviePlayer.MouseDownEvent
		{
			add { mouseDownEvent += value; }
			remove { mouseDownEvent -= value; }
		}

		event AxWMPLib._WMPOCXEvents_MouseUpEventHandler mouseUpEvent = delegate { };
		event AxWMPLib._WMPOCXEvents_MouseUpEventHandler IMoviePlayer.MouseUpEvent
		{
			add { mouseUpEvent += value; }
			remove { mouseUpEvent -= value; }
		}

		event AxWMPLib._WMPOCXEvents_MouseMoveEventHandler mouseMoveEvent = delegate { };
		event AxWMPLib._WMPOCXEvents_MouseMoveEventHandler IMoviePlayer.MouseMoveEvent
		{
			add { mouseMoveEvent += value; }
			remove { mouseMoveEvent += value; }
		}

		event AxWMPLib._WMPOCXEvents_DoubleClickEventHandler doubleClickEvent = delegate { };
		event AxWMPLib._WMPOCXEvents_DoubleClickEventHandler IMoviePlayer.DoubleClickEvent
		{
			add { doubleClickEvent += value; }
			remove { doubleClickEvent -= value; }
		}

		event AxWMPLib._WMPOCXEvents_KeyDownEventHandler keyDownEvent = delegate { };
		event AxWMPLib._WMPOCXEvents_KeyDownEventHandler IMoviePlayer.KeyDownEvent
		{
			add { keyDownEvent += value; }
			remove { keyDownEvent -= value; }
		}

		IntPtr IMoviePlayer.WMPHandle
		{
			get { return IntPtr.Zero; }
		}

		bool IMoviePlayer.EnableContextMenu
		{
			get { return false; }
			set { }
		}

		int IMoviePlayer.NowFrameRate
		{
			get { return flashManager.GetNowFrameRate(); }
		}

		int IMoviePlayer.FrameRate
		{
			get { return flashManager.GetFrameRate(); }
		}

		int IMoviePlayer.NowBitrate
		{
			get { return flashManager.GetNowBitRate(); }
		}

		int IMoviePlayer.Bitrate
		{
			get { return flashManager.GetBitRate(); }
		}

		Control IMoviePlayer.MovieControl
		{
			get { return this; }
		}

		/// <summary>
		/// 初回ファイルオープンフラグ(MovieStartに使用)
		/// </summary>
		private bool isFirstMediaOpen = true;

		void IMoviePlayer.PlayMoive(string streamUrl)
		{
            axShockwaveFlash.LoadMovie(0, FormUtility.GetExeFolderPath() + "/FlvPlayer.swf");
            flashManager.PlayVideo(streamUrl);
			isFirstMediaOpen = true;
        }
	}
}
