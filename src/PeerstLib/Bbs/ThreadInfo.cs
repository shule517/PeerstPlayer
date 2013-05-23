using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstLib.Bbs
{
	// スレッド情報クラス
	public class ThreadInfo
	{
		public string ThreadNo { get; set; }
		public string ThreadTitle { get; set; }
		public int ResCount { get; set; }
		public float ThreadSpeed { get; set; }
	}
}
