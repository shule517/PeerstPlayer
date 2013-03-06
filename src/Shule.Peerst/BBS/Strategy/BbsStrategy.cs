using Shule.Peerst.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板ストラテジ抽象クラス
	/// </summary>
	abstract class BbsStrategy : IBbsStrategy
	{
		protected BbsInfo bbsUrl;	// 掲示板アドレス情報

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BbsStrategy(BbsInfo bbsUrl)
		{
			this.bbsUrl = bbsUrl;
		}

		/// <summary>
		/// スレッド変更
		/// </summary>
		public void ChangeThread(string threadNo)
		{
			bbsUrl.ThreadNo = threadNo;
		}

		/// <summary>
		/// 掲示板URL取得
		/// </summary>
		public BbsInfo GetBbsUrl()
		{
			return bbsUrl;
		}

		/// <summary>
		/// スレッド一覧の取得
		/// </summary>
		public List<ThreadInfo> GetThreadList()
		{
			List<ThreadInfo> threadList = new List<ThreadInfo>();
			string[] subjects;

			// Subject.txtの取得
			GetSubject(out subjects);

			// スレッドデータ作成
			for (int i = 0; i < subjects.Length - 1; i += 2)
			{
				string[] text = new string[3];
				int index = subjects[i + 1].LastIndexOf('(');
				string threadTitle = subjects[i + 1].Substring(0, index);
				string resNum = subjects[i + 1].Substring(index + 1, subjects[i + 1].Length - index - 2);
				string threadNo = subjects[i];
				ThreadInfo thread = new ThreadInfo(threadTitle, threadNo, resNum);

				// 追加
				threadList.Add(thread);
			}

			return threadList;
		}

		/// <summary>
		/// スレッド一覧の取得
		/// </summary>
		public ThreadInfo GetThreadInfo(string threadNo)
		{
			string[] list;
			GetSubject(out list);

			for (int i = 0; i < list.Length - 1; i += 2)
			{
				// スレッドデータ作成
				string thread = list[i];
				if (threadNo == thread)
				{
					int index = list[i + 1].LastIndexOf('(');
					string threadTitle = list[i + 1].Substring(0, index);
					string resNum = list[i + 1].Substring(index + 1, list[i + 1].Length - index - 2);
					return new ThreadInfo(threadTitle, thread, resNum);
				}
			}

			return new ThreadInfo("", threadNo, "");
		}

		/// <summary>
		/// 掲示板名を取得
		/// </summary>
		public string GetBbsName()
		{
			// 板名を取得：<title>板名</title>
			string html = WebUtility.GetHtml(GetBoadUrl(), GetEncode());

			int startPos = html.IndexOf("<title>");
			int endPos = html.IndexOf("</title>");

			if ((startPos == -1) || (endPos == -1))
				return "";

			int tagSize = "<title>".Length;

			return html.Substring(startPos + tagSize, (endPos - startPos) - tagSize);
		}

		/// <summary>
		/// 掲示板書き込み
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="mail">メール欄</param>
		/// <param name="message">本文</param>
		public bool Write(string name, string mail, string message)
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

			// TODO エラーチェックを行うクラスへ移行する
			// TODO エラーメッセージ表示をメッセージボックスから変更
			// リクエスト受信
			WebResponse response = request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader sr = new StreamReader(responseStream, GetEncode());
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

					if ((s == -1) || (e == -1))
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
		}
	
		/// <summary>
		/// Subject.txtの取得
		/// </summary>
		private void GetSubject(out string[] subjectArray)
		{
			// subject.txtを取得
			string subject_html = WebUtility.GetHtml(GetSubjectUrl(), GetEncode());

			// 区切り文字作成
			string[] separator = new string[2];
			separator[0] = "\n";
			separator[1] = GetSubjectSplit();

			subjectArray = subject_html.Split(separator, StringSplitOptions.None);

			// したらばの場合は、最下位のデータは削除する
			// 最上位のデータと同じため
			if (bbsUrl.BBSServer == BbsServer.Shitaraba)
			{
				Array.Resize(ref subjectArray, subjectArray.Length - 3);
			}
		}

		/// <summary>
		/// 掲示板書き込みリクエスト用データ作成
		/// </summary>
		protected abstract byte[] CreateWriteRequestData(string name, string mail, string message);

		/// <summary>
		/// リクエストURLの取得
		/// </summary>
		protected abstract string GetRequestURL();

		/// <summary>
		/// サブジェクトURL(スレッド一覧)の取得
		/// </summary>
		protected abstract string GetSubjectUrl();

		/// <summary>
		/// サブジェクトURLのスプリット文字の取得
		/// </summary>
		protected abstract string GetSubjectSplit();

		/// <summary>
		/// スレッド読み込み
		/// </summary>
		public abstract List<ResInfo> ReadThread(string threadNo);

		/// <summary>
		/// 文字エンコードを取得
		/// </summary>
		protected abstract Encoding GetEncode();

		/// <summary>
		/// 板URLを取得
		/// </summary>
		protected abstract string GetBoadUrl();
	}
}
