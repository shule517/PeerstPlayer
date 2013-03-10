using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板操作クラス
	/// </summary>
	public class OperationBbs
	{
		readonly BbsFactory bbsFactory = new BbsFactory();	// 掲示板ストラテジを生成
		IBbsStrategy bbsStrategy;							// 掲示板URLに対応したストラテジを保持

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="url">掲示板URL</param>
		public OperationBbs(string url)
		{
			ChangeUrl(url);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="url">掲示板URL</param>
		public OperationBbs()
		{
			bbsStrategy = new NullBbsStrategy();
		}

		/// <summary>
		/// 掲示板URL
		/// </summary>
		public BbsInfo BbsUrl
		{
			get
			{
				return bbsStrategy.GetBbsUrl();
			}
		}

		/// <summary>
		/// スレッドURL
		/// </summary>
		public string ThreadUrl
		{
			get
			{
				return bbsStrategy.GetBbsUrl().ToString();
			}
		}

		/// <summary>
		/// 掲示板サーバ
		/// </summary>
		public BbsServer BBSServer
		{
			get
			{
				return BbsUrl.BBSServer;
			}
		}

		/// <summary>
		/// 板ジャンル
		/// </summary>
		public string BoadGenre
		{
			get
			{
				return BbsUrl.BoadGenre;
			}
		}

		/// <summary>
		/// 板番号
		/// </summary>
		public string BoadNo
			{
			get
			{
				return BbsUrl.BoadNo;
			}
		}

		/// <summary>
		/// スレッド番号
		/// </summary>
		public string ThreadNo
		{
			get
			{
				return BbsUrl.ThreadNo;
			}
		}

		/// <summary>
		/// 取得済みレス番号
		/// </summary>
		public int LastResNo { get; private set; }

		/// <summary>
		/// 掲示板名の取得
		/// </summary>
		/// <returns></returns>
		public string GetBbsName()
		{
			return bbsStrategy.GetBbsName();
		}

		/*
		/// <summary>
		/// スレッド一覧
		/// </summary>
		public List<ThreadInfo> ThreadList
		{
			get;
			private set;
		}

		/// <summary>
		/// スレッド情報
		/// </summary>
		public ThreadInfo ThreadInfo
		{
			get;
			private set;
		}

		/// <summary>
		/// レス一覧
		/// </summary>
		public List<ResInfo> ResList
		{
			get;
			private set;
		}
		 */

		/// <summary>
		/// スレッド一覧の取得
		/// </summary>
		public List<ThreadInfo> GetThreadList()
		{
			return bbsStrategy.GetThreadList();
		}

		/// <summary>
		/// スレッド情報の取得
		/// </summary>
		public ThreadInfo GetThreadInfo()
		{
			return bbsStrategy.GetThreadInfo(bbsStrategy.GetBbsUrl().ThreadNo);
		}

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
			ChangeUrl(threadUrl, "");
		}

		/// <summary>
		/// 掲示板URL変更
		/// </summary>
		/// <param name="bbsUrl"></param>
		/// <param name="threadNo"></param>
		public void ChangeUrl(string bbsUrl, string threadNo)
		{
			// 取得済みレス番号を初期化
			LastResNo = 0;

			if (threadNo == "本スレ")
			{
				bbsStrategy = bbsFactory.Create(threadNo);
				return;
			}

			// スレッドURLの作成
			string threadUrl = bbsUrl + threadNo;

			Regex regex = new Regex(@"(h?ttps?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+)");
			Match match = regex.Match(threadUrl);

			if (match.Groups.Count == 2)
			{
				// ttpをhttpに変換
				if (match.Groups[0].Value[0] == 't')
				{
					threadUrl = "h" + match.Groups[0].Value;
				}
				else
				{
					threadUrl = match.Groups[0].Value;
				}
			}

			// 生成
			bbsStrategy = bbsFactory.Create(threadUrl);
		}

		/// <summary>
		/// スレッド変更
		/// </summary>
		public void ChangeThread(string threadNo)
		{
			// 取得済みレス番号を初期化
			LastResNo = 0;
			bbsStrategy.ChangeThread(threadNo);
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
