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
	// PeerCast対応の動画再生プレイヤー
	public partial class PecaPlayer : UserControl
	{
		public PecaPlayer()
		{
			InitializeComponent();

			// 初期設定
			wmp.uiMode = "none";
			wmp.stretchToFit = true;
		}

		// 指定URLを再生する
		public void open(string streamUrl)
		{

		}
	}
}
