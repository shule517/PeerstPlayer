using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeerstPlayer.Control
{
	public partial class StatusBar : UserControl
	{
		// スレッドタイトル
		public String ThreadTitle
		{
			get { return threadTitleLabel.Text; }
			set { threadTitleLabel.Text = value; }
		}

		// チャンネル詳細
		public String ChannelDetail
		{
			get { return channelDetailLabel.Text; }
			set { channelDetailLabel.Text = value; }
		}

		// 音量
		public String Volume
		{
			get { return volumeLabel.Text; }
			set { volumeLabel.Text = value; }
		}

		// コンストラクタ
		public StatusBar()
		{
			InitializeComponent();

			// 書き込み欄：サイズ変更イベント
			writeField.SizeChanged += writeField_SizeChanged;
		}

		// 書き込み欄：サイズ変更イベント
		void writeField_SizeChanged(object sender, EventArgs e)
		{
			// 書き込み欄パネルのサイズ変更
			writeFieldPanel.Height = threadTitleLabel.Height + writeField.Height;

			// チャンネル詳細パネルの移動
			channelDetailPanel.Top = writeFieldPanel.Bottom;

			// ステータスバーのサイズ変更
			Height = writeFieldPanel.Height + channelDetailPanel.Height;
		}

		// チャンネル詳細ラベル：マウス押下イベント
		private void channelDetailLabel_MouseDown(object sender, MouseEventArgs e)
		{
			// 書き込み欄の表示切り替え
			writeFieldPanel.Visible = !writeFieldPanel.Visible;

			// 表示
			if (writeFieldPanel.Visible)
			{
				writeFieldPanel.Enabled = true;

				channelDetailPanel.Top = writeFieldPanel.Bottom;

				// ステータスバーのサイズ変更
				ClientSize = new System.Drawing.Size(ClientSize.Width, writeFieldPanel.Height + channelDetailPanel.Height);
				//Height = writeFieldPanel.Height + channelDetailPanel.Height;
			}
			// 非表示
			else
			{
				writeFieldPanel.Enabled = false;

				channelDetailPanel.Top = 0;

				// ステータスバーのサイズ変更
				//Height = channelDetailPanel.Height;
				ClientSize = new System.Drawing.Size(ClientSize.Width, channelDetailPanel.Height);
			}
		}
	}
}
