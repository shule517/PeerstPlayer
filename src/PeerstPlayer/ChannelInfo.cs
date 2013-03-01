using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Web;
using Shule.Peerst.Web;
using Shule.Peerst.PeerCast;

namespace PeerstPlayer
{
	public class ChannelInfo
	{
		public ChannelInfo()
		{
		}

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

		// PeerCastマネージャ
		PeerCastManager pecaManager = null;

		public void Update(URLData uRLData)
		{
			if (uRLData.Host == "" || uRLData.PortNo == "" || uRLData.ID == "")
			{
				FileName = uRLData.FileName;
				return;
			}

			FileName = "";

			// TODO 生成タイミングを変える
			pecaManager = new PeerCastManager(uRLData.Host, uRLData.PortNo, uRLData.ID);
			Shule.Peerst.PeerCast.ChannelInfo channelInfo = pecaManager.GetChannelInfo();

			name = channelInfo.Name;
			genre = channelInfo.Genre;

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
			desc = channelInfo.Desc;
			bitrate = channelInfo.Bitrate;
			totalListeners = channelInfo.Listeners;
			totalRelays = channelInfo.Relays;
			status = channelInfo.Status;
			trackArtist = channelInfo.TrackArtist;
			trackTitle = channelInfo.TrackTitle;
			trackAlbum = channelInfo.TrackAlbum;
			contactURL = channelInfo.Url;
			comment = channelInfo.Comment;

			// TODO 詳細に本スレがある場合は、コンタクトＵＲＬを本スレにする
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
