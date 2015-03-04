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
	import flash.net.ObjectEncoding;
	import flash.media.SoundTransform;
	import flash.media.StageVideo;
	import flash.media.StageVideoAvailability;
	import flash.external.ExternalInterface;
	import flash.utils.clearInterval;
	import flash.utils.setTimeout;
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
		private var netConnection:NetConnection = null;
		private var netStr:NetStream = null;
		private var playlistUrl:String = null;
		private var streamUrl:String = null;
		private var protocol:String = null;
		private var enableGpu:Boolean = true;
		private var enableRtmp:Boolean = false;
		
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
			return new MovieInfo(stageVideo, video, netConnection, netStr, lastNetEvent);
		}

		public function FlvPlayer(stage:Stage)
		{
			Logger.Trace("FlvPlayer()");
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
		}
		
		private function releaseNet():void
		{
			if (netStr != null) {
				netStr.removeEventListener(NetStatusEvent.NET_STATUS, rtmpNetStatusHandler);
				netStr.removeEventListener(NetStatusEvent.NET_STATUS, httpNetStatusHandler);
				netStr.close();
				netStr = null;
			}
			if (netConnection != null) {
				netConnection.removeEventListener(NetStatusEvent.NET_STATUS, rtmpNetStatusHandler);
				netConnection.removeEventListener(NetStatusEvent.NET_STATUS, httpNetStatusHandler);
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
						switch (e.status) {
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
				if (netStr != null) {
					stageVideo.attachNetStream(netStr);
					video.attachNetStream(null);
				}
			} else {
				// StageVideoが動いていれば停止させる
				if (stageVideo != null) {
					if (netStr != null) {
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
		public function ChangeVolume(volStr:String):void
		{
			if (netStr == null) {
				return;
			}
			
			volume = parseFloat(volStr);
			if (volume <= 0) {
				volume = 0;
			} else if (volume >= 1) {
				volume = 1;
			}
			netStr.soundTransform = new SoundTransform(volume, pan);
		}
		
		// 音量バランス変更
		public function ChangePan(panStr:String):void
		{
			if (netStr == null) {
				return;
			}
			
			pan = parseFloat(panStr);
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
		
		// 動画幅取得
		public function GetVideoWidth():String
		{
			if (stageVideo != null) {
				return stageVideo.videoWidth.toString();
			}
			return video.videoWidth.toString();
		}
		
		// 動画高さ取得
		public function GetVideoHeight():String
		{
			if (stageVideo != null) {
				return stageVideo.videoHeight.toString();
			}
			return video.videoHeight.toString();
		}
		
		// 再生時間取得
		public function GetDurationString():String
		{
			if (netStr == null || netStr.info.metaData == null) {
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
		
		// FPS取得
		public function GetNowFrameRate():String
		{
			if (netStr == null) {
				return "0";
			}
			return int(netStr.currentFPS).toString();
		}
		
		// FPS取得
		public function GetFrameRate():String
		{
			if (netStr == null || netStr.info.metaData == null) {
				return "0";
			}
			return netStr.info.metaData["framerate"].toString();
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
		
		// ビットレート取得
		public function GetBitRate():String
		{
			if (netStr == null || netStr.info.metaData == null) {
				return "0";
			}
			return String(netStr.info.metaData["audiodatarate"] + netStr.info.metaData["videodatarate"]);
		}
		
		// GPUを使うかどうか
		public function EnableGpu(value:String):void
		{
			Logger.Trace("EnableGpu(" + value + ")");
			if (value.toLowerCase() === "true") {
				enableGpu = true;
			} else {
				enableGpu = false;
			}
		}
		
		// RTMP再生を使うか
		public function EnableRtmp(value:String):void
		{
			Logger.Trace("EnableRtmp(" + value + ")");
			if (value.toLowerCase() == "true") {
				enableRtmp = true;
			} else {
				enableRtmp = false;
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

				if (enableRtmp) {
					rtmpRetryCount = 0;
					playRtmp();
				} else {
					playHttp();
				}
			});
			urlLoader.load(urlRequest);
		}
		
		private function playRtmp():void
		{
			Logger.Trace("playRtmp()");
			releaseNet();
			protocol = "rtmp";
			netConnection = new NetConnection();
			netConnection.client = new Object();
			netConnection.objectEncoding = ObjectEncoding.AMF0;
			netConnection.addEventListener(NetStatusEvent.NET_STATUS, rtmpNetStatusHandler);
			netConnection.connect(streamUrl.replace("http", "rtmp"));
		}
		
		private function playHttp():void
		{
			Logger.Trace("playHttp()");
			releaseNet();
			protocol = "http";
			netConnection = new NetConnection();
			netConnection.connect(null);
			netStr = new NetStream(netConnection);
			netStr.addEventListener(NetStatusEvent.NET_STATUS, httpNetStatusHandler);
			// 非アクティブかつプレイヤーが他のウィンドウの裏に隠れている場合に
			// 短時間バッファが	発生して再生が遅れていく現象が緩和できるかも
			netStr.bufferTime = 0.2;

			netStr.client = new Object;
			// メタ情報を取得した時の処理
			netStr.client.onMetaData = onMetaData;
			// 動画を再生
			netStr.play(streamUrl + "?" + new Date().getTime());
			playStartTime = new Date();
		}
		
		private function rtmpNetStatusHandler(event:NetStatusEvent):void
		{
			switch (event.info.code) {
				case "NetConnection.Connect.Success":
					netStr = new NetStream(netConnection);
					netStr.client = new Object();
					netStr.client.onMetaData = onMetaData;
					netStr.addEventListener(NetStatusEvent.NET_STATUS, rtmpNetStatusHandler);
					var split:Array = streamUrl.split("/");
					var reg:RegExp = /^(\w+)/;
					var result:Object = reg.exec(split[4]);
					netStr.play(result[1]);
					break;
				case "NetConnection.Connect.Failed":
					// PeercastがRTMP再生に対応していないと思われるので、HTTPで再生する
					playHttp();
					break;
				case "NetConnection.Connect.Closed":
					retrayStop();
					++rtmpRetryCount;
					// 短時間でClosedが連発したら、HTTP再生に切り替える
					if (rtmpRetryCount >= 5) {
						Logger.Trace("Change RTMP->HTTP");
						setTimeout(function():void {
							playHttp();
						}, 1);		
					} else {
						clearInterval(rtmpTimeoutHandle);
						setTimeout(function():void {
							playRtmp();
						}, 1);						
					}
					break;
				case "NetStream.Play.FileNotFound":
				case "NetConnection.Play.FileNotFound":
					// Peercastでチャンネルが再生されていないかもしれないので、つなぎ直し
					PlayVideo(playlistUrl);
					break;
				case "NetStream.Play.Start":
					playStartTime = new Date();
					// smoothingを有効にする
					video.smoothing = true;
					// videoをステージに追加
					if (video.parent == null){
						stage.addChildAt(video, 0);
					}
					// 再生支援が使えたら使う
					if (stageVideo != null){
						stageVideo.attachNetStream(netStr);
						video.visible = false;
					} else {
						video.attachNetStream(netStr);
						video.visible = true;
					}
					// Videoのサイズを変更する
					ChangeSize(stage.stageWidth, stage.stageHeight);
					// 10秒再生できていれば再接続カウントをリセットする
					rtmpTimeoutHandle = setTimeout(function():void {
						rtmpRetryCount = 0;
					}, 10000);
					break;
				case "NetStream.Play.Stop":
					break;
				case "NetStream.Play.StreamNotFound":
					enableRtmp = false;
					playHttp();
					break;
			}
			
			lastNetEvent = event.info.code;
			Logger.Trace("rtmpNetStatusHandler:" + event.info.code);
		}
		
		// ネットステータス
		private function httpNetStatusHandler(event:NetStatusEvent):void
		{
			switch (event.info.code) {
				case "NetStream.Buffer.Full":
					retrayStop();
					break;
				case "NetStream.Buffer.Empty":
					// 再生時間をリセット
					playStartTime = new Date();
					retrayStart();
					break;
				case "NetStream.Buffer.Flush":
					break;
				case "NetStream.Play.Start":
					// smoothingを有効にする
					video.smoothing = true;
					// videoをステージに追加
					if (video.parent == null){
						stage.addChildAt(video, 0);
					}
					// 再生支援が使えたら使う
					if (stageVideo != null){
						stageVideo.attachNetStream(netStr);
						video.visible = false;
					} else {
						video.attachNetStream(netStr);
						video.visible = true;
					}
					// Videoのサイズを変更する
					ChangeSize(stage.stageWidth, stage.stageHeight);
					break;
			}
			lastNetEvent = event.info.code;
			Logger.Trace("httpNetStatusHandler:" + event.info.code);
		}
		
		private function onMetaData(obj:Object):void
		{
			prevTime = netStr.time;
			prevBytesLoaded = netStr.bytesLoaded;
			prevBitrate = netStr.info.metaData["audiodatarate"] + netStr.info.metaData["videodatarate"];
						
			// 再接続時に音量が初期化されるので、一度変更済みであればここ変えておく
			if (volume != -1) {
				ChangeVolume(volume.toString());
			}
			CSharpCommand.RaiseOpenStateChange();
		}
		
		// 動画再生リトライ
		private function retry():void
		{
			Logger.Trace("retry");
			releaseNet();
			PlayVideo(playlistUrl);
			retryTimer.stop();
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
			Logger.Trace("retryTimerHandler()")
			if (netStr == null) {	
				retry();
				return;
			}
			// NetStream.Buffer.Empty発動時からタイムが進んでいなければリコネクト
			if (retryPrevTime == netStr.time) {
				retry();
				return;
			}
			retryPrevTime = netStr.time;
			// Buffer0の状況が続いていればBumpする(謎の黒画面対策)
			if (!netConnection.connected || (protocol == "http" && netStr.bufferLength == 0)) {
				retryCount++;
				if (retryCount > 3) {
					CSharpCommand.RequestBump();
					retry();
					retryCount = 0;
				}
				Logger.Trace("retryCount++");
				return;
			}
			retryCount = 0;
		}
	}
}