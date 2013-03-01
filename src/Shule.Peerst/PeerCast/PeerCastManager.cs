using Shule.Peerst.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Shule.Peerst.PeerCast
{
	public class ChannelInfo
	{
		public string Name { get; set; }
		public string Id { get; set; }
		public string Bitrate { get; set; }
		public string Type { get; set; }
		public string Genre { get; set; }
		public string Desc { get; set; }
		public string Url { get; set; }
		public string Uptime { get; set; }
		public string Comment { get; set; }
		public string Skips { get; set; }
		public string Age { get; set; }
		public string BcFlags { get; set; }
		public string Listeners { get; set; }
		public string Relays { get; set; }
		public string Hosts { get; set; }
		public string Status { get; set; }
		public string TrackTitle { get; set; }
		public string TrackArtist { get; set; }
		public string TrackAlbum { get; set; }
		public string TrackGenre { get; set; }
		public string TrackContact { get; set; }
	}

	public class PeerCastManager
	{
		private string host;		// PeerCastアドレス
		private string portNo;		// PeerCastポート番号
		private string channelId;	// チャンネルID

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
		}

		/// <summary>
		/// チャンネル情報取得
		/// </summary>
		/// <returns>指定チャンネルのチャンネル情報</returns>
		public ChannelInfo GetChannelInfo()
		{
			string xmlUrl = "http://" + host + ":" + portNo + "/admin?cmd=viewxml";
			XmlTextReader reader = new XmlTextReader(xmlUrl);

			//XMLファイルを1ノードずつ読み込む
			while (reader.Read())
			{
				reader.MoveToContent();

				//ノードに属性がある場合(例：<chr animal="熊">フィロ</chr>の「animal」)
				if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "channel"))
				{
					// TODO リスナー情報などを取得する
					ChannelInfo channelInfo = null;
					readChannel(reader, out channelInfo);

					// 指定したチャンネルを返す
					if (channelInfo.Id == channelId)
					{
						return channelInfo;
					}

					//インデックスを要素に移動します
					reader.MoveToElement();
				}
			}
			//XMLファイルを閉じる
			reader.Close();

			return null;
		}

		/// <summary>
		/// チャンネル読み込み
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="channelInfo"></param>
		private static void readChannel(XmlTextReader reader, out ChannelInfo channelInfo)
		{
			channelInfo = new ChannelInfo();

			//ノードの属性の数だけループ
			for (int i = 0; i < reader.AttributeCount; i++)
			{
				//インデックスを属性に移動します
				reader.MoveToAttribute(i);
				if (reader.Name == "name")
				{
					channelInfo.Name = reader.Value;
				}
				else if (reader.Name == "id")
				{
					channelInfo.Id = reader.Value;
				}
				else if (reader.Name == "bitrate")
				{
					channelInfo.Bitrate = reader.Value;
				}
				else if (reader.Name == "type")
				{
					channelInfo.Type = reader.Value;
				}
				else if (reader.Name == "genre")
				{
					channelInfo.Genre = reader.Value;
				}
				else if (reader.Name == "desc")
				{
					channelInfo.Desc = reader.Value;
				}
				else if (reader.Name == "url")
				{
					channelInfo.Url = reader.Value;
				}
				else if (reader.Name == "uptime")
				{
					channelInfo.Uptime = reader.Value;
				}
				else if (reader.Name == "comment")
				{
					channelInfo.Comment = reader.Value;
				}
				else if (reader.Name == "skips")
				{
					channelInfo.Skips = reader.Value;
				}
				else if (reader.Name == "age")
				{
					channelInfo.Age = reader.Value;
				}
				else if (reader.Name == "bcflags")
				{
					channelInfo.BcFlags = reader.Value;
				}
			}
		}
	}
}
