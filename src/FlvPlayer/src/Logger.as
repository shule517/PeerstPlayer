package 
{
	/**
	 * ...
	 * @author ...
	 */
	public class Logger
	{
		private static var message:String = "";
		
		// DEBUGを表示する場合はここをtrueにしてね！
		private static var isDebug:Boolean = false;
		
		public static function IsDebug():Boolean
		{
			return Logger.isDebug;
		}
		
		public static function Trace(message:String):void
		{
			Logger.message = message + "\n" + Logger.message;
			trace(message);
			CSharpCommand.OutputLog(message);
		}
		
		public static function GetMessage():String
		{
			return message;
		}
	}
}