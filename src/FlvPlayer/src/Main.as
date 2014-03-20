package 
{
	import flash.events.TimerEvent;
	import flash.display.Sprite;
	import flash.display.StageAlign;
	import flash.display.StageScaleMode;
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.events.NetStatusEvent;
	import flash.media.Video;
	import flash.net.NetConnection;
	import flash.net.NetStream;
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.media.SoundTransform;
	import flash.external.ExternalInterface;
	import flash.system.fscommand;
	import flash.utils.Timer;

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
		
		private var video:Video = new Video();
		private var netStr:NetStream = null;
		private var streamUrl:String = null;
		
		// 前回の時刻など
		private var prevTime:Number = 0;
		private var prevBytesLoaded:uint = 0;
		private var prevBitrate:int = 0;
		
		// 再接続監視タイマー
		private var retryTimer:Timer = null;
		private var volume:String = null;
		private var retryPrevTime:Number = 0;
		
		private function Call(functionName:String, ...args):void
		{
			if (ExternalInterface.available)
			{
				ExternalInterface.call.apply(this, [functionName].concat(args));
			}
		}

		// 音量変更
		private function ChangeVolume(vol:String):void
		{
			if (netStr == null)
			{
				return;
			}
			
			this.volume = vol;
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
		
		// サイズ変更
		private function SizeChanged(width:int, height:int):void
		{
			var w:Number = stage.stageWidth / video.videoWidth;
			var h:Number = stage.stageHeight / video.videoHeight;
			// 横の方が長い
			if (w > h)
			{
				video.width = stage.stageHeight * video.videoWidth / video.videoHeight;
				video.height = stage.stageHeight;
			}
			// 縦の方が長い
			else
			{
				video.width = stage.stageWidth;
				video.height = stage.stageWidth * video.videoHeight / video.videoWidth;
			}
			video.x = stage.stageWidth / 2 - video.width / 2;
			video.y = stage.stageHeight / 2 - video.height / 2;
		}
		
		// 動画幅取得
		private function GetVideoWidth():String
		{
			return video.videoWidth.toString();
		}
		
		// 動画高さ取得
		private function GetVideoHeight():String
		{
			return video.videoHeight.toString();
		}
		
		// 再生時間取得
		private function GetDurationString():String
		{
			if (netStr == null)
			{
				return "00:00:00";
			}
			
			var sec:String = new String(Math.floor(netStr.time % 60));
			var min:String = new String(Math.floor(netStr.time /60 % 60));
			var hour:String = new String(int(netStr.time / 60 / 60));
			if (hour.length <= 1)
			{
				hour = "0" + hour;
			}
			return hour + ":" +
				("0" + min.toString()).slice(-2) + ":" +
				("0" + sec.toString()).slice(-2);
		}
		
		// FPS取得
		private function GetNowFrameRate():String
		{
			if (netStr == null)
			{
				return "0";
			}
			return int(netStr.currentFPS).toString();
		}
		
		// FPS取得
		private function GetFrameRate():String
		{
			if (netStr == null || netStr.info.metaData == null)
			{
				return "0";
			}
			return netStr.info.metaData["framerate"].toString();
		}
		
		// ビットレート取得
		private function GetNowBitRate():String
		{
			if (netStr == null)
			{
				return "0";
			}
			var diffBytes:uint = netStr.bytesLoaded - prevBytesLoaded;
			var diffTime:Number = netStr.time - prevTime;
			var bitrate:int = int(diffBytes / diffTime * 8 / 1000);
			// 0bpsになることが少なくないので、前回との平均を取ってみる
			var averageBitrate:String = String(Math.floor((bitrate + prevBitrate) / 2));
			prevBytesLoaded = netStr.bytesLoaded;
			prevTime = netStr.time;
			prevBitrate = bitrate;
			return averageBitrate;
		}
		
		// ビットレート取得
		private function GetBitRate():String
		{
			if (netStr == null || netStr.info.metaData == null)
			{
				return "0";
			}
			return String(netStr.info.metaData["audiodatarate"] + netStr.info.metaData["videodatarate"]);
		}

		// 動画再生
		private function PlayVideo(streamUrl:String):void
		{
			this.streamUrl = streamUrl;
			// プレイリストURLをコマンドライン引数から取得
			var playlistUrl:String = streamUrl;// "http://localhost:7145/pls/8519AAD80ED5D528069071AEE412E3B5?tip=202.122.25.235:7146";
			var urlRequest:URLRequest = new URLRequest(playlistUrl);
			var urlLoader:URLLoader = new URLLoader;
			
			// 動画に接続するための変数
			var netCon:NetConnection = new NetConnection;
			netCon.connect(null);
			netStr = new NetStream(netCon);
			// 非アクティブかつプレイヤーが他のウィンドウの裏に隠れている場合に
			// 短時間バッファが発生して再生が遅れていく現象が緩和できるかも
			netStr.bufferTime = 0.2;

			// サウンドを制御するための変数
			// var stf:soundTransform = netStr.soundTransform;
			// var mute:soundTransform = netStr.soundTransform;
			// mute.volume = 0;

			netStr.client = new Object;

			// メタ情報を取得した時の処理
			netStr.client.onMetaData = function(obj:Object):void {
				// stageWidth, stageHeightはここから変えられない　　はず
				// stage.stageWidth = obj.width;
				// stage.stageHeight = obj.height;
				
				prevTime = netStr.time;
				prevBytesLoaded = netStr.bytesLoaded;
				prevBitrate = netStr.info.metaData["audiodatarate"] + netStr.info.metaData["videodatarate"];
				
				// 再接続時に音量が初期化されるので、一度変更済みであればここ変えておく
				if (volume != null) {
					ChangeVolume(volume);
				}
				
				Call("OpenStateChange");
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
				if (video.parent == null)
				{
					stage.addChild(video);
				}

				video.attachNetStream(netStr);
				video.visible = true;
			});
			urlLoader.load(urlRequest);
			netStr.addEventListener(NetStatusEvent.NET_STATUS, netStatusHandler);
			
			if (!retryTimer.running)
			{
				retryTimer.start();
			}
		}
		
		/**
		 * 初期化メソッド
		 * @param	e
		 */
		private function init(e:Event = null):void 
		{
			removeEventListener(Event.ADDED_TO_STAGE, init);
			// entry point
			
			// 外部インタフェースの追加
            if (ExternalInterface.available) {
                ExternalInterface.addCallback("PlayVideo",PlayVideo);
                ExternalInterface.addCallback("SizeChanged",SizeChanged);
                ExternalInterface.addCallback("ChangeVolume", ChangeVolume);
				ExternalInterface.addCallback("GetVideoWidth", GetVideoWidth);
				ExternalInterface.addCallback("GetVideoHeight", GetVideoHeight);
				ExternalInterface.addCallback("GetDurationString", GetDurationString);
				ExternalInterface.addCallback("GetDurationString", GetDurationString);
				ExternalInterface.addCallback("GetNowFrameRate", GetNowFrameRate);
				ExternalInterface.addCallback("GetFrameRate", GetFrameRate);
				ExternalInterface.addCallback("GetNowBitRate", GetNowBitRate);
				ExternalInterface.addCallback("GetBitRate", GetBitRate);
			}
			
			//　キー入力の判定
            stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDownHandler);
			
			// リサイズされたときに呼び出されるイベント
			stage.addEventListener(Event.RESIZE, function (e:Event):void{
				SizeChanged(stage.stageWidth, stage.stageHeight);
			});
			
			// ステージの拡大縮小を無効にする
			stage.scaleMode = StageScaleMode.NO_SCALE;
			// 表示基準位置を左上に設定
			stage.align = StageAlign.TOP_LEFT;
			
			// 監視用タイマー
			retryTimer = new Timer(2000);
			retryTimer.addEventListener(TimerEvent.TIMER, retryTimerHandler);
		}
		
		// ネットステータス
		private function netStatusHandler(event:NetStatusEvent):void {
			switch (event.info.code)
			{
				case "NetStream.Buffer.Full":
					break;
				case "NetStream.Buffer.Empty":
					break;
				case "NetStream.Buffer.Flush":
					break;
				case "NetStream.Play.Start":
					break;
				case "NetStream.Play.Stop":
					break;
				case "NetStream.Play.StreamNotFound":
					break;
			}
		}
		
		// タイマー
		private function retryTimerHandler(event:TimerEvent):void {
			// NetStream.Buffer.Empty発動時からタイムが進んでいなければリコネクト
			if (retryPrevTime == netStr.time)
			{
				netStr.close();
				PlayVideo(streamUrl);
			}
			else
			{
				retryPrevTime = netStr.time;
			}
		}
		
        // キー押下イベント
        private function keyDownHandler(event:KeyboardEvent):void {
            trace("keyDownHandler:" + event.keyCode.toString() + "," + (event.shiftKey ? "1":"0"));
			fscommand("KeyDownEvent", event.keyCode.toString() + "," + (event.shiftKey ? "1":"0"));
        }
	}
}