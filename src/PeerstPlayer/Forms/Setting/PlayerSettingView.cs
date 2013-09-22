using System.Collections.Generic;
using System.Windows.Forms;
using PeerstLib.Util;
using PeerstPlayer.Forms.Player;
using PeerstPlayer.Shortcut;

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

				// ショートカット・ジェスチャー表示
				shortcutListView.Items.Clear();
				gestureListView.Items.Clear();
				foreach (KeyValuePair<Commands, ShortcutCommand> commandPair in shortcut.CommandMap)
				{
					// ショートカットのアイテム追加
					ListViewItem keyItem = CreateKeyItem(shortcut, commandPair);
					shortcutListView.Items.Add(keyItem);

					// ジェスチャーのアイテム追加
					ListViewItem gestureItem = createGestureItem(shortcut, commandPair);
					gestureListView.Items.Add(gestureItem);
				}
			};

			// Tab遷移しないようにする
			shortcutListView.PreviewKeyDown += (sender, e) => e.IsInputKey = true;

			// ショートカット設定時にエラー音がならないようにする
			shortcutListView.KeyPress += (sender, e) => e.Handled = true;

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
				KeyInput input = new KeyInput(e.Modifiers, e.KeyCode);
				int index = shortcutListView.SelectedIndices[0];
				shortcutListView.Items[index].SubItems[1].Text = ConvertKeyInputToString(input);
				shortcutListView.Items[index].SubItems[1].Tag = input;

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
		/// ショートカットのアイテム作成
		/// </summary>
		private ListViewItem CreateKeyItem(ShortcutManager shortcut, KeyValuePair<Commands, ShortcutCommand> commandPair)
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
					subItem.Text = ConvertKeyInputToString(keyPair.Key);
					subItem.Tag = keyPair.Key;
					break;
				}
			}

			item.SubItems.Add(subItem);
			return item;
		}

		/// <summary>
		/// ジェスチャーのアイテム作成
		/// </summary>
		private ListViewItem createGestureItem(ShortcutManager shortcut, KeyValuePair<Commands, ShortcutCommand> commandPair)
		{
			ListViewItem item = new ListViewItem();
			item.Text = shortcut.CommandMap[commandPair.Key].Detail;
			item.Tag = commandPair.Key;

			ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
			subItem.Text = "-";
			subItem.Tag = null;

			foreach (KeyValuePair<string, Commands> keyPair in shortcut.Settings.GestureMap)
			{
				if (commandPair.Key == keyPair.Value)
				{
					subItem.Text = keyPair.Key;
					subItem.Tag = keyPair.Key;
					break;
				}
			}

			item.SubItems.Add(subItem);
			return item;
		}

		/// <summary>
		/// KeyInputを文字列に変換
		/// </summary>
		private string ConvertKeyInputToString(KeyInput input)
		{
			string key = new KeysConverter().ConvertToInvariantString(input.Key);
			string modifiers = input.Modifiers.ToString();
			string text = (input.Modifiers == Keys.None) ? key : modifiers + " + " + key;
			return text;
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
