using PeerstPlayer.Controls.PecaPlayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 画面分割コマンド
	/// </summary>
	class ScreenSplitCommand : IShortcutCommand
	{
		private Form form;
		private PecaPlayerControl pecaPlayer;

		public ScreenSplitCommand(Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			// ウィンドウ最大化時は実行しない
			if (form.WindowState == FormWindowState.Maximized)
			{
				return;
			}

			ScreenSplitCommandArgs args = (ScreenSplitCommandArgs)commandArgs;

			// 分割サイズを設定
			if (args.WidthNum > 0)
			{
				int formWidth = Screen.GetWorkingArea(form).Width / args.WidthNum;
				form.Width = formWidth;
				int movieHeight = (int)(pecaPlayer.Width / pecaPlayer.AspectRate);
				pecaPlayer.SetSize(pecaPlayer.Width, movieHeight);
			}
			else if (args.HeightNum > 0)
			{
				int formHeight = Screen.GetWorkingArea(form).Height / args.HeightNum;
				form.Height = formHeight;
				int movieWidth = (int)(pecaPlayer.Height * pecaPlayer.AspectRate);
				pecaPlayer.SetSize(movieWidth, pecaPlayer.Height);
			}
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			ScreenSplitCommandArgs args = (ScreenSplitCommandArgs)commandArgs;

			if (args.WidthNum > 0)
			{
				return string.Format("画面分割:幅{0}分割", args.WidthNum);
			}
			else if (args.HeightNum > 0)
			{
				return string.Format("画面分割:高さ{0}分割", args.HeightNum);
			}

			return "画面分割";
		}
	}
}
