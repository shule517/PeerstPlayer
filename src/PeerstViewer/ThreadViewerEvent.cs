using PeerstPlayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Shule.Peerst.Util;
using Shule.Peerst.BBS;
using Shule.Peerst.Form;

namespace PeerstViewer
{
	partial class ThreadViewer
	{
		/// <summary>
		/// 書き込み
		/// </summary>
		private void buttonWrite_Click(object sender, EventArgs e)
		{
			textBoxMessage.ReadOnly = true;

			// スレッド書き込み
			bool isSuccess = operationBbs.Write(textBoxName.Text, textBoxMail.Text, textBoxMessage.Text);
			if (isSuccess)
			{
				textBoxMessage.Text = "";
				Reload(true);
			}
			textBoxMessage.ReadOnly = false;
		}

		/// <summary>
		/// オートリロードタイマー
		/// </summary>
		void timerAutoReload_Tick(object sender, EventArgs e)
		{
			// コンボボックス(スレ一覧)を開いている時は更新しない
			if (this.comboBox.DroppedDown == false)
			{
				// 更新
				Reload(true);
			}
		}

		/// <summary>
		/// 更新完了
		/// </summary>
		private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			webBrowser.Document.MouseMove += new HtmlElementEventHandler(Document_MouseMove);
			webBrowser.Document.Window.Scroll += new HtmlElementEventHandler(Window_Scroll);
			webBrowser.Document.MouseDown += new HtmlElementEventHandler(Document_MouseDown);

			webBrowser.Visible = true;

			// 最下位までスクロール
			if (IsScrollBottom)
			{
				webBrowser.Document.Window.ScrollTo(0, webBrowser.Document.Body.ScrollRectangle.Bottom);
			}
			// 前回の位置までスクロール
			else
			{
				webBrowser.Document.Window.ScrollTo(ScrollPosX, ScrollPosY);
			}
		}

		/// <summary>
		/// MouseDown
		/// </summary>
		void Document_MouseDown(object sender, HtmlElementEventArgs e)
		{
			try
			{
				// ブラウザ上のマウスの位置を取得
				Point p = new Point();
				p = webBrowser.PointToClient(MousePosition);

				// マウスの位置にあるエレメントを取得
				HtmlElement element = webBrowser.Document.GetElementFromPoint(p);

				if (e.MouseButtonsPressed == MouseButtons.Left)
				{
					// 画像リンク
					if (element.TagName == "U")
					{
						// カーソルを変更
						Cursor.Current = Cursors.Hand;

						// 規定ブラウザでURLを開く
						Process.Start(element.OuterText);
					}
				}
				else if (e.MouseButtonsPressed == MouseButtons.Right)
				{
					// 画像リンク
					if (element.TagName == "U")
					{
						// カーソルを変更
						Cursor.Current = Cursors.Hand;

						// コンテキストメニューを開く
						OuterText = element.OuterText;
						contextMenuStrip.Show(MousePosition);
					}
				}
			}
			catch
			{
			}
		}

		/// <summary>
		/// スクロールイベント
		/// </summary>
		void Window_Scroll(object sender, HtmlElementEventArgs e)
		{
			try
			{
				// 最上位までスクロールしているか
				if (webBrowser.Document.Body.ScrollRectangle.Top == 0)
				{
					toolStripButtonScrollTop.Checked = true;
				}
				else
				{
					toolStripButtonScrollTop.Checked = false;
				}

				// 最下位までスクロールしているか
				if (webBrowser.Document.Body.ScrollRectangle.Height - webBrowser.Document.Body.ScrollRectangle.Top <= webBrowser.Height + 4 + (IsWriteView ? /*-94*/0 : 0))
				{
					toolStripButtonScroolBottom.Checked = true;
					IsScrollBottom = true;
				}
				else
				{
					toolStripButtonScroolBottom.Checked = false;
					IsScrollBottom = false;
				}

				ScrollPosX = webBrowser.Document.Body.ScrollRectangle.X;
				ScrollPosY = webBrowser.Document.Body.ScrollRectangle.Y;
			}
			catch
			{
			}
		}

