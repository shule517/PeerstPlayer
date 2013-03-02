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

		Shule.Peerst.PeerCast.ChannelInfo channelInfo = null;

		/// <summary>
		/// チャンネル情報が取得されているか
		/// </summary>
		public bool IsInfo = false;

		#region チャンネル情報を取得

		public override string ToString()
		{
			if (channelInfo == null)
			{
				return "";
			}

			string text = "";

			// チャンネル名
			text += channelInfo.Name;

			// [ジャンル - 詳細]
			if (channelInfo.Genre != "")
			{
				if (channelInfo.Desc != "")
				{
					text += " [" + channelInfo.Genre + " - " + channelInfo.Desc + "]";
				}
				else
				{
					text += " [" + channelInfo.Genre + "]";
				}
			}
			else
			{
				if (channelInfo.Desc != "")
				{
					text += " [" + channelInfo.Desc + "]";
				}
			}

			/*
			// アーティスト
			if (channelInfo.TrackArtist != "")
			{
				text += " " + channelInfo.TrackArtist;
			}

			// タイトル
			if (channelInfo.TrackTitle != "")
			{
				text += " " + channelInfo.TrackTitle;
			}

			// アルバム
			if (channelInfo.TrackAlbum != "")
			{
				text += " " + channelInfo.TrackAlbum;
			}
			 */

			// コメント
			if (channelInfo.Comment != "")
			{
				text += " " + channelInfo.Comment;
			}

			/*
			// ビットレート
			if (Bitrate != "")
			{
				text += " <" + channelInfo.Bitrate + ">";
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
				return;
			}

			// TODO 生成タイミングを変える
			pecaManager = new PeerCastManager(uRLData.Host, uRLData.PortNo, uRLData.ID);
			channelInfo = pecaManager.GetChannelInfo();

			// アイコンURLを取得
			iconURL = GetIconURL(genre);

			// ジャンルからアイコンURLをはずす
			if (iconURL != "")
			{
				genre = genre.Replace(iconURL, "");
			}

			// ジャンル調整
			genre = SelectGenre(genre);

			// TODO 詳細に本スレがある場合は、コンタクトＵＲＬを本スレにする

			// チャンネル情報取得完了
			IsInfo = true;
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
		string genre = "";
		string status = "";
		string trackArtist = "";
		string trackTitle = "";
		string trackAlbum = "";

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
				return channelInfo.Name;
			}
		}

		public string Genre
		{
			get
			{
				return channelInfo.Genre;
			}
		}

		public string Desc
		{
			get
			{
				return channelInfo.Desc;
			}
		}

		public string Bitrate
		{
			get
			{
				return channelInfo.Bitrate;
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
				return channelInfo.Url;
			}
		}

		public string Comment
		{
			get
			{
				return channelInfo.Comment;
			}
		}

		#endregion
	}
}
