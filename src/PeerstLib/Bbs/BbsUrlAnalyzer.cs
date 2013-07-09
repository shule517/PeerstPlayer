using System;
using System.Text;
using System.Text.RegularExpressions;
using PeerstLib.Bbs.Data;
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
		public static BbsInfo Analyze(string threadUrl)
		{
			Logger.Instance.DebugFormat("Analyze(url:{0})", threadUrl);

			Uri uri = null;
			try
			{
				uri = new Uri(threadUrl);
			}
			catch
			{
				Logger.Instance.WarnFormat("不正なURLです。[uri:{0}]", threadUrl);
				return new BbsInfo
				{
					Url = "",
					BbsServer = BbsServer.UnSupport,
				};
			}

			// したらば
			if (uri.Host == "jbbs.livedoor.jp")
			{
				Logger.Instance.DebugFormat("掲示板サーバ：したらば [HOST:{0}]", uri.Host);
				return AnalyzeShitaraba(threadUrl);
			}
			// YY
			else if (uri.Host.StartsWith("yy"))
			{
				Logger.Instance.DebugFormat("掲示板サーバ：わいわいKakiko [HOST:{0}]", uri.Host);
				return AnalayzeYY(threadUrl, uri.Host);
			}
			// 2ch互換かチェック
			else
			{
				// スレッド一覧ページが存在するかで2ch互換かチェック
				BbsInfo info = AnalayzeYY(threadUrl, uri.Host);
				string subjectUrl = string.Format("http://{0}/{1}/subject.txt", info.BoardGenre, info.BoardNo);
				string html = WebUtil.GetHtml(subjectUrl, Encoding.GetEncoding("Shift_JIS"));

				if ((html.Length > 16) && (html.Substring(10, 6) == ".dat<>"))
				{
					Logger.Instance.DebugFormat("掲示板サーバ：2ch互換サーバ [HOST:{0}]", uri.Host);
					return info;
				}
			}

			// 未対応
			Logger.Instance.DebugFormat("掲示板サーバ：未対応 [HOST:{0}]", uri.Host);
			return new BbsInfo
			{
				Url = threadUrl,
				BbsServer = BbsServer.UnSupport,
			};
		}

		//-------------------------------------------------------------
		// 概要：したらばURLの解析
		// 詳細：掲示板情報を取得する
		//-------------------------------------------------------------
		private static BbsInfo AnalyzeShitaraba(string url)
		{
			Logger.Instance.DebugFormat("AnalyzeShitaraba(url:{0})", url);

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
			Logger.Instance.DebugFormat("AnalayzeYY(url:{0}, host:{1})", url, host);

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
