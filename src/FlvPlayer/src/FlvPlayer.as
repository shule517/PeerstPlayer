package  
{
	import flash.display.Stage;
	import flash.events.StageVideoAvailabilityEvent;
	import flash.events.TimerEvent;
	import flash.events.Event;
	import flash.events.NetStatusEvent;
	import flash.geom.Rectangle;
	import flash.media.StageVideo;
	import flash.media.Video;
	import flash.net.NetConnection;
	import flash.net.NetStream;
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.net.ObjectEncoding;
	import flash.media.SoundTransform;
	import flash.utils.clearInterval;
	import flash.utils.setTimeout;
	import flash.utils.Timer;
	import flash.events.StageVideoEvent;
	import flash.media.VideoStatus
	/**
	 * FLV Player
	 * @author shule517
	 */
	public class FlvPlayer
	{
		private var stage:Stage;
		private var video:Video = new Video();
		private var stageVideo:StageVideo;
		private var netConnection:NetConnection = null;
		private var netStr:NetStream = null;
		private var playlistUrl:String = null;
		private var streamUrl:String = null;
		private var renderStatus:String = null;
		private var enableGpu:Boolean = true;
		private var enableRtmp:Boolean = false;
		private var bufferTime:Number = 0.0;
		private var bufferTimeMax:Number = 0.0;
		
		// 前回の時刻など
		private var prevTime:Number = 0;
		private var prevBytesLoaded:uint = 0;
		private var prevBitrate:int = 0;
		private var playStartTime:Date = null;
		
		// 再接続監視タイマー
		private var retryTimer:Timer = null;
		private var volume:Number = -1;
		private var pan:Number = 0;
		private var retryPrevTime:Number = 0;
		private var retryCount:Number = 0;
		private var rtmpRetryCount:int = 0;
		private var rtmpTimeoutHandle:uint = 0;
		
		// デバッグ用
		private var lastNetEvent:String = "";

		public function get Info():MovieInfo
		{
			return new MovieInfo(stageVideo, video, netConnection, netStr, lastNetEvent, (enableRtmp ? "RTMP" : "HTTP"), renderStatus);
		}
		
		public function set EnableGpu(value:Boolean):void
		{
			if (enableGpu == value) {
				return;
			}
			enableGpu = value;
			SwitchVideo();
		}
		
		public function set EnableRtmp(value:Boolean):void
		{
			if (enableRtmp == value) {
				return;
			}
			enableRtmp = value;
			Retry();
		}
		
		public function set BufferTime(value:Number):void
		{
			bufferTime = value;
			if (netStr != null) netStr.bufferTime = value;
		}
		
		public function set BufferTimeMax(value:Number):void
		{
			bufferTimeMax = value;
			if (netStr != null) netStr.bufferTimeMax = value;
		}

		public function FlvPlayer(stage:Stage)
		{
			Logger.Trace("FlvPlayer()");
			this.stage = stage;
			
			video.smoothing = true;
			stage.addChildAt(video, 0);
			
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
		}
		
		private function releaseNet():void
		{
			if (netStr) {
				netStr.removeEventListener(NetStatusEvent.NET_STATUS, netStatusHandler);
				netStr.close();
				netStr = null;
			}
			if (netConnection) {
				netConnection.removeEventListener(NetStatusEvent.NET_STATUS, netStatusHandler);
				netConnection.close();
				netConnection = null;
			}
		}
		
		// StageVideoとVideoの切り替え
		private function SwitchVideo():void
		{
			Logger.Trace("SwitchVideo()");
			// GPUを使う設定にしている、GPUが使用可能である
			if (enableGpu && stage.stageVideos.length != 0) {
				stageVideo = stage.stageVideos[0];
				// イベントリスナーをもっていなければ登録する
				if (!stageVideo.hasEventListener(StageVideoEvent.RENDER_STATE)) {
					stageVideo.addEventListener(StageVideoEvent.RENDER_STATE, function (e:StageVideoEvent):void {
						renderStatus = e.status;
						switch (renderStatus) {
							case VideoStatus.ACCELERATED:
								break;
							case VideoStatus.SOFTWARE:
								break;
							case VideoStatus.UNAVAILABLE:
								// StageVideoが利用できない
								EnableGpu = false;
								break;
						}
					});
				}
				
				// Videoを非表示
				video.visible = false;
				video.clear();
				if (netStr) {
					stageVideo.attachNetStream(netStr);
					video.attachNetStream(null);
				}
			} else {
				// StageVideoが動いていれば停止させる
				if (stageVideo) {
					if (netStr) {
						stageVideo.attachNetStream(null);
					}
					stageVideo = null;
				}
				// Videoを表示
				video.visible = true;
				if (netStr) {
					video.attachNetStream(netStr);
				}
			}
			// 描画する範囲を指定
			ChangeSize(stage.stageWidth, stage.stageHeight);
		}

		// 音量変更
		public function ChangeVolume(vol:Number):void
		{
			if (netStr == null) {
				return;
			}
			
			volume = vol;
			if (volume <= 0) {
				volume = 0;
			} else if (volume >= 1) {
				volume = 1;
			}
			netStr.soundTransform = new SoundTransform(volume, pan);
		}
		
		// 音量バランス変更
		public function ChangePan(pan:Number):void
		{
			if (netStr == null) {
				return;
			}
			
			this.pan = pan;
			if (pan <= -1) {
				pan = -1;
			} else if (pan >= 1) {
				pan = 1;
			}
			netStr.soundTransform = new SoundTransform(volume, pan);
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
			if (video.videoWidth == 0 && (stageVideo && stageVideo.videoWidth == 0)) {
				return;
			}
			
			if (stageVideo) {
				w = stage.stageWidth / stageVideo.videoWidth;
				h = stage.stageHeight / stageVideo.videoHeight;
				// 横の方が長い
				if (w > h) {
					changeWidth = stage.stageHeight * stageVideo.videoWidth / stageVideo.videoHeight;
					changeHeight = stage.stageHeight;
				// 縦の方が長い
				} else {
					changeWidth = stage.stageWidth;
					changeHeight = stage.stageWidth * stageVideo.videoHeight / stageVideo.videoWidth;
				}
				changeX = stage.stageWidth / 2 - changeWidth / 2;
				changeY = stage.stageHeight / 2 - changeHeight / 2;
				stageVideo.viewPort = new Rectangle(changeX, changeY, changeWidth, changeHeight);
			} else {
				w = stage.stageWidth / video.videoWidth;
				h = stage.stageHeight / video.videoHeight;
				// 横の方が長い
				if (w > h) {
					changeWidth = stage.stageHeight * video.videoWidth / video.videoHeight;
					changeHeight = stage.stageHeight;
				// 縦の方が長い
				} else {
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
		
		// 再生時間取得
		public function GetDurationString():String
		{
			if (netStr == null || netStr.info.metaData == null || playStartTime == null) {
				return "00:00:00";
			}

			var time:Date = new Date();
			time.setTime(time.getTime() - playStartTime.getTime());
			var totalSecond:int = time.getTime() / 1000;
			var sec:String = new String(Math.floor(totalSecond % 60));
			var min:String = new String(Math.floor(totalSecond /60 % 60));
			var hour:String = new String(int(totalSecond / 60 / 60));
			return ("0" + hour).slice(-2) + ":" +
				("0" + min).slice(-2) + ":" +
				("0" + sec).slice(-2);
		}
		
		// ビットレート取得
		public function GetNowBitRate():String
		{
			if (netStr == null) {
				return "0";
			}
			if (netStr.info.currentBytesPerSecond != 0) {
				return String(int(netStr.info.currentBytesPerSecond * 8 / 1000));
			} else {
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
		}
		
		// 動画再生
		public function PlayVideo(playlistUrl:String):void
		{
			this.playlistUrl = playlistUrl;
			var requestUrl:String = playlistUrl + "&" + new Date().getTime();
			Logger.Trace("PlayVideo(" + requestUrl + ")");
			var urlRequest:URLRequest = new URLRequest(requestUrl);
			var urlLoader:URLLoader = new URLLoader;

			// リトライ開始
			retrayStart();

			urlLoader.addEventListener(Event.COMPLETE, function (event:Event):void {
				// ストリームURLを取得
				streamUrl = urlLoader.data;
				Logger.Trace("urlLoader(Event.COMPLETE) streamUrl:" + streamUrl);

				play();
			});
			urlLoader.load(urlRequest);
		}
		
		// 再接続
		public function Retry():void
		{
			Logger.Trace("Retry()");
			retryTimer.stop();
			releaseNet();
			PlayVideo(playlistUrl);
		}
		
		private function play():void
		{
			Logger.Trace("play()");
			releaseNet();
			netConnection = new NetConnection();
			netConnection.client = new Object();
			netConnection.addEventListener(NetStatusEvent.NET_STATUS, netStatusHandler);
			if (enableRtmp) {
				netConnection.connect(streamUrl.replace("http", "rtmp"));
			} else {
				netConnection.connect(null);
			}
		}

		private function netStatusHandler(event:NetStatusEvent):void
		{
			Logger.Trace("netStatusHandler:" + event.info.code);

			switch (event.info.code) {
			case "NetConnection.Connect.Success":
				connect();
				break;
			case "NetStream.Buffer.Full":
				retrayStop();
				playStartTime = new Date();
				break;
			case "NetStream.Buffer.Empty":
			case "NetStream.Buffer.Flush":
				retrayStart();
				break;
			case "NetConnection.Connect.Closed":
				if (enableRtmp) {
					rtmpRetryCount++;
					// 短時間でClosedが連発したら、HTTP再生に切り替える
					if (rtmpRetryCount >= 4) {
						Logger.Trace("Change RTMP->HTTP");
						rtmpRetryCount = 0;
						enableRtmp = false;
						Retry();
					} else {
						Retry();
					}
				}
				break;
			case "NetStream.Play.FileNotFound":
				if (enableRtmp) {
					Logger.Trace("Change RTMP->HTTP");
					enableRtmp = false;
					Retry();
				}
				break;
			default:
				break;
			}
			lastNetEvent = event.info.code;
		}
		
		private function connect():void
		{
			Logger.Trace("connect()");

			netStr = new NetStream(netConnection);
			netStr.client = new Object();
			netStr.client.onMetaData = onMetaData;
			netStr.addEventListener(NetStatusEvent.NET_STATUS, netStatusHandler);
			
			netStr.bufferTime = bufferTime;
			netStr.bufferTimeMax = bufferTimeMax;

			if (enableRtmp) {
				var split:Array = streamUrl.split("/");
				var reg:RegExp = /^(\w+)/;
				var result:Object = reg.exec(split[4]);
				netStr.play(result[1]);
			} else {
				netStr.play(streamUrl);
			}
			SwitchVideo();
		}
		
		private function onMetaData(obj:Object):void
		{
			prevTime = netStr.time;
			prevBytesLoaded = netStr.bytesLoaded;
			prevBitrate = Number(Info.BitRate);

			CSharpCommand.RaiseOpenStateChange();
		}
		
		// リトライ停止
		private function retrayStop():void
		{
			Logger.Trace("retrayStop()");
			retryTimer.stop();
		}
		
		// リトライ開始
		private function retrayStart():void
		{
			if (!retryTimer.running) {
				Logger.Trace("retryTimer.start()")
				retryTimer.start();
			}
		}
		
		// タイマー
		private function retryTimerHandler(event:TimerEvent):void
		{
			Logger.Trace("retryTimerHandler()");
			if (netStr == null) {	
				Retry();
				return;
			}
			// NetStream.Buffer.Empty発動時からタイムが進んでいなければリコネクト
			if (retryPrevTime == netStr.time) {
				Retry();
				return;
			}
			retryPrevTime = netStr.time;
		}
	}
}