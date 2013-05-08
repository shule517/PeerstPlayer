using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeerstLib.PeerCast;
using WMPLib;

namespace PeerstPlayer.Control
{
	// PeerCast対応の動画再生プレイヤー
	public partial class PecaPlayer : UserControl
	{
		// チャンネル情報
		public ChannelInfo ChannelInfo { get; set; }

		// ミュート
		public bool Mute { get { return wmp.settings.mute; } set { wmp.settings.mute = value; } }

		// 音量
		public int Volume { get { return wmp.settings.volume; } set { wmp.settings.volume = value; } }

		// 再生時間
		public string Duration
		{
			get
			{
				string position = wmp.Ctlcontrols.currentPositionString;
				if (position.Length == 5)
				{
					position = "00:" + position;
				}

				return position;
			}
		}

		// 再生状態
		public WMPPlayState PlayState
		{
			get { return wmp.playState; }
		}

		// チャンネル情報更新イベント
		public event EventHandler ChannelInfoChange;

		// マウス押下イベント
		public event EventHandler MouseDownEvent;

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：コントロールの初期化
		//-------------------------------------------------------------
		public PecaPlayer()
		{
			InitializeComponent();

			// 初期設定
			wmp.uiMode = "none";
			wmp.stretchToFit = true;

			// マウス押下イベント
			wmp.MouseDownEvent += (sender, e) =>
			{
				if (MouseDownEvent != null) MouseDownEvent(sender, new EventArgs());
			};

			// WMPフルスクリーンを無効
			wmp.MouseDownEvent += (sender, e) =>
			{
				if (wmp.fullScreen)
				{
					wmp.fullScreen = false;
				}
			};
		}

		//-------------------------------------------------------------
		// 概要：指定URLを再生
		// 詳細：動画を再生し、チャンネル情報を取得する
		//-------------------------------------------------------------
		public void Open(string streamUrl)
		{
			StreamUrlInfo info = StreamUrlAnalyzer.GetUrlInfo(streamUrl);
			pecaConnect = new PeerCastConnection(info);

			// 動画の再生
			wmp.URL = streamUrl;

			// チャンネル情報の取得
			// TODO BackGroundで実行する
			Timer timer = new Timer();
			timer.Interval = UpdateInterval;
			timer.Tick += (sender, e) =>
			{
				this.ChannelInfo = pecaConnect.GetChannelInfo();
				if (ChannelInfoChange != null)
				{
					ChannelInfoChange(this, new EventArgs());
				}
			};
			timer.Start();
		}

		#region 非公開プロパティ

		// PeerCast通信
		private PeerCastConnection pecaConnect = null;

		// チャンネル更新間隔
		private const int UpdateInterval = 10000;

		#endregion
	}
}
