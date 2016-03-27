using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PeerstLib.Forms;

namespace PeerstLib.Controls.Glow
{
	public class GlowForm : Form
	{
		readonly GlowParts top, bottom, left, right;

		public bool FrameInvisible
		{
			get; set;
		}

		public GlowForm()
		{
			if (IsWindowsTen())
			{
				top = new GlowTop(this);
				bottom = new GlowBottom(this);
				left = new GlowLeft(this);
				right = new GlowRight(this);
			}
		}

		public void ShowGlow()
		{
			if (IsWindowsTen())
			{
				top.Show();
				bottom.Show();
				left.Show();
				right.Show();
			}
		}

		public void HideGlow()
		{
			if (IsWindowsTen())
			{
				top.Hide();
				bottom.Hide();
				left.Hide();
				right.Hide();
			}
		}

		public bool GlowVisible()
		{
			if (IsWindowsTen())
			{
				return top.Visible;
			}
			{
				return false;
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			// フォーム非表示にした場合は、Glowも消す
			base.OnVisibleChanged(e);
			if (!Visible)
			{
				HideGlow();
			}
		}

		public bool IsWindowsTen()
		{
			return Environment.OSVersion.Version.Major >= 10;
		}

		protected override void WndProc(ref Message m)
		{
			if (!IsWindowsTen() && !FrameInvisible)
			{
				base.WndProc(ref m);
				return;
			}

			switch ((WindowsMessage)m.Msg)
			{
			case WindowsMessage.WM_NCPAINT:
				// Win10の時に非クライアント領域の描画を行わない
				if (IsWindowsTen())
				{
					Refresh();
					return;
				}
				break;
			case WindowsMessage.WM_NCACTIVATE:
				// モードレスダイアログがアクティブになる時はメッセージを処理する
				if (m.LParam != IntPtr.Zero)
				{
					base.WndProc(ref m);
					Refresh();
					return;
				}

				if (m.WParam != IntPtr.Zero && m.LParam == IntPtr.Zero)
				{
					OnActivated(new EventArgs());

					return;
				}
				OnDeactivate(new EventArgs());

				// 書き込み入力欄のキャレットが他のウィンドウがアクティブになっても表示される問題を処理する
				if (ActiveControl != null && ActiveControl.ToString() == "PeerstPlayer.Controls.StatusBar.StatusBarControl")
				{
					ActiveControl = null;
				}

				return;
            case WindowsMessage.WM_NCCALCSIZE:
				// 最大化の時は普通に処理する
				if (Win32API.IsZoomed(Handle))
				{
					break;
				}
				// 非クライアント領域を無しにする
				return;
			case WindowsMessage.WM_WINDOWPOSCHANGED:
				DefWndProc(ref m);
				UpdateBounds();
				return;
			}
			base.WndProc(ref m);
		}

	}
}
