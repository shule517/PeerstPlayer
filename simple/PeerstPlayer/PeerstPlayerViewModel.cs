using Shule.Peerst.BBS;
using Shule.Peerst.Form;
using Shule.Peerst.PeerCast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PeerstPlayer
{
	delegate void VolumeChangeEventHandler();		// ボリューム変更
	delegate void DurationChangeEventHandler();		// 再生時間変更
	delegate void ThreadTitleChangeEventHandler();	// スレッドタイトル変更
	delegate void ChannelInfoChangeEventHandler();	// チャンネル情報変更

	class PeerstPlayerViewModel
	{
		// イベント
		public event VolumeChangeEventHandler OnVolumeChange;			// ボリューム変更
		public event DurationChangeEventHandler DurationChange;			// 再生時間変更
		public event ThreadTitleChangeEventHandler OnThreadTitleChange;	// スレッドタイトル変更
		public event ChannelInfoChangeEventHandler OnChannelInfoChange;	// チャンネル情報変更

		// 掲示板管理
		OperationBbs operationBbs = new OperationBbs();

		// Peca管理
		PeerCastManager pecaManager;

		// スレッド
		BackgroundWorker backgroundWorker = new BackgroundWorker();

		/// <summary>
		/// チャンネル情報
		/// </summary>
		public ChannelInfo ChannelInfo { get { return pecaManager.ChannelInfo; } }

		/// <summary>
		/// スレッド情報
		/// </summary>
		public ThreadInfo ThreadInfo { get { return operationBbs.ThreadInfo; } }

		/// <summary>
		/// スレッドURL
		/// </summary>
		public string ThreadUrl { get { return operationBbs.ThreadUrl; } }

		/// <summary>
		/// 掲示板名
		/// </summary>
		public string BbsName { get { return operationBbs.BbsName; } }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PeerstPlayerViewModel()
		{
			// backgroundWorker
			this.backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
		}

		/// <summary>
		/// 動画を開く
		/// </summary>
		/// <param name="streamUrl">ストリームURL</param>
		public void Open(string streamUrl)
		{
			// ストリームURLの解析
			PecaInfo pecaInfo = StreamUrlAnalyzer.GetPecaInfo(streamUrl);

			// チャンネル情報取得
			pecaManager = new PeerCastManager(pecaInfo.Host, pecaInfo.PortNo, pecaInfo.StreamId);

			// チャンネル情報取得スレッドの実行
			backgroundWorker.RunWorkerAsync();
		}

		/// <summary>
		/// スレッドビューワを開く
		/// </summary>
		public void OpenThreadViewer()
		{
			try
			{
				// スレビューワを開く
				string viewerPath = FormUtility.GetExeFileDirectory() + "\\PeerstViewer.exe";
				Process.Start(viewerPath, ThreadUrl + " " + ChannelInfo.Name);
			}
			catch
			{
			}
		}

		/// <summary>
		/// チャンネル情報取得
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			// 取得できるまでループ
			for (; ; )
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
			// スレタイ変更
			OnThreadTitleChange();

			// チャンネル情報更新
			OnChannelInfoChange();
		}

		/// <summary>
		/// 書き込み
		/// </summary>
		/// <param name="name"></param>
		/// <param name="mail"></param>
		/// <param name="message"></param>
		public bool Write(string name, string mail, string message)
		{
			return operationBbs.Write(name, mail, message);
		}

		/// <summary>
		/// スレッドURL変更
		/// </summary>
		/// <param name="threadUrl"></param>
		/// <param name="threadNo"></param>
		public void ChangeUrl(string threadUrl, string threadNo)
		{
			operationBbs.ChangeUrl(threadUrl, threadNo);

			// スレタイ変更
			OnThreadTitleChange();
		}
	}
}
