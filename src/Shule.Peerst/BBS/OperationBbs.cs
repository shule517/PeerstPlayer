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
			ChangeUrl(url);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="url">掲示板URL</param>
		public OperationBbs()
		{
			ChangeUrl("");
		}

		/// <summary>
		/// 掲示板情報
		/// </summary>
		public BbsInfo BbsInfo { get { return bbsStrategy.BbsInfo; } }

		/// <summary>
		/// スレッドURL
		/// </summary>
		public string ThreadUrl { get { return bbsStrategy.BbsInfo.ToString(); } }

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
			if (threadUrl == "")
			{
				bbsStrategy = new NullBbsStrategy();
			}
			else
			{
				ChangeUrl(threadUrl, "");
			}

			// 取得済みレス番号を初期化
			LastResNo = 0;

			// スレッド情報更新
			bbsStrategy.UpdateThreadInfo();

			// 掲示板名の更新
			bbsStrategy.UpdateBbsName();
		}

		/// <summary>
		/// 掲示板URL変更
		/// </summary>
		/// <param name="bbsUrl"></param>
		/// <param name="threadNo"></param>
		public void ChangeUrl(string threadUrl, string threadNo)
		{
			// 取得済みレス番号を初期化
			LastResNo = 0;

			if (threadNo == "本スレ")
			{
				bbsStrategy = bbsFactory.Create(threadNo);
				return;
			}

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

			// TODO 板移動時に、ChangeThreadを実行しなくて良いようにする

			// スレッド変更
			if (threadNo != "")
			{
				bbsStrategy.ChangeThread(threadNo);
			}

			// スレッド情報更新
			bbsStrategy.UpdateThreadInfo();

			// 掲示板名の更新
			bbsStrategy.UpdateBbsName();
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

			// 掲示板名の更新
			bbsStrategy.UpdateBbsName();
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
