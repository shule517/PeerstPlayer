using System;
using System.IO;

namespace TestPeerstLib
{
	public static class TestSettings
	{
		// PeerCast動画
		public static string StreamUrl = "http://localhost:7145/pls/90E13182A11873DF1B8ADD5F4E7C0A38?tip=210.170.196.60:7154";

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
