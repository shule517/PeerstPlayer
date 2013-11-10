using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Shule.Peerst.PeerCast
{
	public class PecaInfo
	{
		public string Host { get; set; }
		public string PortNo { get; set; }
		public string StreamId { get; set; }
	}

	public static class StreamUrlAnalyzer
	{
		public static PecaInfo GetPecaInfo(string url)
		{
			PecaInfo pecaInfo = new PecaInfo();
			Regex regex = new Regex(@"ttp://(.*):(.*)/pls/(.*)?tip=");
			Match match = regex.Match(url);

			if (match.Groups.Count == 4)
			{
				pecaInfo.Host = match.Groups[1].Value;
				pecaInfo.PortNo = match.Groups[2].Value;
				pecaInfo.StreamId = match.Groups[3].Value.Substring(0, match.Groups[3].Value.Length - 1);
			}

			return pecaInfo;
		}
	}
}
