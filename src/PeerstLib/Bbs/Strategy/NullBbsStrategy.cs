using System.Collections.Generic;
using System.Text;

namespace PeerstLib.Bbs.Strategy
{
	//-------------------------------------------------------------
	// 概要：ヌル掲示板ストラテジ
	//-------------------------------------------------------------
	public class NullBbsStrategy : BbsStrategy
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// スレッドURL
		public override string ThreadUrl { get { return BbsInfo.Url; } }

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// 掲示板エンコード
		protected override Encoding encoding { get { return Encoding.Default; } }

		// 掲示板一覧URL
		protected override string subjectUrl { get { return string.Empty; } }

		// スレッド情報URL
		protected override string datUrl { get { return string.Empty; } }

		// 板URL
		protected override string boardUrl { get { return string.Empty; } }

		// 書き込みリクエストURL
		protected override string writeUrl { get { return string.Empty; } }

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：掲示板情報の初期化
		//-------------------------------------------------------------
		public NullBbsStrategy(BbsInfo bbsInfo) : base(bbsInfo)
		{
		}

		//-------------------------------------------------------------
		// 概要：スレッド情報の解析
		//-------------------------------------------------------------
		override protected List<ThreadInfo> AnalyzeSubjectText(string[] lines)
		{
			return new List<ThreadInfo>();
		}

		//-------------------------------------------------------------
		// 概要：書き込み用リクエストデータ作成
		//-------------------------------------------------------------
		override protected byte[] CreateWriteRequestData(string name, string mail, string message)
		{
			return new byte[0];
		}
	}
}
