using System;
using System.Runtime.InteropServices;
using System.Text;

namespace LibVlcWrapper
{
	internal static class Win32API
	{
		[DllImport("user32.dll")]
		public static extern IntPtr PostMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);


		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LoadLibrary(string lpFileName);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		[DllImport("user32.dll")]
		public static extern IntPtr SetFocus(IntPtr hWnd);

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
		/// 指定された親ウィンドウに属する子ウィンドウを列挙します。
		/// </summary>
		/// <param name="hWndParent">親ウィンドウのハンドル</param>
		/// <param name="lpEnumFunc">デリゲート</param>
		/// <param name="lParam">デリゲートに渡す値</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool EnumChildWindows(IntPtr hWndParent,
			[MarshalAs(UnmanagedType.FunctionPtr)] EnumWindowsProcDelegate lpEnumFunc, IntPtr lParam);

		/// <summary>
		/// 指定されたウィンドウが属するクラスの名前を取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドル</param>
		/// <param name="lpClassName">クラス名</param>
		/// <param name="nMaxCount">クラス名バッファのサイズ</param>
		/// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

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

		/// <summary>
		/// 指定されたウィンドウの位置とサイズを変更します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドル</param>
		/// <param name="X">横方向の位置</param>
		/// <param name="Y">縦方向の位置</param>
		/// <param name="nWidth">幅</param>
		/// <param name="nHeight">高さ</param>
		/// <param name="bRepaint">再描画オプション</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		/// <summary>
		/// 画面上にある指定された点を、スクリーン座標からクライアント座標へ変換します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドルを指定します。このウィンドウのクライアント領域を利用して変換を行います。</param>
		/// <param name="lpPoint">変換対象のスクリーン座標を保持している、1 個の 構造体へのポインタを指定します。関数から制御が返り、関数が成功すると、この構造体にクライアント座標が格納されます。</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetCursor(IntPtr hCursor);

		/// <summary>
		/// 指定されたウィンドウに関する情報を取得します
		/// </summary>
		/// <param name="hwnd">ウィンドウのハンドル</param>
		/// <param name="nIndex">取得する値のオフセット</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetWindowLong(IntPtr hwnd, GWLIndexes nIndex);

		/// <summary>
		/// 指定されたウィンドウの属性を変更します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドル</param>
		/// <param name="nIndex">設定する値のオフセット</param>
		/// <param name="dwNewLong">新しい値</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SetWindowLong(IntPtr hWnd, GWLIndexes nIndex, int dwNewLong);

		/// <summary>
		/// 指定されたウィンドウが最大化されているかどうかを調べます。
		/// </summary>
		/// <param name="hWnd">調査するウィンドウのハンドルを指定します。</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool IsZoomed(IntPtr hWnd);

		/// <summary>
		/// 指定されたウィンドウの表示状態を設定します。
		/// </summary>
		[DllImport("user32.dll")]
		/// <param name="hWnd">ウィンドウのハンドル</param>
		/// <param name="nCmdSHow">表示状態</param>
		/// <returns></returns>
		public static extern bool ShowWindow(IntPtr hWnd, ShowCmd nCmdSHow);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, char bAlpha, uint dwFlags);

		/// <summary>
		/// 指定されたウィンドウの表示状態、および通常表示のとき、最小化されたとき、最大化されたときの位置を返します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドル</param>
		/// <param name="lpwndpl">位置データ</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);
	}

	/// <summary>EnumChildWindowsで使用するデリゲート</summary>
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate bool EnumWindowsProcDelegate(IntPtr handle, IntPtr lParam);

	/// <summary>SetWindowsHookExで使用するデリゲート</summary>
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate IntPtr HookProcedureDelegate(int nCode, IntPtr wParam, IntPtr lParam);

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

	enum GWLIndexes
	{
		GWL_WNDPROC = -4,
		GWL_HINSTANCE = -6,
		GWL_HWNDPARENT = -8,
		GWL_ID = -12,
		GWL_STYLE = -16,
		GWL_EXSTYLE = -20,
		GWL_USERDATA = -21,
	}

	[Flags]
	enum ExStyle : int
	{
		WS_EX_LEFT = 0x00000000,
		WS_EX_LTRREADING = 0x00000000,
		WS_EX_RIGHTSCROLLBAR = 0x00000000,
		WS_EX_DLGMODALFRAME = 0x00000001,
		WS_EX_NOPARENTNOTIFY = 0x00000004,
		WS_EX_TOPMOST = 0x00000008,
		WS_EX_ACCEPTFILES = 0x00000010,
		WS_EX_TRANSPARENT = 0x00000020,
		WS_EX_MDICHILD = 0x00000040,
		WS_EX_TOOLWINDOW = 0x00000080,
		WS_EX_WINDOWEDGE = 0x00000100,
		WS_EX_CLIENTEDGE = 0x00000200,
		WS_EX_CONTEXTHELP = 0x00000400,
		WS_EX_RIGHT = 0x00001000,
		WS_EX_RTLREADING = 0x00002000,
		WS_EX_LEFTSCROLLBAR = 0x00004000,
		WS_EX_CONTROLPARENT = 0x00010000,
		WS_EX_STATICEDGE = 0x00020000,
		WS_EX_APPWINDOW = 0x00040000,
		WS_EX_LAYERED = 0x00080000,
		WS_EX_NOINHERITLAYOUT = 0x00100000,
		WS_EX_NOREDIRECTIONBITMAP = 0x00200000,
		WS_EX_LAYOUTRTL = 0x00400000,
		WS_EX_COMPOSITED = 0x02000000,
		WS_EX_NOACTIVATE = 0x08000000,
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
	public class MOUSEHOOKSTRUCT
	{
		public POINT pt;
		public IntPtr hwnd;
		public uint wHitCodeTest;
		public IntPtr dwExtraInfo;
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
}
