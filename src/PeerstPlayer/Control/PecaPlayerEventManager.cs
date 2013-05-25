using AxWMPLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer.Control
{
	class PecaPlayerEventManager
	{
		// イベント
		public event EventHandler LeftClick = delegate { };
		public event EventHandler RightClick = delegate { };
		public event EventHandler MiddleClick = delegate { };

		public event EventHandler DoubleLeftClick = delegate { };
		public event EventHandler DoubleRightClick = delegate { };
		public event EventHandler DoubleMiddleClick = delegate { };

		public event EventHandler WheelDown = delegate { };
		public event EventHandler WheelUp = delegate { };

		public event EventHandler RightDownWheelDown = delegate { };
		public event EventHandler RightDownWheelUp = delegate { };

		public event EventHandler LeftDownWheelDown = delegate { };
		public event EventHandler LeftDownWheelUp = delegate { };

		public event EventHandler RightToLeftClick = delegate { };
		public event EventHandler LeftToRightClick = delegate { };

		public PecaPlayerEventManager(AxWindowsMediaPlayer wmp)
		{
			wmp.DoubleClickEvent += wmp_DoubleClickEvent;
			wmp.MouseDownEvent += wmp_MouseDownEvent;
		}

		void wmp_MouseDownEvent(object sender, _WMPOCXEvents_MouseDownEvent e)
		{
		}

		private void wmp_DoubleClickEvent(object sender, _WMPOCXEvents_DoubleClickEvent e)
		{
			// 左クリック
			if (e.nButton == (short)Keys.LButton)
			{
				// TODO 右クリック->左クリック

				// 左クリック
				LeftClick(sender, new EventArgs());
			}
			// 右クリック
			else if (e.nButton == (short)Keys.RButton)
			{
				// TODO 左クリック->右クリック

				// 右クリック
				RightClick(sender, new EventArgs());
			}
			// 中クリック
			else if (e.nButton == (short)Keys.MButton)
			{
				MiddleClick(sender, new EventArgs());
			}
		}
	}
}
