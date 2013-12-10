
using PeerstLib.Bbs;
using PeerstLib.Bbs.Data;
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
		/// スレッドURL
		/// </summary>
		public string ThreadUrl { get { return operationBbs.ThreadUrl; } }

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
			operationBbs = new OperationBbs();
			updateThreadCommand = new UpdateThreadCommand(operationBbs);
			writeResCommand = new WriteResCommand(operationBbs);
			updateThreadCommand.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);
			writeResCommand.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);

			// コマンドライン引数チェック
			if (Environment.GetCommandLineArgs().Length > 1)
			{
				string url = Environment.GetCommandLineArgs()[1];
				ChangeUrl(url);
			}
		}

		/// <summary>
		/// スレッド更新
		/// </summary>
		public void UpdateThread()
		{
			updateThreadCommand.Execute(new object());
		}

		/// <summary>
		/// URL変更
		/// </summary>
		public void ChangeUrl(string url)
		{
			updateThreadCommand.Execute(url);
			RaisePropertyChanged("ThreadUrl");
		}

		/// <summary>
		/// スレッド一覧更新
		/// </summary>
		public void UpdateThreadList()
		{
			operationBbs.UpdateThreadList();
			operationBbs.ThreadListChange += (sender, e) => RaisePropertyChanged("ThreadList");
		}

		/// <summary>
		/// スレッド変更
		/// </summary>
		public void ChangeThread(string threadNo)
		{
			operationBbs.ChangeThread(threadNo);
			UpdateThread();
			RaisePropertyChanged("ThreadUrl");
		}

		/// <summary>
		/// レス書き込み
		/// </summary>
		public void WriteRes(string name, string mail, string message)
		{
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
				d(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
