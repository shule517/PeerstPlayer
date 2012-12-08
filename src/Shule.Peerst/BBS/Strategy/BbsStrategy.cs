using System.IO;
using System.Net;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板ストラテジ抽象クラス
	/// </summary>
	abstract class BbsStrategy
	{
		protected BbsUrl bbsUrl;	// 掲示板アドレス情報

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BbsStrategy()
		{
			bbsUrl = new BbsUrl();
			// TODO URL情報を解析して設定する
			bbsUrl.BoadGenre = "game";
			bbsUrl.BoadNo = "45037";
			bbsUrl.ThreadNo = "1340716570";
		}

		/// <summary>
		/// 掲示板書き込み
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="mail">メール欄</param>
		/// <param name="message">本文</param>
		public void Write(string name, string mail, string message)
		{
			// POSTデータ作成
			string requestUrl = GetRequestURL(); // リクエストURL
			byte[] data = CreateWriteRequestData(name, mail, message);

			// リクエスト作成
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = data.Length;
			request.Referer = requestUrl;

			// POST送信
			Stream requestStream = request.GetRequestStream();
			requestStream.Write(data, 0, data.Length);
			requestStream.Close();

			/*
			// TODO エラーチェックを行う
			// リクエスト受信
			WebResponse response = request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader sr;
			sr = new StreamReader(responseStream, encode);
			string html = sr.ReadToEnd();
			sr.Close();
			responseStream.Close();

			#region エラーチェック

			if ((html.IndexOf("ERROR") != -1) || (html.IndexOf("ＥＲＲＯＲ") != -1))
			{
				if (html.IndexOf("リンクＵＲＬ") != -1)
				{
					// したらば
					MessageBox.Show("書き込みエラー\nリンクＵＲＬを含む投稿を許可しない設定になっています。", "Error!!");
					return false;
				}
				else if (html.IndexOf("多重書き込み") != -1)
				{
					// したらば
					//<html>
					//<head>
					//<title>ERROR!!</title>

					//</head>
					//<body bgcolor="#FFFFFF"><!-- 2ch_X:error -->
					//<table width="100%" border="1" cellspacing="0" cellpadding="10">
					//<tr><td><b>ERROR!!<br><br>多重書き込みです。 あと 29秒お待ちください。</b></td></tr>
					//</table>
					//<hr size=1>
					//<div align="right"><a href="http://rentalbbs.livedoor.com/">livedoor したらば掲示板 (無料レンタル)</a></div>
					//</body>
					//</html>

					int s = html.IndexOf("多重書き込み");
					int e = html.IndexOf("お待ちください。");

					if (s == -1 || e == -1)
					{
						MessageBox.Show("書き込みエラー\n連投規制です。しばらくしてから書き込み直してください。", "Error!!");
						return false;
					}
					else
					{
						string error = html.Substring(s, e - s + 8);
						MessageBox.Show("書き込みエラー\n" + error, "Error!!");
						return false;
					}
				}
				else if (html.IndexOf("たたないと") != -1)
				{
					// YY
					//<html><head><title>ＥＲＲＯＲ！</title><meta http-equiv="Content-Type" content="text/html; charset=Shift_JIS"></head>
					//<body><!-- 2ch_X:error -->
					//ＥＲＲＯＲ - 593 3 sec たたないと書けません。(1回目、1 sec しかたってない) 1
					//<br>
					//<hr>
					//</body>
					//</html>
					//					MessageBox.Show("書き込みエラー\n連投規制です。しばらくしてから書き込み直してください。", "Error!!");
					int s = html.IndexOf("ＥＲＲＯＲ -");
					int e = html.IndexOf("しかたってない");

					if (s == -1 || e == -1)
					{
						MessageBox.Show("書き込みエラー\n連投規制です。しばらくしてから書き込み直してください。", "Error!!");
						return false;
					}
					else
					{
						string error = html.Substring(s + 12, (e + 8) - (s + 12));
						MessageBox.Show("書き込みエラー\n" + error, "Error!!");
						return false;
					}

				}
				else if (html.IndexOf("スレッドストップ") != -1 || html.IndexOf("書き込めないスレッド") != -1)
				{
					MessageBox.Show("書き込みエラー\nスレッドストップです。スレッドを変更してください。", "Error!!");
					return false;
				}
				else
				{
					MessageBox.Show("書き込みエラー", "Error!!");
					return false;
				}
			}

			#endregion

			return true;
			*/
		}


		/// <summary>
		/// 掲示板書き込みリクエスト用データ作成
		/// </summary>
		protected abstract byte[] CreateWriteRequestData(string name, string mail, string message);

		/// <summary>
		/// リクエストURLの取得
		/// </summary>
		protected abstract string GetRequestURL();

		public void ReadThread()
		{
		}
		protected abstract void ReadDat();
		protected abstract void AnalyzeData();
	}
}
