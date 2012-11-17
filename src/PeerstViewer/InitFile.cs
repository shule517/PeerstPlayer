using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PeerstPlayer
{
	class IniFile
	{
		private String fileName;

		#region 内部メソッド

		[DllImport("KERNEL32.DLL")]
		private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

		[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringA")]
		private static extern uint GetPrivateProfileStringByByteArray(string lpAppName, string lpKeyName, string lpDefault, byte[] lpReturnedString, uint nSize, string lpFileName);

		[DllImport("KERNEL32.DLL")]
		private static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

		[DllImport("KERNEL32.DLL")]
		private static extern uint WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public IniFile(String fileName)
		{
			this.fileName = fileName;
		}

		/// <summary>
		/// データを書き込む
		/// </summary>
		public void Write(string section, string key, string data)
		{
			WritePrivateProfileString(section, key, data, fileName);
		}

		/// <summary>
		/// 文字データを読み込む（データがなければ""を返す
		/// </summary>
		public string ReadString(string section, string key)
		{
			StringBuilder sb = new StringBuilder(1024);
			GetPrivateProfileString(section, key, "", sb, (uint)sb.Capacity, fileName);

			return sb.ToString();
		}

		/// <summary>
		/// 整数データを読み込む（データがなければ-1を返す
		/// </summary>
		public int ReadInt(string section, string key, int def)
		{
			return (int)GetPrivateProfileInt(section, key, def, fileName);
		}

		/// <summary>
		/// 指定したキーを削除する
		/// </summary>
		public void DeleteKey(string section, string key)
		{
			WritePrivateProfileString(section, key, null, fileName);
		}

		/// <summary>
		///  指定したセクションを削除する
		/// </summary>
		public void DeleteSection(string section)
		{
			WritePrivateProfileString(section, null, null, fileName);
		}

		/// <summary>
		///  セクションの一覧を取得する
		/// </summary>
		public string[] GetSections()
		{
			byte[] ar = new byte[1024];
			uint resultSize = GetPrivateProfileStringByByteArray(null, null, "default", ar, (uint)ar.Length, fileName);
			string result = Encoding.Default.GetString(ar, 0, (int)resultSize - 1);
			return result.Split('\0');
		}

		/// <summary>
		///  指定したセクションのキーの一覧を取得する
		/// </summary>
		public string[] GetKeys(string section)
		{
			byte[] ar = new byte[1024];
			uint resultSize = GetPrivateProfileStringByByteArray(section, null, "default", ar, (uint)ar.Length, fileName);
			string result = Encoding.Default.GetString(ar, 0, (int)resultSize - 1);
			return result.Split('\0');
		}
	}
}
