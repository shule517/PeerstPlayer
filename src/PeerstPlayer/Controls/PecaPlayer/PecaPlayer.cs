using PeerstLib.PeerCast;
using PeerstLib.PeerCast.Data;
using PeerstLib.PeerCast.Util;
using PeerstLib.Util;
using PeerstPlayer.Controls.MoviePlayer;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;

namespace PeerstPlayer.Controls.PecaPlayer
{
	//-------------------------------------------------------------
	// 概要：ペカプレイヤークラスコントロール
	// 詳細：PeerCast対応の動画再生プレイヤー
	//-------------------------------------------------------------
	public partial class PecaPlayerControl : UserControl
	{
		//-------------------------------------------------------------
		// 公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// チャンネル情報
		/// </summary>
		public ChannelInfo ChannelInfo { get; set; }

		/// <summary>
		/// チャンネル情報変更イベント
		/// </summary>
		public event EventHandler ChannelInfoChange = delegate { };

		/// <summary>
		/// 音量
		/// </summary>
		public int Volume
		{
			get { return moviePlayer.Volume; }
			set { moviePlayer.Volume = value; }
		}

		/// <summary>
		/// 音量バランス
		/// </summary>
		public int VolumeBalance
		{
			get { return moviePlayer.VolumeBalance; }
			set { moviePlayer.VolumeBalance = value; }
		}

		/// <summary>
		/// 音量変更イベント
		/// </summary>
		public event EventHandler VolumeChange
		{
			add { moviePlayer.VolumeChange += value; }
			remove { moviePlayer.VolumeChange -= value; }
		}

		/// <summary>
		/// 動画サイズ変更イベント
		/// </summary>
		public event EventHandler MovieSizeChange = delegate { };

		/// <summary>
		/// ミュート
		/// </summary>
		public bool Mute
		{
			get { return moviePlayer.Mute; }
			set { moviePlayer.Mute = value; }
		}

		/// <summary>
		/// 再生時間
		/// </summary>
		public string Duration
		{
			get { return moviePlayer.Duration; }
		}

		/// <summary>
		/// バッファー率
		/// </summary>
		public int BufferingProgress
		{
			get { return moviePlayer.BufferingProgress; }
		}

		/// <summary>
		/// 再生状態
		/// </summary>
		public WMPPlayState PlayState
		{
			get { return moviePlayer.PlayState; }
		}

		/// <summary>
		/// 動画再生状態
		/// </summary>
		public WMPOpenState OpenState
		{
			get { return moviePlayer.OpenState; }
		}

		/// <summary>
		/// 動画再生開始イベント
		/// </summary>
		public event EventHandler MovieStart
		{
			add { moviePlayer.MovieStart += value; }
			remove { moviePlayer.MovieStart -= value; }
		}

		/// <summary>
		/// クリック開始位置(マウスジェスチャ用)
		/// </summary>
		public Point ClickPoint { get; set; }

		/// <summary>
		/// アスペクト比
		/// </summary>
		public float AspectRate
		{
			get { return moviePlayer.AspectRate; }
		}

		/// <summary>
		/// 動画の幅
		/// </summary>
		public int ImageWidth
		{
			get { return moviePlayer.ImageWidth; }
		}

		/// <summary>
		/// 動画の高さ
		/// </summary>
		public int ImageHeight
		{
			get { return moviePlayer.ImageHeight; }
		}

		/// <summary>
		/// マウス押下イベント
		/// </summary>
		public event AxWMPLib._WMPOCXEvents_MouseDownEventHandler MouseDownEvent
		{
			add { moviePlayer.MouseDownEvent += value; }
			remove { moviePlayer.MouseDownEvent -= value; }
		}

		/// <summary>
		///  マウスアップイベント
		/// </summary>
		public event AxWMPLib._WMPOCXEvents_MouseUpEventHandler MouseUpEvent
		{
			add { moviePlayer.MouseUpEvent += value; }
			remove { moviePlayer.MouseUpEvent -= value; }
		}

