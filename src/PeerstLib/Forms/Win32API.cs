using System;
using System.Drawing;
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

		[DllImport("user32.dll")]
		public static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

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

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

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
		/// 指定されたウィンドウに関する情報を取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドル</param>
		/// <param name="nIndex">取得する値のオフセット</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern int GetWindowLong(IntPtr hWnd, GWL nIndex);

		/// <summary>
		/// 指定されたウィンドウの属性を変更します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドル</param>
		/// <param name="nIndex">設定する値のオフセット</param>
		/// <param name="dwNewLong">新しい値</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, int dwNewLong);

		/// <summary>
		/// 子ウィンドウ、ポップアップウィンドウ、またはトップレベルウィンドウのサイズ、位置、および Z オーダーを変更します
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドル</param>
		/// <param name="hWndInsertAfter">配置順序のハンドル</param>
		/// <param name="x">横方向の位置</param>
		/// <param name="y">縦方向の位置</param>
		/// <param name="cx">幅</param>
		/// <param name="cy">高さ</param>
		/// <param name="flags">ウィンドウ位置のオプション</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint flags);


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

		/// <summary>
		/// レイヤードウィンドウの位置、サイズ、形、内容、透明度を更新します。
		/// </summary>
		/// <param name="hWnd">レイヤードウィンドウのハンドル</param>
		/// <param name="hdcDst">画面のデバイスコンテキストのハンドル</param>
		/// <param name="pptDst">画面の新しい位置</param>
		/// <param name="psize">レイヤードウィンドウの新しいサイズ</param>
		/// <param name="hdcSrc">サーフェスのデバイスコンテキストのハンドル</param>
		/// <paran name="pptSrc">レイヤの位置</paran>
		/// <param name="crKey">カラーキー</param>
		/// <param name="pblend">ブレンド機能</param>
		/// <param name="dwFlags">フラグ</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern int UpdateLayeredWindow(IntPtr hWnd, IntPtr hdcDst,
			ref POINT pptDst, ref Size psize, IntPtr hdcSrc, ref POINT pptSrc,
			int crKey, ref BLENDFUNCTION pblend, int dwFlags);

		/// <summary>
		/// 指定されたウィンドウが最大化されているかどうかを調べます。
		/// </summary>
		/// <param name="hWnd">調査するウィンドウのハンドルを指定します。</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool IsZoomed(IntPtr hWnd);

		[DllImport("kernel32.dll")]
		public static extern int LCMapStringW(int Local, MapFlags dwMapFlags,
			[MarshalAs(UnmanagedType.LPWStr)]string lpSrcStr, int cchSrc,
			[MarshalAs(UnmanagedType.LPWStr)]string lpDestStr, int cchDest);

		/// <summary>
		/// 指定されたデバイスコンテキストで、指定された 1 個のオブジェクトを選択します。
		/// 新しいオブジェクトは、同じタイプの以前のオブジェクトを置き換えます。
		/// </summary>
		/// <param name="hdc">デバイスコンテキストのハンドル</param>
		/// <param name="hgdiobj">オブジェクトのハンドル</param>
		/// <returns></returns>
		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		/// <summary>
		/// ペン、ブラシ、フォント、ビットマップ、リージョン、パレットのいずれかの論理オブジェクトを削除し、そのオブジェクトに関連付けられていたすべてのシステムリソースを解放します。
		/// オブジェクトを削除した後は、指定されたハンドルは無効になります。
		/// </summary>
		/// <param name="hobject">グラフィックオブジェクトのハンドル</param>
		/// <returns></returns>
		[DllImport("gdi32.dll")]
		public static extern int DeleteObject(IntPtr hobject);

		[DllImport("dwmapi.dll")]
		public static extern void DwmGetColorizationColor(out int pcrColorization, out bool pfOpaqueBlend);

		/// <summary>
		/// 指定されたウィンドウを作成したスレッドのIDを取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドル</param>
		/// <param name="lpdwProcessId">プロセスID</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		/// <summary>
		/// フックプロシージャをフックチェーン内にインストールします。
		/// </summary>
		/// <param name="idHook">フックタイプ</param>
		/// <param name="lpfn">フックデリゲート</param>
		/// <param name="hMod">アプリケーションインスタンスのハンドル</param>
		/// <param name="dwThreadId">スレッドの識別子</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr SetWindowsHookEx(HookType idHook,
			[MarshalAs(UnmanagedType.FunctionPtr)] HookProcedureDelegate lpfn, IntPtr hMod, int dwThreadId);

		/// <summary>
		/// フックチェーン内にインストールされたフックプロシージャを削除します。
		/// </summary>
		/// <param name="idHook"></param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool UnhookWindowsHookEx(IntPtr idHook);

		/// <summary>
		/// 現在のフックチェーン内の次のフックプロシージャにフック情報を渡します。
		/// </summary>
		/// <param name="hhk">現在のフックのハンドル</param>
		/// <param name="nCode">フックプロシージャに渡すフックコード</param>
		/// <param name="wParam">フックプロシージャに渡す値</param>
		/// <param name="lParam">フックプロシージャに渡す値</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

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

	// フックタイプ
	public enum HookType
	{
		WH_JOURNALRECORD = 0,
		WH_JOURNALPLAYBACK = 1,
		WH_KEYBOARD = 2,
		WH_GETMESSAGE = 3,
		WH_CALLWNDPROC = 4,
		WH_CBT = 5,
		WH_SYSMSGFILTER = 6,
		WH_MOUSE = 7,
		WH_HARDWARE = 8,
		WH_DEBUG = 9,
		WH_SHELL = 10,
		WH_FOREGROUNDIDLE = 11,
		WH_CALLWNDPROCRET = 12,
		WH_KEYBOARD_LL = 13,
		WH_MOUSE_LL = 14,
	}

	/// <summary>EnumChildWindowsで使用するデリゲート</summary>
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate bool EnumWindowsProcDelegate(IntPtr handle, IntPtr lParam);

	/// <summary>SetWindowsHookExで使用するデリゲート</summary>
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate IntPtr HookProcedureDelegate(int nCode, IntPtr wParam, IntPtr lParam);


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

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct BLENDFUNCTION
	{
		public byte BlendOp;
		public byte BlendFlags;
		public byte SourceConstantAlpha;
		public byte AlphaFormat;
	}

	[StructLayout(LayoutKind.Sequential)]
	public class MOUSEHOOKSTRUCT
	{
		public POINT pt;
		public IntPtr hwnd;
		public uint wHitCodeTest;
		public IntPtr dwExtraInfo;
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

	[Flags]
	public enum SWP
	{
		NOSIZE = 0x01,
		NOMOVE = 0x02,
		NOZORDER = 0x04,
		NOREDRAW = 0x08,
		NOACTIVATE = 0x10,
		FRAMECHANGED = 0x20,
		SHOWWINDOW = 0x40,
		NOOWNERZORDER = 0x0200,
		NOSENDCHANGING = 0x0400,
		ASYNCWINDOWPOS = 0x4000,
	}

	public enum GWL
	{
		WNDPROC = -4,
		HINSTANCE = -6,
		HWNDPARENT = -8,
		ID = -12,
		STYLE = -16,
		EXSTYLE = -20,
		USERDATA = -21,
	}

	[Flags]
	public enum WSEX
	{
		DLGMODALFRAME = 0x00000001,
		NOPARENTNOTIFY = 0x00000004,
		TOPMOST = 0x00000008,
		ACCEPTFILES = 0x00000010,
		TRANSPARENT = 0x00000020,
		MDICHILD = 0x00000040,
		TOOLWINDOW = 0x00000080,
		WINDOWEDGE = 0x00000100,
		CLIENTEDGE = 0x00000200,
		CONTEXTHELP = 0x00000400,
		RIGHT = 0x00001000,
		LEFT = 0x00000000,
		RTLREADING = 0x00002000,
		LTRREADING = 0x00000000,
		LEFTSCROLLBAR = 0x00004000,
		RIGHTSCROLLBAR = 0x00000000,
		CONTROLPARENT = 0x00010000,
		STATICEDGE = 0x00020000,
		APPWINDOW = 0x00040000,
		OVERLAPPEDWINDOW = WINDOWEDGE | CLIENTEDGE,
		PALETTEWINDOW = WINDOWEDGE | TOOLWINDOW | TOPMOST,
		LAYERED = 0x00080000,
		NOINHERITLAYOUT = 0x00100000,
		NOREDIRECTIONBITMAP = 0x00200000,
		LAYOUTRTL = 0x00400000,
		COMPOSITED = 0x02000000,
		NOACTIVATE = 0x08000000,
	}
}
