using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstPlayer;

namespace UIUnitTest.Player
{
	[TestClass]
	public class PlayerViewModelTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			PlayerViewModel viewModel = new PlayerViewModel();
			viewModel.OpenMovie("http://localhost:7145/pls/0CB988CD37D8F920F12E2820DE678DE3?tip=219.117.192.180:7144");
		}
	}
}
