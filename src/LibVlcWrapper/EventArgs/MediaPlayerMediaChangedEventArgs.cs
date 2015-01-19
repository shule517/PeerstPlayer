using System;
using LibVlcWrapper.Structures;

namespace LibVlcWrapper.Structures
{
	public class MediaPlayerMediaChangedEventArgs : System.EventArgs
	{
		public LibVlcMedia NewMedia { get; private set; }
		public MediaPlayerMediaChangedEventArgs(LibVlcMedia newMedia)
		{
			NewMedia = newMedia;
		}
	}
}
