using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Drawing;

namespace PeerstPlayer
{
	class WMPEx : AxWMPLib.AxWindowsMediaPlayer
	{
		#region イベント

		/// <summary>
		/// バッファになった
		/// </summary>
		public event EventHandler Buffer;
		protected virtual void OnBuffer(EventArgs e)
		{
			if (Buffer != null)
			{
				Buffer(this, e);
			}
		}

		/// <summary>
		/// 再生時間が変更された
		/// </summary>
		public event EventHandler DurationChange;
		protected virtual void OnDurationChange(EventArgs e)
		{
			if (DurationChange != null)
			{
				DurationChange(this, e);
			}
		}

		/// <summary>
		/// URLが変更された
		/// </summary>
		public event EventHandler URLChange;
		protected virtual void OnURLChange(EventArgs e)
		{
			if (URLChange != null)
			{
				URLChange(this, e);
			}
		}

		/// <summary>
		/// 動画が再生開始した
		/// </summary>
		public event EventHandler MovieStart;
		protected virtual void OnMovieStart(EventArgs e)
		{
			if (MovieStart != null)
			{
				MovieStart(this, e);
			}
		}

		/// <summary>
		/// ジェスチャー
		/// </summary>
		public event EventHandlerString Gesture;
		protected virtual void OnGesture(string gesture)
		{
			if (Gesture != null)
			{
				Gesture(this, gesture);
			}
		}

		/// <summary>
		/// ボリュームが変更された
		/// </summary>
		public event EventHandler VolumeChange;
		protected virtual void OnVolumeChange(EventArgs e)
		{
			if (VolumeChange != null)
			{
				VolumeChange(this, e);
			}
		}

		#endregion

		const int frameSize = 10;		// ドラッグ枠の大きさ
		const int frameSkewSize = 20;	// ドラッグ斜め判定

		/// <summary>
		/// Moveした？MouseUp２回起動防ぎ
		/// </summary>
		bool IsMouseMoving = false;

		/// <summary>
		/// クリックポイント
		/// </summary>
		Point clickPoint = new Point();
		public Point ClickPoint
		{
			get
			{
				return clickPoint;
			}
		}

		/// <summary>
		/// URLデータ
		/// </summary>
		public URLData URLData = new URLData();

		/// <summary>
		/// 動画が再生されているか
		/// </summary>
		bool IsMoviePlaying = false;

		/// <summary>
		/// 動画が再生された瞬間
		/// </summary>
		bool IsMovieStart = false;

		/// <summary>
		/// マウスジェスチャー
		/// </summary>
		public MouseGesture mouseGesture = new MouseGesture();

		/// <summary>
		/// Form
		/// </summary>
		MainForm form;

		/// <summary>
		/// 1秒タイマー
		/// </summary>
		Timer timer;

		/// <summary>
		/// ショートカット用タイマー
		/// </summary>
		Timer timerShortCut;

		/// <summary>
		/// 前回の受信パケット数
		/// </summary>
		int beforeReceivedPackets = 0;

		/// <summary>
		/// リトライ限界
		/// </summary>
		const int RetryMax = 10;

		/// <summary>
		/// リトライした回数
		/// </summary>
		int retryNum = 0;

		/// <summary>
		/// リトライした回数
		/// </summary>
		public int RetryNum
		{
			get
			{
				return retryNum;
			}
		}

		/// <summary>
		/// リトライまでのカウント
		/// </summary>
		int retryCount = RetryTime;

		/// <summary>
		/// リトライ時間
		/// </summary>
		const int RetryTime = 15;

		/// <summary>
		/// パケットを受信していない時間
		/// </summary>
		int receivedPacketsTime = 0;
		public int ReceivedPacketsTime
		{
			get
			{
				return receivedPacketsTime;
			}
		}

		#region マウスジェスチャー

		/// <summary>
		/// マウスジェスチャー
		/// </summary>
		public string MouseGesture
		{
			get
			{
				return mouseGesture.ToString();
			}
		}

		#endregion

		#region 動画の横幅

