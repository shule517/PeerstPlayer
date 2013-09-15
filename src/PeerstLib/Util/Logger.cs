using System.Diagnostics;
using System.Reflection;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace PeerstLib.Util
{
	//-------------------------------------------------------------
	// 概要：ロガークラス
	//-------------------------------------------------------------
	public static class Logger
	{
		//-------------------------------------------------------------
		// 概要：プログラムIDの設定
		//-------------------------------------------------------------
		static Logger()
		{
			GlobalContext.Properties["pid"] = Process.GetCurrentProcess().Id;
		}

		//-------------------------------------------------------------
		// 概要：ロガーの取得
		//-------------------------------------------------------------
		public static ILog Instance
		{
			get
			{
				const int callerFrameIndex = 1;
				StackFrame callerFrame = new StackFrame(callerFrameIndex);
				MethodBase callerMethod = callerFrame.GetMethod();
				return LogManager.GetLogger(callerMethod.DeclaringType);
			}
		}

		//-------------------------------------------------------------
		// 概要：ログ出力レベルの変更
		//-------------------------------------------------------------
		public static void ChangeLogLevel(Level level)
		{
			var rep = LogManager.GetRepository() as Hierarchy;
			rep.Threshold = level;
		}
	}
}
