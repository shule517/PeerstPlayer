using System.Collections.Generic;
using System.Windows.Forms;
using PeerstPlayer.Forms.Player;
using PeerstPlayer.Shortcut;
using PeerstPlayer.Shortcut.Command;

namespace PeerstPlayer.Forms.Setting
{
	/// <summary>
	/// プレイヤー設定画面表示クラス
	/// </summary>
	public partial class PlayerSettingView : Form
	{
		/// <summary>
		/// ショートカット
		/// </summary>
		private ShortcutManager shortcut;

		public PlayerSettingView(ShortcutManager shortcut)
		{
			this.shortcut = shortcut;

			InitializeComponent();

			// 親ウィンドウの中心に表示
			StartPosition = FormStartPosition.CenterParent;

			// 初期化
			Shown += (sender, e) =>
			{
				// チェックボックスの表示
				disconnectRealyOnCloseCheckBox.Checked = PlayerSettings.DisconnectRealyOnClose;
				windowSnapEnableCheckBox.Checked = PlayerSettings.WindowSnapEnable;
				topMostCheckBox.Checked = PlayerSettings.TopMost;
				writeFieldVisibleCheckBox.Checked = PlayerSettings.WriteFieldVisible;

				// ショートカット表示
				foreach (KeyValuePair<Commands, ShortcutCommand> commandPair in shortcut.CommandMap)
				{
					ListViewItem item = new ListViewItem();

					item.Text = shortcut.CommandMap[commandPair.Key].Detail;
					item.Tag = commandPair.Key;

					string inputKey = "-";
					foreach (KeyValuePair<KeyInput, Commands> keyPair in shortcut.Settings.KeyMap)
					{
						if (commandPair.Key == keyPair.Value)
						{
							string modifiers = (keyPair.Key.Modifiers == Keys.None) ? "" : ", " + keyPair.Key.Modifiers.ToString();
							inputKey = keyPair.Key.Key + modifiers;
							break;
						}
					}

					item.SubItems.Add(inputKey);
					shortcutListView.Items.Add(item);
				}
			};

			// Tab遷移しないようにする
			shortcutListView.PreviewKeyDown += (sender, e) =>
			{
				e.IsInputKey = true;
			};

			// ショートカット設定時にエラー音がならないようにする
			shortcutListView.KeyPress += (sender, e) =>
				e.Handled = true;

			// ショートカット入力
			shortcutListView.KeyDown += (sender, e) =>
			{
				if (shortcutListView.SelectedIndices.Count <= 0) return;
				if (e.KeyData == Keys.ProcessKey) return;

				int index = shortcutListView.SelectedIndices[0];
				shortcutListView.Items[index].SubItems[1].Text = e.KeyData.ToString();

				e.Handled = true;
			};

			// ダブルクリックでコマンド削除
			shortcutListView.DoubleClick += (sender, e) =>
			{
				if (shortcutListView.SelectedIndices.Count <= 0) return;

				int index = shortcutListView.SelectedIndices[0];
				shortcutListView.Items[index].SubItems[1].Text = "-";
				shortcutListView.Items[index].SubItems[1].Tag = null;
			};
		}

		/// <summary>
		/// 保存ボタン押下
		/// </summary>
		private void saveButton_Click(object sender, System.EventArgs e)
		{
			// プレイヤー設定
			PlayerSettings.DisconnectRealyOnClose = disconnectRealyOnCloseCheckBox.Checked;
			PlayerSettings.WindowSnapEnable = windowSnapEnableCheckBox.Checked;
			PlayerSettings.TopMost = topMostCheckBox.Checked;
			PlayerSettings.WriteFieldVisible = writeFieldVisibleCheckBox.Checked;
			PlayerSettings.Save();
			Close();

			// ショートカット設定
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
