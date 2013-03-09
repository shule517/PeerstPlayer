using PeerstPlayer;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Shule.Peerst.Form
{
	public class FormUtility
	{
		/// <summary>
		/// タイトルバーの表示切替
		/// </summary>
		/// <param name="handle">ウィンドウハンドル</param>
		/// <param name="visible">true:表示 / false:非表示</param>
		static public void VisibleTitlebar(IntPtr handle, bool visible)
		{
			if (visible)
			{
				// タイトルバーを出す
				UInt32 style = Win32API.GetWindowLong(handle, Win32API.GWL_STYLE);	// 現在のスタイルを取得
				style = (style | Win32API.WS_CAPTION);								// キャプションのスタイルを削除
				Win32API.SetWindowLong(handle, Win32API.GWL_STYLE, style);			// スタイルを反映
	
				// ウィンドウを再描画
				Win32API.SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, Win32API.SWP.NOMOVE | Win32API.SWP.NOSIZE | Win32API.SWP.NOZORDER | Win32API.SWP.FRAMECHANGED);
			}
			else
			{
				// タイトルバーを消す
				UInt32 style = Win32API.GetWindowLong(handle, Win32API.GWL_STYLE);	// 現在のスタイルを取得
				style = (style & ~Win32API.WS_CAPTION);								// キャプションのスタイルを削除
				Win32API.SetWindowLong(handle, Win32API.GWL_STYLE, style);			// スタイルを反映
	
				// ウィンドウを再描画
				Win32API.SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, Win32API.SWP.NOMOVE | Win32API.SWP.NOSIZE | Win32API.SWP.NOZORDER | Win32API.SWP.FRAMECHANGED);
			}
		}

		/// <summary>
		/// 作業フォルダを取得
		/// </summary>
		static public string GetCurrentDirectory()
		{
			if (Environment.GetCommandLineArgs().Length > 0)
			{
				string folder = Environment.GetCommandLineArgs()[0];
				folder = folder.Substring(0, folder.LastIndexOf('\\'));

				return folder;
			}
			return "";
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
	}
}
