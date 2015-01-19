using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibVlcWrapper.Structures
{
	public class MediaPlayerScrambleChangedEventArgs : System.EventArgs
	{
		public bool NewScrambled { get; private set; }

		public MediaPlayerScrambleChangedEventArgs(bool newScrambled)
		{
			NewScrambled = newScrambled;
		}
	}
}
