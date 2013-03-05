using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Shule.Peerst.Web;

namespace PeerstPlayer
{
	public class BBS
	{
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
					string html = WebUtility.GetHtml(url, Encoding.GetEncoding("EUC-JP"));

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
					string html = WebUtility.GetHtml(url, Encoding.GetEncoding("Shift_JIS"));

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
