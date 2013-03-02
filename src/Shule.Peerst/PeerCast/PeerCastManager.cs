using Shule.Peerst.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
		public string Status { get; set; }
		public string IconURL { get; set; }
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
							channelInfo.IsInfo = true;
						}
						else
						{
							channelInfo.IsInfo = false;
						}

						return channelInfo;
					}

					// インデックスを要素に移動します
					reader.MoveToElement();
				}
			}

			// XMLファイルを閉じる
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
	}
}
