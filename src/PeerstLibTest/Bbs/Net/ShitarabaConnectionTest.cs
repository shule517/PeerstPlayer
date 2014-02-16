using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstLib.Bbs.Net;
using PeerstLib.Net;
using System;
using System.Net;

namespace PeerstLibTest.Bbs.Net
{
	[TestClass]
	public class ShitarabaConnectionTest
	{
		const string category = "game";
		const string boardNo = "45037";

		[TestMethod]
		public void ShitarabaConnection_GetSubject()
		{
			// 取得成功
			RequestResult result = ShitarabaConnection.GetSubjects(category, boardNo);
			Assert.AreEqual(result.HttpStatusCode, HttpStatusCode.OK);
			Assert.IsTrue(result.BodyText.Length > 0);
		}

		[TestMethod]
		public void ShitarabaConnection_GetSubjectDiff()
		{
			RequestResult result;

			// 差分あり
			result = ShitarabaConnection.GetSubjectsDiff(category, boardNo, DateTime.Now);
			Assert.AreEqual(result.HttpStatusCode, HttpStatusCode.OK);
			Assert.IsTrue(result.BodyText.Length > 0);

			// 差分なし
			result = ShitarabaConnection.GetSubjectsDiff(category, boardNo, DateTime.Now.AddSeconds(-10));
			Assert.AreEqual(result.HttpStatusCode, HttpStatusCode.NotModified);
			Assert.IsTrue(result.BodyText.Length == 0);
		}
	}
}
