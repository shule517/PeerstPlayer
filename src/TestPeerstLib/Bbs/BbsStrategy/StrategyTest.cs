using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstLib.Bbs.Strategy;
using PeerstLib.Bbs;
using System.Windows.Forms;
using System.Threading;

namespace TestPeerstLib.Bbs.Strategy
{
	[TestClass]
	public class StrategyTest
	{
		[TestMethod]
		public void Strategy_ThreadListChange()
		{
			BbsStrategy strategy = BbsStrategyFactory.Create("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/");
			bool result = false;
			strategy.ThreadListChange += (sender, e) => result = true;
			strategy.UpdateThreadList();

			for (int i = 0; i < 10; i++)
			{
				Application.DoEvents();
				Thread.Sleep(200);

				if (result)
				{
					return;
				}
			}

			Assert.Fail("ThreadListChangeイベントが実行されなかった");
		}
	}
}
