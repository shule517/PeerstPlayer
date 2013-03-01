using Shule.Peerst.BBS;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeerstPlayer
{
	public interface SelectThreadObserver
	{
		void UpdateThreadUrl(string threadUrl, string threadName);
	}
}