		/// <summary>
		/// 動画の横幅
		/// </summary>
		public int ImageWidth
		{
			get
			{
				try
				{
					if (currentMedia.imageSourceWidth != 0)
					{
						return currentMedia.imageSourceWidth;
					}
				}
				catch
				{
				}

				return 320;
			}
		}

		#endregion

		#region 動画の高さ

		/// <summary>
		/// 動画の高さ
		/// </summary>
		public int ImageHeight
		{
			get
			{
				try
				{
					if (currentMedia.imageSourceHeight != 0)
					{
						return currentMedia.imageSourceHeight;
					}
				}
				catch
				{
				}

				return 240;
			}
		}

		#endregion

		#region アスペクト比

		/// <summary>
		/// アスペクト比
		/// </summary>
		public float AspectRate
		{
			get
			{
				if (ImageHeight != 0 && ImageWidth != 0)
				{
					try
					{
						return ImageHeight / (float)ImageWidth;
					}
					catch
					{
					}
				}

				return 240 / (float)320;
			}
		}

		#endregion

		#region 音量

		/// <summary>
		/// 音量
		/// </summary>
		public int Volume
		{
			get
			{
				try
				{
					return settings.volume;
				}
				catch
				{
					return 50;
				}
			}
			set
			{
				try
				{
					settings.volume = value;
					OnVolumeChange(EventArgs.Empty);
				}
				catch
				{
				}
			}
		}

		#endregion

		#region ミュート

		/// <summary>
		/// ミュート
		/// </summary>
		public bool Mute
		{
			get
			{
				try
				{
					return settings.mute;
				}
				catch
				{
					return false;
				}
			}
			set
			{
				try
				{
					settings.mute = value;
					OnVolumeChange(EventArgs.Empty);
				}
				catch
				{
				}
			}
		}

		#endregion

		#region 帯域

		public string BandWidth
		{
			get
			{
				try
				{

					return (network.bandWidth / 1000).ToString() + "/" + (network.bitRate / 1000).ToString();
				}
				catch
				{
				}

				return "0 / 0";
			}
		}


		public string FPS
		{
			get
			{
				try
				{
					return (network.frameRate / 100).ToString() + "/" + network.encodedFrameRate.ToString();
				}
				catch
				{
				}

				return "0 / 0";
			}
		}

		#endregion

		#region 受信したパケット数

		/// <summary>
		/// 受信したパケット数
		/// </summary>
		public int ReceivedPackets
		{
			get
			{
				try
				{
					return network.receivedPackets;
				}
				catch
				{
				}

				return 0;
			}
		}

		#endregion

		#region 再生時間

		/// <summary>
		/// 再生時間
		/// </summary>
		string duration = "00:00";
		public string Duration
		{
			get
			{
				return duration;
			}
			set
			{
				if (value == "")
				{
					duration = "00:00:00";
				}
				else if (value.Length == 5 && value != "接続中..")
				{
					duration = "00:" + value;
				}
				else
				{
					duration = value;
				}
				OnDurationChange(EventArgs.Empty);
			}
		}

		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public WMPEx(MainForm form)
		{
			this.form = form;

			form.KeyDown += new KeyEventHandler(form_KeyDown);
			form.MouseWheel += new MouseEventHandler(form_MouseWheel);
			MouseUpEvent += new AxWMPLib._WMPOCXEvents_MouseUpEventHandler(WMPEx_MouseUpEvent);
			MouseDownEvent += new AxWMPLib._WMPOCXEvents_MouseDownEventHandler(WMPEx_MouseDownEvent);
			MouseMoveEvent += new AxWMPLib._WMPOCXEvents_MouseMoveEventHandler(WMPEx_MouseMoveEvent);
			DoubleClickEvent += new AxWMPLib._WMPOCXEvents_DoubleClickEventHandler(WMPEx_DoubleClickEvent);
			KeyDownEvent += new AxWMPLib._WMPOCXEvents_KeyDownEventHandler(WMPEx_KeyDownEvent);
			URLChange += new EventHandler(WMPEx_URLChange);
			Buffer += new EventHandler(WMPEx_Buffer);
			
			// 1秒タイマー
			timer = new Timer();
			timer.Interval = 1000;
			timer.Tick += new EventHandler(timer_Tick);

			// ショートカット用タイマー
			timerShortCut = new Timer();
			timerShortCut.Interval = 150;
			timerShortCut.Tick += new EventHandler(timerShortCut_Tick);
			//timerShortCut.Start();
		}

