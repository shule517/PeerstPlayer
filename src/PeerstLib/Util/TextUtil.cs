using System;
using System.Globalization;
using PeerstLib.Controls;

namespace PeerstLib.Util
{
	static public class TextUtil
	{
		/// <summary>
		/// 全角文字を半角文字に変換する
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string FullWidthToHalfWidth(string text)
		{
			var result = new string(' ', text.Length);
			Win32API.LCMapStringW(CultureInfo.CurrentCulture.LCID, MapFlags.LCMAP_HALFWIDTH, text, text.Length, result, result.Length);
			return result;
		}
	}
}
