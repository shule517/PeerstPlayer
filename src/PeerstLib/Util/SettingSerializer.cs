using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace PeerstLib.Util
{
	/// <summary>
	/// 設定読み込み用のシリアライザ
	/// </summary>
	public static class SettingSerializer
	{
		/// <summary>
		/// 設定を保存する
		/// </summary>
		/// <typeparam name="T">設定クラスの型</typeparam>
		/// <param name="fileName">保存先のファイル名</param>
		/// <param name="settings">設定が格納されたオブジェクト</param>
		public static void SaveSettings<T>(string fileName, T settings)
		{
			Type type = typeof(T);
			List<Type> knownTypes = GetKnownTypes<T>();

			// シリアライズ
			DataContractSerializer serializer = new DataContractSerializer(type, knownTypes);
			using (FileStream fs = new FileStream(fileName, FileMode.Create))
			{
				serializer.WriteObject(fs, settings);
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <typeparam name="T">設定クラスの型</typeparam>
		/// <param name="fileName">読み込むファイル名</param>
		/// <returns>設定を格納したオブジェクト</returns>
		public static T LoadSettings<T>(string fileName)
		{
			List<Type> knownTypes = GetKnownTypes<T>();

			// 逆シリアライズ
			DataContractSerializer serializer = new DataContractSerializer(typeof(T), knownTypes);
			using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				return (T)serializer.ReadObject(fs);
			}
		}

		/// <summary>
		/// 派生クラスの取得
		/// </summary>
		private static List<Type> GetKnownTypes<T>()
		{
			Type type = typeof(T);
			List<Type> knownTypes = new List<Type>();
			foreach (Attribute attribute in Attribute.GetCustomAttributes(type, typeof(KnownTypeAttribute)))
			{
				knownTypes.Add(((KnownTypeAttribute)attribute).Type);
			}

			return knownTypes;
		}
	}
}
