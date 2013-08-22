using System;
using System.Runtime.InteropServices;

namespace PeerstLib.Controls
{
	//-------------------------------------------------------------
	// 概要：Win32APIクラス
	// 詳細：Win32APIのimport
	//-------------------------------------------------------------
	public class Win32API
	{
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		[DllImport("user32.dll", EntryPoint = "SetWindowText")]
		public static extern void SetWindowText(IntPtr hWnd, String text);

		/// <summary>
		/// 座標を含むウインドウのハンドルを取得
		/// </summary>
		/// <param name="Point">調査する座標</param>
		/// <returns>ポイントにウインドウがなければNULL</returns>
		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(POINT Point);

		/// <summary>
		/// 指定したハンドルの祖先のハンドルを取得
		/// </summary>
		/// <param name="hwnd">ハンドル</param>
		/// <param name="gaFlags">フラグ</param>
		/// <returns>祖先のハンドル</returns>
		[DllImport("user32.dll")]
		public static extern IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);

		// フラグ
		public const uint GA_ROOT = 2;

		/// <summary>
		/// ハンドルからウインドウの位置を取得
		/// </summary>
		/// <param name="hWnd">ウインドウのハンドル</param>
		/// <param name="lpRect">ウインドウの座標</param>
		/// <returns>成功すればtrue</returns>
		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
	}

	// ウィンドウメッセージ
	public enum WindowMessage : int
	{
		WM_NCLBUTTONDOWN = 0x00A1,
		WM_LBUTTONDBLCLK = 0x0203,
		WM_RBUTTONDOWN = 0x0204,
		WM_NCHITTEST = 0x0084,
		WM_LBUTTONUP = 0x0202,
		WM_MOUSEHOVER = 0x02A1,
		WM_SIZE = 0x0005,
		WM_SIZING = 0x214,
		WM_MOVE = 0x0003,
		WM_MOVING = 0x0216,
		WM_MOUSEMOVE = 0x200,
		WM_LBUTTONDOWN = 0x201,
		WM_MOUSEWHEEL = 0x020A,
		WM_CONTEXTMENU = 0x007B,
		WM_KEYDOWN = 0x0100,
		WM_SETFOCUS = 0x0007,
	}

	// 当たり判定
	enum HitTest : int
	{
		Caption = 2,		// タイトルバー
		Top = 12,			// 可変枠の上辺境界線上にある
		Bottom = 15,		// 可変枠の下辺境界線上にある
		Left = 10,			// 可変枠の左辺境界線上にある
		Right = 11,			// 可変枠の右辺境界線上にある
		TopLeft = 13,		// 可変枠の左上隅にある
		TopRight = 14,		// 可変枠の右上隅にある
		BottomLeft = 16,	// 可変枠の左下隅にある
		BottomRight = 17,	// 可変枠の右下隅にある
	}

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
}
