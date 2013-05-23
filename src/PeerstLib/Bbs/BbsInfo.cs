using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstLib.Bbs
{
	// 掲示板情報クラス
	public class BbsInfo
	{
		public string Url { get; set; }
		public string BoardGenre { get; set; }
		public string BoardNo { get; set; }
		public string ThreadNo { get; set; }
		public string BoardName { get; set; }
		public BbsServer BbsServer { get; set; }
	}
}
