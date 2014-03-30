using System;
using System.Windows.Forms;
using PeerstLib.Controls;
using AxWMPLib;
using PeerstPlayer.Forms.Player;

namespace PeerstPlayer.Controls.MoviePlayer
{
	//-------------------------------------------------------------
	// 概要：WMPウィンドウのサブクラス化
	//-------------------------------------------------------------
	class WmpNativeWindow : NativeWindow
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// WMP
		AxWindowsMediaPlayer wmp;

		// ダブルクリックイベント
		public event AxWMPLib._WMPOCXEvents_DoubleClickEventHandler DoubleClick = delegate { };

		// ウィンドウサイズ変更用の枠サイズ
		private const int frameSize = 15;

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		//-------------------------------------------------------------
		public WmpNativeWindow(AxWindowsMediaPlayer wmp)
		{
			this.wmp = wmp;

			// サブクラスウィンドウの設定
			AssignHandle(wmp.Handle);

			// 枠なし時のサイズ変更処理
			wmp.MouseMoveEvent += (sender, e) =>
			{
				// 枠なしのときだけ処理を実行する
				if (!PlayerSettings.FrameInvisible)
				{
					return;
				}

				HitArea area = GetHitArea(frameSize, e.fX, e.fY);
				if (area == HitArea.HTNONE)
				{
					return;
				}

				switch (area)
				{
					case HitArea.HTTOP:
					case HitArea.HTBOTTOM:
						Cursor.Current = Cursors.SizeNS;
						break;
					case HitArea.HTLEFT:
					case HitArea.HTRIGHT:
						Cursor.Current = Cursors.SizeWE;
						break;
					case HitArea.HTTOPLEFT:
					case HitArea.HTBOTTOMRIGHT:
						Cursor.Current = Cursors.SizeNWSE;
						break;
					case HitArea.HTTOPRIGHT:
					case HitArea.HTBOTTOMLEFT:
						Cursor.Current = Cursors.SizeNESW;
						break;
				}
			};

			// 枠なし時のサイズ変更処理
			wmp.MouseDownEvent += (sender, e) =>
			{
				// 枠なしのときだけ処理を実行する
				if (!PlayerSettings.FrameInvisible)
				{
					return;
				}

				HitArea area = GetHitArea(frameSize, e.fX, e.fY);
				if (area != HitArea.HTNONE)
				{
					Win32API.SendMessage(wmp.Parent.Parent.Handle, (int)WindowMessage.WM_NCLBUTTONDOWN, new IntPtr((int)area), new IntPtr(0));
				}
			};
		}

		/// <summary>
		/// マウスとウィンドウ枠の当たり判定
		/// </summary>
		private HitArea GetHitArea(int frameSize, int fX, int fY)
		{
			// 斜め判定（上
			if (fY <= frameSize)
			{
				// 左上
				if (fX <= frameSize)
				{
					return HitArea.HTTOPLEFT;
				}
				// 右上
				else if (fX > (wmp.Width - frameSize))
				{
					return HitArea.HTTOPRIGHT;
				}
			}
			// 斜め判定（下
			else if (fY >= (wmp.Height - frameSize))
			{
				// 左下
				if (fX <= frameSize)
				{
					return HitArea.HTBOTTOMLEFT;
				}
				// 右下
				else if (fX > (wmp.Width - frameSize))
				{
					return HitArea.HTBOTTOMRIGHT;
				}
			}

			// 上
			if (fY <= frameSize)
			{
				return HitArea.HTTOP;
			}
			// 下
			else if (fY >= (wmp.Height - frameSize))
			{
				return HitArea.HTBOTTOM;
			}

			// 左
			if (fX <= frameSize)
			{
				return HitArea.HTLEFT;
			}
			// 右
			else if (fX > (wmp.Width - frameSize))
			{
				return HitArea.HTRIGHT;
			}

			return HitArea.HTNONE;
		}

		//-------------------------------------------------------------
		// 概要：ウィンドウプロシージャ
		// 詳細：ダブルクリック押下のイベント通知
		//-------------------------------------------------------------
		protected override void WndProc(ref Message m)
		{
			switch ((WindowMessage)m.Msg)
			{
				case WindowMessage.WM_LBUTTONUP:
					// ウィンドウにフォーカスがない場合にダブルクリックイベントが走ってしまうためのガード
					if (wmp.Focused)
					{
						DoubleClick(this, new AxWMPLib._WMPOCXEvents_DoubleClickEvent((short)Keys.LButton, 0, (int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
					}
					break;

				case WindowMessage.WM_MOUSEMOVE:
					// マウスカーソルの更新
					Cursor.Current = Cursors.Arrow;
					break;

				default:
					break;
			}

			base.WndProc(ref m);
		}
	}
}
