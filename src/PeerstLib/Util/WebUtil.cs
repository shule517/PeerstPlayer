using System;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace PeerstLib.Util
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
			Logger.Instance.DebugFormat("GetHtml(url:{0}, encoding:{1})", url, encoding);

			try
			{
				WebClient wc = new WebClient();
				wc.Proxy = null; // 遅延回避のため
				byte[] data = wc.DownloadData(url);
				string result = encoding.GetString(data);
				Logger.Instance.DebugFormat("HTMLの取得:正常 [url:{0}]", url);
				return result;
			}
			catch
			{
				Logger.Instance.ErrorFormat("HTMLの取得:異常 [url:{0}]", url);
				return string.Empty;
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

		/// <summary>
		/// HTMLタグを除去
		/// <>ではさまれている文字列を削除して返す
		/// </summary>
		public static string DeleteHtmlTag(string text)
		{
			return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
		}

		/// <summary>
		/// コマンド送信
		/// </summary>
		public static void SendCommand(string host, int portNo, string url, Encoding encoding)
		{
			Logger.Instance.DebugFormat("SendCommand(host:{0}, portNo:{1}, url:{2}, encoding:{3})", host, portNo, url, encoding);

			try
			{
				// 送信するデータの作成
				string sendMessage = string.Format("GET {0} HTTP/1.0\r\n\r\n", url);
				byte[] sendBytes = encoding.GetBytes(sendMessage);

				// データ送信
				TcpClient tcp = new TcpClient(host, portNo);
				NetworkStream ns = tcp.GetStream();
				ns.Write(sendBytes, 0, sendBytes.Length);
			}
			catch (Exception e)
			{
				Logger.Instance.ErrorFormat("コマンドの送信に失敗 [error:{0}]", e.Message);
			}
		}
	}
}
