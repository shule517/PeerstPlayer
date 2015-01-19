using System.Runtime.InteropServices;

namespace LibVlcWrapper
{
	[StructLayout(LayoutKind.Sequential)]
	public struct MediaStats
	{
		public int Read;
		public float inputBitrate;
		public int DemuxReadBytes;
		public float DemuxBitrate;
		public int DemuxCorrupted;
		public int DemuxDiscontinuity;
		public int DecodedVideo;
		public int DecodedAudio;
		public int DisplayedPictures;
		public int LostPictures;
		public int PlayedAbuffers;
		public int LostAbuffers;
		public int SendPackets;
		public int SentBytes;
		public float SendBitrate;
	}
}
