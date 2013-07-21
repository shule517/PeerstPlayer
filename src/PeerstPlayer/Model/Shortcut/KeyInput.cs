using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer.Model.Shortcut
{
	//-------------------------------------------------------------
	// 概要：キー入力情報クラス
	//-------------------------------------------------------------
	class KeyInput
	{
		/// <summary>入力キー</summary>
		public Keys Key { get; set; }

		/// <summary>修飾キー</summary>
		public Keys ModifierKey { get; set; }

		public KeyInput(Keys key, Keys modifier)
		{
			Key = key;
			ModifierKey = modifier;
		}

		public KeyInput(Keys key)
		{
			Key = key;
			ModifierKey = Keys.None;
		}
	}
}
