
using PeerstLib.Bbs;
using PeerstLib.Bbs.Data;
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

		private string FooterText = string.Empty;
		private string HeaderText = string.Empty;
		private string NewResText = string.Empty;
		private string PopupResText = string.Empty;
		private string ResText = string.Empty;

		/// <summary>
		/// スキンのフォルダパス
		/// </summary>
		private string skinFolderPath = string.Empty;

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
		public string Generate(OperationBbs operationBbs)
		{
			// ヘッダー追加
			string documentText = ReplaceData(HeaderText, operationBbs);

			foreach (var res in operationBbs.ResList)
			{
				string resText = ReplaceResData(ResText, res);
				documentText += ReplaceData(resText, operationBbs);
			}

			// フッター追加
			documentText += ReplaceData(FooterText, operationBbs);

			return documentText;
		}

		/// <summary>
		/// レスデータの置換
		/// </summary>
		private static string ReplaceResData(string documentText, ResInfo res)
		{
			return documentText
				.Replace("<NUMBER/>", res.ResNo)
				.Replace("<PLAINNUMBER/>", res.ResNo)
				.Replace("<NAME/>", res.Name)
				.Replace("<MAILNAME/>", string.Format("{0} : {1}", res.Mail, res.Name))
				.Replace("<MAIL/>", res.Mail)
				.Replace("<DATE/>", res.Date)
				.Replace("<MESSAGE/>", res.Message);
		}

		/// <summary>
		/// 特殊文字列の置換
		/// </summary>
		private string ReplaceData(string documentText, OperationBbs operationBbs)
		{
			return documentText
				.Replace("<THREADNAME/>", operationBbs.SelectThread.ThreadTitle)
				.Replace("<THREADURL/>", operationBbs.ThreadUrl)
				.Replace("<SKINPATH/>", skinFolderPath)
				.Replace("<GETRESCOUNT/>", 0.ToString())
				.Replace("<NEWRESCOUNT/>", operationBbs.ResList.Count.ToString())
				.Replace("<ALLRESCOUNT/>", operationBbs.ResList.Count.ToString())
				.Replace("<BBSNAME/>", operationBbs.BbsInfo.BbsName)
				.Replace("<BOARDNAME/>", operationBbs.BbsInfo.BbsName)
				.Replace("<BOARDURL/>", operationBbs.BoardUrl);
		}
	}
}
