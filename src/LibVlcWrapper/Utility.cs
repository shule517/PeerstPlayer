using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibVlcWrapper
{
	internal static class Utility
	{
		/// <summary>DLLが64Bit用か取得します</summary>
		internal static bool Is64BitDll(string filePath)
		{
			using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			using (var binaryReader = new BinaryReader(fileStream))
			{
				binaryReader.BaseStream.Position = 0x84;
				if (binaryReader.ReadUInt16() == 0x8664)
				{
					return true;
				}
			}
			return false;
		}
	}
}
