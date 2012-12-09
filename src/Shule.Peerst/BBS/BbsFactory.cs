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
			BbsUrl bbsUrl = null;
			BbsServer server = BbsServer.UnSupport;

			// 掲示板URL解析
			BbsUrlAnalyzer analyzer = new BbsUrlAnalyzer();
			analyzer.Analyze(url, out server, out bbsUrl);

			// 各ストラテジ生成
			switch (server)
			{
				case BbsServer.Shitaraba:
					instance = new ShitarabaBbsStrategy(bbsUrl);
					break;
				case BbsServer.YYKakiko:
					instance = new YYKakikoBbsStrategy(bbsUrl);
					break;
			}

			return instance;
		}
	}
}
