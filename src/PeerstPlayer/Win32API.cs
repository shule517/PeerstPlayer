using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PeerstPlayer
{
	public class Win32API
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int x;
			public int y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern int MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, int bRepaint);

		/// <summary>
		/// 座標を含むウインドウのハンドルを取得
		/// </summary>
		/// <param name="Point">調査する座標</param>
		/// <returns>ポイントにウインドウがなければNULL</returns>
		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(POINT Point);

		/// <summary>
		/// ハンドルからウインドウの位置を取得
		/// </summary>
		/// <param name="hWnd">ウインドウのハンドル</param>
		/// <param name="lpRect">ウインドウの座標</param>
		/// <returns>成功すればtrue</returns>
		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		/// <summary>
		/// 指定したハンドルの祖先のハンドルを取得
		/// </summary>
		/// <param name="hwnd">ハンドル</param>
		/// <param name="gaFlags">フラグ</param>
		/// <returns>祖先のハンドル</returns>
		[DllImport("user32.dll")]
		public static extern IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);

		[DllImport("USER32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam); 

		public enum SWP : int
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

		public const UInt32 WS_CAPTION = (UInt32)0x00C00000;
		public const int GWL_STYLE = -16;

		[DllImport("user32.dll")]
		public static extern UInt32 GetWindowLong(IntPtr hWnd, int index);
		[DllImport("user32.dll")]
		public static extern UInt32 SetWindowLong(IntPtr hWnd, int index, UInt32 unValue);
		[DllImport("user32.dll")]
		public static extern UInt32 SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, SWP flags);

		public const uint GA_ROOT = 2;

		public const int WM_SIZE = 0x0005;
		public const int WM_SIZING = 0x214;
		public const int WM_MOVE = 0x0003;
		public const int WM_MOVING = 0x0216;

		public const int WM_MOUSEMOVE = 0x200;
		public const int WM_LBUTTONDOWN = 0x201;
		public const int WM_NCLBUTTONDOWN = 0x00A1;
		public const int HTCAPTION = 2;

		public const int WM_MOUSEWHEEL = 0x020A;

		public const int WM_CONTEXTMENU = 0x007B;

		public const int WMSZ_LEFT = 1;
		public const int WMSZ_RIGHT = 2;
		public const int WMSZ_TOP = 3;
		public const int WMSZ_TOPLEFT = 4;
		public const int WMSZ_TOPRIGHT = 5;
		public const int WMSZ_BOTTOM = 6;
		public const int WMSZ_BOTTOMLEFT = 7;
		public const int WMSZ_BOTTOMRIGHT = 8;

		public const int WM_KEYDOWN = 0x0100;

		[DllImport("user32.dll")]
		public static extern short GetAsyncKeyState(int vKey);

		[DllImport("user32.dll")]
		public static extern bool GetKeyboardState(byte[] lpKeyState);

		//[DllImport("user32.dll")]
		//public static extern short GetKeyState(VirtualKeyStates nVirtKey);

		public const int WM_SETFOCUS = 0x0007;


		public const int WM_LBUTTONDBLCLK = 0x0203;
		public const int WM_RBUTTONDOWN = 0x0201;
		public const int WM_NCHITTEST = 0x0084;
		public const int WM_LBUTTONUP = 0x0202;
		public const int WM_MOUSEHOVER = 0x02A1;

		public const int HTTOP = 12;			// 可変枠の上辺境界線上にある
		public const int HTBOTTOM = 15;			// 可変枠の下辺境界線上にある
		public const int HTLEFT = 10;			// 可変枠の左辺境界線上にある
		public const int HTRIGHT = 11;			// 可変枠の右辺境界線上にある
		public const int HTTOPLEFT = 13;		// 可変枠の左上隅にある
		public const int HTTOPRIGHT = 14;		// 可変枠の右上隅にある
		public const int HTBOTTOMLEFT = 16;		// 可変枠の左下隅にある
		public const int HTBOTTOMRIGHT = 17;	// 可変枠の右下隅にある
	}
}
