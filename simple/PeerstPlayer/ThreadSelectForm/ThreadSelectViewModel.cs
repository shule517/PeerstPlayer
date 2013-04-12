using Shule.Peerst.BBS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PeerstPlayer
{

	public class ThreadSelectViewModel
	{
		// プロパティ名
		public class Property
		{
			// スレッド一覧
			public const string ThreadList = "ThreadList";

			// スレッドURL
			public const string ThreadUrl = "ThreadUrl";
		};

		// スレッドストップレス数
		public int ThreadStopResNum { get { return 1000; } } // TODO スレッドストップ数の取得

		// スレッド一覧
		public List<ThreadInfo> ThreadList { get { return operationBbs.ThreadList; } }

		// 掲示板情報
		public BbsInfo BbsInfo { get { return operationBbs.BbsInfo; } }

		// プロパティ変更イベント
		public event PropertyChangedEventHandler PropertyChanged;

		// 掲示板操作クラス
		private OperationBbs operationBbs = new OperationBbs();

		// スレッド一覧更新worker
		private BackgroundWorker updateWorker = new BackgroundWorker();

		// スレッド変更worker
		private BackgroundWorker changeThreadWorker = new BackgroundWorker();

		// コンストラクタ
		public ThreadSelectViewModel()
		{
			updateWorker.DoWork += updateWorker_DoWork;
			updateWorker.RunWorkerCompleted += updateWorker_RunWorkerCompleted;

			changeThreadWorker.DoWork += changeThreadWorker_DoWork;
			changeThreadWorker.RunWorkerCompleted += changeThreadWorker_RunWorkerCompleted;
		}

		// スレッド変更
		public void ChangeThread(string threadNo)
		{
			// 更新スレッドの実行
			if (!changeThreadWorker.IsBusy)
			{
				changeThreadWorker.RunWorkerAsync(threadNo);
			}
		}

		// スレッド一覧の更新
		public void Update(string url)
		{
			// 更新スレッドの実行
			if (!updateWorker.IsBusy)
			{
				updateWorker.RunWorkerAsync(url);
			}
		}
	
		#region スレッド変更

		// スレッド変更
		private void changeThreadWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			// スレッド変更
			operationBbs.ChangeThread((string)e.Argument);
		}

		// スレッド変更完了
		private void changeThreadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			// スレッド一覧更新完了
			RaisePropertyChanged(Property.ThreadUrl);
		}

		#endregion

		#region スレッド一覧更新

		// マルチスレッド：スレッド一覧の更新
		private void updateWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			// スレッド変更
			operationBbs.ChangeUrl((string)e.Argument);
		}

		// マルチスレッド：スレッド一覧の更新完了
		private void updateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			// スレッド一覧更新完了
			RaisePropertyChanged(Property.ThreadList);
		}

		#endregion

		// プロパティ変更通知
		private void RaisePropertyChanged(string propertyName)
		{
			if (PropertyChanged == null){ return; }
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
