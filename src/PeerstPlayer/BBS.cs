using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PeerstPlayer
{
	public class BBS
	{
		/// <summary>
		/// 書き込み
		/// </summary>
		/// <param name="bbs">板の種類</param>
		/// <param name="BoadGenre">板ジャンル</param>
		/// <param name="BoadNo">板番号</param>
		/// <param name="ThreadNo">スレッド番号</param>
		/// <returns>書き込み成功したらtrue</returns>
		/// <param name="name">名前</param>
		/// <param name="mail">メール</param>
		/// <param name="message">本文</param>
		public static bool Write(KindOfBBS bbs, string BoadGenre, string BoadNo, string ThreadNo, string name, string mail, string message)
		{
			// POSTデータ作成
			Encoding encode; // エンコード
			string param = ""; // データ配列
			string requestUrl = ""; // リクエストURL
			byte[] data; // データ

			if (bbs == KindOfBBS.JBBS)
			{
				encode = Encoding.GetEncoding("EUC-JP");

				param += "DIR=" + HttpUtility.UrlEncode(BoadGenre, encode) + "&"; // 板ジャンル
				param += "BBS=" + HttpUtility.UrlEncode(BoadNo, encode) + "&"; // 板番号
				param += "KEY=" + HttpUtility.UrlEncode(ThreadNo, encode) + "&"; // スレ番号
				param += "NAME=" + HttpUtility.UrlEncode(name, encode) + "&"; // 名前
				param += "MAIL=" + HttpUtility.UrlEncode(mail, encode) + "&"; // メール
				param += "MESSAGE=" + HttpUtility.UrlEncode(message, encode) + "&"; // 本文
				param += "SUBMIT=" + HttpUtility.UrlEncode("書き込む", encode) + "&"; // 書き込む

				requestUrl = "http://jbbs.livedoor.jp/bbs/write.cgi";
				data = Encoding.ASCII.GetBytes(param);
			}
			else if (bbs == KindOfBBS.YYKakiko)
			{
				encode = Encoding.GetEncoding("Shift_JIS");

				param += "bbs=" + HttpUtility.UrlEncode(BoadNo, encode) + "&"; // 板番号
				param += "key=" + HttpUtility.UrlEncode(ThreadNo, encode) + "&"; // スレ番号
				param += "FROM=" + HttpUtility.UrlEncode(name, encode) + "&"; // 名前
				param += "mail=" + HttpUtility.UrlEncode(mail, encode) + "&"; // メール
				param += "MESSAGE=" + HttpUtility.UrlEncode(message, encode) + "&"; // 本文
				param += "submit=" + HttpUtility.UrlEncode("書き込む", encode) + "&"; // 書き込む

				requestUrl = "http://" + BoadGenre + "/test/bbs.cgi";
				data = Encoding.ASCII.GetBytes(param);
			}
			else
			{
				return false;
			}

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
					/*
					<html>
					<head>
					<title>ERROR!!</title>

					</head>
					<body bgcolor="#FFFFFF"><!-- 2ch_X:error -->
					<table width="100%" border="1" cellspacing="0" cellpadding="10">
					<tr><td><b>ERROR!!<br><br>多重書き込みです。 あと 29秒お待ちください。</b></td></tr>
					</table>
					<hr size=1>
					<div align="right"><a href="http://rentalbbs.livedoor.com/">livedoor したらば掲示板 (無料レンタル)</a></div>
					</body>
					</html>
					*/

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
					/*
					<html><head><title>ＥＲＲＯＲ！</title><meta http-equiv="Content-Type" content="text/html; charset=Shift_JIS"></head>
					<body><!-- 2ch_X:error -->
					ＥＲＲＯＲ - 593 3 sec たたないと書けません。(1回目、1 sec しかたってない) 1
					<br>
					<hr>
					</body>
					</html>
					*/
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
		}

		/// <summary>
		/// リンクの書き換え
		/// </summary>
		private static string LinkReplace(string text)
		{
			string[] list = text.Split('\n');
			text = "";

			for (int i = 0; i < list.Length; i++)
			{
				Regex regex = new Regex(@"(h?ttps?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+)");
				Match match = regex.Match(list[i]);

				while (match.Groups.Count == 2)
				{
					// ttpをhttpに変換
					string url = "";
					if (match.Groups[0].Value[0] == 't')
						url = "h" + match.Groups[0].Value;
					else
						url = match.Groups[0].Value;

					list[i] = list[i].Replace(match.Groups[0].Value, "<u>" + url + "</u>");
					match = match.NextMatch();
				}

				text += list[i] + "\n";
			}

			return text;
		}

		/// <summary>
		/// スレッドを読み込む（レス番号～
		/// </summary>
		/// <param name="bbs">板の種類</param>
		/// <param name="BoadGenre">板ジャンル</param>
		/// <param name="BoadNo">板番号</param>
		/// <param name="ThreadNo">スレッド番号</param>
		/// <param name="ResNum">レス番号～</param>
		/// <returns>取得した内容（HTML）</returns>
		public static List<string[]> ReadThread(KindOfBBS bbs, string BoadGenre, string BoadNo, string ThreadNo, int ResNum)
		{
			try
			{
				List<string[]> ThreadData = new List<string[]>();

				// したらば
				if (bbs == KindOfBBS.JBBS)
				{
					// datの取得
					// http://jbbs.livedoor.jp/bbs/rawmode.cgi/game/41324/1260939060/930-
					string url = "http://jbbs.livedoor.jp/bbs/rawmode.cgi/" + BoadGenre + "/" + BoadNo + "/" + ThreadNo + "/" + ResNum.ToString() + "-";
					string html = HTTP.GetHtml(url, "EUC-JP");

					// 本文の修正
					// html = html.Replace("<a", @"<u><font color=""blue""");
					// html = html.Replace("</a>", "</font></u>");
					// html = html.Replace("<br>", "<br><DD>");
					// html = html.Replace("<br>", "<br>");
					html = html.Replace("<a", "<tt");
					html = html.Replace("</a>", "</tt>");

					// リンクの書き換え
					html = LinkReplace(html);

					// 1レスごと区切る
					string[] ResList = html.Split('\n');

					// データごと区切る
					string[] split = new string[1];
					split[0] = "<>";
					for (int i = 0; i < ResList.Length; i++)
					{
						string[] ResData = ResList[i].Split(split, StringSplitOptions.None);
						if (ResData.Length == 7)
						{
							// 投稿者がレス番号か
							int num = 0;
							if (true == int.TryParse(ResData[1], out num))
							{
								ResData[1] = "<tt>" + ResData[1] + "</tt>";
							}

							// したらばの名前太文字バグを対策
							// 「名前</b>ID<b>」と取得されるので、<\b>を追加する
							ResData[1] = ResData[1].Replace("<b>", "").Replace("</b>", "");

							// データ作成
							string[] data = new string[5];
							data[0] = ResData[1]; // 名前
							data[1] = ResData[2]; // メール
							data[2] = ResData[3]; // 日付
							data[3] = ResData[6]; // ID
							data[4] = ResData[4]; // 本文

							// レスデータを追加
							ThreadData.Add(data);
						}
					}
				}
				// YY
				else if (bbs == KindOfBBS.YYKakiko)
				{
					// datの取得
					// http://yy67.60.kg/ff11peca/dat/1263298996.dat
					string url = "http://" + BoadGenre + "/" + BoadNo + "/dat/" + ThreadNo + ".dat";
					string html = HTTP.GetHtml(url, "Shift_JIS");

					// 指定レス(ResNum)まで飛ばす
					int find = 0;
					for (int i = 0; i < ResNum - 1; i++)
					{
						find = html.IndexOf('\n', find) + 1;
					}
					html = html.Substring(find);

					// 本文の修正
					// html = html.Replace("<a", @"<u><font color=""blue""");
					// html = html.Replace("</a>", "</font></u>");
					//html = html.Replace("<br>", "<br><DD>");
					//html = html.Replace("<br>", "<DD>");
					html = html.Replace("<a", "<tt");
					html = html.Replace("</a>", "</tt>");

					// リンクの書き換え
					html = LinkReplace(html);

					// 1レスごと区切る
					string[] ResList = html.Split('\n');

					// データごと区切る
					string[] split = new string[1];
					split[0] = "<>";
					for (int i = 0; i < ResList.Length; i++)
					{
						string[] ResData = ResList[i].Split(split, StringSplitOptions.None);
						if (ResData.Length == 5)
						{
							// 名前の太文字バグを除去
							ResData[0] = ResData[0].Replace("<b>", "").Replace("</b>", "");

							string id = "";
							string date = "";

							int index = ResData[2].LastIndexOf("ID:");
							if (index != -1)
							{
								id = ResData[2].Substring(index + 3, ResData[2].Length - (index + 3));
								date = ResData[2].Substring(0, index - 1);
							}
							else
							{
								date = ResData[2];
							}

							// データ作成
							string[] data = new string[5];
							data[0] = ResData[0]; // 名前
							data[1] = ResData[1]; // メール
							data[2] = date; // 日付
							data[3] = id; // ID
							data[4] = ResData[3]; // 本文

							// レスデータを追加
							ThreadData.Add(data);
						}
					}
				}

				return ThreadData;
			}
			catch
			{
			}

			return new List<string[]>();
		}

		/// <summary>
		/// URLからデータを取得
		/// </summary>
		public static void GetDataFromUrl(string url, out string BoadName, out KindOfBBS KindOfBBS, out string BoadGenre, out string BoadNo, out string ThreadNo)
		{
			// 初期化
			KindOfBBS = KindOfBBS.None;
			BoadGenre = "";
			BoadNo = "";
			ThreadNo = "";
			BoadName = "";

			if (url == "")
				return;

			// 空白：本スレ
			if (url == "本スレ")
			{
				KindOfBBS = KindOfBBS.YYKakiko;
				BoadGenre = "yy25.60.kg";
				BoadNo = "peercastjikkyou";
				ThreadNo = "";
				BoadName = "今からpeercastでゲーム実況配信";

				List<string[]> threadList = GetThreadList(KindOfBBS, BoadGenre, BoadNo);
				for (int i = 0; i < threadList.Count; i++)
				{
					string title = threadList[i][1];
					if (title.Length >= 15)
					{
						string str = title.Substring(0, 15);
						if (str == "今からpeercastでゲーム")
						{
							ThreadNo = threadList[i][0];
							return;
						}
					}

				}
				return;
			}

			// ラスト「/」にする
			if (url[url.Length - 1] != '/')
			{
				url += "/";
			}

			// ◆ したらば ◆
			// スレッド
			if (GetThreadDataFromThreadURL(url, KindOfBBS.JBBS, @"http://jbbs.livedoor.jp/bbs/read.cgi/(\w*)/(\w*)/(\w*)/", out KindOfBBS, out BoadGenre, out BoadNo, out ThreadNo))
			{
				BoadName = GetBoadName("http://jbbs.livedoor.jp/" + BoadGenre + "/" + BoadNo, "EUC-JP"); // 板名を取得
				return;
			}

			// 板
			if (GetThreadDataFromBoardURL(url, KindOfBBS.JBBS, @"http://jbbs.livedoor.jp/(\w*)/(\w*)/", out KindOfBBS, out BoadGenre, out BoadNo, out ThreadNo))
			{
				BoadName = GetBoadName("http://jbbs.livedoor.jp/" + BoadGenre + "/" + BoadNo, "EUC-JP"); // 板名を取得
				return;
			}

			// 板２
			if (GetThreadDataFromBoardURL(url, KindOfBBS.JBBS, @"http://jbbs.livedoor.jp/bbs/read.cgi/(\w*)/(\w*)/", out KindOfBBS, out BoadGenre, out BoadNo, out ThreadNo))
			{
				BoadName = GetBoadName("http://jbbs.livedoor.jp/" + BoadGenre + "/" + BoadNo, "EUC-JP"); // 板名を取得
				return;
			}

			// ◆ YY ◆
			// スレッド
			if (GetThreadDataFromThreadURL(url, KindOfBBS.YYKakiko, @"http://yy(\w*.*.*)/test/read.cgi/(\w*)/(\w*)/", out KindOfBBS, out BoadGenre, out BoadNo, out ThreadNo))
			{
				BoadName = GetBoadName("http://" + BoadGenre + "/" + BoadNo, "Shift_JIS"); // 板名を取得
				return;
			}

			// 間違ったURL
			if (GetThreadDataFromBoardURL(url, KindOfBBS.YYKakiko, @"http://yy(\w*.*.*)/test/read.cgi/(\w*)/", out KindOfBBS, out BoadGenre, out BoadNo, out ThreadNo))
			{
				BoadName = GetBoadName("http://" + BoadGenre + "/" + BoadNo, "Shift_JIS"); // 板名を取得
				return;
			}

			// 板
			if (GetThreadDataFromBoardURL(url, KindOfBBS.YYKakiko, @"http://yy(\w*.*.*)/(\w*)/", out KindOfBBS, out BoadGenre, out BoadNo, out ThreadNo))
			{
				BoadName = GetBoadName("http://" + BoadGenre + "/" + BoadNo, "Shift_JIS"); // 板名を取得
				return;
			}

			// 掲示板非対応
		}

		/// <summary>
		/// 板URLからデータを抽出
		/// </summary>
		private static bool GetThreadDataFromBoardURL(string url, KindOfBBS bbs, string pattern, out KindOfBBS KindOfBBS, out string BoadGenre, out string BoadNo, out string ThreadNo)
		{
			// 初期化
			KindOfBBS = KindOfBBS.None;
			BoadGenre = "";
			BoadNo = "";
			ThreadNo = "";

			Regex regex = new Regex(pattern);
			Match match = regex.Match(url);

			// 一致
			if (match.Groups.Count == 3)
			{
				// データセット
				if (bbs == KindOfBBS.YYKakiko)
					BoadGenre = "yy" + match.Groups[1].Value;
				else
					BoadGenre = match.Groups[1].Value;

				KindOfBBS = bbs;
				BoadNo = match.Groups[2].Value;

				return true;
			}

			return false;
		}

		/// <summary>
		/// スレッドURLからデータを抽出
		/// </summary>
		private static bool GetThreadDataFromThreadURL(string url, KindOfBBS bbs, string pattern, out KindOfBBS KindOfBBS, out string BoadGenre, out string BoadNo, out string ThreadNo)
		{
			// 初期化
			KindOfBBS = KindOfBBS.None;
			BoadGenre = "";
			BoadNo = "";
			ThreadNo = "";

			Regex regex = new Regex(pattern);
			Match match = regex.Match(url);

			// 一致
			if (match.Groups.Count == 4)
			{
				// データセット
				if (bbs == KindOfBBS.YYKakiko)
					BoadGenre = "yy" + match.Groups[1].Value;
				else
					BoadGenre = match.Groups[1].Value;

				KindOfBBS = bbs;
				BoadNo = match.Groups[2].Value;
				ThreadNo = match.Groups[3].Value;

				return true;
			}
			return false;
		}

		/// <summary>
		/// 板名を取得
		/// </summary>
		static string GetBoadName(string title_url, string encode)
		{
			string BoardTitle = "";

			// 板名を取得：<title>板名</title>
			string html = HTTP.GetHtml(title_url, encode);

			int s = html.IndexOf("<title>");
			int e = html.IndexOf("</title>");

			if (s == -1 || e == -1)
				return "";

			BoardTitle = html.Substring(s + 7, e - s - 7);

			return BoardTitle;
			// Text = "板名：" + SelectedThread.BoadTitle;
		}

		/// <summary>
		/// スレッドリストの取得
		/// </summary>
		public static List<string[]> GetThreadList(KindOfBBS KindOfBBS, string BoadGenre, string BoadNo)
		{
			List<string[]> ThreadList = new List<string[]>();

			// エラーチェック
			if (BoadGenre == "" || BoadNo == "")
				return new List<string[]>();

			switch (KindOfBBS)
			{
				// したらば
				case KindOfBBS.JBBS:
					{
						// subject.txtを取得
						string subject_url = "http://jbbs.livedoor.jp/" + BoadGenre + "/" + BoadNo + "/subject.txt";
						string subject_html = HTTP.GetHtml(subject_url, "EUC-JP");

						// 区切り文字作成
						string[] separator = new string[2];
						separator[0] = "\n";
						separator[1] = ".cgi,";

						string[] list = subject_html.Split(separator, StringSplitOptions.None);

						for (int i = 0; i < list.Length - 1; i += 2)
						{
							string[] text = new string[3];

							int index = list[i + 1].LastIndexOf('(');
							string threadTitle = list[i + 1].Substring(0, index);
							string resNum = list[i + 1].Substring(index + 1, list[i + 1].Length - index - 2);

							text[0] = list[i];
							text[1] = threadTitle;
							text[2] = resNum;

							ThreadList.Add(text);
						}
						break;
					}

				// YYKakiko
				case KindOfBBS.YYKakiko:
					{
						// subject.txtを取得
						string subject_url = "http://" + BoadGenre + "/" + BoadNo + "/subject.txt";
						string subject_html = HTTP.GetHtml(subject_url, "Shift_JIS");

						// 区切り文字作成
						string[] separator = new string[2];
						separator[0] = "\n";
						separator[1] = ".dat<>";

						string[] list = subject_html.Split(separator, StringSplitOptions.None);

						for (int i = 0; i < list.Length - 1; i += 2)
						{
							string[] text = new string[3];

							int index = list[i + 1].LastIndexOf('(');
							string threadTitle = list[i + 1].Substring(0, index);
							string resNum = list[i + 1].Substring(index + 1, list[i + 1].Length - index - 2);

							text[0] = list[i];
							text[1] = threadTitle;
							text[2] = resNum;

							ThreadList.Add(text);
						}
						break;
					}
			}

			return ThreadList;
		}
	}

	/// <summary>
	/// 掲示板の種類
	/// </summary>
	public enum KindOfBBS
	{
		JBBS,
		YYKakiko,
		None
	}
}
