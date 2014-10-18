using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeerstViewer.Settings;

namespace PeerstViewer.ThreadViewer
{
	/// <summary>
	/// ビューワー設定画面表示クラス
	/// </summary>
	public partial class ViewerSettingView : Form
	{
		public ViewerSettingView()
		{
			InitializeComponent();

			// 初期化
			Shown += (sender, e) =>
			{
				// チェックボックスの表示
				returnPositionOnStartCheckBox.Checked = ViewerSettings.ReturnPositionOnStart;
				returnSizeOnStartCheckBox.Checked = ViewerSettings.ReturnSizeOnStart;
				saveReturnPositionCheckBox.Checked = ViewerSettings.SaveReturnPositionOnClose;
				saveReturnSizeCheckBox.Checked = ViewerSettings.SaveReturnSizeOnClose;
			};
		}

		/// <summary>
		/// 保存ボタン押下
		/// </summary>
		private void saveButton_Click(object sender, EventArgs e)
		{
			// ビューワー設定
			ViewerSettings.ReturnPositionOnStart = returnPositionOnStartCheckBox.Checked;
			ViewerSettings.ReturnSizeOnStart = returnSizeOnStartCheckBox.Checked;
			ViewerSettings.SaveReturnPositionOnClose = saveReturnPositionCheckBox.Checked;
			ViewerSettings.SaveReturnSizeOnClose = saveReturnSizeCheckBox.Checked;

			// 設定を保存
			ViewerSettings.Save();
			Close();
		}

		/// <summary>
		/// キャンセル押下
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
