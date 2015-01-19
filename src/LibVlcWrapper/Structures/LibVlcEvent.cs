using System;
using System.Runtime.InteropServices;

namespace LibVlcWrapper.Structures
{

	[StructLayout(LayoutKind.Sequential)]
	internal struct LibVlcEvent
	{
		public EventTypes EventType;
		public IntPtr Sender;
		internal LibVlcEventUnion Union;

		[StructLayout(LayoutKind.Explicit)]
		internal struct LibVlcEventUnion
		{
			[FieldOffset(0)]
			internal MediaMetaChanged MediaMetaChanged;

			[FieldOffset(0)]
			internal MediaDurationChanged MediaDurationChanged;

			[FieldOffset(0)]
			internal MediaParsedChanged MediaParsedChanged;

			[FieldOffset(0)]
			internal MediaStateChanged MediaStateChanged;

			[FieldOffset(0)]
			internal MediaPlayerBuffering MediaPlayerBuffering;

			[FieldOffset(0)]
			internal MediaPlayerPositionChanged MediaPlayerPositionChanged;

			[FieldOffset(0)]
			internal MediaPlayerTimeChanged MediaPlayerTimeChanged;

			[FieldOffset(0)]
			internal MediaPlayerTitleChanged MediaPlayerTitleChanged;

			[FieldOffset(0)]
			internal MediaPlayerSeekableChanged MediaPlayerSeekableChanged;

			[FieldOffset(0)]
			internal MediaPlayerPausableChanged MediaPlayerPausableChanged;

			[FieldOffset(0)]
			internal MediaPlayerSnapshotToken MediaPlayerSnapshotToken;

			[FieldOffset(0)]
			internal MediaPlayerScrambledChanged MediaPlayerScrambledChanged;

			[FieldOffset(0)]
			internal MediaPlayerNewVout MediaPlayerNewVout;

			[FieldOffset(0)]
			internal MediaPlayerLengthChanged MediaPlayerLengthChanged;

			[FieldOffset(0)]
			internal MediaPlayerMediaChanged MediaPlayerMediaChanged;

			[FieldOffset(0)]
			internal MediaPlayerEsChanged MediaPlayerEsChanged;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaMetaChanged
		{
			public MediaMetadatas Meta;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaDurationChanged
		{
			public long NewDuration;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaParsedChanged
		{
			public int Status;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaStateChanged
		{
			public MediaStates NewState;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerBuffering
		{
			public float NewCache;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerPositionChanged
		{
			public float NewPosition;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerTimeChanged
		{
			public long NewTime;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerTitleChanged
		{
			public int NewTitle;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerSeekableChanged
		{
			public bool NewSeekable;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerPausableChanged
		{
			public bool NewPausable;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerSnapshotToken
		{
			//[MarshalAs(UnmanagedType.LPStr)]
			//public string FileName;
			public IntPtr FileName;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerScrambledChanged
		{
			public bool NewScrambled;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerNewVout
		{
			public int NewCount;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerLengthChanged
		{
			public long NewLength;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerMediaChanged
		{
			public LibVlcMedia NewMedia;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct MediaPlayerEsChanged
		{
			public TrackType Type;
			public int Id;
		}
	}


// 	[StructLayout(LayoutKind.Explicit)]
// 	internal struct LibVlcEvent
// 	{
// 		[FieldOffset(0)]
// 		public EventTypes EventType;
// 		[FieldOffset(8)]
// 		public IntPtr Sender;
//  		[FieldOffset(16)]
//  		internal MediaMetaChanged MediaMetaChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaDurationChanged MediaDurationChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaParsedChanged MediaParsedChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaStateChanged MediaStateChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerBuffering MediaPlayerBuffering;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerPositionChanged MediaPlayerPositionChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerTimeChanged MediaPlayerTimeChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerTitleChanged MediaPlayerTitleChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerSeekableChanged MediaPlayerSeekableChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerPausableChanged MediaPlayerPausableChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerSnapshotToken MediaPlayerSnapshotToken;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerScrambledChanged MediaPlayerScrambledChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerNewVout MediaPlayerNewVout;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerLengthChanged MediaPlayerLengthChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerMediaChanged MediaPlayerMediaChanged;
// 
// 		[FieldOffset(16)]
// 		internal MediaPlayerEsChanged MediaPlayerEsChanged;
// 
// 		[StructLayout(LayoutKind.Explicit)]
// 		internal struct LibVlcEventUnion
// 		{
// 			[FieldOffset(0)]
// 			internal MediaMetaChanged MediaMetaChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaDurationChanged MediaDurationChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaParsedChanged MediaParsedChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaStateChanged MediaStateChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerBuffering MediaPlayerBuffering;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerPositionChanged MediaPlayerPositionChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerTimeChanged MediaPlayerTimeChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerTitleChanged MediaPlayerTitleChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerSeekableChanged MediaPlayerSeekableChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerPausableChanged MediaPlayerPausableChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerSnapshotToken MediaPlayerSnapshotToken;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerScrambledChanged MediaPlayerScrambledChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerNewVout MediaPlayerNewVout;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerLengthChanged MediaPlayerLengthChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerMediaChanged MediaPlayerMediaChanged;
// 
// 			[FieldOffset(0)]
// 			internal MediaPlayerEsChanged MediaPlayerEsChanged;
// 		}
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaMetaChanged
// 	{
// 		public MediaMetadatas Meta;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaDurationChanged
// 	{
// 		public long NewDuration;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaParsedChanged
// 	{
// 		public int Status;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaStateChanged
// 	{
// 		public MediaStates NewState;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerBuffering
// 	{
// 		public float NewCache;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerPositionChanged
// 	{
// 		public float NewPosition;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerTimeChanged
// 	{
// 		public long NewTime;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerTitleChanged
// 	{
// 		public int NewTitle;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerSeekableChanged
// 	{
// 		public bool NewSeekable;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerPausableChanged
// 	{
// 		public bool NewPausable;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerSnapshotToken
// 	{
// 		//[MarshalAs(UnmanagedType.LPStr)]
// 		//public string FileName;
// 		public IntPtr FileName;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerScrambledChanged
// 	{
// 		public bool NewScrambled;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerNewVout
// 	{
// 		public int NewCount;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerLengthChanged
// 	{
// 		public long NewLength;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerMediaChanged
// 	{
// 		public LibVlcMedia NewMedia;
// 	}
// 
// 	[StructLayout(LayoutKind.Sequential)]
// 	internal struct MediaPlayerEsChanged
// 	{
// 		public TrackType Type;
// 		public int Id;
// 	}


// 	[StructLayout(LayoutKind.Explicit)]
// 	internal struct VlcEventArgs
// 	{
// 		[FieldOffset(0)]
// 		public EventTypes EventType;
// 
// 		[FieldOffset(4)]
// 		public IntPtr Sender;
// 
// #if X86
// 		[FieldOffset(8)]
// #else
// 		[FieldOffset(12)]
// #endif
// 		public IntPtr 
// 	}
}
