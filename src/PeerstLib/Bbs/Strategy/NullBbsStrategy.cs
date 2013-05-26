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
		protected override Encoding encoding { get { return Encoding.Default; } }
		protected override string subjectUrl { get { return string.Empty; } }
		protected override string datUrl { get { return string.Empty; } }
		protected override string boardUrl { get { return string.Empty; } }
		protected override string writeUrl { get { return string.Empty; } }

		override protected List<ThreadInfo> AnalyzeSubjectText(string[] lines)
		{
			return new List<ThreadInfo>();
		}

		// 書き込み用リクエストデータ作成
		override protected byte[] CreateWriteRequestData(string name, string mail, string message)
		{
			return new byte[0];
		}
	}
}
