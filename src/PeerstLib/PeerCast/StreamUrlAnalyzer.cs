using PeerstLib.Bbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PeerstLib.PeerCast
{
	//-------------------------------------------------------------
	// 概要：ストリームURL解析クラス
	//-------------------------------------------------------------
	public static class StreamUrlAnalyzer
	{
		//-------------------------------------------------------------
		// 定義
		//-------------------------------------------------------------
		const string StreamUrlPattern = @"ttp://(.*):(.*)/pls/(.*)?tip=";
		const int GroupCount = 4;

		//-------------------------------------------------------------
		// 概要：ストリームURL情報の取得
		//-------------------------------------------------------------
		public static StreamUrlInfo GetUrlInfo(string streamUrl)
		{
			StreamUrlInfo info = new StreamUrlInfo();
			Regex regex = new Regex(StreamUrlPattern);
			Match match = regex.Match(streamUrl);

			if (match.Groups.Count == GroupCount)
			{
				info.Host = match.Groups[1].Value;
				info.PortNo = match.Groups[2].Value;
				info.StreamId = match.Groups[3].Value.Substring(0, match.Groups[3].Value.Length - 1);
			}

			return info;
		}
	}
}
