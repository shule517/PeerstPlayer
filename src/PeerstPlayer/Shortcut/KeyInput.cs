using System;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace PeerstPlayer.Shortcut
{
	/// <summary>
	/// キー入力情報
	/// </summary>
	[DataContract(Name = "KeyInput")]
	class KeyInput : IEquatable<KeyInput>
	{
		/// <summary>
		/// 修飾キー
		/// </summary>
		[DataMember]
		public Keys Modifiers;

		/// <summary>
		/// キー
		/// </summary>
		[DataMember]
		public Keys Key;

		public KeyInput(Keys modifiers, Keys key)
		{
			this.Modifiers = modifiers;
			this.Key = key;
		}

		public KeyInput(Keys key)
		{
			this.Modifiers = Keys.None;
			this.Key = key;
		}

		/// <summary>
		/// Dictionary用比較処理
		/// </summary>
		public bool Equals(KeyInput other)
		{
			if ((Modifiers.Equals(other.Modifiers)) &&
				(Key.Equals(other.Key)))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Dictionary用比較処理
		/// </summary>
		public override bool Equals(object obj)
		{
			if (obj.GetType() != this.GetType()) return false;
			return this.Equals((KeyInput)obj);
		}

		/// <summary>
		/// Dictionary用比較処理
		/// </summary>
		public override int GetHashCode()
		{
			return (int)(Modifiers | Key);
		}
	}
}
