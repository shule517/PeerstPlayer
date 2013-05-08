using PeerstLib.Control;
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
using WMPLib;

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
			pecaPlayer.MouseDownEvent += (sender, e) =>
			{
				FormUtility.WindowDragStart(this.Handle);
			};

			// チャンネル情報更新
			pecaPlayer.ChannelInfoChange += (sender, e) =>
			{
				ChannelInfo info = pecaPlayer.ChannelInfo;
				statusBar.ChannelDetail = info.Name + " [" + info.Genre + "] " + info.Desc;

				// TODO 初回だけコンタクトURLを設定する
				statusBar.SelectThreadUrl = info.Url;
			};

			// ステータスバーのサイズ変更
			statusBar.HeightChanged += (sender, e) =>
			{
				this.ClientSize = new Size(ClientSize.Width, pecaPlayer.Height + statusBar.Height);
			};

			// サイズ変更
			SizeChanged += (sender, e) =>
			{
				// 幅
				pecaPlayer.Width = ClientSize.Width;
				statusBar.Width = ClientSize.Width;

				// 高さ
				pecaPlayer.Height = ClientSize.Height - statusBar.Height;
				statusBar.Top = pecaPlayer.Bottom;
			};

			// 音量クリック
			statusBar.VolumeClick += (sender, e) =>
			{
				// ミュート切替
				pecaPlayer.Mute = !pecaPlayer.Mute;

				if (pecaPlayer.Mute)
				{
					statusBar.Volume = "-";
				}
				else
				{
					statusBar.Volume = pecaPlayer.Volume.ToString();
				}
			};

			// マウスホイール
			MouseWheel += (sender, e) =>
			{
				// 音量変更
				if (e.Delta > 0)
				{
					pecaPlayer.Volume += 10;
				}
				else if (e.Delta < 0)
				{
					pecaPlayer.Volume -= 10;
				}

				// 表示反映
				statusBar.Volume = pecaPlayer.Volume.ToString();
			};
			Timer timer = new Timer();
			timer.Interval = 1000;
			timer.Tick += (sender, e) =>
			{
				// 動画ステータスを表示
				if (pecaPlayer.Duration.Length == 0)
				{
					switch (pecaPlayer.PlayState)
					{
						case WMPPlayState.wmppsUndefined: statusBar.MovieStatus = "未定義"; break;
						case WMPPlayState.wmppsStopped: statusBar.MovieStatus = "停止"; break;
						case WMPPlayState.wmppsPaused: statusBar.MovieStatus = "一時停止"; break;
						case WMPPlayState.wmppsPlaying: statusBar.MovieStatus = "再生中"; break;
						case WMPPlayState.wmppsScanForward: statusBar.MovieStatus = "早送り"; break;
						case WMPPlayState.wmppsScanReverse: statusBar.MovieStatus = "巻き戻し"; break;
						case WMPPlayState.wmppsBuffering: statusBar.MovieStatus = "バッファ中"; break;
						case WMPPlayState.wmppsWaiting: statusBar.MovieStatus = "接続待機"; break;
						case WMPPlayState.wmppsMediaEnded: statusBar.MovieStatus = "再生完了"; break;
						case WMPPlayState.wmppsTransitioning: statusBar.MovieStatus = "準備中"; break;
						case WMPPlayState.wmppsReady: statusBar.MovieStatus = "準備完了"; break;
						case WMPPlayState.wmppsReconnecting: statusBar.MovieStatus = "再接続"; break;
					}
				}
				// 再生時間を表示
				else
				{
					statusBar.MovieStatus = pecaPlayer.Duration;
				}
			};
			timer.Start();

			// マウスオーバー時にフォーカスを当てる
			MouseEnter += (sender, e) =>
			{
				if (!this.Focused)
				{
					this.Focus();
				}
			};

			// ダブルクリック
			pecaPlayer.DoubleClick += (sender, e) =>
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

			// 書き込み欄の非表示
			statusBar.WriteFieldVisible = false;
			// TODO デバッグ終了
		}

		public void Open(string url)
		{
			pecaPlayer.Open(url);
		}
	}
}
