
namespace PeerstLib.Bbs
{
	//-------------------------------------------------------------
	// 概要：スレッド情報クラス
	//-------------------------------------------------------------
	public class ThreadInfo
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// スレッド番号
		public string ThreadNo { get; set; }

		// スレッドタイトル
		public string ThreadTitle { get; set; }

		// レス数
		public int ResCount { get; set; }
	
		// レス勢い
		public float ThreadSpeed { get; set; }

		// スレッド作成からの経過日数
		public double ThreadSince { get; set; }

		// スレッドストップ
		public bool IsStopThread { get { return ResCount >= MaxResCount; } }

		// 最大レス数
		public int MaxResCount { get { return 1000; } } // TODO 掲示板情報から取得する
	}
}
