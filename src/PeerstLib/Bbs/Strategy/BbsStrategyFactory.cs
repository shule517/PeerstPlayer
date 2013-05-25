using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstLib.Bbs.Strategy
{
	// 掲示板ストラテジファクトリクラス
	static public class BbsStrategyFactory
	{
		//-------------------------------------------------------------
		// 概要：URLに対応したストラテジの生成
		//-------------------------------------------------------------
		static public BbsStrategy Create(string url)
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
