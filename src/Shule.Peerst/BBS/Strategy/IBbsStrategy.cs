using System.Collections.Generic;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板ストラテジインターフェイス
	/// </summary>
	interface IBbsStrategy
	{
		/// <summary>
		/// 掲示板書き込み
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="mail">メール欄</param>
		/// <param name="message">本文</param>
		bool Write(string name, string mail, string message);

		/// <summary>
		/// スレッド一覧の取得
		/// </summary>
		List<ThreadInfo> GetThreadList();

		/// <summary>
		/// レス読み込み
		/// </summary>
		List<ResInfo> ReadThread(string threadNo);

		/// <summary>
		/// 掲示板名を取得
		/// </summary>
		string GetBbsName();

		/// <summary>
		/// スレッド変更
		/// </summary>
		void ChangeThread(string threadNo);

		/// <summary>
		/// 掲示板URL取得
		/// </summary>
		BbsUrl GetBbsUrl();
	}
}
