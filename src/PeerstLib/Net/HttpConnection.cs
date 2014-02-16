using System;
using System.IO;
using System.Net;
using System.Text;

namespace PeerstLib.Net
{
	/// <summary>
	/// HTTP通信クラス
	/// </summary>
	public class HttpConnection
	{
		/// <summary>
		/// ユーザーエージェント
		/// </summary>
		const string UserAgent = "Peerst";

		/// <summary>
		/// GETリクエスト
		/// </summary>
		/// <param name="url">リクエストURL</param>
		/// <param name="modifiedSince">最終更新日時</param>
		/// <param name="encoding">文字エンコード</param>
		/// <returns>HTTPステータスコード</returns>
		public static RequestResult GetRequest(string url, DateTime modifiedSince, Encoding encoding)
		{
			HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
			req.UserAgent = UserAgent;
			req.IfModifiedSince = modifiedSince;

			try
			{
				using (HttpWebResponse response = req.GetResponse() as HttpWebResponse)
				using (Stream stream = response.GetResponseStream())
				using (StreamReader reader = new StreamReader(stream, encoding))
				{
					return new RequestResult(response.StatusCode, reader.ReadToEnd());
				}
			}
			catch (WebException ex)
			{
				HttpWebResponse response = ex.Response as HttpWebResponse;
				return new RequestResult(response.StatusCode, string.Empty);
			}
		}
	}
}
