using System;
using System.IO;

namespace TestPeerstLib
{
	public static class TestSettings
	{
		// PeerCast動画
		public static string StreamUrl = "http://localhost:7145/pls/BCFF60B0A56E77509D3A1EE98F1794DE?tip=113.197.174.108:7144";

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
