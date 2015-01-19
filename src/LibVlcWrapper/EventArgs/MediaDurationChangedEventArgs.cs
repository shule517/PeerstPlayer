namespace LibVlcWrapper.Structures
{
	public class MediaDurationChangedEventArgs : System.EventArgs
	{
		public long Duration { get; private set; }
		public MediaDurationChangedEventArgs(long duration)
		{
			Duration = duration;
		}
	}
}
