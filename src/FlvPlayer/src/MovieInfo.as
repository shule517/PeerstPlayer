package 
{
	import flash.media.StageVideo;
	import flash.media.Video;
	import flash.net.NetConnection;
	import flash.net.NetStream;
	
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

		public function MovieInfo(stageVideo:StageVideo, video:Video, netConnection:NetConnection, netStr:NetStream, lastNSEvent:String)
		{
			this.stageVideo = stageVideo;
			this.video = video;
			this.netConnection = netConnection;
			this.netStr = netStr;
			this.lastNSEvent = lastNSEvent;
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
			if (stageVideo == null) return 0;
			return Math.floor(stageVideo.viewPort.width);
		}

		public function get CurrentHeight():Number
		{
			if (stageVideo != null)
			{
				return Math.floor(stageVideo.viewPort.height);
			}
			else
			{
				return video.width;
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

		public function get Width():String
		{
			if (netStr == null) return "0";
			return netStr.info.metaData["width"];
		}

		public function get Height():String
		{
			if (netStr == null) return "0";
			return netStr.info.metaData["height"];
		}
		
		public function get FrameRate():String
		{
			if (netStr == null) return "0";
			return netStr.info.metaData["framerate"];
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
			if (netStr == null) return "0";
			return netStr.info.metaData["audiocodecid"];
		}

		public function get AudioDataRate():String
		{
			if (netStr == null) return "0";
			return netStr.info.metaData["audiodatarate"];
		}

		public function get AudioSampleRate():String
		{
			if (netStr == null) return "0";
			return netStr.info.metaData["audiosamplerate"];
		}

		public function get AudioSampleSize():String
		{
			if (netStr == null) return "0";
			return netStr.info.metaData["audiosamplesize"];
		}

		public function get AudioChannels():String
		{
			if (netStr == null) return "0";
			return netStr.info.metaData["audiochannels"];
		}

		public function get VideoCodecId():String
		{
			if (netStr == null) return "0";
			return netStr.info.metaData["videocodecid"];
		}

		public function get VideoDataRate():String
		{
			if (netStr == null) return "0";
			return netStr.info.metaData["videodatarate"];
		}

		public function get Encoder():String
		{
			if (netStr == null) return "0";
			return netStr.info.metaData["encoder"];
		}
		
		public function get LastNSEvent():String
		{
			return lastNSEvent;
		}
	}
}
