using System;
using System.Windows.Forms;
using PeerstLib.Controls;
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

		/// <summary>
		/// 動画再生開始イベント
		/// </summary>
		event EventHandler IMoviePlayer.MovieStart
		{
			add { movieStart += value; }
			remove { movieStart -= value; }
		}
		event EventHandler movieStart = delegate { };

		bool IMoviePlayer.Mute
		{
			get { return false; }
			set { volumeChange(this, new EventArgs()); }
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
						case "RightDownEvent":
							mouseDownEvent(this, new AxWMPLib._WMPOCXEvents_MouseDownEvent((short)Keys.RButton, 0, x, y));
							break;
						case "MouseUpEvent":
							mouseUpEvent(this, new AxWMPLib._WMPOCXEvents_MouseUpEvent((short)Keys.LButton, 0, x, y));
							break;
						case "RightUpEvent":
							mouseUpEvent(this, new AxWMPLib._WMPOCXEvents_MouseUpEvent((short)Keys.RButton, 0, x, y));
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
			flashManager.OpenStateChange += (sender, args) =>
			{
				// 動画再生開始イベント
				if (isFirstMediaOpen)
				{
					var width = ((IMoviePlayer)this).ImageWidth;
					var height = ((IMoviePlayer)this).ImageHeight;
					axShockwaveFlash.Width = width;
					axShockwaveFlash.Height = height;
					isFirstMediaOpen = false;
					movieStart(this, new EventArgs());
				}
			};
		}

		public void RaiseOnMouseDown(MouseButtons mouseButtons, int clicks, int x, int y, int delta)
		{
			mouseDownEvent(this, new AxWMPLib._WMPOCXEvents_MouseDownEvent((short)Keys.LButton, 0, x, y));
		}

		public void RaiseDoubleClick()
		{
			doubleClickEvent(this, new EventArgs());
		}
	}

	public class ShockwaveFlashWrapper : AxShockwaveFlashObjects.AxShockwaveFlash
	{
		private bool dbFlag = false;

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case (int)WindowMessage.WM_LBUTTONDOWN:
					(Parent as FlashMoviePlayerControl).RaiseOnMouseDown(MouseButtons.Left, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16, 0);
					return;
				case (int)WindowMessage.WM_LBUTTONDBLCLK:
					// ここで処理すると2回目のLBUTTONDOWN時に処理されてしまい、
					// 挙動が少し変わってしまうのでフラグを立ててWM_LBUTTONUPで処理する
					dbFlag = true;
					break;
				case (int)WindowMessage.WM_LBUTTONUP:
					if (dbFlag)
					{
						(Parent as FlashMoviePlayerControl).RaiseDoubleClick();
						dbFlag = false;
					}
					break;
			}
			base.WndProc(ref m);
		}
	}
}
