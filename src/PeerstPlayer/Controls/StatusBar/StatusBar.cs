using System;
using System.Windows.Forms;
using PeerstLib.Util;
using PeerstPlayer.Forms.Player;

namespace PeerstPlayer.Controls.StatusBar
{
	//-------------------------------------------------------------
	// 概要：ステータスバーコントロール
	//-------------------------------------------------------------
	public partial class StatusBarControl : UserControl
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// チャンネル詳細
		/// </summary>
		string channelDetail = "";
		public string ChannelDetail
		{
			get { return channelDetail; }
			set
			{
				channelDetail = value;
				if (messageDisplayTimer.Enabled == false)
				{
					UpdateChannelDetail();
				}
			}
		}

		/// <summary>
		/// 動画ステータス
		/// </summary>
		public string MovieStatus
		{
			get { return movieDetail.MovieStatus; }
			set { movieDetail.MovieStatus = value; }
		}

		/// <summary>
		/// 音量
		/// </summary>
		public string Volume
		{
			get { return movieDetail.Volume; }
			set { movieDetail.Volume = value; }
		}
		
		/// <summary>
		/// 選択スレッドURL
		/// </summary>
		public string SelectThreadUrl
		{
			get { return writeField.SelectThreadUrl; }
			set { writeField.SelectThreadUrl = value; }
		}

		/// <summary>
		/// 書き込み欄の表示
		/// </summary>
		public bool WriteFieldVisible
		{
			get { return writeField.Visible; }
			set
			{
				// 書き込み欄の表示切り替え
				writeField.Visible = value;

				// 高さの調節
				if (writeField.Visible)
				{
					Logger.Instance.Debug("書き込み欄の高さを自動調節");
					writeField.Height = writeField.PreferredSize.Height;
				}
				else
				{
					Logger.Instance.Debug("書き込み欄を非表示");
					writeField.Height = 0;
				}

				// 高さ変更イベント
				HeightChanged(this, new EventArgs());
			}
		}

		/// <summary>
		/// 高さ変更イベント
		/// </summary>
		public event EventHandler HeightChanged = delegate { };

		/// <summary>
		/// ステータスバークリックイベント
		/// </summary>
		public event MouseEventHandler ChannelDetailClick = delegate { };

		/// <summary>
		/// スレッドタイトル右クリック
		/// </summary>
		public event EventHandler ThreadTitleRightClick = delegate { };

		/// <summary>
		/// 音量クリックイベント
		/// </summary>
		public event EventHandler VolumeClick
		{
			add { movieDetail.VolumeClick += value; }
			remove { movieDetail.VolumeClick -= value; }
		}

		/// <summary>
		/// マウスホバーイベント
		/// </summary>
		public event EventHandler MouseHoverEvent
		{
			add { movieDetail.MouseHoverEvent += value; }
			remove { movieDetail.MouseHoverEvent -= value; }
		}

		/// <summary>
		/// コンストラクタ
		/// イベント登録
		/// </summary>
		public StatusBarControl()
		{
			Logger.Instance.Debug("StatusBar()");

			InitializeComponent();

			// サイズ変更イベント登録
			writeField.SizeChanged += writeField_SizeChanged;
			writeField.HeightChanged += (sender, e) => HeightChanged(sender, e);

			// チャンネル詳細クリック
			movieDetail.ChannelDetailClick += (sender, e) =>
			{
				Logger.Instance.Info("チャンネル詳細をクリック");
				ChannelDetailClick(sender, e);
			};

			// スレッドタイトル右クリック
			writeField.RightClick += (sender, e) =>
			{
				Logger.Instance.Info("スレッドタイトルを右クリック");
				ThreadTitleRightClick(sender, e);
			};
		}

		/// <summary>
		/// 動画情報
		/// </summary>
		MovieInfo movieInfo = new MovieInfo();

		/// <summary>
		/// 動画詳細を更新
		/// </summary>
		public void UpdateMovieInfo(MovieInfo movieInfo)
		{
			this.movieInfo = movieInfo;
		}

		/// <summary>
		/// サイズ変更イベント
		/// 書き込み欄のサイズ自動調節
		/// </summary>
		private void writeField_SizeChanged(object sender, EventArgs e)
		{
			Logger.Instance.Debug("writeField_SizeChanged()");
			Height = writeField.Height + movieDetail.Height;
			movieDetail.Top = writeField.Height;
			// サイズ変更時に親が最大化されていたら、詳細を非表示にする設定
			if (PlayerSettings.HideStatusBarOnFullscreen && ParentForm.WindowState == FormWindowState.Maximized)
			{
				Height = writeField.Height;
				movieDetail.Hide();
			}
			else
			{
				Height = writeField.Height + movieDetail.Height;
				movieDetail.Show();				
			}
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		public void Close()
		{
			Logger.Instance.Debug("Close()");
			writeField.Close();
		}

		/// <summary>
		/// 新着レス取得
		/// </summary>
		public string ReadNewRes()
		{
			return writeField.ReadNewRes();
		}

		/// <summary>
		/// メッセージ表示時間
		/// </summary>
		private const int MessageDisplayTime = 2000;
		Timer messageDisplayTimer = new Timer();

		/// <summary>
		/// メッセージ表示
		/// </summary>
		public void ShowMessage(string text)
		{
			messageDisplayTimer.Stop();
			movieDetail.ChannelDetail = text;
			messageDisplayTimer.Interval = MessageDisplayTime;
			messageDisplayTimer.Tick += (sender, e) =>
			{
				UpdateChannelDetail();
			};
			messageDisplayTimer.Start();
		}

		/// <summary>
		/// 動画詳細を更新
		/// </summary>
		private void UpdateChannelDetail()
		{
			string detail = channelDetail;

			// データが取得できるまで非表示
			if (!string.IsNullOrEmpty(movieInfo.Status))
			{
				if (PlayerSettings.DisplayFps || PlayerSettings.DisplayBitrate)
				{
					detail += " (";
				}

				// FPS表示
				if (PlayerSettings.DisplayFps)
				{
					detail += string.Format("{0}fps", movieInfo.NowFps);
				}

				if (PlayerSettings.DisplayFps && PlayerSettings.DisplayBitrate)
				{
					detail += " ";
				}

				// ビットレート表示
				if (PlayerSettings.DisplayBitrate)
				{
					detail += string.Format("{0}kbps", movieInfo.NowBitrate);
				}

				// リスナー数表示
				if (PlayerSettings.DisplayListenerNumber)
				{
					detail += string.Format(" [{0}/{1}]", movieInfo.ListenerNumber, movieInfo.RelayNumber);
				}

				if (PlayerSettings.DisplayFps || PlayerSettings.DisplayBitrate)
				{
					detail += ")";
				}
			}

			movieDetail.ChannelDetail = detail;
		}
	}

	/// <summary>
	/// 動画詳細情報
	/// </summary>
	public class MovieInfo
	{
		/// <summary>
		/// 現在のFPS
		/// </summary>
		public int NowFps = 0;

		/// <summary>
		/// FPS
		/// </summary>
		public int Fps = 0;

		/// <summary>
		/// 現在のビットレート
		/// </summary>
		public int NowBitrate = 0;

		/// <summary>
		/// ビットレート
		/// </summary>
		public int Bitrate = 0;

		/// <summary>
		/// リスナー数
		/// </summary>
		public string ListenerNumber;

		/// <summary>
		/// リレー数
		/// </summary>
		public string RelayNumber;

		/// <summary>
		/// ストリームタイプ
		/// </summary>
		public string StreamType;

		/// <summary>
		/// ステータス
		/// </summary>
		public string Status;
	}
}
