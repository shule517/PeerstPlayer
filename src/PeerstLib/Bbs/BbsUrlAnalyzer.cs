using System;
using System.Text;
using System.Text.RegularExpressions;
using PeerstLib.Utility;

namespace PeerstLib.Bbs
{
	//-------------------------------------------------------------
	// 概要：掲示板URL解析クラス
	// 責務：掲示板URLからデータを抽出する
	//-------------------------------------------------------------
	public static class BbsUrlAnalyzer
	{
		//-------------------------------------------------------------
		// 概要：URLの解析
		// 詳細：掲示板情報を取得する
		//-------------------------------------------------------------
		public static BbsInfo Analyze(string url)
		{
			// 未入力
			if (String.IsNullOrEmpty(url))
			{
				return new BbsInfo
				{
					Url = "",
					BoardGenre = null,
					BoardNo = null,
					ThreadNo = null,
					BbsName = null,
					BbsServer = BbsServer.UnSupport,
				}; 
			}

			try
			{
				Uri uri = new Uri(url);
				string host = uri.Host;

				// したらば
				if (host == "jbbs.livedoor.jp")
				{
					return AnalyzeShitaraba(url);
				}
				// YY
				else if (host.StartsWith("yy"))
				{
					return AnalayzeYY(url, host);
				}
				// 2ch互換かチェック
				else
				{
					// スレッド一覧ページが存在するかで2ch互換かチェック
					BbsInfo info = AnalayzeYY(url, host);
					string subjectUrl = string.Format("http://{0}/{1}/subject.txt", info.BoardGenre, info.BoardNo);
					string html = WebUtil.GetHtml(subjectUrl, Encoding.GetEncoding("Shift_JIS"));
					if (html.Substring(10, 6) == ".dat<>")
					{
						return info;
					}
				}
			}
			catch
			{
			}
	
			// 未対応
			return new BbsInfo
			{
				Url = url,
				BoardGenre = null,
				BoardNo = null,
				ThreadNo = null,
				BbsName = null,
				BbsServer = BbsServer.UnSupport,
			};
		}

		//-------------------------------------------------------------
		// 概要：したらばURLの解析
		// 詳細：掲示板情報を取得する
		//-------------------------------------------------------------
		private static BbsInfo AnalyzeShitaraba(string url)
		{
			const int threadUrlIndex = 0;
			const int genreIndex = 2;
			const int boardNoIndex = 3;
			const int threadNoIndex = 4;

			Regex regex = new Regex(@"http://jbbs.livedoor.jp(/bbs/read.cgi)?/(\w*)/(\w*)/?(\w*)?/?");
			Match match = regex.Match(url);

			string threadUrl = match.Groups[threadUrlIndex].Value;
			string genre = match.Groups[genreIndex].Value;
			string boardNo = match.Groups[boardNoIndex].Value;
			string threadNo = match.Groups[threadNoIndex].Value;
			return new BbsInfo
			{
				Url = (threadUrl.EndsWith("/") ? threadUrl : String.Format("{0}/", threadUrl)),
				BoardGenre = String.IsNullOrEmpty(genre) ? null : genre,
				BoardNo = String.IsNullOrEmpty(boardNo) ? null : boardNo,
				ThreadNo = String.IsNullOrEmpty(threadNo) ? null : threadNo,
				BbsName = null,
				BbsServer = BbsServer.Shitaraba,
			};
		}

		//-------------------------------------------------------------
		// 概要：わいわいKakikoURLの解析
		// 詳細：掲示板情報を取得する
		//-------------------------------------------------------------
		private static BbsInfo AnalayzeYY(string url, string host)
		{
			const int threadUrlIndex = 0;
			const int boardNoIndex = 2;
			const int threadNoIndex = 3;

			string pattern = String.Format(@"http://{0}/(test/read.cgi/)?(\w*)/?(\w*)?/?", host);
			Regex regex = new Regex(pattern);
			Match match = regex.Match(url);

			string threadUrl = match.Groups[threadUrlIndex].Value;
			string boardNo = match.Groups[boardNoIndex].Value;
			string threadNo = match.Groups[threadNoIndex].Value;
			return new BbsInfo
			{
				Url = (threadUrl.EndsWith("/") ? threadUrl : String.Format("{0}/", threadUrl)),
				BoardGenre = String.IsNullOrEmpty(host) ? null : host,
				BoardNo = String.IsNullOrEmpty(boardNo) ? null : boardNo,
				ThreadNo = String.IsNullOrEmpty(threadNo) ? null : threadNo,
				BbsName = null,
				BbsServer = BbsServer.YYKakiko,
			};
		}
	}
}
