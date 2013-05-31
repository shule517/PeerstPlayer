using PeerstLib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Web;
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

		// 掲示板情報クラス
		public BbsInfo BbsInfo { get; set; }

		// スレッド一覧
		public List<ThreadInfo> ThreadList { get; set; }

		// スレッド選択状態
		public bool ThreadSelected
		{
			get { return (!string.IsNullOrEmpty(BbsInfo.ThreadNo)); }
		}

		// スレッドURL
		public abstract string ThreadUrl { get; }

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// 掲示板のエンコード
		protected abstract Encoding encoding { get; }

		// スレッド一覧情報URL
		protected abstract string subjectUrl { get; }

		// スレッド情報取得
		protected abstract string datUrl { get; }

		// 板URL
		protected abstract string boardUrl { get; }

		// 書き込みリクエストURL
		protected abstract string writeUrl { get; }

		//-------------------------------------------------------------
		// 概要：スレッド変更
		//-------------------------------------------------------------
		public void ChangeThread(string threadNo)
		{
			BbsInfo.ThreadNo = threadNo;
		}

		//-------------------------------------------------------------
		// 概要：スレッド一覧更新
		//-------------------------------------------------------------
		public void UpdateThreadList()
		{
			string subjectText = WebUtil.GetHtml(subjectUrl, encoding);
			string[] lines = subjectText.Replace("\r\n", "\n").Split('\n');
			ThreadList = AnalyzeSubjectText(lines);
		}

		//-------------------------------------------------------------
		// 概要：掲示板名の更新
		//-------------------------------------------------------------
		public void UpdateBbsName()
		{
			string html = WebUtil.GetHtml(boardUrl, encoding);

			Regex regex = new Regex("<title>(.*)</title>");
			Match match = regex.Match(html);

			// 取得成功
			if (match.Groups.Count > 1)
			{
				BbsInfo.BbsName = match.Groups[1].Value;
				return;
			}

			// 取得失敗
			BbsInfo.BbsName = string.Empty;
		}

		//-------------------------------------------------------------
		// 概要：レス書き込み
		//-------------------------------------------------------------
		public void Write(string name, string mail, string message)
		{
			// POSTデータ作成
			byte[] requestData = CreateWriteRequestData(name, mail, message);

			// リクエスト作成
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(writeUrl);
			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.ContentLength = requestData.Length;
			webRequest.Referer = writeUrl;

			// POST送信
			Stream requestStream = webRequest.GetRequestStream();
			requestStream.Write(requestData, 0, requestData.Length);
			requestStream.Close();

			// リクエスト受信
			HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
			Stream responseStream = webResponse.GetResponseStream();
			StreamReader sr = new StreamReader(responseStream, encoding);
			string html = sr.ReadToEnd();
			sr.Close();
			responseStream.Close();

			// 書き込みエラーチェック
			CheckWriteError(html);
		}

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		//-------------------------------------------------------------
		protected BbsStrategy(BbsInfo bbsInfo)
		{
			this.BbsInfo = bbsInfo;
		}

		//-------------------------------------------------------------
		// 概要：スレッド一覧解析
		//-------------------------------------------------------------
		protected abstract List<ThreadInfo> AnalyzeSubjectText(string[] lines);

		//-------------------------------------------------------------
		// 概要：書き込み用リクエストデータ作成
		//-------------------------------------------------------------
		protected abstract byte[] CreateWriteRequestData(string name, string mail, string message);

		//-------------------------------------------------------------
		// 概要：書き込みエラーチェック
		//-------------------------------------------------------------
		private void CheckWriteError(string html)
		{
			Regex regex = new Regex("<title>(.*)</title>");
			Match match = regex.Match(html);
			string title = match.Groups[1].Value;

			// 書き込み失敗
			if (title.StartsWith("ERROR") || title.StartsWith("ＥＲＲＯＲ"))
			{
				throw new Exception();
			}
		}
	}
}
