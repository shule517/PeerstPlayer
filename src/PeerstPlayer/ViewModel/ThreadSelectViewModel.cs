using PeerstLib.Bbs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PeerstPlayer.ViewModel
{
	public class ThreadSelectViewModel
	{
		// スレッド一覧
		public List<ThreadInfo> ThreadList { get { return operationBbs.ThreadList; } }

		// スレッドURL
		public string ThreadUrl { get { return operationBbs.ThreadUrl; } }

		// スレッド一覧変更イベント
		public event EventHandler ThreadListChange = delegate { };

		// 掲示板操作クラス
		private　OperationBbs operationBbs = new OperationBbs();

		public ThreadSelectViewModel()
		{
			operationBbs.ThreadListChange += (sender, e) => ThreadListChange(this, new EventArgs());
		}

		// スレッド一覧更新
		public void Update(string threadUrl)
		{
			operationBbs.ChangeUrl(threadUrl);
		}

		// スレッド変更
		public void ChangeThread(string threadNo)
		{
			operationBbs.ChangeThread(threadNo);
		}
	}
}
