namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板アドレス情報クラス
	/// </summary>
	public class BbsInfo
	{
		public BbsServer BBSServer { get; private set; }	// 掲示板サーバ
		public string BoadGenre { get; private set; }		// 板ジャンル
		public string BoadNo { get; private set; }			// 板番号
		public string ThreadNo { get; set; }				// スレッド番号

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BbsInfo(BbsServer bbsServer, string boadGenre, string boadNo, string threadNo)
		{
			this.BBSServer = bbsServer;
			this.BoadGenre = boadGenre;
			this.BoadNo = boadNo;
			this.ThreadNo = threadNo;
		}

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
