using System.Threading;
using System.Windows.Forms;
using AxWMPLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstPlayer.Controls.PecaPlayer;
using TestPeerstLib;
using WMPLib;

namespace TestPeertPlayer
{
	[TestClass]
	public class PecaPlayerTest
	{
		//-------------------------------------------------------------
		// 確認：指定URLが再生できるか
		//-------------------------------------------------------------
		[TestMethod]
		public void Test_PecaPlayer_Open_PeerCastMovie()
		{
			// PeerCastアドレス：動画が再生されること
			OpenTest(TestSettings.StreamUrl);

			// TODO PeerCast以外のアドレス：PeerCastに接続しないこと
			// 動画が表示されること
			//OpenTest("http://localhost:7145/pls/9BFF1089B90973DD388ECB7A4B4B2EDB?tip=183.181.158.208:7154");

			// ローカル動画ファイル：動画が再生されること
			OpenTest(TestSettings.LocalMoviePath);
		}

		//-------------------------------------------------------------
		// 概要：PlayStateChangeが発生するか確認
		//-------------------------------------------------------------
		private static void OpenTest(string streamUrl)
		{
			// 再生中の判定
			bool isPlaying = false;

			// イベント登録
			PecaPlayerForm form = new PecaPlayerForm();
			form.Show();

			PecaPlayerControl pecaPlayer = form.pecaPlayer;
			PrivateObject accessor = new PrivateObject(pecaPlayer);
			AxWindowsMediaPlayer wmp = (AxWindowsMediaPlayer)accessor.GetField("wmp");
			wmp.PlayStateChange += (sender, e) =>
			{
				WMPPlayState playState = wmp.playState;
				if (playState == WMPPlayState.wmppsPlaying)
				{
					isPlaying = true;
				}
			};

			// テスト対象を実行
			pecaPlayer.Open(streamUrl);

			// 再生されるまで待つ
			while (isPlaying == false)
			{
				Application.DoEvents();
				Thread.Sleep(100);
			}
		}
	}
}
