
using System;
using System.Runtime.InteropServices;

namespace LibVlcWrapper
{
	[StructLayout(LayoutKind.Sequential)]
	public struct SubtitleTrack
	{
		public IntPtr Encoding;
	}
}
