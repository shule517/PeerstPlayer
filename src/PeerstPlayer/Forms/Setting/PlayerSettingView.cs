using System.Windows.Forms;
using PeerstPlayer.Forms.Player;

namespace PeerstPlayer.Forms.Setting
{
	/// <summary>
	/// プレイヤー設定画面表示クラス
	/// </summary>
	public partial class PlayerSettingView : Form
	{
		public PlayerSettingView()
		{
			InitializeComponent();

			// 親ウィンドウの中心に表示
			StartPosition = FormStartPosition.CenterParent;

			// チェックボックスの表示
			Shown += (sender, e) =>
			{
				disconnectRealyOnCloseCheckBox.Checked = PlayerSettings.DisconnectRealyOnClose;
				windowSnapEnableCheckBox.Checked = PlayerSettings.WindowSnapEnable;
				topMostCheckBox.Checked = PlayerSettings.TopMost;
				writeFieldVisibleCheckBox.Checked = PlayerSettings.WriteFieldVisible;
			};
		}

		/// <summary>
		/// 保存ボタン押下
		/// </summary>
		private void saveButton_Click(object sender, System.EventArgs e)
		{
			PlayerSettings.DisconnectRealyOnClose = disconnectRealyOnCloseCheckBox.Checked;
			PlayerSettings.WindowSnapEnable = windowSnapEnableCheckBox.Checked;
			PlayerSettings.TopMost = topMostCheckBox.Checked;
			PlayerSettings.WriteFieldVisible = writeFieldVisibleCheckBox.Checked;
			PlayerSettings.Save();
			Close();
		}

		/// <summary>
		/// キャンセル押下
		/// </summary>
		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
