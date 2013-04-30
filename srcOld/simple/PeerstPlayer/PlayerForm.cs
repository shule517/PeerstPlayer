using PeerstPlayer.Event;
using Shule.Peerst.BBS;
using Shule.Peerst.Form;
using Shule.Peerst.Observer;
using Shule.Peerst.PeerCast;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace PeerstPlayer
{
	public partial class PlayerForm : Form, Observer
	{
		// ViewModel
		PeerstPlayerViewModel viewModel = new PeerstPlayerViewModel();

		// スレッド選択フォーム
		ThreadSelectView threadSelectView;

		// イベントマネージャ
		WmpEventManager wmpEventManager;
		FormEventManager formEventManager;
		WmpNativeWindow wmpNativeWindow;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PlayerForm()
		{
			InitializeComponent();

			// スレッド選択フォーム
			threadSelectView = new ThreadSelectView();
			threadSelectView.ThreadUrlChanged += threadSelectView_ThreadUrlChanged;

			// イベントマネージャ
			formEventManager = new FormEventManager(this);
			formEventManager.FormEvent += formEventManager_FormEvent;

			wmpEventManager = new WmpEventManager(wmp);
			wmpNativeWindow = new WmpNativeWindow(wmp.Handle);

			// イベント登録
			viewModel.OnChannelInfoChange += viewModel_OnChannelInfoChange;
			viewModel.OnThreadTitleChange += viewModel_OnThreadTitleChange;

			// タイトルバーを非表示
			FormUtility.VisibleTitlebar(Handle, false);

			if (Environment.GetCommandLineArgs().Length <= 1)
			{
				// コマンドライン引数指定なし
				return;
			}

			// 動画再生
			string streamUrl = Environment.GetCommandLineArgs()[1];
			wmp.URL = streamUrl;

			// チャンネル再生開始
			viewModel.Open(streamUrl);
		}

		// スレッドURL変更イベント
		void threadSelectView_ThreadUrlChanged(object sender, PropertyChangedEventArgs e)
		{
			// スレッド更新
			viewModel.ChangeUrl(threadSelectView.ThreadUrl);
		}

		void formEventManager_FormEvent(FormEventArgs args)
		{
			if (args.Event == FormEvents.LeftClick)
			{
				MessageBox.Show("test");
			}
		}

		#region GUI更新

		/// <summary>
		/// スレッドタイトル更新
		/// </summary>
		void viewModel_OnThreadTitleChange()
		{
			// スレッド未選択
			if (viewModel.ThreadInfo.ThreadTitle == "")
			{
				threadTitleLabel.Text = "掲示板[ " + viewModel.BbsName + " ] スレッドを選択してください。";
				threadTitleLabel.ForeColor = Color.SpringGreen;
			}
			// スレッドタイトル表示
			else
			{
				threadTitleLabel.Text = viewModel.ThreadInfo.ThreadTitle + " (" + viewModel.ThreadInfo.ResCount + ")";

				if (viewModel.ThreadInfo.ResCount >= 1000)
				{
					threadTitleLabel.ForeColor = Color.Red;
				}
				else
				{
					threadTitleLabel.ForeColor = Color.SpringGreen;
				}
			}
		}

		/// <summary>
		/// チャンネル情報更新
		/// </summary>
		void viewModel_OnChannelInfoChange()
		{
			channelInfoLabel.Text = viewModel.ChannelInfo.ToString();
		}

		#endregion

		#region GUIイベント

		/// <summary>
		/// 書き込み欄：キーダウンイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void writeField_KeyDown(object sender, KeyEventArgs e)
		{
			// Ctrl + Enter
			if (e.Control && (e.KeyCode == Keys.Enter))
			{
				// レス書き込み
				if (viewModel.Write("", "sage", writeField.Text))
				{
					writeField.Text = "";
				}
			}
		}

		/// <summary>
		/// スレッドタイトルラベル：キーダウンイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void threadTitleLabel_MouseDown(object sender, MouseEventArgs e)
		{
			// 左クリック
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				// スレッド選択フォームを開く
				threadSelectView.ThreadUrl = viewModel.ThreadUrl;
				threadSelectView.Show();
			}
			// 右クリック
			else
			{
				// スレビューワを開く
				viewModel.OpenThreadViewer();
			}
		}

		/// <summary>
		/// 書き込み欄のサイズ調節
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void writeField_TextChanged(object sender, EventArgs e)
		{
			writeField.Height = writeField.PreferredSize.Height;
		}

		#endregion

		/// <summary>
		/// イベント処理
		/// </summary>
		/// <param name="param"></param>
		void Observer.Update(object param)
		{
			FormEventArgs args = param as FormEventArgs;
			FormEvents events = args.Event;

			// TODO args.ModifyKeysを見るようにする
			// TODO Chain of Responsibilityパターンを使用する

			// ホイールアップ
			if (events == Event.FormEvents.WheelUp)
			{
				wmp.settings.volume += 5;
			}
			// ホイールダウン
			else if (events == Event.FormEvents.WheelDown)
			{
				wmp.settings.volume -= 5;
			}
			// 左クリック
			else if (events == Event.FormEvents.LeftClick)
			{
				// ウィンドウドラッグ開始
				FormUtility.WindowDragStart(Handle);

				// WMPフルスクリーン解除
				if (wmp.fullScreen)
				{
					wmp.fullScreen = false;
				}
			}
			// 左ダブルクリック
			else if (events == Event.FormEvents.DoubleLeftClick)
			{
				FormUtility.ToggleWindowMaximize(this);

				if (WindowState == FormWindowState.Normal)
				{
					// タイトルバー分小さくする
					Height -= SystemInformation.CaptionHeight + 2;
					Width -= 2;
				}
			}
		}
	}
}
