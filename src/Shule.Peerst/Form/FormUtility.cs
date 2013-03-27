using PeerstPlayer;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Shule.Peerst.Form
{
	public static class FormUtility
	{
		/// <summary>
		/// タイトルバーの表示切替
		/// </summary>
		/// <param name="handle">ウィンドウハンドル</param>
		/// <param name="visible">true:表示 / false:非表示</param>
		static public void VisibleTitlebar(IntPtr handle, bool visible)
		{
			// 現在のスタイルを取得
			UInt32 style = Win32API.GetWindowLong(handle, Win32API.GWL_STYLE);			

			if (visible)
			{
				// タイトルバーを出す
				style = (style | Win32API.WS_CAPTION);
			}
			else
			{
				// タイトルバーを消す
				style = (style & ~Win32API.WS_CAPTION);
			}

			// スタイルを反映
			Win32API.SetWindowLong(handle, Win32API.GWL_STYLE, style);

			// ウィンドウを再描画
			Win32API.SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, Win32API.SWP.NOMOVE | Win32API.SWP.NOSIZE | Win32API.SWP.NOZORDER | Win32API.SWP.FRAMECHANGED);
		}

		/// <summary>
		/// 実行ファイルのフォルダパスを取得
		/// </summary>
		static public string GetExeFileDirectory()
		{
			if (Environment.GetCommandLineArgs().Length <= 0)
			{
				return "";
			}

			// コマンドライン[0]から、フォルダパスのみ切り出す
			string folder = Environment.GetCommandLineArgs()[0];
			folder = folder.Substring(0, folder.LastIndexOf('\\'));
			return folder;
		}

		/// <summary>
		/// ウィンドウドラッグを開始
		/// </summary>
		/// <param name="handle">ウィンドウハンドル</param>
		public static void WindowDragStart(IntPtr handle)
		{
			Win32API.SendMessage(handle, Win32API.WM_NCLBUTTONDOWN, new IntPtr(Win32API.HTCAPTION), new IntPtr(0));
		}

		/// <summary>
		/// ウィンドウの最大化/解除
		/// </summary>
		/// <param name="form">対象のフォーム</param>
		public static void ToggleWindowMaximize(System.Windows.Forms.Form form)
		{
			if (form.WindowState == FormWindowState.Normal)
			{
				// ウィンドウの最大化
				form.WindowState = FormWindowState.Maximized;
			}
			else if (form.WindowState == FormWindowState.Maximized)
			{
				// 解除
				form.WindowState = FormWindowState.Normal;
			}
		}

		/// <summary>
		/// 修飾キーの状態取得
		/// Shift / Control / Altキーを監視
		/// </summary>
		/// <returns></returns>
		public static List<Keys> GetModifyKeys()
		{
			List<Keys> keys = new List<Keys>();

			// キーボードの状態取得
			byte[] keyState = new byte[256];
			Win32API.GetKeyboardState(keyState);

			// 判定
			if ((keyState[(int)Keys.ShiftKey] & 128) != 0)
			{
				keys.Add(Keys.ShiftKey);
			}

			if ((keyState[(int)Keys.ControlKey] & 128) != 0)
			{
				keys.Add(Keys.ControlKey);
			}

			if ((keyState[(int)Keys.Menu] & 128) != 0)
			{
				keys.Add(Keys.Menu);
			}

			return keys;
		}

		// タイトルバーの高さ
		public static int TitlebarHeight { get { return SystemInformation.CaptionHeight; } }
	}
}
