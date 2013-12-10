using PeerstLib.Bbs;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace PeerstViewer.ThreadViewer.Command
{
	/// <summary>
	/// レス書き込みコマンド
	/// </summary>
	class WriteResCommand : ICommand
	{
		/// <summary>
		/// 掲示板操作
		/// </summary>
		private OperationBbs operationBbs;

		/// <summary>
		/// プロパティ変更イベント
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		/// <summary>
		/// 掲示板書き込みスレッド
		/// </summary>
		private BackgroundWorker worker = new BackgroundWorker();

		public WriteResCommand(OperationBbs operationBbs)
		{
			this.operationBbs = operationBbs;

			worker.DoWork += (sender, e) =>
			{
				e.Result = null;

				try
				{
					WrireResCommandArg arg = e.Argument as WrireResCommandArg;
					operationBbs.Write(arg.Name, arg.Mail, arg.Message);
				}
				catch (Exception excetpion)
				{
					e.Result = excetpion;
				}
			};
			worker.RunWorkerCompleted += (sender, e) =>
			{
				Exception exception = e.Result as Exception;

				if (exception != null)
				{
					MessageBox.Show(exception.Message);
				}
				else
				{
					PropertyChanged(this, new PropertyChangedEventArgs("Message"));
				}
			};
		}

		/// <summary>
		/// コマンド実行
		/// </summary>
		public void Execute(object parameter)
		{
			if (!worker.IsBusy)
			{
				worker.RunWorkerAsync(parameter);
			}
		}

		public event EventHandler CanExecuteChanged;
		public bool CanExecute(object parameter)
		{
			return true;
		}
	}

	/// <summary>
	/// レス書きコマンドの引数
	/// </summary>
	class WrireResCommandArg
	{
		public string Name;
		public string Mail;
		public string Message;
	}
}
