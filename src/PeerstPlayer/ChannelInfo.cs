using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Web;

namespace PeerstPlayer
{
	public class ChannelInfo
	{
		public ChannelInfo()
		{
		}

		string host = "";
		string port_no = "";
		string id = "";
		string url = "";
		string html = "";

		/// <summary>
		/// チャンネル情報が取得されているか
		/// </summary>
		public bool IsInfo = false;

		#region チャンネル情報を取得

		public override string ToString()
		{
			if (FileName != "")
			{
				return FileName;
			}

			string text = "";

			// チャンネル名
			text += Name;

			// [ジャンル - 詳細]
			if (Genre != "")
			{
				if (Desc != "")
				{
					text += " [" + Genre + " - " + Desc + "]";
				}
				else
				{
					text += " [" + Genre + "]";
				}
			}
			else
			{
				if (Desc != "")
				{
					text += " [" + Desc + "]";
				}
			}

			// アーティスト
			if (TrackArtist != "")
			{
				text += " " + TrackArtist;
			}

			// タイトル
			if (TrackTitle != "")
			{
				text += " " + TrackTitle;
			}

			// アルバム
			if (TrackAlbum != "")
			{
				text += " " + TrackAlbum;
			}

			// コメント
			if (Comment != "")
			{
				text += " " + Comment;
			}

			/*
			// ビットレート
			if (Bitrate != "")
			{
				text += " <" + Bitrate + ">";
			}
			 */

			return text;
		}

		#endregion

		#region チャンネル情報をアップデート

		public void Update(URLData uRLData)
		{
			if (uRLData.Host == "" || uRLData.PortNo == "" || uRLData.ID == "")
			{
				FileName = uRLData.FileName;
				return;
			}

			FileName = "";
			this.host = uRLData.Host;
			this.port_no = uRLData.PortNo;
			this.id = uRLData.ID;

			url = "/html/peerst/channel_info.html?id=" + id;

			// httpスレッド初期化
			html = "";
			/*
			httpThread = new System.Threading.Thread(new System.Threading.ThreadStart(HttpThreadMethod));
			httpThread.Start();
			while (html == "")
			{
				Application.DoEvents();
			}
			 */
			html = HTTP.GetHtml(host, port_no, url, "utf-8");

			//string html = HTTP.GetHtml(host, port_no, url);

			// channel_info.htmlがあるか？
			if (IsInfoHtml(html))
			{
				// なければ作ってもう一度取得
				try
				{
					html = HTTP.GetHtml(host, port_no, url, "utf-8");
				}
				catch
				{
					return;
				}
			}
			
			// HTMLをデコード
			html = EncodeHtmlToText(html);

			string[] info_list = html.Split('\n');
			if (info_list.Length == 12)
			{
				// 取得できたか判定
				if (info_list[0] == "page.channel.name" || info_list[0] == "")// || info_list[6] == "ERROR")
				{
					// 取得失敗
					return;
				}
				else
				{
					// 取得成功
					IsInfo = true;
				}

				// チャンネル名
				name = info_list[0];

				// ジャンル
				genre = info_list[1];

				// アイコンURLを取得
				iconURL = GetIconURL(genre);

				// ジャンルからアイコンURLをはずす
				if (iconURL != "")
				{
					genre = genre.Replace(iconURL, "");
				}

				// ジャンル調整
				genre = SelectGenre(genre);

				// 詳細
				desc = info_list[2];
				bitrate = info_list[3];
				totalListeners = info_list[4];
				totalRelays = info_list[5];
				status = info_list[6];
				trackArtist = info_list[7];
				trackTitle = info_list[8];
				trackAlbum = info_list[9];
				contactURL = info_list[10];
				comment = info_list[11];

				if (contactURL == "")
				{
					contactURL = "本スレ";
				}
			}
		}

