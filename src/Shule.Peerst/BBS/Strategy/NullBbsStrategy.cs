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
		/// スレッド読み込み
		/// </summary>
		public List<ThreadInfo> ReadThread(string threadNo)
		{
			return new List<ThreadInfo>();
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
