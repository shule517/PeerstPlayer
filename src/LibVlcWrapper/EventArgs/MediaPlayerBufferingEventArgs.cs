using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibVlcWrapper.Structures
{
	public class MediaPlayerBufferingEventArgs : System.EventArgs
	{
		public float NewCache { get; private set; }

		public MediaPlayerBufferingEventArgs(float newCache)
		{
			NewCache = newCache;
		}
	}
}
