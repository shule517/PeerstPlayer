using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PeerstLib.Bbs.Strategy
{
	//-------------------------------------------------------------
	// 概要：YYかきこ掲示板ストラテジ
	// 責務：YYかきこ掲示板の操作を行う
	//-------------------------------------------------------------
	public class YYKakikoBbsStrategy : BbsStrategy
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// スレッドURL
		public override string ThreadUrl
		{
			get
			{
				if (ThreadSelected)
				{
					return String.Format("http://{0}/test/read.cgi/{1}/{2}/", BbsInfo.BoardGenre, BbsInfo.BoardNo, BbsInfo.ThreadNo);
				}
				else
				{
					return String.Format("http://{0}/{1}/", BbsInfo.BoardGenre, BbsInfo.BoardNo);
				}
			}
		}

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// 掲示板エンコード
		protected override Encoding encoding
		{
			get { return Encoding.GetEncoding("Shift_JIS"); }
		}

		// 掲示板一覧URL
		protected override string subjectUrl
		{
			get { return String.Format("http://{0}/{1}/subject.txt", BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		// スレッド情報URL
		protected override string datUrl
		{
			get { return String.Format("http://{0}/{1}/dat/{2}.dat", BbsInfo.BoardGenre, BbsInfo.BoardNo, BbsInfo.ThreadNo); }
		}

		// 板URL
		protected override string boardUrl
		{
			get { return String.Format("http://{0}/{1}/", BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		// 書き込みリクエストURL
		protected override string writeUrl
		{
			get { return "http://" + BbsInfo.BoardGenre + "/test/bbs.cgi"; }
		}

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：掲示板情報の初期化
		//-------------------------------------------------------------
		public YYKakikoBbsStrategy(BbsInfo bbsInfo)
			: base(bbsInfo)
		{
		}

		//-------------------------------------------------------------
		// 概要：スレッド一覧解析
		// 詳細：subject.txtからスレッド一覧情報を作成する
		//-------------------------------------------------------------
		override protected List<ThreadInfo> AnalyzeSubjectText(string[] lines)
		{
			const int threadNoStart = 0;
			const int threadNoEnd = 10;
			const int cgiLength = 6;
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

			return threadList;
		}

		//-------------------------------------------------------------
		// 概要：書き込み用リクエストデータ作成
		//-------------------------------------------------------------
		override protected byte[] CreateWriteRequestData(string name, string mail, string message)
		{
			StringBuilder param = new StringBuilder();

			param.AppendFormat("bbs={0}&", HttpUtility.UrlEncode(BbsInfo.BoardNo, encoding)); // 板番号
			param.AppendFormat("key={0}&", HttpUtility.UrlEncode(BbsInfo.ThreadNo, encoding)); // スレ番号
			param.AppendFormat("FROM={0}&", HttpUtility.UrlEncode(name, encoding)); // 名前
			param.AppendFormat("mail={0}&", HttpUtility.UrlEncode(mail, encoding)); // メール
			param.AppendFormat("MESSAGE={0}&", HttpUtility.UrlEncode(message, encoding)); // 本文
			param.AppendFormat("submit={0}&", HttpUtility.UrlEncode("書き込む", encoding)); // 書き込む

			return Encoding.ASCII.GetBytes(param.ToString());
		}
	}
}
