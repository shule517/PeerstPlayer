using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PeerstLib.Mvvm
{
	public static class MvvmUtils
	{
		/// <summary>
		/// プロパティチェンジ用イベントハンドラ作成
		/// </summary>
		/// <param name="propertyName">プロパティ名</param>
		/// <param name="control">操作対象コントロール</param>
		/// <param name="action">実行操作</param>
		/// <returns>プロパティチェンジ用イベントハンドラ</returns>
		public static PropertyChangedEventHandler ToPropertyChangedEventHandler(string propertyName, Control control, Action action)
		{
			return (sender, e) =>
			{
				if (e.PropertyName != propertyName)
				{
					return;
				}

				if (control.InvokeRequired)
				{
					// 別スレッドから起動
					control.BeginInvoke(action);
				}
				else
				{
					// UIスレッドから起動
					action();
				}
			};
		}
	}
}
