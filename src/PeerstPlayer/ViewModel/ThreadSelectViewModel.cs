using System;
using System.Collections.Generic;
using PeerstLib.Bbs;
using PeerstLib.Utility;

namespace PeerstPlayer.ViewModel
{
	//-------------------------------------------------------------
	// 概要：スレッド選択画面ViewModelクラス
	//-------------------------------------------------------------
	public class ThreadSelectViewModel
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// スレッド一覧
		public List<ThreadInfo> ThreadList { get { return operationBbs.ThreadList; } }

		// スレッドURL
		public string ThreadUrl { get { return operationBbs.ThreadUrl; } }

		// スレッド一覧変更イベント
		public event EventHandler ThreadListChange = delegate { };

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// 掲示板操作クラス
		private　OperationBbs operationBbs = new OperationBbs();

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		//-------------------------------------------------------------
		public ThreadSelectViewModel()
		{
			Logger.Instance.DebugFormat("ThreadSelectViewModel()");
			operationBbs.ThreadListChange += (sender, e) => ThreadListChange(this, new EventArgs());
		}

		//-------------------------------------------------------------
		// 概要：スレッド一覧更新
		//-------------------------------------------------------------
		public void Update(string threadUrl)
		{
			Logger.Instance.DebugFormat("Update(threadUrl:{0})", threadUrl);
			operationBbs.ChangeUrl(threadUrl);
		}

		//-------------------------------------------------------------
		// 概要：スレッド変更
		//-------------------------------------------------------------
		public void ChangeThread(string threadNo)
		{
			Logger.Instance.DebugFormat("ChangeThread(threadNo:{0})", threadNo);
			operationBbs.ChangeThread(threadNo);
		}
	}
}
