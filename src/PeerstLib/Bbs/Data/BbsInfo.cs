
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

		/// <summary>
		/// 掲示板サーバのホスト
		/// </summary>
		public string Host { get; set; }

		/// <summary>
		/// URL
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// 板ジャンル
		/// </summary>
		public string BoardGenre { get; set; }

		/// <summary>
		/// 掲示板番号
		/// </summary>
		public string BoardNo { get; set; }

		/// <summary>
		/// スレッド番号
		/// </summary>
		public string ThreadNo { get; set; }

		/// <summary>
		/// 掲示板名
		/// </summary>
		public string BbsName { get; set; }

		/// <summary>
		/// 掲示板サーバ
		/// </summary>
		public BbsServer BbsServer { get; set; }
	}
}
