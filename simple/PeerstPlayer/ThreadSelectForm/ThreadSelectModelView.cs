using Shule.Peerst.BBS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PeerstPlayer
{

	public class ThreadSelectModelView
	{
		// プロパティ名
		public class Property
		{
			// スレッド一覧
			public const string ThreadList = "ThreadList";
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

		// コンストラクタ
		public ThreadSelectModelView()
		{
		}

		// スレッド変更
		public void ChangeThread(string threadNo)
		{
			operationBbs.ChangeThread(threadNo);
		}

		// スレッド一覧更新
		public void Update(string url)
		{
			// スレッド変更
			operationBbs.ChangeUrl(url);

			// TODO Backgroundで実行する
			RaisePropertyChanged(Property.ThreadList);
		}

		// プロパティ変更通知
		private void RaisePropertyChanged(string propertyName)
		{
			if (PropertyChanged == null){ return; }
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
