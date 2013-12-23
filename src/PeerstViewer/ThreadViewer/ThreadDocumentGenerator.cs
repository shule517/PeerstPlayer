
using PeerstLib.Bbs.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace PeerstViewer.ThreadViewer
{
	/// <summary>
	/// スレッドドキュメントの生成クラス
	/// </summary>
	class ThreadDocumentGenerator
	{
		private const string FooterFileName = "Footer.html";
		private const string HeaderFileName = "Header.html";
		private const string NewResFileName = "NewRes.html";
		private const string PopupResFileName = "PopupRes.html";
		private const string ResFileName = "Res.html";

		private string FooterText = "Footer.html";
		private string HeaderText = "Header.html";
		private string NewResText = "NewRes.html";
		private string PopupResText = "PopupRes.html";
		private string ResText = "Res.html";

		/// <summary>
		/// スキンのフォルダパス
		/// </summary>
		private string skinFolderPath = "";

		public ThreadDocumentGenerator(string skinFolderPath)
		{
			this.skinFolderPath = skinFolderPath;
			ReadSkin(skinFolderPath);
		}

		/// <summary>
		/// スキンファイルの読み込み
		/// </summary>
		public void ReadSkin(string skinFolderPath)
		{
			FooterText = ReadFile(Path.Combine(skinFolderPath, FooterFileName));
			HeaderText = ReadFile(Path.Combine(skinFolderPath, HeaderFileName));
			NewResText = ReadFile(Path.Combine(skinFolderPath, NewResFileName));
			PopupResText = ReadFile(Path.Combine(skinFolderPath, PopupResFileName));
			ResText = ReadFile(Path.Combine(skinFolderPath, ResFileName));
		}

		/// <summary>
		/// ファイル読み込み
		/// </summary>
		private string ReadFile(string filePath)
		{
			string text;
			StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("Shift_JIS"));
			text = sr.ReadToEnd();
			sr.Close();
			return text;
		}

		/// <summary>
		/// ドキュメント生成
		/// </summary>
		public string Generate(List<ResInfo> resList, string threadUrl, string threadName)
		{
			string documentText = string.Empty;

			// ヘッダー追加
			documentText += ReplaceData(HeaderText, resList, threadUrl, threadName);

			foreach (var res in resList)
			{
				documentText += ResText
					.Replace("<NUMBER/>", res.ResNo)
					.Replace("<PLAINNUMBER/>", res.ResNo)
					.Replace("<NAME/>", res.Name)
					.Replace("<MAILNAME/>", string.Format("{0} : {1}", res.Mail, res.Name))
					.Replace("<MAIL/>", res.Mail)
					.Replace("<DATE/>", res.Date)
					.Replace("<MESSAGE/>", res.Message)
					.Replace("<THREADNAME/>", threadName)
					.Replace("<THREADURL/>", threadUrl)
					.Replace("<SKINPATH/>", skinFolderPath)
					.Replace("<GETRESCOUNT/>", 0.ToString())
					.Replace("<NEWRESCOUNT/>", resList.Count.ToString())
					.Replace("<ALLRESCOUNT/>", resList.Count.ToString());
			}

			// フッター追加
			documentText += ReplaceData(FooterText, resList, threadUrl, threadName);

			return documentText;
		}

		private string ReplaceData(string text, List<ResInfo> resList, string threadUrl, string threadName)
		{
			return text
				.Replace("<THREADNAME/>", threadName)
				.Replace("<THREADURL/>", threadUrl)
				.Replace("<SKINPATH/>", skinFolderPath)
				.Replace("<GETRESCOUNT/>", 0.ToString())
				.Replace("<NEWRESCOUNT/>", resList.Count.ToString())
				.Replace("<ALLRESCOUNT/>", resList.Count.ToString());
		}
	}
}
