using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxWMPLib;
using LibVlcWrapper;
using Microsoft.Win32;
using PeerstLib.Util;
using PeerstPlayer.Forms.Player;
using WMPLib;
using _WMPOCXEvents_DoubleClickEventHandler = AxWMPLib._WMPOCXEvents_DoubleClickEventHandler;
using _WMPOCXEvents_KeyDownEventHandler = AxWMPLib._WMPOCXEvents_KeyDownEventHandler;
using _WMPOCXEvents_MouseDownEventHandler = AxWMPLib._WMPOCXEvents_MouseDownEventHandler;
using _WMPOCXEvents_MouseMoveEventHandler = AxWMPLib._WMPOCXEvents_MouseMoveEventHandler;
using _WMPOCXEvents_MouseUpEventHandler = AxWMPLib._WMPOCXEvents_MouseUpEventHandler;

namespace PeerstPlayer.Controls.MoviePlayer
{
	public partial class VlcMediaPlayerControl : UserControl, IMoviePlayer
	{
		private string playlistUrl;
		private float lastBuffering;
		private Timer timer;
		private VlcNativeWindow nativeWindow;

		public VlcMediaPlayerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			nativeWindow = new VlcNativeWindow(vlcControl.Handle);
			nativeWindow.MouseDownEvent += (sender, e) => mouseDownEvent(this, e);
			nativeWindow.MouseUpEvent += (sender, e) => mouseUpEvent(this, e);
			nativeWindow.MouseMoveEvent += (sender, e) => mouseMoveEvent(this, e);
			nativeWindow.KeyDownEvent += (sender, e) => keyDownEvent(this, e);

			timer = new Timer();
			timer.Interval = 1;
			timer.Tick += (sender, args) =>
			{
				// 再生が始まってからじゃないと音量変更できないので音量が変更できるまで続ける
				((IMoviePlayer)this).Volume = volume;
				if (vlcControl.Volume == volume)
				{
					timer.Stop();
				}
			};

			vlcControl.Buffering += (sender, e) => lastBuffering = e.NewCache;
			vlcControl.Playing += (sender, e) =>
			{
				movieStart(this, new EventArgs());
				volumeChange(this, new EventArgs());
				timer.Start();
			};
			vlcControl.EndReached += (sender, e) => ((IMoviePlayer)this).PlayMoive(playlistUrl);
			vlcControl.MouseDown += (sender, e) => mouseDownEvent(this, new _WMPOCXEvents_MouseDownEvent((short)((int)e.Button >> 20), 0, e.X, e.Y));
			vlcControl.MouseUp += (sender, e) => mouseUpEvent(this, new _WMPOCXEvents_MouseUpEvent((short)((int)e.Button >> 20), 0, e.X, e.Y));
			vlcControl.MouseMove += (sender, e) => mouseMoveEvent(this, new _WMPOCXEvents_MouseMoveEvent((short)((int)e.Button >> 20), 0, e.X, e.Y));
			vlcControl.MouseDoubleClick += (sender, e) => doubleClickEvent(this, new _WMPOCXEvents_DoubleClickEvent((short)((int)e.Button >> 20), 0, e.X, e.Y));
		}

