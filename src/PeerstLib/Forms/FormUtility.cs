using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using PeerstLib.Util;
using PeerstLib.Forms;

namespace PeerstLib.Controls
{
	//-------------------------------------------------------------
	// 概要：フォーム用のUtilityクラス
	//-------------------------------------------------------------
	public class FormUtility
	{
		//-------------------------------------------------------------
		// 概要：ウィンドウドラッグ開始
		// 詳細：タイトルバーをクリックしたことにする
		//-------------------------------------------------------------
		public static void WindowDragStart(IntPtr handle)
		{
			Logger.Instance.DebugFormat("WindowDragStart(handle:{0})", handle);
			Win32API.SendMessage(handle, (int)WindowsMessage.WM_NCLBUTTONDOWN, new IntPtr((int)HitTest.Caption), new IntPtr(0));
		}

		//-------------------------------------------------------------
		// 概要：コンテキストメニュー表示
		//-------------------------------------------------------------
		public static void ShowContextMenu(IntPtr handle, Point mousePositon)
		{
			Logger.Instance.DebugFormat("ShowContextMenu(handle:{0})", handle);
			Win32API.SendMessage(handle, (int)WindowsMessage.WM_CONTEXTMENU, new IntPtr(mousePositon.X), new IntPtr(mousePositon.Y));
		}

		//-------------------------------------------------------------
		// 概要：EXEが存在するフォルダパスを取得
		//-------------------------------------------------------------
		public static string GetExeFolderPath()
		{
			return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
		}
	}
}
