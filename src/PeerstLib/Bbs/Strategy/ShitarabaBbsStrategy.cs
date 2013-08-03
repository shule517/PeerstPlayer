using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using PeerstLib.Bbs.Data;
using PeerstLib.Utility;
using System.Linq;
using System.Text.RegularExpressions;

namespace PeerstLib.Bbs.Strategy
{
	//-------------------------------------------------------------
	// 概要：したらば掲示板ストラテジ
	// 責務：したらば掲示板の操作を行う
	//-------------------------------------------------------------
	public class ShitarabaBbsStrategy : BbsStrategy
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// したらばのドメイン
		public const string Domain = "jbbs.livedoor.jp";

		// スレッドURL
		public override string ThreadUrl
		{
			get
			{
				if (ThreadSelected)
				{
					return String.Format("http://{0}/bbs/read.cgi/{1}/{2}/{3}/", Domain, BbsInfo.BoardGenre, BbsInfo.BoardNo, BbsInfo.ThreadNo);
				}
				else
				{
					return String.Format("http://{0}/{1}/{2}/", Domain, BbsInfo.BoardGenre, BbsInfo.BoardNo);
				}
			}
		}

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// 掲示板エンコード
		protected override Encoding encoding { get { return Encoding.GetEncoding("EUC-JP"); } }

		// スレッド一覧URL
		protected override string subjectUrl
		{
			get { return String.Format("http://{0}/{1}/{2}/subject.txt", Domain, BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		// スレッド情報URL
		protected override string datUrl
		{
			get { return String.Format("http://{0}/bbs/rawmode.cgi/{1}/{2}/{3}/", Domain, BbsInfo.BoardGenre, BbsInfo.BoardNo, BbsInfo.ThreadNo); }
		}

		// 板URL
		protected override string boardUrl
		{
			get { return String.Format("http://{0}/{1}/{2}/", Domain, BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		// 書き込みリクエストURL
		protected override string writeUrl
		{
			get { return String.Format("http://{0}/bbs/write.cgi", Domain); }
		}

		//-------------------------------------------------------------
		// 定義
		//-------------------------------------------------------------

		// レスデータのインデックス
		enum DatIndex : int
		{
			ResNo = 0,
			Name,
			Mail,
			Date,
			Message,
			ThreadName,
			Id,
		};

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：掲示板情報の初期化
		//-------------------------------------------------------------
		public ShitarabaBbsStrategy(BbsInfo bbsInfo)
			: base(bbsInfo)
		{
			Logger.Instance.DebugFormat("ShitarabaBbsStrategy(url:{0})", bbsInfo.Url);
		}

		//-------------------------------------------------------------
		// 概要：スレッド一覧解析
		// 詳細：subject.txtからスレッド一覧情報を作成する
		//-------------------------------------------------------------
		override protected List<ThreadInfo> AnalyzeSubjectText(string[] lines)
		{
			Logger.Instance.DebugFormat("AnalyzeSubjectText(lines:{0})", lines);

			const int threadNoStart = 0;
			const int threadNoEnd = 10;
			const int cgiLength = 5;
			const int titleStart = threadNoEnd + cgiLength;

			List<ThreadInfo> threadList = new List<ThreadInfo>();

			foreach (string line in lines)
			{
				if (String.IsNullOrEmpty(line))
					continue;

				// スレッドタイトル抽出
				int titleEnd = line.LastIndexOf('(');
				string threadTitle = line.Substring(titleStart, titleEnd - titleStart);

				// レス数抽出
				int resStart = titleEnd + 1;
				string resCount = line.Substring(resStart, (line.Length - resStart - 1));
				string threadNo = line.Substring(threadNoStart, threadNoEnd);
				double days = BbsUtil.GetThreadSince(threadNo);

				ThreadInfo threadInfo = new ThreadInfo
				{
					ThreadNo = threadNo,
					ThreadTitle = threadTitle.Trim(),
					ResCount = int.Parse(resCount),
					ThreadSpeed = BbsUtil.GetThreadSpeed(days, resCount),
					ThreadSince = days
				};
				threadList.Add(threadInfo);
			}

			// 末尾を削除(不要なデータ)
			if (threadList.Count > 0)
			{
				threadList.RemoveAt(threadList.Count - 1);
			}

			return threadList;
		}

		//-------------------------------------------------------------
		// 概要：書き込み用リクエストデータ作成
		//-------------------------------------------------------------
		override protected byte[] CreateWriteRequestData(string name, string mail, string message)
		{
			Logger.Instance.DebugFormat("CreateWriteRequestData(name:{0}, mail:{1}, message:{2})", name, mail, message);
			StringBuilder param = new StringBuilder();

			param.AppendFormat("DIR={0}&",		HttpUtility.UrlEncode(BbsInfo.BoardGenre,	encoding));
			param.AppendFormat("BBS={0}&",		HttpUtility.UrlEncode(BbsInfo.BoardNo,		encoding));
			param.AppendFormat("KEY={0}&",		HttpUtility.UrlEncode(BbsInfo.ThreadNo,		encoding));
			param.AppendFormat("NAME={0}&",		HttpUtility.UrlEncode(name,					encoding));
			param.AppendFormat("MAIL={0}&",		HttpUtility.UrlEncode(mail,					encoding));
			param.AppendFormat("MESSAGE={0}&",	HttpUtility.UrlEncode(message,				encoding));
			param.AppendFormat("SUBMIT={0}&",	HttpUtility.UrlEncode("書き込む",			encoding));

			return Encoding.ASCII.GetBytes(param.ToString());
		}

		//-------------------------------------------------------------
		// 概要：スレッドデータ解析
		// 詳細：datからレス一覧情報を作成する
		//-------------------------------------------------------------
		override protected List<ResInfo> AnalyzeDatText(string[] lines)
		{
			Logger.Instance.DebugFormat("AnalyzeDatText(lines:{0})", lines);

			List<ResInfo> resList = new List<ResInfo>();

			foreach (var line in lines.Select((v, i) => new { v, i }))
			{
				if (String.IsNullOrEmpty(line.v))
					continue;

				String[] data = line.v.Split(new[] { "<>" }, StringSplitOptions.None);
				
				ResInfo resInfo = new ResInfo
				{
					ResNo	= data[(int)DatIndex.ResNo],
					Date	= data[(int)DatIndex.Date],
					Id		= data[(int)DatIndex.Id],
					Mail	= HttpUtility.HtmlDecode(data[(int)DatIndex.Mail]),
					Message	= HttpUtility.HtmlDecode(data[(int)DatIndex.Message]),
					Name	= HttpUtility.HtmlDecode(data[(int)DatIndex.Name]),
				};
				resList.Add(resInfo);
			}

			return resList;
		}
	}
}
