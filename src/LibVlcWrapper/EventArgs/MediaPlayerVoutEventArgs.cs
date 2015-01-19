namespace LibVlcWrapper.Structures
{
	public class MediaPlayerVoutEventArgs : System.EventArgs
	{
		public int NewCount { get; private set; }

		public MediaPlayerVoutEventArgs(int newCount)
		{
			NewCount = newCount;
		}
	}
}
