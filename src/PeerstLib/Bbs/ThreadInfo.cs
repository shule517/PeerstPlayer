using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstLib.Bbs
{
	//-------------------------------------------------------------
	// 概要：スレッド情報クラス
	//-------------------------------------------------------------
	public class ThreadInfo
	{
		// スレッド番号
		public string ThreadNo { get; set; }

		// スレッドタイトル
		public string ThreadTitle { get; set; }

		// レス数
		public int ResCount { get; set; }
	
		// レス勢い
		public float ThreadSpeed { get; set; }
	}
}
