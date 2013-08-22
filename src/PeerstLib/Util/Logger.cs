using System.Diagnostics;
using System.Reflection;
using log4net;

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
	}
}
