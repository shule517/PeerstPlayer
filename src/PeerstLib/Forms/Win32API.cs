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

		[DllImport("kernel32.dll")]
		public static extern int LCMapStringW(int Local, MapFlags dwMapFlags,
			[MarshalAs(UnmanagedType.LPWStr)]string lpSrcStr, int cchSrc,
			[MarshalAs(UnmanagedType.LPWStr)]string lpDestStr, int cchDest);
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

	[Flags]
	public enum MapFlags : uint
	{
		NORM_IGNORECASE = 0x00000001,
		NORM_IGNORENONSPACE = 0x00000002,
		NORM_IGNORESYMBOLS = 0x00000004,
		LCMAP_LOWERCASE = 0x00000100,
		LCMAP_UPPERCASE = 0x00000200,
		LCMAP_SORTKEY = 0x00000400,
		LCMAP_BYTEREV = 0x00000800,
		SORT_STRINGSORT = 0x00001000,
		NORM_IGNOREKANATYPE = 0x00010000,
		NORM_IGNOREWIDTH = 0x00020000,
		LCMAP_HIRAGANA = 0x00100000,
		LCMAP_KATAKANA = 0x00200000,
		LCMAP_HALFWIDTH = 0x00400000,
		LCMAP_FULLWIDTH = 0x00800000,
		LCMAP_LINGUISTIC_CASING = 0x01000000,
		LCMAP_SIMPLIFIED_CHINESE = 0x02000000,
		LCMAP_TRADITIONAL_CHINESE = 0x04000000,
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
