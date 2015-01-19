namespace LibVlcWrapper.Structures
{
	public class MediaStateChangedEventArgs : System.EventArgs
	{
		public MediaStates State { get; private set; }
		public MediaStateChangedEventArgs(MediaStates state)
		{
			State = state;
		}
	}
}
