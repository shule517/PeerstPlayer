using PeerstLib.Bbs.Data;
using PeerstLib.Bbs.Strategy;
using PeerstLib.Util;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace PeerstLib.Bbs.Util
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
			if (uri.Host.StartsWith(ShitarabaBbsStrategy.Domain))
			{
				Logger.Instance.DebugFormat("掲示板サーバ：したらば [HOST:{0}]", uri.Host);
				return AnalyzeShitaraba(uri.Host, threadUrl);
			}
			// YY
			else if (uri.Host.StartsWith(YYKakikoBbsStrategy.Domain))
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
		private static BbsInfo AnalyzeShitaraba(string host, string url)
		{
			Logger.Instance.DebugFormat("AnalyzeShitaraba(url:{0})", url);

			const int threadUrlIndex = 0;
			const int genreIndex = 2;
			const int boardNoIndex = 3;
			const int threadNoIndex = 4;

			Regex regex = new Regex(String.Format(@"http://{0}(/bbs/read.cgi)?/(\w*)/(\w*)/?(\w*)?/?", host));
			Match match = regex.Match(url);

			string threadUrl = match.Groups[threadUrlIndex].Value;
			string genre = match.Groups[genreIndex].Value;
			string boardNo = match.Groups[boardNoIndex].Value;
			string threadNo = match.Groups[threadNoIndex].Value;
			return new BbsInfo
			{
				Host = host,
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

			Uri uri = new Uri(url);
			// スレッドURL
			if (url.IndexOf("test/read.cgi") != -1)
			{
				const int threadUrlIndex = 0;
				const int pathIndex = 1;
				const int boardNoIndex = 3;
				const int threadNoIndex = 4;

				string pattern = String.Format(@"/?(.*?)?/?(test/read.cgi)/?(\w*)/?(\w*)/?");
				Regex regex = new Regex(pattern);
				Match match = regex.Match(uri.AbsolutePath);

				string threadUrl = String.Format("{0}://{1}{2}", uri.Scheme, uri.Authority,
					match.Groups[threadUrlIndex]);
				string boardNo = match.Groups[boardNoIndex].Value;
				string threadNo = match.Groups[threadNoIndex].Value;

				if (!String.IsNullOrEmpty(match.Groups[pathIndex].Value) && !String.IsNullOrEmpty(host))
				{
					host += "/" + match.Groups[pathIndex].Value;
				}
				return new BbsInfo
				{
					Host = String.IsNullOrEmpty(host) ? null : host,
					Url = (threadUrl.EndsWith("/") ? threadUrl : String.Format("{0}/", threadUrl)),
					BoardGenre = String.IsNullOrEmpty(host) ? null : host,
					BoardNo = String.IsNullOrEmpty(boardNo) ? null : boardNo,
					ThreadNo = String.IsNullOrEmpty(threadNo) ? null : threadNo,
					BbsName = null,
					BbsServer = BbsServer.YYKakiko,
				};
			}
			// 掲示板URL
			else
			{
				const int threadUrlIndex = 0;
				const int pathIndex = 1;
				const int boardNoIndex = 2;

				string pattern = @"/?(.*?)/?(\w*)?/?$"; 
				Regex regex = new Regex(pattern);
				Match match = regex.Match(uri.AbsolutePath);

				string threadUrl = uri.ToString();
				string boardNo = match.Groups[boardNoIndex].Value;

				if (!String.IsNullOrEmpty(match.Groups[pathIndex].Value) && !String.IsNullOrEmpty(host))
				{
					host += "/" + match.Groups[pathIndex].Value;
				}
				return new BbsInfo
				{
					Host = String.IsNullOrEmpty(host) ? null : host,
					Url = (threadUrl.EndsWith("/") ? threadUrl : String.Format("{0}/", threadUrl)),
					BoardGenre = String.IsNullOrEmpty(host) ? null : host,
					BoardNo = String.IsNullOrEmpty(boardNo) ? null : boardNo,
					ThreadNo = null,
					BbsName = null,
					BbsServer = BbsServer.YYKakiko,
				};
			}
			

		}
	}
}
