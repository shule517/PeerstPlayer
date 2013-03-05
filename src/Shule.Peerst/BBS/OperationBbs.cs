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
		public BbsUrl BbsUrl
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
		/// スレッド情報読み込み
		/// </summary>
		public List<ResInfo> ReadThread(string threadNo)
		{
			return bbsStrategy.ReadThread(threadNo);
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
		/// <param name="url">掲示板URL</param>
		public void ChangeUrl(string url)
		{
			if (url == "本スレ")
			{
				bbsStrategy = bbsFactory.Create(url);
				return;
			}

			Regex regex = new Regex(@"(h?ttps?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+)");
			Match match = regex.Match(url);

			if (match.Groups.Count == 2)
			{
				// ttpをhttpに変換
				if (match.Groups[0].Value[0] == 't')
				{
					url = "h" + match.Groups[0].Value;
				}
				else
				{
					url = match.Groups[0].Value;
				}
			}

			bbsStrategy = bbsFactory.Create(url);
		}

		/// <summary>
		/// スレッド変更
		/// </summary>
		public void ChangeThread(string threadNo)
		{
			bbsStrategy.ChangeThread(threadNo);
		}

		/// <summary>
		/// 掲示板名の取得
		/// </summary>
		/// <returns></returns>
		public string GetBbsName()
		{
			return bbsStrategy.GetBbsName();
		}
	}
}
