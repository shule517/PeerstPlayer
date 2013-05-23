using PeerstLib.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer.Control
{
	// WMPウィンドウのサブクラス化
	class WmpNativeWindow : NativeWindow
	{
		// ダブルクリックイベント
		public event EventHandler DoubleClick = delegate { };

		// コンストラクタ
		public WmpNativeWindow(IntPtr handle)
		{
			// サブクラスウィンドウの設定
			AssignHandle(handle);
		}

		// ウィンドウプロシージャ
		protected override void WndProc(ref Message m)
		{
			switch ((WindowMessage)m.Msg)
			{
				case WindowMessage.WM_LBUTTONUP:
					DoubleClick(this, new EventArgs());
					break;

				default:
					break;
			}

			base.WndProc(ref m);
		}
	}
}