		private void VlcMediaPlayerControl_Load(object sender, EventArgs e)
		{
			try
			{
				vlcControl.InitializeVlc(new DirectoryInfo(PlayerSettings.VlcFolder));
			}
			catch (Exception)
			{
				var registry = Registry.LocalMachine.OpenSubKey(@"Software\VideoLAN\VLC\");
				if (registry == null)
				{
					// 64bitOSの時に、32bitVLCがインストールされている場合は別のメッセージを出す
					if (IntPtr.Size == 8)
					{
						registry = Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\VideoLAN\VLC\");
						if (registry != null)
						{
							MessageBox.Show("64bit版VLCが見つかりませんでした。\n64bit版をインストールする必要があります。",
								"ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
					}
					MessageBox.Show("VLCが見つかりませんでした。\nVLCをインストールする必要があります。",
						"ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				var directory = (string)registry.GetValue("InstallDir");
				vlcControl.InitializeVlc(new DirectoryInfo(directory));
				// ここで設定変えちゃう
				PlayerSettings.VlcFolder = directory;
			}
		}

		private int volume = 0;
		/// <summary>
		/// 音量
		/// </summary>
		int IMoviePlayer.Volume
		{
			get { return vlcControl.Volume == -1 ? volume : vlcControl.Volume; }
			set
			{
				if (value < 0)
				{
					volume = 0;
				}
				else if (value > 100)
				{
					volume = 100;
				}
				else
				{
					volume = value;
				}

				// ミュートを解除する
				vlcControl.Mute = false;
				vlcControl.Volume = volume;
				volumeChange(this, new EventArgs());
			}
		}

		/// <summary>
		/// 音量バランス
		/// </summary>
		int IMoviePlayer.VolumeBalance
		{
			get { return 0; }
			set { }
		}

		/// <summary>
		/// ミュート
		/// </summary>
		bool IMoviePlayer.Mute
		{
			get { return vlcControl.Mute; }
			set
			{
				vlcControl.Mute = value;
				volumeChange(this, new EventArgs());
			}
		}

		/// <summary>
		/// 再生時間
		/// </summary>
		string IMoviePlayer.Duration
		{
			get { return new DateTime().Add(vlcControl.Time).ToString("HH:mm:ss"); }
		}

		/// <summary>
		/// バッファー率
		/// </summary>
		int IMoviePlayer.BufferingProgress
		{
			get { return (int)lastBuffering; }
		}

		/// <summary>
		/// 再生状態
		/// </summary>
		WMPPlayState IMoviePlayer.PlayState
		{
			get
			{
				switch (vlcControl.State)
				{
				case MediaStates.Playing:
					return WMPPlayState.wmppsPlaying;
				case MediaStates.Buffering:
					return WMPPlayState.wmppsBuffering;
				case MediaStates.Stopped:
				case MediaStates.Error:
					return WMPPlayState.wmppsStopped;
				case MediaStates.Ended:
					return WMPPlayState.wmppsMediaEnded;
				case MediaStates.Paused:
					return WMPPlayState.wmppsPaused;
				}
				return WMPPlayState.wmppsPlaying;
			}
		}

		/// <summary>
		/// 動画再生状態
		/// </summary>
		WMPOpenState IMoviePlayer.OpenState
		{
			get
			{
				switch (vlcControl.State)
				{
				case MediaStates.Opening:
					return WMPOpenState.wmposMediaOpen;
				}
				return WMPOpenState.wmposMediaConnecting;
			}
		}

		/// <summary>
		/// アスペクト比
		/// </summary>
		float IMoviePlayer.AspectRate
		{
			get
			{
				return ((IMoviePlayer)this).ImageWidth / (float)((IMoviePlayer)this).ImageHeight;
			}
		}

		/// <summary>
		/// 動画の幅
		/// </summary>
		int IMoviePlayer.ImageWidth
		{
			get { return vlcControl.VideoSize.Width == 0 ? 800 : vlcControl.VideoSize.Width; }
		}

		/// <summary>
		/// 動画の高さ
		/// </summary>
		int IMoviePlayer.ImageHeight
		{
			get { return vlcControl.VideoSize.Height == 0 ? 600 : vlcControl.VideoSize.Height; }
		}

		/// <summary>
		/// 音量変更イベント
		/// </summary>
		event EventHandler IMoviePlayer.VolumeChange
		{
			add { volumeChange += value; }
			remove { volumeChange -= value; }
		}
		private event EventHandler volumeChange = delegate { };

		/// <summary>
		/// 動画再生開始イベント
		/// </summary>
		event EventHandler IMoviePlayer.MovieStart
		{
			add { movieStart += value; }
			remove { movieStart -= value; }
		}
		private event EventHandler movieStart = delegate { };

		/// <summary>
		/// マウス押下イベント
		/// </summary>
		event _WMPOCXEvents_MouseDownEventHandler IMoviePlayer.MouseDownEvent
		{
			add { mouseDownEvent += value; }
			remove { mouseDownEvent -= value; }
		}
		private event _WMPOCXEvents_MouseDownEventHandler mouseDownEvent = delegate { };

		/// <summary>
		///  マウスアップイベント
		/// </summary>
		event _WMPOCXEvents_MouseUpEventHandler IMoviePlayer.MouseUpEvent
		{
			add { mouseUpEvent += value; }
			remove { mouseUpEvent -= value; }
		}
		private event _WMPOCXEvents_MouseUpEventHandler mouseUpEvent = delegate { };

		/// <summary>
		///  マウス移動イベント
		/// </summary>
		event _WMPOCXEvents_MouseMoveEventHandler IMoviePlayer.MouseMoveEvent
		{
			add { mouseMoveEvent += value; }
			remove { mouseMoveEvent -= value; }
		}
		event _WMPOCXEvents_MouseMoveEventHandler mouseMoveEvent = delegate { };

		/// <summary>
		/// ダブルクリックイベント
		/// </summary>
		event _WMPOCXEvents_DoubleClickEventHandler IMoviePlayer.DoubleClickEvent
		{
			add { doubleClickEvent += value; }
			remove { doubleClickEvent -= value; }
		}
		event _WMPOCXEvents_DoubleClickEventHandler doubleClickEvent = delegate { };

		/// <summary>
		/// キー押下イベント
		/// </summary>
		event _WMPOCXEvents_KeyDownEventHandler IMoviePlayer.KeyDownEvent
		{
			add { keyDownEvent += value; }
			remove { keyDownEvent -= value; }
		}
		event _WMPOCXEvents_KeyDownEventHandler keyDownEvent = delegate { };

		/// <summary>
		/// WMPのハンドル
		/// </summary>
		IntPtr IMoviePlayer.WMPHandle { get { return IntPtr.Zero; } }

		/// <summary>
		/// コンテキストメニューの有効
		/// </summary>
		bool IMoviePlayer.EnableContextMenu
		{
			get { return false; }
			set { }
		}

		private int previousDecodedVideo;
		private int nextTick;
		/// <summary>
		/// 現在のフレームレート
		/// </summary>
		int IMoviePlayer.NowFrameRate
		{
			get
			{
				if (nextTick < Environment.TickCount)
				{
					var fps = vlcControl.Stats.DecodedVideo - previousDecodedVideo;
					previousDecodedVideo = vlcControl.Stats.DecodedVideo;
					nextTick = Environment.TickCount + 1000;
					return fps;					
				}
				return vlcControl.Stats.DecodedVideo - previousDecodedVideo;
			}
		}

		/// <summary>
		/// フレームレート
		/// </summary>
		int IMoviePlayer.FrameRate
		{
			get
			{
				return vlcControl.Stats.DecodedVideo - previousDecodedVideo;
			}
		}

		/// <summary>
		/// 現在のビットレート
		/// </summary>
		int IMoviePlayer.NowBitrate
		{
			get { return (int)(vlcControl.Stats.inputBitrate * 10000); }
		}

		/// <summary>
		/// ビットレート
		/// </summary>
		int IMoviePlayer.Bitrate
		{
			get { return (int)vlcControl.GetTracks().Sum(x => x.Bitrate); }
		}

		/// <summary>
		/// 動画コントロール
		/// </summary>
		Control IMoviePlayer.MovieControl
		{
			get { return this; }
		}

		/// <summary>
		/// 動画再生
		/// </summary>
		/// <param name="streamUrl">ストリームURL</param>
		void IMoviePlayer.PlayMoive(string streamUrl)
		{
			playlistUrl = streamUrl;
			try
			{
				vlcControl.SetMedia(new Uri(WebUtil.GetHtml(streamUrl, Encoding.UTF8)));
				vlcControl.Play();
			}
			catch (Exception)
			{
			}
		}


	}
}
