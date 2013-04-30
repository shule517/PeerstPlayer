using PeerstPlayer.Event;
using Shule.Peerst.BBS;
using Shule.Peerst.PeerCast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace PeerstPlayer
{
	public class PlayerViewModel
	{
		//-----------------------
		// 公開プロパティ
		//-----------------------

		// プロパティ
		public class Property
		{
			public const string ChannelInfo = "ChannelInfo";
			public const string ThreadInfo = "ThreadInfo";
			public const string BbsName = "BbsName";
		};

		// プロパティ変更イベント
		public event PropertyChangedEventHandler PropertyChanged;

		// チャンネル情報
		public ChannelInfo ChannelInfo { get { return pecaManager.ChannelInfo; } }

		// スレッド情報
		public ThreadInfo ThreadInfo { get { return operationBbs.ThreadInfo; } }

		// 掲示板名
		public string BbsName { get { return operationBbs.BbsName; } }

		//-----------------------
		// 内部変数
		//-----------------------

		// PeerCast操作
		private PeerCastManager pecaManager = null;

		// 掲示板操作
		private OperationBbs operationBbs = new OperationBbs();

		// チャンネル更新worker
		private BackgroundWorker updateChannelInfoWorker = new BackgroundWorker();

		//-----------------------
		// メソッド
		//-----------------------

		// コンストラクタ
		public PlayerViewModel()
		{
			updateChannelInfoWorker.DoWork += updateChannelInfoWorker_DoWork;
			updateChannelInfoWorker.RunWorkerCompleted += updateChannelInfoWorker_RunWorkerCompleted;
		}

		// チャンネル更新
		void updateChannelInfoWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			for (; ; )
			{
				bool result = pecaManager.GetChannelInfo();
				if (result)
				{
					break;
				}
				Thread.Sleep(1);
			}
		}

		// チャンネル更新完了
		void updateChannelInfoWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			NotifyPropertyChanged(Property.ChannelInfo);
			ChangeThreadUrl(pecaManager.ChannelInfo.ContactUrl);
		}

		// 動画再生
		public void OpenMovie(string streamUrl)
		{
			PecaInfo info = StreamUrlAnalyzer.GetPecaInfo(streamUrl);
			pecaManager = new PeerCastManager(info.Host, info.PortNo, info.StreamId);

			// チャンネル更新
			updateChannelInfoWorker.RunWorkerAsync();
		}

		// イベント実行
		public void DoEvents(FormEventArgs args)
		{
		}

		// プロパティ変更通知
		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null){ return; }
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		// スレッドURL変更
		public void ChangeThreadUrl(string threadUrl)
		{
			operationBbs.ChangeUrl(threadUrl);
			NotifyPropertyChanged(Property.ThreadInfo);
			NotifyPropertyChanged(Property.BbsName);
		}
	}
}
