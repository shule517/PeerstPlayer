using System;
using System.IO;

namespace TestPeerstLib
{
	public static class TestSettings
	{
		// PeerCast動画
		public static string StreamUrl = "http://localhost:7145/pls/900DEB6DF526A220CAC96D1E4043E772?tip=220.100.23.59:7000";

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
