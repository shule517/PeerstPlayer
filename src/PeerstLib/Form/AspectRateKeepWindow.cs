using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using PeerstLib.Control;

namespace PeerstLib.Form
{
	// アスペクト比維持
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

		// コンストラクタ
		public AspectRateKeepWindow(IntPtr handle)
		{
			// サブクラスウィンドウの設定
			AssignHandle(handle);
		}

		// ウィンドウプロシージャ
		protected override void WndProc(ref Message m)
		{
			switch ((WindowMessage)m.Msg)
			{
				case WindowMessage.WM_SIZING:
					KeepAspectRate(m);
					break;

				default:
					break;
			}

			base.WndProc(ref m);
		}

		private void KeepAspectRate(Message m)
		{
			// 各辺の座標を取得
			int left = Marshal.ReadInt32(m.LParam, 0);
			int top = Marshal.ReadInt32(m.LParam, 4);
			int right = Marshal.ReadInt32(m.LParam, 8);
			int bottom = Marshal.ReadInt32(m.LParam, 12);

			int width = right - left;
			int height = bottom - top;

			//Size dif = this.pa - panelWMP.Size;

			// ドラッグされている辺に応じて、新たなサイズを指定
			switch (m.WParam.ToInt32())
			{
				// 左
				case WMSZ_LEFT:
				// 右
				case WMSZ_RIGHT:
					{
						bottom = top + (int)(width * (800 / 600));
						/*
						int panelWidth = width;
						B = T;// +(int)(panelWidth * wmp.AspectRate) + dif.Height;
						*/
					}
					break;
				// 上
				case WMSZ_TOP:
				// 下
				case WMSZ_BOTTOM:
					{
						right = left + (int)(height * (800 / 600));
						/*
						int panelHeight = height;// -(panelStatusLabel.Visible ? panelStatusLabel.Height : 0) - (panelResBox.Visible ? panelResBox.Height : 0) - (Size - ClientSize).Height;
						R = (int)(L * 0.75);// +(int)(panelHeight * (1 / wmp.AspectRate)) + dif.Width;
						*/
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
