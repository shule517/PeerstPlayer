using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer
{
	class ResBox : TextBox
	{
		string message = "";

		/// <summary>
		/// 選択されているか
		/// </summary>
		public bool Selected
		{
			get
			{
				return selected;
			}
			set
			{
				selected = value;

				// 選択する
				if (value)
				{
					Enabled = true; // 表示
					Text = message; // 前回のメッセージを入力する
					Focus(); // 選択する
					SelectionStart = Text.Length;
				}
				// 選択をはずす
				else
				{
					if (Text == "スレッドが選択されていません" || Text.IndexOf("スレッド：") != -1)
					{
					}
					else
					{
						message = Text; // 前回のメッセージを控える
					}

					Enabled = false; // 非表示
					// Multiline = false; // １行にする

					// スレッドが選択されていない
					if (ThreadTitle == "")
					{
						Text = "スレッドが選択されていません";
					}
					// スレッドが選択されていたらスレタイを表示
					else
					{
						Text = "スレッド：" + ThreadTitle;
					}
				}
			}
		}
		private bool selected;

		/// <summary>
		/// スレッドタイトル
		/// </summary>
		public string ThreadTitle
		{
			get
			{
				return threadTitle;
			}
			set
			{
				threadTitle = value;
				Selected = Selected; // スレタイ表示し直し
			}
		}
		string threadTitle = "";

		/// <summary>
		/// 入力中に枠外をクリックした時に、選択をはずすか
		/// </summary>
		internal bool InputingSelected
		{
			get { return inputingSelected; }
			set { inputingSelected = value; }
		}
		bool inputingSelected = true;

		const int inputRange = 15;
		/// <summary>
		/// クリックしたら選択
		/// </summary>
		public bool AutoEnable(int x)
		{
			// 文字を入力中は表示したまま
			//if (InputingSelected && Text != "スレッドが選択されていません" && Text != "")
			if (Text == "スレッドが選択されていません" | Text == "")
			{
				return false;
			}

			if (x <= Right - inputRange)
			{
				Selected = true;
				return true;
			}
			else
			{
				Selected = false;
				return false;
			}
		}

		/*
		/// <summary>
		/// マウスオーバーで表示
		/// </summary>
		public void AutoVisible(int x, int y)
		{
			const int StatusLabelHeight = 22;
			// 選択中は表示したまま
			if (Selected)
			{
				return;
			}

			if (Parent.ClientSize.Height - StatusLabelHeight - Height <= y &&
				y <= Parent.ClientSize.Height - StatusLabelHeight)
			{
				Visible = true;
			}
			else
			{
				Visible = false;
			}
		}
		 */
	}
}
