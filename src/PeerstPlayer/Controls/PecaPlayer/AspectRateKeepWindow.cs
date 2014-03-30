using PeerstLib.Controls;
using PeerstLib.Forms;
using PeerstPlayer.Forms.Player;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PeerstPlayer.Controls.PecaPlayer
{
	//-------------------------------------------------------------
	// 概要：アスペクト比維持クラス
	// 責務：指定ウィンドウのアスペクト比維持
	//-------------------------------------------------------------
	public class AspectRateKeepWindow : NativeWindow
	{
		const int WMSZ_LEFT = 1;
		const int WMSZ_RIGHT = 2;
		const int WMSZ_TOP = 3;
		const int WMSZ_TOPLEFT = 4;
		const int WMSZ_TOPRIGHT = 5;
		const int WMSZ_BOTTOM = 6;
		const int WMSZ_BOTTOMLEFT = 7;
		const int WMSZ_BOTTOMRIGHT = 8;

		private Form form;
		private PecaPlayerControl pecaPlayer;

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		//-------------------------------------------------------------
		public AspectRateKeepWindow(Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;

			// サブクラスウィンドウの設定
			AssignHandle(form.Handle);
		}

		//-------------------------------------------------------------
		// 概要：ウィンドウプロシージャ
		// 詳細：ウィンドウサイズの変更時にアスペクト比維持を実行
		//-------------------------------------------------------------
		protected override void WndProc(ref Message m)
		{
			switch ((WindowsMessage)m.Msg)
			{
				case WindowsMessage.WM_SIZING:
					if (PlayerSettings.AspectRateFix)
					{
						KeepAspectRate(m);
					}
					break;

				default:
					break;
			}

			base.WndProc(ref m);
		}

		//-------------------------------------------------------------
		// 概要：アスペクト非を維持する
		//-------------------------------------------------------------
		private void KeepAspectRate(Message m)
		{
			// 各辺の座標を取得
			int left = Marshal.ReadInt32(m.LParam, 0);
			int top = Marshal.ReadInt32(m.LParam, 4);
			int right = Marshal.ReadInt32(m.LParam, 8);
			int bottom = Marshal.ReadInt32(m.LParam, 12);

			// 幅・高さの取得
			int formWidth = right - left;
			int formHeight = bottom - top;

			// フレーム幅・高さの取得
			int frameWidth = form.Width - pecaPlayer.Width;
			int frameHeight = form.Height - pecaPlayer.Height;

			// ドラッグされている辺に応じて、新たなサイズを指定
			switch (m.WParam.ToInt32())
			{
				// 左
				case WMSZ_LEFT:
				// 右
				case WMSZ_RIGHT:
					{
						// 動画の幅に合わせて、フォームの高さを設定する
						int movieWidth = formWidth - frameWidth;
						int movieHeight = (int)(movieWidth / pecaPlayer.AspectRate);
						int newHeight = movieHeight + frameHeight;
						bottom = top + newHeight;
					}
					break;
				// 上
				case WMSZ_TOP:
				// 下
				case WMSZ_BOTTOM:
					{
						// 動画の高さに合わせて、フォームの幅を設定する
						int movieHeight = formHeight - frameHeight;
						int movieWidth = (int)(movieHeight * pecaPlayer.AspectRate);
						int newWidth = movieWidth + frameWidth;
						right = left + newWidth;
					}
					break;
			}

			// 新しいサイズで上書き
			Marshal.WriteInt32(m.LParam, 0, left);
			Marshal.WriteInt32(m.LParam, 4, top);
			Marshal.WriteInt32(m.LParam, 8, right);
			Marshal.WriteInt32(m.LParam, 12, bottom);
		}
	}
}
