package 
{
	import flash.media.StageVideo;
	import flash.media.Video;
	import flash.net.NetConnection;
	import flash.net.NetStream;
	import flash.media.VideoStatus;
	
	/**
	 * ...
	 * @author ...
	 */
	public class MovieInfo 
	{
		private var stageVideo:StageVideo = null;
		private var video:Video = null;
		private var netConnection:NetConnection = null;
		private var netStr:NetStream = null;
		private var lastNSEvent:String = "";
		private var protocol:String = "";
		private var renderStatus:String = null;

		public function MovieInfo(stageVideo:StageVideo, video:Video, netConnection:NetConnection, netStr:NetStream, lastNSEvent:String, protocol:String, renderStatus:String)
		{
			this.stageVideo = stageVideo;
			this.video = video;
			this.netConnection = netConnection;
			this.netStr = netStr;
			this.lastNSEvent = lastNSEvent;
			this.protocol = protocol;
			this.renderStatus = renderStatus;
		}
		
		public function get Protocol():String
		{
			return this.protocol;
		}

		public function get CurrentFps():String
		{
			if (netStr == null) return "0";
			return netStr.currentFPS.toFixed(1);
		}

		public function get EncodeDuration():String
		{
			if (netStr == null) {
				return "00:00:00";
			}
			var sec:String = new String(Math.floor(netStr.time % 60));
			var min:String = new String(Math.floor(netStr.time /60 % 60));
			var hour:String = new String(int(netStr.time / 60 / 60));
			return ("0" + hour.toString()).slice(-2) + ":" +
				("0" + min.toString()).slice(-2) + ":" +
				("0" + sec.toString()).slice(-2);
		}

		public function get BufferLength():Number
		{
			if (netStr == null) return 0;
			return netStr.bufferLength;
		}
		
		public function get CurrentWidth():Number
		{
			if (netStr == null) return 0;
			if (stageVideo != null)
			{
				return Math.floor(stageVideo.viewPort.width);
			}
			else
			{
				return video.width;
			}
		}

		public function get CurrentHeight():Number
		{
			if (netStr == null) return 0;
			if (stageVideo != null)
			{
				return Math.floor(stageVideo.viewPort.height);
			}
			else
			{
				return video.height;
			}
		}
		
		public function get VideoWidth():String
		{
			if (stageVideo != null) {
				return stageVideo.videoWidth.toString();
			}
			return video.videoWidth.toString();
		}
		
		public function get VideoHeight():String
		{
			if (stageVideo != null) {
				return stageVideo.videoHeight.toString();
			}
			return video.videoHeight.toString();
		}

		// MetaDataの取得メソッド
		private function GetMetaData(key:String, defaultValue:String):String
		{
			try {
				if (netStr == null) {
					return defaultValue;
				}

				return netStr.info.metaData[key];
			}
			catch (error:Error) {
			}
			
			return defaultValue;
		}

		public function get Width():String
		{
			return GetMetaData("width", "0");
		}

		public function get Height():String
		{
			return GetMetaData("height", "0");
		}
		
		public function get FrameRate():String
		{
			return GetMetaData("framerate", "0");
		}
		
		public function get NowFrameRate():String
		{
			if (netStr == null) return "0";
			return int(netStr.currentFPS).toString();
		}

		public function get BitRate():String
		{
			if (netStr == null || netStr.info.metaData == null) {
				return "0";
			}
			return String(this.AudioDataRate + this.VideoDataRate);
		}

		public function get AudioCodecId():String
		{
			return GetMetaData("audiocodecid", "-");
		}

		public function get AudioDataRate():String
		{
			return GetMetaData("audiodatarate", "0");
		}

		public function get AudioSampleRate():String
		{
			return GetMetaData("audiosamplerate", "0");
		}

		public function get AudioSampleSize():String
		{
			return GetMetaData("audiosamplesize", "-");
		}

		public function get AudioChannels():String
		{
			return GetMetaData("audiochannels", "-");
		}

		public function get VideoCodecId():String
		{
			return GetMetaData("videocodecid", "-");
		}

		public function get VideoDataRate():String
		{
			return GetMetaData("videodatarate", "-");
		}

		public function get Encoder():String
		{
			return GetMetaData("encoder", "-");
		}
		
		public function get LastNSEvent():String
		{
			return lastNSEvent;
		}

		public function get GPUEnable():String
		{
			if ((video.visible == false) && (stageVideo != null))
			{
				if (renderStatus == VideoStatus.ACCELERATED)
				{
					return "有効";
				}
				else if (renderStatus == VideoStatus.SOFTWARE)
				{
					return "無効(ソフトウェアモード)";
				}
			}

			return "無効";
		}
	}
}
