using System;
using System.IO;
using System.Xml.Serialization;

namespace PeerstLib.Util
{
	/// <summary>
	/// シリアライザ
	/// </summary>
	public class Serializer
	{
		/// <summary>
		/// シリアライズ：保存
		/// </summary>
		public static void Save(string filePath, Object data)
		{
			XmlSerializer serializer = new XmlSerializer(data.GetType());
			using (FileStream fs = new FileStream(filePath, FileMode.Create))
			{
				serializer.Serialize(fs, data);
			}
		}

		/// <summary>
		/// 逆シリアライズ：読み込み
		/// </summary>
		public static Object Load(string filePath, Type type)
		{
			XmlSerializer serializer = new XmlSerializer(type);
			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				return serializer.Deserialize(fs); ;
			}
		}

		/// <summary>
		/// 逆シリアライズ：読み込み
		/// </summary>
		public static bool TryLoad(string filePath, Type type, out Object data)
		{
			data = null;

			try
			{
				data = Load(filePath, type);
			}
			catch
			{
				return false;
			}

			return true;
		}
	}
}
