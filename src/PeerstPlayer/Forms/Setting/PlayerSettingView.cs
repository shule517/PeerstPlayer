using System;
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

		#region 動画再生開始時のコマンド一覧

		/// <summary>
		/// 動画再生開始時のコマンド一覧
		/// </summary>
		private Commands[] movieStartCommandList = {

			// 処理なし
			Commands.None,

			// 動画サイズに合わせる
			Commands.FitMovieSize,
			
			// ウィンドウ拡大率指定
			Commands.WindowScale50Per,
			Commands.WindowScale75Per,
			Commands.WindowScale100Per,
			Commands.WindowScale150Per,
			Commands.WindowScale200Per,
			
			// ウィンドウサイズ指定
			Commands.WindowSize160x120,
			Commands.WindowSize320x240,
			Commands.WindowSize480x360,
			Commands.WindowSize640x480,
			Commands.WindowSize800x600,
			
			// 画面分割
			Commands.ScreenSplitWidthx5,
			Commands.ScreenSplitWidthx4,
			Commands.ScreenSplitWidthx3,
			Commands.ScreenSplitWidthx2,
			Commands.ScreenSplitWidthx1,
			Commands.ScreenSplitHeightx5,
			Commands.ScreenSplitHeightx4,
			Commands.ScreenSplitHeightx3,
			Commands.ScreenSplitHeightx2,
			Commands.ScreenSplitHeightx1,
		};

		#endregion
		
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
				returnPositionOnStartCheckBox.Checked = PlayerSettings.ReturnPositionOnStart;
				returnSizeOnStartCheckBox.Checked = PlayerSettings.ReturnSizeOnStart;
				windowSnapEnableCheckBox.Checked = PlayerSettings.WindowSnapEnable;
				aspectRateFixCheckBox.Checked = PlayerSettings.AspectRateFix;
				frameInvisibleCheckBox.Checked = PlayerSettings.FrameInvisible;
				topMostCheckBox.Checked = PlayerSettings.TopMost;
				writeFieldVisibleCheckBox.Checked = PlayerSettings.WriteFieldVisible;
				initVolumeTextBox.Text = PlayerSettings.InitVolume.ToString();
				saveReturnPositionCheckBox.Checked = PlayerSettings.SaveReturnPositionOnClose;
				saveReturnSizeCheckBox.Checked = PlayerSettings.SaveReturnSizeOnClose;
				exitedViewerCloseCheckBox.Checked = PlayerSettings.ExitedViewerClose;

				// チェックボックスの設定(ステータスバー)
				displayFpsCheckBox.Checked = PlayerSettings.DisplayFps;
				displayBitrateCheckBox.Checked = PlayerSettings.DisplayBitrate;
				listenerNumberCheckBox.Checked = PlayerSettings.DisplayListenerNumber;
				hideStatusBarOnFullscreenCheckBox.Checked = PlayerSettings.HideStatusBarOnFullscreen;

				// 音量変化
				volumeChangeNoneTextBox.Text = PlayerSettings.VolumeChangeNone.ToString();
				volumeChangeCtrlTextBox.Text = PlayerSettings.VolumeChangeCtrl.ToString();
				volumeChangeShiftTextBox.Text = PlayerSettings.VolumeChangeShift.ToString();

				// スクリーンショット
				screenshotFolderTextBox.Text = PlayerSettings.ScreenshotFolder;
				foreach (var item in screenshotExtensionComboBox.Items)
				{
					if (item.ToString() == PlayerSettings.ScreenshotExtension)
					{
						screenshotExtensionComboBox.SelectedItem = item;
					}
				}
				// 拡張子が選択されていなければpngを選ばせる
				if (screenshotExtensionComboBox.SelectedItem == null)
				{
					screenshotExtensionComboBox.SelectedIndex = screenshotExtensionComboBox.FindString("png");
				}
				// 書式
				screenshotFormatTextBox.Text = PlayerSettings.ScreenshotFormat;
				screenshotSampleTextBox.Text = Shortcut.Command.ScreenshotCommand.Format(screenshotFormatTextBox.Text, null);

				// スレッド
				autoReadThreadCheckBox.Checked = PlayerSettings.AutoReadThread;

				// FLV
				flvGpuCheckBox.Checked = PlayerSettings.Gpu;

				// VLC
				vlcFolderTextBox.Text = PlayerSettings.VlcFolder;

				// 動画再生開始時のコマンド
				movieStartComboBox.Items.Clear();
				foreach (Commands command in movieStartCommandList)
				{
					string detail = shortcut.CommandMap[command].Detail;
					movieStartComboBox.Items.Add(detail);

					if (PlayerSettings.MovieStartCommand == command)
					{
						movieStartComboBox.SelectedIndex = (movieStartComboBox.Items.Count - 1);
					}
				}

				// ショートカット・ジェスチャー表示
				shortcutListView.Items.Clear();
				foreach (KeyValuePair<Commands, ShortcutCommand> commandPair in shortcut.CommandMap)
				{
					// ショートカットのアイテム追加
					ListViewItem keyItem = CreateKeyItem(shortcut, commandPair);
					shortcutListView.Items.Add(keyItem);
				}
			};

			// スクリーンショットを保存するフォルダを選ぶダイアログを表示するボタン
			browseScreenshotFolderButton.Click += (sender, args) =>
			{
				folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
				folderBrowserDialog.SelectedPath = PlayerSettings.ScreenshotFolder;
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				{
					screenshotFolderTextBox.Text = folderBrowserDialog.SelectedPath;
				}
			};

			// スクリーンショットファイル名の書式
			screenshotFormatTextBox.TextChanged += (sender, args) =>
			{
				screenshotSampleTextBox.Text = Shortcut.Command.ScreenshotCommand.Format(screenshotFormatTextBox.Text, null);
			};
			// 書式の補助メニュー
			screenshotFormatButton.Click += (sender, args) => screenshotContextMenuStrip.Show(screenshotFormatButton, 0, 0);
			Action<string> formatHelper = (x) =>
			{
				// 選択されているテキストの位置を取得しておく
				var selectionStart = screenshotFormatTextBox.SelectionStart;
				screenshotFormatTextBox.Text = screenshotFormatTextBox.Text.Insert(screenshotFormatTextBox.SelectionStart, x);
				// 追加した文字列の後ろにカーソルを移動
				screenshotFormatTextBox.SelectionStart = selectionStart + x.Length;
				screenshotFormatTextBox.Focus();
			};
			year2ToolStripMenuItem.Click += (sender, args) => formatHelper("yy");
			year4ToolStripMenuItem.Click += (sender, args) => formatHelper("yyyy");
			monthToolStripMenuItem.Click += (sender, args) => formatHelper("MM");
			dayToolStripMenuItem.Click += (sender, args) => formatHelper("dd");
			hourToolStripMenuItem.Click += (sender, args) => formatHelper("hh");
			minuteToolStripMenuItem.Click += (sender, args) => formatHelper("mm");
			secondToolStripMenuItem.Click += (sender, args) => formatHelper("ss");
			channelNameToolStripMenuItem.Click += (sender, args) => formatHelper("$0");

			// VLCのフォルダを選ぶダイアログを表示するボタン
			browseVlcFolderButton.Click += (sender, args) =>
			{
				folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
				folderBrowserDialog.SelectedPath = PlayerSettings.VlcFolder;
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				{
					vlcFolderTextBox.Text = folderBrowserDialog.SelectedPath;
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

				// 同じキー入力を削除
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
			shortcutListView.MouseDoubleClick += (sender, e) =>
			{
				if (shortcutListView.SelectedIndices.Count <= 0) return;

				if (e.Button == MouseButtons.Left)
				{
					int index = shortcutListView.SelectedIndices[0];
					shortcutListView.Items[index].SubItems[1].Text = "-";
					shortcutListView.Items[index].SubItems[1].Tag = null;
				}
				else if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					int index = shortcutListView.SelectedIndices[0];
					shortcutListView.Items[index].SubItems[2].Text = "-";
					shortcutListView.Items[index].SubItems[2].Tag = null;
				}
			};

			// マウスジェスチャ
			MouseGesture mouseGesture = new MouseGesture();
			mouseGesture.Interval = PlayerSettings.MouseGestureInterval;
			bool gestureing = false;
			shortcutListView.MouseDown += (sender, e) =>
			{
				if (e.Button == MouseButtons.Right)
				{
					mouseGesture.Start();
					gestureing = true;
				}
			};
			shortcutListView.MouseMove += (sender, e) =>
			{
				if (shortcutListView.SelectedIndices.Count <= 0) return;
				if (gestureing == false) return;

				mouseGesture.Moving(e.Location);

				int index = shortcutListView.SelectedIndices[0];

				string gesture = mouseGesture.ToString();
				if (string.IsNullOrEmpty(gesture)) return;
				if (shortcutListView.Items[index].SubItems[2].Text == mouseGesture.ToString()) return;

				// 同じジェスチャを削除
				foreach (ListViewItem item in shortcutListView.Items)
				{
					string text = item.SubItems[2].Text;
					if (gesture == text)
					{
						item.SubItems[2].Text = "-";
						item.SubItems[2].Tag = null;
					}
				}

				// ジェスチャを登録
				shortcutListView.Items[index].SubItems[2].Text = gesture;
				shortcutListView.Items[index].SubItems[2].Tag = null;
			};
			shortcutListView.MouseUp += (sender, e) => gestureing = false;
		}

		/// <summary>
		/// ショートカットのアイテム作成
		/// </summary>
		private ListViewItem CreateKeyItem(ShortcutManager shortcut, KeyValuePair<Commands, ShortcutCommand> commandPair)
		{
			ListViewItem item = new ListViewItem();
			item.Text = shortcut.CommandMap[commandPair.Key].Detail;
			item.Tag = commandPair.Key;

			ListViewItem.ListViewSubItem shortcutSubItem = GetShortcutSubItem(shortcut, commandPair, item);
			ListViewItem.ListViewSubItem gestureSubItem = GetGestureSubItem(shortcut, commandPair, item);
			item.SubItems.Add(shortcutSubItem);
			item.SubItems.Add(gestureSubItem);

			return item;
		}

		/// <summary>
		/// マウスジェスチャのアイテム取得
		/// </summary>
		private static ListViewItem.ListViewSubItem GetGestureSubItem(ShortcutManager shortcut, KeyValuePair<Commands, ShortcutCommand> commandPair, ListViewItem item)
		{
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

			return subItem;
		}

		/// <summary>
		/// ショートカットのアイテム取得
		/// </summary>
		private ListViewItem.ListViewSubItem GetShortcutSubItem(ShortcutManager shortcut, KeyValuePair<Commands, ShortcutCommand> commandPair, ListViewItem item)
		{
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

			return subItem;
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
			PlayerSettings.ReturnPositionOnStart = returnPositionOnStartCheckBox.Checked;
			PlayerSettings.ReturnSizeOnStart = returnSizeOnStartCheckBox.Checked;
			PlayerSettings.WindowSnapEnable = windowSnapEnableCheckBox.Checked;
			PlayerSettings.AspectRateFix = aspectRateFixCheckBox.Checked;
			PlayerSettings.FrameInvisible = frameInvisibleCheckBox.Checked;
			PlayerSettings.TopMost = topMostCheckBox.Checked;
			PlayerSettings.WriteFieldVisible = writeFieldVisibleCheckBox.Checked;
			PlayerSettings.SaveReturnPositionOnClose = saveReturnPositionCheckBox.Checked;
			PlayerSettings.SaveReturnSizeOnClose = saveReturnSizeCheckBox.Checked;
			PlayerSettings.ExitedViewerClose = exitedViewerCloseCheckBox.Checked;

			// チェックボックスの設定(ステータスバー)
			PlayerSettings.DisplayFps = displayFpsCheckBox.Checked;
			PlayerSettings.DisplayBitrate = displayBitrateCheckBox.Checked;
			PlayerSettings.DisplayListenerNumber = listenerNumberCheckBox.Checked;
			PlayerSettings.HideStatusBarOnFullscreen = hideStatusBarOnFullscreenCheckBox.Checked;

			// 初期音量
			int initVolume = 0;
			if (int.TryParse(initVolumeTextBox.Text, out initVolume))
			{
				PlayerSettings.InitVolume = initVolume;
			}

			// 音量変化
			int volumeChangeNone = 10;
			if (int.TryParse(volumeChangeNoneTextBox.Text, out volumeChangeNone))
			{
				PlayerSettings.VolumeChangeNone = volumeChangeNone;
			}
			int volumeChangeCtrl = 5;
			if (int.TryParse(volumeChangeCtrlTextBox.Text, out volumeChangeCtrl))
			{
				PlayerSettings.VolumeChangeCtrl = volumeChangeCtrl;
			}
			int volumeChangeShift = 1;
			if (int.TryParse(volumeChangeShiftTextBox.Text, out volumeChangeShift))
			{
				PlayerSettings.VolumeChangeShift = volumeChangeShift;
			}

			// 動画再生開始時のコマンド
			if (movieStartComboBox.SelectedIndex != -1)
			{
				PlayerSettings.MovieStartCommand = movieStartCommandList[movieStartComboBox.SelectedIndex];
			}

			// スクリーンショット
			PlayerSettings.ScreenshotFolder = screenshotFolderTextBox.Text;
			PlayerSettings.ScreenshotExtension = screenshotExtensionComboBox.SelectedItem.ToString();
			PlayerSettings.ScreenshotFormat = screenshotFormatTextBox.Text;

			// スレッド
			PlayerSettings.AutoReadThread = autoReadThreadCheckBox.Checked;

			// FLV
			PlayerSettings.Gpu = flvGpuCheckBox.Checked;

			// VLC
			PlayerSettings.VlcFolder = vlcFolderTextBox.Text;

			// 設定を保存
			PlayerSettings.Save();
			Close();

			// ショートカット設定
			shortcut.Settings.KeyMap = new Dictionary<KeyInput, Commands>();
			shortcut.Settings.GestureMap = new Dictionary<string, Commands>();
			foreach (ListViewItem item in shortcutListView.Items)
			{
				object tag1 = item.Tag;				// Commands
				object tag2 = item.SubItems[1].Tag;	// KeyInput

				// コマンド取得失敗
				if (tag1 == null)
				{
					continue;
				}

				Commands command = (Commands)item.Tag;

				// ショートカット設定
				if (tag2 != null)
				{
					KeyInput keyInput = (KeyInput)item.SubItems[1].Tag;
					shortcut.Settings.KeyMap.Add(keyInput, command);
				}

				// マウスジェスチャ設定
				string gesture = item.SubItems[2].Text;
				if (gesture != "-")
				{
					shortcut.Settings.GestureMap.Add(gesture, command);
				}
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
