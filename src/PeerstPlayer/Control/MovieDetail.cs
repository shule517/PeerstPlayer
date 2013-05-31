using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
			InitializeComponent();

			// 動画詳細クリック
			ChannelDetailLabel.MouseClick += (sender, e) => ChannelDetailClick(sender, e);
			movieStatusLabel.MouseClick += (sender, e) => ChannelDetailClick(sender, e);

			// 音量クリック
			volumeLabel.Click += (sender, e) => VolumeClick(sender, e);
		}
	}
}
