namespace Shule.Peerst.BBS
{
	class BBSFactory
	{
		/// <summary>
		/// 掲示板ストラテジを生成する
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public BBSStrategy Create(string url)
		{
			BBSStrategy instance = null;
			BBSServerType server = JudgeBBSServer(url);

			switch (server)
			{
				case BBSServerType.Shitaraba:
					instance = new ShitarabaBBSStrategy();
					break;
			}

			return instance;
		}

		/// <summary>
		/// 掲示板サーバ判定
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		private BBSServerType JudgeBBSServer(string url)
		{
			// TODO URLを解析して、サーバを指定する
			// TODO 暫定で「したらばサーバ」を指定
			return BBSServerType.Shitaraba;
		}
	}
}
