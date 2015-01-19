using System;
using System.Runtime.InteropServices;
using System.Text;

namespace LibVlcWrapper
{
	internal static class Win32API
	{
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
}
