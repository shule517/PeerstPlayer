using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PeerstLib.PeerCast
{
	// PeerCast通信クラス
	public class PeerCastConnection
	{
		private StreamUrlInfo urlInfo = null;

		public PeerCastConnection(StreamUrlInfo streamUrlInfo)
		{
			urlInfo = streamUrlInfo;
		}

		// チャンネル情報の取得
		public ChannelInfo GetChannelInfo()
		{
			// チャンネル情報の取得
			XElement elements = XElement.Load("http://" + urlInfo.Host + ":" + urlInfo.PortNo + "/admin?cmd=viewxml");

			// ホスト情報の取得
			IEnumerable<HostInfo> hostInfo = GetHostInfo(elements, urlInfo.StreamId);
			List<HostInfo> hostList = hostInfo.ToList();

			// チャンネル情報の取得
			var query =
				from channel in elements.Elements("channels_relayed").Elements("channel")
				where (string)channel.Attribute("id") == urlInfo.StreamId
				from relay in channel.Elements("relay")
				from track in channel.Elements("track")
				select new ChannelInfo
				{
					// チャンネル情報
					Name = (string)channel.Attribute("name"),
					Id = (string)channel.Attribute("id"),
					Bitrate = (string)channel.Attribute("bitrate"),
					Type = (string)channel.Attribute("type"),
					Genre = (string)channel.Attribute("genre"),
					Desc = (string)channel.Attribute("desc"),
					Url = (string)channel.Attribute("url"),
					Uptime = (string)channel.Attribute("uptime"),
					Comment = (string)channel.Attribute("comment"),
					Skips = (string)channel.Attribute("skips"),
					Age = (string)channel.Attribute("age"),
					Bcflags = (string)channel.Attribute("bcflags"),

					// リレー情報
					Listeners = (string)relay.Attribute("listeners"),
					Relays = (string)relay.Attribute("relays"),
					Hosts = (string)relay.Attribute("hosts"),
					Status = (string)relay.Attribute("status"),

					// トラック情報
					TrackTitle = (string)track.Attribute("title"),
					TrackArtist = (string)track.Attribute("artist"),
					TrackAlbum = (string)track.Attribute("album"),
					TrackGenre = (string)track.Attribute("genre"),
					TrackContact = (string)track.Attribute("contact"),

					HostList = hostList,
				};

			if (query.Count() > 0)
			{
				return query.Single();
			}
			else
			{
				return new ChannelInfo();
			}
		}

		// ホスト情報の取得
		private static IEnumerable<HostInfo> GetHostInfo(XElement elements, string streamId)
		{
			var query =
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

			return query;
		}
	}
}
