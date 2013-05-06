using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeerstLib.PeerCast;

namespace PeerstPlayer.Control
{
	// PeerCast対応の動画再生プレイヤー
	public partial class PecaPlayer : UserControl
	{
		// チャンネル情報
		public ChannelInfo ChannelInfo
		{
			get { if (pecaConnect != null) { return pecaConnect.GetChannelInfo(); } else { return new ChannelInfo(); } }
		}

		// マウス押下イベント
		public AxWMPLib._WMPOCXEvents_MouseDownEventHandler MouseDownEvent { set { wmp.MouseDownEvent += value; } }

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
		//-------------------------------------------------------------
		public void Open(string streamUrl)
		{
			StreamUrlInfo info = StreamUrlAnalyzer.GetUrlInfo(streamUrl);
			pecaConnect = new PeerCastConnection(info);

			// 動画の再生
			wmp.URL = streamUrl;
		}

		#region 非公開プロパティ

		// PeerCast通信
		private PeerCastConnection pecaConnect = null;

		#endregion
	}
}
