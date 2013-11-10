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
		public IBbsStrategy Create(string url)
		{
			IBbsStrategy instance = null;
			BbsInfo bbsUrl = null;

			// 掲示板URL解析
			BbsUrlAnalyzer analyzer = new BbsUrlAnalyzer();
			analyzer.Analyze(url, out bbsUrl);

			// 各ストラテジ生成
			switch (bbsUrl.BBSServer)
			{
				// したらば掲示板
				case BbsServer.Shitaraba:
					instance = new ShitarabaBbsStrategy(bbsUrl);
					break;
				// わいわいkakiko
				case BbsServer.YYKakiko:
					instance = new YYKakikoBbsStrategy(bbsUrl);
					break;
				// 未対応(NULLオブジェクト)
				default:
					instance = new NullBbsStrategy();
					break;
			}

			return instance;
		}
	}
}