		private string SelectGenre(string genre)
		{
			// リンクアドレスを抽出
			Regex http = new Regex(@"(cp|xp|rp|tp|hktv|sp|np|op|gp|lp|nm|np|twyp)([:]*)([?]*)([@]*)([+]*)(?<genre>.*)");
			Match m = http.Match(genre);

			string s = m.Groups["genre"].Value;
			string a = m.Value;

			return m.Groups["genre"].Value;
		}

		/// <summary>
		/// ジャンルからIconURLを取得
		/// </summary>
		private string GetIconURL(string genre)
		{
			// リンクアドレスを抽出
			Regex http = new Regex(@"http://(?<domain>[\w\.]*)/(?<path>[\w\./]*)");
			Match m = http.Match(genre);
			string Ext = Path.GetExtension(m.Value);

			switch (Ext)
			{
				// 画像リンク
				case ".ico":
				case ".ICO":
				case ".png":
				case ".PNG":
				case ".gif":
				case ".GIF":
				case ".jpg":
				case ".JPG":
				case ".bmp":
				case ".BMP":
					return m.Value;
			}

			return "";
		}

		/// <summary>
		/// HTMLをデコードする
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		private string EncodeHtmlToText(string str)
		{
			return HttpUtility.HtmlDecode(str);
		}

		#endregion

		#region channel_info.htmlを作成

		private bool IsInfoHtml(string text)
		{
			Regex regex = new Regex(@"Unable to open file : (.*)\\html");
			Match match = regex.Match(text);

			if (match.Groups.Count <= 1)
			{
				return false;
			}
			else
			{
				try
				{
					string info = HTTP.GetHtml(host, port_no, url, "shift_jis");
					regex = new Regex(@"Unable to open file : (.*)\\html");
					match = regex.Match(info);

					string path = match.Groups[1].Value;

					// peerstフォルダを作成
					path += "\\html\\peerst";
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}

					// channel_info.htmlを作成
					string html = @"{$page.channel.name}
{$page.channel.genre}
{$page.channel.desc}
{$page.channel.bitrate} kbps
{$page.channel.totalListeners}
{$page.channel.totalRelays}
{$page.channel.status}
{$page.channel.track.artist}
{$page.channel.track.title}
{$page.channel.track.album}
{$page.channel.contactURL}
{$page.channel.comment}";
					FileStream writer = new FileStream(path + "\\channel_info.html", FileMode.Create, FileAccess.Write);

					//文字コードを指定する
					System.Text.Encoding enc = System.Text.Encoding.UTF8;

					//文字列をByte型配列に変換
					byte[] sendBytes = enc.GetBytes(html);

					writer.Write(sendBytes, 0, sendBytes.Length);
					writer.Close();

					return true;
				}
				catch
				{
					return false;
				}
			}
		}

		#endregion

		#region データ

		string iconURL = "";
		string name = "";
		string genre = "";
		string desc = "";
		string bitrate = "";
		string totalListeners = "";
		string totalRelays = "";
		string status = "";
		string trackArtist = "";
		string trackTitle = "";
		string trackAlbum = "";
		string contactURL = "";
		string comment = "";
		string fileName = "";

		public string IconURL
		{
			get
			{
				return iconURL;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

		public string Genre
		{
			get
			{
				return genre;
			}
		}

		public string Desc
		{
			get
			{
				return desc;
			}
		}

		public string Bitrate
		{
			get
			{
				return bitrate;
			}
		}

		public string TotalListeners
		{
			get
			{
				return totalListeners;
			}
		}

		public string TotalRelays
		{
			get
			{
				return totalRelays;
			}
		}

		public string Status
		{
			get
			{
				return status;
			}
		}

		public string TrackArtist
		{
			get
			{
				return trackArtist;
			}
		}

		public string TrackTitle
		{
			get
			{
				return trackTitle;
			}
		}

		public string TrackAlbum
		{
			get
			{
				return trackAlbum;
			}
		}

		public string ContactURL
		{
			get
			{
				return contactURL;
			}
		}

		public string Comment
		{
			get
			{
				return comment;
			}
		}

		public string FileName
		{
			get
			{
				return fileName;
			}
			set
			{
				fileName = value;
			}
		}

		#endregion
	}
}
