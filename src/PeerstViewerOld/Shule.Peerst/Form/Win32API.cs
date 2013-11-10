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

		[DllImport("user32.dll")]
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

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern short GetAsyncKeyState(int vKey);

		[DllImport("user32.dll")]
		public static extern bool GetKeyboardState(byte[] lpKeyState);

		public const uint GA_ROOT = 2;

		public const int WM_SIZE = 0x0005;
		public const int WM_SIZING = 0x214;
		public const int WM_MOVE = 0x0003;
		public const int WM_MOVING = 0x0216;
		public const int WM_MOUSEMOVE = 0x200;
		public const int WM_LBUTTONDOWN = 0x201;
		public const int WM_NCLBUTTONDOWN = 0x00A1;
		public const int WM_MOUSEWHEEL = 0x020A;
		public const int WM_CONTEXTMENU = 0x007B;
		public const int WM_KEYDOWN = 0x0100;
		public const int WM_SETFOCUS = 0x0007;

		public const int WMSZ_LEFT = 1;
		public const int WMSZ_RIGHT = 2;
		public const int WMSZ_TOP = 3;
		public const int WMSZ_TOPLEFT = 4;
		public const int WMSZ_TOPRIGHT = 5;
		public const int WMSZ_BOTTOM = 6;
		public const int WMSZ_BOTTOMLEFT = 7;
		public const int WMSZ_BOTTOMRIGHT = 8;

		public const int WM_LBUTTONDBLCLK = 0x0203;
		public const int WM_RBUTTONDOWN = 0x0204;
		public const int WM_NCHITTEST = 0x0084;
		public const int WM_LBUTTONUP = 0x0202;
		public const int WM_MOUSEHOVER = 0x02A1;

		public const int HTCAPTION = 2;
		public const int HTTOP = 12;			// 可変枠の上辺境界線上にある
		public const int HTBOTTOM = 15;			// 可変枠の下辺境界線上にある
		public const int HTLEFT = 10;			// 可変枠の左辺境界線上にある
		public const int HTRIGHT = 11;			// 可変枠の右辺境界線上にある
		public const int HTTOPLEFT = 13;		// 可変枠の左上隅にある
		public const int HTTOPRIGHT = 14;		// 可変枠の右上隅にある
		public const int HTBOTTOMLEFT = 16;		// 可変枠の左下隅にある
		public const int HTBOTTOMRIGHT = 17;	// 可変枠の右下隅にある

		#region タイトルバー用定義

		[DllImport("user32.dll")]
		public static extern UInt32 GetWindowLong(IntPtr hWnd, int index);
		[DllImport("user32.dll")]
		public static extern UInt32 SetWindowLong(IntPtr hWnd, int index, UInt32 unValue);
		[DllImport("user32.dll")]
		public static extern UInt32 SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, SWP flags);

		public const UInt32 WS_CAPTION = (UInt32)0x00C00000;
		public const int GWL_STYLE = -16;

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

		#endregion

		[DllImport("user32.dll")]
		public static extern int GetSystemMetrics(SystemMetric smIndex);
		public enum SystemMetric : int
		{
			SM_CXSCREEN = 0,
			SM_CYSCREEN = 1,
			SM_CYVSCROLL = 2,
			SM_CXVSCROLL = 3,
			SM_CYCAPTION = 4,
			SM_CXBORDER = 5,
			SM_CYBORDER = 6,
			SM_CXDLGFRAME = 7,
			SM_CYDLGFRAME = 8,
			SM_CYVTHUMB = 9,
			SM_CXHTHUMB = 10,
			SM_CXICON = 11,
			SM_CYICON = 12,
			SM_CXCURSOR = 13,
			SM_CYCURSOR = 14,
			SM_CYMENU = 15,
			SM_CXFULLSCREEN = 16,
			SM_CYFULLSCREEN = 17,
			SM_CYKANJIWINDOW = 18,
			SM_MOUSEWHEELPRESENT = 75,
			SM_CYHSCROLL = 20,
			SM_CXHSCROLL = 21,
			SM_DEBUG = 22,
			SM_SWAPBUTTON = 23,
			SM_RESERVED1 = 24,
			SM_RESERVED2 = 25,
			SM_RESERVED3 = 26,
			SM_RESERVED4 = 27,
			SM_CXMIN = 28,
			SM_CYMIN = 29,
			SM_CXSIZE = 30,
			SM_CYSIZE = 31,
			SM_CXFRAME = 32,
			SM_CYFRAME = 33,
			SM_CXMINTRACK = 34,
			SM_CYMINTRACK = 35,
			SM_CXDOUBLECLK = 36,
			SM_CYDOUBLECLK = 37,
			SM_CXICONSPACING = 38,
			SM_CYICONSPACING = 39,
			SM_MENUDROPALIGNMENT = 40,
			SM_PENWINDOWS = 41,
			SM_DBCSENABLED = 42,
			SM_CMOUSEBUTTONS = 43,
			SM_CXFIXEDFRAME = SM_CXDLGFRAME,
			SM_CYFIXEDFRAME = SM_CYDLGFRAME,
			SM_CXSIZEFRAME = SM_CXFRAME,
			SM_CYSIZEFRAME = SM_CYFRAME,
			SM_SECURE = 44,
			SM_CXEDGE = 45,
			SM_CYEDGE = 46,
			SM_CXMINSPACING = 47,
			SM_CYMINSPACING = 48,
			SM_CXSMICON = 49,
			SM_CYSMICON = 50,
			SM_CYSMCAPTION = 51,
			SM_CXSMSIZE = 52,
			SM_CYSMSIZE = 53,
			SM_CXMENUSIZE = 54,
			SM_CYMENUSIZE = 55,
			SM_ARRANGE = 56,
			SM_CXMINIMIZED = 57,
			SM_CYMINIMIZED = 58,
			SM_CXMAXTRACK = 59,
			SM_CYMAXTRACK = 60,
			SM_CXMAXIMIZED = 61,
			SM_CYMAXIMIZED = 62,
			SM_NETWORK = 63,
			SM_CLEANBOOT = 67,
			SM_CXDRAG = 68,
			SM_CYDRAG = 69,
			SM_SHOWSOUNDS = 70,
			SM_CXMENUCHECK = 71,
			SM_CYMENUCHECK = 72,
			SM_SLOWMACHINE = 73,
			SM_MIDEASTENABLED = 74,
			SM_MOUSEPRESENT = 19,
			SM_XVIRTUALSCREEN = 76,
			SM_YVIRTUALSCREEN = 77,
			SM_CXVIRTUALSCREEN = 78,
			SM_CYVIRTUALSCREEN = 79,
			SM_CMONITORS = 80,
			SM_SAMEDISPLAYFORMAT = 81,
			SM_IMMENABLED = 82,
			SM_CXFOCUSBORDER = 83,
			SM_CYFOCUSBORDER = 84,
			SM_TABLETPC = 86,
			SM_MEDIACENTER = 87,
			SM_CMETRICS_OTHER = 76,
			SM_CMETRICS_2000 = 83,
			SM_CMETRICS_NT = 88,
			SM_REMOTESESSION = 0x1000,
			SM_SHUTTINGDOWN = 0x2000,
			SM_REMOTECONTROL = 0x2001,
		}
	}
}
