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
			var info:MovieInfo = flvPlayer.Info;
			text += "<font face=\"ＭＳ ゴシック\">"
			text +=   "<bold>HTTP/RTMP        </bold>: " + ((info.Protocol == null) ? "-" : info.Protocol);
			text += "\n<bold>GPU再生支援      </bold>: " + info.GPUEnable;
			text += "\n<bold>配信時間         </bold>: " + info.EncodeDuration;
			text += "\n<bold>バッファー(秒)   </bold>: " + info.BufferLength;
			text += "\n<bold>ウィンドウサイズ </bold>: " + info.CurrentWidth + " x " + info.CurrentHeight;
			text += "\n<bold>動画サイズ       </bold>: " + info.Width + " x " + info.Height;
			text += "\n<bold>FPS              </bold>: " + info.CurrentFps;
			text += "\n<bold>Video            </bold>: " + info.AudioCodecId + " " + info.VideoDataRate + "kbps";
			text += "\n<bold>Audio            </bold>: " + info.AudioCodecId + " " + info.AudioDataRate + "kbps"
			text += "\n<bold>Audio Type       </bold>: " + info.AudioSampleRate + "Hz " + info.AudioSampleSize + "bit " + info.AudioChannels + "ch";
			text += "\n<bold>Encoder          </bold>: " + info.Encoder;
			text += "\n<bold>LastNSEvent      </bold>: " + info.LastNSEvent;
			text += "</font>"
			
			debugText.htmlText = text;
		}
	}
}