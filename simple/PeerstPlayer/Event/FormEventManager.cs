using PeerstPlayer.Event;
using Shule.Peerst.Form;
using Shule.Peerst.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer
{
	class FormEventManager : Observable
	{
		/// <summary>
		/// Formクラス
		/// </summary>
		Form form;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="form"></param>
		public FormEventManager(Form form)
		{
			// データ登録
			this.form = form;

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
				Notify(FormEvents.DoubleLeftClick);
			}
			// 右ダブルクリック
			else if (e.Button == MouseButtons.Right)
			{
				Notify(FormEvents.DoubleRightClick);
			}
			// 中ダブルクリック
			else if (e.Button == MouseButtons.Middle)
			{
				Notify(FormEvents.DoubleMiddleClick);
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
				Notify(FormEvents.LeftClick);
			}
			// 右クリック
			else if (e.Button == MouseButtons.Right)
			{
				Notify(FormEvents.RightClick);
			}
			// 中クリック
			else if (e.Button == MouseButtons.Middle)
			{
				Notify(FormEvents.MiddleClick);
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
					Notify(FormEvents.LeftClick_WheelUp);
				}
				// 右クリック + ホイールアップ
				else if (Win32API.GetAsyncKeyState(KeyRight) < 0)
				{
					Notify(FormEvents.RightClick_WheelUp);
				}
				// ホイールアップ
				else
				{
					Notify(FormEvents.WheelUp);
				}
			}
			else if (e.Delta < 0)
			{
				// 左クリック + ホイールダウン
				if (Win32API.GetAsyncKeyState(KeyLeft) < 0)
				{
					Notify(FormEvents.LeftClick_WheelDown);
				}
				// 右クリック + ホイールダウン
				else if (Win32API.GetAsyncKeyState(KeyRight) < 0)
				{
					Notify(FormEvents.RightClick_WheelDown);
				}
				// ホイールダウン
				else
				{
					Notify(FormEvents.WheelDown);
				}
			}
		}

		/// <summary>
		/// 通知
		/// </summary>
		/// <param name="events"></param>
		private void Notify(FormEvents events)
		{
			List<Keys> keys = FormUtility.GetModifyKeys();
			NotifyObservers(new FormEventArgs(events, keys));
		}
	}
}
