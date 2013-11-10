using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板操作クラス
	/// </summary>
	public class OperationBbs
	{
		/// <summary>
		/// 掲示板ストラテジファクトリ
		/// </summary>
		readonly BbsFactory bbsFactory = new BbsFactory();

		/// <summary>
		/// 掲示板ストラテジを保持
		/// </summary>
		IBbsStrategy bbsStrategy = new NullBbsStrategy();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="url">掲示板URL</param>
		public OperationBbs(string url)
		{
			// 取得済みレス番号を初期化
			LastResNo = 0;

			ChangeUrl(url);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="url">掲示板URL</param>
		public OperationBbs()
		{
			// 取得済みレス番号を初期化
			LastResNo = 0;
		}

		/// <summary>
		/// 掲示板情報
		/// </summary>
		public BbsInfo BbsInfo { get { return bbsStrategy.BbsInfo; } }

		/// <summary>
		/// スレッドURL
		/// </summary>
		public string ThreadUrl { get { return bbsStrategy.BbsInfo.ThreadUrl; } }

		/// <summary>
		/// スレッド一覧
		/// </summary>
		public List<ThreadInfo> ThreadList { get { return bbsStrategy.ThreadList; } }

		/// <summary>
		/// スレッド情報
		/// </summary>
		public ThreadInfo ThreadInfo { get { return bbsStrategy.ThreadInfo; } }

		/// <summary>
		/// 掲示板名の取得
		/// </summary>
		/// <returns></returns>
		public string BbsName { get { return bbsStrategy.BbsName; } }

		/// <summary>
		/// スレッド情報の更新
		/// ThreadList / ThreadInfoの内容を更新する
		/// </summary>
		public void UpdateThreadInfo()
		{
			bbsStrategy.UpdateThreadInfo();
		}

		/// <summary>
		/// 取得済みレス番号
		/// </summary>
		public int LastResNo { get; private set; }

		/// <summary>
		/// 掲示板書き込み
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="mail">メール欄</param>
		/// <param name="message">本文</param>
		public bool Write(string name, string mail, string message)
		{
			return bbsStrategy.Write(name, mail, message);
		}

		/// <summary>
		/// 掲示板URL変更
		/// URLにあったストラテジに変更する
		/// </summary>
		/// <param name="threadUrl">スレッドURL</param>
		public void ChangeUrl(string threadUrl)
		{
			// 取得済みレス番号を初期化
			LastResNo = 0;

			// ttp → httpに変換
			threadUrl = FixThreadUrl(threadUrl);

			// 生成
			bbsStrategy = bbsFactory.Create(threadUrl);

			// スレッド情報更新
			bbsStrategy.UpdateThreadInfo();

			// 掲示板名の更新
			bbsStrategy.UpdateBbsName();
		}

		/// <summary>
		/// URLの修正
		/// ttp → http
		/// </summary>
		/// <param name="threadUrl"></param>
		/// <returns></returns>
		private static string FixThreadUrl(string threadUrl)
		{
			string ttp = "ttp://";
			int start = threadUrl.IndexOf(ttp);

			if (start == 0)
			{
				threadUrl = "h" + threadUrl;
			}
			return threadUrl;
		}

		/// <summary>
		/// スレッド変更
		/// </summary>
		public void ChangeThread(string threadNo)
		{
			// 取得済みレス番号を初期化
			LastResNo = 0;
			bbsStrategy.ChangeThread(threadNo);

			// スレッド情報更新
			bbsStrategy.UpdateThreadInfo();
		}

		/// <summary>
		/// スレッド情報読み込み
		/// </summary>
		public List<ResInfo> ReadThread(string threadNo)
		{
			List<ResInfo> resList = bbsStrategy.ReadThread(threadNo, LastResNo + 1);
			LastResNo += resList.Count;
			return resList;
		}
	}
}
