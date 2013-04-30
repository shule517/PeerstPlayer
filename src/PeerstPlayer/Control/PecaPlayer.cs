using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer.Control
{
	public partial class PecaPlayer : UserControl
	{
		public PecaPlayer()
		{
			InitializeComponent();

			wmp.uiMode = "none";
			wmp.stretchToFit = true;
		}
	}
}
