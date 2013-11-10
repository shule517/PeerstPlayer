
namespace PeerstPlayer.Controls.StatusBar
{
	static class ToastMessage
	{
		/// <summary>
		/// ステータスバー
		/// </summary>
		private static StatusBarControl statusBar = null;

		/// <summary>
		/// 初期化
		/// </summary>
		public static void Init(StatusBarControl statusBar)
		{
			ToastMessage.statusBar = statusBar;
		}

		/// <summary>
		/// メッセージ表示
		/// </summary>
		public static void Show(string message)
		{
			if (statusBar == null)
			{
				return;
			}

			statusBar.ShowMessage(message);
		}
	}
}
