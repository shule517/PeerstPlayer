namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板ファクトリー
	/// </summary>
	class BbsFactory
	{
		/// <summary>
		/// 掲示板ストラテジを生成する
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public BbsStrategy Create(string url)
		{
			BbsStrategy instance = null;
			BbsServer server = JudgeBBSServer(url);

			switch (server)
			{
				case BbsServer.Shitaraba:
					instance = new ShitarabaBbsStrategy();
					break;
			}

			return instance;
		}

		/// <summary>
		/// 掲示板サーバ判定
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		private BbsServer JudgeBBSServer(string url)
		{
			// TODO URLを解析して、サーバを指定する
			// TODO 暫定で「したらばサーバ」を指定
			return BbsServer.Shitaraba;
		}
	}
}
