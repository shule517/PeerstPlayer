using Shule.Peerst.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// したらば掲示板ストラテジクラス
	/// </summary>
	class ShitarabaBbsStrategy : BbsStrategy
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="bbsUrl">掲示板アドレス情報</param>
		public ShitarabaBbsStrategy(BbsInfo bbsUrl)
		: base(bbsUrl)
		{
		}
		
		/// <summary>
		/// 掲示板書き込みリクエスト用データ作成
		/// </summary>
		/// <param name="name"></param>
		/// <param name="mail"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		protected override byte[] CreateWriteRequestData(string name, string mail, string message)
		{
			Encoding encode = GetEncode(); // エンコード
			string param = ""; // データ配列

			param += "DIR=" + HttpUtility.UrlEncode(bbsUrl.BoadGenre, encode) + "&"; // 板ジャンル
			param += "BBS=" + HttpUtility.UrlEncode(bbsUrl.BoadNo, encode) + "&"; // 板番号
			param += "KEY=" + HttpUtility.UrlEncode(bbsUrl.ThreadNo, encode) + "&"; // スレ番号
			param += "NAME=" + HttpUtility.UrlEncode(name, encode) + "&"; // 名前
			param += "MAIL=" + HttpUtility.UrlEncode(mail, encode) + "&"; // メール
			param += "MESSAGE=" + HttpUtility.UrlEncode(message, encode) + "&"; // 本文
			param += "SUBMIT=" + HttpUtility.UrlEncode("書き込む", encode) + "&"; // 書き込む

			return Encoding.ASCII.GetBytes(param);
		}

		/// <summary>
		/// リクエストURLを取得
		/// </summary>
		protected override string GetRequestURL()
		{
			return "http://jbbs.livedoor.jp/bbs/write.cgi";
		}

		/// <summary>
		/// サブジェクトURL(スレッド一覧)の取得
		/// </summary>
		protected override string GetSubjectUrl()
		{
			return "http://jbbs.livedoor.jp/" + bbsUrl.BoadGenre + "/" + bbsUrl.BoadNo + "/subject.txt";
		}

		/// <summary>
		/// サブジェクトURLのスプリット文字の取得
		/// </summary>
		protected override string GetSubjectSplit()
		{
			return ".cgi,";
		}

		/// <summary>
		/// スレッド読み込み
		/// </summary>
		/// <returns>スレッド情報一覧</returns>
		public override List<ResInfo> ReadThread(string threadNo, int resNo)
		{
			List<ResInfo> threadData = new List<ResInfo>();

			// datの取得
			// http://jbbs.livedoor.jp/bbs/rawmode.cgi/game/41324/1260939060/930-
			string url = "http://jbbs.livedoor.jp/bbs/rawmode.cgi/" + bbsUrl.BoadGenre + "/" + bbsUrl.BoadNo + "/" + threadNo + "/" + resNo.ToString() + "-"; // TODO 検討の必要あり
			string html = WebUtility.GetHtml(url, GetEncode());

			// 本文の修正
			// html = html.Replace("<a", @"<u><font color=""blue""");
			// html = html.Replace("</a>", "</font></u>");
			// html = html.Replace("<br>", "<br><DD>");
			// html = html.Replace("<br>", "<br>");
			html = html.Replace("<a", "<tt");
			html = html.Replace("</a>", "</tt>");

			// リンクの書き換え
			// TODO html = LinkReplace(html);

			// 1レスごと区切る
			string[] ResList = html.Split('\n');

			// データごと区切る
			string[] split = new string[1];
			split[0] = "<>";
			for (int i = 0; i < ResList.Length; i++)
			{
				string[] ResData = ResList[i].Split(split, StringSplitOptions.None);
				if (ResData.Length == 7)
				{
					// 投稿者がレス番号か
					int num = 0;
					if (true == int.TryParse(ResData[1], out num))
					{
						ResData[1] = "<tt>" + ResData[1] + "</tt>";
					}

					// したらばの名前太文字バグを対策
					// 「名前</b>ID<b>」と取得されるので、<\b>を追加する
					ResData[1] = ResData[1].Replace("<b>", "").Replace("</b>", "");

					// データ作成
					ResInfo data = new ResInfo();
					data.Name = ResData[1]; // 名前
					data.Mail = ResData[2]; // メール
					data.Date = ResData[3]; // 日付
					data.ID = ResData[6]; // ID
					data.Text = ResData[4]; // 本文

					// レスデータを追加
					threadData.Add(data);
				}
			}

			return threadData;
		}

		/// <summary>
		/// 文字エンコードの取得
		/// </summary>
		protected override Encoding GetEncode()
		{
			return Encoding.GetEncoding("EUC-JP");
		}

		/// <summary>
		/// 板URLを取得
		/// </summary>
		protected override string GetBoadUrl()
		{
			return "http://jbbs.livedoor.jp/" + bbsUrl.BoadGenre + "/" + bbsUrl.BoadNo + "/";
		}
	}
}
