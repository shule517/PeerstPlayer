using System;
using System.Windows.Forms;
using PeerstLib.Utility;

namespace PeerstPlayer.Control
{
	//-------------------------------------------------------------
	// 概要：ステータスバーコントロール
	//-------------------------------------------------------------
	public partial class StatusBar : UserControl
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		// チャンネル詳細
		public string ChannelDetail
		{
			get { return movieDetail.ChannelDetail; }
			set { movieDetail.ChannelDetail = value; }
		}

		// 動画ステータス
		public string MovieStatus
		{
			get { return movieDetail.MovieStatus; }
			set { movieDetail.MovieStatus = value; }
		}

		// 音量
		public string Volume
		{
			get { return movieDetail.Volume; }
			set { movieDetail.Volume = value; }
		}
		
		// 選択スレッドURL
		public string SelectThreadUrl
		{
			get { return writeField.SelectThreadUrl; }
			set { writeField.SelectThreadUrl = value; }
		}

		// 書き込み欄の表示
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

		// 高さ変更イベント
		public event EventHandler HeightChanged = delegate { };

		// ステータスバークリックイベント
		public event MouseEventHandler ChannelDetailClick = delegate { };

		// スレッドタイトル右クリック
		public event EventHandler ThreadTitleRightClick = delegate { };

		// 音量クリックイベント
		public event EventHandler VolumeClick
		{
			add { movieDetail.VolumeClick += value; }
			remove { movieDetail.VolumeClick -= value; }
		}

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：イベント登録
		//-------------------------------------------------------------
		public StatusBar()
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

		//-------------------------------------------------------------
		// 概要：サイズ変更イベント
		// 詳細：書き込み欄のサイズ自動調節
		//-------------------------------------------------------------
		private void writeField_SizeChanged(object sender, EventArgs e)
		{
			Logger.Instance.Debug("writeField_SizeChanged()");
			Height = writeField.Height + movieDetail.Height;
			movieDetail.Top = writeField.Height;
		}
	}
}
