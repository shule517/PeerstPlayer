using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using PeerstLib.Bbs.Data;
using PeerstLib.Utility;

namespace PeerstLib.PeerCast
{
	//-------------------------------------------------------------
	// 概要：PeerCast通信クラス
	//-------------------------------------------------------------
	public class PeerCastConnection
	{
		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// ストリームURL情報(接続先)
		private StreamUrlInfo urlInfo = null;

		//-------------------------------------------------------------
		// 概要：ストリーム情報の初期化
		//-------------------------------------------------------------
		public PeerCastConnection(StreamUrlInfo streamUrlInfo)
		{
			Logger.Instance.DebugFormat("PeerCastConnection(Host:{0}, PortNo:{1}, StreamId:{2})", streamUrlInfo.Host, streamUrlInfo.PortNo, streamUrlInfo.StreamId);
			urlInfo = streamUrlInfo;
		}

		//-------------------------------------------------------------
		// 概要：チャンネル情報の取得
		// 詳細：読み込む対象のXMLを指定する
		//-------------------------------------------------------------
		public ChannelInfo GetChannelInfo()
		{
			Logger.Instance.DebugFormat("GetChannelInfo(Host:{0}, PortNo:{1}, StreamId:{2})", urlInfo.Host, urlInfo.PortNo, urlInfo.StreamId);
			string xmlUrl = string.Format("http://{0}:{1}/admin?cmd=viewxml", urlInfo.Host, urlInfo.PortNo);

			try
			{
				// ViewXMLの取得
				XElement elements = XElement.Load(xmlUrl);
				Logger.Instance.DebugFormat("ViewXMLの取得結果：正常 [xmlUrl:{0}]", xmlUrl);

				// ViewXMLの解析
				return AnlyzeViewXML(elements);
			}
			catch
			{
				Logger.Instance.ErrorFormat("ViewXMLの取得結果：異常 [xmlUrl:{0}]", xmlUrl);
				return new ChannelInfo();
			}
		}

		//-------------------------------------------------------------
		// 概要：ViewXMLを解析
		//-------------------------------------------------------------
		private ChannelInfo AnlyzeViewXML(XElement elements)
		{
			// ホスト情報の取得
			IEnumerable<HostInfo> hostInfo = SelectHostInfo(elements, urlInfo.StreamId);
			List<HostInfo> hostList = hostInfo.ToList();

			// チャンネル情報の取得
			ChannelInfo channelInfo = SelectChannelInfo(elements, hostList);

			// リレー色の設定
			SetRelayColor(channelInfo);

			return channelInfo;
		}

		//-------------------------------------------------------------
		// 概要：チャンネル情報の取得
		//-------------------------------------------------------------
		private ChannelInfo SelectChannelInfo(XElement elements, List<HostInfo> hostList)
		{
			var channelList =
				// channels_relayed
				from chRelay in elements.Elements("channels_relayed").Elements("channel")
				where (string)chRelay.Attribute("id") == urlInfo.StreamId
				from relay in chRelay.Elements("relay")
				from track in chRelay.Elements("track")
				// channels_found
				from chFound in elements.Elements("channels_found").Elements("channel")
				where (string)chFound.Attribute("id") == urlInfo.StreamId
				from hits in chFound.Elements("hits")
				select new ChannelInfo
				{
					// チャンネル情報
					Name = (string)chRelay.Attribute("name"),
					Id = (string)chRelay.Attribute("id"),
					Bitrate = (string)chRelay.Attribute("bitrate"),
					Type = (string)chRelay.Attribute("type"),
					Genre = (string)chRelay.Attribute("genre"),
					Desc = (string)chRelay.Attribute("desc"),
					Url = (string)chRelay.Attribute("url"),
					Uptime = (string)chRelay.Attribute("uptime"),
					Comment = (string)chRelay.Attribute("comment"),
					Skips = (string)chRelay.Attribute("skips"),
					Age = (string)chRelay.Attribute("age"),
					Bcflags = (string)chRelay.Attribute("bcflags"),

					// リレー情報
					Listeners = (string)relay.Attribute("listeners"),
					Relays = (string)relay.Attribute("relays"),
					Hosts = (string)relay.Attribute("hosts"),
					Status = (string)relay.Attribute("status"),
					Firewalled = (string)hits.Attribute("firewalled"),

					// トラック情報
					TrackTitle = (string)track.Attribute("title"),
					TrackArtist = (string)track.Attribute("artist"),
					TrackAlbum = (string)track.Attribute("album"),
					TrackGenre = (string)track.Attribute("genre"),
					TrackContact = (string)track.Attribute("contact"),

					HostList = hostList,
				};

			// チャンネル情報の取得
			ChannelInfo channelInfo = new ChannelInfo();
			if (channelList.Count() > 0)
			{
				channelInfo = channelList.SingleOrDefault();
				channelInfo.Genre = FitGenre(channelInfo.Genre);
				Logger.Instance.DebugFormat("チャンネル情報取得結果：正常 [チャンネル名:{0}]", channelInfo.Name);
			}
			else
			{
				Logger.Instance.ErrorFormat("チャンネル情報取得結果：異常 [ストリームID:{0}]", urlInfo.StreamId);
			}

			return channelInfo;
		}

