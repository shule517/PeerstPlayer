using PeerstPlayer.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerstPlayer
{
	class FormEventManager
	{
		/// <summary>
		/// Formクラス
		/// </summary>
		Form form;

		/// <summary>
		/// イベントオブザーバ
		/// </summary>
		EventObserver eventObserver;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="form"></param>
		public FormEventManager(Form form, EventObserver eventObserver)
		{
			this.form = form;
			this.eventObserver = eventObserver;

			form.MouseWheel += form_MouseWheel;
		}

		void form_MouseWheel(object sender, MouseEventArgs e)
		{
			eventObserver.OnEvent(Events.MouseWheel, e.Delta);
		}
	}
}
