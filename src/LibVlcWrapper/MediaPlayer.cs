using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using LibVlcWrapper.Structures;

namespace LibVlcWrapper
{
	public class MediaPlayer : IDisposable
	{
		public LibVlcMediaPlayer Handle { get; private set; }

		/// <summary>
		/// 現在のメディアを取得します。
		/// </summary>
		public Media CurrentMedia
		{
			get { return media; }
		}

		/// <summary>
		/// 現在再生中かどうか。
		/// </summary>
		public bool IsPlaying
		{
			get { return vlc.Manager.libvlc_media_player_is_playingDelegate(Handle); }
		}

		/// <summary>
		/// メディアプレイヤーのハンドルを取得または設定します。
		/// </summary>
		public IntPtr WindowHandle
		{
			get
			{
				return vlc.Manager.libvlc_media_player_get_hwndDelegate(Handle);
			}
			set { vlc.Manager.libvlc_media_player_set_hwndDelegate(Handle, value); }
		}

		/// <summary>
		/// 現在の動画の長さ(ms)を取得します。
		/// </summary>
		public long Length
		{
			get { return vlc.Manager.libvlc_media_player_get_lengthDelegate(Handle); }
		}

		/// <summary>
		/// 現在の動画の時間を取得または設定します。
		/// </summary>
		public long Time
		{
			get { return vlc.Manager.libvlc_media_player_get_timeDelegate(Handle); }
			set { vlc.Manager.libvlc_media_player_set_timeDelegate(Handle, value); }
		}

		/// <summary>
		/// 現在の動画の再生速度を取得または設定します。
		/// </summary>
		public float Playrate
		{
			get { return vlc.Manager.libvlc_media_player_get_rateDelegate(Handle); }
			set { vlc.Manager.libvlc_media_player_set_rateDelegate(Handle, value); }
		}

		/// <summary>
		/// 現在の動画のFPSを取得します。
		/// </summary>
		public float Fps
		{
			get { return vlc.Manager.libvlc_media_player_get_fpsDelegate(Handle); }
		}

		/// <summary>
		/// ミュート状態を取得または設定します。
		/// </summary>
		public bool Mute
		{
			get { return vlc.Manager.libvlc_audio_get_muteDelegate(Handle); }
			set { vlc.Manager.libvlc_audio_set_muteDelegate(Handle, value); }
		}

		/// <summary>
		/// 音量を取得または設定します。
		/// </summary>
		public int Volume
		{
			get { return vlc.Manager.libvlc_audio_get_volumeDelegate(Handle); }
			set { vlc.Manager.libvlc_audio_set_volumeDelegate(Handle, value); }
		}

		/// <summary>
		/// 現在のビデオの横幅を取得します。
		/// </summary>
		public int VideoWidth
		{
			get { return vlc.Manager.libvlc_video_get_widthDelegate(Handle); }
		}

		/// <summary>
		/// 現在のビデオの縦幅を取得します。
		/// </summary>
		public int VideoHeight
		{
			get { return vlc.Manager.libvlc_video_get_heightDelegate(Handle); }
		}

		/// <summary>
		/// 現在の動画のアスペクト比を取得または設定します。
		/// </summary>
		public float AspectRatio
		{
			get { return vlc.Manager.libvlc_video_get_aspect_ratioDelegate(Handle); }
			set { vlc.Manager.libvlc_video_set_aspect_ratioDelegate(Handle, value); }
		}

