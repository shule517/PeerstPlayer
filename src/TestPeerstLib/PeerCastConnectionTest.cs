using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstLib.PeerCast;

namespace TestPeerstLib
{
	[TestClass]
	public class PeerCastConnectionTest
	{
		[TestMethod]
		public void Test_PeerCastConnection_GetChannelInfo()
		{
			StreamUrlInfo info = StreamUrlAnalyzer.GetUrlInfo(TestSettings.StreamUrl);
			PeerCastConnection connect = new PeerCastConnection(info);
			connect.GetChannelInfo();
		}
	}
}
