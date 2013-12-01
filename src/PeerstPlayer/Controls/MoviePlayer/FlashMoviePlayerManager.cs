using AxShockwaveFlashObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstPlayer.Controls.MoviePlayer
{
	class FlashMoviePlayerManager
	{
		private AxShockwaveFlash flash;

		private const string PlayVideoMethod = "PlayVideo";
		private const string ChangeVolumeMethod = "ChangeVolume";

		public FlashMoviePlayerManager(AxShockwaveFlash flash)
		{
			this.flash = flash;
		}

		/// <summary>
		/// 動画再生
		/// </summary>
		/// <param name="streamUrl">ストリームURL</param>
		public void PlayVideo(string streamUrl)
		{
			CallFlashMethod(PlayVideoMethod, streamUrl);
		}

		/// <summary>
		/// 音量変更
		/// </summary>
		/// <param name="volume">音量</param>
		public void ChangeVolume(int volume)
		{
			CallFlashMethod(ChangeVolumeMethod, volume.ToString());
		}

		/// <summary>
		/// Flashの関数を実行する
		/// </summary>
		/// <param name="methodName">メソッド名</param>
		/// <param name="param">引数</param>
		private void CallFlashMethod(string methodName, string param)
		{
			if (flash.FrameLoaded(0))
			{
				flash.CallFunction("<invoke name=\"" + methodName + "\" returntype=\"xml\"><arguments><string>" + param + "</string></arguments></invoke>");
			}
		}
	}
}
