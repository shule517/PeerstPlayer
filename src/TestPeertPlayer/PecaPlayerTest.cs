using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstPlayer.Control;

namespace TestPeertPlayer
{
	[TestClass]
	public class PecaPlayerTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			PecaPlayer pecaPlayer = new PecaPlayer();
			string streamUrl = "http://localhost:7145/pls/90E13182A11873DF1B8ADD5F4E7C0A38?tip=183.181.158.208:7154";
			pecaPlayer.open(streamUrl);
		}
	}
}
