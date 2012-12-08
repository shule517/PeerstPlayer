namespace Shule.Peerst.BBS
{
	public class OperationBBS
	{
		BBSFactory factory;
		BBSStrategy bbsStrategy;

		OperationBBS()
		{
			factory = new BBSFactory();
		}

		/// <summary>
		/// 掲示板URL変更
		/// </summary>
		/// <param name="url"></param>
		public void SetURL(string url)
		{
			bbsStrategy = factory.Create(url);
		}
	}
}
