using System.Windows.Forms;
using PeerstPlayer.Controls.PecaPlayer;

namespace PeerstPlayer.Shortcut.Command
{
	/// <summary>
	/// 動画サイズに合わせるコマンド
	/// </summary>
	class FitMovieSizeCommand : IShortcutCommand
	{
		private Form form;
		private PecaPlayerControl pecaPlayer;

		public FitMovieSizeCommand(Form form, PecaPlayerControl pecaPlayer)
		{
			this.form = form;
			this.pecaPlayer = pecaPlayer;
		}

		void IShortcutCommand.Execute(CommandArgs commandArgs)
		{
			// ウィンドウ最大化時は実行しない
			if (form.WindowState == FormWindowState.Maximized)
			{
				return;
			}

			float aspectRate = (float)pecaPlayer.Width / (float)pecaPlayer.Height;

			if (pecaPlayer.AspectRate > aspectRate)
			{
				// 高さを合わせる
				int height = (int)(pecaPlayer.Width / pecaPlayer.AspectRate);
				pecaPlayer.SetSize(pecaPlayer.Width, height);
			}
			else
			{
				// 幅を合わせる
				int width = (int)(pecaPlayer.Height * pecaPlayer.AspectRate);
				pecaPlayer.SetSize(width, pecaPlayer.Height);
			}
		}

		string IShortcutCommand.GetDetail(CommandArgs commandArgs)
		{
			return "動画サイズに合わせる";
		}
	}
}
