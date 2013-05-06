using PeerstLib.Form;
using PeerstLib.PeerCast;
using PeerstPlayer.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer
{
	// 動画プレイヤーの表示
	public partial class PlayerView : Form
	{
		public PlayerView()
		{
			InitializeComponent();

			// TODO デバッグ開始

			// 最小化ボタン
			minToolStripButton.Click += (sender, e) =>
			{
				this.WindowState = FormWindowState.Minimized;
			};
			// 最大化ボタン
			maxToolStripButton.Click += (sender, e) =>
			{
				if (this.WindowState == FormWindowState.Normal)
				{
					this.WindowState = FormWindowState.Maximized;
				}
				else
				{
					this.WindowState = FormWindowState.Normal;
				}
			};
			// 閉じるボタン
			closeToolStripButton.Click += (sender, e) =>
			{
				this.Close();
			};
			// マウスドラッグ
			pecaPlayer.MouseDownEvent = (sender, e) =>
			{
				FormUtility.WindowDragStart(this.Handle);
			};
			// 選択スレッドをクリック
			statusBar.SelectThreadClick += (sender, e) =>
			{
				PlayerSettingView view = new PlayerSettingView();
				view.Show();
			};
			// チャンネル情報更新
			pecaPlayer.ChannelInfoChange += (sender, e) =>
			{
				ChannelInfo info = pecaPlayer.ChannelInfo;
				statusBar.ChannelDetail = info.Name + " [" + info.Genre + "] " + info.Desc;
			};

			// 動画再生
			pecaPlayer.Open("http://localhost:7145/pls/9072B7771C771AB60CDB6AF9A846B64D?tip=114.167.196.248:7144");
			// TODO デバッグ終了
		}
	}
}
