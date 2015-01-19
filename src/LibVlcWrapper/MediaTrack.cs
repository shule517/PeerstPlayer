using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LibVlcWrapper
{
	[StructLayout(LayoutKind.Sequential)]
	public struct MediaTrack
	{
		public uint Codec;
		public uint OriginalFourcc;
		public int Id;
		public TrackType Type;
		public int Profile;
		public int Level;
		public MediaTrackUnion Union;
		public uint Bitrate;
		[MarshalAs(UnmanagedType.LPStr)] 
		public string Language;
		[MarshalAs(UnmanagedType.LPStr)] 
		public string Description;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct MediaTrackUnion
	{
		[FieldOffset(0)]
		private IntPtr ptr;

		public AudioTrack Audio
		{
			get { return (AudioTrack)Marshal.PtrToStructure(ptr, typeof(AudioTrack)); }
		}

		public VideoTrack Video
		{
			get { return (VideoTrack)Marshal.PtrToStructure(ptr, typeof(VideoTrack)); }
		}

		public SubtitleTrack Subtitle
		{
			get { return (SubtitleTrack)Marshal.PtrToStructure(ptr, typeof(SubtitleTrack)); }
		}
	}
}
