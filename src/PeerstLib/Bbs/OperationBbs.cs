using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PeerstLib.Bbs.Data;
using PeerstLib.Bbs.Strategy;
using PeerstLib.Utility;

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

		// レス一覧
		public List<ResInfo> ResList
		{
			get { return strategy.ResList; }
		}
		public event EventHandler ResListChange = delegate { };

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

		// スレッド一覧更新Worker
		BackgroundWorker updateThreadListWorker = new BackgroundWorker();

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		//-------------------------------------------------------------
		public OperationBbs()
		{
			// スレッドURL変更
			changeUrlWorker.WorkerSupportsCancellation = true; // キャンセル処理を許可
			changeUrlWorker.DoWork += (sender, e) =>
			{
				string url = (string)e.Argument;
				strategy = BbsStrategyFactory.Create(url);
				strategy.UpdateThreadList();
				strategy.UpdateBbsName();
			};
			changeUrlWorker.RunWorkerCompleted += (sender, e) =>
			{
				Logger.Instance.Debug("RaiseThreadListChange");
				ThreadListChange(this, new EventArgs());
			};

			// スレッド一覧更新
			updateThreadListWorker.WorkerSupportsCancellation = true;
			updateThreadListWorker.DoWork += (sender, e) =>
			{
				strategy.UpdateThreadList();
				strategy.UpdateBbsName();
			};
			updateThreadListWorker.RunWorkerCompleted += (sender, e) =>
			{
				Logger.Instance.Debug("RaiseThreadListChange");
				ThreadListChange(this, new EventArgs());
			};
		}

		//-------------------------------------------------------------
		// 概要：URL変更
		// 詳細：掲示板ストラテジを切り替える
		//-------------------------------------------------------------
		public void ChangeUrl(string url)
		{
			if (url == null)
			{
				// 初期化中のため、スルー
				return;
			}
			else if (url.Equals(""))
			{
				// スレッド一覧更新あり(URL指定なしに更新)
				Logger.Instance.DebugFormat("ChangeUrl [URL指定なし]", url);
				Logger.Instance.Debug("RaiseThreadListChange");
				strategy.ThreadList = new List<ThreadInfo>();
				ThreadListChange(this, new EventArgs());
				return;
			}

			Logger.Instance.DebugFormat("ChangeUrl(url:{0})", url);

			// データ更新
			if (!changeUrlWorker.IsBusy)
			{
				changeUrlWorker.RunWorkerAsync(url);
			}
		}

		//-------------------------------------------------------------
		// 概要：スレッド一覧更新
		//-------------------------------------------------------------
		public void UpdateThreadList()
		{
			Logger.Instance.DebugFormat("UpdateThreadList(url:{0})", strategy.ThreadUrl);

			// データ更新
			if (!updateThreadListWorker.IsBusy)
			{
				updateThreadListWorker.RunWorkerAsync();
			}
		}

		//-------------------------------------------------------------
		// 概要：スレッド変更
		//-------------------------------------------------------------
		public void ChangeThread(string threadNo)
		{
			Logger.Instance.DebugFormat("ChangeThread(threadNo:{0})", threadNo);
			strategy.ChangeThread(threadNo);
		}

		//-------------------------------------------------------------
		// 概要：レス書き込み
		//-------------------------------------------------------------
		public void Write(string name, string mail, string message)
		{
			Logger.Instance.DebugFormat("Write(name:{0}, mail:{1}, message:{2})", name, mail, message);
			strategy.Write(name, mail, message);
		}

		//-------------------------------------------------------------
		// 概要：スレッド読み込み
		//-------------------------------------------------------------
		public void ReadThread()
		{
			// 選択しているスレッドからデータを取得する
			Logger.Instance.DebugFormat("ReadThread");
			strategy.ReadThread();
		}

		//-------------------------------------------------------------
		// 概要：終了処理
		//-------------------------------------------------------------
		public void Close()
		{
			Logger.Instance.Debug("Close()");
			if (changeUrlWorker.IsBusy)
			{
				changeUrlWorker.CancelAsync();
			}
		}
	}
}
