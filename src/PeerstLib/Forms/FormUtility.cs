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
			if (Assembly.GetEntryAssembly() == null)
			{
				return null;
			}
			return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
		}

		/// <summary>
		/// マウスとウィンドウ枠の当たり判定
		/// </summary>
		public static HitArea GetHitArea(int frameSize, int fX, int fY, int width, int height)
		{
			// 斜め判定（上
			if (fY <= frameSize)
			{
				// 左上
				if (fX <= frameSize)
				{
					return HitArea.HTTOPLEFT;
				}
				// 右上
				else if (fX > (width - frameSize))
				{
					return HitArea.HTTOPRIGHT;
				}
			}
			// 斜め判定（下
			else if (fY >= (height - frameSize))
			{
				// 左下
				if (fX <= frameSize)
				{
					return HitArea.HTBOTTOMLEFT;
				}
				// 右下
				else if (fX > (width - frameSize))
				{
					return HitArea.HTBOTTOMRIGHT;
				}
			}

			// 上
			if (fY <= frameSize)
			{
				return HitArea.HTTOP;
			}
			// 下
			else if (fY >= (height - frameSize))
			{
				return HitArea.HTBOTTOM;
			}

			// 左
			if (fX <= frameSize)
			{
				return HitArea.HTLEFT;
			}
			// 右
			else if (fX > (width - frameSize))
			{
				return HitArea.HTRIGHT;
			}

			return HitArea.HTNONE;
		}
	}
}
