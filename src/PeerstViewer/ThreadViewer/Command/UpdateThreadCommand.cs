using PeerstLib.Bbs;
using PeerstLib.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using PeerstLib.Controls;

namespace PeerstViewer.ThreadViewer.Command
{
	/// <summary>
	/// スレッド更新コマンド
	/// </summary>
	class UpdateThreadCommand : ICommand, INotifyPropertyChanged
	{
		/// <summary>
		/// ドキュメント
		/// </summary>
		public string DocumentText;

        public string DiffText;
		/// <summary>
		/// プロパティ変更イベント
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		/// <summary>
		/// 掲示板操作
		/// </summary>
		private OperationBbs operationBbs;

		/// <summary>
		/// スレッド更新ワーカー
		/// </summary>
		private BackgroundWorker worker = new BackgroundWorker();

		/// <summary>
		/// スレッドドキュメント生成
		/// </summary>
		private ThreadDocumentGenerator threadDocumentGenerator = new ThreadDocumentGenerator(FormUtility.GetExeFolderPath() + "/skin");

		/// <summary>
		/// 前回のドキュメント
		/// </summary>
		private string beforeDocumentText = "";

		public UpdateThreadCommand(OperationBbs operationBbs)
		{
			Logger.Instance.DebugFormat("UpdateThreadCommand");
			this.operationBbs = operationBbs;

			// キャンセル可能
			worker.WorkerSupportsCancellation = true;
			worker.DoWork += (sender, e) => e.Result = UpdateDocumentText();
			worker.RunWorkerCompleted += (sender, e) =>
			{
				Logger.Instance.Debug("UpdateThreadCommandWorker.RunWorkerCompleted");
				if ((e.Error != null) || e.Cancelled)
				{
					Logger.Instance.Debug("RunWorkerCompleted Cancell");
					return;
				}

				DocumentText = e.Result as string;

				// 更新があった時だけ通知
				if (DocumentText != beforeDocumentText)
				{
					try
					{
                        DiffText = DocumentText.Replace(beforeDocumentText,"");
						Logger.Instance.Debug("PropertyChangedEventArgs(DocumentText)");
						PropertyChanged(this, new PropertyChangedEventArgs("DocumentText"));
					}
					catch (Exception exception)
					{
						Logger.Instance.ErrorFormat("UpdateThreadCommandError : {0} {1}", exception.Message, exception.StackTrace);
					}
				}
				beforeDocumentText = DocumentText;
			};
		}

		/// <summary>
		/// コマンド実行
		/// </summary>
		public void Execute(object parameter)
		{
			string url = parameter as string;
			Logger.Instance.DebugFormat("Execute[url:{0}]", url);
			if (url != null)
			{
				operationBbs.ChangeUrl(url);
				operationBbs.ThreadListChange += (sender, e) =>
				{
					Logger.Instance.Debug("ThreadListChange[]");
					// スレッドURL変更＋更新
					if (!worker.IsBusy)
					{
						Logger.Instance.Debug("UpdateThreadCommandWorker.CancelAsync");
						worker.CancelAsync();
						Logger.Instance.Debug("UpdateThreadCommandWorker.RunWorkerAsync : スレッドURL変更 + 更新");
						worker.RunWorkerAsync();
					}
				};
			}
			else
			{
				// 更新のみ
				if (!worker.IsBusy)
				{
					Logger.Instance.Debug("UpdateThreadCommandWorker.RunWorkerAsync : 更新のみ");
					worker.RunWorkerAsync();
				}
			}
		}

		/// <summary>
		/// ドキュメント更新
		/// </summary>
		private string UpdateDocumentText()
		{
			Logger.Instance.Debug("UpdateDocumentText[]");
			int oldResNum = operationBbs.ResList.Count;
			operationBbs.ReadThread(false);
			return threadDocumentGenerator.Generate(operationBbs, oldResNum);
		}

		public event System.EventHandler CanExecuteChanged = delegate { };
		public bool CanExecute(object parameter)
		{
			return true;
		}

	}
}
