using System.Collections.Generic;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// NULL掲示板ストラテジクラス(NULLオブジェクトパターン)
	/// </summary>
	class NullBbsStrategy : IBbsStrategy
	{
		/// <summary>
		/// 掲示板書き込み
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="mail">メール欄</param>
		/// <param name="message">本文</param>
		public bool Write(string name, string mail, string message)
		{
			return true;
		}

		/// <summary>
		/// スレッド一覧の取得
		/// </summary>
		public List<ThreadInfo> GetThreadList()
		{
			return new List<ThreadInfo>();
		}

		/// <summary>
		/// スレッド読み込み
		/// </summary>
		public List<ResInfo> ReadThread(string threadNo)
		{
			return new List<ResInfo>();
		}

		/// <summary>
		/// 掲示板名を取得
		/// </summary>
		public string GetBbsName()
		{
			return "";
		}

		/// <summary>
		/// スレッド変更
		/// </summary>
		public void ChangeThread(string threadNo)
		{
		}

		/// <summary>
		/// 掲示板URL取得
		/// </summary>
		public BbsUrl GetBbsUrl()
		{
			return new BbsUrl(BbsServer.UnSupport, "", "", "");
		}
	}
}
