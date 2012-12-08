namespace Shule.Peerst.BBS
{
	abstract class BBSStrategy
	{
		public void ReadThread()
		{
		}

		protected abstract void ReadDat();
		protected abstract void AnalyzeData();
	}
}
