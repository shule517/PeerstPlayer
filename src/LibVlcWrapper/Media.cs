using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using LibVlcWrapper.Structures;

namespace LibVlcWrapper
{
	public class Media : IDisposable
	{
		public LibVlcMedia Handle { get; private set; }

		/// <summary>
		/// メディアの長さ(ms)を取得します。
		/// </summary>
		public long Duration
		{
			get { return vlc.Manager.libvlc_media_get_durationDelegate(Handle); }
		}

		/// <summary>
		/// メディアリソースロケータを取得します。
		/// </summary>
		public string Mrl
		{
			get { return vlc.Manager.libvlc_media_get_mrlDelegate(Handle); }
		}

		/// <summary>
		/// 状態を取得します。
		/// </summary>
		public MediaStates State
		{
			get { return vlc.Manager.libvlc_media_get_stateDelegate(Handle); }
		}

		/// <summary>
		/// 統計を取得します。
		/// </summary>
		public MediaStats Stats
		{
			get
			{
				IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MediaStats)));
				if (!vlc.Manager.libvlc_media_get_statsDelegate(Handle, ptr)) return new MediaStats();
				return (MediaStats)Marshal.PtrToStructure(ptr, typeof(MediaStats));
			}
		}

		public event EventHandler<MediaStateChangedEventArgs> StateChanged = delegate { };
		public event EventHandler<MediaMetaChangedEventArgs> MetaChanged = delegate { };
		public event EventHandler<MediaDurationChangedEventArgs> DurationChanged = delegate { };
		public event EventHandler<EventArgs> ParsedChanged = delegate { }; 

		private readonly LibVlc vlc;
		private LibVlcCallback onMediaStateChanged;
		private LibVlcCallback onMediaMetaChanged;
		private LibVlcCallback onMediaDurationChanged;
		private LibVlcCallback onMediaParsedChanged;

		internal Media(LibVlc vlc, FileInfo fileInfo)
		{
			this.vlc = vlc;
			Handle = vlc.Manager.libvlc_media_new_pathDelegate(vlc.Handle, fileInfo.FullName);
			AttachEvents();
		}

		internal Media(LibVlc vlc, Uri uri)
		{
			this.vlc = vlc;
			Handle = vlc.Manager.libvlc_media_new_locationDelegate(vlc.Handle, uri.ToString());
			AttachEvents();
		}

		~Media()
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
			vlc.Manager.libvlc_media_releaseDelegate(Handle);
			GC.SuppressFinalize(this);
		}

		public void AddOption(string option)
		{
			vlc.Manager.libvlc_media_add_optionDelegate(Handle, option);
		}

		public void Parse()
		{
			vlc.Manager.libvlc_media_parseDelegate(Handle);
		}

		public bool IsParsed()
		{
			return vlc.Manager.libvlc_media_is_parsedDelegate(Handle);
		}

		public IEnumerable<MediaTrack> GetTracks()
		{
			if (!IsParsed()) Parse();
			IntPtr tracks;
			var result = new List<MediaTrack>();
			var count = vlc.Manager.libvlc_media_tracks_getDelegate(Handle, out tracks);
			IntPtr ptr = tracks;
			for (uint i = 0; i < count; ++i)
			{
				var ptr2 = (IntPtr)Marshal.PtrToStructure(ptr, typeof(IntPtr));
				var track = (MediaTrack)Marshal.PtrToStructure(ptr2, typeof(MediaTrack));
				result.Add(track);
				ptr = new IntPtr((int)ptr + IntPtr.Size);
			}
			vlc.Manager.libvlc_media_tracks_releaseDelegate(tracks, count);
			return result;
		}

		private void AttachEvents()
		{
			var eventManager = vlc.Manager.libvlc_media_event_managerDelegate(Handle);
			vlc.AttachEvent(eventManager, EventTypes.MediaStateChanged, onMediaStateChanged = OnMediaStateChanged);
			vlc.AttachEvent(eventManager, EventTypes.MediaMetaChanged, onMediaMetaChanged = OnMediaMetaChanged);
			vlc.AttachEvent(eventManager, EventTypes.MediaDurationChanged, onMediaDurationChanged = OnMediaDurationChanged);
			vlc.AttachEvent(eventManager, EventTypes.MediaParsedChanged, onMediaParsedChanged = OnMediaParsedChanged);
		}

		private void DetachEvents()
		{
			var eventManager = vlc.Manager.libvlc_media_event_managerDelegate(Handle);
			vlc.DetachEvent(eventManager, EventTypes.MediaStateChanged, onMediaStateChanged);
			vlc.DetachEvent(eventManager, EventTypes.MediaMetaChanged, onMediaMetaChanged);
			vlc.DetachEvent(eventManager, EventTypes.MediaDurationChanged, onMediaDurationChanged);
			vlc.DetachEvent(eventManager, EventTypes.MediaParsedChanged, onMediaParsedChanged);
		}

		public void OnMediaStateChanged(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			StateChanged(this, new MediaStateChangedEventArgs(eventArgs.Union.MediaStateChanged.NewState));
		}

		public void OnMediaMetaChanged(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			MetaChanged(this, new MediaMetaChangedEventArgs(eventArgs.Union.MediaMetaChanged.Meta));
		}

		public void OnMediaDurationChanged(IntPtr ptr)
		{
			var eventArgs = (LibVlcEvent)Marshal.PtrToStructure(ptr, typeof(LibVlcEvent));
			DurationChanged(this, new MediaDurationChangedEventArgs(eventArgs.Union.MediaDurationChanged.NewDuration));
		}

		public void OnMediaParsedChanged(IntPtr ptr)
		{
			ParsedChanged(this, new EventArgs());
		}
	}
}
