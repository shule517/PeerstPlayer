using System;
using System.Windows.Forms;
using PeerstLib.Utility;

namespace PeerstPlayer.Control
{
	//-------------------------------------------------------------
	// 概要：動画詳細コントロールクラス
	//-------------------------------------------------------------
	public partial class MovieDetail : UserControl
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// チャンネル詳細
		public string ChannelDetail
		{
			get { return ChannelDetailLabel.Text; }
			set { ChannelDetailLabel.Text = value; }
		}

		// 動画ステータス
		public string MovieStatus
		{
			get { return movieStatusLabel.Text; }
			set { movieStatusLabel.Text = value; }
		}

		// 音量
		public string Volume
		{
			get { return volumeLabel.Text; }
			set { volumeLabel.Text = value; }
		}

		// 動画詳細のクリックイベント
		public event MouseEventHandler ChannelDetailClick = delegate { };

		// 音量のクリックイベント
		public event EventHandler VolumeClick = delegate { };

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：イベントの設定
		//-------------------------------------------------------------
		public MovieDetail()
		{
			Logger.Instance.Debug("MovieDetail()");
			InitializeComponent();

			// チャンネル詳細クリック
			ChannelDetailLabel.MouseClick += (sender, e) =>
			{
				Logger.Instance.Info("チャンネル詳細クリック");
				ChannelDetailClick(sender, e);
			};
			movieStatusLabel.MouseClick += (sender, e) =>
			{
				Logger.Instance.Info("チャンネル詳細クリック");
				ChannelDetailClick(sender, e);
			};

			// 音量クリック
			volumeLabel.Click += (sender, e) =>
			{
				Logger.Instance.Info("音量クリック");
				VolumeClick(sender, e);
			};
		}
	}
}
