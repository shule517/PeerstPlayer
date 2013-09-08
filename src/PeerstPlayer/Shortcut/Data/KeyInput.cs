using System.Windows.Forms;

namespace PeerstPlayer.Shortcut.Data
{
	//-------------------------------------------------------------
	// 概要：キー入力情報クラス
	//-------------------------------------------------------------
	class KeyInput
	{
		/// <summary>
		/// 入力キー
		/// </summary>
		public Keys Key { get; set; }

		/// <summary>
		/// 修飾キー
		/// </summary>
		public Keys ModifierKey { get; set; }

		/// <summary>
		/// 修飾キー付きの入力を分解する
		/// [入力]
		///    Keys.Control | Keys.Shift | Keys.D1
		/// [設定値]
		///    Key : Keys.D1
		///    ModifierKey : Keys.Control | Keys.Shift
		/// </summary>
		public KeyInput(Keys key)
		{
			Key = key & (~Keys.Modifiers);
			ModifierKey = key & Keys.Modifiers;
		}
	}
}
