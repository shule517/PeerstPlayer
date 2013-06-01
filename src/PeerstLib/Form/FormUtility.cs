using System;

namespace PeerstLib.Control
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
			Win32API.SendMessage(handle, (int)WindowMessage.WM_NCLBUTTONDOWN, new IntPtr((int)HitTest.Caption), new IntPtr(0));
		}
	}
}
