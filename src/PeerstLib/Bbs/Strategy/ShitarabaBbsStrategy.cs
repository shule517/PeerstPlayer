using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PeerstLib.Bbs.Strategy
{
	// したらば掲示板ストラテジ
	public class ShitarabaBbsStrategy : BbsStrategy
	{
		public ShitarabaBbsStrategy(BbsInfo bbsInfo) : base(bbsInfo)
		{
		}

		protected override Encoding encoding { get { return Encoding.GetEncoding("EUC-JP"); } }

		public override string ThreadUrl
		{
			get
			{
				if (ThreadSelected)
				{
					return String.Format("http://jbbs.livedoor.jp/bbs/read.cgi/{0}/{1}/{2}/", BbsInfo.BoardGenre, BbsInfo.BoardNo, BbsInfo.ThreadNo);
				}
				else
				{
					return String.Format("http://jbbs.livedoor.jp/{0}/{1}/", BbsInfo.BoardGenre, BbsInfo.BoardNo);
				}
			}
		}

		protected override string subjectUrl
		{
			get { return String.Format("http://jbbs.livedoor.jp/{0}/{1}/subject.txt", BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		protected override string datUrl
		{
			get { return String.Format("http://jbbs.livedoor.jp/bbs/rawmode.cgi/{0}/{1}/{2}/", BbsInfo.BoardGenre, BbsInfo.BoardNo, BbsInfo.ThreadNo); }
		}

		protected override string boardUrl
		{
			get { return String.Format("http://jbbs.livedoor.jp/{0}/{1}/", BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		protected override string writeUrl
		{
			get { return "http://jbbs.livedoor.jp/bbs/write.cgi"; }
		}

		//-------------------------------------------------------------
		// 概要：スレッド一覧解析
		// 詳細：subject.txtからスレッド一覧情報を作成する
		//-------------------------------------------------------------
		override protected List<ThreadInfo> AnalyzeSubjectText(string[] lines)
		{
			const int threadNoStart = 0;
			const int threadNoEnd = 10;
			const int cgiLength = 5;
			const int titleStart = threadNoEnd + cgiLength;

			List<ThreadInfo> threadList = new List<ThreadInfo>();

			foreach (string line in lines)
			{
				if (String.IsNullOrEmpty(line))
					continue;

				// スレッドタイトル
				int titleEnd = line.LastIndexOf('(');
				string threadTitle = line.Substring(titleStart, titleEnd - titleStart);

				// レス数
				int resStart = titleEnd + 1;
				string resCount = line.Substring(resStart, (line.Length - resStart - 1));

				ThreadInfo threadInfo = new ThreadInfo
				{
					ThreadNo = line.Substring(threadNoStart, threadNoEnd),
					ThreadTitle = threadTitle.Trim(),
					ResCount = int.Parse(resCount),
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

		// 書き込み用リクエストデータ作成
		override protected byte[] CreateWriteRequestData(string name, string mail, string message)
		{
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
	}
}
