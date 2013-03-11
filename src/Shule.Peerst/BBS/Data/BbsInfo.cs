namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板アドレス情報クラス
	/// </summary>
	public class BbsInfo
	{
		/// <summary>
		/// 掲示板サーバ
		/// </summary>
		public BbsServer BBSServer { get; private set; }

		/// <summary>
		/// 板ジャンル
		/// </summary>
		public string BoadGenre { get; private set; }

		/// <summary>
		/// 板番号
		/// </summary>
		public string BoadNo { get; private set; }

		/// <summary>
		/// スレッド番号
		/// </summary>
		public string ThreadNo { get; set; }

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
