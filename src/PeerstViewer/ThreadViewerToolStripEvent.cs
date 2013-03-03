using PeerstPlayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Shule.Peerst.Util;

namespace PeerstViewer
{
	partial class ThreadViewer
	{
		/// <summary>
		/// 更新ボタン
		/// </summary>
		private void toolStripButtonReload_Click(object sender, EventArgs e)
		{
			// 自動更新を停止
			timerAutoReload.Stop();

			// スレURLを更新
			string url = this.comboBox.Text;
			if (isEnableUrl(url))
			{
				operationBbs.ChangeUrl(url);
			}

			// 更新
			Reload(true);

			// 自動更新を開始
			timerAutoReload.Start();
		}

		/// <summary>
		/// 自動更新ボタン
		/// </summary>
		private void toolStripButtonAutoReload_Click(object sender, EventArgs e)
		{
			IsAutoReload = !IsAutoReload;
		}

		/// <summary>
		/// スクロールボタン(下)を押下
		/// </summary>
		private void toolStripButtonScroolBottom_Click(object sender, EventArgs e)
		{
			// 最下位までスクロール
			try
			{
				webBrowser.Document.Window.ScrollTo(0, webBrowser.Document.Body.ScrollRectangle.Bottom);
			}
			catch
			{
			}
		}

		/// <summary>
		/// スクロールボタン(上)を押下
		/// </summary>
		private void toolStripButtonScrollTop_Click(object sender, EventArgs e)
		{
			// 最上位までスクロール
			try
			{
				webBrowser.Document.Window.ScrollTo(0, 0);
			}
			catch
			{
			}
		}

		/// <summary>
		/// 書き込み欄の表示
		/// </summary>
		private void toolStripButtonWriteView_Click(object sender, EventArgs e)
		{
			IsWriteView = !IsWriteView;
		}

		/// <summary>
		/// 最前列表示ボタン押下
		/// </summary>
		private void toolStripButtonTopMost_Click(object sender, EventArgs e)
		{
			toolStripButtonTopMost.Checked = !toolStripButtonTopMost.Checked;
			TopMost = toolStripButtonTopMost.Checked;
		}
		
		/// <summary>
		/// クリップボードからURLを開く
		/// </summary>
		private void toolStripButtonOpenClipBoad_Click(object sender, EventArgs e)
		{
			// クリップボードを取得
			String url = String.Empty;

			// クリップボードに文字列データがあるか確認
			if (Clipboard.ContainsText())
			{
				// 文字列データがあるときはこれを取得する
				// 取得できないときは空の文字列（String.Empty）を返す
				url = Clipboard.GetText();
			}

			// スレッド一覧更新
			if (url.Length > 0)
			{
				operationBbs.ChangeUrl(url);
				ThreadListUpdate();
			}
		}

		/// <summary>
		/// URLをクリップボードにコピー
		/// </summary>
		private void URLをコピー_Click(object sender, EventArgs e)
		{
			// クリップボードへコピー
			// アプリケーション終了後もクリップボードにデータを残しておく
			Clipboard.SetDataObject(operationBbs.GetUrl(), true);
		}

		private void 折り返し表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Viewer", "NoBR", (!折り返し表示ToolStripMenuItem.Checked).ToString());

			NoBR = (!折り返し表示ToolStripMenuItem.Checked);
		}

		/// <summary>
		/// 設定メニューを開いた時
		/// </summary>
		private void toolStripDropDownButtonSetting_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Viewer");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Viewer", keys[i]);
				switch (keys[i])
				{
					case "NoBR":
						折り返し表示ToolStripMenuItem.Checked = (data == "True");
						break;
				}
			}
		}

		/// <summary>
		/// デフォルトメニューを開いた時
		/// </summary>
		private void デフォルトToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Viewer");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Viewer", keys[i]);
				switch (keys[i])
				{
					case "TopMost":
						最前列表示toolStripMenuItem.Checked = (data == "True");
						break;
					case "WriteView":
						書き込み欄の表示toolStripMenuItem.Checked = (data == "True");
						break;
					case "ScrollBottom":
						最下位へスクロールtoolStripMenuItem.Checked = (data == "True");
						break;
					case "AutoReload":
						自動更新ToolStripMenuItem.Checked = (data == "True");
						break;
				}
			}
		}

		/// <summary>
		/// 自動更新クリック
		/// </summary>
		private void 自動更新ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Viewer", "AutoReload", (!自動更新ToolStripMenuItem.Checked).ToString());
		}

		/// <summary>
		/// 最前列表示クリック
		/// </summary>
		private void 最前列表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Viewer", "TopMost", (!最前列表示toolStripMenuItem.Checked).ToString());
		}

		/// <summary>
		/// 書き込み欄の表示クリック
		/// </summary>
		private void 書き込み欄の表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Viewer", "WriteView", (!書き込み欄の表示toolStripMenuItem.Checked).ToString());
		}

		/// <summary>
		/// 最下位へスクロールクリック
		/// </summary>
		private void 最下位へスクロールToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Viewer", "ScrollBottom", (!最下位へスクロールtoolStripMenuItem.Checked).ToString());
		}

		/// <summary>
		/// X：オープン
		/// </summary>
		private void xToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Viewer");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Viewer", keys[i]);
				switch (keys[i])
				{
					case "X":
						toolStripTextBoxX.Text = data;
						break;
				}
			}
		}

		/// <summary>
		/// Y：オープン
		/// </summary>
		private void yToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Viewer");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Viewer", keys[i]);
				switch (keys[i])
				{
					case "Y":
						toolStripTextBoxY.Text = data;
						break;
				}
			}
		}

		/// <summary>
		/// 幅：オープン
		/// </summary>
		private void 幅ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Viewer");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Viewer", keys[i]);
				switch (keys[i])
				{
					case "Width":
						toolStripTextBoxWidth.Text = data;
						break;
				}
			}
		}

		/// <summary>
		/// 高さ：オープン
		/// </summary>
		private void 高さToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Viewer");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Viewer", keys[i]);
				switch (keys[i])
				{
					case "Height":
						toolStripTextBoxHeight.Text = data;
						break;
				}
			}
		}

		/// <summary>
		/// X：エンター
		/// </summary>
		private void toolStripTextBoxX_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
				iniFile.Write("Viewer", "X", toolStripTextBoxX.Text);
			}
		}

		/// <summary>
		/// Y：エンター
		/// </summary>
		private void toolStripTextBoxY_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
				iniFile.Write("Viewer", "Y", toolStripTextBoxY.Text);
			}
		}

		/// <summary>
		/// 幅：エンター
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripTextBoxWidth_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
				iniFile.Write("Viewer", "Width", toolStripTextBoxWidth.Text);
			}
		}

		/// <summary>
		/// 高さ：エンター
		/// </summary>
		private void toolStripTextBoxHeight_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
				iniFile.Write("Viewer", "Height", toolStripTextBoxHeight.Text);
			}
		}
		private void 位置を保存するToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Viewer", "X", Location.X.ToString());
			iniFile.Write("Viewer", "Y", Location.Y.ToString());
		}

		private void サイズを保存するToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Viewer", "Width", Size.Width.ToString());
			iniFile.Write("Viewer", "Height", Size.Height.ToString());
		}
	}
}
