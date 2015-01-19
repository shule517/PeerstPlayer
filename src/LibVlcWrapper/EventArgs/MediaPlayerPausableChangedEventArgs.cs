namespace LibVlcWrapper.Structures
{
	public class MediaPlayerPausableChangedEventArgs : System.EventArgs
	{
		public bool NewPausable { get; private set; }
		public MediaPlayerPausableChangedEventArgs(bool newPausable)
		{
			NewPausable = newPausable;
		}
	}
}
