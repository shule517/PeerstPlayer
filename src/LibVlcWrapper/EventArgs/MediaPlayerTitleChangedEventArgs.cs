namespace LibVlcWrapper.Structures
{
	public class MediaPlayerTitleChangedEventArgs : System.EventArgs
	{
		public int NewTitle { get; private set; }

		public MediaPlayerTitleChangedEventArgs(int newTitle)
		{
			NewTitle = newTitle;
		}
	}
}
