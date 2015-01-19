namespace LibVlcWrapper.Structures
{
	public class MediaPlayerSeekableChangedEventArgs : System.EventArgs
	{
		public bool NewSeekable { get; private set; }

		public MediaPlayerSeekableChangedEventArgs(bool newSeekable)
		{
			NewSeekable = newSeekable;
		}
	}
}
