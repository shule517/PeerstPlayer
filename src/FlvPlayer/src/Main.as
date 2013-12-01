package 
{
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.text.StyleSheet;
	import flash.text.TextField;
	import flash.text.TextFieldAutoSize;
	import flash.text.TextFormat;
	import flash.utils.getTimer;
	import flash.display.Sprite;
	import flash.display.StageAlign;
	import flash.display.StageDisplayState;
	import flash.display.StageQuality;
	import flash.display.StageScaleMode;
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.events.NetStatusEvent;
	import flash.geom.Rectangle;
	import flash.media.SoundTransform;
	import flash.media.Video;
	import flash.net.NetConnection;
	import flash.net.NetStream;
	import flash.net.SharedObject;
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.ui.Keyboard;
	import flash.utils.clearInterval;
	import flash.utils.setInterval;
	import flash.media.SoundTransform;
	import flash.external.ExternalInterface;
	import flash.system.fscommand;

	/**
	 * FPS 表示テスト
	 * @author Hikipuro
	 */
	[SWF(width = "500", height = "500", backgroundColor = "#000000")]
	public class Main extends Sprite 
	{
		/**
		 * FPS 計測用
		 */
		private var fps:int = 0;
		
		/**
		 * 1 秒計測用
		 */
		private var time:int = 0;
		
		/**
		 * FPS 表示用
		 */
		private var textField:TextField;
		private var textField2:TextField;
		
		/**
		 * コンストラクタ
		 */
		public function Main():void 
		{
			if (stage) init();
			else addEventListener(Event.ADDED_TO_STAGE, init);
		}
		
		private var video:Video = new Video();
		private var netStr:NetStream = null;

		//C#からのデータ受信
		private function ChangeVolume(vol:String):void
		{
			if (netStr == null)
			{
				return;
			}
			
			var volume:Number = parseInt(vol);
			
			if (volume <= 0)
			{
				netStr.soundTransform = new SoundTransform(0);
			}
			else if (volume >= 100)
			{
				netStr.soundTransform = new SoundTransform(1);
			}
			else
			{
				netStr.soundTransform = new SoundTransform(volume / 100.0);
			}
		}
		
		//C#からのデータ受信
        private function SizeChanged(width:int, height:int):void
		{
			video.width = stage.stageWidth;
			video.height = stage.stageHeight;
		}

		//C#からのデータ受信
        private function PlayVideo(streamUrl:String):void
		{
			// プレイリストURLをコマンドライン引数から取得
			var playlistUrl:String = streamUrl;// "http://localhost:7145/pls/8519AAD80ED5D528069071AEE412E3B5?tip=202.122.25.235:7146";
			var urlRequest:URLRequest = new URLRequest(playlistUrl);
			var urlLoader:URLLoader = new URLLoader;
			
			// 動画に接続するための変数
			var netCon:NetConnection = new NetConnection;
			netCon.connect(null);
			netStr = new NetStream(netCon);

			// サウンドを制御するための変数
			//var stf:soundTransform = netStr.soundTransform;
			//var mute:soundTransform = netStr.soundTransform;
			//mute.volume = 0;

			netStr.client = new Object;

			// メタ情報を取得した時の処理
			netStr.client.onMetaData = function(obj:Object):void {
				stage.stageWidth = obj.width;
				stage.stageHeight = obj.height;
			}
			
			urlLoader.addEventListener(Event.COMPLETE, function (event:Event):void {
				// ストリームURLを取得
				var streamUrl:String = urlLoader.data;

				// 動画を再生
				netStr.play(streamUrl);

				// Videoをステージサイズにする
				//video = new Video(stage.stageWidth, stage.stageHeight);
				video.width = stage.stageWidth;
				video.height = stage.stageHeight;

				// smoothingを有効にする
				video.smoothing = true;

				// videoをステージに追加
				stage.addChild(video);

				video.attachNetStream(netStr);
				video.visible = true;
			});
			urlLoader.load(urlRequest);
		}
		
		/**
		 * 初期化メソッド
		 * @param	e
		 */
		private function init(e:Event = null):void 
		{
			removeEventListener(Event.ADDED_TO_STAGE, init);
			// entry point
			
			//外部インタフェースの追加
            if (ExternalInterface.available) {
                ExternalInterface.addCallback("PlayVideo",PlayVideo);
                ExternalInterface.addCallback("SizeChanged",SizeChanged);
                ExternalInterface.addCallback("ChangeVolume",ChangeVolume);
			}
			
			// ダブルクリックを有効
			stage.doubleClickEnabled = true;
			
			//マウスイベントリスナー
            stage.addEventListener(MouseEvent.CLICK, clickHandler);
            stage.addEventListener(MouseEvent.MOUSE_DOWN, mouseDownHandler);
            stage.addEventListener(MouseEvent.MOUSE_UP, mouseUpHandler);
            stage.addEventListener(MouseEvent.MOUSE_MOVE, mouseMoveHandler);
            stage.addEventListener(MouseEvent.DOUBLE_CLICK, doubleClickHandler);
            stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDownHandler);
		}
		
		//クリック
        private function clickHandler(event:MouseEvent):void {
            trace("clickHandler");
			fscommand("MouseClickEvent", (int)(event.localX).toString() + "," + (int)(event.localY).toString());
        }
        //マウスボタンを押した瞬間
        private function mouseDownHandler(event:MouseEvent):void {
            trace("mouseDownHandler");
			fscommand("MouseDownEvent", (int)(event.localX).toString() + "," + (int)(event.localY).toString());
        }
        //マウスボタンを離した瞬間
        private function mouseUpHandler(event:MouseEvent):void {
            trace("mouseUpHandler");
			fscommand("MouseUpEvent", (int)(event.localX).toString() + "," + (int)(event.localY).toString());
        }
        //マウスボタンを動かした瞬間
        private function mouseMoveHandler(event:MouseEvent):void {
            trace("mouseMoveHandler");
			fscommand("MouseMoveEvent", (int)(event.localX).toString() + "," + (int)(event.localY).toString());
        }
        //ダブルクリックイベント
        private function doubleClickHandler(event:MouseEvent):void {
            trace("doubleClickHandler:" + (int)(event.localX).toString() + "," + (int)(event.localY).toString());
			fscommand("DoubleClickEvent", (int)(event.localX).toString() + "," + (int)(event.localY).toString());
        }
        //キー押下イベント
        private function keyDownHandler(event:KeyboardEvent):void {
            trace("keyDownHandler:" + event.keyCode.toString() + "," + (event.shiftKey ? "1":"0"));
			fscommand("KeyDownEvent", event.keyCode.toString() + "," + (event.shiftKey ? "1":"0"));
        }
	}
}