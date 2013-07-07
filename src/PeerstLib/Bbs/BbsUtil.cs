using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PeerstLib.Utility;

namespace PeerstLib.Bbs
{
	//-------------------------------------------------------------
	// 概要：掲示板ユーティリティクラス
	//-------------------------------------------------------------
	public static class BbsUtil
	{
		//-------------------------------------------------------------
		// 概要：書き込みステータス
		//-------------------------------------------------------------
		public enum WriteStatus
		{
			Posted,				// 書きこみました
			NothingMessage,		// 本文がありません
			MultiWriteError,	// 多重書き込みです
			NothingThreadError, // 該当スレッドは存在しません
			LostUserInfoError,	// ユーザー設定が消失しています
			ThreadStopError,	// スレッドストップです
		}

		//-------------------------------------------------------------
		// 概要：書き込みエラーチェック
		//-------------------------------------------------------------
		public static string CheckWriteError(string html)
		{
			Regex titleRegex = new Regex("<title>(.*)</title>");
			Match titleMatch = titleRegex.Match(html);
			string title = titleMatch.Groups[1].Value;

			Regex tagRegex = new Regex("<!-- 2ch_X:(.*) -->");
			Match tagMatch = tagRegex.Match(html);
			string tag = tagMatch.Groups[1].Value;

			// 書き込み失敗
			if (title.StartsWith("ERROR") || title.StartsWith("ＥＲＲＯＲ"))
			{
				Logger.Instance.ErrorFormat("レス書き込み：異常 [実行結果:{0}]", html);
				throw new Exception();
			}

			Logger.Instance.DebugFormat("レス書き込み：正常 [実行結果:{0}]", title);
			return html;
		}

		//-------------------------------------------------------------
		// 概要：スレッド作成日時の取得
		//-------------------------------------------------------------
		public static double GetThreadSince(string threadNo)
		{
			// 経過秒数の取得
			int second;
			if (!int.TryParse(threadNo, out second)) { return -1; }

			// 経過日時の取得
			DateTime time = new DateTime(1970, 1, 1, 0, 0, 0);
			time = time.AddSeconds(second);
			time = System.TimeZone.CurrentTimeZone.ToLocalTime(time);
			return (DateTime.Now - time).TotalDays;
		}

		//-------------------------------------------------------------
		// 概要：スレッド勢いの取得
		//-------------------------------------------------------------
		public static float GetThreadSpeed(double days, string resCount)
		{
			// 勢いの計算
			int count;
			if (!int.TryParse(resCount, out count)) { return -1; }
			return (float)(count / days);
		}
	}
}
