package 
{
	import flash.desktop.ClipboardFormats;
	import flash.display.Stage;
	import flash.events.TimerEvent;
	import flash.text.TextFormat;
	import flash.utils.Timer;
	import flash.text.TextField;
	import flash.display.Shape;

	/**
	 * ...
	 * @author ...
	 */
	public class MovieInfoPanel 
	{
		private var debugTimer:Timer = null;
		private var debugText:TextField = new TextField();
		private var debugTextBack:Shape = new Shape();
		private var flvPlayer:FlvPlayer;
		
		public function MovieInfoPanel(stage:Stage, flvPlayer:FlvPlayer)
		{
			this.flvPlayer = flvPlayer;
			
			debugTimer = new Timer(1000);
			debugTimer.addEventListener(TimerEvent.TIMER, debugTimerHandler);
			debugTimer.start();
			debugTextBack.x = 50;
			debugTextBack.y = 50;
			debugTextBack.graphics.beginFill(0x000000, 0.75);
			debugTextBack.graphics.lineStyle(1, 0xAAAAAA, 1);
			debugTextBack.graphics.drawRect(0, 0, 330, 160);
			debugTextBack.graphics.endFill();
			debugTextBack.visible = false;
			
			debugText.multiline = true;
			debugText.x = 55;
			debugText.y = 55;
			debugText.width = 330;
			debugText.height = 160;
			debugText.textColor = 0xFFFFFF;
			debugText.visible = false;
			stage.addChild(debugTextBack);
			stage.addChild(debugText);
		}

		// 動画情報を表示
		public function ShowDebug():void
		{
			debugTextBack.visible = !debugTextBack.visible;
			debugText.visible = !debugText.visible;
		}

		// デバッグ表示用タイマー
		private function debugTimerHandler(event:TimerEvent):void
		{
			// デバッグの場合は、トレース情報を表示する
			if (Logger.IsDebug())
			{
				debugText.htmlText = Logger.GetMessage();
				return;
			}
			
			var text:String = "";
			text += "<font face=\"ＭＳ ゴシック\">"
			text +=   "<bold>HTTP/RTMP        </bold>: " + ((flvPlayer.Info.Protocol == null) ? "-" : flvPlayer.Info.Protocol);
			text += "\n<bold>GPU再生支援      </bold>: " + flvPlayer.Info.GPUEnable;
			text += "\n<bold>配信時間         </bold>: " + flvPlayer.Info.EncodeDuration;
			text += "\n<bold>バッファー(秒)   </bold>: " + flvPlayer.Info.BufferLength;
			text += "\n<bold>ウィンドウサイズ </bold>: " + flvPlayer.Info.CurrentWidth + " x " + flvPlayer.Info.CurrentHeight;
			text += "\n<bold>動画サイズ       </bold>: " + flvPlayer.Info.Width + " x " + flvPlayer.Info.Height;
			text += "\n<bold>FPS              </bold>: " + flvPlayer.Info.CurrentFps;
			text += "\n<bold>Video            </bold>: " + flvPlayer.Info.AudioCodecId + " " + flvPlayer.Info.VideoDataRate + "kbps";
			text += "\n<bold>Audio            </bold>: " + flvPlayer.Info.AudioCodecId + " " + flvPlayer.Info.AudioDataRate + "kbps"
			text += "\n<bold>Audio Type       </bold>: " + flvPlayer.Info.AudioSampleRate + "Hz " + flvPlayer.Info.AudioSampleSize + "bit " + flvPlayer.Info.AudioChannels + "ch";
			text += "\n<bold>Encoder          </bold>: " + flvPlayer.Info.Encoder;
			text += "\n<bold>LastNSEvent      </bold>: " + flvPlayer.Info.LastNSEvent;
			text += "</font>"
			
			debugText.htmlText = text;
		}
	}
}