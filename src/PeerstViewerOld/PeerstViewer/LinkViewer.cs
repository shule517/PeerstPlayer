using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web;

namespace PeerstViewer
{
	public partial class LinkViewer : Form
	{
		public LinkViewer()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 開く
		/// </summary>
		public void Show(string url, Point MousePosition)
		{
			Show();
			Visible = true;
			Focus();

			/*
			// ZIP
			string Ext = Path.GetExtension(url);
			if (Ext == ".zip")
			{
				Visible = false;
				if (MessageBox.Show(url + "\nダウンロードしますか？", "ダウンロード", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					webBrowser.Url = new Uri(url);
				}
			}
			// その他
			else
			{
				url = HttpUtility.UrlDecode(url, Encoding.GetEncoding("EUC-JP"));
				webBrowser.Url = new Uri(url);
				textBoxURL.Text = url;
			}
			 */
			url = HttpUtility.UrlDecode(url, Encoding.GetEncoding("EUC-JP"));
			webBrowser.Url = new Uri(url);
			textBoxURL.Text = url;
		}

		/// <summary>
		/// フォームを終了させない
		/// </summary>
		private void LinkViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Visible = false;

			webBrowser.DocumentText = "Loading...";
		}

		/// <summary>
		/// 読み込み完了
		/// </summary>
		private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			// フォーカス
			Focus();

			// URLを更新
			textBoxURL.Text = webBrowser.Url.ToString();
		}

		/// <summary>
		/// URLBoxでKeyDown
		/// </summary>
		private void textBoxURL_KeyDown(object sender, KeyEventArgs e)
		{
			// URLを更新
			if (e.KeyCode == Keys.Enter)
			{
				try
				{
					string url = "";

					if (textBoxURL.Text.Length > 7)
					{
						if (textBoxURL.Text.Substring(0, 7) == "http://")
						{
							url = textBoxURL.Text;
							webBrowser.Url = new Uri(url);
							textBoxURL.Text = url;
							return;
						}
					}
					
					if (textBoxURL.Text.Length > 6)
					{
						if (textBoxURL.Text.Substring(0, 6) == "ttp://")
						{
							url = "h" + textBoxURL.Text;
							webBrowser.Url = new Uri(url);
							textBoxURL.Text = url;
							return;
						}
					}

					url = "http://" + textBoxURL.Text;
					webBrowser.Url = new Uri(url);
					textBoxURL.Text = url;
					return;
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// URLボックスMouseDown
		/// </summary>
		private void textBoxURL_MouseDown(object sender, MouseEventArgs e)
		{
			textBoxURL.SelectAll();
		}
	}
}