		/// <summary>
		/// ショートカット用
		/// </summary>
		void timerShortCut_Tick(object sender, EventArgs e)
		{
			OnKeyDown();
		}

		/// <summary>
		/// 1秒タイマー
		/// </summary>
		void timer_Tick(object sender, EventArgs e)
		{
			// 動画を再生中
			if (ReceivedPackets != beforeReceivedPackets)
			{
				IsMoviePlaying = true;

				if (!IsMovieStart)
				{
					IsMovieStart = true;
					OnMovieStart(EventArgs.Empty);
				}

				// パケット受信時間を初期化
				receivedPacketsTime = 0;
				retryCount = RetryTime;
				retryNum = 0;

				try
				{
					// 再生時間を設定
					Duration = Ctlcontrols.currentPositionString;
				}
				catch
				{
				}
			}
			// パケットを受信していない
			else
			{
				receivedPacketsTime++;

				/*
				// 再生じゃなかったら再生する
				if (playState != WMPLib.WMPPlayState.wmppsPlaying)
				{
					// Ctlcontrols.play();
				}
				 */

				#region Durationの変更：接続中...

				if (!IsMoviePlaying)
				{
					// リトライカウントを減らす
					retryCount--;

					switch (Duration)
					{
						case "接続中":
							Duration = "接続中.";
							break;
						case "接続中.":
							Duration = "接続中..";
							break;
						case "接続中..":
							Duration = "接続中...";
							break;
						case "接続中...":
							Duration = "接続中";
							break;
						case "リトライ":
						default:
							Duration = "接続中";
							break;
					}
				}
				else
				{
					retryCount--;
				}

				#endregion
			}

			// 動画が再生されていない（５秒パケットを受信していない）
			if (retryCount <= 0)
			{
				Duration = "接続中";
				IsMoviePlaying = false;
				OnBuffer(EventArgs.Empty);
			}

			// 前回のパケット数
			beforeReceivedPackets = ReceivedPackets;
		}

		/// <summary>
		/// バッファになった
		/// </summary>
		void WMPEx_Buffer(object sender, EventArgs e)
		{
			// リトライ
			if (retryNum < RetryMax)
			{
				retryNum++;
				retryCount = RetryTime;
				Retry(false);
			}
			// リトライ限界
			else
			{
				// 停止
				Duration = "リト限";
				timer.Stop();
				Ctlcontrols.stop();
			}
		}

		public void Retry(bool IsRetryReset)
		{
			Duration = "リトライ";
			timer.Start();
			if (IsRetryReset)
			{
				retryNum = 0;
			}
			IsMoviePlaying = false;
			receivedPacketsTime = 0;
			retryCount = RetryTime;
			beforeReceivedPackets = 0;

			Ctlcontrols.stop();
			Ctlcontrols.play();
		}

		/// <summary>
		/// MouseUp
		/// </summary>
		void WMPEx_MouseUpEvent(object sender, AxWMPLib._WMPOCXEvents_MouseUpEvent e)
		{
			switch (e.nButton)
			{
				case 1: // Left
					break;
				case 2: // Right
					break;
				case 4: // Middle
					break;
			}

			if (e.nButton == 2 && IsMouseMoving)
			{
				IsMouseMoving = false;

				// マウスジェスチャーイベント
				List<Direction> list = mouseGesture.Value;
				if (form.UseMouseGesture && list.Count > 0)
				{
					OnGesture(mouseGesture.ToString());
				}
			}
		}
	
