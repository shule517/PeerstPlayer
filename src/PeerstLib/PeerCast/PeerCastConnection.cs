using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using PeerstLib.Bbs.Data;
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
				return ViewXMLAnalyzer.AnlyzeViewXML(elements, urlInfo.StreamId);
			}
			catch
			{
				Logger.Instance.ErrorFormat("ViewXMLの取得結果：異常 [xmlUrl:{0}]", xmlUrl);
				return new ChannelInfo();
			}
		}

	}
}
