using System.Linq;
using PeerstLib.Bbs.Data;
using PeerstLib.Bbs.Util;
using PeerstLib.Util;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace PeerstLib.Bbs.Strategy
{
	//-------------------------------------------------------------
	// 概要：掲示板ストラテジクラス
	//-------------------------------------------------------------
	public abstract class BbsStrategy
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// 掲示板情報クラス
		/// </summary>
		public BbsInfo BbsInfo { get; set; }

		/// <summary>
		/// スレッド一覧
		/// </summary>
		public List<ThreadInfo> ThreadList { get; set; }

		/// <summary>
		/// レス一覧
		/// </summary>
		public List<ResInfo> ResList { get; set; }

		/// <summary>
		/// スレッド選択状態
		/// </summary>
		public bool ThreadSelected
		{
			get { return (!string.IsNullOrEmpty(BbsInfo.ThreadNo)); }
		}

		/// <summary>
		/// スレッドURL
		/// </summary>
		public abstract string ThreadUrl { get; }

		/// <summary>
		/// 板URL
		/// </summary>
		public abstract string BoardUrl { get; }

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// 掲示板のエンコード
		/// </summary>
		protected abstract Encoding encoding { get; }

		/// <summary>
		/// 掲示板設定のURL
		/// </summary>
		protected abstract string settingUrl { get; }

		/// <summary>
		/// スレッド一覧情報URL
		/// </summary>
		protected abstract string subjectUrl { get; }

		/// <summary>
		/// スレッド情報取得
		/// </summary>
		protected abstract string datUrl { get; }

		/// <summary>
		/// 板URL
		/// </summary>
		protected abstract string boardUrl { get; }

		/// <summary>
		/// 書き込みリクエストURL
		/// </summary>
		protected abstract string writeUrl { get; }

		/// <summary>
		/// 書き込みタイムアウト時間
		/// </summary>
		private int WriteResTimeOut = 10*1000;

		//-------------------------------------------------------------
		// 概要：スレッド変更
		//-------------------------------------------------------------
		public void ChangeThread(string threadNo)
		{
			Logger.Instance.DebugFormat("ChangeThread(threadNo:{0})", threadNo);
			BbsInfo.ThreadNo = threadNo;
		}

		//-------------------------------------------------------------
		// 概要：スレッド一覧更新
		//-------------------------------------------------------------
		public void UpdateThreadList()
		{
			Logger.Instance.DebugFormat("UpdateThreadList()");
			string subjectText = WebUtil.GetHtml(subjectUrl, encoding);
			string[] lines = subjectText.Replace("\r\n", "\n").Split('\n');
			ThreadList = AnalyzeSubjectText(lines);

			Logger.Instance.DebugFormat("スレッド一覧取得：正常 [スレッド取得数：{0}]", ThreadList.Count);
		}

		//-------------------------------------------------------------
		// 概要：SETTING.txt
		//-------------------------------------------------------------
		public virtual void UpdateBbsSetting()
		{
			Logger.Instance.DebugFormat("UpdateBbsSetting()");
			var settingText = WebUtil.GetHtml(settingUrl, encoding);
			var regex = new Regex("(.+?)=(.+)");
			var lines = settingText.Replace("\r\n", "\n").Split('\n');
			foreach (var match in lines.Select(line => regex.Match(line)).Where(match => match.Success))
			{
				BbsInfo.Setting[match.Groups[1].Value] = match.Groups[2].Value;
			}

			Logger.Instance.DebugFormat("掲示板設定取得：正常");
		}

		//-------------------------------------------------------------
		// 概要：掲示板名の更新
		//-------------------------------------------------------------
		public void UpdateBbsName()
		{
			Logger.Instance.DebugFormat("UpdateBbsName()");
			string html = WebUtil.GetHtml(boardUrl, encoding);

			Regex regex = new Regex("<title>(.*)</title>");
			Match match = regex.Match(html);

			// 取得成功
			if (match.Groups.Count > 1)
			{
				BbsInfo.BbsName = match.Groups[1].Value;
				Logger.Instance.DebugFormat("掲示板名取得:正常 [掲示板名:{0}]", BbsInfo.BbsName);
				return;
			}

			// 取得失敗
			BbsInfo.BbsName = string.Empty;
			Logger.Instance.ErrorFormat("掲示板名取得:異常 [指定URL:{0}]", BbsInfo.Url);
		}

		//-------------------------------------------------------------
		// 概要：レス書き込み
		//-------------------------------------------------------------
		public string Write(string name, string mail, string message)
		{
			Logger.Instance.DebugFormat("Write(name:{0}, mail:{1}, message:{2})", name, mail, message);

			// POSTデータ作成
			byte[] requestData = CreateWriteRequestData(name, mail, message);

			// リクエスト作成
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(writeUrl);
			webRequest.Proxy = null;
			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.ContentLength = requestData.Length;
			webRequest.Referer = ThreadUrl;
			webRequest.Timeout = WriteResTimeOut;
			webRequest.UserAgent = "Monazilla/1.00";

			// POST送信
			Stream requestStream = webRequest.GetRequestStream();
			requestStream.Write(requestData, 0, requestData.Length);
			requestStream.Close();

			string response = "";
			try
			{
				// リクエスト受信
				HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
				Stream responseStream = webResponse.GetResponseStream();
				StreamReader sr = new StreamReader(responseStream, encoding);
				response = sr.ReadToEnd();
				sr.Close();
				responseStream.Close();
			}
			catch
			{
				throw;
			}

			// 書き込みエラーチェック
			return BbsUtil.CheckWriteError(response);
		}

		//-------------------------------------------------------------
		// 概要：レス読み込み
		//-------------------------------------------------------------
		public void ReadThread(bool isHtmlDecode)
		{
			if (!ThreadSelected)
			{
				// スレッド未選択
				ResList = new List<ResInfo>();
				return;
			}

			Logger.Instance.DebugFormat("ReadThread()");

			string datText = WebUtil.GetHtml(datUrl, encoding);
			string[] lines = datText.Replace("\r\n", "\n").Split('\n');
			ResList = AnalyzeDatText(lines, isHtmlDecode);

			Logger.Instance.DebugFormat("レス一覧取得：正常 [レス取得数：{0}]", ResList.Count);
		}

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		//-------------------------------------------------------------
		protected BbsStrategy(BbsInfo bbsInfo)
		{
			Logger.Instance.DebugFormat("BbsStrategy(url:{0})", bbsInfo.Url);
			ThreadList = new List<ThreadInfo>();
			ResList = new List<ResInfo>();
			this.BbsInfo = bbsInfo;
		}

		//-------------------------------------------------------------
		// 概要：スレッド一覧解析
		//-------------------------------------------------------------
		protected abstract List<ThreadInfo> AnalyzeSubjectText(string[] lines);

		//-------------------------------------------------------------
		// 概要：スレッドデータ解析
		//-------------------------------------------------------------
		protected abstract List<ResInfo> AnalyzeDatText(string[] lines, bool isHtmlDecode);

		//-------------------------------------------------------------
		// 概要：書き込み用リクエストデータ作成
		//-------------------------------------------------------------
		protected abstract byte[] CreateWriteRequestData(string name, string mail, string message);
	}
}
