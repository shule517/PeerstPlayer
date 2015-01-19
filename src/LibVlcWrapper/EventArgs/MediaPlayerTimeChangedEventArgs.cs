namespace LibVlcWrapper.Structures
{
	public class MediaPlayerTimeChangedEventArgs : System.EventArgs
	{
		public long NewTime { get; private set; }

		public MediaPlayerTimeChangedEventArgs(long newTime)
		{
			NewTime = newTime;
		}
	}
}
