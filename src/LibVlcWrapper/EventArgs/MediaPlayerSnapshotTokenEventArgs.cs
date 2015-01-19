namespace LibVlcWrapper.Structures
{
	public class MediaPlayerSnapshotTokenEventArgs : System.EventArgs
	{
		public string FileName { get; private set; }

		public MediaPlayerSnapshotTokenEventArgs(string fileName)
		{
			FileName = fileName;
		}
	}
}
