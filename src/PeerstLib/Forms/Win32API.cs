using System;
using System.Runtime.InteropServices;
using System.Text;

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

		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();
	
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


		/// <summary>
		/// 指定されたウィンドウの表示状態を設定し、そのウィンドウの通常表示のとき、最小化されたとき、および最大化されたときの位置を設定します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドル</param>
		/// <param name="lpwndpl">位置データ</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		/// <summary>
		/// 指定されたウィンドウの表示状態、および通常表示のとき、最小化されたとき、最大化されたときの位置を返します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドル</param>
		/// <param name="lpwndpl">位置データ</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);

	}

	// ウィンドウ枠の当たり判定
	public enum HitArea
	{
		HTNONE = 0,
		HTCLIENT = 1,
		HTCAPTION = 2,
		HTLEFT = 10,
		HTRIGHT = 11,
		HTTOP = 12,
		HTTOPLEFT = 13,
		HTTOPRIGHT = 14,
		HTBOTTOM = 15,
		HTBOTTOMLEFT = 16,
		HTBOTTOMRIGHT = 17,
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

	[StructLayout(LayoutKind.Sequential)]
	public struct WINDOWPLACEMENT
	{
		public int length;
		public int flags;
		public ShowCmd showCmd;
		public POINT minPosition;
		public POINT maxPosition;
		public RECT normalPosition;
	}

	public enum ShowCmd
	{
		HIDE = 0,
		SHOWNORMAL = 1,
		SHOWMINIMIZED = 2,
		SHOWMAXIMIZED = 3,
		SHOWNOACTIVATE = 4,
		SHOW = 5,
		MINIMIZE = 6,
		SHOWMINNOACTIVE = 7,
		SHOWNA = 8,
		RESTORE = 9,
		SHOWDEFAULT = 10,
	}
}
