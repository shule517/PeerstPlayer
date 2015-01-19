namespace LibVlcWrapper.Structures
{
	public class MediaPlayerLengthChangedEventArgs : System.EventArgs
	{
		public long NewLength { get; private set; }

		public MediaPlayerLengthChangedEventArgs(long newLength)
		{
			NewLength = newLength;
		}
	}
}
