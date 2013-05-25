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

		// スレッド一覧
		public List<ThreadInfo> ThreadList
		{
			get { return strategy.ThreadList; }
		}
		public event EventHandler ThreadListChange = delegate { };

		// 選択スレッド情報
		public ThreadInfo SelectThread
		{
			get { return ThreadList.Single(thread => (thread.ThreadNo == BbsInfo.ThreadNo)); }
		}

		// スレッドURL
		public string ThreadUrl
		{
			get { return strategy.ThreadUrl; }
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

		// 掲示板ストラテジ
		private BbsStrategy strategy = new NullBbsStrategy(new BbsInfo { BbsServer = BbsServer.UnSupport });

		// URL変更
		// 掲示板ストラテジを切り替える
		public void ChangeUrl(string url)
		{
			strategy = BbsStrategyFactory.Create(url);

			// データ更新
			strategy.UpdateThreadList();
			strategy.UpdateBbsName();
		}

		// スレッド変更
		public void ChangeThread(string threadNo)
		{
			strategy.ChangeThread(threadNo);
		}

		// レス書き込み
		public void Write(string name, string mail, string text)
		{
			strategy.Write(name, mail, text);
		}

		// TODO Update -> レス数の更新用
		// TODO Read -> レス読み込み
	}
}
