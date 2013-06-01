using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using log4net.Core;

namespace PeerstLib.Utility
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
