using System.Text;
using System.Web;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// わいわいKakikoストラテジクラス
	/// </summary>
	class YYKakikoBbsStrategy : BbsStrategy
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
			Encoding encode = Encoding.GetEncoding("Shift_JIS");
			string param = ""; // リクエストデータ

			param += "bbs=" + HttpUtility.UrlEncode(bbsUrl.BoadNo, encode) + "&";			// 板番号
			param += "key=" + HttpUtility.UrlEncode(bbsUrl.ThreadNo, encode) + "&";		// スレ番号
			param += "FROM=" + HttpUtility.UrlEncode(name, encode) + "&";			// 名前
			param += "mail=" + HttpUtility.UrlEncode(mail, encode) + "&";			// メール
			param += "MESSAGE=" + HttpUtility.UrlEncode(message, encode) + "&";		// 本文
			param += "submit=" + HttpUtility.UrlEncode("書き込む", encode) + "&";	// 書き込む

			return Encoding.ASCII.GetBytes(param);
		}

		/// <summary>
		/// リクエストURLを取得
		/// </summary>
		protected override string GetRequestURL()
		{
			return "http://" + bbsUrl.BoadGenre + "/test/bbs.cgi";
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