		public event EventHandler<MediaPlayerMediaChangedEventArgs> MediaChanged = delegate { };
		public event EventHandler<EventArgs> NothingSpecial = delegate { };
		public event EventHandler<EventArgs> Opening = delegate { };
		public event EventHandler<MediaPlayerBufferingEventArgs> Buffering = delegate { };
		public event EventHandler<EventArgs> Playing = delegate { };
		public event EventHandler<EventArgs> Paused = delegate { };
		public event EventHandler<EventArgs> Stopped = delegate { };
		public event EventHandler<EventArgs> Forward = delegate { };
		public event EventHandler<EventArgs> Backward = delegate { };
		public event EventHandler<EventArgs> EndReached = delegate { };
		public event EventHandler<EventArgs> EncounteredError = delegate { };
		public event EventHandler<MediaPlayerTimeChangedEventArgs> TimeChanged = delegate { };
		public event EventHandler<MediaPlayerPositionChangedEventArgs> PositionChanged = delegate { };
		public event EventHandler<MediaPlayerSeekableChangedEventArgs> SeekableChanged = delegate { };
		public event EventHandler<MediaPlayerPausableChangedEventArgs> PausableChanged = delegate { };
		public event EventHandler<MediaPlayerTitleChangedEventArgs> TitleChanged = delegate { };
		public event EventHandler<MediaPlayerSnapshotTokenEventArgs> SnapshotTaken = delegate { };
		public event EventHandler<MediaPlayerLengthChangedEventArgs> LengthChanged = delegate { };
		public event EventHandler<MediaPlayerVoutEventArgs> Vout = delegate { };
		public event EventHandler<MediaPlayerScrambleChangedEventArgs> ScrambledChanged = delegate { };

		private readonly LibVlc vlc;
		private Media media;
		private LibVlcCallback onMediaChanged;
		private LibVlcCallback onNothingSpecial;
		private LibVlcCallback onOpening;
		private LibVlcCallback onBuffering;
		private LibVlcCallback onPlaying;
		private LibVlcCallback onPaused;
		private LibVlcCallback onStopped;
		private LibVlcCallback onForward;
		private LibVlcCallback onBackward;
		private LibVlcCallback onEndReached;
		private LibVlcCallback onEncounteredError;
		private LibVlcCallback onTimeChanged;
		private LibVlcCallback onPositionChanged;
		private LibVlcCallback onSeekableChanged;
		private LibVlcCallback onPausableChanged;
		private LibVlcCallback onTitleChanged;
		private LibVlcCallback onSnapshotTaken;
		private LibVlcCallback onLengthChanged;
		private LibVlcCallback onVout;
		private LibVlcCallback onScrambledChanged;

		public MediaPlayer(LibVlc vlc)
		{
			this.vlc = vlc;
			Handle = vlc.Manager.libvlc_media_player_newDelegate(vlc.Handle);
			AttachEvents();
		}

		~MediaPlayer()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
 			if (Handle.Pointer == IntPtr.Zero) return;
 			DetachEvents();
 			vlc.Manager.libvlc_media_player_releaseDelegate(Handle);
			GC.SuppressFinalize(this);
		}

		public void SetMedia(Media media)
		{
			if (media == null) throw new ArgumentException("media");
			this.media = media;
			vlc.Manager.libvlc_media_player_set_mediaDelegate(Handle, media.Handle);
		}

		public void Play()
		{
			if (media == null) return;
			vlc.Manager.libvlc_media_player_playDelegate(Handle);
		}

		public void Pause()
		{
			if (media == null) return;
			vlc.Manager.libvlc_media_player_pauseDelegate(Handle);
		}

		public void Stop()
		{
			if (media == null) return;
			vlc.Manager.libvlc_media_player_stopDelegate(Handle);
			media.Dispose();
			media = null;
		}

		public void ToggleMute()
		{
			vlc.Manager.libvlc_audio_toggle_muteDelegate(Handle);
		}

