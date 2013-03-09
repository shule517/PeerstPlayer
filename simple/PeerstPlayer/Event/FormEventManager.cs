using PeerstPlayer.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer
{
	class FormEventManager
	{
		/// <summary>
		/// Formクラス
		/// </summary>
		Form form;

		/// <summary>
		/// イベントオブザーバ
		/// </summary>
		EventObserver eventObserver;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="form"></param>
		public FormEventManager(Form form, EventObserver eventObserver)
		{
			this.form = form;
			this.eventObserver = eventObserver;

			// イベント登録
			form.MouseDown += form_MouseDown;
			form.MouseDoubleClick += form_MouseDoubleClick;
			form.MouseWheel += form_MouseWheel;
		}

		/// <summary>
		/// ダブルクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void form_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			// 左ダブルクリック
			if (e.Button == MouseButtons.Left)
			{
				eventObserver.OnEvent(Events.DoubleLeftClick, e);
			}
			// 右ダブルクリック
			else if (e.Button == MouseButtons.Right)
			{
				eventObserver.OnEvent(Events.DoubleRightClick, e);
			}
			// 中ダブルクリック
			else if (e.Button == MouseButtons.Middle)
			{
				eventObserver.OnEvent(Events.DoubleMiddleClick, e);
			}
		}

		/// <summary>
		/// マウスダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void form_MouseDown(object sender, MouseEventArgs e)
		{
			// 左クリック
			if (e.Button == MouseButtons.Left)
			{
				eventObserver.OnEvent(Events.LeftClick, e);
			}
			// 右クリック
			else if (e.Button == MouseButtons.Right)
			{
				eventObserver.OnEvent(Events.RightClick, e);
			}
			// 中クリック
			else if (e.Button == MouseButtons.Middle)
			{
				eventObserver.OnEvent(Events.MiddleClick, e);
			}
		}

		const int KeyLeft = 1;		// 左クリック
		const int KeyRight = 2;		// 右クリック

		/// <summary>
		/// マウスホイール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void form_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta > 0)
			{
				// 左クリック + ホイールアップ
				if (Win32API.GetAsyncKeyState(KeyLeft) < 0)
				{
					eventObserver.OnEvent(Events.LeftClick_WheelUp, e.Delta);
				}
				// 右クリック + ホイールアップ
				else if (Win32API.GetAsyncKeyState(KeyRight) < 0)
				{
					eventObserver.OnEvent(Events.RightClick_WheelUp, e.Delta);
				}
				// ホイールアップ
				else
				{
					eventObserver.OnEvent(Events.WheelUp, e.Delta);
				}
			}
			else if (e.Delta < 0)
			{
				// 左クリック + ホイールダウン
				if (Win32API.GetAsyncKeyState(KeyLeft) < 0)
				{
					eventObserver.OnEvent(Events.LeftClick_WheelDown, e.Delta);
				}
				// 右クリック + ホイールダウン
				else if (Win32API.GetAsyncKeyState(KeyRight) < 0)
				{
					eventObserver.OnEvent(Events.RightClick_WheelDown, e.Delta);
				}
				// ホイールダウン
				else
				{
					eventObserver.OnEvent(Events.WheelDown, e.Delta);
				}
			}
		}
	}
}
