using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstLib.Form
{
	class MouseCapture
	{
		 #region P/Invoke
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct POINT {
        public int x;
        public int y;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT {
        public POINT pt;
        public int mouseData;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
    delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, ref MSLLHOOKSTRUCT lParam);
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    static extern IntPtr GetModuleHandle(string lpModuleName);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, int dwThreadId);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, ref MSLLHOOKSTRUCT lParam);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool UnhookWindowsHookEx(IntPtr hHook);

    const int WH_MOUSE_LL = 14;
    const int HC_ACTION = 0;
    const int WM_LBUTTONDOWN = 0x201;
    const int WM_LBUTTONUP = 0x202;
    //const int WM_LBUTTONDBLCLK = 0x0203;
    const int WM_MBUTTONDOWN = 0x207;
    const int WM_MBUTTONUP = 0x208;
    //const int WM_MBUTTONDBLCLK = 0x0209;
    const int WM_RBUTTONDOWN = 0x205;
    const int WM_RBUTTONUP = 0x206;
    //const int WM_RBUTTONDBLCLK = 0x204;

    const int WM_MOUSEMOVE = 0x200;
    const int WM_MOUSEWHEEL = 0x20A;

    const int WM_XBUTTONDOWN = 0x20B;
    const int WM_XBUTTONUP = 0x20C;
    //const int WM_XBUTTONDBLCLK = 0x20D;
    //const int WM_NCXBUTTONDOWN = 0x0AB;
    //const int WM_NCXBUTTONUP = 0x00AC;
    //const int WM_NCXBUTTONDBLCLK = 0x00AD;

    const int WHEEL_DELTA = 120;

    const int XBUTTON1 = 0x1;
    const int XBUTTON2 = 0x2;

    #endregion

    public enum MouseButtons {
        None,
        Left,
        Right,
        Middle,
        XButton1,
        XButton2
    }

    public sealed class MouseCaptureEventArgs : System.EventArgs {
        private bool m_cancel;
        private int m_nativeWParam;
        private MSLLHOOKSTRUCT m_nativeLParam;
        private bool m_isValueUpdate;

        private MouseButtons m_button;
        private int m_x;
        private int m_y;
        private int m_delta;
        private int m_time;

        internal MouseCaptureEventArgs(int wParam, MSLLHOOKSTRUCT lParam) {
            this.m_cancel = false;
            this.m_isValueUpdate = false;
            this.m_nativeWParam = wParam;
            this.m_nativeLParam = lParam;

            this.m_button = MouseButtons.None;

            switch(wParam) {
            case WM_LBUTTONDOWN:
            case WM_LBUTTONUP:
                this.m_button = MouseButtons.Left;
                break;
            case WM_MBUTTONDOWN:
            case WM_MBUTTONUP:
                this.m_button = MouseButtons.Middle;
                break;
            case WM_RBUTTONDOWN:
            case WM_RBUTTONUP:
                this.m_button = MouseButtons.Right;
                break;

            case WM_XBUTTONDOWN:
            case WM_XBUTTONUP:
                switch(lParam.mouseData >> 16) {
                case XBUTTON1:
                    this.m_button = MouseButtons.XButton1;
                    break;
                case XBUTTON2:
                    this.m_button = MouseButtons.XButton2;
                     break;
                }
                break;

            default:
                break;
            }
            this.m_x = lParam.pt.x;
            this.m_y = lParam.pt.y;
            this.m_delta = (wParam == WM_MOUSEWHEEL)
                ? (int)((short)(lParam.mouseData >> 16)) : 0;
            this.m_time = lParam.time;
        }

        public MouseButtons Button { get { return this.m_button; } }
        public int X { get { return this.m_x; } }
        public int Y { get { return this.m_y; } }
        public int Delta { get { return this.m_delta; } }
        public int Time { get { return this.m_time; } }

        public bool Cancel {
            set { this.m_cancel = value; }
            get { return this.m_cancel; }
        }
        public int NativeWParam {
            set {
                this.m_nativeWParam = value;
                this.m_isValueUpdate = true;
            }
            get { return this.m_nativeWParam; }
        }
        public MSLLHOOKSTRUCT NativeLParam {
            set {
                this.m_nativeLParam = value;
                this.m_isValueUpdate = true;
            }
            get { return this.m_nativeLParam; }
        }
        internal bool IsValueUpdate { get { return this.m_isValueUpdate; } }
    }

    private static IntPtr s_hook;
    static LowLevelMouseProc s_proc;
    public static event EventHandler<MouseCaptureEventArgs> MouseDown;
    public static event EventHandler<MouseCaptureEventArgs> MouseUp;
    public static event EventHandler<MouseCaptureEventArgs> MouseMove;
    public static event EventHandler<MouseCaptureEventArgs> MouseWheel;
		/*
    static GlobalMouseCapture() {
        s_hook = SetWindowsHookEx(WH_MOUSE_LL,
            s_proc = new LowLevelMouseProc(HookProc),
            System.Runtime.InteropServices.Marshal.GetHINSTANCE(typeof(GlobalMouseCapture).Module),
            //GetModuleHandle(null),
            0);
        AppDomain.CurrentDomain.DomainUnload += delegate
        {
            if(s_hook != IntPtr.Zero)
                UnhookWindowsHookEx(s_hook);
        };
    }
		 */

    static IntPtr HookProc(int nCode, IntPtr wParam, ref MSLLHOOKSTRUCT lParam) {
        bool cancel = false;
        if(nCode == HC_ACTION) {
            MouseCaptureEventArgs ev = new MouseCaptureEventArgs((int)wParam, lParam);
            switch(wParam.ToInt32()) {
            case WM_LBUTTONDOWN:
            case WM_MBUTTONDOWN:
            case WM_RBUTTONDOWN:
            case WM_XBUTTONDOWN:
                CallEvent(MouseDown, ev);
                break;

            case WM_LBUTTONUP:
            case WM_MBUTTONUP:
            case WM_RBUTTONUP:
            case WM_XBUTTONUP:
                CallEvent(MouseUp, ev);
                break;

            case WM_MOUSEMOVE:
                CallEvent(MouseMove, ev);
                break;
            case WM_MOUSEWHEEL:
                CallEvent(MouseWheel, ev);
                break;
            }
            if(ev.IsValueUpdate) {
                wParam = (IntPtr)ev.NativeWParam;
                lParam = ev.NativeLParam;
            }
            cancel = ev.Cancel;
        }
        return cancel ? (IntPtr)1 : CallNextHookEx(s_hook, nCode, wParam, ref lParam);
    }

    public static bool IsCapture { get { return s_hook != IntPtr.Zero; } }

    private static void CallEvent(EventHandler<MouseCaptureEventArgs> eh, MouseCaptureEventArgs ev) {
        if(eh != null)
            eh(null, ev);
    }
	}
}
