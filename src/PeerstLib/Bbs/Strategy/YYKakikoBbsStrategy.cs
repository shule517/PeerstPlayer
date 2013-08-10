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
	// 概要：YYかきこ掲示板ストラテジ
	// 責務：YYかきこ掲示板の操作を行う
	//-------------------------------------------------------------
	public class YYKakikoBbsStrategy : BbsStrategy
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// YYかきこのドメイン
		public const string Domain = "yy";

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
		// 定義
		//-------------------------------------------------------------

		// レスデータのインデックス
		enum DatIndex : int
		{
			Name = 0,
			Mail,
			DateAndId,
			Message,
			ThreadName,
		};

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：掲示板情報の初期化
		//-------------------------------------------------------------
		public YYKakikoBbsStrategy(BbsInfo bbsInfo)
			: base(bbsInfo)
		{
			Logger.Instance.DebugFormat("YYKakikoBbsStrategy(url:{0})", bbsInfo.Url);
		}

		//-------------------------------------------------------------
		// 概要：スレッド一覧解析
		// 詳細：subject.txtからスレッド一覧情報を作成する
		//-------------------------------------------------------------
		override protected List<ThreadInfo> AnalyzeSubjectText(string[] lines)
		{
			Logger.Instance.DebugFormat("AnalyzeSubjectText(lines:{0})", lines);

			List<ThreadInfo> threadList = new List<ThreadInfo>();

			foreach (string line in lines)
			{
				// 行末はスルー
				if (String.IsNullOrEmpty(line))
					continue;

				Regex regex = new Regex(@"(?<threadNo>[0-9]+)\.dat<>(?<threadTitle>.+)\((?<resCount>[0-9]+)\)");
				Match match = regex.Match(line);

				// 正規表現がヒットしなければスルー
				if (match.Success == false) { continue; }

				// データ抽出
				string threadNo = match.Groups["threadNo"].Value;
				string resCount = match.Groups["resCount"].Value;
				double days = BbsUtil.GetThreadSince(threadNo);
				ThreadInfo threadInfo = new ThreadInfo
				{
					ThreadNo = threadNo,
					ThreadTitle = match.Groups["threadTitle"].Value.Trim(),
					ResCount = int.Parse(resCount),
					ThreadSpeed = BbsUtil.GetThreadSpeed(days, resCount),
					ThreadSince = days
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
			Logger.Instance.DebugFormat("CreateWriteRequestData(name:{0}, mail:{1}, message:{2})", name, mail, message);
			StringBuilder param = new StringBuilder();

			param.AppendFormat("bbs={0}&", HttpUtility.UrlEncode(BbsInfo.BoardNo, encoding)); // 板番号
			param.AppendFormat("key={0}&", HttpUtility.UrlEncode(BbsInfo.ThreadNo, encoding)); // スレ番号
			param.AppendFormat("FROM={0}&", HttpUtility.UrlEncode(name, encoding)); // 名前
			param.AppendFormat("mail={0}&", HttpUtility.UrlEncode(mail, encoding)); // メール
			param.AppendFormat("MESSAGE={0}&", HttpUtility.UrlEncode(message, encoding)); // 本文
			param.AppendFormat("submit={0}&", HttpUtility.UrlEncode("書き込む", encoding)); // 書き込む

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
					ResNo	= (line.i + 1).ToString(),
					Date	= data[(int)DatIndex.DateAndId],
					Id		= "", // TODO IDを取得する
					Mail	= HttpUtility.HtmlDecode(data[(int)DatIndex.Mail]),
					Message = HttpUtility.HtmlDecode(data[(int)DatIndex.Message]),
					Name	= HttpUtility.HtmlDecode(data[(int)DatIndex.Name]),
				};
				resList.Add(resInfo);
			}

			return resList;
		}
	}
}