		//-------------------------------------------------------------
		// 概要：ジャンルから不要な文字を削除する
		//-------------------------------------------------------------
		private string FitGenre(string genre)
		{
			if (string.IsNullOrEmpty(genre))
			{
				Logger.Instance.Debug("genre : 空");
				return string.Empty;
			}

			// リンクアドレスを抽出
			Regex http = new Regex(@"(cp|xp|rp|tp|hktv|sp|np|op|gp|lp|nm|np|twyp)([:]*)([?]*)([@]*)([+]*)(?<genre>.*)");
			Match m = http.Match(genre);

			string result = m.Groups["genre"].Value.Trim();
			Logger.Instance.DebugFormat("FitGenre(genre:{0}) -> [{1}]", genre, result);

			return result;
		}

		//-------------------------------------------------------------
		// 概要：ホスト情報の取得
		//-------------------------------------------------------------
		private static IEnumerable<HostInfo> SelectHostInfo(XElement elements, string streamId)
		{
			var hostList =
				from channel in elements.Elements("channels_found").Elements("channel")
				where (string)channel.Attribute("id") == streamId
				from host in channel.Elements("hits").Elements("host")
				where (string)host.Attribute("hops") == "1"
				select new HostInfo
				{
					Ip = (string)host.Attribute("ip"),
					Hops = (string)host.Attribute("hops"),
					Listeners = (string)host.Attribute("listeners"),
					Relays = (string)host.Attribute("relays"),
					Uptime = (string)host.Attribute("uptime"),
					Push = (string)host.Attribute("push"),
					Relay = (string)host.Attribute("relay"),
					Direct = (string)host.Attribute("direct"),
					Cin = (string)host.Attribute("cin"),
					Stable = (string)host.Attribute("stable"),
					Version = (string)host.Attribute("version"),
					Update = (string)host.Attribute("update"),
					Tracker = (string)host.Attribute("tracker"),
				};

			return hostList;
		}

		//-------------------------------------------------------------
		// 概要：リレー色の設定
		//-------------------------------------------------------------
		private void SetRelayColor(ChannelInfo chInfo)
		{
			// 自分のリレー色
			{
				bool isPortOpen = (chInfo.Firewalled == "0");
				bool isRelay = (chInfo.HostList.Count != 0);
				bool canRelay = (chInfo.Relays != "0");

				chInfo.RelayColor = JudgeRelayColor(chInfo, isPortOpen, isRelay, canRelay);
			}

			// リレー先のリレー色
			foreach (HostInfo hostInfo in chInfo.HostList)
			{
				bool isPortOpen = (hostInfo.Push == "0");
				bool isRelay = (hostInfo.Relay != "0");
				bool canRelay = (hostInfo.Relays != "0");

				hostInfo.RelayColor = JudgeRelayColor(chInfo, isPortOpen, isRelay, canRelay);
			}
		}

		//-------------------------------------------------------------
		// 概要：リレー色の判定
		//-------------------------------------------------------------
		private static RelayColor JudgeRelayColor(ChannelInfo chInfo, bool isPortOpen, bool isRelay, bool canRelay)
		{
			// ポート解放済み
			if (isPortOpen)
			{
				// リレーあり
				if (isRelay)
				{
					return RelayColor.Green;
				}
				// リレーなし
				else
				{
					// リレーできる
					if (canRelay)
					{
						return RelayColor.Blue;
					}
					// リレーできない
					else
					{
						return RelayColor.Purple;
					}
				}
			}
			// ポート未開放
			else
			{
				// リレーできる
				if (canRelay)
				{
					return RelayColor.Orange;
				}
				// リレーできない
				else
				{
					return RelayColor.Red;
				}
			}
		}
	}
}
