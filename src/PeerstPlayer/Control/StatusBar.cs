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
	// ステータスバーコントロール
	public partial class StatusBar : UserControl
	{
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
					writeField.Height = writeField.PreferredSize.Height;
				}
				else
				{
					writeField.Height = 0;
				}

				// 高さ変更イベント
				if (HeightChanged != null) HeightChanged(this, new EventArgs());
			}
		}

		// 高さ変更イベント
		public event EventHandler HeightChanged;

		// 音量クリックイベント
		public event EventHandler VolumeClick;

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：イベント登録
		//-------------------------------------------------------------
		public StatusBar()
		{
			InitializeComponent();

			// サイズ変更イベント登録
			writeField.SizeChanged += writeField_SizeChanged;
			writeField.HeightChanged += (sender, e) =>
			{
				if (HeightChanged != null) HeightChanged(sender, e);
			};

			// チャンネル詳細クリック
			movieDetail.ChannelDetailClick += (sender, e) =>
			{
				WriteFieldVisible = !WriteFieldVisible;
			};

			// 音量クリック
			movieDetail.VolumeClick += (sender, e) =>
			{
				// 音量クリックイベント
				if (VolumeClick != null) VolumeClick(sender, e);
			};
		}

		#region 非公開プロパティ

		//-------------------------------------------------------------
		// 概要：サイズ変更イベント
		// 詳細：書き込み欄のサイズ自動調節
		//-------------------------------------------------------------
		private void writeField_SizeChanged(object sender, EventArgs e)
		{
			Height = writeField.Height + movieDetail.Height;
			movieDetail.Top = writeField.Height;
		}

		#endregion
	}
}
