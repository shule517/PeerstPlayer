namespace LibVlcWrapper.Structures
{
	public class MediaPlayerPositionChangedEventArgs : System.EventArgs
	{
		public float NewPosition { get; private set; }

		public MediaPlayerPositionChangedEventArgs(float newPosition)
		{
			NewPosition = newPosition;
		}
	}
}