		/// <summary>
		///  マウス移動イベント
		/// </summary>
		public event AxWMPLib._WMPOCXEvents_MouseMoveEventHandler MouseMoveEvent
		{
			add { moviePlayer.MouseMoveEvent += value; }
			remove { moviePlayer.MouseMoveEvent -= value; }
		}

		/// <summary>
		/// キー押下イベント
		/// </summary>
		public event AxWMPLib._WMPOCXEvents_KeyDownEventHandler KeyDownEvent
		{
			add { moviePlayer.KeyDownEvent += value; }
			remove { moviePlayer.KeyDownEvent -= value; }
		}

		/// <summary>
		/// ダブルクリックイベント
		/// </summary>
		public event AxWMPLib._WMPOCXEvents_DoubleClickEventHandler DoubleClickEvent
		{
			add { moviePlayer.DoubleClickEvent += value; }
			remove { moviePlayer.DoubleClickEvent -= value; }
		}

		/// <summary>
		/// WMPのハンドル
		/// </summary>
		public IntPtr WMPHandle { get { return moviePlayer.WMPHandle; } }

		/// <summary>
		/// コンテキストメニューの有効
		/// </summary>
		public bool EnableContextMenu
		{
			get { return moviePlayer.EnableContextMenu; }
			set { moviePlayer.EnableContextMenu = value; }
		}

		/// <summary>
		/// WMPを使用しているか
		/// </summary>
		public bool UsedWMP { get { return moviePlayer is WindowsMediaPlayerControl; } }

		/// <summary>
		/// FlashPlayerを使用しているか
		/// </summary>
		public bool UsedFlash { get { return moviePlayer is FlashMoviePlayerControl; } }

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// 動画プレイヤー
		/// </summary>
		private IMoviePlayer moviePlayer = null;

		/// <summary>
		/// PeerCast通信
		/// </summary>
		private PeerCastConnection pecaConnect = null;

		/// <summary>
		/// チャンネル更新用
		/// </summary>
		private BackgroundWorker updateChannelInfoWorker = new BackgroundWorker();

		//-------------------------------------------------------------
		// 定義
		//-------------------------------------------------------------

		/// <summary>
		/// チャンネル更新間隔
		/// </summary>
		private const int UpdateInterval = 60000;

		/// <summary>
		/// チャンネル更新のリトライ回数
		/// </summary>
		private const int UpdateRetryCount = 5;

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：コントロールの初期化
		//-------------------------------------------------------------
		public PecaPlayerControl()
		{
			Logger.Instance.Debug("PecaPlayer()");
			InitializeComponent();

			// チャンネル情報更新スレッド：キャンセル許可
			updateChannelInfoWorker.WorkerSupportsCancellation = true;

			this.SuspendLayout();
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			if (commandLineArgs.Length > 2 && commandLineArgs[2] == "FLV")
			{
				moviePlayer = new FlashMoviePlayerControl(this);
			}
			else if (commandLineArgs.Length > 2 && commandLineArgs[2] == "WMV")
			{
				moviePlayer = new WindowsMediaPlayerControl();
			}
			else
			{
				moviePlayer = new VlcMediaPlayerControl();
			}
			this.Controls.Add(this.moviePlayer.MovieControl);
			this.ResumeLayout(false);

			// ウィンドウドラッグ用
			moviePlayer.MouseDownEvent += (sender, e) =>
			{
				// クリック開始位置を保持
				ClickPoint = new Point(e.fX, e.fY);
			};
		}

