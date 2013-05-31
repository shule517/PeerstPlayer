using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PeerstLib.Control
{
	//-------------------------------------------------------------
	// 概要：Win32APIクラス
	// 詳細：Win32APIのimport
	//-------------------------------------------------------------
	class Win32API
	{
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
	}

	// ウィンドウメッセージ
	public enum WindowMessage : int
	{
		WM_NCLBUTTONDOWN = 0x00A1,
		WM_LBUTTONDBLCLK = 0x0203,
		WM_RBUTTONDOWN = 0x0204,
		WM_NCHITTEST = 0x0084,
		WM_LBUTTONUP = 0x0202,
		WM_MOUSEHOVER = 0x02A1,
		WM_SIZE = 0x0005,
		WM_SIZING = 0x214,
		WM_MOVE = 0x0003,
		WM_MOVING = 0x0216,
		WM_MOUSEMOVE = 0x200,
		WM_LBUTTONDOWN = 0x201,
		WM_MOUSEWHEEL = 0x020A,
		WM_CONTEXTMENU = 0x007B,
		WM_KEYDOWN = 0x0100,
		WM_SETFOCUS = 0x0007,
	}

	// 当たり判定
	enum HitTest : int
	{
		Caption = 2,		// タイトルバー
		Top = 12,			// 可変枠の上辺境界線上にある
		Bottom = 15,		// 可変枠の下辺境界線上にある
		Left = 10,			// 可変枠の左辺境界線上にある
		Right = 11,			// 可変枠の右辺境界線上にある
		TopLeft = 13,		// 可変枠の左上隅にある
		TopRight = 14,		// 可変枠の右上隅にある
		BottomLeft = 16,	// 可変枠の左下隅にある
		BottomRight = 17,	// 可変枠の右下隅にある
	}
}
