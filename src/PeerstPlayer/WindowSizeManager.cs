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

		public WindowSizeManager(Form form, WMPEx wmp, WindowSizeMediator mediator)
		{
			this.form = form;
			this.wmp = wmp;
			this.mediator = mediator;
		}

		/*
		/// <summary>
		/// 画面分割：幅
		/// </summary>
		public void ScreenSplitWidth(int num)
		{

			// TODO WMPサイズ基準でないと、アスペクト比がずれてしまう
			// ウィンドウ幅 = WMP幅 + ウィンドウフレーム
			// WMP幅 = ウィンドウ幅 - ウィンドウフレーム
			// WMP高さ = WMP幅 * アスペクト比

			// TODO ウィンドウフレーム幅の取得方法
			int windowFrameWidth = 0;

			int windowWidth = (int)(GetScreen().Width / (float)num);
			int wmpWidth = windowWidth - windowFrameWidth;
			int wmpHeight = (int)(wmp.AspectRate * wmpWidth);
			SetWMPSize(wmpWidth, wmpHeight);
		}

		/// <summary>
		/// 画面分割：高さ
		/// </summary>
		public void ScreenSplitHeight(int num)
		{
			//int height = (int)(Screen.Height / (float)num);
			//Size = new Size(Width, height);
			//panelWMP.Size = new Size((int)(1 / wmp.AspectRate * wmp.Height), wmp.Height);
			//OnPanelSizeChange();
		}

		/// <summary>
		/// 画面分割
		/// </summary>
		public void ScreenSplit(int num)
		{
			int windowWidth = (int)(GetScreen().Width / (float)num);
			int windowHeight = (int)(GetScreen().Height / (float)num);

			// TODO WMPの指定だとアスペクト比があった状態になってしまう
	
			//int width = (int)(Screen.Width / (float)num);
			//int height = (int)(Screen.Height / (float)num);
			//Size = new Size(width, height);
			//OnPanelSizeChange();
		}
		 */

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