		/// <summary>
		/// MouseMoveイベント
		/// </summary>
		void Document_MouseMove(object sender, HtmlElementEventArgs e)
		{
			try
			{
				// ブラウザ上のマウスの位置を取得
				Point p = new Point();
				p = webBrowser.PointToClient(MousePosition);

				// マウスの位置にあるエレメントを取得
				HtmlElement element = webBrowser.Document.GetElementFromPoint(p);

				switch (element.TagName)
				{
					// レスリンク
					case "TT":
						// カーソルを変更
						Cursor.Current = Cursors.Hand;

						// レス（リンク）
						{
							try
							{
								int res_num = int.Parse(element.InnerText) - 1;
								Help.ShowPopup(this, ResList[res_num], MousePosition);
								return;
							}
							catch
							{
								Help.ShowPopup(this, "まだレスがありません。", MousePosition);
							}

							try
							{
								int res_num = int.Parse(element.InnerText.Substring(2)) - 1;
								Help.ShowPopup(this, ResList[res_num], MousePosition);
								return;
							}
							catch
							{
								Help.ShowPopup(this, "まだレスがありません。", MousePosition);
							}
						}
						break;

					// 画像リンク
					case "U":
						if (element.InnerText.Length > 7)
						{
							// 実際のURLでは、ttp://なども存在するが
							// hはじまりのURLへ変換されているため、以下の2URLのチェックだけで問題なし
							if (((element.InnerText.Substring(0, 7) == "http://") ||
							(element.InnerText.Substring(0, 8) == "https://")))
							{
								// カーソルを変更
								Cursor.Current = Cursors.Hand;

								// 拡張子の取得
								string Ext = Path.GetExtension(element.InnerText);
								switch (Ext)
								{
									// 画像リンク
									case ".png":
									case ".PNG":
									case ".gif":
									case ".GIF":
									case ".jpg":
									case ".JPG":
									case ".bmp":
									case ".BMP":
										//if (!imageViewer.Visible)
										{
											imageViewer.Location = new Point(Control.MousePosition.X - (imageViewer.Width / 2), Control.MousePosition.Y - (imageViewer.Height / 2));
											imageViewer.Show(element.InnerHtml);
										}
										return;

									/*
									// zipはクリックで起動
									case ".zip":
										return;

							
									// その他のリンク
									default:
										if (!imageViewer.Visible)
										{
											linkViewer.Show(element.InnerHtml, MousePosition);
										}
										break;
									 */
								}
							}
						}
						break;

					// ID抽出
					case "NOBR":
						try
						{
							// ID：???は避ける
							if (element.InnerText.IndexOf("???") != -1)
								return;

							// カーソルを変更
							Cursor.Current = Cursors.Hand;

							string text = GetIDRes(element.InnerText);

							Help.ShowPopup(webBrowser, text, MousePosition);
							return;
						}
						catch
						{
						}
						break;

					case "FONT":
					case "HR":
					case "UL":
					case "BODY":
						break;

					default:
						break;
				}
			}
			catch
			{
			}
		}


		/// <summary>
		/// キーダウン
		/// </summary>
		private void textBoxMessage_KeyDown(object sender, KeyEventArgs e)
		{
			// 書き込み
			if (e.Control && (e.KeyCode == Keys.Enter))
			{
				textBoxMessage.ReadOnly = true;

				if (operationBbs.Write(textBoxName.Text, textBoxMail.Text, textBoxMessage.Text))
				{
					textBoxMessage.Text = "";
					Reload(true);
					WriteCheck = true;
				}
				textBoxMessage.ReadOnly = false;
			}
			// 全選択
			else if (e.Control && (e.KeyCode == Keys.A))
			{
				textBoxMessage.SelectAll();
			}
		}

		/// <summary>
		/// キーアップ
		/// </summary>
		private void textBoxMessage_KeyUp(object sender, KeyEventArgs e)
		{
			if (WriteCheck)
			{
				textBoxMessage.Text = "";
				WriteCheck = false;
			}
		}

		/// <summary>
		/// 書き込み欄のサイズ変更
		/// </summary>
		private void splitContainer_Panel2_SizeChanged(object sender, EventArgs e)
		{
			Panel2Height = splitContainer.Panel2.Height;
		}

