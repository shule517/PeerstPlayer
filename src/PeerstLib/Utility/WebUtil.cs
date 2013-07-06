using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Text;
using System.Web;

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
				wc.Proxy = null; // 遅延回避のため
				byte[] data = wc.DownloadData(url);
				string result = encoding.GetString(data);
				result = HttpUtility.HtmlDecode(result);
				Logger.Instance.DebugFormat("HTMLの取得:正常");
				return result;
			}
			catch
			{
				Logger.Instance.Error("HTMLの取得:異常");
				return "";
			}
		}

		/// <summary>
		/// プロトコル違反を許容
		/// PeerCastがプロトコル違反しているバージョンがあるため、許容する
		/// </summary>
		public static void SettingDisableResponseError()
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			SettingsSection section = (SettingsSection)config.GetSection("system.net/settings");
			section.HttpWebRequest.UseUnsafeHeaderParsing = true;
			config.Save();
		}
	}
}
