
namespace PeerstLib.Bbs.Strategy
{
	//-------------------------------------------------------------
	// 概要：掲示板ストラテジファクトリクラス
	// 責務：URLに対応したストラテジを生成すること
	//-------------------------------------------------------------
	public static class BbsStrategyFactory
	{
		//-------------------------------------------------------------
		// 概要：URLに対応したストラテジの生成
		//-------------------------------------------------------------
		public static BbsStrategy Create(string url)
		{
			// 掲示板解析
			BbsInfo bbsInfo = BbsUrlAnalyzer.Analyze(url);

			switch (bbsInfo.BbsServer)
			{
				// したらば掲示板
				case BbsServer.Shitaraba:
					return new ShitarabaBbsStrategy(bbsInfo);

				// わいわいKakiko
				case BbsServer.YYKakiko:
					return new YYKakikoBbsStrategy(bbsInfo);

				// 未対応
				default:
					return new NullBbsStrategy(bbsInfo);
			}
		}
	}
}
