using System.Runtime.InteropServices;

namespace LibVlcWrapper
{
	[StructLayout(LayoutKind.Sequential)]
	public struct VideoTrack
	{
		public uint Height;
		public uint Width;
		public uint SarNum;
		public uint SarDen;
		public uint FramerateNum;
		public uint FramerateDen;
	}
}
