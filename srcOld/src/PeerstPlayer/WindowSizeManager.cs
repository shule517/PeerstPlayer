using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer
{
	/// <summary>
	/// ウィンドウサイズメディエイター
	/// </summary>
	interface WindowSizeMediator
	{
		/// <summary>
		/// WMPサイズ変更
		/// </summary>
		/// <param name="width">WMP幅</param>
		/// <param name="height">WMP高さ</param>
		void OnChangeWmpSize(int width, int height);
	}

	class WindowSizeManager
	{
		// サイズ調節対象フォーム
		private Form form = null;
		private WMPEx wmp = null;
		WindowSizeMediator mediator = null;

		/// <summary>
		/// スクリーンサイズ
		/// </summary>
		private Rectangle GetScreen()
		{
			return Screen.GetWorkingArea(form);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="form">フォーム</param>
		/// <param name="wmp">WMP</param>
		/// <param name="mediator">ウィンドウサイズメディエイター</param>
		public WindowSizeManager(Form form, WMPEx wmp, WindowSizeMediator mediator)
		{
			this.form = form;
			this.wmp = wmp;
			this.mediator = mediator;
		}

		/// <summary>
		/// 画面分割：幅
		/// </summary>
		public void ScreenSplitWidth(int num)
		{
			int width = (int)(GetScreen().Width / (float)num);
			form.Size = new Size(width, form.Height);

			int wmpWidth = wmp.Width;
			int wmpHeight = (int)(wmp.AspectRate * wmp.Width);

			mediator.OnChangeWmpSize(wmpWidth, wmpHeight);
		}

		/// <summary>
		/// 画面分割：高さ
		/// </summary>
		public void ScreenSplitHeight(int num)
		{
			int height = (int)(GetScreen().Height / (float)num);
			form.Size = new Size(form.Width, height);

			int wmpWidth = (int)(1 / wmp.AspectRate * wmp.Height);
			int wmpHeight = wmp.Height;

			mediator.OnChangeWmpSize(wmpWidth, wmpHeight);
		}

		/// <summary>
		/// 画面分割
		/// </summary>
		public void ScreenSplit(int num)
		{
			int width = (int)(GetScreen().Width / (float)num);
			int height = (int)(GetScreen().Height / (float)num);
			form.Size = new Size(width, height);

			int wmpWidth = wmp.Width;
			int wmpHeight = wmp.Height;

			mediator.OnChangeWmpSize(wmpWidth, wmpHeight);
		}

		/// <summary>
		/// 幅指定
		/// 高さ：幅
		/// </summary>
		public void SetWidth(int width)
		{
			int height = (int)(wmp.AspectRate * width);
			mediator.OnChangeWmpSize(width, height);
		}

		/// <summary>
		/// 高さ指定
		/// </summary>
		public void SetHeight(int height)
		{
			int width = (int)(1 / wmp.AspectRate * height);
			mediator.OnChangeWmpSize(width, height);
		}

		/// <summary>
		/// サイズ指定
		/// </summary>
		public void SetSize(int width, int height)
		{
			mediator.OnChangeWmpSize(width, height);
		}

		/// <summary>
		/// 拡大率指定
		/// </summary>
		public void SetScale(int scale)
		{
			int width = (int)(wmp.ImageWidth * (scale / (float)100));
			int height = (int)(wmp.ImageHeight * (scale / (float)100));
			mediator.OnChangeWmpSize(width, height);
		}
	}
}
