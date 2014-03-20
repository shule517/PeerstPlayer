package 
{
	import flash.display.Sprite;
	import flash.display.StageAlign;
	import flash.display.StageScaleMode;
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.external.ExternalInterface;
	import flash.system.fscommand;

	/**
	 * FLV　Player
	 * @author shule517
	 */
	[SWF(width = "500", height = "500", backgroundColor = "#000000")]
	public class Main extends Sprite 
	{
		public function Main():void 
		{
			if (stage) init();
			else addEventListener(Event.ADDED_TO_STAGE, init);
		}
		
		private var flvPlayer:FlvPlayer;
		
		/**
		 * 初期化メソッド
		 * @param	e
		 */
		private function init(e:Event = null):void 
		{
			flvPlayer = new FlvPlayer(stage);
			removeEventListener(Event.ADDED_TO_STAGE, init);

			//　キー入力の判定
			stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDownHandler);

			// ステージの拡大縮小を無効にする
			stage.scaleMode = StageScaleMode.NO_SCALE;
			// 表示基準位置を左上に設定
			stage.align = StageAlign.TOP_LEFT;

			// 外部インタフェースの追加
            if (ExternalInterface.available) {
                ExternalInterface.addCallback("PlayVideo", flvPlayer.PlayVideo);
                ExternalInterface.addCallback("SizeChanged", flvPlayer.SizeChanged);
                ExternalInterface.addCallback("ChangeVolume", flvPlayer.ChangeVolume);
				ExternalInterface.addCallback("GetVideoWidth", flvPlayer.GetVideoWidth);
				ExternalInterface.addCallback("GetVideoHeight", flvPlayer.GetVideoHeight);
				ExternalInterface.addCallback("GetDurationString", flvPlayer.GetDurationString);
				ExternalInterface.addCallback("GetDurationString", flvPlayer.GetDurationString);
				ExternalInterface.addCallback("GetNowFrameRate", flvPlayer.GetNowFrameRate);
				ExternalInterface.addCallback("GetFrameRate", flvPlayer.GetFrameRate);
				ExternalInterface.addCallback("GetNowBitRate", flvPlayer.GetNowBitRate);
				ExternalInterface.addCallback("GetBitRate", flvPlayer.GetBitRate);
			}
		}
		
		// キー押下イベント
        private function keyDownHandler(event:KeyboardEvent):void {
            trace("keyDownHandler:" + event.keyCode.toString() + "," + (event.shiftKey ? "1":"0"));
			fscommand("KeyDownEvent", event.keyCode.toString() + "," + (event.shiftKey ? "1":"0"));
        }
	}
}
