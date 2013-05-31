using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstLib.PeerCast
{
	//-------------------------------------------------------------
	// 概要：チャンネル情報クラス
	//-------------------------------------------------------------
	public class ChannelInfo
	{
		public ChannelInfo()
		{
			HostList = new List<HostInfo>();
		}

		// チャンネル情報
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
		public string Bcflags { get; set; }

		// リレー情報
		public string Listeners { get; set; }
		public string Relays { get; set; }
		public string Hosts { get; set; }
		public string Status { get; set; }
		public string Firewalled { get; set; }
		public RelayColor RelayColor { get; set; }

		// トラック情報
		public string TrackTitle { get; set; }
		public string TrackArtist { get; set; }
		public string TrackAlbum { get; set; }
		public string TrackGenre { get; set; }
		public string TrackContact { get; set; }

		// ホスト情報一覧
		public List<HostInfo> HostList { get; set; }
	}

	//-------------------------------------------------------------
	// 概要：ホスト情報クラス
	//-------------------------------------------------------------
	public class HostInfo
	{
		public string Ip { get; set; }
		public string Hops{ get; set; }
		public string Listeners { get; set; }
		public string Relays { get; set; }
		public string Hosts { get; set; }
		public string Status { get; set; }
		public string Uptime { get; set; }
		public string Push { get; set; }
		public string Relay { get; set; }
		public string Direct { get; set; }
		public string Cin { get; set; }
		public string Stable { get; set; }
		public string Version { get; set; }
		public string Update { get; set; }
		public string Tracker { get; set; }
		public RelayColor RelayColor { get; set; }
	}

	//-------------------------------------------------------------
	// 概要：リレー色
	//-------------------------------------------------------------
	public enum RelayColor
	{
		None,
		Red,
		Orange,
		Blue,
		Green,
		Purple,
	};
}
