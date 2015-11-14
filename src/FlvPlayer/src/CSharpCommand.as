package 
{
	import flash.external.ExternalInterface;

	/**
	 * ...
	 * @author ...
	 */
	public class CSharpCommand 
	{
		// C#側を呼び出すコマンド
		private static var commandOpenStateChange:String = "OpenStateChange";
		private static var commandRequestBump:String = "RequestBump";
		private static var commandOutputLog:String = "OutputLog";

		// 再生状態の変更イベント
		public static function RaiseOpenStateChange():void
		{
			Call(commandOpenStateChange);
		}

		// BUMPを行う
		public static function RequestBump():void
		{
			Call(commandRequestBump);
		}

		// ログ出力
		public static function OutputLog(message:String):void
		{
			Call(commandOutputLog, message);
		}

		// C#メソッドの呼び出し
		private static function Call(functionName:String, ...args):void
		{
			if (ExternalInterface.available) {
				ExternalInterface.call.apply(null, [functionName].concat(args));
			}
		}
	}
}
