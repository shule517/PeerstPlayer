using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
namespace HongliangSoft.Utilities.Gui
{

	///<summary>マウスをフックし、任意のメソッドを実行する。</summary>
	[DefaultEvent("MouseHooked")]
	public class MouseHook : System.ComponentModel.Component
	{
		public enum HookType : int
		{
			LocalHook = 7,
			GlobalHook = 14,
		};

		#region DLLImport

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int hookType, MouseHookDelegate hookDelegate, IntPtr hInstance, uint threadId);
		[DllImport("user32.dll", SetLastError = true)]
		private static extern int CallNextHookEx(IntPtr hook, int code, MouseMessage message, ref MouseState state);
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool UnhookWindowsHookEx(IntPtr hook);

		#endregion

		private delegate int MouseHookDelegate(int code, MouseMessage message, ref MouseState state);
		private IntPtr hook;
		private GCHandle hookDelegate;
		private static readonly object EventMouseHooked = new object();

		///<summary>マウスが入力されたときに発生する。</summary>
		public event MouseHookedEventHandler MouseHooked
		{
			add { base.Events.AddHandler(EventMouseHooked, value); }
			remove { base.Events.RemoveHandler(EventMouseHooked, value); }
		}

		///<summary>
		///インスタンスを作成する。
		///</summary>
		///<exception cref="Win32Exception">フックに失敗しました。原因の詳細はエラーコードを参照してください。</exception>
		public MouseHook(HookType hookType)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
				throw new PlatformNotSupportedException("Windows 98/Meではサポートされていません。");

