using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PeerstLib.PeerCast;
using PeerstLib.PeerCast.Data;
using PeerstLib.PeerCast.Util;
using PeerstLib.Util;
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
			get { return wmp.settings.volume; }
			set
			{
				wmp.settings.volume = value;
				VolumeChange(this, new EventArgs());
			}
		}

		/// <summary>
		/// 音量変更イベント
		/// </summary>
		public event EventHandler VolumeChange = delegate { };

		/// <summary>
		/// 動画サイズ変更イベント
		/// </summary>
		public event EventHandler MovieSizeChange = delegate { };

		/// <summary>
		/// ミュート
		/// </summary>
		public bool Mute
		{
			get { return wmp.settings.mute; }
			set
			{
				wmp.settings.mute = value;
				VolumeChange(this, new EventArgs());
			}
		}

		/// <summary>
		/// 再生時間
		/// </summary>
		public string Duration
		{
			get
			{
				string position = wmp.Ctlcontrols.currentPositionString;
				if (position.Length == 5)
				{
					position = "00:" + position;
				}

				return position;
			}
		}

		/// <summary>
		/// バッファー率
		/// </summary>
		public int BufferingProgress
		{
			get { return wmp.network.bufferingProgress; }
		}

		/// <summary>
		/// 再生状態
		/// </summary>
		public WMPPlayState PlayState
		{
			get { return wmp.playState; }
		}

		/// <summary>
		/// 動画再生状態
		/// </summary>
		public WMPOpenState OpenState
		{
			get { return wmp.openState; }
		}

		/// <summary>
		/// 動画再生開始イベント
		/// </summary>
		public event EventHandler MovieStart = delegate { };

		/// <summary>
		/// クリック開始位置(マウスジェスチャ用)
		/// </summary>
		public Point ClickPoint { get; set; }

		/// <summary>
		/// アスペクト比
		/// </summary>
		public float AspectRate
		{
			get { return (float)ImageWidth / (float)ImageHeight; }
		}

		/// <summary>
		/// 動画の幅
		/// </summary>
		public int ImageWidth
		{
			get { return ((wmp.currentMedia == null) || (wmp.currentMedia.imageSourceWidth == 0)) ? 800 : wmp.currentMedia.imageSourceWidth; }
		}

		/// <summary>
		/// 動画の高さ
		/// </summary>
		public int ImageHeight
		{
			get { return ((wmp.currentMedia == null) || (wmp.currentMedia.imageSourceHeight == 0)) ? 600 : wmp.currentMedia.imageSourceHeight; }
		}

		/// <summary>
		/// マウス押下イベント
		/// </summary>
		public event AxWMPLib._WMPOCXEvents_MouseDownEventHandler MouseDownEvent
		{
			add { wmp.MouseDownEvent += value; }
			remove { wmp.MouseDownEvent -= value; }
		}

		/// <summary>
		///  マウスアップイベント
		/// </summary>
		public event AxWMPLib._WMPOCXEvents_MouseUpEventHandler MouseUpEvent
		{
			add { wmp.MouseUpEvent += value; }
			remove { wmp.MouseUpEvent -= value; }
		}

		/// <summary>
		///  マウス移動イベント
		/// </summary>
		public event AxWMPLib._WMPOCXEvents_MouseMoveEventHandler MouseMoveEvent
		{
			add { wmp.MouseMoveEvent += value; }
			remove { wmp.MouseMoveEvent -= value; }
		}

		/// <summary>
		/// キー押下イベント
		/// </summary>
		public event AxWMPLib._WMPOCXEvents_KeyDownEventHandler KeyDownEvent
		{
			add { wmp.KeyDownEvent += value; }
			remove { wmp.KeyDownEvent -= value; }
		}

		/// <summary>
		/// ダブルクリックイベント
		/// </summary>
		public event EventHandler DoubleClickEvent = delegate { };

		/// <summary>
		/// WMPのハンドル
		/// </summary>
		public IntPtr WMPHandle { get { return wmp.Handle; } }

		/// <summary>
		/// コンテキストメニューの有効
		/// </summary>
		public bool EnableContextMenu
		{
			get { return wmp.enableContextMenu; }
			set { wmp.enableContextMenu = value; }
		}

		//-------------------------------------------------------------
		// 非公開プロパティ
		//-------------------------------------------------------------

		/// <summary>
		/// PeerCast通信
		/// </summary>
		private PeerCastConnection pecaConnect = null;

		/// <summary>
		/// チャンネル更新用
		/// </summary>
		private BackgroundWorker updateChannelInfoWorker = new BackgroundWorker();

		/// <summary>
		/// 初回ファイルオープンフラグ(MovieStartに使用)
		/// </summary>
		private bool isFirstMediaOpen = true;

		//-------------------------------------------------------------
		// 定義
		//-------------------------------------------------------------

		// チャンネル更新間隔
		private const int UpdateInterval = 60000;

		//-------------------------------------------------------------
		// 概要：コンストラクタ
		// 詳細：コントロールの初期化
		//-------------------------------------------------------------
		public PecaPlayerControl()
		{
			Logger.Instance.Debug("PecaPlayer()");
			InitializeComponent();

			// 初期設定
			wmp.uiMode = "none";
			wmp.stretchToFit = true;
			wmp.enableContextMenu = false;

			// キャンセル許可
			updateChannelInfoWorker.WorkerSupportsCancellation = true;

			// ダブルクリックイベント
			new WmpNativeWindow(wmp.Handle).DoubleClick += (sender, e) => DoubleClickEvent(sender, e);

			// チャンネル自動リトライ
			new ChannelAutoRetry(wmp);

			// WMPフルスクリーンを無効
			wmp.MouseDownEvent += (sender, e) =>
			{
				// クリック開始位置を設定
				ClickPoint = new System.Drawing.Point(e.fX, e.fY);

				// WMPのフルクリーンを解除
				if (wmp.fullScreen)
				{
					wmp.fullScreen = false;
				}
			};
		}

		//-------------------------------------------------------------
		// 概要：指定URLを再生
		// 詳細：動画を再生し、チャンネル情報を取得する
		//-------------------------------------------------------------
		public void Open(string streamUrl)
		{
			Logger.Instance.DebugFormat("Open(streamUrl:{0})", streamUrl);

			StreamUrlInfo info = StreamUrlAnalyzer.GetUrlInfo(streamUrl);
			pecaConnect = new PeerCastConnection(info);

			// 初回ファイルオープンフラグをリセット
			isFirstMediaOpen = true;

			// 動画の再生
			wmp.URL = streamUrl;

			// 再生変更イベント
			wmp.OpenStateChange += (sender, e) =>
			{
				Logger.Instance.Debug(String.Format("OpenStateChange [{0}]", wmp.openState.ToString()));
				// 動画切替時に、音量が初期化されるための対応
				// TODO ミュート時に音量が変わらないようにする
				VolumeChange(this, new EventArgs());

				// 動画再生開始イベント
				if ((wmp.openState == WMPOpenState.wmposMediaOpen) && isFirstMediaOpen)
				{
					isFirstMediaOpen = false;
					MovieStart(this, new EventArgs());
				}
			};

			// チャンネル更新スレッド
			updateChannelInfoWorker.DoWork += (sender, e) =>
			{
				while (true)
				{
					Logger.Instance.Debug("チャンネル更新トライ");
					ChannelInfo chInfo = pecaConnect.GetChannelInfo();
					if (!String.IsNullOrEmpty(chInfo.Name))
					{
						ChannelInfo = chInfo;
						return;
					}

					System.Threading.Thread.Sleep(3000);
				}
			};
			updateChannelInfoWorker.RunWorkerCompleted += (sender, e) =>
			{
				Logger.Instance.InfoFormat("チャンネル更新完了 [チャンネル名:{0}] [ジャンル：{1}] [詳細:{2}] [コメント:{3}] [コンタクトURL:{4}]", ChannelInfo.Name, ChannelInfo.Genre, ChannelInfo.Desc, ChannelInfo.Comment, ChannelInfo.Url);
				ChannelInfoChange(sender, e);
			};

			// チャンネル情報の取得
			Timer timer = new Timer();
			timer.Interval = UpdateInterval;
			timer.Tick += (sender, e) =>
			{
				// チャンネル更新
				UpdateChannelInfo();

				// メモリリーク防止
				if (wmp.Ctlcontrols != null)
				{
					wmp.Ctlcontrols.play();
				}
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
	}
}
