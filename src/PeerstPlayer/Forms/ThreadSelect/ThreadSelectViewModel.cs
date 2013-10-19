using System;
using System.Collections.Generic;
using PeerstLib.Bbs;
using PeerstLib.Bbs.Data;
using PeerstLib.Util;

namespace PeerstPlayer.Forms.ThreadSelect
{
	//-------------------------------------------------------------
	// 概要：スレッド選択画面ViewModelクラス
	//-------------------------------------------------------------
	public class ThreadSelectViewModel
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// スレッド一覧
		/// </summary>
		public List<ThreadInfo> ThreadList { get { return operationBbs.ThreadList; } }

		/// <summary>
		/// スレッドURL
		/// </summary>
		public string ThreadUrl { get { return operationBbs.ThreadUrl; } }

		/// <summary>
		/// 選択スレッド番号
		/// </summary>
		public string ThreadNo { get { return operationBbs.BbsInfo.ThreadNo; } }

		/// <summary>
		/// スレッド一覧変更イベント
		/// </summary>
		public event EventHandler ThreadListChange = delegate { };

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// 掲示板操作クラス
		/// </summary>
		private　OperationBbs operationBbs = new OperationBbs();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ThreadSelectViewModel()
		{
			Logger.Instance.DebugFormat("ThreadSelectViewModel()");
			operationBbs.ThreadListChange += (sender, e) => ThreadListChange(this, new EventArgs());
		}

		/// <summary>
		/// スレッド一覧更新
		/// </summary>
		public void Update(string threadUrl)
		{
			Logger.Instance.DebugFormat("Update(threadUrl:{0})", threadUrl);
			operationBbs.ChangeUrl(threadUrl);
		}

		/// <summary>
		/// スレッド変更
		/// </summary>
		public void ChangeThread(string threadNo)
		{
			Logger.Instance.DebugFormat("ChangeThread(threadNo:{0})", threadNo);
			operationBbs.ChangeThread(threadNo);
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		public void Close()
		{
			Logger.Instance.Debug("Close()");
			operationBbs.Close();
		}

		/// <summary>
		/// OprationBbsのコピー
		/// </summary>
		public OperationBbs CloneOperationBbs()
		{
			return (OperationBbs)operationBbs.Clone();
		}
	}
}
