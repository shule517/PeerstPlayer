using System.Collections.Generic;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板操作クラス
	/// </summary>
	public class OperationBbs
	{
		BbsFactory bbsFactory;		// 掲示板ストラテジを生成
		BbsStrategy bbsStrategy;	// 掲示板URLに対応したストラテジを保持

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="url">掲示板URL</param>
		public OperationBbs(string url)
		{
			bbsFactory = new BbsFactory();
			SetURL(url);
		}

		/// <summary>
		/// 掲示板URL変更
		/// URLにあったストラテジに変更する
		/// </summary>
		/// <param name="url">掲示板URL</param>
		public void SetURL(string url)
		{
			bbsStrategy = bbsFactory.Create(url);
		}

		/// <summary>
		/// 掲示板名の取得
		/// </summary>
		/// <returns></returns>
		public string GetBbsName()
		{
			return bbsStrategy.GetBbsName();
		}

		/// <summary>
		/// 掲示板書き込み
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="mail">メール欄</param>
		/// <param name="message">本文</param>
		public void Write(string name, string mail, string message)
		{
			bbsStrategy.Write(name, mail, message);
		}

		/// <summary>
		/// スレッド情報読み込み
		/// </summary>
		public List<ThreadInfo> ReadThread(string threadNo)
		{
			return bbsStrategy.ReadThread(threadNo);
		}
	}
}
