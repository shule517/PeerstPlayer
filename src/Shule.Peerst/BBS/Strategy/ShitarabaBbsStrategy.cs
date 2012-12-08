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
		/// 掲示板書き込みリクエスト用データ作成
		/// </summary>
		/// <param name="name"></param>
		/// <param name="mail"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		protected override byte[] CreateWriteRequestData(string name, string mail, string message)
		{
			Encoding encode; // エンコード
			encode = Encoding.GetEncoding("EUC-JP");
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

		protected override void ReadDat()
		{
			//string url = "http://jbbs.livedoor.jp/bbs/rawmode.cgi/" + BoadGenre + "/" + BoadNo + "/" + ThreadNo + "/" + ResNum.ToString() + "-";
		}

		protected override void AnalyzeData()
		{
		}
	}
}
