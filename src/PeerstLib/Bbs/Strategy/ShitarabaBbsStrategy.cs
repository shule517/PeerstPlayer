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
	// 概要：したらば掲示板ストラテジ
	// 責務：したらば掲示板の操作を行う
	//-------------------------------------------------------------
	public class ShitarabaBbsStrategy : BbsStrategy
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// したらばのドメイン
		/// </summary>
		public const string Domain = "jbbs";

		/// <summary>
		/// ホスト
		/// </summary>
		private const string Host = "jbbs.shitaraba.net";

		/// <summary>
		/// スレッドURL
		/// </summary>
		public override string ThreadUrl
		{
			get
			{
				if (ThreadSelected)
				{
					return String.Format("http://{0}/bbs/read.cgi/{1}/{2}/{3}/", BbsInfo.Host, BbsInfo.BoardGenre, BbsInfo.BoardNo, BbsInfo.ThreadNo);
				}
				else
				{
					return String.Format("http://{0}/{1}/{2}/", BbsInfo.Host, BbsInfo.BoardGenre, BbsInfo.BoardNo);
				}
			}
		}

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// 掲示板エンコード
		/// </summary>
		protected override Encoding encoding { get { return Encoding.GetEncoding("EUC-JP"); } }

		/// <summary>
		/// スレッド一覧URL
		/// </summary>
		protected override string subjectUrl
		{
			get { return String.Format("http://{0}/{1}/{2}/subject.txt", BbsInfo.Host, BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		/// <summary>
		/// スレッド情報URL
		/// </summary>
		protected override string datUrl
		{
			get { return String.Format("http://{0}/bbs/rawmode.cgi/{1}/{2}/{3}/", BbsInfo.Host, BbsInfo.BoardGenre, BbsInfo.BoardNo, BbsInfo.ThreadNo); }
		}

		/// <summary>
		/// 板URL
		/// </summary>
		protected override string boardUrl
		{
			get { return String.Format("http://{0}/{1}/{2}/", BbsInfo.Host, BbsInfo.BoardGenre, BbsInfo.BoardNo); }
		}

		/// <summary>
		/// 書き込みリクエストURL
		/// </summary>
		protected override string writeUrl
		{
			get { return String.Format("http://{0}/bbs/write.cgi", Host); }
		}

		//-------------------------------------------------------------
		// 定義
		//-------------------------------------------------------------

		/// <summary>
		/// レスデータのインデックス
		/// </summary>
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

			List<ThreadInfo> threadList = new List<ThreadInfo>();

			foreach (string line in lines)
			{
				// 行末はスルー
				if (String.IsNullOrEmpty(line))
					continue;

				Regex regex = new Regex(@"(?<threadNo>[0-9]+)\.cgi,(?<threadTitle>.+)\((?<resCount>[0-9]+)\)");
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
		override protected List<ResInfo> AnalyzeDatText(string[] lines, bool isHtmlDecode)
		{
			Logger.Instance.DebugFormat("AnalyzeDatText(lines:{0})", lines);

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
					ResNo	= data[(int)DatIndex.ResNo],
					Date	= data[(int)DatIndex.Date],
					Id		= data[(int)DatIndex.Id],
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
