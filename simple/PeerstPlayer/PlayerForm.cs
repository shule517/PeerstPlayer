using PeerstPlayer.Event;
using Shule.Peerst.BBS;
using Shule.Peerst.Form;
using Shule.Peerst.PeerCast;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
namespace PeerstPlayer
{
	public partial class PlayerForm : Form, ThreadSelectObserver, EventObserver
	{
		// 掲示板管理
		OperationBbs operationBbs = new OperationBbs();

		// Peca管理
		PeerCastManager pecaManager;

		// スレッド選択フォーム
		ThreadSelectForm threadSelectForm;

		// イベントマネージャ
		WmpEventManager wmpEventManager;
		FormEventManager formEventManager;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PlayerForm()
		{
			InitializeComponent();

			// スレッド選択フォーム
			threadSelectForm = new ThreadSelectForm((ThreadSelectObserver)this);

			// イベントマネージャ
			formEventManager = new FormEventManager((Form)this, (EventObserver)this);
			wmpEventManager = new WmpEventManager(wmp, (EventObserver)this);

			// タイトルバーを非表示
			FormUtility.VisibleTitlebar(Handle, false);
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

			// ストリームURLの解析
			PecaInfo pecaInfo = StreamUrlAnalyzer.GetPecaInfo(streamUrl);

			// チャンネル情報取得
			pecaManager = new PeerCastManager(pecaInfo.Host, pecaInfo.PortNo, pecaInfo.StreamId);

			// チャンネル情報取得スレッドの実行
			backgroundWorker.RunWorkerAsync();
		}

		/// <summary>
		/// チャンネル情報取得
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			// 取得できるまでループ
			for (;;)
			{
				// チャンネル情報取得
				bool result = pecaManager.GetChannelInfo();

				// 取得成功
				if (result)
				{
					break;
				}
				Thread.Sleep(1000);
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
		/// スレッドビューワを開く
		/// </summary>
		public void OpenThreadViewer()
		{
			// スレビューワを開く
			string viewerPath = FormUtility.GetCurrentDirectory() + "\\PeerstViewer.exe";
			string threadUrl = operationBbs.ThreadUrl;
			Process.Start(viewerPath, threadUrl + " " + pecaManager.ChannelInfo.Name);
		}

		/// <summary>
		/// スレッドURL更新通知
		/// </summary>
		/// <param name="threadUrl"></param>
		/// <param name="threadNo"></param>
		void ThreadSelectObserver.UpdateThreadUrl(string threadUrl, string threadNo)
		{
			// スレッド更新
			operationBbs.ChangeUrl(threadUrl, threadNo);

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
				threadSelectForm.Update(operationBbs.ThreadUrl);
				threadSelectForm.Show();
			}
			// 右クリック
			else
			{
				// スレビューワを開く
				OpenThreadViewer();
			}
		}

		/// <summary>
		/// イベント処理
		/// </summary>
		/// <param name="events">イベントの種類</param>
		/// <param name="param">パラメータ</param>
		void EventObserver.OnEvent(Events events, object param)
		{
			// TODO Chain of Responsibilityパターンを使用する

			// ホイールアップ
			if (events == Event.Events.WheelUp)
			{
				wmp.settings.volume += 5;
			}
			// ホイールダウン
			else if (events == Event.Events.WheelDown)
			{
				wmp.settings.volume -= 5;
			}
			// 左クリック
			else if (events == Event.Events.LeftClick)
			{
				// ウィンドウドラッグ開始
				FormUtility.WindowDragStart(Handle);
				
				// WMPフルスクリーン解除
				wmp.fullScreen = false;
			}
			// 左ダブルクリック
			else if (events == Event.Events.DoubleLeftClick)
			{
				// ウィンドウ最大化/解除
				FormUtility.ToggleWindowMaximize(this);

				// WMPフルスクリーン解除
				wmp.fullScreen = false;
			}
		}
	}
}
