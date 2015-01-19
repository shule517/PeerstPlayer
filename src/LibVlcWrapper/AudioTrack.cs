using System.Runtime.InteropServices;

namespace LibVlcWrapper
{
	[StructLayout(LayoutKind.Sequential)]
	public struct AudioTrack
	{
		public uint Channels;
		public uint Rate;
	}
}
