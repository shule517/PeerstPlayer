using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PeerstLib.Bbs.Strategy;

namespace PeerstLib.Bbs
{
	//-------------------------------------------------------------
	// 概要：掲示板操作クラス
	// 詳細：ストラテジの切替を行う
	//-------------------------------------------------------------
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

		// URL変更Worker
		BackgroundWorker changeUrlWorker = new BackgroundWorker();

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		//-------------------------------------------------------------
		public OperationBbs()
		{
			changeUrlWorker.DoWork += (sender, e) =>
			{
				string url = (string)e.Argument;
				strategy = BbsStrategyFactory.Create(url);
				strategy.UpdateThreadList();
				strategy.UpdateBbsName();
			};
			changeUrlWorker.RunWorkerCompleted += (sender, e) =>
			{
				ThreadListChange(this, new EventArgs());
			};
		}

		//-------------------------------------------------------------
		// 概要：URL変更
		// 詳細：掲示板ストラテジを切り替える
		//-------------------------------------------------------------
		public void ChangeUrl(string url)
		{
			// データ更新
			if (!changeUrlWorker.IsBusy)
			{
				changeUrlWorker.RunWorkerAsync(url);
			}
		}

		//-------------------------------------------------------------
		// 概要：スレッド変更
		//-------------------------------------------------------------
		public void ChangeThread(string threadNo)
		{
			strategy.ChangeThread(threadNo);
		}

		//-------------------------------------------------------------
		// 概要：レス書き込み
		//-------------------------------------------------------------
		public void Write(string name, string mail, string message)
		{
			strategy.Write(name, mail, message);
		}

		// TODO Update -> レス数の更新用
		// TODO Read -> レス読み込み
	}
}
