
using PeerstLib.Net;
using System;
using System.Text;
namespace PeerstLib.Bbs.Net
{
	/// <summary>
	/// したらば通信クラス
	/// </summary>
	public static class ShitarabaConnection
	{
		/// <summary>
		/// したらば掲示板のホスト
		/// </summary>
		const string Host = "http://jbbs.shitaraba.net/";

		/// <summary>
		/// スレッド一覧情報
		/// </summary>
		const string Subjects = "subject.txt";

		/// <summary>
		/// 文字エンコード
		/// </summary>
		static Encoding Encoding = Encoding.GetEncoding("EUC-JP");

		/// <summary>
		/// スレッド一覧情報(subjects)を取得
		/// </summary>
		/// <param name="category">カテゴリ</param>
		/// <param name="boardNo">板番号</param>
		/// <returns>リクエスト結果</returns>
		public static RequestResult GetSubjects(string category, string boardNo)
		{
			string url = Host + category + "/" + boardNo + "/" + Subjects;
			return HttpConnection.GetRequest(url, new DateTime(0), Encoding);
		}

		/// <summary>
		/// スレッド一覧情報(subjects)を差分取得
		/// </summary>
		/// <param name="category">カテゴリ</param>
		/// <param name="boardNo">板番号</param>
		/// <param name="modifiedSince">最終更新日時</param>
		/// <returns>リクエスト結果</returns>
		public static RequestResult GetSubjectsDiff(string category, string boardNo, DateTime modifiedSince)
		{
			string url = Host + category + "/" + boardNo + "/" + Subjects;
			return HttpConnection.GetRequest(url, modifiedSince, Encoding);
		}

		public static string GetDat(string category, string boardNo, string threadNo)
		{
			return string.Empty;
		}

		public static string GetDatDiff(string category, string boardNo, string threadNo, string resNo)
		{
			return string.Empty;
		}

		public static string GetLocalRule(string category, string boardNo)
		{
			return string.Empty;
		}

		public static string WriteRes(string category, string boardNo, string threadNo, string name, string mail, string message)
		{
			return string.Empty;
		}

		public static string CreateThread(string category, string boardNo, string threadTitle, string name, string mail, string message)
		{
			return string.Empty;
		}
	}
}
