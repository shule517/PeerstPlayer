using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstPlayer.Controls.WriteField;

namespace TestPeertPlayer.Control
{
	[TestClass]
	public class WriteFieldTest
	{
		//-------------------------------------------------------------
		// 確認：選択スレッドタイトルの表示確認
		//-------------------------------------------------------------
		[TestMethod]
		public void WriteFieldTest_SelectThreadUrl_Change()
		{
			// 新したらば：スレッド設定有り
			TestThreadTitle(
				"http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/1352127309/",
				"スレッド[ 【qwebview.html】当たり前だろ！シュールch75【流行らない・・】 ] (1000)");

			// 新したらば：スレッド設定無し
			TestThreadTitle(
				"http://jbbs.shitaraba.net/bbs/read.cgi/game/45037/",
				"掲示板[ シュール・備長炭と愉快な仲間たち ] スレッドを選択してください");

			// 旧したらば：スレッド設定有り
			TestThreadTitle(
				"http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/1352127309/",
				"スレッド[ 【qwebview.html】当たり前だろ！シュールch75【流行らない・・】 ] (1000)");

			// 旧したらば：スレッド設定無し
			TestThreadTitle(
				"http://jbbs.livedoor.jp/bbs/read.cgi/game/45037/",
				"掲示板[ シュール・備長炭と愉快な仲間たち ] スレッドを選択してください");

			// YY：スレッド設定有り
			TestThreadTitle(
				"http://jbbs.livedoor.jp/bbs/read.cgi/netgame/11007/1343713121/",
				"スレッド[ シャクレ84cm ] (1000)");

			// YY：スレッド設定無し
			TestThreadTitle(
				"http://jbbs.livedoor.jp/bbs/read.cgi/netgame/11007/",
				"掲示板[ シャクレ ] スレッドを選択してください");

			// 2ch互換：スレッド設定有り
			/*
			TestThreadTitle(
				"http://sepia0330.dyndns.org/test/read.cgi/eicar/1346411572/l50",
				"スレッド[ これは酷いな142 ] (1001)");

			// 2ch互換：スレッド設定無し
			TestThreadTitle(
				"http://sepia0330.dyndns.org/eicar/",
				"掲示板[ これは酷いな避難所BBS ] スレッドを選択してください");
			 */

			// 未対応URL
			TestThreadTitle(
				"https://www.google.co.jp/",
				"未対応URLです");

			// 空
			TestThreadTitle(
				"",
				"URLが指定されていません");
		}

		//-------------------------------------------------------------
		// 概要：取得した選択スレッドタイトルと、想定結果の比較
		//-------------------------------------------------------------
		private static void TestThreadTitle(string url, string threadTitle)
		{
			// URLを設定
			WriteFieldControl writeField = new WriteFieldControl();
			writeField.SelectThreadUrl = url;

			for (int i = 0; i < 30; i++)
			{
				Application.DoEvents();
				Thread.Sleep(150);
			}

			// スレッドタイトルのチェック
			PrivateObject accessor = new PrivateObject(writeField);
			Label label = (Label)accessor.GetField("selectThreadLabel");
			Assert.AreEqual(threadTitle, label.Text);
		}
	}
}