		/// <summary>
		/// MouseMove
		/// </summary>
		void WMPEx_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
		{
			IsMouseMoving = true;

			// マウスジェスチャー
			mouseGesture.Moving(MousePosition);

			#region カーソル変更

			if (!form.Frame)
			{
				// 斜め判定（上
				if (e.fY <= frameSkewSize)
				{
					// 左上
					if (e.fX <= frameSkewSize)
					{
						System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNWSE;
						return;
					}
					// 右上
					else if (e.fX > form.WMPSize.Width - frameSkewSize)
					{
						System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNESW;
						return;
					}
				}
				// 斜め判定（下
				else if (e.fY >= form.WMPSize.Height - frameSkewSize)
				{
					// 左下
					if (e.fX <= frameSkewSize)
					{
						System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNESW;
						return;
					}
					// 右下
					else if (e.fX > form.WMPSize.Width - frameSkewSize)
					{
						System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNWSE;
						return;
					}
				}

				// 上
				if (e.fY <= frameSize)
				{
					System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNS;
					return;
				}
				// 下
				else if (e.fY >= form.WMPSize.Height - frameSize)
				{
					System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeNS;
					return;
				}

				// 左
				if (e.fX <= frameSize)
				{
					System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeWE;
					return;
				}
				// 右
				else if (e.fX > form.WMPSize.Width - frameSize)
				{
					System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeWE;
					return;
				}
				// 通常
				else
				{
					System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
				}
			}
			else
			{
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			}

			#endregion
		}

		/// <summary>
		/// Bump
		/// </summary>
		public void Bump()
		{
			Duration = "接続中";
			timer.Start();
			retryNum = 0;
			IsMoviePlaying = false;
			receivedPacketsTime = 0;
			retryCount = RetryTime;
			beforeReceivedPackets = 0;

			string url = "/admin?cmd=bump&id=" + URLData.ID;
			HTTP.SendCommand(URLData.Host, URLData.PortNo, url, "Shift_JIS");
		}

		/// <summary>
		/// リレー切断
		/// </summary>
		public void RelayCut()
		{
			// 配信中じゃなければ
			if (form.ChannelInfo.IsInfo && form.ChannelInfo.Status != "BROADCAST")
			{
				string url = "/admin?cmd=stop&id=" + URLData.ID;
				HTTP.SendCommand(URLData.Host, URLData.PortNo, url, "Shift_JIS");
			}
		}

		/// <summary>
		/// リレーキープ
		/// </summary>
		public void RelayKeep()
		{
			string url = "/admin?cmd=keep&id=" + URLData.ID;
			HTTP.SendCommand(URLData.Host, URLData.PortNo, url, "Shift_JIS");
		}

		/// <summary>
		/// URLChange
		/// </summary>
		void WMPEx_URLChange(object sender, EventArgs e)
		{
			timer.Start();
			retryNum = 0;
			IsMoviePlaying = false;
			receivedPacketsTime = 0;
			retryCount = RetryTime;
			beforeReceivedPackets = 0;
			IsMovieStart = false;
		}

		#region ジェスチャー

		/// <summary>
		/// Shift+Control+Altを取得
		/// </summary>
		/// <returns></returns>
		string GetModifiers()
		{
			// ジェスチャー
			byte[] keyState = new byte[256];
			Win32API.GetKeyboardState(keyState);

			string modifiers = "";

			if ((keyState[(int)Keys.ShiftKey] & 128) != 0)
			{
				modifiers += "Shift+";
			}

			if ((keyState[(int)Keys.ControlKey] & 128) != 0)
			{
				modifiers += "Control+";
			}

			if ((keyState[(int)Keys.Menu] & 128) != 0)
			{
				modifiers += "Alt+";
			}

			return modifiers;
		}

		/// <summary>
		/// KeyDown
		/// </summary>
		void OnKeyDown()
		{
			Focus();

			// ジェスチャー
			byte[] keyState = new byte[256];
			Win32API.GetKeyboardState(keyState);

			string gesture = "";

			if ((keyState[(int)Keys.ShiftKey] & 128) != 0)
			{
				gesture += "Shift+";
			}

			if ((keyState[(int)Keys.ControlKey] & 128) != 0)
			{
				gesture += "Control+";
			}

			if ((keyState[(int)Keys.Menu] & 128) != 0)
			{
				gesture += "Alt+";
			}

			for (int i = 0; i < 165; i++)
			{
				if ((keyState[(int)i] & 128) != 0)
				{
					Keys key = (Keys)i;
					if (Enum.IsDefined(typeof(Keys), key))
					{
						string a = gesture + key.ToString();
						OnGesture(a);
					}
				}
			}
		}
		
