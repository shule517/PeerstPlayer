package  
{
	import flash.display.SimpleButton;
	import flash.display.Stage;
	import flash.events.MouseEvent;
	import flash.events.StageVideoAvailabilityEvent;
	import flash.events.TimerEvent;
	import flash.events.Event;
	import flash.events.NetStatusEvent;
	import flash.events.StageVideoAvailabilityEvent;
	import flash.geom.Rectangle;
	import flash.media.StageVideo;
	import flash.media.Video;
	import flash.net.NetConnection;
	import flash.net.NetStream;
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.media.SoundTransform;
	import flash.media.StageVideo;
	import flash.media.StageVideoAvailability;
	import flash.external.ExternalInterface;
	import flash.utils.Timer;
	import flash.events.StageVideoEvent;
	import flash.media.VideoStatus
	import flash.display.Shape;
	import flash.text.TextField;
	/**
	 * FLV Player
	 * @author shule517
	 */
	public class FlvPlayer
	{
		private var stage:Stage;
		private var video:Video = new Video();
		private var stageVideo:StageVideo;
		private var netStr:NetStream = null;
		private var streamUrl:String = null;
		private var enableGpu:Boolean = true;
		
		// 前回の時刻など
		private var prevTime:Number = 0;
		private var prevBytesLoaded:uint = 0;
		private var prevBitrate:int = 0;
		
		// 再接続監視タイマー
		private var retryTimer:Timer = null;
		private var volume:String = null;
		private var retryPrevTime:Number = 0;
		private var retryFpsCount:Number = 0;
		// デバッグ用
		private var debugTimer:Timer = null;
		private var debugText:TextField = new TextField();
		private var debugTextBack:Shape = new Shape();
		private var lastNetEvent:String = "";
		
		public function FlvPlayer(stage:Stage)
		{
			this.stage = stage;
			
			// リサイズされたときに呼び出されるイベント
			stage.addEventListener(Event.RESIZE, function (e:Event):void{
				SizeChanged(stage.stageWidth, stage.stageHeight);
			});
			stage.addEventListener(StageVideoAvailabilityEvent.STAGE_VIDEO_AVAILABILITY, function (e:StageVideoAvailabilityEvent):void {
				SwitchVideo();
			});
			// 監視用タイマー
			retryTimer = new Timer(2000);
			retryTimer.addEventListener(TimerEvent.TIMER, retryTimerHandler);
			
			// デバッグ用表示
			debugTimer = new Timer(1000);
			debugTimer.addEventListener(TimerEvent.TIMER, debugTimerHandler);
			debugTimer.start();
			
			debugTextBack.x = 50;
			debugTextBack.y = 50;
			debugTextBack.graphics.beginFill(0x000000, 0.75);
			debugTextBack.graphics.lineStyle(1, 0xAAAAAA, 1);
			debugTextBack.graphics.drawRect(0, 0, 240, 180);
			debugTextBack.graphics.endFill();
			debugTextBack.visible = false;
			
			debugText.multiline = true;
			debugText.x = 55;
			debugText.y = 55;
			debugText.width = 230;
			debugText.height = 170;
			debugText.textColor = 0xFFFFFF;
			debugText.visible = false;
			stage.addChild(debugTextBack);
			stage.addChild(debugText);
		}
		
		// StageVideoとVideoの切り替え
		private function SwitchVideo():void
		{
			// GPUを使う設定にしている、GPUが使用可能である
			if (enableGpu && stage.stageVideos.length != 0)
			{
				stageVideo = stage.stageVideos[0];
				// イベントリスナーをもっていなければ登録する
				if (!stageVideo.hasEventListener(StageVideoEvent.RENDER_STATE))
				{
					stageVideo.addEventListener(StageVideoEvent.RENDER_STATE, function (e:StageVideoEvent):void {
						switch (e.status)
						{
						case VideoStatus.ACCELERATED:
							break;
						case VideoStatus.SOFTWARE:
							break;
						case VideoStatus.UNAVAILABLE:
							// StageVideoが利用できない
							EnableGpu("false");
							break;
						}
					});
				}
				
				// Videoを非表示
				video.visible = false;
				video.clear();
				if (netStr != null)
				{
					stageVideo.attachNetStream(netStr);
					video.attachNetStream(null);
				}
			}
			else
			{
				// StageVideoが動いていれば停止させる
				if (stageVideo != null)
				{
					if (netStr != null)
					{
						stageVideo.attachNetStream(null);
					}
					stageVideo = null;
				}
				// Videoを表示
				video.visible = true;
				if (netStr)
				{
					video.attachNetStream(netStr);
				}
			}
			// 描画する範囲を指定
			ChangeSize(stage.stageWidth, stage.stageHeight);
		}
		
		private function Call(functionName:String, ...args):void
		{
			if (ExternalInterface.available)
			{
				ExternalInterface.call.apply(this, [functionName].concat(args));
			}
		}

		// 音量変更
		public function ChangeVolume(vol:String):void
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
		public function ChangeSize(width:int, height:int):void
		{
			var changeWidth:Number;
			var changeHeight:Number;
			var changeX:Number;
			var changeY:Number;
			var w:Number;
			var h:Number;

			// ビデオサイズが取得できなければ終わり
			if (video.videoWidth == 0 && (stageVideo && stageVideo.videoWidth == 0))
			{
				return;
			}
			
			if (stageVideo)
			{
				w = stage.stageWidth / stageVideo.videoWidth;
				h = stage.stageHeight / stageVideo.videoHeight;
				// 横の方が長い
				if (w > h)
				{
					changeWidth = stage.stageHeight * stageVideo.videoWidth / stageVideo.videoHeight;
					changeHeight = stage.stageHeight;
				}
				// 縦の方が長い
				else
				{
					changeWidth = stage.stageWidth;
					changeHeight = stage.stageWidth * stageVideo.videoHeight / stageVideo.videoWidth;
				}
				changeX = stage.stageWidth / 2 - changeWidth / 2;
				changeY = stage.stageHeight / 2 - changeHeight / 2;
				stageVideo.viewPort = new Rectangle(changeX, changeY, changeWidth, changeHeight);
			}
			else
			{
				w = stage.stageWidth / video.videoWidth;
				h = stage.stageHeight / video.videoHeight;
				// 横の方が長い
				if (w > h)
				{
					changeWidth = stage.stageHeight * video.videoWidth / video.videoHeight;
					changeHeight = stage.stageHeight;
				}
				// 縦の方が長い
				else
				{
					changeWidth = stage.stageWidth;
					changeHeight = stage.stageWidth * video.videoHeight / video.videoWidth;
				}
				changeX = stage.stageWidth / 2 - changeWidth / 2;
				changeY = stage.stageHeight / 2 - changeHeight / 2;
				video.x = changeX;
				video.y = changeY;
				video.width = changeWidth;
				video.height = changeHeight;
			}
		}
		
		public function SizeChanged(width:int, height:int):void
		{
			ChangeSize(stage.stageWidth, stage.stageHeight);
			return;
		}
		
		// 動画幅取得
		public function GetVideoWidth():String
		{
			if (stageVideo != null)
			{
				return stageVideo.videoWidth.toString();
			}
			return video.videoWidth.toString();
		}
		
		// 動画高さ取得
		public function GetVideoHeight():String
		{
			if (stageVideo != null)
			{
				return stageVideo.videoHeight.toString();
			}
			return video.videoHeight.toString();
		}
		
		// 再生時間取得
		public function GetDurationString():String
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
		public function GetNowFrameRate():String
		{
			if (netStr == null)
			{
				return "0";
			}
			return int(netStr.currentFPS).toString();
		}
		
		// FPS取得
		public function GetFrameRate():String
		{
			if (netStr == null || netStr.info.metaData == null)
			{
				return "0";
			}
			return netStr.info.metaData["framerate"].toString();
		}
		
		// ビットレート取得
		public function GetNowBitRate():String
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
		public function GetBitRate():String
		{
			if (netStr == null || netStr.info.metaData == null)
			{
				return "0";
			}
			return String(netStr.info.metaData["audiodatarate"] + netStr.info.metaData["videodatarate"]);
		}
		
		// GPUを使うかどうか
		public function EnableGpu(value:String):void
		{
			if (value.toLowerCase() === "true")
			{
				enableGpu = true;
			}
			else
			{
				enableGpu = false;
			}
			SwitchVideo();
		}
		
		// 動画情報を表示
		public function ShowDebug():void
		{
			debugTextBack.visible = !debugTextBack.visible;
			debugText.visible = !debugText.visible;
		}
		
		// 動画再生
		public function PlayVideo(streamUrl:String):void
		{
			this.streamUrl = streamUrl;
			// プレイリストURLをコマンドライン引数から取得
			var playlistUrl:String = streamUrl;
			var urlRequest:URLRequest = new URLRequest(playlistUrl);
			var urlLoader:URLLoader = new URLLoader;
			
			// 動画に接続するための変数
			var netCon:NetConnection = new NetConnection;
			netCon.connect(null);
			netStr = new NetStream(netCon);
			// 非アクティブかつプレイヤーが他のウィンドウの裏に隠れている場合に
			// 短時間バッファが発生して再生が遅れていく現象が緩和できるかも
			netStr.bufferTime = 0.2;

			netStr.client = new Object;

			// メタ情報を取得した時の処理
			netStr.client.onMetaData = function(obj:Object):void
			{
				prevTime = netStr.time;
				prevBytesLoaded = netStr.bytesLoaded;
				prevBitrate = netStr.info.metaData["audiodatarate"] + netStr.info.metaData["videodatarate"];
				
				// 再接続時に音量が初期化されるので、一度変更済みであればここ変えておく
				if (volume != null) {
					ChangeVolume(volume);
				}
				
				Call("OpenStateChange");
			}
			
			urlLoader.addEventListener(Event.COMPLETE, function (event:Event):void
			{
				// ストリームURLを取得
				var streamUrl:String = urlLoader.data;

				// 動画を再生
				netStr.play(streamUrl + "?" + new Date().getTime());
				
				// smoothingを有効にする
				video.smoothing = true;
				// videoをステージに追加
				if (video.parent == null)
				{
					stage.addChild(video);
				}
				// 再生支援が使えたら使う
				if (stageVideo != null)
				{
					stageVideo.attachNetStream(netStr);
					video.visible = false;
				}
				else
				{
					video.attachNetStream(netStr);
					video.visible = true;
				}
				// Videoのサイズを変更する
				ChangeSize(stage.stageWidth, stage.stageHeight);
			});
			urlLoader.load(urlRequest);
			netStr.addEventListener(NetStatusEvent.NET_STATUS, netStatusHandler);
			
			if (!retryTimer.running)
			{
				retryTimer.start();
			}
		}
		
		// ネットステータス
		private function netStatusHandler(event:NetStatusEvent):void
		{
			switch (event.info.code)
			{
				case "NetStream.Buffer.Full":
					lastNetEvent = "Buffer.Full";
					break;
				case "NetStream.Buffer.Empty":
					lastNetEvent = "Buffer.Empty";
					break;
				case "NetStream.Buffer.Flush":
					lastNetEvent = "Buffer.Flush";
					break;
				case "NetStream.Play.Start":
					lastNetEvent = "Play.Start";
					break;
				case "NetStream.Play.Stop":
					lastNetEvent = "Play.Stop";
					break;
				case "NetStream.Play.StreamNotFound":
					lastNetEvent = "Play.StreamNotFound";
					break;
			}
		}
		
		// タイマー
		private function retryTimerHandler(event:TimerEvent):void
		{
			// NetStream.Buffer.Empty発動時からタイムが進んでいなければリコネクト
			if (retryPrevTime == netStr.time)
			{
				netStr.close();
				PlayVideo(streamUrl);
				return;
			}
			// FPS0の状況が続いていれば再接続する(謎の黒画面対策)
			if (netStr.currentFPS == 0)
			{
				retryFpsCount++;
				if (retryFpsCount > 5)
				{
					trace("retryFps");
					netStr.close();
					PlayVideo(streamUrl);
					retryFpsCount = 0;
				}
				trace("fpscount++");
				return;
			}
			retryPrevTime = netStr.time;
			retryFpsCount = 0;
		}
		
		private function getEncodeDuration():String
		{
			if (netStr == null)
			{
				return "00:00:00";
			}
			var sec:String = new String(Math.floor(netStr.time % 60));
			var min:String = new String(Math.floor(netStr.time /60 % 60));
			var hour:String = new String(int(netStr.time / 60 / 60));
			return ("0" + hour.toString()).slice(-2) + ":" +
				("0" + min.toString()).slice(-2) + ":" +
				("0" + sec.toString()).slice(-2);
		}
		
		// デバッグ表示用タイマー
		private function debugTimerHandler(event:TimerEvent):void
		{
			if (netStr == null)
			{
				return;
			}
			
			var text:String = "";
			text += "<bold>currentFPS</bold>: " + netStr.currentFPS.toFixed(1);
			text += "\n<bold>time</bold>: " + getEncodeDuration();
			text += "\n<bold>bufferLength</bold>: " + netStr.bufferLength;
			if (stageVideo != null)
			{
				text += "\n<bold>currentSize</bold>: " + Math.floor(stageVideo.viewPort.width) +
					"x" + Math.floor(stageVideo.viewPort.height);
			}
			else
			{
				text += "\n<bold>currentSize</bold>: " + video.width + "x" + video.height;
			}
			if (netStr.info != null)
			{
				text += "\nvideoSize: " + netStr.info.metaData["width"] + "x" + netStr.info.metaData["height"];
				text += "\n<bold>framerate</bold>: " + netStr.info.metaData["framerate"];
				text += "\n<bold>audio</bold>: " + netStr.info.metaData["audiocodecid"] + " " +
					netStr.info.metaData["audiodatarate"] + "kbps"
				text += "\n<bold>audio</bold>: " + netStr.info.metaData["audiosamplerate"] + "Hz " +
					netStr.info.metaData["audiosamplesize"] +"bit " + netStr.info.metaData["audiochannels"] + "ch";
				text += "\n<bold>video</bold>: " + netStr.info.metaData["videocodecid"] + " " +
					netStr.info.metaData["videodatarate"] + "kbps";
				text += "\n<bold>encoder</bold>: " + netStr.info.metaData["encoder"];
			}

			text += "\n<bold>LastNSEvent</bold>: " + lastNetEvent;
			debugText.htmlText = text;
		}
	}
}