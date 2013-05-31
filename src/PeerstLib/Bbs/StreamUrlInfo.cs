using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstLib.Bbs
{
	//-------------------------------------------------------------
	// 概要：ストリームURLから抽出した情報クラス
	//-------------------------------------------------------------
	public class StreamUrlInfo
	{
		public StreamUrlInfo()
		{
			Host = "";
			PortNo = "";
			StreamId = "";
		}

		public string Host { get; set; }		// PeerCastのアドレス
		public string PortNo { get; set; }		// PeerCastのポート番号
		public string StreamId { get; set; }	// ストリームID
	}
}
