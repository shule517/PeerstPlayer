using System.Collections.Generic;
using System.Windows.Forms;
using PeerstLib.Util;
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
				shortcutListView.Items.Clear();
				foreach (KeyValuePair<Commands, ShortcutCommand> commandPair in shortcut.CommandMap)
				{
					ListViewItem item = new ListViewItem();
					item.Text = shortcut.CommandMap[commandPair.Key].Detail;
					item.Tag = commandPair.Key;

					ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
					subItem.Text = "-";
					subItem.Tag = null;

					foreach (KeyValuePair<KeyInput, Commands> keyPair in shortcut.Settings.KeyMap)
					{
						if (commandPair.Key == keyPair.Value)
						{
							string modifiers = (keyPair.Key.Modifiers == Keys.None) ? "" : ", " + keyPair.Key.Modifiers.ToString();
							subItem.Text = keyPair.Key.Key + modifiers;
							subItem.Tag = keyPair.Key;
							break;
						}
					}

					item.SubItems.Add(subItem);
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


				// 同じキー入力は削除
				foreach (ListViewItem item in shortcutListView.Items)
				{
					object tag = item.SubItems[1].Tag;	// KeyInput

					if ((tag == null))
					{
						continue;
					}

					KeyInput keyInput = (KeyInput)tag;

					if ((keyInput.Key == e.KeyCode) && (keyInput.Modifiers == e.Modifiers))
					{
						item.SubItems[1].Text = "-";
						item.SubItems[1].Tag = null;
					}
				}

				// ショートカット登録
				int index = shortcutListView.SelectedIndices[0];
				shortcutListView.Items[index].SubItems[1].Text = e.KeyData.ToString();
				shortcutListView.Items[index].SubItems[1].Tag = new KeyInput(e.Modifiers, e.KeyCode);

				// キー入力を無視
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
			shortcut.Settings.KeyMap = new Dictionary<KeyInput, Commands>();
			foreach (ListViewItem item in shortcutListView.Items)
			{
				object tag1 = item.Tag;				// Commands
				object tag2 = item.SubItems[1].Tag;	// KeyInput

				if ((tag1 == null) || (tag2 == null))
				{
					continue;
				}

				Commands command = (Commands)tag1;
				KeyInput keyInput = (KeyInput)tag2;
				shortcut.Settings.KeyMap.Add(keyInput, command);
			}
			SettingSerializer.SaveSettings<ShortcutSettings>("ShortcutSettings.xml", shortcut.Settings);
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
