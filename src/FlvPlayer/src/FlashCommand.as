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
		}
		
		// サイズ変更イベント
		public function SizeChanged(width:int, height:int):void
		{
		}
		
		// 音量変更
		public function ChangeVolume(volStr:String):void
		{
		}
		
		// 音量バランス変更
		public function ChangePan(panStr:String):void
		{
		}
		
		// サイズ変更
		public function ChangeSize(width:int, height:int):void
		{
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
		
		// GPUを使うかどうか
		public function EnableGpu(value:String):void
		{
		}
		
		// RTMP再生を使うか
		public function EnableRtmp(value:String):void
		{
		}
		
		// 動画情報を表示
		public function ShowDebug():void
		{
		}
	}
}
