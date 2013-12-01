using System;
using System.Windows.Forms;
using WMPLib;

namespace PeerstPlayer.Controls.MoviePlayer
{
	/// <summary>
	/// Flash動画プレイヤーコントロール
	/// </summary>
	public partial class FlashMoviePlayerControl : UserControl, IMoviePlayer
	{
		private FlashMoviePlayerManager flashManager = null;

		public FlashMoviePlayerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			// FlashManagerの初期化
			flashManager = new FlashMoviePlayerManager(axShockwaveFlash);
		}

		int volume = 0;
		int IMoviePlayer.Volume
		{
			get { return volume; }
			set
			{
				if ((value < 0) || (100 < value))
				{
					return;
				}
				volume = value;
				flashManager.ChangeVolume(value);
				volumeChange(this, new EventArgs());
			}
		}

		int IMoviePlayer.VolumeBalance
		{
			get { return 0; }
			set { }
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

		event EventHandler IMoviePlayer.MovieStart
		{
			add { }
			remove { }
		}

		bool IMoviePlayer.Mute
		{
			get { return false; }
			set { volumeChange(this, new EventArgs()); }
		}

		string IMoviePlayer.Duration
		{
			get { return String.Empty; }
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
			get { return 1.0f; }
		}

		int IMoviePlayer.ImageWidth
		{
			get { return 800; }
		}

		int IMoviePlayer.ImageHeight
		{
			get { return 600; }
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

		event EventHandler doubleClickEvent = delegate { };
		event EventHandler IMoviePlayer.DoubleClickEvent
		{
			add { doubleClickEvent += value; }
			remove { doubleClickEvent -= value; }
		}

		event AxWMPLib._WMPOCXEvents_KeyDownEventHandler IMoviePlayer.KeyDownEvent
		{
			add { keyDownEvent += value; }
			remove { keyDownEvent -= value; }
		}
		event AxWMPLib._WMPOCXEvents_KeyDownEventHandler keyDownEvent = delegate { };

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
			get { return 0; }
		}

		int IMoviePlayer.FrameRate
		{
			get { return 0; }
		}

		int IMoviePlayer.NowBitrate
		{
			get { return 0; }
		}

		int IMoviePlayer.Bitrate
		{
			get { return 0; }
		}

		Control IMoviePlayer.MovieControl
		{
			get { return this; }
		}

		void IMoviePlayer.PlayMoive(string streamUrl)
		{
			axShockwaveFlash.FSCommand += (sender, e) =>
			{
				try
				{
					string[] arg = e.args.Split(new string[] { "," }, StringSplitOptions.None);
					int x = 0;
					int y = 0;
					int.TryParse(arg[0], out x);
					int.TryParse(arg[1], out y);

					switch (e.command)
					{
						case "MouseDownEvent":
							mouseDownEvent(this, new AxWMPLib._WMPOCXEvents_MouseDownEvent((short)Keys.LButton, 0, x, y));
							break;
						case "MouseUpEvent":
							mouseUpEvent(this, new AxWMPLib._WMPOCXEvents_MouseUpEvent(0, 0, x, y));
							break;
						case "MouseMoveEvent":
							mouseMoveEvent(this, new AxWMPLib._WMPOCXEvents_MouseMoveEvent(0, 0, x, y));
							break;
						case "DoubleClickEvent":
							doubleClickEvent(this, new EventArgs());
							break;
						case "KeyDownEvent":
							keyDownEvent(this, new AxWMPLib._WMPOCXEvents_KeyDownEvent((short)x, (short)y));
							break;
					}
				}
				catch
				{
				}
			};
			axShockwaveFlash.LoadMovie(0, Environment.CurrentDirectory + "/FlvPlayer.swf");
			flashManager.PlayVideo(streamUrl);
		}
	}
}
