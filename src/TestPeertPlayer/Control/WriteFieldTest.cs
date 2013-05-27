using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstPlayer.Control;
using System.Windows.Forms;
using System.Threading;

namespace TestPeertPlayer.Control
{
	[TestClass]
	public class WriteFieldTest
	{
		[TestMethod]
		public void WriteFieldTest_SelectThreadUrl_Change()
		{
			// したらば：スレッド設定有り
			testThreadTitle(
				"http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1352127309/",
				"スレッド[ 【qwebview.html】当たり前だろ！シュールch75【流行らない・・】 ] (1000)");

			// したらば：スレッド設定無し
			testThreadTitle(
				"http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/",
				"掲示板[ シュール・備長炭と愉快な仲間たち ] スレッドを選択してください");

			// YY：スレッド設定有り
			testThreadTitle(
				"http://jbbs.livedoor.jp/bbs/read.cgi/netgame/11007/1343713121/",
				"スレッド[ シャクレ84cm ] (1000)");

			// YY：スレッド設定無し
			testThreadTitle(
				"http://jbbs.livedoor.jp/bbs/read.cgi/netgame/11007/",
				"掲示板[ シャクレ ] スレッドを選択してください");

			// 2ch互換：スレッド設定有り
			testThreadTitle(
				"http://sepia0330.dyndns.org/test/read.cgi/eicar/1346411572/l50",
				"スレッド[ これは酷いな142 ] (1001)");

			// 2ch互換：スレッド設定無し
			testThreadTitle(
				"http://sepia0330.dyndns.org/eicar/",
				"掲示板[ これは酷いな避難所BBS ] スレッドを選択してください");

			// 未対応URL
			testThreadTitle(
				"https://www.google.co.jp/",
				"未対応URLです");

			// 空
			testThreadTitle(
				"",
				"URLが指定されていません");
		}

		private static void testThreadTitle(string url, string threadTitle)
		{
			// URLを設定
			WriteField writeField = new WriteField();
			writeField.SelectThreadUrl = url;

			for (int i = 0; i < 20; i++)
			{
				Application.DoEvents();
				Thread.Sleep(20);
			}

			// スレッドタイトルのチェック
			PrivateObject accessor = new PrivateObject(writeField);
			Label label = (Label)accessor.GetField("selectThreadLabel");
			Assert.AreEqual(threadTitle, label.Text);
		}
	}
}
