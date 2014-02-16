
using System.Net;
namespace PeerstLib.Net
{
	/// <summary>
	/// HTTPリクエスト結果
	/// </summary>
	public class RequestResult
	{
		/// <summary>
		/// ステータスコード
		/// </summary>
		public HttpStatusCode HttpStatusCode { get { return httpStatusCode; } }
		private readonly HttpStatusCode httpStatusCode;
		
		/// <summary>
		/// 本文
		/// </summary>
		public string BodyText { get { return bodyText; } }
		private readonly string bodyText;

		public RequestResult(HttpStatusCode httpStatusCode, string bodyText)
		{
			this.httpStatusCode = httpStatusCode;
			this.bodyText = bodyText;
		}
	}
}
