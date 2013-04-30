using Shule.Peerst.Form;
using Shule.Peerst.Observer;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
namespace PeerstPlayer
{
	public partial class PlayerView : Form
	{
		// ビューモデル
		private PlayerViewModel viewModel = new PlayerViewModel();

		// スレッド選択画面
		private ThreadSelectView threadSelectView = new ThreadSelectView();


		[DllImport("User32.Dll", EntryPoint = "SetWindowText")]
		public static extern void SetWindowText(IntPtr hwnd, String text);

		// コンストラクタ
		public PlayerView()
		{
			InitializeComponent();

			// タイトルバーを非表示
			//FormUtility.VisibleTitlebar(Handle, false);

			//SetWindowText(Handle, "PeerstPlayer");

			MinimumSize = new System.Drawing.Size(0, statusBar.Height);

			pecaPlayer.FormEvent += formEvent_FormEvent;
			pecaPlayer.PropertyChanged += pecaPlayer_PropertyChanged;

			// フォームイベント登録
			FormEventManager formEvent = new FormEventManager(this);
			formEvent.FormEvent += formEvent_FormEvent;

			// イベント登録
			viewModel.PropertyChanged += viewModel_PropertyChanged;

			// スレッド選択画面
			threadSelectView.ThreadUrlChanged += threadSelectView_ThreadUrlChanged;

			// 動画再生
			string[] commandLines = Environment.GetCommandLineArgs();
			if (commandLines.Length > 1)
			{
				string streamUrl = commandLines[1];
				pecaPlayer.OpenUrl(streamUrl);
				viewModel.OpenMovie(streamUrl);
			}
		}

		// プレイヤー：プロパティ変更
		void pecaPlayer_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == PecaPlayer.Property.Volume)
			{
				if (pecaPlayer.Mute)
				{
					statusBar.Volume = "-";
				}
				else
				{
					statusBar.Volume = pecaPlayer.Volume.ToString();
				}
			}
		}

		// スレッド選択画面：スレッドURL変更
		void threadSelectView_ThreadUrlChanged(object sender, PropertyChangedEventArgs e)
		{
			// スレッド変更
			viewModel.ChangeThreadUrl(threadSelectView.ThreadUrl);
		}

		// フォームイベント
		void formEvent_FormEvent(Event.FormEventArgs args)
		{
			viewModel.DoEvents(args);

			if (args.Event == Event.FormEvents.WheelDown)
			{
				pecaPlayer.Volume -= 5;
			}
			else if (args.Event == Event.FormEvents.WheelUp)
			{
				pecaPlayer.Volume += 5;
			}
			else if (args.Event == Event.FormEvents.LeftClick)
			{
				FormUtility.WindowDragStart(Handle);
			}
			else if (args.Event == Event.FormEvents.DoubleLeftClick)
			{
				//SetWindowText(Handle, "");

				FormUtility.ToggleWindowMaximize(this);

				//SetWindowText(Handle, "PeerstPlayer");
			}
		}

		public class Property
		{
			public static string StreamUrl = "StreamUrl";
		}

		// プロパティ変更イベント
		private void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == PlayerViewModel.Property.ChannelInfo)
			{
				// チャンネル詳細を表示
				string channelName = viewModel.ChannelInfo.Name;
				string genre = viewModel.ChannelInfo.Genre;
				string detail = viewModel.ChannelInfo.Desc;
				string status = String.Format("{0} [{1}] {2}", channelName, genre, detail);
				statusBar.ChannelDetail = status;
			}
			else if (e.PropertyName == PlayerViewModel.Property.ThreadInfo)
			{
				// スレッドタイトルを表示
				string bbsName = viewModel.BbsName;
				string title = viewModel.ThreadInfo.ThreadTitle;
				int resCount = viewModel.ThreadInfo.ResCount;

				if (viewModel.ChannelInfo.ContactUrl == "")
				{
					statusBar.ThreadTitle = "コンタクトURLが指定されていません";
				}
				else if (title == "")
				{
					statusBar.ThreadTitle = String.Format("掲示板[ {0} ] スレッドを選択してください", bbsName);
				}
				else
				{
					statusBar.ThreadTitle = String.Format("スレッド[ {0} ({1}) ]", title, resCount);
				}

				// スレッド選択画面にコンタクトURLを設定
				threadSelectView.ThreadUrl = viewModel.ChannelInfo.ContactUrl;
			}
		}

		// ステータスラベル：マウス押下
		private void statusLabel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// TODO 書き込み欄の表示切り替え
		}

		// スレッドタイトルラベル：マウス押下
		private void threadTitleLabel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			threadSelectView.Show();
		}

		// ステータスバ－：サイズ変更イベント
		private void statusBar_SizeChanged(object sender, EventArgs e)
		{
			AnchorStyles pecaPlayerAnchor = pecaPlayer.Anchor;
			AnchorStyles statucBarAnchor = statusBar.Anchor;

			pecaPlayer.Anchor = new AnchorStyles();
			statusBar.Anchor = new AnchorStyles();

			ClientSize = new System.Drawing.Size(ClientSize.Width, pecaPlayer.Height + statusBar.Height);

			pecaPlayer.Anchor = pecaPlayerAnchor;
			statusBar.Anchor = statucBarAnchor;
		}
	}
}
