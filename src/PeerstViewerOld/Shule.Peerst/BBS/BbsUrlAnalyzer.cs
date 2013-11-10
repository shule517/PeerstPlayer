using System.Text.RegularExpressions;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板URL解析クラス
	/// </summary>
	class BbsUrlAnalyzer
	{
		public void Analyze(string url, out BbsInfo bbsUrl)
		{
			if (url == "")
			{
				// 非対応
				bbsUrl = new BbsInfo(BbsServer.UnSupport, string.Empty, string.Empty, string.Empty); ;
				return;
			}

			// 空白：本スレ
			if (url == "本スレ")
			{
				// TODO 本スレのスレッド番号を指定する
				bbsUrl = new BbsInfo(BbsServer.YYKakiko, "yy25.60.kg", "peercastjikkyou", "本スレ");
				return;
				/*
				// TODO 本スレのスレッド番号を取得する
				// TODO 本スレの掲示板名の設定を行うか検討する

				BoadName = "今からpeercastでゲーム実況配信";

				List<string[]> threadList = GetThreadList(KindOfBBS, BoadGenre, BoadNo);
				for (int i = 0; i < threadList.Count; i++)
				{
					string title = threadList[i][1];
					if (title.Length >= 15)
					{
						string str = title.Substring(0, 15);
						if (str == "今からpeercastでゲーム")
						{
							ThreadNo = threadList[i][0];
							return;
						}
					}

				}
				return;
				 */
			}

			// 指定URLのラストに「/」を設定する
			if (url[url.Length - 1] != '/')
			{
				url += "/";
			}

			//================================
			// したらば掲示板のURLチェック
			//================================

			// スレッドURLかチェック
			if (GetThreadDataFromThreadURL(url, BbsServer.Shitaraba, @"http://jbbs.livedoor.jp/bbs/read.cgi/(\w*)/(\w*)/(\w*)/", out bbsUrl))
			{
				return;
			}

			// 板URLかチェック
			if (GetThreadDataFromBoardURL(url, BbsServer.Shitaraba, @"http://jbbs.livedoor.jp/(\w*)/(\w*)/", out bbsUrl))
			{
				return;
			}

			// 板URLかチェック
			if (GetThreadDataFromBoardURL(url, BbsServer.Shitaraba, @"http://jbbs.livedoor.jp/bbs/read.cgi/(\w*)/(\w*)/", out bbsUrl))
			{
				return;
			}

			//================================
			// YYかきこのURLチェック
			//================================

			// スレッドURLかチェック
			if (GetThreadDataFromThreadURL(url, BbsServer.YYKakiko, @"http://yy(\w*.*.*)/test/read.cgi/(\w*)/(\w*)/", out bbsUrl))
			{
				return;
			}

			// 板URLかチェック
			if (GetThreadDataFromBoardURL(url, BbsServer.YYKakiko, @"http://yy(\w*.*.*)/test/read.cgi/(\w*)/", out bbsUrl))
			{
				return;
			}

			// 板URLかチェック
			if (GetThreadDataFromBoardURL(url, BbsServer.YYKakiko, @"http://yy(\w*.*.*)/(\w*)/", out bbsUrl))
			{
				return;
			}

			// 掲示板非対応
			bbsUrl = new BbsInfo(BbsServer.UnSupport, string.Empty, string.Empty, string.Empty);
		}

		/// <summary>
		/// 板URLからデータを抽出
		/// </summary>
		private static bool GetThreadDataFromBoardURL(string url, BbsServer bbsServer, string pattern, out BbsInfo bbsUrl)
		{
			Regex regex = new Regex(pattern);
			Match match = regex.Match(url);

			// パターン一致確認
			if (match.Groups.Count == 3)
			{
				// 板ジャンル
				string boadGenre;
				if (bbsServer == BbsServer.YYKakiko)
				{
					boadGenre = "yy" + match.Groups[1].Value;
				}
				else
				{
					boadGenre = match.Groups[1].Value;
				}

				string boadNo = match.Groups[2].Value;

				bbsUrl = new BbsInfo(bbsServer, boadGenre, boadNo, string.Empty);
				return true;
			}

			bbsUrl = null;
			return false;
		}

		/// <summary>
		/// スレッドURLからデータを抽出
		/// </summary>
		private static bool GetThreadDataFromThreadURL(string url, BbsServer bbsServer, string pattern, out BbsInfo bbsUrl)
		{
			// 初期化
			Regex regex = new Regex(pattern);
			Match match = regex.Match(url);

			// パターン一致確認
			if (match.Groups.Count == 4)
			{
				// 板ジャンル
				string boadGenre;
				if (bbsServer == BbsServer.YYKakiko)
				{
					boadGenre = "yy" + match.Groups[1].Value;
				}
				else
				{
					boadGenre = match.Groups[1].Value;
				}

				string boadNo = match.Groups[2].Value;
				string threadNo = match.Groups[3].Value;

				bbsUrl = new BbsInfo(bbsServer, boadGenre, boadNo, threadNo);
				return true;
			}

			bbsUrl = null;
			return false;
		}
	}
}