		private void AttachEvents()
		{
			var eventManager = vlc.Manager.libvlc_media_player_event_managerDelegate(Handle);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerMediaChanged, onMediaChanged = OnMediaChanged);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerNothingSpecial, onNothingSpecial = OnNothingSpecial);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerOpening, onOpening = OnOpening);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerBuffering, onBuffering = OnBuffering);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerPlaying, onPlaying = OnPlaying);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerPaused, onPaused = OnPaused);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerStopped, onStopped = OnStopped);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerForward, onForward = OnForward);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerBackward, onBackward = OnBackward);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerEndReached, onEndReached = OnEndReached);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerEncounteredError, onEncounteredError = OnEncounteredError);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerTimeChanged, onTimeChanged = OnTimeChanged);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerPositionChanged, onPositionChanged = OnPositionChanged);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerSeekableChanged, onSeekableChanged = OnSeekableChanged);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerPausableChanged, onPausableChanged = OnPausableChanged);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerTitleChanged, onTitleChanged = OnTitleChanged);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerSnapshotTaken, onSnapshotTaken = OnSnapshotTaken);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerLengthChanged, onLengthChanged = OnLengthChanged);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerVout, onVout = OnVout);
			vlc.AttachEvent(eventManager, EventTypes.MediaPlayerScrambledChanged, onScrambledChanged = OnScrambledChanged);
		}

		private void DetachEvents()
		{
			var eventManager = vlc.Manager.libvlc_media_player_event_managerDelegate(Handle);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerMediaChanged, onMediaChanged);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerNothingSpecial, onNothingSpecial);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerOpening, onOpening);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerBuffering, onBuffering);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerPlaying, onPlaying);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerPaused, onPaused);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerStopped, onStopped);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerForward, onForward);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerBackward, onBackward);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerEndReached, onEndReached);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerEncounteredError, onEncounteredError);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerTimeChanged, onTimeChanged);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerPositionChanged, onPositionChanged);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerSeekableChanged, onSeekableChanged);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerPausableChanged, onPausableChanged);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerTitleChanged, onTitleChanged);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerSnapshotTaken, onSnapshotTaken);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerLengthChanged, onLengthChanged);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerVout, onVout);
			vlc.DetachEvent(eventManager, EventTypes.MediaPlayerScrambledChanged, onScrambledChanged);
		}

		public void OnMediaChanged(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			MediaChanged(this, new MediaPlayerMediaChangedEventArgs(eventArgs.Union.MediaPlayerMediaChanged.NewMedia));
		}

		public void OnNothingSpecial(IntPtr ptr)
		{
			NothingSpecial(this, new EventArgs());
		}

		public void OnOpening(IntPtr ptr)
		{
			Opening(this, new EventArgs());
		}

		public void OnBuffering(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			Buffering(this, new MediaPlayerBufferingEventArgs(eventArgs.Union.MediaPlayerBuffering.NewCache));
		}

		public void OnPlaying(IntPtr ptr)
		{
			Playing(this, new EventArgs());
		}

		public void OnPaused(IntPtr ptr)
		{
			Paused(this, new EventArgs());
		}

		public void OnStopped(IntPtr ptr)
		{
			Stopped(this, new EventArgs());
		}

		public void OnForward(IntPtr ptr)
		{
			Forward(this, new EventArgs());
		}

		public void OnBackward(IntPtr ptr)
		{
			Backward(this, new EventArgs());
		}

		public void OnEndReached(IntPtr ptr)
		{
			EndReached(this, new EventArgs());
		}

		public void OnEncounteredError(IntPtr ptr)
		{
			EncounteredError(this, new EventArgs());
		}

		public void OnTimeChanged(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			TimeChanged(this, new MediaPlayerTimeChangedEventArgs(eventArgs.Union.MediaPlayerTimeChanged.NewTime));
		}

		public void OnPositionChanged(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			PositionChanged(this, new MediaPlayerPositionChangedEventArgs(eventArgs.Union.MediaPlayerPositionChanged.NewPosition));
		}

		public void OnSeekableChanged(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			SeekableChanged(this, new MediaPlayerSeekableChangedEventArgs(eventArgs.Union.MediaPlayerSeekableChanged.NewSeekable));
		}

		public void OnPausableChanged(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			PausableChanged(this, new MediaPlayerPausableChangedEventArgs(eventArgs.Union.MediaPlayerPausableChanged.NewPausable));
		}

		public void OnTitleChanged(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			TitleChanged(this, new MediaPlayerTitleChangedEventArgs(eventArgs.Union.MediaPlayerTitleChanged.NewTitle));
		}

		public void OnSnapshotTaken(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			var fileName = Marshal.PtrToStringAnsi(eventArgs.Union.MediaPlayerSnapshotToken.FileName);
			SnapshotTaken(this, new MediaPlayerSnapshotTokenEventArgs(fileName));
		}

		public void OnLengthChanged(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			LengthChanged(this, new MediaPlayerLengthChangedEventArgs(eventArgs.Union.MediaPlayerLengthChanged.NewLength));
		}

		public void OnVout(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			Vout(this, new MediaPlayerVoutEventArgs(eventArgs.Union.MediaPlayerNewVout.NewCount));
		}

		public void OnScrambledChanged(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			ScrambledChanged(this, new MediaPlayerScrambleChangedEventArgs(eventArgs.Union.MediaPlayerScrambledChanged.NewScrambled));
		}
	}
}
