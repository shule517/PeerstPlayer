using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeerstLib.Bbs;
using PeerstLib.Bbs.Strategy;
using PeerstLib.Bbs.Util;

namespace TestPeerstLib.Bbs.Strategy
{
	[TestClass]
	public class StrategyTest
	{
		//-------------------------------------------------------------
		// 確認：指定URLに対応したストラテジを生成しているか
		//-------------------------------------------------------------
		[TestMethod]
		public void BbsStrategy_CheckWriteError()
		{
			// したらば
			TestCheckWriteError(true, したらば_書きこみました);
			TestCheckWriteError(false, したらば_本文がありません);
			TestCheckWriteError(false, したらば_多重書き込みです);
			TestCheckWriteError(false, したらば_該当スレッドは存在しません);
			TestCheckWriteError(false, したらば_ユーザー設定が消失しています);
			TestCheckWriteError(false, したらば_スレッドストップです);

			/*
			TestCheckWriteError(BbsStrategy.WriteStatus.NothingMessage,		WriteRes本文がありません);
			TestCheckWriteError(BbsStrategy.WriteStatus.MultiWriteError,	WriteRes多重書き込みです);
			TestCheckWriteError(BbsStrategy.WriteStatus.Posted,				WriteRes書きこみました);
			TestCheckWriteError(BbsStrategy.WriteStatus.NothingThreadError,	WriteRes該当スレッドは存在しません);
			TestCheckWriteError(BbsStrategy.WriteStatus.LostUserInfoError,	WriteResユーザー設定が消失しています);
			TestCheckWriteError(BbsStrategy.WriteStatus.ThreadStopError,	WriteResスレッドストップです);
			 */
		}

		private static void TestCheckWriteError(bool result, string response)
		{
			try
			{
				BbsUtil.CheckWriteError(response);
			}
			catch
			{
				Assert.AreEqual(false, result);
				return;
			}

			// 書き込みステータスのチェック
			Assert.AreEqual(true, result);
		}

		#region テストデータ_したらば

		#region したらば_本文がありません

		private string したらば_本文がありません = @"<html>
<head>
<title>ERROR!</title>

</head>
<body bgcolor=""#FFFFFF""><!-- 2ch_X:error -->
<table width=""100%"" border=""1"" cellspacing=""0"" cellpadding=""10"">
<tr><td><b>ERROR!<br><br>本文がありません</b></td></tr>
</table>
<hr size=1>
<div align=""right""><a href=""http://rentalbbs.livedoor.com/"">したらば掲示板 (無料レンタル)</a></div>
</body>
</html>
";
		#endregion

		#region したらば_多重書き込みです

		private string したらば_多重書き込みです = @"<html>
<head>
<title>ERROR!!</title>

</head>
<body bgcolor=""#FFFFFF""><!-- 2ch_X:error -->
<table width=""100%"" border=""1"" cellspacing=""0"" cellpadding=""10"">
<tr><td><b>ERROR!!<br><br>多重書き込みです。 あと 12秒お待ちください。</b></td></tr>
</table>
<hr size=1>
<div align=""right""><a href=""http://rentalbbs.livedoor.com/"">したらば掲示板 (無料レンタル)</a></div>
</body>
</html>
";
		#endregion

		#region したらば_書きこみました

		private string したらば_書きこみました = @"<html lang=""ja"">
<head>
<title>書きこみました。</title>
<meta http-equiv=""content-type"" content=""text/html; charset=euc-jp"">
<meta http-equiv=""refresh"" content=""10;url=/game/45037/"">
<meta http-equiv=""Pragma"" content=""no-cache"">
<meta http-equiv=""cache-control"" content=""no-cache"">
<script type=""text/javascript"">
<!--
//cookiedayosetcookiedayohontodane
function set_cookie(name, mail) {
 var date = new Date;
 date.setMonth(date.getMonth() + 1); // + one month
 var cf = ';expires=' + date.toGMTString() + '; path=/';
 document.cookie = 'NAME=' + escape(name) + cf;
 document.cookie = 'MAIL=' + escape(mail) + cf;
}
// -->
</script>
</head>
<body bgcolor=""#ffffff"" onLoad=""set_cookie('シュール','sage')"">
書きこみが終りました。<br><br>
画面を切り替えるまでしばらくお待ち下さい。<br><br>
<br>
<a href=""/game/45037/"">掲示板に戻る</a>
<br>
<script type=""text/javascript"" language=""JavaScript"">
<!--
google_ad_client = 'ca-livedoor-banner_html';
google_ad_channel = 'banner_shitaraba2';
google_ad_width = 336;
google_ad_height = 280;
google_ad_format = '336x280_as';
google_ad_type = 'image';
google_language = 'ja';
google_encoding = 'euc-jp';
google_safe = 'high';
google_color_bg = 'FFFFFF';
google_color_border = 'DDDDDD';
google_color_line = 'DDDDDD';
google_color_link = '0033cc';
if(document.referrer){ google_page_url = document.referrer }
// -->
</script>
<hr size=""1"">
<div align=""center"">
<!-- script type=""text/javascript"" language=""JavaScript"" src=""http://pagead2.googlesyndication.com/pagead/show_ads.js""></script -->
</div>

</body>
</html>
";
		#endregion

		#region したらば_該当スレッドは存在しません

		private string したらば_該当スレッドは存在しません = @"<html>
<head>
<title>ERROR!!</title>

</head>
<body bgcolor=""#FFFFFF""><!-- 2ch_X:error -->
<table width=""100%"" border=""1"" cellspacing=""0"" cellpadding=""10"">
<tr><td><b>ERROR!!<br><br>該当スレッドは存在しません。</b></td></tr>
</table>
<hr size=1>
<div align=""right""><a href=""http://rentalbbs.livedoor.com/"">したらば掲示板 (無料レンタル)</a></div>
</body>
</html>
";
		#endregion

		#region したらば_ユーザー設定が消失しています

		private string したらば_ユーザー設定が消失しています = @"<html>
<head>
<title>ERROR!!</title>

</head>
<body bgcolor=""#FFFFFF""><!-- 2ch_X:error -->
<table width=""100%"" border=""1"" cellspacing=""0"" cellpadding=""10"">
<tr><td><b>ERROR!!<br><br>ユーザー設定が消失しています</b></td></tr>
</table>
<hr size=1>
<div align=""right""><a href=""http://rentalbbs.livedoor.com/"">したらば掲示板 (無料レンタル)</a></div>
</body>
</html>
";
		#endregion

		#region したらば_スレッドストップです

		private string したらば_スレッドストップです = @"<html>
<head>
<title>ERROR!!</title>

</head>
<body bgcolor=""#FFFFFF""><!-- 2ch_X:error -->
<table width=""100%"" border=""1"" cellspacing=""0"" cellpadding=""10"">
<tr><td><b>ERROR!!<br><br>投稿数 1000 ： スレッドストップです</b></td></tr>
</table>
<hr size=1>
<div align=""right""><a href=""http://rentalbbs.livedoor.com/"">したらば掲示板 (無料レンタル)</a></div>
</body>
</html>
";
		#endregion

		#endregion
	}
}
