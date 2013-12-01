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

		public FlashMoviePlayerManager(AxShockwaveFlash flash)
		{
			this.flash = flash;
		}

		public void PlayVideo(string streamUrl)
		{
			string methodName = "PlayVideo";
			CallFlashMethod(streamUrl, methodName);
		}

		private void CallFlashMethod(string streamUrl, string methodName)
		{
			flash.CallFunction("<invoke name=\"" + methodName + "\" returntype=\"xml\"><arguments><string>" + streamUrl + "</string></arguments></invoke>");
		}
	}
}
