using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// 掲示板操作クラス
	/// </summary>
	public class OperationBbs
	{
		BbsFactory bbsFactory;		// 掲示板ストラテジを生成
		IBbsStrategy bbsStrategy;	// 掲示板URLに対応したストラテジを保持

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="url">掲示板URL</param>
		public OperationBbs(string url)
		{
			bbsFactory = new BbsFactory();
			ChangeUrl(url);
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

				bbsStrategy = bbsFactory.Create(url);
			}
		}

		/// <summary>
		/// 掲示板URL取得
		/// </summary>
		public BbsUrl GetBbsUrl()
		{
			return bbsStrategy.GetBbsUrl();
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
		public bool Write(string name, string mail, string message)
		{
			return bbsStrategy.Write(name, mail, message);
		}

		/// <summary>
		/// スレッド情報読み込み
		/// </summary>
		public List<ThreadInfo> ReadThread(string threadNo)
		{
			return bbsStrategy.ReadThread(threadNo);
		}

		/// <summary>
		/// スレッド変更
		/// </summary>
		public void ChangeThread(string threadNo)
		{
			bbsStrategy.ChangeThread(threadNo);
		}

		/// <summary>
		/// 掲示板URL取得
		/// </summary>
		public string GetUrl()
		{
			return bbsStrategy.GetBbsUrl().ToString();
		}
	}
}
