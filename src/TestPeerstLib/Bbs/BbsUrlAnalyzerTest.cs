using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstLib.Bbs.Data;
using PeerstLib.Bbs.Util;

namespace TestPeerstLib.Bbs
{
	[TestClass]
	public class BbsUrlAnalyzerTest
	{
		//-------------------------------------------------------------
		// 確認：掲示板URLの解析が正しいか
		//-------------------------------------------------------------
		[TestMethod]
		public void BbsUrlAnalyzer_Analyze()
		{
			// 新したらば
			CheckBbsInfo("http://jbbs.shitaraba.net/game/45037/",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/game/45037/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = null,
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.shitaraba.net/game/45037",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/game/45037/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = null,
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.shitaraba.net/game/45037/1286755510/",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.shitaraba.net/game/45037/1286755510",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = null,
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = null,
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/l50",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/50",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/50-",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/50n-",
				new BbsInfo
				{
					Url = "http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});

			// 旧したらば
			CheckBbsInfo("http://jbbs.livedoor.jp/game/45037/",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/game/45037/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = null,
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.livedoor.jp/game/45037",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/game/45037/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = null,
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.livedoor.jp/game/45037/1286755510/",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.livedoor.jp/game/45037/1286755510",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = null,
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = null,
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/l50",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/50",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/50-",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});
			CheckBbsInfo("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/50n-",
				new BbsInfo
				{
					Url = "http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/",
					BoardGenre = "game",
					BoardNo = "45037",
					ThreadNo = "1286755510",
					BbsServer = BbsServer.Shitaraba,
				});

			// 2ch互換
			
			CheckBbsInfo("http://sepia0330.dyndns.org/eicar/",
				new BbsInfo
				{
					Url = "http://sepia0330.dyndns.org/eicar/",
					BoardGenre = "sepia0330.dyndns.org",
					BoardNo = "eicar",
					ThreadNo = null,
					BbsServer = BbsServer.YYKakiko,
				});
			CheckBbsInfo("http://sepia0330.dyndns.org/test/read.cgi/eicar/1363498700/l50",
				new BbsInfo
				{
					Url = "http://sepia0330.dyndns.org/test/read.cgi/eicar/1363498700/",
					BoardGenre = "sepia0330.dyndns.org",
					BoardNo = "eicar",
					ThreadNo = "1363498700",
					BbsServer = BbsServer.YYKakiko,
				});
			CheckBbsInfo("http://ossan.moe.hm/peca/test/read.cgi/ossan/1423442729/l50",
				new BbsInfo
				{
					Url = "http://ossan.moe.hm/peca/test/read.cgi/ossan/1423442729/",
					BoardGenre = "ossan.moe.hm/peca",
					BoardNo = "ossan",
					ThreadNo = "1423442729",
					BbsServer = BbsServer.YYKakiko,
				});
			CheckBbsInfo("http://ossan.moe.hm/peca/test/read.cgi/ossan/1423442729/",
				new BbsInfo
				{
					Url = "http://ossan.moe.hm/peca/test/read.cgi/ossan/1423442729/",
					BoardGenre = "ossan.moe.hm/peca",
					BoardNo = "ossan",
					ThreadNo = "1423442729",
					BbsServer = BbsServer.YYKakiko,
				});
			CheckBbsInfo("http://ossan.moe.hm/peca/ossan/",
				new BbsInfo
				{
					Url = "http://ossan.moe.hm/peca/ossan/",
					BoardGenre = "ossan.moe.hm/peca",
					BoardNo = "ossan",
					ThreadNo = null,
					BbsServer = BbsServer.YYKakiko,
				});
			// 未対応
			CheckBbsInfo("",
				new BbsInfo
				{
					Url = "",
					BoardGenre = null,
					BoardNo = null,
					ThreadNo = null,
					BbsServer = BbsServer.UnSupport,
				});
			CheckBbsInfo("http://bayonet.ddo.jp/sp/",
				new BbsInfo
				{
					Url = "http://bayonet.ddo.jp/sp/",
					BoardGenre = null,
					BoardNo = null,
					ThreadNo = null,
					BbsServer = BbsServer.UnSupport,
				});
			CheckBbsInfo("http://bayonet.ddo.jp/sp/xgame.html#xanc_31",
				new BbsInfo
				{
					Url = "http://bayonet.ddo.jp/sp/xgame.html#xanc_31",
					BoardGenre = null,
					BoardNo = null,
					ThreadNo = null,
					BbsServer = BbsServer.UnSupport,
				});
			CheckBbsInfo("http://listeners.reep.info/index.php?%A5%A4%A5%D9%A5%F3%A5%C8%C6%FC%C4%F8/2013-06-01",
				new BbsInfo
				{
					Url = "http://listeners.reep.info/index.php?%A5%A4%A5%D9%A5%F3%A5%C8%C6%FC%C4%F8/2013-06-01",
					BoardGenre = null,
					BoardNo = null,
					ThreadNo = null,
					BbsServer = BbsServer.UnSupport,
				});
			CheckBbsInfo("http://takami98.sakura.ne.jp/peca-navi/turf-page/index.php",
				new BbsInfo
				{
					Url = "http://takami98.sakura.ne.jp/peca-navi/turf-page/index.php",
					BoardGenre = null,
					BoardNo = null,
					ThreadNo = null,
					BbsServer = BbsServer.UnSupport,
				});
			CheckBbsInfo("http://temp.orz.hm/yp/uptest/",
				new BbsInfo
				{
					Url = "http://temp.orz.hm/yp/uptest/",
					BoardGenre = null,
					BoardNo = null,
					ThreadNo = null,
					BbsServer = BbsServer.UnSupport,
				});
			CheckBbsInfo("http://twitter.com/twityp",
				new BbsInfo
				{
					Url = "http://twitter.com/twityp",
					BoardGenre = null,
					BoardNo = null,
					ThreadNo = null,
					BbsServer = BbsServer.UnSupport,
				});
			CheckBbsInfo("http://www.gamers-review.net/title.php?title=441",
				new BbsInfo
				{
					Url = "http://www.gamers-review.net/title.php?title=441",
					BoardGenre = null,
					BoardNo = null,
					ThreadNo = null,
					BbsServer = BbsServer.UnSupport,
				});
		}

		//-------------------------------------------------------------
		// 概要：指定URLの解析結果と、想定データとの比較
		//-------------------------------------------------------------
		private static void CheckBbsInfo(string url, BbsInfo bbsInfo)
		{
			BbsInfo result = BbsUrlAnalyzer.Analyze(url);
			Assert.AreEqual(bbsInfo.Url, result.Url);
			Assert.AreEqual(bbsInfo.BoardGenre, result.BoardGenre);
			Assert.AreEqual(bbsInfo.BoardNo, result.BoardNo);
			Assert.AreEqual(bbsInfo.ThreadNo, result.ThreadNo);
			Assert.AreEqual(bbsInfo.BbsServer, result.BbsServer);
		}
	}
}
