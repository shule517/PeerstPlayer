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
	// 掲示板ストラテジクラス
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
		abstract public string ThreadUrl { get; }

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// エンコード
		abstract protected Encoding encoding { get; }

		// スレッド一覧情報URL
		abstract protected string subjectUrl { get; }

		// スレッド情報取得
		abstract protected string datUrl { get; }

		// 板URL
		abstract protected string boardUrl { get; }

		// 書き込みリクエストURL
		abstract protected string writeUrl { get; }

		//-------------------------------------------------------------
		// 公開メソッド
		//-------------------------------------------------------------

		// スレッド変更
		public void ChangeThread(string threadNo)
		{
			BbsInfo.ThreadNo = threadNo;
		}

		// スレッド一覧更新
		public void UpdateThreadList()
		{
			string subjectText = WebUtil.GetHtml(subjectUrl, encoding);
			string[] lines = subjectText.Replace("\r\n", "\n").Split('\n');
			ThreadList = AnalyzeSubjectText(lines);
		}

		// 掲示板名の更新
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

		// レス書き込み
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
		// 非公開メソッド
		//-------------------------------------------------------------

		// コンストラクタ
		protected BbsStrategy(BbsInfo bbsInfo)
		{
			this.BbsInfo = bbsInfo;
		}

		// スレッド一覧解析
		abstract protected List<ThreadInfo> AnalyzeSubjectText(string[] lines);

		// 書き込み用リクエストデータ作成
		abstract protected byte[] CreateWriteRequestData(string name, string mail, string message);

		// 書き込みエラーチェック
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
