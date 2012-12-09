namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板アドレス情報クラス
	/// </summary>
	class BbsUrl
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BbsUrl(string boadGenre, string boadNo, string threadNo)
		{
			BoadGenre = boadGenre;
			BoadNo = boadNo;
			ThreadNo = threadNo;
		}

		public string BoadGenre { get; set; }	// 板ジャンル
		public string BoadNo { get; set; }		// 板番号
		public string ThreadNo { get; set; }	// スレッド番号
	}
}
