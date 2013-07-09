
namespace PeerstLib.Bbs.Data
{
	//-------------------------------------------------------------
	// 概要：掲示板情報クラス
	//-------------------------------------------------------------
	public class BbsInfo
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// URL
		public string Url { get; set; }

		// 板ジャンル
		public string BoardGenre { get; set; }

		// 掲示板番号
		public string BoardNo { get; set; }

		// スレッド番号
		public string ThreadNo { get; set; }

		// 掲示板名
		public string BbsName { get; set; }

		// 掲示板サーバ
		public BbsServer BbsServer { get; set; }
	}
}
