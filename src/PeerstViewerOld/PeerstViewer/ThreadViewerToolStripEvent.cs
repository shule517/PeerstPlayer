using PeerstLib.Controls;
using PeerstViewer.Settings;
using System;
using System.Windows.Forms;

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
				// URL更新
				InitDocumentText();
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
				// URL更新
				InitDocumentText();
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
			Clipboard.SetDataObject(operationBbs.ThreadUrl, true);
		}

		private void 折り返し表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ViewerSettings.NoBR = (!折り返し表示ToolStripMenuItem.Checked);
			ViewerSettings.Save();
		}

		/// <summary>
		/// 設定メニューを開いた時
		/// </summary>
		private void toolStripDropDownButtonSetting_DropDownOpened(object sender, EventArgs e)
		{
			折り返し表示ToolStripMenuItem.Checked = ViewerSettings.NoBR;
		}

		/// <summary>
		/// デフォルトメニューを開いた時
		/// </summary>
		private void デフォルトToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			最前列表示toolStripMenuItem.Checked = ViewerSettings.TopMost;
			書き込み欄の表示toolStripMenuItem.Checked = ViewerSettings.WriteView;
			最下位へスクロールtoolStripMenuItem.Checked = ViewerSettings.ScrollBottom;
			自動更新ToolStripMenuItem.Checked = ViewerSettings.AutoReload;
		}

		/// <summary>
		/// 自動更新クリック
		/// </summary>
		private void 自動更新ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ViewerSettings.AutoReload = !自動更新ToolStripMenuItem.Checked;
			ViewerSettings.Save();
		}

		/// <summary>
		/// 最前列表示クリック
		/// </summary>
		private void 最前列表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ViewerSettings.TopMost = !最前列表示toolStripMenuItem.Checked;
			ViewerSettings.Save();
		}

		/// <summary>
		/// 書き込み欄の表示クリック
		/// </summary>
		private void 書き込み欄の表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ViewerSettings.WriteView = !書き込み欄の表示toolStripMenuItem.Checked;
			ViewerSettings.Save();
		}

		/// <summary>
		/// 最下位へスクロールクリック
		/// </summary>
		private void 最下位へスクロールToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ViewerSettings.ScrollBottom = !最下位へスクロールtoolStripMenuItem.Checked;
			ViewerSettings.Save();
		}

		/// <summary>
		/// X：オープン
		/// </summary>
		private void xToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			toolStripTextBoxX.Text = ViewerSettings.X.ToString(); ;
		}

		/// <summary>
		/// X：エンター
		/// </summary>
		private void toolStripTextBoxX_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				int x = 0;
				int.TryParse(toolStripTextBoxX.Text, out x);
				ViewerSettings.X = x;
				ViewerSettings.Save();
			}
		}

		/// <summary>
		/// Y：オープン
		/// </summary>
		private void yToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			toolStripTextBoxY.Text = ViewerSettings.Y.ToString();
		}

		/// <summary>
		/// Y：エンター
		/// </summary>
		private void toolStripTextBoxY_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				int y = 0;
				if (int.TryParse(toolStripTextBoxY.Text, out y))
				{
					ViewerSettings.Y = y;
					ViewerSettings.Save();
				}
			}
		}

		/// <summary>
		/// 幅：オープン
		/// </summary>
		private void 幅ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			toolStripTextBoxWidth.Text = ViewerSettings.Width.ToString();
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
				int width = 0;
				if (int.TryParse(toolStripTextBoxWidth.Text, out width))
				{
					ViewerSettings.Width = width;
					ViewerSettings.Save();
				}
			}
		}

		/// <summary>
		/// 書き込み欄の高さ：オープン
		/// </summary>
		private void 高さToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			toolStripTextBoxHeight.Text = ViewerSettings.WriteHeight.ToString();
		}

		/// <summary>
		/// 高さ：エンター
		/// </summary>
		private void toolStripTextBoxHeight_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				int height = 0;
				if (int.TryParse(toolStripTextBoxHeight.Text, out height))
				{
					ViewerSettings.Height = height;
					ViewerSettings.Save();
				}
			}
		}

		/// <summary>
		/// 位置を保存する
		/// </summary>
		private void 位置を保存するToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ViewerSettings.X = Location.X;
			ViewerSettings.Y = Location.Y;
			ViewerSettings.Save();
		}

		/// <summary>
		/// サイズを保存する
		/// </summary>
		private void サイズを保存するToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ViewerSettings.Width = Size.Width;
			ViewerSettings.Height = Size.Height;
			ViewerSettings.Save();
		}
	}
}
