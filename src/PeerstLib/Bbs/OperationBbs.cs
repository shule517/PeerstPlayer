using System.Globalization;
using System.Text.RegularExpressions;
using PeerstLib.Bbs.Data;
using PeerstLib.Bbs.Strategy;
using PeerstLib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace PeerstLib.Bbs
{
	//-------------------------------------------------------------
	// 概要：掲示板操作クラス
	// 詳細：ストラテジの切替を行う
	//-------------------------------------------------------------
	public class OperationBbs
	{
		/// <summary>
		/// 掲示板情報
		/// </summary>
		public BbsInfo BbsInfo
		{
			get { return strategy.BbsInfo; }
		}

		/// <summary>
		/// スレッド一覧
		/// </summary>
		public List<ThreadInfo> ThreadList
		{
			get { return strategy.ThreadList; }
		}

		/// <summary>
		/// スレッド一覧更新イベント
		/// </summary>
		public event EventHandler ThreadListChange = delegate { };

		/// <summary>
		/// レス一覧
		/// </summary>
		public List<ResInfo> ResList
		{
			get { return strategy.ResList; }
		}

		/// <summary>
		/// レス一覧更新イベント
		/// </summary>
		public event EventHandler ResListChange = delegate { };

		/// <summary>
		/// 選択スレッド情報
		/// </summary>
		public ThreadInfo SelectThread
		{
			get
			{
				try
				{
					return strategy.ThreadList.Single(thread => (thread.ThreadNo == BbsInfo.ThreadNo));
				}
				catch
				{
					return new ThreadInfo();
				}
			}
		}

		/// <summary>
		/// スレッドURL
		/// </summary>
		public string ThreadUrl
		{
			get { return strategy.ThreadUrl; }
		}

		/// <summary>
		/// 板URL
		/// </summary>
		public string BoardUrl
		{
			get { return strategy.BoardUrl; }
		}

		/// <summary>
		/// スレッド選択状態
		/// </summary>
		public bool ThreadSelected
		{
			get { return strategy.ThreadSelected; }
		}

		/// <summary>
		/// 有効状態
		/// </summary>
		public bool Enabled
		{
			get { return (!string.IsNullOrEmpty(BbsInfo.BoardGenre)) && (!string.IsNullOrEmpty(BbsInfo.BoardNo)); }
		}

		/// <summary>
		/// 掲示板ストラテジ
		/// </summary>
		private BbsStrategy strategy = new NullBbsStrategy(new BbsInfo { BbsServer = BbsServer.UnSupport });

		/// <summary>
		/// URL変更Worker
		/// </summary>
		BackgroundWorker changeUrlWorker = new BackgroundWorker();

		/// <summary>
		/// スレッド一覧更新Worker
		/// </summary>
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
				strategy.UpdateBbsSetting();
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
				strategy.UpdateBbsSetting();
				strategy.UpdateThreadList();
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
		public void ReadThread(bool isHtmlDecode)
		{
			// 選択しているスレッドからデータを取得する
			Logger.Instance.DebugFormat("ReadThread");
			strategy.ReadThread(isHtmlDecode);
		}

		//-------------------------------------------------------------
		// 概要：次スレ候補を取得する
		//-------------------------------------------------------------
		public bool ChangeCandidateThread()
		{
			if (SelectThread.ThreadNo != null)
			{
				// 現在のスレッドタイトル名に数字が含まれていれば次のスレ番のスレを探す
				var regex = new Regex(@"(\d+)");
				var compare = CultureInfo.CurrentCulture.CompareInfo;
				foreach (Match match in regex.Matches(SelectThread.ThreadTitle))
				{
					var number = TextUtil.FullWidthToHalfWidth(match.Captures[0].Value);
					var nextNumber = int.Parse(number) + 1;
					var threads = ThreadList.Where(info =>
						compare.IndexOf(info.ThreadTitle, nextNumber.ToString(), CompareOptions.IgnoreWidth) != -1)
						.OrderByDescending(info => info.ThreadSpeed);
					if (!threads.Any())
					{
						continue;
					}
					ChangeThread(threads.First().ThreadNo);
					return true;
				}
			}
			// 埋まっていないスレかつ
			// スレが選択されていれば、現在のスレより新しいスレかつ
			// 勢いの高いスレを選ぶ
			var threads2 = ThreadList.Where(info => !info.IsStopThread)
				.Where(info => !ThreadSelected || int.Parse(BbsInfo.ThreadNo) < int.Parse(info.ThreadNo))
				.OrderByDescending(info => info.ThreadSpeed);
			// 候補スレッドが無ければ終わり
			if (!threads2.Any())
			{
				return false;
			}

			ChangeThread(threads2.First().ThreadNo);
			return true;
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

		/// <summary>
		/// オブジェクトのコピー
		/// </summary>
		public OperationBbs Clone()
		{
			while (changeUrlWorker.IsBusy || updateThreadListWorker.IsBusy)
			{
				Application.DoEvents();
			}

			return (OperationBbs)MemberwiseClone();
		}
	}
}
