using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace PeerstLib.Utility
{
	static public class WebUtil
	{
		//-------------------------------------------------------------
		// 概要：HTMLの取得
		// 詳細：subject.txtからスレッド一覧情報を作成する
		//-------------------------------------------------------------
		public static string GetHtml(string url, Encoding encoding)
		{
			try
			{
				WebClient wc = new WebClient();
				byte[] data = wc.DownloadData(url);
				return encoding.GetString(data);
			}
			catch
			{
				return "";
			}
		}
	}
}
