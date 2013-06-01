using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstLib.Bbs;
using PeerstLib.PeerCast;

namespace TestPeerstLib
{
	[TestClass]
	public class PeerCastConnectionTest
	{
		//-------------------------------------------------------------
		// 概要：チャンネル情報取得
		// 詳細：指定URL,指定ストリームIDのチャンネル情報を取得する
		//-------------------------------------------------------------
		private static ChannelInfo GetChannelInfo(string xmlUri, string streamId) {
			// URL情報の取得
			StreamUrlInfo info = StreamUrlAnalyzer.GetUrlInfo(TestSettings.StreamUrl);
			info.StreamId = streamId;

			// XMLの読み込み
			XElement elements = XElement.Load(xmlUri);

			// チャンネル情報取得
			PeerCastConnection pecaConnection = new PeerCastConnection(info);
			PrivateObject accessor = new PrivateObject(pecaConnection);
			ChannelInfo channelInfo = (ChannelInfo)accessor.Invoke("AnlyzeViewXML", new object[] { elements });

			return channelInfo;
		}

		// TODO ジャンルの調節確認

		// TODO 未実装
		/*
		//-------------------------------------------------------------
		// 概要：チャンネル情報取得
		// 詳細：自分のリレー色判定の確認
		//-------------------------------------------------------------
		[TestMethod]
		public void Test_PeerCastConnection_GetChannelInfo_RelayColor()
		{
			{
				ChannelInfo info = GetChannelInfo(TestSettings.ViewXMLPath, "BCFF60B0A56E77509D3A1EE98F1794DE");
				Assert.AreEqual(RelayColor.Blue, info.RelayColor);
			}
			{
				ChannelInfo info = GetChannelInfo(TestSettings.ViewXMLPath, "90BB93C694EB73DE7BB528E50ECA7FAC");
				Assert.AreEqual(RelayColor.Green, info.RelayColor);
			}
			{
				ChannelInfo info = GetChannelInfo(TestSettings.ViewXMLPath, "9712484B83F719C92072BDB47E0E9A61");
				Assert.AreEqual(RelayColor.Green, info.RelayColor);
			}
			{
				ChannelInfo info = GetChannelInfo(TestSettings.ViewXMLPath, "2116DB2112C6D9AB85493CC4CA4AF648");
				Assert.AreEqual(RelayColor.Purple, info.RelayColor);
			}
			{
				ChannelInfo info = GetChannelInfo(TestSettings.ViewXMLPath, "90BF74DF859B629A6602E403C59038D1");
				Assert.AreEqual(RelayColor.Purple, info.RelayColor);
			}
		}

		//-------------------------------------------------------------
		// 概要：チャンネル情報取得
		// 詳細：リレー先のリレー色判定の確認
		//-------------------------------------------------------------
		[TestMethod]
		public void Test_PeerCastConnection_GetChannelInfo_HostRelayColor()
		{
			ChannelInfo info = GetChannelInfo(TestSettings.ViewXMLPath, "BCFF60B0A56E77509D3A1EE98F1794DE");
			Assert.AreEqual(3, info.HostList.Count);
			Assert.AreEqual(RelayColor.Blue, info.HostList[1].RelayColor);
			Assert.AreEqual(RelayColor.Purple, info.HostList[0].RelayColor);
			Assert.AreEqual(RelayColor.Purple, info.HostList[2].RelayColor);
		}
		 */
	}
}
