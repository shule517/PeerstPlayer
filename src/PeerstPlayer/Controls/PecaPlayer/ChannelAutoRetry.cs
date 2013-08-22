using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeerstLib.Util;

namespace PeerstPlayer.Control
{
	//-------------------------------------------------------------
	// 概要：動画再接続クラス
	//-------------------------------------------------------------
	class ChannelAutoRetry
	{
		public ChannelAutoRetry(AxWMPLib.AxWindowsMediaPlayer wmp)
		{
			//　5秒間パケット受信がなければリトライ
			int packet = 0;
			Timer timer = new Timer();
			timer.Interval = 5000;
			timer.Tick += (sender, e) =>
			{
				if (packet == wmp.network.receivedPackets)
				{
					Logger.Instance.Info("5秒間パケット受信がないため、再接続します。");
					Retry(wmp);
				}
				packet = wmp.network.receivedPackets;
			};
			timer.Start();

			// 停止されたらリトライ
			wmp.PlayStateChange += (sender, e) =>
			{
				// 停止
				if (e.newState == (int)WMPLib.WMPPlayState.wmppsStopped)
				{
					Logger.Instance.Info("動画が停止状態のため、再接続します。");
					Retry(wmp);
				}
			};
		}

		// リトライ(動画再接続)
		private void Retry(AxWMPLib.AxWindowsMediaPlayer wmp)
		{
			wmp.Ctlcontrols.stop();
			wmp.URL = wmp.URL;
		}
	}
}