		/// <summary>
		/// コンボボックスキーダウン
		/// </summary>
		private void comboBox_KeyDown(object sender, KeyEventArgs e)
		{
			// エンター
			if (e.KeyCode == Keys.Enter)
			{
				// コンボボックスに入力されたURLを更新 / 現在の板のスレッド一覧を取得
				string url = this.comboBox.Text;
				if (isEnableUrl(url))
				{
					// URL変更
					InitDocumentText();
					operationBbs.ChangeUrl(url);
				}
				ThreadListUpdate();
			}
		}

		/// <summary>
		/// コンボボックスの選択を変更
		/// </summary>
		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((comboBox.SelectedIndex != -1) && (threadList.Count > comboBox.SelectedIndex))
			{
				// スレッドを変更
				InitDocumentText();
				string threadNo = threadList[comboBox.SelectedIndex].ThreadNo;
				operationBbs.ChangeThread(threadNo);

				// スレッドを開く
				Reload(true);
			}
		}

		/// <summary>
		/// スレッド一覧更新ボタンクリック
		/// </summary>
		private void buttonThreadListUpdate_Click(object sender, EventArgs e)
		{
			// コンボボックスに入力されたURLを更新 / 現在の板のスレッド一覧を取得
			string url = this.comboBox.Text;
			if (isEnableUrl(url))
			{
				// URL変更
				InitDocumentText();
				operationBbs.ChangeUrl(url);
			}
			ThreadListUpdate();
		}

		/// <summary>
		/// 有効なURLか判定
		/// </summary>
		private bool isEnableUrl(string url)
		{
			if (url == "本スレ")
			{
				return true;
			}
			
			if (url.Length >= 7)
			{
				if (url.Substring(0, 7) == "http://")
				{
					return true;
				}
			}

			return false;
		}


		private void textBoxMessage_MouseMove(object sender, MouseEventArgs e)
		{
			textBoxMessage.ReadOnly = false;
		}

		/// <summary>
		/// レスリスト
		/// </summary>
		List<ResInfo> resList = null;

		/// <summary>
		/// スレッド更新スレッドの処理
		/// </summary>
		private void backgroundWorkerReload_DoWork(object sender, EventArgs e)
		{
			BbsInfo bbsUrl = operationBbs.BbsUrl;
			resList = operationBbs.ReadThread(bbsUrl.ThreadNo);

			try
			{
				// 新着を太文字から変更
				string text = DocumentText.Replace("<b>", "").Replace("</b>", "");

				// 新着変更なし
				if (DocumentText == "")
				{
					DocumentText = @"<head>
<style type=""text/css"">
<!--
U
{
color: #0000FF;
}

ul
{
margin: 1px 1px 1px 30px;
}

TT
{
color: #0000FF;
text-decoration:underline;
}
-->
</style>
</head>
<body bgcolor=""#E6EEF3"" style=""font-family:'※※※','ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:16px;line-height:18px;"" >";

				}
				else if (DocumentText == text)
				{
					// 新着レスなし & 前回に新着なし（太文字を消す）
					if (resList.Count == 0)
					{
						resList = null;
						return;
					}
				}
				// 新着変更あり
				else
				{
					DocumentText = text;
				}

				// レスごとにHTML作成
				foreach (ResInfo res in resList)
				{
					// レスリストに追加
					string resHtml = CreateResHtml(res);
					ResList.Add(resHtml);

					// レス数の更新
					ResNum++;
				}

				webBrowser.Invoke(
					(MethodInvoker)delegate()
					{
						webBrowser.Visible = false;
					});

				// 実際にウェブを更新
				webBrowser.DocumentText = DocumentText;

				// スレッドデータ初期化
				resList = null;
			}
			catch
			{
			}
		}

		/// <summary>
		/// レスHTMLの作成
		/// </summary>
		/// <param name="res"></param>
		/// <returns></returns>
		private string CreateResHtml(ResInfo res)
		{
			string html = "";

			// 折り返し
			if (!NoBR)
			{
				html += "<nobr>";
			}

			// レス番号
			html += "<b><u><font color=#0000FF>" + ResNum + "</font></u></b>";
			html += "<font color=#999999>";
			html += " ： ";
			// 名前 [
			html += "<font color=#228B22>" + res.Name + "</font> ";

			// メール欄
			if (res.Mail == "")
			{
				html += "[] ";
			}
			else if (res.Mail == "sage")
			{
				html += "[sage] ";
			}
			else if (res.Mail == "age")
			{
				html += "<font color = red>[age]</font> ";
			}
			else
			{
				html += "<font color = blue>[" + res.Mail + "]</font> ";
			}

			// ]日付
			html += res.Date;

			// ID
			if (res.ID != "")
			{
				html += @" <font color=""blue"">ID:";

				// 折り返し
				if (!NoBR)
				{
					html += "<nobr>";
				}

				html += "</font><ID>" + res.ID + "</ID></nobr>";
			}
			html += "</font><br><ul>";

			// 本文
			html += res.Text;

			// ドキュメントに追加
			DocumentText += html + "</ul><hr></nobr>\n";

			html = html.Replace("&gt;", ">");
			html = html.Replace("<br>", "\n");

			// タグ除去
			Regex regex = new Regex("<.*?>", RegexOptions.Singleline);
			html = regex.Replace(html, "");
			return html;
		}

		/// <summary>
		/// スレッド更新完了イベント
		/// </summary>
		private void backgroundWorkerReload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			comboBox.Enabled = true;
			webBrowser.Visible = true;
		}

		#region コンテキストメニュー

		private void アドレスをコピーToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.SetDataObject(OuterText, true);
		}

		private void リンクを開くToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LinkViewer linkViewer = new LinkViewer();
			linkViewer.Show(OuterText, MousePosition);
		}
	
		#endregion

		#region イベント

		/// <summary>
		/// 開いた時
		/// </summary>
		private void 終了時ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(FormUtility.GetExeFileDirectory() + "\\PeerstPlayer.ini");
			string[] keys = iniFile.GetKeys("Viewer");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Viewer", keys[i]);
				switch (keys[i])
				{
					case "SaveLocationOnClose":
						位置を保存するToolStripMenuItem.Checked = (data == "True");
						break;
					case "SaveSizeOnClose":
						サイズを保存するToolStripMenuItem.Checked = (data == "True");
						break;
				}
			}
		}
	
		/// <summary>
		/// フォーム終了前処理
		/// </summary>
		private void ThreadViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			// INI用
			IniFile iniFile = new IniFile(FormUtility.GetExeFileDirectory() + "\\PeerstPlayer.ini");

			// 位置を保存
			if (SaveLocationOnClose)
			{
				iniFile.Write("Viewer", "X", Left.ToString());
				iniFile.Write("Viewer", "Y", Top.ToString());
			}

			// サイズを保存
			if (SaveSizeOnClose)
			{
				if (WindowState != FormWindowState.Maximized)
				{
					iniFile.Write("Viewer", "Width", Width.ToString());
					iniFile.Write("Viewer", "Height", Height.ToString());
				}
			}
		}

		/// <summary>
		/// フォーム終了処理
		/// </summary>
		private void ThreadViewer_FormClosed(object sender, FormClosedEventArgs e)
		{
			IniFile iniFile = new IniFile(FormUtility.GetExeFileDirectory() + "\\PeerstPlayer.ini");

			// 位置を保存
			if (true)
			{
				iniFile.Write("Viewer", "WriteHeight", splitContainer.SplitterDistance.ToString());
			}
		}

		#endregion

		#region ツールストリップ

		/// <summary>
		/// クリック：位置保存
		/// </summary>
		private void 位置を保存するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(FormUtility.GetExeFileDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Viewer", "SaveLocationOnClose", (!位置を保存するToolStripMenuItem.Checked).ToString());
			SaveLocationOnClose = !位置を保存するToolStripMenuItem.Checked;
		}

		/// <summary>
		/// クリック：サイズ保存
		/// </summary>
		private void サイズを保存するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(FormUtility.GetExeFileDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Viewer", "SaveSizeOnClose", (!サイズを保存するToolStripMenuItem.Checked).ToString());
			SaveSizeOnClose = !サイズを保存するToolStripMenuItem.Checked;
		}

		#endregion
	}
}
