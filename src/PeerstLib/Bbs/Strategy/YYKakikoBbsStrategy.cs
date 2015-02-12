using PeerstLib.Bbs.Data;
using PeerstLib.Bbs.Util;
using PeerstLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

		/// <summary>
		/// YYかきこのドメイン
		/// </summary>
		public const string Domain = "yy";

		/// <summary>
		/// スレッドURL
		/// </summary>
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

		/// <summary>
		/// 板URL
		/// </summary>
		public override string BoardUrl
		{
			get { return String.Format("http://{0}/{1}/", BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// 掲示板エンコード
		/// </summary>
		protected override Encoding encoding
		{
			get { return Encoding.GetEncoding("Shift_JIS"); }
		}

		/// <summary>
		/// 掲示板設定のURL
		/// </summary>
		protected override string settingUrl
		{
			get { return string.Format("http://{0}/{1}/SETTING.txt", BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		/// <summary>
		/// 掲示板一覧URL
		/// </summary>
		protected override string subjectUrl
		{
			get { return String.Format("http://{0}/{1}/subject.txt", BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		/// <summary>
		/// スレッド情報URL
		/// </summary>
		protected override string datUrl
		{
			get { return String.Format("http://{0}/{1}/dat/{2}.dat", BbsInfo.BoardGenre, BbsInfo.BoardNo, BbsInfo.ThreadNo); }
		}

		/// <summary>
		/// 板URL
		/// </summary>
		protected override string boardUrl
		{
			get { return String.Format("http://{0}/{1}/", BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		/// <summary>
		/// 書き込みリクエストURL
		/// </summary>
		protected override string writeUrl
		{
			get { return "http://" + BbsInfo.BoardGenre + "/test/bbs.cgi"; }
		}

		//-------------------------------------------------------------
		// 定義
		//-------------------------------------------------------------

		/// <summary>
		/// レスデータのインデックス
		/// </summary>
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
				ThreadInfo threadInfo = new ThreadInfo(BbsInfo)
				{
					ThreadNo = threadNo,
					ThreadTitle = match.Groups["threadTitle"].Value.Trim(),
					ResCount = int.Parse(resCount),
					ThreadSpeed = BbsUtil.GetThreadSpeed(days, resCount),
					ThreadSince = days
				};
				threadList.Add(threadInfo);
			}
			// 1002レス以上のスレッドがあれば最大レス数を10000として扱う
			BbsInfo.ThreadStop = threadList.Any(x => x.ResCount >= 1002) ? 10000 : 1000;

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
		override protected List<ResInfo> AnalyzeDatText(string[] lines, bool isHtmlDecode)
		{
			Logger.Instance.DebugFormat("AnalyzeDatText(isHtmlDecode:{0})", isHtmlDecode);

			List<ResInfo> resList = new List<ResInfo>();

			foreach (var line in lines.Select((v, i) => new { v, i }))
			{
				if (String.IsNullOrEmpty(line.v))
					continue;

				String[] data = line.v.Split(new[] { "<>" }, StringSplitOptions.None);

				string mail = data[(int)DatIndex.Mail];
				string message = data[(int)DatIndex.Message];
				string name = data[(int)DatIndex.Name];
				ResInfo resInfo = new ResInfo
				{
					ResNo	= (line.i + 1).ToString(),
					Date	= data[(int)DatIndex.DateAndId],
					Id		= "", // TODO IDを取得する
					Mail	= isHtmlDecode ? HttpUtility.HtmlDecode(mail) : mail,
					Message = isHtmlDecode ? HttpUtility.HtmlDecode(message) : message,
					Name	= isHtmlDecode ? HttpUtility.HtmlDecode(name) : name,
				};
				resList.Add(resInfo);
			}

			return resList;
		}
	}
}
