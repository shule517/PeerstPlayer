using System.Text;
using System.Xml.Linq;
using PeerstLib.PeerCast.Data;
using PeerstLib.PeerCast.Util;
using PeerstLib.Util;

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

		// チャンネル情報
		private ChannelInfo channelInfo = new ChannelInfo();

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
			string xmlUrl = string.Format("http://{0}:{1}/admin?cmd=viewxml", urlInfo.Host, urlInfo.PortNo);
			Logger.Instance.DebugFormat("GetChannelInfo(Host:{0}, PortNo:{1}, StreamId:{2} xmlUrl:{3})", urlInfo.Host, urlInfo.PortNo, urlInfo.StreamId, xmlUrl);

			try
			{
				// ViewXMLの取得
				XElement elements = XElement.Load(xmlUrl);
				Logger.Instance.DebugFormat("ViewXMLの取得結果：正常 [xmlUrl:{0}]", xmlUrl);

				// ViewXMLの解析
				channelInfo = ViewXMLAnalyzer.AnlyzeViewXML(elements, urlInfo.StreamId);
				return channelInfo;
			}
			catch
			{
				Logger.Instance.ErrorFormat("ViewXMLの取得結果：異常 [xmlUrl:{0}]", xmlUrl);
				channelInfo = new ChannelInfo();
				return channelInfo;
			}
		}

		/// <summary>
		/// Bump
		/// </summary>
		public void Bump()
		{
			string url = string.Format("/admin?cmd=bump&id={0}", urlInfo.StreamId);
			WebUtil.SendCommand(urlInfo.Host, int.Parse(urlInfo.PortNo), url, Encoding.GetEncoding("Shift_JIS"));
		}

		/// <summary>
		/// リレー切断
		/// </summary>
		public void DisconnectRelay()
		{
			// 配信中はリレー切断しない
			if (channelInfo.Status.Equals("BROADCAST"))
			{
				return;
			}

			// ローカル接続が２本以上あればリレー切断しない
			int relays = 0;
			if (int.TryParse(channelInfo.Relays, out relays))
			{
				if (relays >= 2)
				{
					return;
				}
			}

			string url = string.Format("/admin?cmd=stop&id={0}", urlInfo.StreamId);
			WebUtil.SendCommand(urlInfo.Host, int.Parse(urlInfo.PortNo), url, Encoding.GetEncoding("Shift_JIS"));
		}

		/// <summary>
		/// リレーキープ
		/// </summary>
		public void RelayKeep()
		{
			string url = string.Format("/admin?cmd=keep&id={0}", urlInfo.StreamId);
			WebUtil.SendCommand(urlInfo.Host, int.Parse(urlInfo.PortNo), url, Encoding.GetEncoding("Shift_JIS"));
		}
	}
}
