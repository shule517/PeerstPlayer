using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PeerstPlayer
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			PlayerView playerView = new PlayerView();
			
			// TODO デバッグ開始

			// 設定情報を反映
			playerView.TopMost = true;
			// 動画再生
			playerView.Open("http://localhost:7145/pls/9072B7771C771AB60CDB6AF9A846B64D?tip=114.167.196.248:7144");

			// TODO デバッグ終了

			Application.Run(playerView);
		}
	}
}
