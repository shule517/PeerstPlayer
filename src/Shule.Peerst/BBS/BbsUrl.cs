namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板アドレス情報クラス
	/// </summary>
	public class BbsUrl
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BbsUrl(BbsServer bbsServer, string boadGenre, string boadNo, string threadNo)
		{
			BBSServer = bbsServer;
			BoadGenre = boadGenre;
			BoadNo = boadNo;
			ThreadNo = threadNo;
		}

		public BbsServer BBSServer { get; set; }	// 掲示板サーバ
		public string BoadGenre { get; set; }		// 板ジャンル
		public string BoadNo { get; set; }			// 板番号
		public string ThreadNo { get; set; }		// スレッド番号

		/// <summary>
		/// 掲示板URLを取得
		/// </summary>
		public override string ToString()
		{
			string url = "";
			if (BBSServer == BbsServer.Shitaraba)
			{
				url = "http://jbbs.livedoor.jp/bbs/read.cgi/" + BoadGenre + "/" + BoadNo + "/";
			}
			else if (BBSServer == BbsServer.YYKakiko)
			{
				url = "http://" + BoadGenre + "/test/read.cgi/" + BoadNo + "/";
			}
			else
			{
				return string.Empty;
			}

			if (ThreadNo != "")
			{
				url += ThreadNo + "/";
			}

			return url;
		}
	}
}
