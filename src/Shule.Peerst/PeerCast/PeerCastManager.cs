using Shule.Peerst.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Configuration;
using System.Net.Configuration;

namespace Shule.Peerst.PeerCast
{
	public class ChannelInfo
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Bitrate { get; set; }
		public string Comment { get; set; }
		public string Desc { get; set; }
		public string Genre { get; set; }
		public string Type { get; set; }
		public string ContactUrl { get; set; }

		public bool IsInfo { get; set; }
		public string Status { get; set; }	// TODO ステータスの取得
		public string IconURL { get; set; }

		public override string ToString()
		{
			if (IsInfo == false)
			{
				return "";
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

			/*
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
			 */

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
	}

	public class PeerCastManager
	{
		private string host;									// PeerCastアドレス
		private string portNo;									// PeerCastポート番号
		private string channelId;								// チャンネルID
		public ChannelInfo ChannelInfo { get; private set; }	// チャンネル情報

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="host">PeerCastアドレス</param>
		/// <param name="portNo">PeerCastポート番号</param>
		/// <param name="channelId">チャンネルID</param>
		public PeerCastManager(string host, string portNo, string channelId)
		{
			this.host = host;			// PeerCastアドレス
			this.portNo = portNo;		// PeerCastポート番号
			this.channelId = channelId;	// チャンネルID

			// プロトコル違反を許容
			SettingDisableResponseError();
		}

		/// <summary>
		/// プロトコル違反を許容
		/// PeerCastがプロトコル違反しているバージョンがあるため、許容する
		/// </summary>
		private static void SettingDisableResponseError()
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			SettingsSection section = (SettingsSection)config.GetSection("system.net/settings");
			section.HttpWebRequest.UseUnsafeHeaderParsing = true;
			config.Save();
		}

		/// <summary>
		/// チャンネル情報取得
		/// </summary>
		/// <returns>指定チャンネルのチャンネル情報</returns>
		public void GetChannelInfo()
		{
			string xmlUrl = "http://" + host + ":" + portNo + "/admin?cmd=viewxml";

			XmlTextReader reader = null;

			try
			{
				reader = new XmlTextReader(xmlUrl);

				// XMLファイルを1ノードずつ読み込む
				while (reader.Read())
				{
					reader.MoveToContent();

					// チャンネル情報を取得
					if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "channel"))
					{
						ChannelInfo channelInfo = null;
						readChannel(reader, out channelInfo);

						// 指定したチャンネルを返す
						if (channelInfo.Id == channelId)
						{
							// TODO 
							// 取得結果をtrue
							if (channelInfo.Name != "")
							{
								// アイコンURLを取得
								channelInfo.IconURL = GetIconURL(channelInfo.Genre);

								// ジャンルからアイコンURLをはずす
								if (channelInfo.IconURL != "")
								{
									channelInfo.Genre = channelInfo.Genre.Replace(channelInfo.IconURL, "");
								}

								// ジャンル調整
								channelInfo.Genre = SelectGenre(channelInfo.Genre);

								channelInfo.IsInfo = true;
							}
							else
							{
								channelInfo.IsInfo = false;
							}

							this.ChannelInfo = channelInfo;
						}

						// インデックスを要素に移動します
						reader.MoveToElement();
					}
				}
			}
			catch (Exception)
			{
				// PeerCastの接続エラー
			}
			finally
			{
				if (reader != null)
				{
					// XMLファイルを閉じる
					reader.Close();
				}
			}
		}

		/// <summary>
		/// チャンネル読み込み
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="channelInfo"></param>
		private static void readChannel(XmlTextReader reader, out ChannelInfo channelInfo)
		{
			channelInfo = new ChannelInfo();

			// ノードの属性の数だけループ
			for (int i = 0; i < reader.AttributeCount; i++)
			{
				// インデックスを属性に移動
				reader.MoveToAttribute(i);
				if (reader.Name == "id")
				{
					channelInfo.Id = reader.Value;
				}
				else if (reader.Name == "name")
				{
					channelInfo.Name = reader.Value;
				}
				else if (reader.Name == "bitrate")
				{
					channelInfo.Bitrate = reader.Value;
				}
				else if (reader.Name == "comment")
				{
					channelInfo.Comment = reader.Value;
				}
				else if (reader.Name == "desc")
				{
					channelInfo.Desc = reader.Value;
				}
				else if (reader.Name == "genre")
				{
					channelInfo.Genre = reader.Value;
				}
				else if (reader.Name == "type")
				{
					channelInfo.Type = reader.Value;
				}
				else if (reader.Name == "url")
				{
					channelInfo.ContactUrl = reader.Value;
				}
			}
		}

		/// <summary>
		/// ジャンルの抽出
		/// </summary>
		/// <param name="genre">ジャンル</param>
		/// <returns></returns>
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
		/// <param name="genre">ジャンル</param>
		/// <returns>IconURL</returns>
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
				default:
					return "";
			}
		}

		/// <summary>
		/// Bump
		/// </summary>
		public void Bump()
		{
			string url = "/admin?cmd=bump&id=" + channelId;
			HTTP.SendCommand(host, portNo, url, "Shift_JIS");
		}

		/// <summary>
		/// リレー切断
		/// </summary>
		public void DisconnectRelay()
		{
			// TODO 配信中でない場合は切断
			//if (form.channelInfo.IsInfo && form.channelInfo.Status != "BROADCAST")
			{
				string url = "/admin?cmd=stop&id=" + channelId;
				HTTP.SendCommand(host, portNo, url, "Shift_JIS");
			}
		}

		/// <summary>
		/// リレーキープ
		/// </summary>
		public void RelayKeep()
		{
			string url = "/admin?cmd=keep&id=" + channelId;
			HTTP.SendCommand(host, portNo, url, "Shift_JIS");
		}

	}
}
