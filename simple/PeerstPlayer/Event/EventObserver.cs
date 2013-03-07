using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeerstPlayer.Event
{
	/// <summary>
	/// イベントオブザーバ
	/// </summary>
	interface EventObserver
	{
		void OnEvent(Events events, Object param);
	}

	/// <summary>
	/// イベントの種類
	/// </summary>
	enum Events
	{
		None = 0,
		MouseWheel,
		DoubleClick,
		MouseDown,
	}
}
