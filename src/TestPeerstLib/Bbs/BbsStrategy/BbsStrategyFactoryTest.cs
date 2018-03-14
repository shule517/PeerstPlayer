﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstLib.Bbs.Strategy;

namespace TestPeerstLib.Bbs
{
	[TestClass]
	public class BbsStrategyFactoryTest
	{
		//-------------------------------------------------------------
		// 確認：指定URLに対応したストラテジを生成しているか
		//-------------------------------------------------------------
		[TestMethod]
		public void BbsStrategyFactory_Create()
		{
			// 新したらば
			CheckStrategy("http://jbbs.shitaraba.net/game/45037/", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.shitaraba.net/game/45037", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.shitaraba.net/game/45037/1286755510/", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.shitaraba.net/game/45037/1286755510", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/l50", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/50", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/50-", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/50n-", typeof(ShitarabaBbsStrategy));
			CheckStrategy("https://jbbs.shitaraba.net/game/45037/", typeof(ShitarabaBbsStrategy));
			CheckStrategy("https://jbbs.shitaraba.net/game/45037/1286755510/", typeof(ShitarabaBbsStrategy));
			CheckStrategy("https://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/", typeof(ShitarabaBbsStrategy));
			CheckStrategy("https://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1286755510/50-", typeof(ShitarabaBbsStrategy));

			// 旧したらば
			CheckStrategy("http://jbbs.livedoor.jp/game/45037/", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.livedoor.jp/game/45037", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.livedoor.jp/game/45037/1286755510/", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.livedoor.jp/game/45037/1286755510", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/l50", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/50", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/50-", typeof(ShitarabaBbsStrategy));
			CheckStrategy("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/50n-", typeof(ShitarabaBbsStrategy));

			// 2ch互換
			//CheckStrategy("http://sepia0330.dyndns.org/eicar/", typeof(YYKakikoBbsStrategy));	// TODO 別サーバタイプとする？
			//CheckStrategy("http://sepia0330.dyndns.org/eicar", typeof(YYKakikoBbsStrategy));

			// 未対応
			CheckStrategy("", typeof(NullBbsStrategy));
			CheckStrategy("http://bayonet.ddo.jp/sp/", typeof(NullBbsStrategy));
			CheckStrategy("http://bayonet.ddo.jp/sp/xgame.html#xanc_31", typeof(NullBbsStrategy));
			CheckStrategy("http://listeners.reep.info/index.php?%A5%A4%A5%D9%A5%F3%A5%C8%C6%FC%C4%F8/2013-06-01", typeof(NullBbsStrategy));
			CheckStrategy("http://takami98.sakura.ne.jp/peca-navi/turf-page/index.php", typeof(NullBbsStrategy));
			CheckStrategy("http://temp.orz.hm/yp/uptest/", typeof(NullBbsStrategy));
			CheckStrategy("http://twitter.com/twityp", typeof(NullBbsStrategy));
			CheckStrategy("http://www.gamers-review.net/title.php?title=441", typeof(NullBbsStrategy));
		}

		//-------------------------------------------------------------
		// 概要：生成されたストラテジのチェック
		//-------------------------------------------------------------
		private static void CheckStrategy(string url, Type type)
		{
			BbsStrategy strategy = BbsStrategyFactory.Create(url);
			Assert.AreEqual(type, strategy.GetType());
		}
	}
}
