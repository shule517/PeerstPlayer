using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using PeerstPlayer;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace PeerstViewer
{
	public partial class ThreadViewer : Form
	{
		/// <summary>
		/// 掲示板の名前（板名）
		/// </summary>
		string BoardName = "";

		/// <summary>
		/// 掲示板の種類
		/// </summary>
		KindOfBBS KindOfBBS = KindOfBBS.None;

		/// <summary>
		/// 板ジャンル
		/// </summary>
		string BoadGenre = "";

		/// <summary>
		/// 板番号
		/// </summary>
		string BoadNo = "";

		/// <summary>
		/// スレ番号
		/// </summary>
		string ThreadNo = "";

		/// <summary>
		/// 現在取得している次のレス番号
		/// </summary>
		int ResNum = 1;

		/// <summary>
		/// スクロール位置X
		/// </summary>
		int ScrollPosX = 0;

		/// <summary>
		/// スクロール位置Y
		/// </summary>
		int ScrollPosY = 0;

		/// <summary>
		/// １番下までスクロールするか
		/// </summary>
		bool IsScrollBottom = true;

		/// <summary>
		/// 初期リロード時に最下位までスクロール
		/// </summary>
		bool ScrollBottomOnOpen = true;

		/// <summary>
		/// 折りかえり表示
		/// </summary>
		bool NoBR = true;

		/// <summary>
		/// 終了時に位置保存するか
		/// </summary>
		bool SaveLocationOnClose = false;

		/// <summary>
		/// 終了時にサイズ保存するか
		/// </summary>
		bool SaveSizeOnClose = false;

		/// <summary>
		/// フォント名
		/// </summary>
		string FontName = @"<body bgcolor=""#E6EEF3"" style=""font-family:'メイリオ','ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:16px;line-height:18px;"" >";

		/// <summary>
		/// OuterText
		/// </summary>
		string OuterText = "";

		/// <summary>
		/// 現在のブラウザのHTML
		/// </summary>
		string DocumentText = "";

		/// <summary>
		/// 現在開いているスレURL
		/// </summary>
		string ThreadURL = "";

		/// <summary>
		/// レスのデータリスト
		/// </summary>
		List<string> ResList = new List<string>();

		/// <summary>
		/// スレッドの一覧
		/// </summary>
		List<string[]> ThreadList = new List<string[]>();

		/// <summary>
		/// Panel2の高さ：書き込み欄表示時のスクロール用
		/// </summary>
		int Panel2Height = 0;

		/// <summary>
		/// チャンネル名
		/// </summary>
		string ChannelName = "";

		/// <summary>
		/// 書き込み欄の表示
		/// </summary>
		bool IsWriteView
		{
			get
			{
				return toolStripButtonWriteView.Checked;
			}
			set
			{
				// チェック
				toolStripButtonWriteView.Checked = value;
				// 表示 / 非表示
				splitContainer.Panel2Collapsed = !value;

				try
				{
					if (value)
					{
						Panel2Height = splitContainer.Panel2.Height;
						webBrowser.Document.Window.ScrollTo(ScrollPosX, ScrollPosY + splitContainer.Panel2.Height);
					}
					else
					{
						webBrowser.Document.Window.ScrollTo(ScrollPosX, ScrollPosY - Panel2Height);
					}
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// オートリロード
		/// </summary>
		bool IsAutoReload
		{
			get
			{
				return toolStripButtonAutoReload.Checked;
			}
			set
			{
				// 自動更新ストップ
				if (!value)
				{
					// チェックをはずす
					toolStripButtonAutoReload.Checked = false;
					// 自動更新秒数
					toolStripComboBoxReloadTime.Enabled = true;
					// 自動更新タイマーストップ
					timerAutoReload.Stop();
				}
				// 自動更新スタート
				else
				{
					// チェックする
					toolStripButtonAutoReload.Checked = true;
					// 自動更新秒数
					toolStripComboBoxReloadTime.Enabled = false;
					try
					{
						// 自動更新タイマースタート
						int reload_time = int.Parse(toolStripComboBoxReloadTime.Text.ToString()) * 1000;
						timerAutoReload.Interval = reload_time;
						timerAutoReload.Start();
					}
					catch
					{
						// チェックをはずす
						toolStripButtonAutoReload.Checked = false;
						// 自動更新秒数
						toolStripComboBoxReloadTime.Enabled = true;
					}
				}
			}
		}

		/// <summary>
		/// 画像ビューワ
		/// </summary>
		ImageViewer imageViewer = new ImageViewer();

		/// <summary>
		/// リンクビューワ
		/// </summary>
		//LinkViewer linkViewer = new LinkViewer();

		/// <summary>
		/// オートリロードタイマー
		/// </summary>
		Timer timerAutoReload;

		/// <summary>
		/// HTTPスレッド
		/// </summary>
		System.Threading.Thread httpThread;

		/// <summary>
		/// 取得したスレッドデータ
		/// </summary>
		List<string[]> ThreadData = null;

		/// <summary>
		/// HTTPスレッド関数
		/// </summary>
		void HttpThreadMethod()
		{
			ThreadData = BBS.ReadThread(KindOfBBS, BoadGenre, BoadNo, ThreadNo, ResNum);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ThreadViewer()
		{
			InitializeComponent();
			Show();
			
			// オートリロードタイマー
			timerAutoReload = new Timer();
			timerAutoReload.Tick += new EventHandler(timerAutoReload_Tick);

			// コンボボックスを選択
			toolStripComboBoxReloadTime.SelectedIndex = 0;

			// 書き込み欄を非表示
			IsWriteView = false;

			// オートリロード開始
			IsAutoReload = true;

			// 起動時に最下位にスクロールさせるか
			IsScrollBottom = true;

			// 初期化ファイル読み込み
			LoadIni();

			// チャンネル名を取得
			if (Environment.GetCommandLineArgs().Length > 2)
			{
				ChannelName = Environment.GetCommandLineArgs()[2].ToString();
			}

			// コマンドライン入力
			if (Environment.GetCommandLineArgs().Length > 1)
			{
				// スレッドURL
				String url = Environment.GetCommandLineArgs()[1].ToString();

				// スレ一覧更新
				ThreadListUpdate(url);
			}
			else
			{
				// ブラウザのフォント、背景色を設定
				webBrowser.DocumentText = @"<body bgcolor=""#E6EEF3"" style=""font-family:'ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:14px;line-height:16px;"" ><br>↑スレッド(板)URLを入力してください。";
			}
		}

		/// <summary>
		/// スレッドビューワを開く
		/// </summary>
		public void OpenUrl(string ThreadTitle, KindOfBBS KindOfBBS, string BoadGenre, string BoadNo, string ThreadNo)
		{
			try
			{
				// 開く
				//Show();

				// データをコピー
				this.KindOfBBS = KindOfBBS;
				this.BoadGenre = BoadGenre;
				this.BoadNo = BoadNo;
				this.ThreadNo = ThreadNo;

				if (ChannelName != "")
				{
					Text = ChannelName + "：" + ThreadTitle;
				}
				else
				{
					Text = ThreadTitle;
				}

				// ブラウザのフォント、背景色を設定
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
" + FontName;
				ResNum = 1;
				IsScrollBottom = ScrollBottomOnOpen;

				ResList.Clear();
				ThreadData = null;

				// スレッド更新
				Reload(false);
			}
			catch
			{
			}
		}

		/// <summary>
		/// 自動更新ボタン
		/// </summary>
		private void toolStripButtonAutoReload_Click(object sender, EventArgs e)
		{
			IsAutoReload = !IsAutoReload;
		}

		/// <summary>
		/// 書き込み欄の表示
		/// </summary>
		private void toolStripButtonWriteView_Click(object sender, EventArgs e)
		{
			IsWriteView = !IsWriteView;
		}

		/// <summary>
		/// 書き込み
		/// </summary>
		private void buttonWrite_Click(object sender, EventArgs e)
		{
			textBoxMessage.ReadOnly = true;
			if (BBS.Write(KindOfBBS, BoadGenre, BoadNo, ThreadNo, textBoxName.Text, textBoxMail.Text, textBoxMessage.Text))
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
		/// 更新ボタン
		/// </summary>
		private void toolStripButtonReload_Click(object sender, EventArgs e)
		{
			// 自動更新を停止
			timerAutoReload.Stop();

			// スレURLを更新
			ThreadURL = this.comboBox.Text;

			// 更新
			Reload(true);

			// 自動更新を開始
			timerAutoReload.Start();
		}

		/// <summary>
		/// 更新
		/// </summary>
		void Reload(bool UseThread)
		{
			/*
			if (backgroundWorkerReload.IsBusy)
			{
				// キャンセル要求
				backgroundWorkerReload.CancelAsync();
			}
			 */

			// 動作中の場合キャンセル
			if (backgroundWorkerReload.IsBusy)
				return;

			// 実行
			comboBox.Enabled = false;
			backgroundWorkerReload.RunWorkerAsync();

			/*
			try
			{
				// スレッドデータを取得
				if (ThreadData != null)
				{
					return;
				}

				//if (UseThread)
				if (false)
				{
					// httpスレッド初期化
					httpThread = new System.Threading.Thread(new System.Threading.ThreadStart(HttpThreadMethod));
					httpThread.IsBackground = true;
					httpThread.Start();
					while (ThreadData == null)
					{
						Application.DoEvents();
					}
				}
				else
				{
					ThreadData = BBS.ReadThread(KindOfBBS, BoadGenre, BoadNo, ThreadNo, ResNum);
				}

				// 新着を太文字から変更
				string text = DocumentText.Replace("<b>", "").Replace("</b>", "");

				// 新着変更なし
				if (DocumentText == text)
				{
					// 新着レスなし & 前回に新着なし（太文字を消す）
					if (ThreadData.Count == 0)
					{
						ThreadData = null;
						return;
					}
				}
				// 新着変更あり
				else
				{
					DocumentText = text;
				}

				// レスごとにHTML作成
				foreach (string[] data in ThreadData)
				{
					string document_text = "";

					// レス番号
					document_text += "<nobr><b><u><font color=#0000FF>" + ResNum + "</font></u></b>";
					document_text += "<font color=#999999>";
					document_text += " ： ";
					// 名前 [
					document_text += "<font color=#228B22>" + data[0] + "</font> ";

					// メール欄
					if (data[1] == "")
					{
						document_text += "[] ";
					}
					else if (data[1] == "sage")
					{
						document_text += "[sage] ";
					}
					else if (data[1] == "age")
					{
						document_text += "<font color = red>[age]</font> ";
					}
					else
					{
						document_text += "<font color = blue>[" + data[1] + "]</font> ";
					}
					
					// ]日付
					document_text += data[2];

					// ID
					if (data[3] != "")
					{

						document_text += @" <font color=""blue"">ID:<nobr></font>" + data[3] + "</nobr>";
					}
					document_text += "</font><br><DD>";
					// 本文
					document_text += data[4];

					// ドキュメントに追加
					DocumentText += document_text + "<br><hr></nobr>\n";

					document_text = document_text.Replace("&gt;", ">");
					document_text = document_text.Replace("<br>", "\n");
					// タグ除去
					Regex regex = new Regex("<.*?>", RegexOptions.Singleline);
					document_text = regex.Replace(document_text, "");

					// レスリストに追加
					ResList.Add(document_text);

					// レス数の更新
					ResNum++;
				}

				// 実際にウェブを更新
				webBrowser.DocumentText = DocumentText;

				// スレッドデータ初期化
				ThreadData = null;
			}
			catch
			{
			}
			*/
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

							/*
							//ToolTipを作成する
							ToolTip ToolTip1 = new ToolTip(this.components);
							//フォームにcomponentsがない場合
							//ToolTip1 = new ToolTip();

							ToolTip1.IsBalloon = true;
							//ToolTipの設定を行う
							//ToolTipが表示されるまでの時間
							//ToolTip1.InitialDelay = 2000;
							//ToolTipが表示されている時に、別のToolTipを表示するまでの時間
							//ToolTip1.ReshowDelay = 1000;
							//ToolTipを表示する時間
							//ToolTip1.AutoPopDelay = 10000;
							//フォームがアクティブでない時でもToolTipを表示する
							//ToolTip1.ShowAlways = true;

							ToolTip1.Show(text, this, 30000);

							//Button1とButton2にToolTipが表示されるようにする
							//ToolTip1.SetToolTip(webBrowser, text);
							*/
							Help.ShowPopup(webBrowser, text, MousePosition);

							//toolTip1.IsBalloon = true;

							//toolTip1.Show(text, this, 0, 0, 10000);
							//toolTip1.SetToolTip(webBrowser, text);
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
		/// ID抽出
		/// </summary>
		private string GetIDRes(string id)
		{
			string text = "";

			for (int i = 0; i < ResList.Count; i++)
			{
				int index = ResList[i].IndexOf(id);
				if (index != -1)
				{
					if (text != "")
					{
						text += "\n\n";
					}

					text += ResList[i];
				}
			}

			return text;
		}

		/// <summary>
		/// スクロールボタンを押した
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
		/// 書き込み後の改行対策
		/// </summary>
		bool WriteCheck = false;

		/// <summary>
		/// キーダウン
		/// </summary>
		private void textBoxMessage_KeyDown(object sender, KeyEventArgs e)
		{
			// 書き込み
			if (e.Shift && e.KeyCode == Keys.Enter)
			{
				textBoxMessage.ReadOnly = true;
				if (BBS.Write(KindOfBBS, BoadGenre, BoadNo, ThreadNo, textBoxName.Text, textBoxMail.Text, textBoxMessage.Text))
				{
					textBoxMessage.Text = "";
					Reload(true);
					WriteCheck = true;
				}
				textBoxMessage.ReadOnly = false;
			}
			// 全選択
			else if (e.Control && e.KeyCode == Keys.A)
			{
				textBoxMessage.SelectAll();
			}
		}

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
		/// コンボボックスに入力されたURLを更新 / 現在の板のスレッド一覧を取得
		/// </summary>
		void ThreadListUpdate(String url)
		{
			// スレッドURLの更新
			ThreadURL = url;

			try
			{
				// URLからデータを取得
				string board_name = "";
				KindOfBBS kind_of_bbs = KindOfBBS.None;
				string boad_genre = "";
				string boad_no = "";
				string thread_no = "";

				BBS.GetDataFromUrl(ThreadURL, out board_name, out kind_of_bbs, out boad_genre, out boad_no, out thread_no);

				// 取得できていなかったら 現在開いている板のスレ一覧を更新
				if (kind_of_bbs == KindOfBBS.None || boad_genre == "" || boad_no == "")
				{
					webBrowser.Url = new Uri(comboBox.Text);
					/*
					if (KindOfBBS != KindOfBBS.None && BoadGenre != "" && BoadNo != "")
					{
						// スレッド一覧を取得
						ThreadList = BBS.GetThreadList(KindOfBBS, BoadGenre, BoadNo);

						// コンボボックスにセット
						comboBox.Items.Clear();
						for (int i = 0; i < ThreadList.Count; i++)
						{
							// スレタイ(レス数)
							comboBox.Items.Add(ThreadList[i][1] + "(" + ThreadList[i][2] + ")");
						}
					}

					if (KindOfBBS != KindOfBBS.None && BoadGenre != "" && BoadNo != "" && ThreadNo != "")
					{
						// コンボボックスのスレッドを選択
						int index = 0;
						for (int i = 0; i < ThreadList.Count; i++)
						{
							if (ThreadNo == ThreadList[i][0])
							{
								index = i;
							}
						}
						if (comboBox.Items.Count > index)
						{
							comboBox.SelectedIndex = index;
						}
					}
					else
					{
						// １番上を選択
						if (comboBox.Items.Count > 0)
						{
							comboBox.SelectedIndex = 0;
						}
					}
					 */
				}
				// データが取得できていたら新しく開きなおす
				else
				{
					// 反映
					BoardName = board_name;
					KindOfBBS = kind_of_bbs;
					BoadGenre = boad_genre;
					BoadNo = boad_no;
					ThreadNo = thread_no;

					if (BoardName != "")
					{
						Text = BoardName;
					}

					if (KindOfBBS != KindOfBBS.None && BoadGenre != "" && BoadNo != "")
					{
						// スレッド一覧を取得
						ThreadList = BBS.GetThreadList(KindOfBBS, BoadGenre, BoadNo);

						// コンボボックスにセット
						comboBox.Items.Clear();
						for (int i = 0; i < ThreadList.Count; i++)
						{
							// スレタイ(レス数)
							comboBox.Items.Add(ThreadList[i][1] + "(" + ThreadList[i][2] + ")");
						}

						// ブラウザのフォント、背景色を設定
						// webBrowser.DocumentText = @"<body bgcolor=""#E6EEF3"" style=""font-family:'ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:14px;line-height:16px;"" ><br>↑スレッドを選択してください";
					}

					if (KindOfBBS != KindOfBBS.None && BoadGenre != "" && BoadNo != "" && ThreadNo != "")
					{
						// スレッド更新
						//OpenUrl(BoardName, KindOfBBS, BoadGenre, BoadNo, ThreadNo);

						// コンボボックスのスレッドを選択
						int index = 0;
						for (int i = 0; i < ThreadList.Count; i++)
						{
							if (ThreadNo == ThreadList[i][0])
							{
								index = i;
							}
						}
						if (comboBox.Items.Count > index)
						{
							comboBox.SelectedIndex = index;
						}
					}
					else
					{
						// １番上を選択
						if (comboBox.Items.Count > 0)
						{
							comboBox.SelectedIndex = 0;
						}
					}
				}
			}
			catch
			{
			}
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
				ThreadListUpdate(this.comboBox.Text);
			}
		}

		/// <summary>
		/// コンボボックスの選択を変更
		/// </summary>
		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox.SelectedIndex != -1 && ThreadList.Count > comboBox.SelectedIndex)
			{
				// スレッドを変更
				ThreadNo = ThreadList[comboBox.SelectedIndex][0];
				OpenUrl(BoardName, KindOfBBS, BoadGenre, BoadNo, ThreadNo);

				// スレッドURLを更新
				ThreadURL = GetThreadURL();
			}
		}

		/// <summary>
		/// 現在開いているスレッドURLを取得する
		/// </summary>
		private String GetThreadURL()
		{
			String url = "";

			switch (KindOfBBS)
			{
				case PeerstPlayer.KindOfBBS.JBBS:
					url = "http://jbbs.livedoor.jp/bbs/read.cgi/" + BoadGenre + "/" + BoadNo + "/" + ThreadNo + "/";
					break;
				case PeerstPlayer.KindOfBBS.YYKakiko:
					url = "http://" + BoadGenre + "/test/read.cgi/" + BoadNo + "/" + ThreadNo + "/";
					break;
			}

			return url;
		}

		/// <summary>
		/// スレッド一覧更新ボタンクリック
		/// </summary>
		private void buttonThreadListUpdate_Click(object sender, EventArgs e)
		{
			// コンボボックスに入力されたURLを更新 / 現在の板のスレッド一覧を取得
			ThreadListUpdate(this.comboBox.Text);
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

		private void textBoxMessage_MouseMove(object sender, MouseEventArgs e)
		{
			textBoxMessage.ReadOnly = false;
		}

		private void toolStripButtonTopMost_Click(object sender, EventArgs e)
		{
			toolStripButtonTopMost.Checked = !toolStripButtonTopMost.Checked;
			TopMost = toolStripButtonTopMost.Checked;
		}

		/// <summary>
		/// 作業フォルダを取得
		/// </summary>
		string GetCurrentDirectory()
		{
			if (Environment.GetCommandLineArgs().Length > 0)
			{
				string folder = Environment.GetCommandLineArgs()[0];

				//string folder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
				folder = folder.Substring(0, folder.LastIndexOf('\\'));

				return folder;
			}
			return "";
		}

		/// <summary>
		/// 初期設定ロード
		/// </summary>
		private void LoadIni()
		{
			string iniFileName = GetCurrentDirectory() + "\\PeerstPlayer.ini";
			IniFile iniFile = new IniFile(iniFileName);

			if (!System.IO.File.Exists(iniFileName))
			{

				string str = @"/**************************************************************************/
/* ここからは、Ｐｌａｙｅｒの設定です
/* 全ての値は、デフォルト値となります
/**************************************************************************/
[Player]

// スレッド内容を字幕表示するか
// True ： 表示
// False ： 表示しない
ThreadCaption =True

// タイトルバー
// True ： 表示
// False ： 表示しない
TitleBar =False

// レスボックスの表示
// True ： 表示
// False ： 表示しない
ResBox =False

// ステータスバーの表示
// True ： 表示
// False ： 表示しない
StatusLabel =True

// フレームの表示
// True ： 表示
// False ： 表示しない
Frame =False

// アスペクト比を維持
// True : 維持する
// False : 維持しない
AspectRate =True

// 最前列表示
// True ： 最前列表示 ON
// False ： 最前列表示 OFF
TopMost =False

// ボリュームの値
Volume =50

// レスボックスの操作
// True（Enter：改行 / Shift+Enter：書き込み）
// False（Enter：書き込み / Shift+Enter：改行）
ResBoxType =True

// レスボックスを自動に隠すか
// True ： 自動に隠れる
// False ： ステータスバークリックで表示
ResBoxAutoVisible =False

// レスボックスの表示 / 非表示
// （スーテタスラベルをクリックした時の動作）
// True ： レスボックスを閉じる
// False ： レスボックスを閉じない
ClickToResBoxClose =False

// 終了時にリレーを切断するか
// True　：　終了時にリレー切断
// False ： 終了時にリレー切断しない
RlayCutOnClose =True

// 終了時に位置を保存
// True ： 保存する
// False ： 保存しない
SaveLocationOnClose =False

// 起動時の位置（X, Y）
// 指定しない場合は空白「X =」「Y =」
X =
Y =

// 起動時のサイズ（Width, Height）
// 指定しない場合は空白「Width =」「Height =」
Width =
Height =

// 起動時の拡大率
Scale =100

// 起動時に動画サイズを合わせる
FitSizeMovie =True

// 終了時にサイズを保存
// True ： 保存する
// False ： 保存しない
SaveSizeOnClose =False

// 終了時に音量を保存
// True ： 保存する
// False ： 保存しない
SaveVolumeOnClose =False

// ステータスラベルのフォント指定
FontName =MS UI Gothic

// フォントの大きさ(pt)
FontSize =9

// フォントの色(RGB)
FontColorR =0
FontColorG =255
FontColorB =128

// 書き込み時にレスボックスを閉じる
// True ： 閉じる
// False ： 閉じない
CloseResBoxOnWrite =False

// バックスペースでレスボックスを閉じる
CloseResBoxOnBackSpace =False

// マウスジェスチャーを使うか
// falseにすると、ショートカットにマウスジェスチャーが指定されていても反応しません
// True ： 使う
// False ： 使わない
UseMouseGesture =True

// スクリーン吸着をするか
// True ： する
// False ： しない
UseScreenMagnet =True

// プレイヤーを閉じたときにビューワも閉じるか
// True ： 閉じる
// False ： 閉じない
CloseViewerOnClose=True

// スクリーン吸着の感度（指定したドット範囲内だと吸着します）
ScreenMagnetDockDist=20

// マウスジェスチャー感度
MouseGestureInterval =10

/// ブラウザアドレス(exeの場所を入力してください)
BrowserAddress =

// スレッドブラウザアドレス(exeの場所を入力してください)
ThreadBrowserAddress =

/**************************************************************************/
/* ここからは、Viewerの設定です
/**************************************************************************/
[Viewer]
// 起動時の位置（X, Y）
// 指定しない場合は空白「X =」「Y =」
X =
Y =

// 起動時のサイズ（Width, Height）
// 指定しない場合は空白「Width =」「Height =」
Width =
Height =

// 最前列表示にするか
// True : 最前列表示
// False : 最前列表示にしない
TopMost =False

// 自動更新をするか
// True : 自動更新をする
// False : 自動行進をしない
AutoReload =True

// 自動更新時間（秒）（7、10、15、20、25、30）
AutoReloadInterval =7

// 書き込み欄を表示するか
// True : 表示
// False : 非表示
WriteView =False

// フォント指定
// AAがずれないフォント：<body style=""font-family:'ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:16px;line-height:18px;"" >
// htmlのbodyタグを埋め込んでフォントを変えています。 いろいろいじってみてね！
// FontName = <body bgcolor=""背景色"" style=""font-family:'フォント名';font-size:サイズpx;line-height:高さpx;"" >

FontName =<body bgcolor=""#E6EEF3"" style=""font-family:'※※※','ＭＳ Ｐゴシック','ＭＳＰゴシック','MSPゴシック','MS Pゴシック';font-size:16px;line-height:18px;"" >

// 書き込み欄の高さ
WriteHeight =223

// スレッドを開いた時に最下位まで移動させるか
// True : スクロールする
// False : スクロールしない
ScrollBottom =True
/**************************************************************************/
/* ここからは、ショートカットの設定です
/**************************************************************************/
[PlayerShortcut]
↓ = OpenThreadViewer
↓↑ = ChannelInfoUpdate
↑↓ = ThreadListUpdate
↓→ = Close
← = balance-10
→ = balance+10
MiddleClick = Mini&Mute
Shift+WheelUp = Volume+1
Shift+WheelDown = Volume-1
Control+WheelUp = Volume+5
Control+WheelDown = Volume-5
WheelUp = Volume+10
WheelDown = Volume-10
Up = Volume+10
Down = Volume-10
Alt+B = Bump
Alt+X = Close&RelayCut
Right = VolumeBalance+10
Left = VolumeBalance-10
Alt+Left = VolumeBalanceLeft
Alt+Right = VolumeBalanceRight
Alt+Down = VolumeBalanceMiddle
RightClick+WheelUp = Size-10%
RightClick+WheelDown = Size+10%
Shift+Up = Size-10%
Shift+Down = Size+10%
Control+Up = Size-10
Control+Down = Size+10
Space = SelectResBox
O = OpenThreadViewer
U = ThreadListUpdate
Escape = Close
T = TopMost
A = ResBox
S = StatusLabel
D = Frame
F = TitleBar
Z = AspectRate
Return = OpenContextMenu
Delete = Mute
Alt+Return = FullScreen
D1 = Size=50%
D2 = Size=75%
D3 = Size=100%
D4 = Size=150%
D5 = Size=200%
Alt+D1 = ScreenSplit=5
Alt+D2 = ScreenSplit=4
Alt+D3 = ScreenSplit=3
Alt+D4 = ScreenSplit=2
Q = ScreenSplitWidth=5
W = ScreenSplitWidth=4
E = ScreenSplitWidth=3
R = ScreenSplitWidth=2
Alt+Q = ScreenSplitHeight=5
Alt+W = ScreenSplitHeight=4
Alt+E = ScreenSplitHeight=3
Alt+R = ScreenSplitHeight=2
Alt+LeftClick = Frame
Right->LeftClick = Frame
P = ScreenShot
↑ = ScreenShot
L = OpenScreenShotFolder
G = FitSizeMovie
Control+V = OpenClipBoard
K = OpenFile
H = Retry
";
				FileStream writer = new FileStream(iniFileName,
									   FileMode.Create,  // 上書き
									   FileAccess.Write);

				byte[] bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(str);
				for (int i = 0; i < bytes.Length; i++)
				{
					writer.WriteByte(bytes[i]);
				}
				writer.Close();
			}

			#region 初期設定

			// デフォルト
			string[] keys = iniFile.GetKeys("Viewer");

			for (int i = 0; i < keys.Length; i++)
			{
				string data = iniFile.ReadString("Viewer", keys[i]);
				switch (keys[i])
				{
					// 初期位置X
					case "X":
						try
						{
							Left = int.Parse(data);
						}
						catch
						{
						}
						break;

					// 初期位置Y
					case "Y":
						try
						{
							Top = int.Parse(data);
						}
						catch
						{
						}
						break;

					// 初期Width
					case "Width":
						try
						{
							Width = int.Parse(data);
						}
						catch
						{
						}
						break;

					// 初期Height
					case "Height":
						try
						{
							Height = int.Parse(data);
						}
						catch
						{
						}
						break;

					// 最前列表示
					case "TopMost":
						TopMost = (data == "True");
						break;

					// 自動更新をするか
					case "AutoReload":
						IsAutoReload = (data == "True");
						break;

					// 自動更新時間
					case "AutoReloadInterval":
						switch (data)
						{
							case "7":
								toolStripComboBoxReloadTime.SelectedIndex = 0;
								break;
							case "10":
								toolStripComboBoxReloadTime.SelectedIndex = 1;
								break;
							case "15":
								toolStripComboBoxReloadTime.SelectedIndex = 2;
								break;
							case "20":
								toolStripComboBoxReloadTime.SelectedIndex = 3;
								break;
							case "25":
								toolStripComboBoxReloadTime.SelectedIndex = 4;
								break;
							case "30":
								toolStripComboBoxReloadTime.SelectedIndex = 5;
								break;
						}
						IsAutoReload = IsAutoReload;
						break;

					// 書き込み欄を表示するか
					case "WriteView":
						IsWriteView = (data == "True");
						break;

					// 初期スレッド更新時に最下位までスクロールすか
					case "ScrollBottom":
						ScrollBottomOnOpen = (data == "True");
						break;

					// フォント名
					case "FontName":
						FontName = data;
						break;

					// 書き込み欄表示
					case "WriteHeight":
						try
						{
							splitContainer.SplitterDistance = int.Parse(data);
						}
						catch
						{
						}
						break;

					// 折り返し表示
					case "NoBR":
						NoBR = (data == "True");
						break;

					// 終了時に位置を保存
					case "SaveLocationOnClose":
						SaveLocationOnClose = (data == "True");
						break;

					// 終了時にサイズを保存
					case "SaveSizeOnClose":
						SaveSizeOnClose = (data == "True");
						break;
				}
			}

			// 最前列表示
			iniFile.Write("Viewer", "TopMost", TopMost.ToString());

			// 自動更新をするか
			iniFile.Write("Viewer", "AutoReload", IsAutoReload.ToString());

			// 自動更新時間
			int time = 7;
			switch (toolStripComboBoxReloadTime.SelectedIndex)
			{
				case 0:
					time = 7;
					break;
				case 1:
					time = 10;
					break;
				case 2:
					time = 15;
					break;
				case 3:
					time = 20;
					break;
				case 4:
					time = 25;
					break;
				case 5:
					time = 30;
					break;
			}
			iniFile.Write("Viewer", "AutoReloadInterval", time.ToString());

			// 書き込み欄を表示するか
			iniFile.Write("Viewer", "WriteView", IsWriteView.ToString());

			// 初期スレッド更新時に最下位までスクロールすか
			iniFile.Write("Viewer", "ScrollBottom", ScrollBottomOnOpen.ToString());

			// フォント名
			iniFile.Write("Viewer", "FontName", FontName);

			// 書き込み欄の高さ
			iniFile.Write("Viewer", "WriteHeight", splitContainer.SplitterDistance.ToString());

			#endregion
		}

		private void ThreadViewer_FormClosed(object sender, FormClosedEventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
	
			// 位置を保存
			if (true)
			{
				iniFile.Write("Viewer", "WriteHeight", splitContainer.SplitterDistance.ToString());
			}
		}

		string BeforeThreadNo = "";
		private void backgroundWorkerReload_DoWork(object sender, DoWorkEventArgs e)
		{
			BeforeThreadNo = ThreadNo;
			ThreadData = BBS.ReadThread(KindOfBBS, BoadGenre, BoadNo, ThreadNo, ResNum);

			// キャンセル要求
			//if (backgroundWorkerReload.CancellationPending)
			//	return;

			try
			{
				// 新着を太文字から変更
				string text = DocumentText.Replace("<b>", "").Replace("</b>", "");

				// 新着変更なし
				if (DocumentText == text)
				{
					// 新着レスなし & 前回に新着なし（太文字を消す）
					if (ThreadData.Count == 0)
					{
						ThreadData = null;
						return;
					}
				}
				// 新着変更あり
				else
				{
					DocumentText = text;
				}

				// レスごとにHTML作成
				foreach (string[] data in ThreadData)
				{
					string document_text = "";
					
					// 折り返し
					if (!NoBR)
					{
						document_text += "<nobr>";
					}

					// レス番号
					document_text += "<b><u><font color=#0000FF>" + ResNum + "</font></u></b>";
					document_text += "<font color=#999999>";
					document_text += " ： ";
					// 名前 [
					document_text += "<font color=#228B22>" + data[0] + "</font> ";

					// メール欄
					if (data[1] == "")
					{
						document_text += "[] ";
					}
					else if (data[1] == "sage")
					{
						document_text += "[sage] ";
					}
					else if (data[1] == "age")
					{
						document_text += "<font color = red>[age]</font> ";
					}
					else
					{
						document_text += "<font color = blue>[" + data[1] + "]</font> ";
					}
					
					// ]日付
					document_text += data[2];

					// ID
					if (data[3] != "")
					{
						document_text += @" <font color=""blue"">ID:";
						
						// 折り返し
						if (!NoBR)
						{
							document_text += "<nobr>";
						}
						
						document_text += "</font><ID>" + data[3] + "</ID></nobr>";
					}
					document_text += "</font><br><ul>";
					//document_text += "</font><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
					//document_text += "</font><br><DD>";
					
					// 本文
					document_text += data[4];

					// ドキュメントに追加
					DocumentText += document_text + "</ul><hr></nobr>\n";

					document_text = document_text.Replace("&gt;", ">");
					document_text = document_text.Replace("<br>", "\n");

					// タグ除去
					Regex regex = new Regex("<.*?>", RegexOptions.Singleline);
					document_text = regex.Replace(document_text, "");

					// レスリストに追加
					ResList.Add(document_text);

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
				ThreadData = null;
			}
			catch
			{
			}
		}
		
		private void backgroundWorkerReload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			comboBox.Enabled = true;

			webBrowser.Visible = true;

			/*
			if (BeforeThreadNo != ThreadNo)
			{
				// 実行
				backgroundWorkerReload.RunWorkerAsync();
			}
			 */
		}

		private void backgroundWorkerInit_DoWork(object sender, DoWorkEventArgs e)
		{

		}

		private void backgroundWorkerInit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{

		}

		#region 設定

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

		#region デフォルト

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

		#endregion

		#region 状態保存

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

		#endregion

		#endregion

		#region 終了時

		/// <summary>
		/// 開いた時
		/// </summary>
		private void 終了時ToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
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
		/// クリック：位置保存
		/// </summary>
		private void 位置を保存するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Viewer", "SaveLocationOnClose", (!位置を保存するToolStripMenuItem.Checked).ToString());
			SaveLocationOnClose = !位置を保存するToolStripMenuItem.Checked;
		}

		/// <summary>
		/// クリック：サイズ保存
		/// </summary>
		private void サイズを保存するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");
			iniFile.Write("Viewer", "SaveSizeOnClose", (!サイズを保存するToolStripMenuItem.Checked).ToString());
			SaveSizeOnClose = !サイズを保存するToolStripMenuItem.Checked;
		}

		private void ThreadViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			// INI用
			IniFile iniFile = new IniFile(GetCurrentDirectory() + "\\PeerstPlayer.ini");

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

		#endregion

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		/// <summary>
		/// クリップボードからURLを開く
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
				ThreadListUpdate(url);
			}
		}

		/// <summary>
		/// URLをクリップボードにコピー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void URLをコピー_Click(object sender, EventArgs e)
		{
			// クリップボードへコピー
			// アプリケーション終了後もクリップボードにデータを残しておく
			Clipboard.SetDataObject(ThreadURL, true);
		}
	}
}
