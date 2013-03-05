using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Shule.Peerst.Form
{
	public class FormUtility
	{
		private const UInt32 WS_CAPTION = (UInt32)0x00C00000;
		private const int GWL_STYLE = -16;

		[DllImport("user32.dll")]
		private static extern UInt32 GetWindowLong(IntPtr hWnd, int index);
		[DllImport("user32.dll")]
		private static extern UInt32 SetWindowLong(IntPtr hWnd, int index, UInt32 unValue);
		[DllImport("user32.dll")]
		private static extern UInt32 SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, SWP flags);

		private enum SWP : int
		{
			NOSIZE = 0x0001,
			NOMOVE = 0x0002,
			NOZORDER = 0x0004,
			NOREDRAW = 0x0008,
			NOACTIVATE = 0x0010,
			FRAMECHANGED = 0x0020,
			SHOWWINDOW = 0x0040,
			HIDEWINDOW = 0x0080,
			NOCOPYBITS = 0x0100,
			NOOWNERZORDER = 0x0200,
			NOSENDCHANGING = 0x400
		}

		/// <summary>
		/// タイトルバーの表示切替
		/// </summary>
		/// <param name="handle"></param>
		/// <param name="visible"></param>
		static public void VisibleTitlebar(IntPtr handle, bool visible)
		{
			if (visible)
			{
				// タイトルバーを出す
				UInt32 style = GetWindowLong(handle, GWL_STYLE);	// 現在のスタイルを取得
				style = (style | WS_CAPTION);							// キャプションのスタイルを削除
				SetWindowLong(handle, GWL_STYLE, style);				// スタイルを反映
				SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, SWP.NOMOVE | SWP.NOSIZE | SWP.NOZORDER | SWP.FRAMECHANGED); // ウィンドウを再描画
			}
			else
			{
				// タイトルバーを消す
				UInt32 style = GetWindowLong(handle, GWL_STYLE);	// 現在のスタイルを取得
				style = (style & ~WS_CAPTION);							// キャプションのスタイルを削除
				SetWindowLong(handle, GWL_STYLE, style);				// スタイルを反映
				SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, SWP.NOMOVE | SWP.NOSIZE | SWP.NOZORDER | SWP.FRAMECHANGED); // ウィンドウを再描画
			}
		}
	}
}