		/// <summary>
		/// KeyDown
		/// </summary>
		void WMPEx_KeyDownEvent(object sender, AxWMPLib._WMPOCXEvents_KeyDownEvent e)
		{
			OnKeyDown();
			/*
			// KeyDownイベント
			string gesture = "";
			switch (e.nShiftState)
			{
				case 1: // Shift
					if (((Keys)e.nKeyCode) == Keys.ShiftKey)
					{
						gesture = "Shift";
						OnGesture(gesture);
						return;
					}
					else
					{
						gesture = "Shift+";
					}
					break;
				case 2: // Control
					if (((Keys)e.nKeyCode) == Keys.ControlKey)
					{
						gesture = "Control";
						OnGesture(gesture);
						return;
					}
					else
					{
						gesture = "Control+";
					}
					break;
				case 4: // Alt
					if (((Keys)e.nKeyCode) == Keys.Menu)
					{
						gesture = "Alt";
						OnGesture(gesture);
						return;
					}
					else
					{
						gesture = "Alt+";
					}
					break;
			}
			gesture += ((Keys)e.nKeyCode).ToString();
			OnGesture(gesture);
			 */
		}

		void form_KeyDown(object sender, KeyEventArgs e)
		{
			OnKeyDown();

			/*
			// KeyDownイベント
			string gesture = "";
			switch (e.Modifiers)
			{
				case Keys.Shift: // Shift
					if (((Keys)e.KeyCode) == Keys.ShiftKey)
					{
						gesture = "Shift";
						OnGesture(gesture);
						return;
					}
					else
					{
						gesture = "Shift+";
					}
					break;
				case Keys.Control: // Control
					if (((Keys)e.KeyCode) == Keys.ControlKey)
					{
						gesture = "Control";
						OnGesture(gesture);
						return;
					}
					else
					{
						gesture = "Control+";
					}
					break;
				case Keys.Alt: // Alt
					if (((Keys)e.KeyCode) == Keys.Menu)
					{
						gesture = "Alt";
						OnGesture(gesture);
						return;
					}
					else
					{
						gesture = "Alt+";
					}
					break;
			}
			gesture += ((Keys)e.KeyCode).ToString();
			OnGesture(gesture);
			 */
		}

		/// <summary>
		/// MouseWheel
		/// </summary>
		void form_MouseWheel(object sender, MouseEventArgs e)
		{
			form.IsOpenContextMenu = false;

			// MouseWheelイベント
			string gesture = GetModifiers();

			if (e.Delta > 0)
			{
				/*
				// 左クリック + 上
				if (e.Button == MouseButtons.Left)
				{
					gesture += "LeftClick+WheelUp";
				}
				// 右クリック + 上
				else if (e.Button == MouseButtons.Right)
				{
					gesture += "RightClick+WheelUp";
				}
				// 中クリック + 上
				else if (e.Button == MouseButtons.Right)
				{
					gesture += "Middle+WheelUp";
				}
				// 上
				else
				 */
				short a = Win32API.GetAsyncKeyState(2);
				if (Win32API.GetAsyncKeyState(1) < 0)
				{
					gesture += "LeftClick+WheelUp";
				}
				else if (Win32API.GetAsyncKeyState(2) < 0)
				{
					gesture += "RightClick+WheelUp";
				}
				else
				{
					gesture += "WheelUp";
				}
			}
			else if (e.Delta < 0)
			{
				/*
				// 左クリック + 下
				if (e.Button == MouseButtons.Left)
				{
					gesture += "LeftClick+WheelDown";
				}
				// 右クリック + 下
				else if (e.Button == MouseButtons.Right)
				{
					gesture += "RightClick+WheelDown";
				}
				// 右クリック + 下
				else if (e.Button == MouseButtons.Middle)
				{
					gesture += "MiddleClick+WheelDown";
				}
				// 下
				else
				 */
				short a = Win32API.GetAsyncKeyState(2);
				if (Win32API.GetAsyncKeyState(1) < 0)
				{
					gesture += "LeftClick+WheelDown";
				}
				else if (Win32API.GetAsyncKeyState(2) < 0)
				{
					gesture += "RightClick+WheelDown";
				}
				else
				{
					gesture += "WheelDown";
				}
			}

			OnGesture(gesture);
		}

