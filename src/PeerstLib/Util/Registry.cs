using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace PeerstLib.Util
{
	/// <summary>
	/// レジストリを扱うクラス
	/// </summary>
	public class Registry
	{
		public static bool TryGetString(string baseKey, string subKey, string name, out string value)
		{
			var registry = GetRegistryKey(baseKey).OpenSubKey(subKey);
			if (registry != null && registry.GetValue(name) != null)
			{
				value = (string)registry.GetValue(name);
				return true;
			}
			else
			{
				value = "";
				return false;
			}
		}

		public static bool TryGetInt(string baseKey, string subKey, string name, out int value)
		{
			var registry = GetRegistryKey(baseKey).OpenSubKey(subKey);
			if (registry != null && registry.GetValue(name) != null)
			{
				value = (int)registry.GetValue(name);
				return true;
			}
			else
			{
				value = 0;
				return false;
			}
		}

		private static RegistryKey GetRegistryKey(string baseKey)
		{
			baseKey = baseKey.ToUpper();
			if (baseKey == "HKEY_LOCAL_MACHINE")
			{
				return Microsoft.Win32.Registry.LocalMachine;
			}
			if (baseKey == "HKEY_CURRENT_USER")
			{
				return Microsoft.Win32.Registry.CurrentUser;
			}
			if (baseKey == "HKEY_CURRENT_CONFIG")
			{
				return Microsoft.Win32.Registry.CurrentConfig;
			}
			if (baseKey == "HKEY_CLASSES_ROOT")
			{
				return Microsoft.Win32.Registry.ClassesRoot;
			}
			throw new ArgumentException("baseKey");
		}
	}
}
