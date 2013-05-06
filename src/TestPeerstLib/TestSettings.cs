using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestPeerstLib
{
	public static class TestSettings
	{
		// PeerCast動画
		public static string StreamUrl = "http://localhost:7145/pls/90E13182A11873DF1B8ADD5F4E7C0A38?tip=183.181.158.208:7154";

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
