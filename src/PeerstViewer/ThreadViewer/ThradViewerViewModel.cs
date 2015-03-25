
using PeerstLib.Bbs;
using PeerstLib.Bbs.Data;
using PeerstLib.Util;
using PeerstViewer.ThreadViewer.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace PeerstViewer.ThreadViewer
{
	class ThradViewerViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// 掲示板操作クラス
		/// </summary>
		private OperationBbs operationBbs;

		/// <summary>
		/// ドキュメント
		/// </summary>
		public string DocumentText { get { return updateThreadCommand.DocumentText; } }

		/// <summary>
		/// 掲示板名
		/// </summary>
		public string BoardName { get { return operationBbs.BbsInfo.BbsName; } }

		/// <summary>
		/// スレッドURL
		/// </summary>
		public string ThreadUrl { get { return operationBbs.ThreadUrl; } }

		/// <summary>
		/// スレッド名
		/// </summary>
		public string ThreadName { get { return operationBbs.SelectThread.ThreadTitle; } }

		/// <summary>
		/// 書き込みメッセージ
		/// </summary>
		public string Message { get { return ""; } }

		/// <summary>
		/// スレッド一覧
		/// </summary>
		public List<ThreadInfo> ThreadList { get { return operationBbs.ThreadList; } }

		/// <summary>
		/// スレッド更新コマンド
		/// </summary>
		private UpdateThreadCommand updateThreadCommand;

		public ThradViewerViewModel()
		{
			Logger.Instance.Debug("ThradViewerViewModel()");
			operationBbs = new OperationBbs();
			updateThreadCommand = new UpdateThreadCommand(operationBbs);
			writeResCommand = new WriteResCommand(operationBbs);
			updateThreadCommand.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);
			writeResCommand.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);

			// コマンドライン引数チェック
			if (Environment.GetCommandLineArgs().Length > 1)
			{
				string url = Environment.GetCommandLineArgs()[1];
				Logger.Instance.DebugFormat("ThradViewerViewModel[url:{0}]", url);
				ChangeUrl(url);
			}
		}

		/// <summary>
		/// スレッド更新
		/// </summary>
		public void UpdateThread()
		{
			Logger.Instance.Debug("UpdateThread[]");
			updateThreadCommand.Execute(new object());
		}

		/// <summary>
		/// URL変更
		/// </summary>
		public void ChangeUrl(string url)
		{
			Logger.Instance.DebugFormat("ChangeUrl[url:{0}]", url);
			updateThreadCommand.Execute(url);
			RaisePropertyChanged("ThreadUrl");
		}

		/// <summary>
		/// スレッド一覧更新
		/// </summary>
		public void UpdateThreadList()
		{
			Logger.Instance.Debug("UpdateThreadList[]");
			operationBbs.UpdateThreadList();
			operationBbs.ThreadListChange += (sender, e) => RaisePropertyChanged("ThreadList");
		}

		/// <summary>
		/// スレッド変更
		/// </summary>
		public void ChangeThread(string threadNo)
		{
			Logger.Instance.DebugFormat("ChangeThread[threadNo:{0}]", threadNo);
			operationBbs.ChangeThread(threadNo);
			UpdateThread();
			RaisePropertyChanged("ThreadUrl");
		}

		/// <summary>
		/// レス書き込み
		/// </summary>
		public void WriteRes(string name, string mail, string message)
		{
			Logger.Instance.DebugFormat("ChangeThread[name:{0}, mail:{1}, message:{2}]", name, mail, message);
			writeResCommand.Execute(new WrireResCommandArg { Name = name, Mail = mail, Message = message });
			UpdateThread();
		}

		/// <summary>
		/// レス書き込みコマンド
		/// </summary>
		private WriteResCommand writeResCommand;

		/// <summary>
		/// データ変更イベント
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// データ変更イベント通知
		/// </summary>
		protected void RaisePropertyChanged(string propertyName)
		{
			var d = PropertyChanged;
			if (d != null)
			{
				Logger.Instance.DebugFormat("RaisePropertyChanged[propertyName:{0}]", propertyName);
				d(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
