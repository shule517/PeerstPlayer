namespace LibVlcWrapper.Structures
{
	public class MediaMetaChangedEventArgs : System.EventArgs
	{
		public MediaMetadatas Metadata { get; private set; }

		public MediaMetaChangedEventArgs(MediaMetadatas metadata)
		{
			Metadata = metadata;
		}
	}
}
