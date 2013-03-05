using PeerstPlayer;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Shule.Peerst.Form
{
	public class FormUtility
	{
		/// <summary>
		/// タイトルバーの表示切替
		/// </summary>
		/// <param name="handle"></param>
		/// <param name="visible"></param>
		static public void VisibleTitlebar(IntPtr handle, bool visible)
		{
			if (visible)
			{
				// タイトルバーを出す
				UInt32 style = Win32API.GetWindowLong(handle, Win32API.GWL_STYLE);	// 現在のスタイルを取得
				style = (style | Win32API.WS_CAPTION);							// キャプションのスタイルを削除
				Win32API.SetWindowLong(handle, Win32API.GWL_STYLE, style);				// スタイルを反映
				Win32API.SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, Win32API.SWP.NOMOVE | Win32API.SWP.NOSIZE | Win32API.SWP.NOZORDER | Win32API.SWP.FRAMECHANGED); // ウィンドウを再描画
			}
			else
			{
				// タイトルバーを消す
				UInt32 style = Win32API.GetWindowLong(handle, Win32API.GWL_STYLE);	// 現在のスタイルを取得
				style = (style & ~Win32API.WS_CAPTION);							// キャプションのスタイルを削除
				Win32API.SetWindowLong(handle, Win32API.GWL_STYLE, style);				// スタイルを反映
				Win32API.SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, Win32API.SWP.NOMOVE | Win32API.SWP.NOSIZE | Win32API.SWP.NOZORDER | Win32API.SWP.FRAMECHANGED); // ウィンドウを再描画
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
	}
}