			MouseHookDelegate handler = new MouseHookDelegate(CallNextHook);
			this.hookDelegate = GCHandle.Alloc(handler);
			IntPtr module = Marshal.GetHINSTANCE(typeof(MouseHook).Assembly.GetModules()[0]);
			this.hook = SetWindowsHookEx((int)hookType, handler, module, 0);
			if (hook == IntPtr.Zero)
				throw new Win32Exception(Marshal.GetLastWin32Error());
		}

		///<summary>
		///マウスが入力されたときに実行するデリゲートを指定してインスタンスを作成する。
		///</summary>
		///<param name="handler">マウスが入力されたときに実行するメソッドを表すデリゲート。</param>
		public MouseHook(HookType hookType, MouseHookedEventHandler handler)
			: this(hookType)
		{
			this.MouseHooked += handler;
		}

		///<summary>
		///MouseHookedイベントを発生させる。
		///</summary>
		///<param name="e">イベントのデータ。</param>
		protected virtual void OnMouseHooked(MouseHookedEventArgs e)
		{
			MouseHookedEventHandler handler = base.Events[EventMouseHooked] as MouseHookedEventHandler;
			if (handler != null)
				handler(this, e);
		}

		private int CallNextHook(int code, MouseMessage message, ref MouseState state)
		{
			if (code >= 0)
			{
				MouseHookedEventArgs e = new MouseHookedEventArgs(message, ref state);
				this.OnMouseHooked(e);
				if (e.Cancel)
					return -1;
			}
			return CallNextHookEx(hook, code, message, ref state);
		}

		///<summary>
		///使用されているアンマネージリソースを解放し、オプションでマネージリソースも解放する。
		///</summary>
		///<param name="disposing">マネージリソースも解放する場合はtrue。</param>
		protected override void Dispose(bool disposing)
		{
			if (this.hookDelegate.IsAllocated)
			{
				UnhookWindowsHookEx(this.hook);
				this.hookDelegate.Free();
				if (disposing)
				{
					hook = IntPtr.Zero;
				}
			}
			base.Dispose(disposing);
		}
	}

	///<summary>マウスが入力されたときに実行されるメソッドを表すイベントハンドラ。</summary>
	public delegate void MouseHookedEventHandler(object sender, MouseHookedEventArgs e);

	///<summary>MouseHookedイベントのデータを提供する。</summary>
	public class MouseHookedEventArgs : CancelEventArgs {

		///<summary>
		///新しいインスタンスを作成する。
		///</summary>
		///<param name="message">マウス操作の種類を表すMouseMessage値の一つ。</param>
		///<param name="state">マウスの状態を表すMouseState構造体。</param>
		internal MouseHookedEventArgs(MouseMessage message, ref MouseState state) {
			this.message = message;
			this.state = state;
		}
		private MouseMessage message;
		private MouseState state;

		///<summary>マウス操作の種類を表すMouseMessage値。</summary>
		public MouseMessage Message {get {return message;}}

		///<summary>スクリーン座標における現在のマウスカーソルの位置。</summary>
		public Point Point {get {return state.Point;}}

		///<summary>ホイールの情報を表すWheelData構造体。</summary>
		public WheelData WheelData {get {return state.WheelData;}}

		///<summary>XButtonの情報を表すXButtonData構造体。</summary>
		public XButtonData XButtonData {get {return state.XButtonData;}}
	}

	///<summary>マウス操作の種類を表す。</summary>
	public enum MouseMessage {
		///<summary>マウスカーソルが移動した。</summary>
		Move         = 0x200,
		///<summary>左ボタンが押された。</summary>
		LDown        = 0x201,
		///<summary>左ボタンが解放された。</summary>
		LUp          = 0x202,
		///<summary>左ダブルクリックされた。</summary>
		LDblClk      = 0x203,
		///<summary>右ボタンが押された。</summary>
		RDown        = 0x204,
		///<summary>左ボタンが解放された。</summary>
		RUp          = 0x205,
		///<summary>右ダブルクリックされた。</summary>
		RDblClk      = 0x206,
		///<summary>中ボタンが押された。</summary>
		MDown        = 0x207,
		///<summary>中ボタンが解放された。</summary>
		MUp          = 0x208,
		///<summary>ホイールが回転した。</summary>
		Wheel        = 0x20A,
		///<summary>Xボタンが押された。</summary>
		XDown        = 0x20B,
		///<summary>Xボタンが解放された。</summary>
		XUp          = 0x20C,
	}

	///<summary>マウスの状態を表す。</summary>
	[StructLayout(LayoutKind.Explicit)] internal struct MouseState {

		///<summary>スクリーン座標によるマウスカーソルの現在位置。</summary>
		[FieldOffset(0)]
		public Point Point;

		///<summary>messageがMouseMessage.Wheelの時にその詳細データを持つ。</summary>
		[FieldOffset(8)]
		public WheelData WheelData;

		///<summary>messageがMouseMessage.XDown/MouseMessage.XUpの時にその詳細データを持つ。</summary>
		[FieldOffset(8)]
		public XButtonData XButtonData;

		///<summary>マウスのイベントインジェクト。</summary>
		[FieldOffset(12)]
		public MouseStateFlag Flag;

		///<summary>メッセージが送られたときの時間</summary>
		[FieldOffset(16)]
		public int Time;

		///<summary>メッセージに関連づけられた拡張情報</summary>
		[FieldOffset(20)]
		public IntPtr ExtraInfo;
	}

	///<summary>マウスホイールの状態の詳細を表す。</summary>
	public struct WheelData {

		///<summary>ビットデータ。</summary>
		public int State;

		///<summary>ホイールの回転一刻みを表す。</summary>
		public const int OneWheel = 120;

		///<summary>ホイールの回転量を表す。クリックされたときは-1。</summary>
		public int WheelDelta {
			get {
				int delta = State >> 16;
				return (delta < 0) ? -delta : delta;
			}
		}

		///<summary>ホイールが一刻み分動かされたかどうかを表す。</summary>
		public bool IsOneWheel {get {return (State >> 16) == OneWheel;}}

		///<summary>ホイールの回転方向を表す。</summary>
		public WheelDirection Direction {
			get {
				int delta = State >> 16;
				if (delta == 0) return WheelDirection.None;
				return (delta < 0) ? WheelDirection.Backward : WheelDirection.Forward;
			}
		}
	}

	///<summary>ホイールの回転方向を表す。</summary>
	public enum WheelDirection {
		///<summary>回転していない。</summary>
		None     =  0,
		///<summary>ユーザから離れる方向へ回転した。</summary>
		Forward  =  1,
		///<summary>ユーザに近づく方向へ回転した。</summary>
		Backward = -1,
	}

	///<summary>Xボタンの状態の詳細を表す。</summary>
	public struct XButtonData {
		///<summary>ビットデータ。</summary>
		public int State;

		///<summary>操作されたボタンを示す。</summary>
		public int ControlledButton {get {return State >> 16;}}

		///<summary>Xボタン1が押されたかどうかを示す。</summary>
		public bool IsXButton1 {get {return (State >> 16) == 1;}}

		///<summary>Xボタン2が押されたかどうかを示す。</summary>
		public bool IsXButton2 {get {return (State >> 16) == 2;}}
	}

	///<summary>マウスの状態を補足する。</summary>
	internal struct MouseStateFlag {

		///<summary>ビットデータ。</summary>
		public int Flag;

		///<summary>イベントがインジェクトされたかどうかを表す。</summary>
		public bool IsInjected {
			get {return (Flag & 1) != 0;}
			set {Flag = value ? (Flag | 1) : (Flag & ~1);}
		}
	}
}