		/// <summary>
		/// MouseDown
		/// </summary>
		void WMPEx_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
		{
			clickPoint = new Point(MousePosition.X - Parent.Parent.Location.X, MousePosition.Y - Parent.Parent.Location.Y);

			// フルスクリーン解除
			if (fullScreen)
			{
				fullScreen = false;
			}

			// MouseDownイベント
			string gesture = GetModifiers();

			switch (e.nButton)
			{
				case 1: // Left
					OnGesture(gesture + "LeftClick");
					break;
				case 2: // Right
					OnGesture(gesture + "RightClick");
					form.IsOpenContextMenu = true;
					break;
				case 4: // Middle
					OnGesture(gesture + "MiddleClick");
					break;
			}

			// マウスジェスチャー
			mouseGesture.Start();

			#region サイズ変更、ウィンドウドラッグ

			if (!form.Frame && e.nButton == 1)
			{
				// 斜め判定（上
				if (e.fY <= frameSkewSize)
				{
					// 左上
					if (e.fX <= frameSkewSize)
					{
						Win32API.SendMessage(Parent.Parent.Handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTTOPLEFT), new IntPtr(0));
						return;
					}
					// 右上
					else if (e.fX > form.WMPSize.Width - frameSkewSize)
					{
						Win32API.SendMessage(Parent.Parent.Handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTTOPRIGHT), new IntPtr(0));
						return;
					}
				}
				// 斜め判定（下
				else if (e.fY >= form.WMPSize.Height - frameSkewSize)
				{
					// 左下
					if (e.fX <= frameSkewSize)
					{
						Win32API.SendMessage(Parent.Parent.Handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTBOTTOMLEFT), new IntPtr(0));
						return;
					}
					// 右下
					else if (e.fX > form.WMPSize.Width - frameSkewSize)
					{
						Win32API.SendMessage(Parent.Parent.Handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTBOTTOMRIGHT), new IntPtr(0));
						return;
					}
				}

				// 上
				if (e.fY <= frameSize)
				{
					Win32API.SendMessage(Parent.Parent.Handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTTOP), new IntPtr(0));
					return;
				}
				// 下
				else if (e.fY >= form.WMPSize.Height - frameSize)
				{
					Win32API.SendMessage(Parent.Parent.Handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTBOTTOM), new IntPtr(0));
					return;
				}

				// 左
				if (e.fX <= frameSize)
				{
					Win32API.SendMessage(Parent.Parent.Handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTLEFT), new IntPtr(0));
					return;
				}
				// 右
				else if (e.fX > form.WMPSize.Width - frameSize)
				{
					Win32API.SendMessage(Parent.Parent.Handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTRIGHT), new IntPtr(0)); 
					return;
				}
				// 通常
				else
				{
					// マウスドラッグ
					Win32API.SendMessage(Parent.Parent.Handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTCAPTION), new IntPtr(0));
				}
			}
			else
			{
				// マウスドラッグ
				Win32API.SendMessage(Parent.Parent.Handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTCAPTION), new IntPtr(0));
			}

			#endregion
		}

		/// <summary>
		/// DoubleClick
		/// </summary>
		void WMPEx_DoubleClickEvent(object sender, AxWMPLib._WMPOCXEvents_DoubleClickEvent e)
		{
			// DoubleClickイベント
			string gesture = GetModifiers();
			switch (e.nButton)
			{
				case 1:
					gesture += "DoubleLeftClick";
					break;
				case 2:
					gesture += "DoubleRightClick";
					break;
				case 4:
					gesture += "DoubleMiddleClick";
					break;
			}
			OnGesture(gesture);
		}

		#endregion

		/// <summary>
		/// コマンドラインから再生
		/// </summary>
		public void LoadCommandLine()
		{
			if (Environment.GetCommandLineArgs().Length > 1)
			{
				URL = Environment.GetCommandLineArgs()[1];
			}
		}

		#region URL

		/// <summary>
		/// URL
		/// </summary>
		public override string URL
		{
			get
			{
				return base.URL;
			}
			set
			{
				try
				{
					// 指定なしなら変更なし
					if (value == "")
						return;

					if (base.URL != "" && form.RlayCutOnClose)
					{
						// 現在のチャンネルを切断
						RelayCut();
					}

					string url = "";

					try
					{
						if (value.Substring(0, 6) == "ttp://")
						{
							url = "h" + value;
						}
						else
						{
							url = value;
						}
					}
					catch
					{
					}

					base.URL = url;
					URLData.GetDataFromURL(url);
					Duration = "接続中";
					OnURLChange(EventArgs.Empty);
				}
				catch
				{
				}
			}
		}

		#endregion

		#region WndProc

		/// <summary>
		/// ウィンドウプロシージャ
		/// </summary>
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			const int WM_MOUSEMOVE = 0x200;
			const int WM_LBUTTONDOWN = 0x201;
			const int WM_LBUTTONDBLCLK = 0x0203;
			const int WM_RBUTTONDOWN = 0x0204;

			base.WndProc(ref m);

			switch (m.Msg)
			{
				case Win32API.WM_SETFOCUS:
					Win32API.SendMessage(Parent.Parent.Handle, Win32API.WM_SETFOCUS, Handle, new IntPtr(0));
					form.Focus();
					break;

				case Win32API.WM_KEYDOWN:
					// OnGesture(m.LParam.ToString());
					break;

				//case Win32API.WM_MOUSEHOVER:
				//	form.ShowToolTipDetail(this);
				//	break;

				case WM_MOUSEMOVE:
					break;

				case WM_RBUTTONDOWN:
					break;

				case WM_LBUTTONDOWN:
					if (Win32API.GetAsyncKeyState(2) < 0)
					{
						OnGesture("Right->LeftClick");
						form.IsOpenContextMenu = false;
					}
					break;

				case WM_LBUTTONDBLCLK:
					if (form.WindowsXP)
					{
						if (form.WindowState == FormWindowState.Maximized)
						{
							form.WindowState = FormWindowState.Normal;
							form.OnPanelSizeChange(form.PanelWMPSize);
						}
						else
						{
							form.PanelWMPSize = form.WMPSize;
							form.WindowState = FormWindowState.Maximized;
							form.OnPanelSizeChange();
						}
					}
					break;
				case Win32API.WM_LBUTTONUP: // ダブルクリック
					if ((!form.WindowsXP) && (!form.ToolStipVisile))
					{
						// 右→左クリックした時を除く
						if (Win32API.GetAsyncKeyState(2) >= 0)
						{
							if (form.WindowState == FormWindowState.Maximized)
							{
								form.WindowState = FormWindowState.Normal;
								form.OnPanelSizeChange(form.PanelWMPSize);
							}
							else
							{
								form.PanelWMPSize = form.WMPSize;
								form.WindowState = FormWindowState.Maximized;
								form.OnPanelSizeChange();
							}
						}
					}
					break;
				default:
					break;
			}
		}

		#endregion
	}

	public delegate void EventHandlerString(object sender, string str);

	/// <summary>
	/// URLデータ
	/// </summary>
	public class URLData
	{
		public void GetDataFromURL(string url)
		{
			Regex regex = new Regex(@"ttp://(.*):(.*)/pls/(.*)?tip=");
			Match match = regex.Match(url);

			if (match.Groups.Count == 4)
			{
				Host = match.Groups[1].Value;
				PortNo = match.Groups[2].Value;
				ID = match.Groups[3].Value.Substring(0, match.Groups[3].Value.Length - 1);
				FileName = "";
			}
			else
			{
				Host = "";
				PortNo = "";
				ID = "";
				FileName = url;
			}
		}

		public string Host = "";
		public string PortNo = "";
		public string ID = "";
		public string FileName = "";
	}
}
