using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibVlcWrapper.Structures;

namespace LibVlcWrapper
{
	public class VlcControl : Control
	{
		private LibVlc vlc;
		private MediaPlayer mediaPlayer;
		private MouseHook mouseHook;

		/// <summary>
		/// 現在再生中かどうか。
		/// </summary>
		[Browsable(false)]
		public bool IsPlaying
		{
			get
			{
				if (mediaPlayer == null) return false;
				return mediaPlayer.IsPlaying;
			}
		}

		[Browsable(false)]
		public float AspectRatio
		{
			get
			{
				if (mediaPlayer == null) return 0.0f;
				return mediaPlayer.AspectRatio;
			}
		}

		/// <summary>
		/// 現在のビデオのサイズを取得します。
		/// </summary>
		[Browsable(false)]
		public Size VideoSize
		{
			get
			{
				if (mediaPlayer == null) return new Size(0, 0);
				return new Size(mediaPlayer.VideoWidth, mediaPlayer.VideoHeight);
			}
		}

		/// <summary>
		/// メディアプレイヤーのハンドルを取得または設定します。
		/// </summary>
		[Browsable(false)]
		public IntPtr WindowHandle
		{
			get
			{
				if (mediaPlayer == null) return IntPtr.Zero;
				return mediaPlayer.WindowHandle;
			}
			set { mediaPlayer.WindowHandle = value; }
		}

		/// <summary>
		/// 現在の動画の長さ(ms)を取得します。
		/// </summary>
		[Browsable(false)]
		public long Length
		{
			get
			{
				if (mediaPlayer == null) return 0;
				return mediaPlayer.Length;
			}
		}

		/// <summary>
		/// 現在の動画の時間を取得または設定します。
		/// </summary>
		[Browsable(false)]
		public TimeSpan Time
		{
			get
			{
				if (mediaPlayer == null) return new TimeSpan(0);
				return new TimeSpan(mediaPlayer.Time * 10000);
			}
			set
			{
				if (mediaPlayer == null) return;
				mediaPlayer.Time = (long)value.TotalMilliseconds;
			}
		}

		/// <summary>
		/// 現在の動画のFPSを取得します。
		/// </summary>
		[Browsable(false)]
		public float Fps
		{
			get
			{
				if (mediaPlayer == null) return 0.0f;
				return mediaPlayer.Fps;
			}
		}

		/// <summary>
		/// ミュート状態を取得または設定します。
		/// </summary>
		[Browsable(false)]
		public bool Mute
		{
			get
			{
				if (mediaPlayer == null) return false;
				return mediaPlayer.Mute;
			}
			set
			{
				if (mediaPlayer == null) return;
				mediaPlayer.Mute = value;
			}
		}

		/// <summary>
		/// 音量を取得または設定します。
		/// </summary>
		[Browsable(false)]
		public int Volume
		{
			get
			{
				if (mediaPlayer == null) return -1;
				return mediaPlayer.Volume;
			}
			set
			{
				if (mediaPlayer == null) return;
				mediaPlayer.Volume = value;
			}
		}

		[Browsable(false)]
		public MediaStats Stats
		{
			get
			{
				if (mediaPlayer == null || mediaPlayer.CurrentMedia == null) return new MediaStats();
				return mediaPlayer.CurrentMedia.Stats;
			}
		}

		[Browsable(false)]
		public MediaStates State
		{
			get
			{
				if (mediaPlayer == null || mediaPlayer.CurrentMedia == null) return MediaStates.NothingSpecial;
				return mediaPlayer.CurrentMedia.State;
			}
		}

		public event EventHandler<MediaPlayerBufferingEventArgs> Buffering = delegate { };
		public event EventHandler<EventArgs> EndReached = delegate { };
		public event EventHandler<EventArgs> Playing = delegate { };
		public event EventHandler<MediaStateChangedEventArgs> OpenStateChange = delegate { }; 

		public VlcControl()
		{
			mouseHook = new MouseHook(this);
			mouseHook.MouseDownEvent += (sender, e) => BeginInvoke(new Action(() => OnMouseDown(e)));
			mouseHook.MouseUpEvent += (sender, e) => BeginInvoke(new Action(() => OnMouseUp(e)));
			mouseHook.MouseMoveEvent += (sender, e) => BeginInvoke(new Action(() => OnMouseMove(e)));
			mouseHook.DoubleClickEvent += (sender, e) => BeginInvoke(new Action(() => OnMouseDoubleClick(e)));

		}

		public void InitializeVlc(DirectoryInfo directoryInfo)
		{
			vlc = new LibVlc(directoryInfo);
			//vlc.CreateInstance(null);
			vlc.CreateInstance(new[] { "--vout=direct3d", "--loop" });
			//vlc.CreateInstance(new[] { "--vout=glwin32", "--loop" });

			mediaPlayer = vlc.CreateMediaPlayer();
			mediaPlayer.Buffering += (sender, args) => BeginInvoke(new Action(() => Buffering(this, args)));
			mediaPlayer.EndReached += (sender, args) => BeginInvoke(new Action(() => EndReached(this, args)));
			mediaPlayer.Playing += (sender, args) => BeginInvoke(new Action(() => Playing(this, args)));
		}

		public void SetMedia(FileInfo fileInfo)
		{
			var media = vlc.CreateMedia(fileInfo);
			mediaPlayer.SetMedia(media);
		}

		public void SetMedia(Uri uri)
		{
			var media = vlc.CreateMedia(uri);
			media.StateChanged += (sender, args) => BeginInvoke(new Action(() => OpenStateChange(this, args)));
			mediaPlayer.SetMedia(media);
		}

		public void Play()
		{
			if (mediaPlayer == null || mediaPlayer.CurrentMedia == null) return;
			WindowHandle = Handle;
			mediaPlayer.Play();
			mouseHook.Hook();
		}

		public void Pause()
		{
			if (mediaPlayer == null || mediaPlayer.CurrentMedia == null) return;
			mediaPlayer.Pause();
		}

		public void Stop()
		{
			if (mediaPlayer == null || mediaPlayer.CurrentMedia == null) return;
			mediaPlayer.Stop();
		}

		public IEnumerable<MediaTrack> GetTracks()
		{
			if (mediaPlayer == null || mediaPlayer.CurrentMedia == null) return new List<MediaTrack>();
			return mediaPlayer.CurrentMedia.GetTracks();
		}
	}
}
