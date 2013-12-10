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

		public WriteResCommand(OperationBbs operationBbs)
		{
			this.operationBbs = operationBbs;
		}

		/// <summary>
		/// コマンド実行
		/// </summary>
		public void Execute(object parameter)
		{
			try
			{
				WrireResCommandArg arg = parameter as WrireResCommandArg;
				operationBbs.Write(arg.Name, arg.Mail, arg.Message);
				PropertyChanged(this, new PropertyChangedEventArgs("ClearWriteField"));
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error");
			}
		}

		public event EventHandler CanExecuteChanged;
		public bool CanExecute(object parameter)
		{
			return true;
		}
	}

	class WrireResCommandArg
	{
		public string Name;
		public string Mail;
		public string Message;
	}
}
