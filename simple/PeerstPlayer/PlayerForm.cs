using Shule.Peerst.BBS;
using Shule.Peerst.Form;
using Shule.Peerst.PeerCast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PeerstPlayer
{
	public partial class PlayerForm : Form, ThreadSelectObserver
	{
		// 掲示板管理
		OperationBbs operationBbs = new OperationBbs();

		// Peca管理
		PeerCastManager pecaManager = null;

		// スレッド選択フォーム
		ThreadSelectForm threadSelectForm = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PlayerForm()
		{
			InitializeComponent();
			threadSelectForm = new ThreadSelectForm(this);
			FormManager.VisibleTitlebar(Handle, false);
			MouseWheel += PlayerForm_MouseWheel;
		}

		#region ライブラリ化したい

		/// <summary>
		/// 作業フォルダを取得
		/// </summary>
		string GetCurrentDirectory()
		{
			if (Environment.GetCommandLineArgs().Length > 0)
			{
				string folder = Environment.GetCommandLineArgs()[0];
				folder = folder.Substring(0, folder.LastIndexOf('\\'));

				return folder;
			}
			return "";
		}

		/// <summary>
		/// スレッドビューワを開く
		/// </summary>
		public void OpenThreadViewer()
		{
			// スレビューワを開く
			string viewerPath = GetCurrentDirectory() + "\\PeerstViewer.exe";
			string threadUrl = operationBbs.GetUrl();
			Process.Start(viewerPath, threadUrl + " " + pecaManager.ChannelInfo.Name);
		}

		#endregion

		/// <summary>
		/// マウスホイールイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PlayerForm_MouseWheel(object sender, MouseEventArgs e)
		{
			if (wmp.settings == null)
			{
				return;
			}

			// 音量調節
			if (e.Delta > 0)
			{
				wmp.settings.volume += 5;
			}
			else
			{
				wmp.settings.volume -= 5;
			}
		}

		/// <summary>
		/// ロードイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PlayerForm_Load(object sender, EventArgs e)
		{
			if (Environment.GetCommandLineArgs().Length <= 1)
			{
				// コマンドライン引数指定なし
				return;
			}
		
			// 動画再生
			string streamUrl = Environment.GetCommandLineArgs()[1];
			wmp.URL = streamUrl;

			// スレッドの実行
			backgroundWorker.RunWorkerAsync();
		}

		/// <summary>
		/// チャンネル情報取得
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			// ストリームURLの解析
			string streamUrl = Environment.GetCommandLineArgs()[1];
			PecaInfo pecaInfo = StreamUrlAnalyzer.GetPecaInfo(streamUrl);

			// チャンネル情報取得
			pecaManager = new PeerCastManager(pecaInfo.Host, pecaInfo.PortNo, pecaInfo.StreamId);

			// 取得できるまでループ
			for (;;)
			{
				bool result = pecaManager.GetChannelInfo();

				if ((pecaManager.ChannelInfo != null) && (pecaManager.ChannelInfo.Name != ""))
				{
					break;
				}
				Thread.Sleep(1);
				Application.DoEvents();
			}

			// スレッド更新
			operationBbs.ChangeUrl(pecaManager.ChannelInfo.ContactUrl);
		}

		/// <summary>
		/// チャンネル情報取得完了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			// ステータスラベルの更新
			channelInfoLabel.Text = pecaManager.ChannelInfo.ToString();

			// スレッドタイトルの更新
			threadTitleLabel.Text = operationBbs.GetBbsName();
		}

		/// <summary>
		/// スレッドURL更新通知
		/// </summary>
		/// <param name="threadUrl"></param>
		/// <param name="threadName"></param>
		void ThreadSelectObserver.UpdateThreadUrl(string threadUrl, string threadName)
		{
			// スレッド更新
			operationBbs.ChangeUrl(threadUrl);
			operationBbs.ChangeThread(threadName);

			// スレッドタイトルの更新
			threadTitleLabel.Text = operationBbs.GetBbsName();
		}

		/// <summary>
		/// 書き込み欄：キーダウンイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void writeField_KeyDown(object sender, KeyEventArgs e)
		{
			// Ctrl + Enter
			if (e.Control && (e.KeyCode == Keys.Enter))
			{
				// レス書き込み
				operationBbs.Write("", "sage", writeField.Text);
				writeField.Text = "";
			}
		}

		/// <summary>
		/// WMP：マウスダウンイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void wmp_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
		{
			// マウスドラッグ
			Win32API.SendMessage(Handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTCAPTION), new IntPtr(0));
		}

		/// <summary>
		/// スレッドタイトルラベル：キーダウンイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void threadTitleLabel_MouseDown(object sender, MouseEventArgs e)
		{
			// 左クリック
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				// スレッド選択フォームを開く
				threadSelectForm.Update(operationBbs.GetUrl());
				threadSelectForm.Show();
			}
			// 右クリック
			else
			{
				// スレビューワを開く
				OpenThreadViewer();
			}
		}
	}
}
