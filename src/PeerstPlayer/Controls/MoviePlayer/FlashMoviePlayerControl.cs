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
		public FlashMoviePlayerControl()
		{
			InitializeComponent();

			MouseDown+= (e, sender) =>
			{
				mouseDownEvent(this, new AxWMPLib._WMPOCXEvents_MouseDownEvent(0 ,0, 0, 0));
			};
		}

		int IMoviePlayer.Volume
		{
			get { return 0; }
			set { }
		}

		int IMoviePlayer.VolumeBalance
		{
			get { return 0; }
			set { }
		}

		event EventHandler IMoviePlayer.VolumeChange
		{
			add { }
			remove { }
		}

		event EventHandler IMoviePlayer.MovieStart
		{
			add { }
			remove { }
		}

		bool IMoviePlayer.Mute
		{
			get { return false; }
			set { }
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

		event AxWMPLib._WMPOCXEvents_MouseDownEventHandler mouseDownEvent;
		event AxWMPLib._WMPOCXEvents_MouseDownEventHandler IMoviePlayer.MouseDownEvent
		{
			add { mouseDownEvent += value; }
			remove { mouseDownEvent -= value; }
		}

		event AxWMPLib._WMPOCXEvents_MouseUpEventHandler IMoviePlayer.MouseUpEvent
		{
			add { }
			remove { }
		}

		event AxWMPLib._WMPOCXEvents_MouseMoveEventHandler IMoviePlayer.MouseMoveEvent
		{
			add { }
			remove { }
		}

		event EventHandler IMoviePlayer.DoubleClickEvent
		{
			add { }
			remove { }
		}

		event AxWMPLib._WMPOCXEvents_KeyDownEventHandler IMoviePlayer.KeyDownEvent
		{
			add { }
			remove { }
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
			get { return new Control(); }
		}

		void IMoviePlayer.PlayMoive(string streamUrl)
		{
		}
	}
}
