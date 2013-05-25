using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstLib.Bbs.Strategy
{
	// ヌル掲示板ストラテジ
	public class NullBbsStrategy : BbsStrategy
	{
		public NullBbsStrategy(BbsInfo bbsInfo) : base(bbsInfo)
		{
		}

		public override string ThreadUrl { get { return BbsInfo.Url; } }
		protected override Encoding Encoding { get { return Encoding.Default; } }
		protected override string SubjectUrl { get { return string.Empty; } }
		protected override string DatUrl { get { return string.Empty; } }
		protected override string BoardUrl { get { return string.Empty; } }

		override protected List<ThreadInfo> AnalyzeSubjectText(string[] lines)
		{
			return new List<ThreadInfo>();
		}
	}
}
