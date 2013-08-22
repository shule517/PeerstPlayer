using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstPlayer.Forms.ThreadSelect;

namespace TestPeertPlayer.ViewModel
{
	[TestClass]
	public class ThreadSelectViewModelTest
	{
		//-------------------------------------------------------------
		// 確認：ThreadListChangeイベントの実行確認
		//-------------------------------------------------------------
		[TestMethod]
		public void ThreadSelectViewModel_Update()
		{
			bool result = false;
			ThreadSelectViewModel viewModel = new ThreadSelectViewModel();
			viewModel.ThreadListChange += (sender, e) =>
			{
				Assert.IsTrue(viewModel.ThreadList.Count > 0);
				result = true;
			};

			viewModel.Update("http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1286755510/");

			for (int i = 0; i < 100; i++)
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
