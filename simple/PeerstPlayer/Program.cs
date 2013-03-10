using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PeerstPlayer
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			// 設定
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Formの初期化
			PlayerForm form = new PlayerForm();

			// TODO 設定ファイルの読み込み

			// Formの表示
			Application.Run(form);

			// TODO 終了処理
		}
	}
}
