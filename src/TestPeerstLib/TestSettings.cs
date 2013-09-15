using System;
using System.IO;

namespace TestPeerstLib
{
	public static class TestSettings
	{
		// PeerCast動画
		public static string StreamUrl = "http://localhost:7145/pls/C745E16E05F6C5DB014570C85EA1B3C0?tip=223.217.98.217:17144";

		// ローカル動画(WMV)
		public static string LocalMoviePath
		{
			get
			{
				string dir = Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory));
				return dir + @"\TestData\movie.wmv";
			}
		}

		// ViewXMLテストデータ
		public static string ViewXMLPath
		{
			get
			{
				string dir = Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory));
				return dir + @"\TestData\admin.xml";
			}
		}
	}
}
