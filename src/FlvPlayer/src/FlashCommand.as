package 
{
	/**
	 * ...
	 * @author ...
	 */
	public class FlashCommand 
	{
		private var flvPlayer:FlvPlayer;

		public function FlashCommand(flvPlayer:FlvPlayer)
		{
			this.flvPlayer = flvPlayer;
		}
		
		// 動画再生
		public function PlayVideo(playlistUrl:String):void
		{
			flvPlayer.PlayVideo(playlistUrl);
		}
		
		// 再接続
		public function Retry():void
		{
			flvPlayer.Retry();
		}
		
		// 音量変更
		public function ChangeVolume(volStr:String):void
		{
			var volume:Number = parseFloat(volStr);
			flvPlayer.ChangeVolume(volume);
		}
		
		// 音量バランス変更
		public function ChangePan(panStr:String):void
		{
			var pan:Number = parseFloat(panStr);
			flvPlayer.ChangePan(pan);
		}
		
		// サイズ変更
		public function ChangeSize(width:int, height:int):void
		{
			flvPlayer.ChangeSize(width, height);
		}
		
		// 動画幅取得
		public function GetVideoWidth():String
		{
			return flvPlayer.Info.Width;
		}
		
		// 動画高さ取得
		public function GetVideoHeight():String
		{
			return flvPlayer.Info.Height;
		}
		
		// FPS取得
		public function GetNowFrameRate():String
		{
			return flvPlayer.Info.NowFrameRate;
		}
		
		// FPS取得
		public function GetFrameRate():String
		{
			return flvPlayer.Info.FrameRate;
		}
		
		// ビットレート取得
		public function GetBitRate():String
		{
			return flvPlayer.Info.BitRate;
		}
		
		// 再生時間取得
		public function GetDurationString():String
		{
			return flvPlayer.GetDurationString();
		}
		
		// 今のビットレートを取得
		public function GetNowBitRate():String
		{
			return flvPlayer.GetNowBitRate();
		}
		
		// GPUを使うかどうか
		public function EnableGpu(value:String):void
		{
			Logger.Trace("EnableGpu(" + value + ")");
			if (value.toLowerCase() == "true") {
				flvPlayer.EnableGpu = true;
			} else {
				flvPlayer.EnableGpu = false;
			}
		}
		
		// RTMP再生を使うか
		public function EnableRtmp(value:String):void
		{
			Logger.Trace("EnableRtmp(" + value + ")");
			if (value.toLowerCase() === "true") {
				flvPlayer.EnableRtmp = true;
			} else {
				flvPlayer.EnableRtmp = false;
			}
		}
		
		// バッファリング時間を設定する
		public function SetBufferTime(value:String):void
		{
			Logger.Trace("SetBufferTime(" + value + ")");
			var bufferTime:Number = parseFloat(value);
			flvPlayer.BufferTime = bufferTime;
		}
		
		// 最大バッファリング時間を設定する
		public function SetBufferTimeMax(value:String):void
		{
			Logger.Trace("SetBufferTimeMax(" + value + ")");
			var bufferTimeMax:Number = parseFloat(value);
			flvPlayer.BufferTimeMax = bufferTimeMax;
		}
		
		// 動画情報を表示
		public function ShowDebug():void
		{
		}
	}
}
