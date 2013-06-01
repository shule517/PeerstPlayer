using System.Net;
using System.Text;

namespace PeerstLib.Utility
{
	//-------------------------------------------------------------
	// 概要：Web関連のUtilityクラス
	//-------------------------------------------------------------
	static public class WebUtil
	{
		//-------------------------------------------------------------
		// 概要：HTMLの取得
		// 詳細：指定URLからHTMLコードを取得する
		//-------------------------------------------------------------
		public static string GetHtml(string url, Encoding encoding)
		{
			try
			{
				WebClient wc = new WebClient();
				byte[] data = wc.DownloadData(url);
				return encoding.GetString(data);
			}
			catch
			{
				return "";
			}
		}
	}
}
