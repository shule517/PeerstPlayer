using Shule.Peerst.BBS;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeerstPlayer
{
	public interface ThreadSelectObserver
	{
		void UpdateThreadUrl(string threadUrl, string threadName);
	}
}
