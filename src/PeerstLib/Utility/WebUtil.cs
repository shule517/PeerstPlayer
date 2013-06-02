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
			Logger.Instance.DebugFormat("GetHtml(url:{0}, encoding:{1}", url, encoding);

			try
			{
				WebClient wc = new WebClient();
				byte[] data = wc.DownloadData(url);
				string result = encoding.GetString(data);
				Logger.Instance.DebugFormat("HTMLの取得:正常");
				return result;
			}
			catch
			{
				Logger.Instance.Error("HTMLの取得:異常");
				return "";
			}
		}
	}
}
