using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PeerstPlayer;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace PeerstCaption
{
	public partial class ThreadCaption : Form
	{
		/// <summary>
		/// レスリスト
		/// </summary>
		List<string[]> ResList = new List<string[]>();

		/// <summary>
		/// 掲示板の種類
		/// </summary>
		KindOfBBS KindOfBBS = KindOfBBS.None;

		/// <summary>
		/// 板名
		/// </summary>
		string BoardName = "";

		/// <summary>
		/// 板ジャンル
		/// </summary>
		string BoadGenre = "";

		/// <summary>
		/// 板番号
		/// </summary>
		string BoadNo = "";

		/// <summary>
		/// スレ番号
		/// </summary>
		string ThreadNo = "";

		/// <summary>
		/// 描画文字
		/// </summary>
		string CaptionText = "";

		/// <summary>
		/// 描画レス位置
		/// </summary>
		int CaptionIndex = 0;

		/// <summary>
		/// 描画レス数
		/// </summary>
		int CaptionNum = 15;

		/// <summary>
		/// 描画レスサイズ
		/// </summary>
		float CaptionSize = 8;

		/// <summary>
		/// マウスオーバーしているか
		/// </summary>
		bool IsMouseOver = false;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ThreadCaption()
		{
			InitializeComponent();
			MouseWheel += new MouseEventHandler(PeerstCaption_MouseWheel);

			// コマンドライン入力
			if (Environment.GetCommandLineArgs().Length > 1)
			{
				string url = Environment.GetCommandLineArgs()[1].ToString();

				// urlからデータ抽出
				BBS.GetDataFromUrl(url, out BoardName, out KindOfBBS, out BoadGenre, out BoadNo, out ThreadNo);
				// BBS.GetDataFromUrl(url, out board_name, out kind_of_bbs, out boad_genre, out boad_no, out thread_no);

				// 更新
				UpdateResList(KindOfBBS, BoadGenre, BoadNo, ThreadNo);

				// レス位置を指定
				CaptionIndex = ResList.Count - CaptionNum;

				// 文字更新
				UpdateCaptionText(CaptionIndex, CaptionNum);
			}
		}

		/// <summary>
		/// マウスホイール
		/// </summary>
		void PeerstCaption_MouseWheel(object sender, MouseEventArgs e)
		{
			string gesture = GetModifiers();

			if (gesture == "Control")
			{
				if (e.Delta > 0)
				{
					CaptionSize -= 0.5f;
				}
				else if (e.Delta < 0)
				{
					CaptionSize += 0.5f;
				}

				if (CaptionSize <= 0)
				{
					CaptionSize = 0.5f;
				}

				Refresh();
			}
			else if (gesture == "Alt")
			{
				if (e.Delta > 0)
				{
					CaptionIndex -= CaptionNum;
				}
				else if (e.Delta < 0)
				{
					CaptionIndex += CaptionNum;
				}

				if (CaptionIndex < 0)
					CaptionIndex = 0;
				if (CaptionIndex > ResList.Count)
					CaptionIndex = ResList.Count;

				UpdateCaptionText(CaptionIndex, CaptionNum);
			}
			else if (gesture == "Shift")
			{
				if (e.Delta > 0)
				{
					CaptionNum -= 1;
				}
				else if (e.Delta < 0)
				{
					CaptionNum += 1;
				}

				if (CaptionNum < 1)
					CaptionNum = 1;

				UpdateCaptionText(CaptionIndex, CaptionNum);
			}
			else if (gesture == "")
			{
				if (e.Delta > 0)
				{
					CaptionIndex--;
				}
				else if (e.Delta < 0)
				{
					CaptionIndex++;
				}

				if (CaptionIndex < 0)
					CaptionIndex = 0;
				if (CaptionIndex > ResList.Count)
					CaptionIndex = ResList.Count;

				UpdateCaptionText(CaptionIndex, CaptionNum);
			}
		}

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
				return modifiers += "Shift";
			}

			if ((keyState[(int)Keys.ControlKey] & 128) != 0)
			{
				return modifiers += "Control";
			}

			if ((keyState[(int)Keys.Menu] & 128) != 0)
			{
				return modifiers += "Alt";
			}

			return modifiers;
		}

		/// <summary>
		/// レスデータ更新
		/// </summary>
		public void UpdateResList(KindOfBBS KindOfBBS, string BoadGenre, string BoadNo, string ThreadNo)
		{
			try
			{
				List<string[]> list = BBS.ReadThread(KindOfBBS, BoadGenre, BoadNo, ThreadNo, ResList.Count + 1);

				for (int i = 0; i < list.Count; i++)
				{
					ResList.Add(list[i]);
				}
			}
			catch
			{
			}
		}

		/// <summary>
		/// 字幕文字を更新
		/// </summary>
		private void UpdateCaptionText(int captionIndex, int count)
		{
			string text = "";
			for (int i = 0; i < count; i++)
			{
				int index = captionIndex + i;

				if (ResList.Count != 0 &&
					0 <= index && index <= ResList.Count - 1)
				{
					text += (index + 1).ToString().PadLeft(4) + "  " + ResList[index][4] + "\n\n";
				}
				// text += ResList[index - count + i][0] + " : " + ResList[index - count + i][4] + "\n";
			}

			// タグ除去
			text = text.Replace("<br>", "\n        ");

			Regex regex = new Regex("<.*?>", RegexOptions.Singleline);
			text = regex.Replace(text, "");

			text = text.Replace("&quot;", "\"");
			text = text.Replace("&amp;", "&");
			text = text.Replace("&lt;", "<");
			text = text.Replace("&gt;", ">");
			text = text.Replace("&nbsp;", " ");
			text = text.Replace("&copy;", "@");
			text = text.Replace("&qute;", "\\");

			CaptionText = text;

			Refresh();
		}

		/// <summary>
		/// コンテキストメニュー
		/// </summary>
		private void 更新ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateResList(KindOfBBS, BoadGenre, BoadNo, ThreadNo);
			UpdateCaptionText(CaptionIndex, CaptionNum);
		}

		/// <summary>
		/// リッチテキストを描画
		/// </summary>
		private void DrawRichText(string text, Graphics graphics, Brush brush, float size, int x, int y)
		{
			try
			{/*
				graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x - 1, y));
				graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x + 1, y));
				graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x, y - 1));
				graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x, y + 1));
				graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x - 1, y + 1));
				graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x + 1, y - 1));
				graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x - 1, y - 1));
				graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x + 1, y + 1));
				graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), brush, new PointF(x, y));
			  */

				for (int a = 0; a < 3; a++)
				{
					graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x - a, y));
					graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x + a, y));
					graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x, y - a));
					graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x, y + a));
					graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x - a, y + a));
					graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x + a, y - a));
					graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x - a, y - a));
					graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), Brushes.Black, new PointF(x + a, y + a));
				}
				graphics.DrawString(text, new Font("ＭＳ Ｐゴシック", size), brush, new PointF(x, y));
			}
			catch
			{
			}
		}

		/// <summary>
		/// 描画
		/// </summary>
		private void PeerstCaption_Paint(object sender, PaintEventArgs e)
		{
				e.Graphics.DrawRectangle(Pens.Red, 0, 0, Width - 1, Height - 1);
			DrawRichText(CaptionText, e.Graphics, Brushes.SpringGreen, CaptionSize, 0, 0);

			if (IsMouseOver)
			{

				DrawRichText(BoardName, e.Graphics, Brushes.Yellow, 10, 0, Height - 20);
				DrawScrollBar(e.Graphics);
			}
		}

		private void DrawScrollBar(Graphics graphics)
		{
			int scrollWidth = 10;
			int scrollHeight= 20;
			int x = Width - scrollWidth;
			int y = 0;

			if (ResList.Count == 0)
				return;
			
			float per = (float)CaptionIndex / (float)(ResList.Count);
			int scrollY = (int)(per * Height);

			graphics.DrawRectangle(Pens.Red, x, y, scrollHeight, Height);
			graphics.FillRectangle(Brushes.Red, x, scrollY, scrollHeight, scrollWidth);
		}

		/// <summary>
		/// ウィンドウプロシージャ
		/// </summary>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			#region マウスジェスチャ + ウィンドウサイズ変更

			const int frameSize = 10;
			const int frameSkewSize = 20;

			if (m.Msg == Win32API.WM_NCHITTEST)
			{
				if (MouseButtons == MouseButtons.Right)
				{
					return;
				}

				Point pos = PointToClient(Cursor.Position);
				int x = pos.X;
				int y = pos.Y;

				// 斜め判定（上
				if (y <= frameSkewSize)
				{
					// 左上
					if (x <= frameSkewSize)
					{
						m.Result = (IntPtr)Win32API.HTTOPLEFT;
						return;
					}
					// 右上
					else if (x > Width - frameSkewSize)
					{
						m.Result = (IntPtr)Win32API.HTTOPRIGHT;
						return;
					}
				}
				// 斜め判定（下
				else if (y >= Height - frameSkewSize)
				{
					// 左下
					if (x <= frameSkewSize)
					{
						m.Result = (IntPtr)Win32API.HTBOTTOMLEFT;
						return;
					}
					// 右下
					else if (x > Width - frameSkewSize)
					{
						m.Result = (IntPtr)Win32API.HTBOTTOMRIGHT;
						return;
					}
				}

				// 上
				if (y <= frameSize)
				{
					m.Result = (IntPtr)Win32API.HTTOP;
					return;
				}
				// 下
				else if (y >= Height - frameSize)
				{
					m.Result = (IntPtr)Win32API.HTBOTTOM;
					return;
				}

				// 左
				if (x <= frameSize)
				{
					m.Result = (IntPtr)Win32API.HTLEFT;
					return;
				}
				// 右
				else if (x > Width - frameSize)
				{
					m.Result = (IntPtr)Win32API.HTRIGHT;
					return;
				}
				// マウスドラッグ
				else
				{
					m.Result = (IntPtr)Win32API.HTCAPTION;
					return;
				}
			}
			#endregion
		}

		/// <summary>
		/// マウスオーバーチェック
		/// </summary>
		private void timerMouseCheck_Tick(object sender, EventArgs e)
		{
			if (Left < MousePosition.X && MousePosition.X < Right &&
				Top < MousePosition.Y && MousePosition.Y < Bottom)
			{
				if (!IsMouseOver)
				{
					IsMouseOver = true;
					toolStrip.Visible = true;
					Refresh();
				}
			}
			else
			{
				if (IsMouseOver)
				{
					IsMouseOver = false;
					toolStrip.Visible = false;
					Refresh();
				}
			}
		}

		private void PeerstCaption_MouseDown(object sender, MouseEventArgs e)
		{
//			if (MouseButtons == MouseButtons.Right)
//			{
//				contextMenuStrip.Show(MousePosition);
//			}
		}

		private void PeerstCaption_Resize(object sender, EventArgs e)
		{
			Refresh();
		}

		private void toolStripButtonUpdate_Click(object sender, EventArgs e)
		{
			UpdateResList(KindOfBBS, BoadGenre, BoadNo, ThreadNo);
			UpdateCaptionText(CaptionIndex, CaptionNum);
		}

		private void toolStripButtonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void timerResUpdate_Tick(object sender, EventArgs e)
		{
			UpdateResList(KindOfBBS, BoadGenre, BoadNo, ThreadNo);
			UpdateCaptionText(CaptionIndex, CaptionNum);
		}
	}
}