		//-------------------------------------------------------------
		// 概要：指定URLを再生
		// 詳細：動画を再生し、チャンネル情報を取得する
		//-------------------------------------------------------------
		public void Open(string streamUrl)
		{
			Logger.Instance.DebugFormat("Open(streamUrl:{0})", streamUrl);

			// PeerCast通信の準備
			StreamUrlInfo info = StreamUrlAnalyzer.GetUrlInfo(streamUrl);
			pecaConnect = new PeerCastConnection(info);

			// 動画再生開始
			moviePlayer.PlayMoive(streamUrl);

			// チャンネル更新スレッド
			updateChannelInfoWorker.DoWork += (sender, e) =>
			{
				for (int i = 0; i < UpdateRetryCount; i++)
				{
					Logger.Instance.DebugFormat("チャンネル更新トライ[{0}回目]", i+1);
					ChannelInfo chInfo = pecaConnect.GetChannelInfo();
					if (!String.IsNullOrEmpty(chInfo.Name))
					{
						ChannelInfo = chInfo;
						e.Result = true;
						return;
					}

					System.Threading.Thread.Sleep(1000);
				}
				Logger.Instance.Debug("チャンネル更新に失敗しました。");
				e.Result = false;
			};
			updateChannelInfoWorker.RunWorkerCompleted += (sender, e) =>
			{
				if ((bool)e.Result)
				{
					Logger.Instance.InfoFormat("チャンネル更新完了 [チャンネル名:{0}] [ジャンル：{1}] [詳細:{2}] [コメント:{3}] [コンタクトURL:{4}]", ChannelInfo.Name, ChannelInfo.Genre, ChannelInfo.Desc, ChannelInfo.Comment, ChannelInfo.Url);
					ChannelInfoChange(sender, e);
				}
			};

			// チャンネル情報の取得
			Timer timer = new Timer();
			timer.Interval = UpdateInterval;
			timer.Tick += (sender, e) =>
			{
				// チャンネル更新
				UpdateChannelInfo();

				/*
				// メモリリーク防止
				if (moviePlayer.Ctlcontrols != null)
				{
					moviePlayer.Ctlcontrols.play();
				}
				 */
			};
			timer.Start();

			// チャンネル更新
			UpdateChannelInfo();
		}

		//-------------------------------------------------------------
		// 概要：チャンネル更新
		//-------------------------------------------------------------
		public void UpdateChannelInfo()
		{
			// チャンネル更新
			if (!updateChannelInfoWorker.IsBusy)
			{
				Logger.Instance.Info("チャンネル更新開始");
				updateChannelInfoWorker.RunWorkerAsync();
			}
		}

		//-------------------------------------------------------------
		// 概要：動画サイズ設定
		//-------------------------------------------------------------
		public void SetSize(int width, int height)
		{
			Size = new Size(width, height);
			MovieSizeChange(this, new EventArgs());
		}

		//-------------------------------------------------------------
		// 概要：Bump
		//-------------------------------------------------------------
		public void Bump()
		{
			if (pecaConnect == null) return;
			pecaConnect.Bump();
		}

		//-------------------------------------------------------------
		// 概要：リレー切断
		//-------------------------------------------------------------
		public void DisconnectRelay()
		{
			if (pecaConnect == null) return;
			pecaConnect.DisconnectRelay();
		}

		//-------------------------------------------------------------
		// 概要：終了処理
		//-------------------------------------------------------------
		public void Close()
		{
			Logger.Instance.Debug("Close()");
			if (updateChannelInfoWorker.IsBusy)
			{
				updateChannelInfoWorker.CancelAsync();
			}
		}

		//-------------------------------------------------------------
		// 概要：FLV用動画情報表示
		//-------------------------------------------------------------
		public void ShowDebug()
		{
			if (moviePlayer is FlashMoviePlayerControl)
			{
				((FlashMoviePlayerControl)moviePlayer).ShowDebug();
			}
		}

		/// <summary>
		/// 現在のフレームレート
		/// </summary>
		public int NowFrameRate { get { return moviePlayer.NowFrameRate; } }

		/// <summary>
		/// フレームレート
		/// </summary>
		public int FrameRate { get { return moviePlayer.FrameRate; } }

		/// <summary>
		/// 現在のビットレート
		/// </summary>
		public int NowBitrate { get { return moviePlayer.NowBitrate; } }

		/// <summary>
		/// ビットレート
		/// </summary>
		public int Bitrate { get { return moviePlayer.Bitrate; } }
	}
}
