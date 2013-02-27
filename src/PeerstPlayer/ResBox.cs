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
	}
}
