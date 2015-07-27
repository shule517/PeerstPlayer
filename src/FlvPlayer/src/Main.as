package 
{
	import flash.display.Sprite;
	import flash.display.StageAlign;
	import flash.display.StageScaleMode;
	import flash.events.Event;
	import flash.external.ExternalInterface;

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
		
		/**
		 * 初期化メソッド
		 * @param	e
		 */
		private function init(e:Event = null):void 
		{
			Logger.Trace("Main.init()");
			
			// FLVプレイヤー
			var flvPlayer:FlvPlayer = new FlvPlayer(stage);
			removeEventListener(Event.ADDED_TO_STAGE, init);

			// 動画再生情報
			var panel:MovieInfoPanel = new MovieInfoPanel(stage, flvPlayer);
			
			// Flashコマンド
			var flashCommand:FlashCommand = new FlashCommand(flvPlayer);
			
			// ステージの拡大縮小を無効にする
			stage.scaleMode = StageScaleMode.NO_SCALE;
			// 表示基準位置を左上に設定
			stage.align = StageAlign.TOP_LEFT;

			// 外部インタフェースの追加
			if (ExternalInterface.available) {
				ExternalInterface.addCallback("PlayVideo", flashCommand.PlayVideo);
				ExternalInterface.addCallback("SizeChanged", flashCommand.SizeChanged);
				ExternalInterface.addCallback("ChangeVolume", flashCommand.ChangeVolume);
				ExternalInterface.addCallback("ChangePan", flashCommand.ChangePan);
				ExternalInterface.addCallback("GetVideoWidth", flashCommand.GetVideoWidth);
				ExternalInterface.addCallback("GetVideoHeight", flashCommand.GetVideoHeight);
				ExternalInterface.addCallback("GetDurationString", flashCommand.GetDurationString);
				ExternalInterface.addCallback("GetNowFrameRate", flashCommand.GetNowFrameRate);
				ExternalInterface.addCallback("GetFrameRate", flashCommand.GetFrameRate);
				ExternalInterface.addCallback("GetNowBitRate", flashCommand.GetNowBitRate);
				ExternalInterface.addCallback("GetBitRate", flashCommand.GetBitRate);
				ExternalInterface.addCallback("EnableGpu", flashCommand.EnableGpu);
				ExternalInterface.addCallback("EnableRtmp", flashCommand.EnableRtmp);
				ExternalInterface.addCallback("SetBufferTime", flashCommand.SetBufferTime);
				ExternalInterface.addCallback("SetBufferTimeMax", flashCommand.SetBufferTimeMax);
				ExternalInterface.addCallback("ShowDebug", panel.ShowDebug);
				ExternalInterface.call("Initialized");
			}
		}
	}
}
