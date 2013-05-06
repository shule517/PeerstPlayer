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
	// 動画詳細コントロール
	public partial class MovieDetail : UserControl
	{
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

		public MovieDetail()
		{
			InitializeComponent();
		}
	}
}
