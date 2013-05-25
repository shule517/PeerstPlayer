using PeerstLib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace PeerstLib.Bbs.Strategy
{
	// 掲示板ストラテジクラス
	public abstract class BbsStrategy
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// 掲示板情報クラス
		public BbsInfo BbsInfo { get; set; }

		// スレッド一覧
		public List<ThreadInfo> ThreadList { get; set; }

		// スレッド選択状態
		public bool ThreadSelected
		{
			get { return (!string.IsNullOrEmpty(BbsInfo.ThreadNo)); }
		}

		// スレッドURL
		abstract public string ThreadUrl { get; }

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		// エンコード
		abstract protected Encoding encoding { get; }

		// スレッド一覧情報URL
		abstract protected string subjectUrl { get; }

		// スレッド情報取得
		abstract protected string datUrl { get; }

		// 板URL
		abstract protected string boardUrl { get; }

		//-------------------------------------------------------------
		// 公開メソッド
		//-------------------------------------------------------------

		// スレッド変更
		public void ChangeThread(string threadNo)
		{
			BbsInfo.ThreadNo = threadNo;
		}

		// スレッド一覧更新
		public void UpdateThreadList()
		{
			string subjectText = WebUtil.GetHtml(subjectUrl, encoding);
			string[] lines = subjectText.Replace("\r\n", "\n").Split('\n');
			ThreadList = AnalyzeSubjectText(lines);
		}

		// 掲示板名の更新
		public void UpdateBbsName()
		{
			string html = WebUtil.GetHtml(boardUrl, encoding);

			int startPos = html.IndexOf("<title>");
			int endPos = html.IndexOf("</title>");

			if ((startPos == -1) || (endPos == -1))
			{
				BbsInfo.BbsName = "";
				return;
			}

			int tagSize = "<title>".Length;
			BbsInfo.BbsName = html.Substring(startPos + tagSize, (endPos - startPos) - tagSize);
		}

		// レス書き込み
		public void Write(string name, string mail, string text)
		{
			throw new Exception();
		}

		//-------------------------------------------------------------
		// 非公開メソッド
		//-------------------------------------------------------------

		// スレッド一覧解析
		abstract protected List<ThreadInfo> AnalyzeSubjectText(string[] lines);

		// コンストラクタ
		protected BbsStrategy(BbsInfo bbsInfo)
		{
			this.BbsInfo = bbsInfo;
		}
	}
}
