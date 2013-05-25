using PeerstLib.Bbs.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstLib.Bbs
{
	// 掲示板操作クラス
	public class OperationBbs
	{
		// 掲示板情報
		public BbsInfo BbsInfo
		{
			get { return strategy.BbsInfo; }
		}

		// スレッド選択状態
		public bool ThreadSelected
		{
			get { return strategy.ThreadSelected; }
		}

		// 有効状態
		public bool Enabled
		{
			get { return (!string.IsNullOrEmpty(BbsInfo.BoardGenre)) && (!string.IsNullOrEmpty(BbsInfo.BoardNo)); }
		}

		// スレッド一覧
		public List<ThreadInfo> ThreadList
		{
			get { return strategy.ThreadList; }
		}
		public event EventHandler ThreadListChange = delegate { };

		// スレッドURL
		public string ThreadUrl
		{
			get { return strategy.ThreadUrl; }
		}

		// スレッドタイトル
		public ThreadInfo ThreadInfo
		{
			get { return ThreadList.Single(thread => (thread.ThreadNo == BbsInfo.ThreadNo)); }
		}

		// 掲示板ストラテジ
		private BbsStrategy strategy = new NullBbsStrategy(new BbsInfo { BbsServer = BbsServer.UnSupport });

		//-------------------------------------------------------------
		// 概要：URL変更
		// 詳細：掲示板ストラテジを切り替える
		//-------------------------------------------------------------
		public void ChangeUrl(string url)
		{
			strategy = BbsStrategyFactory.Create(url);
			strategy.ThreadListChange += (sender, e) => ThreadListChange(sender, e);

			// データ更新
			strategy.UpdateThreadList();
			strategy.UpdateBbsName();
		}

		//-------------------------------------------------------------
		// 概要：スレッド変更
		//-------------------------------------------------------------
		public void ChangeThread(string threadNo)
		{
			strategy.ChangeThread(threadNo);
		}

		// TODO Update -> レス数の更新用

		//-------------------------------------------------------------
		// 概要：レス書き込み
		//-------------------------------------------------------------
		public void Write(string name, string mail, string text)
		{
			strategy.Write(name, mail, text);
		}
	}
}
