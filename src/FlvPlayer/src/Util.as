package 
{
	import flash.events.Event;
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	/**
	 * ...
	 * @author 
	 */
	public class Util 
	{
		public static function GetChannelId(streamUrl:String):String
		{
			var split:Array = streamUrl.split("/");
			var reg:RegExp = /^(\w+)/;
			var result:Object = reg.exec(split[4]);
			return result[1];
		}
		
		public static function GetPeerCastXml(streamUrl:String, callback:Function):void
		{
			var reg:RegExp = /^http:\/\/.+?\//;
			var result:Object = reg.exec(streamUrl);
			var url:String = result[0] + "admin?cmd=viewxml";
			
			var urlRequest:URLRequest = new URLRequest(url);
			var urlLoader:URLLoader = new URLLoader();
			urlLoader.addEventListener(Event.COMPLETE, function (event:Event):void {
				callback(urlLoader.data);
			});
			urlLoader.load(urlRequest);
		}
	}

}