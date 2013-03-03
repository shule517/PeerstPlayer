using System.Windows.Forms;
namespace PeerstPlayer
{
	partial class MainForm
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.panelWMP = new System.Windows.Forms.Panel();
			this.panelResBox = new System.Windows.Forms.Panel();
			this.resBox = new System.Windows.Forms.TextBox();
			this.labelThreadTitle = new System.Windows.Forms.Label();
			this.contextMenuStripResBox = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.スレッドビューワを開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.コンタクトＵＲＬをブラウザで開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.チャンネル情報を更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.スレッドURLをコピーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxThreadURL = new System.Windows.Forms.ToolStripTextBox();
			this.panelStatusLabel = new System.Windows.Forms.Panel();
			this.panelDetailRight = new System.Windows.Forms.Panel();
			this.labelVolume = new System.Windows.Forms.Label();
			this.labelDuration = new System.Windows.Forms.Label();
			this.labelDetail = new System.Windows.Forms.Label();
			this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
			this.timerUpdateStatubar = new System.Windows.Forms.Timer(this.components);
			this.timerLoadIni = new System.Windows.Forms.Timer(this.components);
			this.toolTipDetail = new System.Windows.Forms.ToolTip(this.components);
			this.contextMenuStripWMP = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.最前列表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.最小化ミュートToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.フルスクリーンToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.動画サイズに合わせるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorView = new System.Windows.Forms.ToolStripSeparator();
			this.タイトルバーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.レスボックスToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ステータスバーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.フレームToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.機能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.リトライToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bump再接続ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.チャンネル情報を更新するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.スレッドビューワを開くToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.スクリーンショットToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.スクリーンショットフォルダを開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.音量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.上げるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.下げるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ミュートToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.バランスToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.中央ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.左ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.右ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.デフォルトToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxX = new System.Windows.Forms.ToolStripTextBox();
			this.yToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxY = new System.Windows.Forms.ToolStripTextBox();
			this.幅ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox幅 = new System.Windows.Forms.ToolStripTextBox();
			this.高さToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox高さ = new System.Windows.Forms.ToolStripTextBox();
			this.音量ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox音量 = new System.Windows.Forms.ToolStripTextBox();
			this.拡大率ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxScale = new System.Windows.Forms.ToolStripTextBox();
			this.動画サイズに合わせるToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.最前列表示ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.タイトルバーToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.レスボックスToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.ステータスバーToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.フレームToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.状態保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.現在の状態を保存ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.位置を保存ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.サイズを保存ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.音量を保存ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.終了時ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.位置を保存するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.サイズを保存するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.音量を保存するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.リレーを切断するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.スレッドビューワを終了するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.フォントToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.フォントToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.レスボックスの操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enter改行ShiftEnter書き込みToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enter書き込みShiftEnter改行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.アスペクト比を維持ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.書き込み時にレスボックスを非表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.レスボックスを自動的に隠すToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.マウスジェスチャーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.スクリーン吸着ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.スクリーン吸着範囲ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxScreenMagnetDockDist = new System.Windows.Forms.ToolStripTextBox();
			this.マウスジェスチャー感度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxマウスジェスチャー感度 = new System.Windows.Forms.ToolStripTextBox();
			this.bSでレスボックスを閉じるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uRLを開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxURL = new System.Windows.Forms.ToolStripTextBox();
			this.開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.クリップボードから開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.終了するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.リレーを切断して終了するToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.wMPメニューToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timerUpdateChannelInfo = new System.Windows.Forms.Timer(this.components);
			this.toolStrip = new PeerstPlayer.ToolStripEx();
			this.toolStripDropDownButtonDivide = new System.Windows.Forms.ToolStripDropDownButton();
			this.幅5分の1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.幅4分の1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.幅3分の1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.幅2分の1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.幅1分の1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorDivid1 = new System.Windows.Forms.ToolStripSeparator();
			this.高さ5分の1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.高さ4分の1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.高さ3分の1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.高さ2分の1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.高さ1分の1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorDivid2 = new System.Windows.Forms.ToolStripSeparator();
			this.分割5X5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.分割4X4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.分割3X3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.分割2X2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.分割1X1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripDropDownButtonSize = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.x120ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x240ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x360ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x480ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x600ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorSize1 = new System.Windows.Forms.ToolStripSeparator();
			this.幅160ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.幅320ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.幅480ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.幅640ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.幅800ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparatorSize2 = new System.Windows.Forms.ToolStripSeparator();
			this.高さ120ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.高さ240ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.高さ360ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.高さ480ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.高さ600ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripButtonOpenViewer = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonMin = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonMax = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
			this.panelWMP.SuspendLayout();
			this.panelResBox.SuspendLayout();
			this.contextMenuStripResBox.SuspendLayout();
			this.panelStatusLabel.SuspendLayout();
			this.panelDetailRight.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
			this.contextMenuStripWMP.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelWMP
			// 
			this.panelWMP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelWMP.Controls.Add(this.toolStrip);
			this.panelWMP.Location = new System.Drawing.Point(0, 0);
			this.panelWMP.Name = "panelWMP";
			this.panelWMP.Size = new System.Drawing.Size(480, 360);
			this.panelWMP.TabIndex = 0;
			// 
			// panelResBox
			// 
			this.panelResBox.Controls.Add(this.resBox);
			this.panelResBox.Controls.Add(this.labelThreadTitle);
			this.panelResBox.Location = new System.Drawing.Point(0, 360);
			this.panelResBox.Name = "panelResBox";
			this.panelResBox.Size = new System.Drawing.Size(480, 32);
			this.panelResBox.TabIndex = 4;
			// 
			// resBox
			// 
			this.resBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.resBox.Location = new System.Drawing.Point(0, 13);
			this.resBox.Multiline = true;
			this.resBox.Name = "resBox";
			this.resBox.Size = new System.Drawing.Size(480, 19);
			this.resBox.TabIndex = 1;
			this.resBox.WordWrap = false;
			this.resBox.TextChanged += new System.EventHandler(this.resBox_TextChanged);
			this.resBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.resBox_KeyDown);
			// 
			// labelThreadTitle
			// 
			this.labelThreadTitle.BackColor = System.Drawing.Color.Black;
			this.labelThreadTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelThreadTitle.ForeColor = System.Drawing.Color.SpringGreen;
			this.labelThreadTitle.Location = new System.Drawing.Point(0, 0);
			this.labelThreadTitle.Name = "labelThreadTitle";
			this.labelThreadTitle.Size = new System.Drawing.Size(480, 32);
			this.labelThreadTitle.TabIndex = 2;
			this.labelThreadTitle.Text = "読み込み中...";
			this.labelThreadTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelThreadTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelThreadTitle_MouseDown);
			// 
			// contextMenuStripResBox
			// 
			this.contextMenuStripResBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.スレッドビューワを開くToolStripMenuItem,
            this.コンタクトＵＲＬをブラウザで開くToolStripMenuItem,
            this.チャンネル情報を更新ToolStripMenuItem,
            this.スレッドURLをコピーToolStripMenuItem,
            this.toolStripTextBoxThreadURL});
			this.contextMenuStripResBox.Name = "contextMenuStripResBox";
			this.contextMenuStripResBox.Size = new System.Drawing.Size(361, 119);
			this.contextMenuStripResBox.Opened += new System.EventHandler(this.contextMenuStripResBox_Opened);
			// 
			// スレッドビューワを開くToolStripMenuItem
			// 
			this.スレッドビューワを開くToolStripMenuItem.Name = "スレッドビューワを開くToolStripMenuItem";
			this.スレッドビューワを開くToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
			this.スレッドビューワを開くToolStripMenuItem.Text = "スレッドビューワを開く";
			this.スレッドビューワを開くToolStripMenuItem.Click += new System.EventHandler(this.スレッドビューワを開くToolStripMenuItem_Click);
			// 
			// コンタクトＵＲＬをブラウザで開くToolStripMenuItem
			// 
			this.コンタクトＵＲＬをブラウザで開くToolStripMenuItem.Name = "コンタクトＵＲＬをブラウザで開くToolStripMenuItem";
			this.コンタクトＵＲＬをブラウザで開くToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
			this.コンタクトＵＲＬをブラウザで開くToolStripMenuItem.Text = "コンタクトＵＲＬをブラウザで開く";
			this.コンタクトＵＲＬをブラウザで開くToolStripMenuItem.Click += new System.EventHandler(this.コンタクトＵＲＬをブラウザで開くToolStripMenuItem_Click);
			// 
			// チャンネル情報を更新ToolStripMenuItem
			// 
			this.チャンネル情報を更新ToolStripMenuItem.Name = "チャンネル情報を更新ToolStripMenuItem";
			this.チャンネル情報を更新ToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
			this.チャンネル情報を更新ToolStripMenuItem.Text = "チャンネル情報を更新";
			this.チャンネル情報を更新ToolStripMenuItem.Click += new System.EventHandler(this.チャンネル情報を更新するToolStripMenuItem_Click);
			// 
			// スレッドURLをコピーToolStripMenuItem
			// 
			this.スレッドURLをコピーToolStripMenuItem.Name = "スレッドURLをコピーToolStripMenuItem";
			this.スレッドURLをコピーToolStripMenuItem.Size = new System.Drawing.Size(360, 22);
			this.スレッドURLをコピーToolStripMenuItem.Text = "スレッドURLをコピー";
			this.スレッドURLをコピーToolStripMenuItem.Click += new System.EventHandler(this.スレッドURLをコピーToolStripMenuItem_Click);
			// 
			// toolStripTextBoxThreadURL
			// 
			this.toolStripTextBoxThreadURL.Name = "toolStripTextBoxThreadURL";
			this.toolStripTextBoxThreadURL.Size = new System.Drawing.Size(300, 25);
			this.toolStripTextBoxThreadURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxThreadURL_KeyDown);
			// 
			// panelStatusLabel
			// 
			this.panelStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelStatusLabel.BackColor = System.Drawing.Color.Black;
			this.panelStatusLabel.Controls.Add(this.panelDetailRight);
			this.panelStatusLabel.Controls.Add(this.labelDetail);
			this.panelStatusLabel.Controls.Add(this.pictureBoxIcon);
			this.panelStatusLabel.Location = new System.Drawing.Point(0, 484);
			this.panelStatusLabel.Name = "panelStatusLabel";
			this.panelStatusLabel.Size = new System.Drawing.Size(480, 16);
			this.panelStatusLabel.TabIndex = 5;
			this.panelStatusLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelStatusLabel_MouseDown);
			this.panelStatusLabel.MouseHover += new System.EventHandler(this.panelStatusLabel_MouseHover);
			// 
			// panelDetailRight
			// 
			this.panelDetailRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelDetailRight.Controls.Add(this.labelVolume);
			this.panelDetailRight.Controls.Add(this.labelDuration);
			this.panelDetailRight.Location = new System.Drawing.Point(393, 0);
			this.panelDetailRight.Name = "panelDetailRight";
			this.panelDetailRight.Size = new System.Drawing.Size(87, 18);
			this.panelDetailRight.TabIndex = 1;
			// 
			// labelVolume
			// 
			this.labelVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelVolume.AutoSize = true;
			this.labelVolume.ForeColor = System.Drawing.Color.SpringGreen;
			this.labelVolume.Location = new System.Drawing.Point(54, 4);
			this.labelVolume.Name = "labelVolume";
			this.labelVolume.Size = new System.Drawing.Size(23, 12);
			this.labelVolume.TabIndex = 2;
			this.labelVolume.Text = "100";
			this.labelVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelVolume.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelVolume_MouseDown);
			// 
			// labelDuration
			// 
			this.labelDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelDuration.AutoSize = true;
			this.labelDuration.ForeColor = System.Drawing.Color.SpringGreen;
			this.labelDuration.Location = new System.Drawing.Point(3, 4);
			this.labelDuration.Name = "labelDuration";
			this.labelDuration.Size = new System.Drawing.Size(29, 12);
			this.labelDuration.TabIndex = 1;
			this.labelDuration.Text = "停止";
			this.labelDuration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelDuration.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelStatusLabel_MouseDown);
			this.labelDuration.MouseHover += new System.EventHandler(this.panelStatusLabel_MouseHover);
			// 
			// labelDetail
			// 
			this.labelDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.labelDetail.AutoSize = true;
			this.labelDetail.ForeColor = System.Drawing.Color.SpringGreen;
			this.labelDetail.Location = new System.Drawing.Point(3, 4);
			this.labelDetail.Name = "labelDetail";
			this.labelDetail.Size = new System.Drawing.Size(75, 12);
			this.labelDetail.TabIndex = 0;
			this.labelDetail.Text = "チャンネル詳細";
			this.labelDetail.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelStatusLabel_MouseDown);
			this.labelDetail.MouseHover += new System.EventHandler(this.panelStatusLabel_MouseHover);
			// 
			// pictureBoxIcon
			// 
			this.pictureBoxIcon.ImageLocation = "";
			this.pictureBoxIcon.Location = new System.Drawing.Point(2, 1);
			this.pictureBoxIcon.Name = "pictureBoxIcon";
			this.pictureBoxIcon.Size = new System.Drawing.Size(18, 18);
			this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxIcon.TabIndex = 4;
			this.pictureBoxIcon.TabStop = false;
			// 
			// timerUpdateStatubar
			// 
			this.timerUpdateStatubar.Enabled = true;
			this.timerUpdateStatubar.Interval = 1000;
			this.timerUpdateStatubar.Tick += new System.EventHandler(this.timerUpdateStatusbar_Tick);
			// 
			// timerLoadIni
			// 
			this.timerLoadIni.Enabled = true;
			this.timerLoadIni.Tick += new System.EventHandler(this.timerLoadIni_Tick);
			// 
			// contextMenuStripWMP
			// 
			this.contextMenuStripWMP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.表示ToolStripMenuItem,
            this.機能ToolStripMenuItem,
            this.音量ToolStripMenuItem,
            this.設定ToolStripMenuItem,
            this.ファイルToolStripMenuItem,
            this.wMPメニューToolStripMenuItem});
			this.contextMenuStripWMP.Name = "contextMenuStripWMP";
			this.contextMenuStripWMP.Size = new System.Drawing.Size(154, 136);
			// 
			// 表示ToolStripMenuItem
			// 
			this.表示ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最前列表示ToolStripMenuItem,
            this.最小化ミュートToolStripMenuItem,
            this.フルスクリーンToolStripMenuItem,
            this.動画サイズに合わせるToolStripMenuItem,
            this.toolStripSeparatorView,
            this.タイトルバーToolStripMenuItem,
            this.レスボックスToolStripMenuItem,
            this.ステータスバーToolStripMenuItem,
            this.フレームToolStripMenuItem});
			this.表示ToolStripMenuItem.Name = "表示ToolStripMenuItem";
			this.表示ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.表示ToolStripMenuItem.Text = "表示";
			this.表示ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.表示ToolStripMenuItem_DropDownOpened);
			// 
			// 最前列表示ToolStripMenuItem
			// 
			this.最前列表示ToolStripMenuItem.Name = "最前列表示ToolStripMenuItem";
			this.最前列表示ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.最前列表示ToolStripMenuItem.Text = "最前列表示";
			this.最前列表示ToolStripMenuItem.Click += new System.EventHandler(this.最前列表示ToolStripMenuItem_Click);
			// 
			// 最小化ミュートToolStripMenuItem
			// 
			this.最小化ミュートToolStripMenuItem.Name = "最小化ミュートToolStripMenuItem";
			this.最小化ミュートToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.最小化ミュートToolStripMenuItem.Text = "最小化ミュート";
			this.最小化ミュートToolStripMenuItem.Click += new System.EventHandler(this.最小化ミュートToolStripMenuItem_Click);
			// 
			// フルスクリーンToolStripMenuItem
			// 
			this.フルスクリーンToolStripMenuItem.Name = "フルスクリーンToolStripMenuItem";
			this.フルスクリーンToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.フルスクリーンToolStripMenuItem.Text = "フルスクリーン";
			this.フルスクリーンToolStripMenuItem.Click += new System.EventHandler(this.フルスクリーンToolStripMenuItem_Click);
			// 
			// 動画サイズに合わせるToolStripMenuItem
			// 
			this.動画サイズに合わせるToolStripMenuItem.Name = "動画サイズに合わせるToolStripMenuItem";
			this.動画サイズに合わせるToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.動画サイズに合わせるToolStripMenuItem.Text = "動画サイズに合わせる";
			this.動画サイズに合わせるToolStripMenuItem.Click += new System.EventHandler(this.動画サイズに合わせるToolStripMenuItem_Click);
			// 
			// toolStripSeparatorView
			// 
			this.toolStripSeparatorView.Name = "toolStripSeparatorView";
			this.toolStripSeparatorView.Size = new System.Drawing.Size(193, 6);
			// 
			// タイトルバーToolStripMenuItem
			// 
			this.タイトルバーToolStripMenuItem.Name = "タイトルバーToolStripMenuItem";
			this.タイトルバーToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.タイトルバーToolStripMenuItem.Text = "タイトルバー";
			this.タイトルバーToolStripMenuItem.Click += new System.EventHandler(this.タイトルバーToolStripMenuItem_Click);
			// 
			// レスボックスToolStripMenuItem
			// 
			this.レスボックスToolStripMenuItem.Name = "レスボックスToolStripMenuItem";
			this.レスボックスToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.レスボックスToolStripMenuItem.Text = "レスボックス";
			this.レスボックスToolStripMenuItem.Click += new System.EventHandler(this.レスボックスToolStripMenuItem_Click);
			// 
			// ステータスバーToolStripMenuItem
			// 
			this.ステータスバーToolStripMenuItem.Name = "ステータスバーToolStripMenuItem";
			this.ステータスバーToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.ステータスバーToolStripMenuItem.Text = "ステータスバー";
			this.ステータスバーToolStripMenuItem.Click += new System.EventHandler(this.ステータスバーToolStripMenuItem_Click);
			// 
			// フレームToolStripMenuItem
			// 
			this.フレームToolStripMenuItem.Name = "フレームToolStripMenuItem";
			this.フレームToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.フレームToolStripMenuItem.Text = "フレーム";
			this.フレームToolStripMenuItem.Click += new System.EventHandler(this.フレームToolStripMenuItem_Click);
			// 
			// 機能ToolStripMenuItem
			// 
			this.機能ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.リトライToolStripMenuItem,
            this.bump再接続ToolStripMenuItem,
            this.toolStripSeparator5,
            this.チャンネル情報を更新するToolStripMenuItem,
            this.スレッドビューワを開くToolStripMenuItem1,
            this.toolStripSeparator4,
            this.スクリーンショットToolStripMenuItem,
            this.スクリーンショットフォルダを開くToolStripMenuItem});
			this.機能ToolStripMenuItem.Name = "機能ToolStripMenuItem";
			this.機能ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.機能ToolStripMenuItem.Text = "機能";
			// 
			// リトライToolStripMenuItem
			// 
			this.リトライToolStripMenuItem.Name = "リトライToolStripMenuItem";
			this.リトライToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.リトライToolStripMenuItem.Text = "リトライ";
			this.リトライToolStripMenuItem.Click += new System.EventHandler(this.リトライToolStripMenuItem_Click);
			// 
			// bump再接続ToolStripMenuItem
			// 
			this.bump再接続ToolStripMenuItem.Name = "bump再接続ToolStripMenuItem";
			this.bump再接続ToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.bump再接続ToolStripMenuItem.Text = "Bump";
			this.bump再接続ToolStripMenuItem.Click += new System.EventHandler(this.bump再接続ToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(265, 6);
			// 
			// チャンネル情報を更新するToolStripMenuItem
			// 
			this.チャンネル情報を更新するToolStripMenuItem.Name = "チャンネル情報を更新するToolStripMenuItem";
			this.チャンネル情報を更新するToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.チャンネル情報を更新するToolStripMenuItem.Text = "チャンネル情報を更新";
			this.チャンネル情報を更新するToolStripMenuItem.Click += new System.EventHandler(this.チャンネル情報を更新するToolStripMenuItem_Click);
			// 
			// スレッドビューワを開くToolStripMenuItem1
			// 
			this.スレッドビューワを開くToolStripMenuItem1.Name = "スレッドビューワを開くToolStripMenuItem1";
			this.スレッドビューワを開くToolStripMenuItem1.Size = new System.Drawing.Size(268, 22);
			this.スレッドビューワを開くToolStripMenuItem1.Text = "スレッドビューワを開く";
			this.スレッドビューワを開くToolStripMenuItem1.Click += new System.EventHandler(this.スレッドビューワを開くToolStripMenuItem1_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(265, 6);
			// 
			// スクリーンショットToolStripMenuItem
			// 
			this.スクリーンショットToolStripMenuItem.Name = "スクリーンショットToolStripMenuItem";
			this.スクリーンショットToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.スクリーンショットToolStripMenuItem.Text = "スクリーンショット";
			this.スクリーンショットToolStripMenuItem.Click += new System.EventHandler(this.スクリーンショットToolStripMenuItem_Click);
			// 
			// スクリーンショットフォルダを開くToolStripMenuItem
			// 
			this.スクリーンショットフォルダを開くToolStripMenuItem.Name = "スクリーンショットフォルダを開くToolStripMenuItem";
			this.スクリーンショットフォルダを開くToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.スクリーンショットフォルダを開くToolStripMenuItem.Text = "スクリーンショットフォルダを開く";
			this.スクリーンショットフォルダを開くToolStripMenuItem.Click += new System.EventHandler(this.スクリーンショットフォルダを開くToolStripMenuItem_Click);
			// 
			// 音量ToolStripMenuItem
			// 
			this.音量ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上げるToolStripMenuItem,
            this.下げるToolStripMenuItem,
            this.ミュートToolStripMenuItem,
            this.バランスToolStripMenuItem});
			this.音量ToolStripMenuItem.Name = "音量ToolStripMenuItem";
			this.音量ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.音量ToolStripMenuItem.Text = "音量";
			this.音量ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.音量ToolStripMenuItem_DropDownOpened);
			// 
			// 上げるToolStripMenuItem
			// 
			this.上げるToolStripMenuItem.Name = "上げるToolStripMenuItem";
			this.上げるToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.上げるToolStripMenuItem.Text = "上げる";
			this.上げるToolStripMenuItem.Click += new System.EventHandler(this.上げるToolStripMenuItem_Click);
			// 
			// 下げるToolStripMenuItem
			// 
			this.下げるToolStripMenuItem.Name = "下げるToolStripMenuItem";
			this.下げるToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.下げるToolStripMenuItem.Text = "下げる";
			this.下げるToolStripMenuItem.Click += new System.EventHandler(this.下げるToolStripMenuItem_Click);
			// 
			// ミュートToolStripMenuItem
			// 
			this.ミュートToolStripMenuItem.Name = "ミュートToolStripMenuItem";
			this.ミュートToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.ミュートToolStripMenuItem.Text = "ミュート";
			this.ミュートToolStripMenuItem.Click += new System.EventHandler(this.ミュートToolStripMenuItem_Click);
			// 
			// バランスToolStripMenuItem
			// 
			this.バランスToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.中央ToolStripMenuItem,
            this.左ToolStripMenuItem,
            this.右ToolStripMenuItem});
			this.バランスToolStripMenuItem.Name = "バランスToolStripMenuItem";
			this.バランスToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.バランスToolStripMenuItem.Text = "バランス";
			this.バランスToolStripMenuItem.DropDownOpened += new System.EventHandler(this.バランスToolStripMenuItem_DropDownOpened);
			// 
			// 中央ToolStripMenuItem
			// 
			this.中央ToolStripMenuItem.Name = "中央ToolStripMenuItem";
			this.中央ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.中央ToolStripMenuItem.Text = "中央";
			this.中央ToolStripMenuItem.Click += new System.EventHandler(this.中央ToolStripMenuItem_Click);
			// 
			// 左ToolStripMenuItem
			// 
			this.左ToolStripMenuItem.Name = "左ToolStripMenuItem";
			this.左ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.左ToolStripMenuItem.Text = "左";
			this.左ToolStripMenuItem.Click += new System.EventHandler(this.左ToolStripMenuItem_Click);
			// 
			// 右ToolStripMenuItem
			// 
			this.右ToolStripMenuItem.Name = "右ToolStripMenuItem";
			this.右ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.右ToolStripMenuItem.Text = "右";
			this.右ToolStripMenuItem.Click += new System.EventHandler(this.右ToolStripMenuItem_Click);
			// 
			// 設定ToolStripMenuItem
			// 
			this.設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.デフォルトToolStripMenuItem,
            this.状態保存ToolStripMenuItem,
            this.終了時ToolStripMenuItem,
            this.フォントToolStripMenuItem,
            this.レスボックスの操作ToolStripMenuItem,
            this.toolStripSeparator1,
            this.アスペクト比を維持ToolStripMenuItem,
            this.書き込み時にレスボックスを非表示ToolStripMenuItem,
            this.レスボックスを自動的に隠すToolStripMenuItem,
            this.マウスジェスチャーToolStripMenuItem,
            this.スクリーン吸着ToolStripMenuItem,
            this.スクリーン吸着範囲ToolStripMenuItem,
            this.マウスジェスチャー感度ToolStripMenuItem,
            this.bSでレスボックスを閉じるToolStripMenuItem});
			this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
			this.設定ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.設定ToolStripMenuItem.Text = "設定";
			this.設定ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.設定ToolStripMenuItem_DropDownOpened);
			// 
			// デフォルトToolStripMenuItem
			// 
			this.デフォルトToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xToolStripMenuItem,
            this.yToolStripMenuItem,
            this.幅ToolStripMenuItem,
            this.高さToolStripMenuItem,
            this.音量ToolStripMenuItem1,
            this.拡大率ToolStripMenuItem,
            this.動画サイズに合わせるToolStripMenuItem1,
            this.toolStripSeparator3,
            this.最前列表示ToolStripMenuItem2,
            this.タイトルバーToolStripMenuItem2,
            this.レスボックスToolStripMenuItem2,
            this.ステータスバーToolStripMenuItem2,
            this.フレームToolStripMenuItem2});
			this.デフォルトToolStripMenuItem.Name = "デフォルトToolStripMenuItem";
			this.デフォルトToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.デフォルトToolStripMenuItem.Text = "デフォルト";
			this.デフォルトToolStripMenuItem.DropDownOpened += new System.EventHandler(this.デフォルトToolStripMenuItem_DropDownOpened);
			// 
			// xToolStripMenuItem
			// 
			this.xToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxX});
			this.xToolStripMenuItem.Name = "xToolStripMenuItem";
			this.xToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.xToolStripMenuItem.Text = "X";
			this.xToolStripMenuItem.DropDownOpened += new System.EventHandler(this.xToolStripMenuItem_DropDownOpened);
			// 
			// toolStripTextBoxX
			// 
			this.toolStripTextBoxX.Name = "toolStripTextBoxX";
			this.toolStripTextBoxX.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBoxX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxX_KeyDown);
			// 
			// yToolStripMenuItem
			// 
			this.yToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxY});
			this.yToolStripMenuItem.Name = "yToolStripMenuItem";
			this.yToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.yToolStripMenuItem.Text = "Y";
			this.yToolStripMenuItem.DropDownOpened += new System.EventHandler(this.yToolStripMenuItem_DropDownOpened);
			// 
			// toolStripTextBoxY
			// 
			this.toolStripTextBoxY.Name = "toolStripTextBoxY";
			this.toolStripTextBoxY.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBoxY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxY_KeyDown);
			// 
			// 幅ToolStripMenuItem
			// 
			this.幅ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox幅});
			this.幅ToolStripMenuItem.Name = "幅ToolStripMenuItem";
			this.幅ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.幅ToolStripMenuItem.Text = "幅";
			this.幅ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.幅ToolStripMenuItem_DropDownOpened);
			// 
			// toolStripTextBox幅
			// 
			this.toolStripTextBox幅.Name = "toolStripTextBox幅";
			this.toolStripTextBox幅.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBox幅.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox幅_KeyDown);
			// 
			// 高さToolStripMenuItem
			// 
			this.高さToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox高さ});
			this.高さToolStripMenuItem.Name = "高さToolStripMenuItem";
			this.高さToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.高さToolStripMenuItem.Text = "高さ";
			this.高さToolStripMenuItem.DropDownOpened += new System.EventHandler(this.高さToolStripMenuItem_DropDownOpened);
			// 
			// toolStripTextBox高さ
			// 
			this.toolStripTextBox高さ.Name = "toolStripTextBox高さ";
			this.toolStripTextBox高さ.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBox高さ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox高さ_KeyDown);
			// 
			// 音量ToolStripMenuItem1
			// 
			this.音量ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox音量});
			this.音量ToolStripMenuItem1.Name = "音量ToolStripMenuItem1";
			this.音量ToolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
			this.音量ToolStripMenuItem1.Text = "音量";
			this.音量ToolStripMenuItem1.DropDownOpened += new System.EventHandler(this.音量ToolStripMenuItem1_DropDownOpened);
			// 
			// toolStripTextBox音量
			// 
			this.toolStripTextBox音量.Name = "toolStripTextBox音量";
			this.toolStripTextBox音量.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBox音量.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox音量_KeyDown);
			// 
			// 拡大率ToolStripMenuItem
			// 
			this.拡大率ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxScale});
			this.拡大率ToolStripMenuItem.Name = "拡大率ToolStripMenuItem";
			this.拡大率ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.拡大率ToolStripMenuItem.Text = "拡大率";
			this.拡大率ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.拡大率ToolStripMenuItem_DropDownOpened);
			// 
			// toolStripTextBoxScale
			// 
			this.toolStripTextBoxScale.Name = "toolStripTextBoxScale";
			this.toolStripTextBoxScale.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBoxScale.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxScale_KeyDown);
			// 
			// 動画サイズに合わせるToolStripMenuItem1
			// 
			this.動画サイズに合わせるToolStripMenuItem1.Name = "動画サイズに合わせるToolStripMenuItem1";
			this.動画サイズに合わせるToolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
			this.動画サイズに合わせるToolStripMenuItem1.Text = "動画サイズに合わせる";
			this.動画サイズに合わせるToolStripMenuItem1.Click += new System.EventHandler(this.動画サイズに合わせるToolStripMenuItem1_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(193, 6);
			// 
			// 最前列表示ToolStripMenuItem2
			// 
			this.最前列表示ToolStripMenuItem2.Name = "最前列表示ToolStripMenuItem2";
			this.最前列表示ToolStripMenuItem2.Size = new System.Drawing.Size(196, 22);
			this.最前列表示ToolStripMenuItem2.Text = "最前列表示";
			this.最前列表示ToolStripMenuItem2.Click += new System.EventHandler(this.最前列表示ToolStripMenuItem2_Click);
			// 
			// タイトルバーToolStripMenuItem2
			// 
			this.タイトルバーToolStripMenuItem2.Name = "タイトルバーToolStripMenuItem2";
			this.タイトルバーToolStripMenuItem2.Size = new System.Drawing.Size(196, 22);
			this.タイトルバーToolStripMenuItem2.Text = "タイトルバー";
			this.タイトルバーToolStripMenuItem2.Click += new System.EventHandler(this.タイトルバーToolStripMenuItem2_Click);
			// 
			// レスボックスToolStripMenuItem2
			// 
			this.レスボックスToolStripMenuItem2.Name = "レスボックスToolStripMenuItem2";
			this.レスボックスToolStripMenuItem2.Size = new System.Drawing.Size(196, 22);
			this.レスボックスToolStripMenuItem2.Text = "レスボックス";
			this.レスボックスToolStripMenuItem2.Click += new System.EventHandler(this.レスボックスToolStripMenuItem2_Click);
			// 
			// ステータスバーToolStripMenuItem2
			// 
			this.ステータスバーToolStripMenuItem2.Name = "ステータスバーToolStripMenuItem2";
			this.ステータスバーToolStripMenuItem2.Size = new System.Drawing.Size(196, 22);
			this.ステータスバーToolStripMenuItem2.Text = "ステータスバー";
			this.ステータスバーToolStripMenuItem2.Click += new System.EventHandler(this.ステータスバーToolStripMenuItem2_Click);
			// 
			// フレームToolStripMenuItem2
			// 
			this.フレームToolStripMenuItem2.Name = "フレームToolStripMenuItem2";
			this.フレームToolStripMenuItem2.Size = new System.Drawing.Size(196, 22);
			this.フレームToolStripMenuItem2.Text = "フレーム";
			this.フレームToolStripMenuItem2.Click += new System.EventHandler(this.フレームToolStripMenuItem2_Click);
			// 
			// 状態保存ToolStripMenuItem
			// 
			this.状態保存ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.現在の状態を保存ToolStripMenuItem1,
            this.位置を保存ToolStripMenuItem1,
            this.サイズを保存ToolStripMenuItem1,
            this.音量を保存ToolStripMenuItem1});
			this.状態保存ToolStripMenuItem.Name = "状態保存ToolStripMenuItem";
			this.状態保存ToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.状態保存ToolStripMenuItem.Text = "状態保存";
			// 
			// 現在の状態を保存ToolStripMenuItem1
			// 
			this.現在の状態を保存ToolStripMenuItem1.Name = "現在の状態を保存ToolStripMenuItem1";
			this.現在の状態を保存ToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.現在の状態を保存ToolStripMenuItem1.Text = "現在の状態を保存";
			this.現在の状態を保存ToolStripMenuItem1.Click += new System.EventHandler(this.現在の状態を保存ToolStripMenuItem_Click);
			// 
			// 位置を保存ToolStripMenuItem1
			// 
			this.位置を保存ToolStripMenuItem1.Name = "位置を保存ToolStripMenuItem1";
			this.位置を保存ToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.位置を保存ToolStripMenuItem1.Text = "位置を保存";
			this.位置を保存ToolStripMenuItem1.Click += new System.EventHandler(this.位置を保存ToolStripMenuItem_Click);
			// 
			// サイズを保存ToolStripMenuItem1
			// 
			this.サイズを保存ToolStripMenuItem1.Name = "サイズを保存ToolStripMenuItem1";
			this.サイズを保存ToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.サイズを保存ToolStripMenuItem1.Text = "サイズを保存";
			this.サイズを保存ToolStripMenuItem1.Click += new System.EventHandler(this.サイズを保存ToolStripMenuItem_Click);
			// 
			// 音量を保存ToolStripMenuItem1
			// 
			this.音量を保存ToolStripMenuItem1.Name = "音量を保存ToolStripMenuItem1";
			this.音量を保存ToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.音量を保存ToolStripMenuItem1.Text = "音量を保存";
			this.音量を保存ToolStripMenuItem1.Click += new System.EventHandler(this.音量を保存するToolStripMenuItem_Click);
			// 
			// 終了時ToolStripMenuItem
			// 
			this.終了時ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.位置を保存するToolStripMenuItem,
            this.サイズを保存するToolStripMenuItem,
            this.音量を保存するToolStripMenuItem,
            this.リレーを切断するToolStripMenuItem,
            this.スレッドビューワを終了するToolStripMenuItem});
			this.終了時ToolStripMenuItem.Name = "終了時ToolStripMenuItem";
			this.終了時ToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.終了時ToolStripMenuItem.Text = "終了時";
			this.終了時ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.終了時ToolStripMenuItem_DropDownOpened);
			// 
			// 位置を保存するToolStripMenuItem
			// 
			this.位置を保存するToolStripMenuItem.Name = "位置を保存するToolStripMenuItem";
			this.位置を保存するToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.位置を保存するToolStripMenuItem.Text = "位置を保存する";
			this.位置を保存するToolStripMenuItem.Click += new System.EventHandler(this.位置を保存するToolStripMenuItem_Click);
			// 
			// サイズを保存するToolStripMenuItem
			// 
			this.サイズを保存するToolStripMenuItem.Name = "サイズを保存するToolStripMenuItem";
			this.サイズを保存するToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.サイズを保存するToolStripMenuItem.Text = "サイズを保存する";
			this.サイズを保存するToolStripMenuItem.Click += new System.EventHandler(this.サイズを保存するToolStripMenuItem_Click);
			// 
			// 音量を保存するToolStripMenuItem
			// 
			this.音量を保存するToolStripMenuItem.Name = "音量を保存するToolStripMenuItem";
			this.音量を保存するToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.音量を保存するToolStripMenuItem.Text = "音量を保存する";
			this.音量を保存するToolStripMenuItem.Click += new System.EventHandler(this.音量を保存するToolStripMenuItem_Click);
			// 
			// リレーを切断するToolStripMenuItem
			// 
			this.リレーを切断するToolStripMenuItem.Name = "リレーを切断するToolStripMenuItem";
			this.リレーを切断するToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.リレーを切断するToolStripMenuItem.Text = "リレーを切断する";
			this.リレーを切断するToolStripMenuItem.Click += new System.EventHandler(this.リレーを切断するToolStripMenuItem_Click);
			// 
			// スレッドビューワを終了するToolStripMenuItem
			// 
			this.スレッドビューワを終了するToolStripMenuItem.Name = "スレッドビューワを終了するToolStripMenuItem";
			this.スレッドビューワを終了するToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.スレッドビューワを終了するToolStripMenuItem.Text = "スレッドビューワを終了する";
			this.スレッドビューワを終了するToolStripMenuItem.Click += new System.EventHandler(this.スレッドビューワを終了するToolStripMenuItem_Click);
			// 
			// フォントToolStripMenuItem
			// 
			this.フォントToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.色ToolStripMenuItem,
            this.フォントToolStripMenuItem1});
			this.フォントToolStripMenuItem.Name = "フォントToolStripMenuItem";
			this.フォントToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.フォントToolStripMenuItem.Text = "フォント";
			// 
			// 色ToolStripMenuItem
			// 
			this.色ToolStripMenuItem.Name = "色ToolStripMenuItem";
			this.色ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.色ToolStripMenuItem.Text = "色";
			this.色ToolStripMenuItem.Click += new System.EventHandler(this.色ToolStripMenuItem_Click);
			// 
			// フォントToolStripMenuItem1
			// 
			this.フォントToolStripMenuItem1.Name = "フォントToolStripMenuItem1";
			this.フォントToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
			this.フォントToolStripMenuItem1.Text = "フォント";
			this.フォントToolStripMenuItem1.Click += new System.EventHandler(this.フォントToolStripMenuItem1_Click);
			// 
			// レスボックスの操作ToolStripMenuItem
			// 
			this.レスボックスの操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enter改行ShiftEnter書き込みToolStripMenuItem,
            this.enter書き込みShiftEnter改行ToolStripMenuItem});
			this.レスボックスの操作ToolStripMenuItem.Name = "レスボックスの操作ToolStripMenuItem";
			this.レスボックスの操作ToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.レスボックスの操作ToolStripMenuItem.Text = "レスボックスの操作";
			this.レスボックスの操作ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.レスボックスの操作ToolStripMenuItem_DropDownOpened);
			// 
			// enter改行ShiftEnter書き込みToolStripMenuItem
			// 
			this.enter改行ShiftEnter書き込みToolStripMenuItem.Name = "enter改行ShiftEnter書き込みToolStripMenuItem";
			this.enter改行ShiftEnter書き込みToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
			this.enter改行ShiftEnter書き込みToolStripMenuItem.Text = "Enter:改行 / Shift+Enter:書き込み";
			this.enter改行ShiftEnter書き込みToolStripMenuItem.Click += new System.EventHandler(this.enter改行ShiftEnter書き込みToolStripMenuItem_Click);
			// 
			// enter書き込みShiftEnter改行ToolStripMenuItem
			// 
			this.enter書き込みShiftEnter改行ToolStripMenuItem.Name = "enter書き込みShiftEnter改行ToolStripMenuItem";
			this.enter書き込みShiftEnter改行ToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
			this.enter書き込みShiftEnter改行ToolStripMenuItem.Text = "Enter:書き込み / Shift+Enter:改行";
			this.enter書き込みShiftEnter改行ToolStripMenuItem.Click += new System.EventHandler(this.enter書き込みShiftEnter改行ToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(265, 6);
			// 
			// アスペクト比を維持ToolStripMenuItem
			// 
			this.アスペクト比を維持ToolStripMenuItem.Name = "アスペクト比を維持ToolStripMenuItem";
			this.アスペクト比を維持ToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.アスペクト比を維持ToolStripMenuItem.Text = "アスペクト比を維持";
			this.アスペクト比を維持ToolStripMenuItem.Click += new System.EventHandler(this.アスペクト比を維持ToolStripMenuItem_Click);
			// 
			// 書き込み時にレスボックスを非表示ToolStripMenuItem
			// 
			this.書き込み時にレスボックスを非表示ToolStripMenuItem.Name = "書き込み時にレスボックスを非表示ToolStripMenuItem";
			this.書き込み時にレスボックスを非表示ToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.書き込み時にレスボックスを非表示ToolStripMenuItem.Text = "書き込み時にレスボックスを非表示";
			this.書き込み時にレスボックスを非表示ToolStripMenuItem.Click += new System.EventHandler(this.書き込み時にレスボックスを非表示ToolStripMenuItem_Click);
			// 
			// レスボックスを自動的に隠すToolStripMenuItem
			// 
			this.レスボックスを自動的に隠すToolStripMenuItem.Name = "レスボックスを自動的に隠すToolStripMenuItem";
			this.レスボックスを自動的に隠すToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.レスボックスを自動的に隠すToolStripMenuItem.Text = "レスボックスを自動的に隠す";
			this.レスボックスを自動的に隠すToolStripMenuItem.Click += new System.EventHandler(this.レスボックスを自動的に隠すToolStripMenuItem_Click);
			// 
			// マウスジェスチャーToolStripMenuItem
			// 
			this.マウスジェスチャーToolStripMenuItem.Name = "マウスジェスチャーToolStripMenuItem";
			this.マウスジェスチャーToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.マウスジェスチャーToolStripMenuItem.Text = "マウスジェスチャー";
			this.マウスジェスチャーToolStripMenuItem.Click += new System.EventHandler(this.マウスジェスチャーToolStripMenuItem_Click);
			// 
			// スクリーン吸着ToolStripMenuItem
			// 
			this.スクリーン吸着ToolStripMenuItem.Name = "スクリーン吸着ToolStripMenuItem";
			this.スクリーン吸着ToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.スクリーン吸着ToolStripMenuItem.Text = "スクリーン吸着";
			this.スクリーン吸着ToolStripMenuItem.Click += new System.EventHandler(this.スクリーン吸着ToolStripMenuItem_Click);
			// 
			// スクリーン吸着範囲ToolStripMenuItem
			// 
			this.スクリーン吸着範囲ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxScreenMagnetDockDist});
			this.スクリーン吸着範囲ToolStripMenuItem.Name = "スクリーン吸着範囲ToolStripMenuItem";
			this.スクリーン吸着範囲ToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.スクリーン吸着範囲ToolStripMenuItem.Text = "スクリーン吸着範囲";
			this.スクリーン吸着範囲ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.スクリーン吸着範囲ToolStripMenuItem_DropDownOpened);
			// 
			// toolStripTextBoxScreenMagnetDockDist
			// 
			this.toolStripTextBoxScreenMagnetDockDist.Name = "toolStripTextBoxScreenMagnetDockDist";
			this.toolStripTextBoxScreenMagnetDockDist.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBoxScreenMagnetDockDist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxScreenMagnetDockDist_KeyDown);
			// 
			// マウスジェスチャー感度ToolStripMenuItem
			// 
			this.マウスジェスチャー感度ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxマウスジェスチャー感度});
			this.マウスジェスチャー感度ToolStripMenuItem.Name = "マウスジェスチャー感度ToolStripMenuItem";
			this.マウスジェスチャー感度ToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.マウスジェスチャー感度ToolStripMenuItem.Text = "マウスジェスチャー感度";
			this.マウスジェスチャー感度ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.マウスジェスチャー感度ToolStripMenuItem_DropDownOpened);
			// 
			// toolStripTextBoxマウスジェスチャー感度
			// 
			this.toolStripTextBoxマウスジェスチャー感度.Name = "toolStripTextBoxマウスジェスチャー感度";
			this.toolStripTextBoxマウスジェスチャー感度.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBoxマウスジェスチャー感度.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxマウスジェスチャー感度_KeyDown);
			// 
			// bSでレスボックスを閉じるToolStripMenuItem
			// 
			this.bSでレスボックスを閉じるToolStripMenuItem.Name = "bSでレスボックスを閉じるToolStripMenuItem";
			this.bSでレスボックスを閉じるToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
			this.bSでレスボックスを閉じるToolStripMenuItem.Text = "BSでレスボックスを閉じる";
			this.bSでレスボックスを閉じるToolStripMenuItem.Click += new System.EventHandler(this.bSでレスボックスを閉じるToolStripMenuItem_Click);
			// 
			// ファイルToolStripMenuItem
			// 
			this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uRLを開くToolStripMenuItem,
            this.開くToolStripMenuItem,
            this.クリップボードから開くToolStripMenuItem,
            this.toolStripSeparator2,
            this.終了するToolStripMenuItem,
            this.リレーを切断して終了するToolStripMenuItem1});
			this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
			this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.ファイルToolStripMenuItem.Text = "ファイル";
			// 
			// uRLを開くToolStripMenuItem
			// 
			this.uRLを開くToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxURL});
			this.uRLを開くToolStripMenuItem.Name = "uRLを開くToolStripMenuItem";
			this.uRLを開くToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.uRLを開くToolStripMenuItem.Text = "URLを開く";
			// 
			// toolStripTextBoxURL
			// 
			this.toolStripTextBoxURL.Name = "toolStripTextBoxURL";
			this.toolStripTextBoxURL.Size = new System.Drawing.Size(200, 25);
			this.toolStripTextBoxURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxURL_KeyDown);
			// 
			// 開くToolStripMenuItem
			// 
			this.開くToolStripMenuItem.Name = "開くToolStripMenuItem";
			this.開くToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.開くToolStripMenuItem.Text = "ファイルを開く";
			this.開くToolStripMenuItem.Click += new System.EventHandler(this.開くToolStripMenuItem_Click);
			// 
			// クリップボードから開くToolStripMenuItem
			// 
			this.クリップボードから開くToolStripMenuItem.Name = "クリップボードから開くToolStripMenuItem";
			this.クリップボードから開くToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.クリップボードから開くToolStripMenuItem.Text = "クリップボードから開く";
			this.クリップボードから開くToolStripMenuItem.Click += new System.EventHandler(this.クリップボードから開くToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
			// 
			// 終了するToolStripMenuItem
			// 
			this.終了するToolStripMenuItem.Name = "終了するToolStripMenuItem";
			this.終了するToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.終了するToolStripMenuItem.Text = "終了する";
			this.終了するToolStripMenuItem.Click += new System.EventHandler(this.終了するToolStripMenuItem_Click);
			// 
			// リレーを切断して終了するToolStripMenuItem1
			// 
			this.リレーを切断して終了するToolStripMenuItem1.Name = "リレーを切断して終了するToolStripMenuItem1";
			this.リレーを切断して終了するToolStripMenuItem1.Size = new System.Drawing.Size(220, 22);
			this.リレーを切断して終了するToolStripMenuItem1.Text = "リレーを切断して終了する";
			this.リレーを切断して終了するToolStripMenuItem1.Click += new System.EventHandler(this.リレーを切断して終了するToolStripMenuItem_Click);
			// 
			// wMPメニューToolStripMenuItem
			// 
			this.wMPメニューToolStripMenuItem.Name = "wMPメニューToolStripMenuItem";
			this.wMPメニューToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.wMPメニューToolStripMenuItem.Text = "WMPメニュー";
			this.wMPメニューToolStripMenuItem.Click += new System.EventHandler(this.wMPメニューToolStripMenuItem_Click);
			// 
			// timerUpdateChannelInfo
			// 
			this.timerUpdateChannelInfo.Enabled = true;
			this.timerUpdateChannelInfo.Interval = 60000;
			this.timerUpdateChannelInfo.Tick += new System.EventHandler(this.timerUpdateChannelInfo_Tick);
			// 
			// toolStrip
			// 
			this.toolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.EnableClickThrough = true;
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonDivide,
            this.toolStripDropDownButtonSize,
            this.toolStripButtonOpenViewer,
            this.toolStripButtonMin,
            this.toolStripButtonMax,
            this.toolStripButtonClose});
			this.toolStrip.Location = new System.Drawing.Point(327, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(153, 25);
			this.toolStrip.TabIndex = 5;
			this.toolStrip.Visible = false;
			// 
			// toolStripDropDownButtonDivide
			// 
			this.toolStripDropDownButtonDivide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButtonDivide.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.幅5分の1ToolStripMenuItem,
            this.幅4分の1ToolStripMenuItem,
            this.幅3分の1ToolStripMenuItem,
            this.幅2分の1ToolStripMenuItem,
            this.幅1分の1ToolStripMenuItem,
            this.toolStripSeparatorDivid1,
            this.高さ5分の1ToolStripMenuItem,
            this.高さ4分の1ToolStripMenuItem,
            this.高さ3分の1ToolStripMenuItem,
            this.高さ2分の1ToolStripMenuItem,
            this.高さ1分の1ToolStripMenuItem,
            this.toolStripSeparatorDivid2,
            this.分割5X5ToolStripMenuItem,
            this.分割4X4ToolStripMenuItem,
            this.分割3X3ToolStripMenuItem,
            this.分割2X2ToolStripMenuItem,
            this.分割1X1ToolStripMenuItem});
			this.toolStripDropDownButtonDivide.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonDivide.Image")));
			this.toolStripDropDownButtonDivide.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonDivide.Name = "toolStripDropDownButtonDivide";
			this.toolStripDropDownButtonDivide.Size = new System.Drawing.Size(29, 22);
			this.toolStripDropDownButtonDivide.Text = "画面分割";
			// 
			// 幅5分の1ToolStripMenuItem
			// 
			this.幅5分の1ToolStripMenuItem.Name = "幅5分の1ToolStripMenuItem";
			this.幅5分の1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.幅5分の1ToolStripMenuItem.Text = "幅：5分の1";
			// 
			// 幅4分の1ToolStripMenuItem
			// 
			this.幅4分の1ToolStripMenuItem.Name = "幅4分の1ToolStripMenuItem";
			this.幅4分の1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.幅4分の1ToolStripMenuItem.Text = "幅：4分の1";
			// 
			// 幅3分の1ToolStripMenuItem
			// 
			this.幅3分の1ToolStripMenuItem.Name = "幅3分の1ToolStripMenuItem";
			this.幅3分の1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.幅3分の1ToolStripMenuItem.Text = "幅：3分の1";
			// 
			// 幅2分の1ToolStripMenuItem
			// 
			this.幅2分の1ToolStripMenuItem.Name = "幅2分の1ToolStripMenuItem";
			this.幅2分の1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.幅2分の1ToolStripMenuItem.Text = "幅：2分の1";
			// 
			// 幅1分の1ToolStripMenuItem
			// 
			this.幅1分の1ToolStripMenuItem.Name = "幅1分の1ToolStripMenuItem";
			this.幅1分の1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.幅1分の1ToolStripMenuItem.Text = "幅：1分の1";
			// 
			// toolStripSeparatorDivid1
			// 
			this.toolStripSeparatorDivid1.Name = "toolStripSeparatorDivid1";
			this.toolStripSeparatorDivid1.Size = new System.Drawing.Size(147, 6);
			// 
			// 高さ5分の1ToolStripMenuItem
			// 
			this.高さ5分の1ToolStripMenuItem.Name = "高さ5分の1ToolStripMenuItem";
			this.高さ5分の1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.高さ5分の1ToolStripMenuItem.Text = "高さ：5分の1";
			// 
			// 高さ4分の1ToolStripMenuItem
			// 
			this.高さ4分の1ToolStripMenuItem.Name = "高さ4分の1ToolStripMenuItem";
			this.高さ4分の1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.高さ4分の1ToolStripMenuItem.Text = "高さ：4分の1";
			// 
			// 高さ3分の1ToolStripMenuItem
			// 
			this.高さ3分の1ToolStripMenuItem.Name = "高さ3分の1ToolStripMenuItem";
			this.高さ3分の1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.高さ3分の1ToolStripMenuItem.Text = "高さ：3分の1";
			// 
			// 高さ2分の1ToolStripMenuItem
			// 
			this.高さ2分の1ToolStripMenuItem.Name = "高さ2分の1ToolStripMenuItem";
			this.高さ2分の1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.高さ2分の1ToolStripMenuItem.Text = "高さ：2分の1";
			// 
			// 高さ1分の1ToolStripMenuItem
			// 
			this.高さ1分の1ToolStripMenuItem.Name = "高さ1分の1ToolStripMenuItem";
			this.高さ1分の1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.高さ1分の1ToolStripMenuItem.Text = "高さ：1分の1";
			// 
			// toolStripSeparatorDivid2
			// 
			this.toolStripSeparatorDivid2.Name = "toolStripSeparatorDivid2";
			this.toolStripSeparatorDivid2.Size = new System.Drawing.Size(147, 6);
			// 
			// 分割5X5ToolStripMenuItem
			// 
			this.分割5X5ToolStripMenuItem.Name = "分割5X5ToolStripMenuItem";
			this.分割5X5ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.分割5X5ToolStripMenuItem.Text = "分割：5 x 5";
			// 
			// 分割4X4ToolStripMenuItem
			// 
			this.分割4X4ToolStripMenuItem.Name = "分割4X4ToolStripMenuItem";
			this.分割4X4ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.分割4X4ToolStripMenuItem.Text = "分割：4 x 4";
			// 
			// 分割3X3ToolStripMenuItem
			// 
			this.分割3X3ToolStripMenuItem.Name = "分割3X3ToolStripMenuItem";
			this.分割3X3ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.分割3X3ToolStripMenuItem.Text = "分割：3 x 3";
			// 
			// 分割2X2ToolStripMenuItem
			// 
			this.分割2X2ToolStripMenuItem.Name = "分割2X2ToolStripMenuItem";
			this.分割2X2ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.分割2X2ToolStripMenuItem.Text = "分割：2 x 2";
			// 
			// 分割1X1ToolStripMenuItem
			// 
			this.分割1X1ToolStripMenuItem.Name = "分割1X1ToolStripMenuItem";
			this.分割1X1ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.分割1X1ToolStripMenuItem.Text = "分割：1 x 1";
			// 
			// toolStripDropDownButtonSize
			// 
			this.toolStripDropDownButtonSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButtonSize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripSeparator6,
            this.x120ToolStripMenuItem,
            this.x240ToolStripMenuItem,
            this.x360ToolStripMenuItem,
            this.x480ToolStripMenuItem,
            this.x600ToolStripMenuItem,
            this.toolStripSeparatorSize1,
            this.幅160ToolStripMenuItem,
            this.幅320ToolStripMenuItem,
            this.幅480ToolStripMenuItem,
            this.幅640ToolStripMenuItem,
            this.幅800ToolStripMenuItem,
            this.toolStripSeparatorSize2,
            this.高さ120ToolStripMenuItem,
            this.高さ240ToolStripMenuItem,
            this.高さ360ToolStripMenuItem,
            this.高さ480ToolStripMenuItem,
            this.高さ600ToolStripMenuItem});
			this.toolStripDropDownButtonSize.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonSize.Image")));
			this.toolStripDropDownButtonSize.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonSize.Name = "toolStripDropDownButtonSize";
			this.toolStripDropDownButtonSize.Size = new System.Drawing.Size(29, 22);
			this.toolStripDropDownButtonSize.Text = "サイズ";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 22);
			this.toolStripMenuItem2.Text = "50%";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(133, 22);
			this.toolStripMenuItem3.Text = "75%";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(133, 22);
			this.toolStripMenuItem4.Text = "100%";
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(133, 22);
			this.toolStripMenuItem5.Text = "150%";
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(133, 22);
			this.toolStripMenuItem6.Text = "200%";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(130, 6);
			// 
			// x120ToolStripMenuItem
			// 
			this.x120ToolStripMenuItem.Name = "x120ToolStripMenuItem";
			this.x120ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.x120ToolStripMenuItem.Text = "160 x 120";
			// 
			// x240ToolStripMenuItem
			// 
			this.x240ToolStripMenuItem.Name = "x240ToolStripMenuItem";
			this.x240ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.x240ToolStripMenuItem.Text = "320 x 240";
			// 
			// x360ToolStripMenuItem
			// 
			this.x360ToolStripMenuItem.Name = "x360ToolStripMenuItem";
			this.x360ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.x360ToolStripMenuItem.Text = "480 x 360";
			// 
			// x480ToolStripMenuItem
			// 
			this.x480ToolStripMenuItem.Name = "x480ToolStripMenuItem";
			this.x480ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.x480ToolStripMenuItem.Text = "640 x 480";
			// 
			// x600ToolStripMenuItem
			// 
			this.x600ToolStripMenuItem.Name = "x600ToolStripMenuItem";
			this.x600ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.x600ToolStripMenuItem.Text = "800 x 600";
			// 
			// toolStripSeparatorSize1
			// 
			this.toolStripSeparatorSize1.Name = "toolStripSeparatorSize1";
			this.toolStripSeparatorSize1.Size = new System.Drawing.Size(130, 6);
			// 
			// 幅160ToolStripMenuItem
			// 
			this.幅160ToolStripMenuItem.Name = "幅160ToolStripMenuItem";
			this.幅160ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.幅160ToolStripMenuItem.Text = "幅：160";
			// 
			// 幅320ToolStripMenuItem
			// 
			this.幅320ToolStripMenuItem.Name = "幅320ToolStripMenuItem";
			this.幅320ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.幅320ToolStripMenuItem.Text = "幅：320";
			// 
			// 幅480ToolStripMenuItem
			// 
			this.幅480ToolStripMenuItem.Name = "幅480ToolStripMenuItem";
			this.幅480ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.幅480ToolStripMenuItem.Text = "幅：480";
			// 
			// 幅640ToolStripMenuItem
			// 
			this.幅640ToolStripMenuItem.Name = "幅640ToolStripMenuItem";
			this.幅640ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.幅640ToolStripMenuItem.Text = "幅：640";
			// 
			// 幅800ToolStripMenuItem
			// 
			this.幅800ToolStripMenuItem.Name = "幅800ToolStripMenuItem";
			this.幅800ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.幅800ToolStripMenuItem.Text = "幅：800";
			// 
			// toolStripSeparatorSize2
			// 
			this.toolStripSeparatorSize2.Name = "toolStripSeparatorSize2";
			this.toolStripSeparatorSize2.Size = new System.Drawing.Size(130, 6);
			// 
			// 高さ120ToolStripMenuItem
			// 
			this.高さ120ToolStripMenuItem.Name = "高さ120ToolStripMenuItem";
			this.高さ120ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.高さ120ToolStripMenuItem.Text = "高さ：120";
			// 
			// 高さ240ToolStripMenuItem
			// 
			this.高さ240ToolStripMenuItem.Name = "高さ240ToolStripMenuItem";
			this.高さ240ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.高さ240ToolStripMenuItem.Text = "高さ：240";
			// 
			// 高さ360ToolStripMenuItem
			// 
			this.高さ360ToolStripMenuItem.Name = "高さ360ToolStripMenuItem";
			this.高さ360ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.高さ360ToolStripMenuItem.Text = "高さ：360";
			// 
			// 高さ480ToolStripMenuItem
			// 
			this.高さ480ToolStripMenuItem.Name = "高さ480ToolStripMenuItem";
			this.高さ480ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.高さ480ToolStripMenuItem.Text = "高さ：480";
			// 
			// 高さ600ToolStripMenuItem
			// 
			this.高さ600ToolStripMenuItem.Name = "高さ600ToolStripMenuItem";
			this.高さ600ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.高さ600ToolStripMenuItem.Text = "高さ：600";
			// 
			// toolStripButtonOpenViewer
			// 
			this.toolStripButtonOpenViewer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonOpenViewer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenViewer.Image")));
			this.toolStripButtonOpenViewer.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonOpenViewer.Name = "toolStripButtonOpenViewer";
			this.toolStripButtonOpenViewer.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonOpenViewer.Text = "スレッドビューワを表示";
			this.toolStripButtonOpenViewer.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// toolStripButtonMin
			// 
			this.toolStripButtonMin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonMin.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMin.Image")));
			this.toolStripButtonMin.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonMin.Name = "toolStripButtonMin";
			this.toolStripButtonMin.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonMin.Text = "最小化";
			this.toolStripButtonMin.Click += new System.EventHandler(this.toolStripButtonMin_Click);
			// 
			// toolStripButtonMax
			// 
			this.toolStripButtonMax.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonMax.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMax.Image")));
			this.toolStripButtonMax.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonMax.Name = "toolStripButtonMax";
			this.toolStripButtonMax.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonMax.Text = "最大化";
			this.toolStripButtonMax.Click += new System.EventHandler(this.toolStripButtonMax_Click);
			// 
			// toolStripButtonClose
			// 
			this.toolStripButtonClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonClose.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClose.Image")));
			this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonClose.Name = "toolStripButtonClose";
			this.toolStripButtonClose.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonClose.Text = "閉じる";
			this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(480, 502);
			this.Controls.Add(this.panelStatusLabel);
			this.Controls.Add(this.panelResBox);
			this.Controls.Add(this.panelWMP);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "PeerstPlayer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.panelWMP.ResumeLayout(false);
			this.panelWMP.PerformLayout();
			this.panelResBox.ResumeLayout(false);
			this.panelResBox.PerformLayout();
			this.contextMenuStripResBox.ResumeLayout(false);
			this.contextMenuStripResBox.PerformLayout();
			this.panelStatusLabel.ResumeLayout(false);
			this.panelStatusLabel.PerformLayout();
			this.panelDetailRight.ResumeLayout(false);
			this.panelDetailRight.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
			this.contextMenuStripWMP.ResumeLayout(false);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelWMP;
		private System.Windows.Forms.Panel panelResBox;
		private ToolStripEx toolStrip;
		private System.Windows.Forms.ToolStripButton toolStripButtonMin;
		private System.Windows.Forms.ToolStripButton toolStripButtonMax;
		private System.Windows.Forms.ToolStripButton toolStripButtonClose;
		private System.Windows.Forms.Panel panelStatusLabel;
		private System.Windows.Forms.Panel panelDetailRight;
		private System.Windows.Forms.Label labelVolume;
		private System.Windows.Forms.Label labelDuration;
		private System.Windows.Forms.Label labelDetail;
		private System.Windows.Forms.PictureBox pictureBoxIcon;
		private TextBox resBox;
		private System.Windows.Forms.Timer timerUpdateStatubar;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripResBox;
		private System.Windows.Forms.ToolStripMenuItem スレッドビューワを開くToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem スレッドURLをコピーToolStripMenuItem;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonDivide;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSize;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSize1;
		private System.Windows.Forms.Timer timerLoadIni;
		private System.Windows.Forms.ToolStripMenuItem 幅5分の1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 幅4分の1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 幅3分の1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 幅2分の1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDivid1;
		private System.Windows.Forms.ToolStripMenuItem 高さ5分の1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高さ4分の1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高さ3分の1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高さ2分の1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 幅160ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 幅320ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 幅480ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 幅640ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 幅800ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSize2;
		private System.Windows.Forms.ToolStripMenuItem 高さ120ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高さ240ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高さ360ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高さ480ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高さ600ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDivid2;
		private System.Windows.Forms.ToolStripMenuItem 分割5X5ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 分割4X4ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 分割3X3ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 分割2X2ToolStripMenuItem;
		private System.Windows.Forms.ToolTip toolTipDetail;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripWMP;
		private System.Windows.Forms.ToolStripMenuItem 表示ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem タイトルバーToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem レスボックスToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ステータスバーToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem フレームToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 最前列表示ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparatorView;
		private System.Windows.Forms.ToolStripMenuItem 機能ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 音量ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 上げるToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 下げるToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ミュートToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem バランスToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 中央ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 左ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 右ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem スクリーンショットToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem スクリーンショットフォルダを開くToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bump再接続ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem スレッドビューワを開くToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem チャンネル情報を更新するToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了時ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 位置を保存するToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem サイズを保存するToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem リレーを切断するToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem 音量を保存するToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem アスペクト比を維持ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem レスボックスを自動的に隠すToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 書き込み時にレスボックスを非表示ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem マウスジェスチャーToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem スクリーン吸着ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem スレッドビューワを終了するToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem デフォルトToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 最前列表示ToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem タイトルバーToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem レスボックスToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem ステータスバーToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem フレームToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem 状態保存ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 現在の状態を保存ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 位置を保存ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem サイズを保存ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 音量を保存ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem レスボックスの操作ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem enter改行ShiftEnter書き込みToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem enter書き込みShiftEnter改行ToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButtonOpenViewer;
		private System.Windows.Forms.ToolStripMenuItem チャンネル情報を更新ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 開くToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uRLを開くToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了するToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem リレーを切断して終了するToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem 最小化ミュートToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem リトライToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem フルスクリーンToolStripMenuItem;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxURL;
		private System.Windows.Forms.ToolStripMenuItem 動画サイズに合わせるToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem クリップボードから開くToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem yToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 幅ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高さToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 音量ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxX;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxY;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBox幅;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBox高さ;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBox音量;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem フォントToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 色ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem フォントToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem スクリーン吸着範囲ToolStripMenuItem;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxScreenMagnetDockDist;
		private System.Windows.Forms.ToolStripMenuItem マウスジェスチャー感度ToolStripMenuItem;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxマウスジェスチャー感度;
		private System.Windows.Forms.ToolStripMenuItem コンタクトＵＲＬをブラウザで開くToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 拡大率ToolStripMenuItem;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxScale;
		private System.Windows.Forms.ToolStripMenuItem 動画サイズに合わせるToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem wMPメニューToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bSでレスボックスを閉じるToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 分割1X1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 幅1分の1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高さ1分の1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem x120ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x240ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x360ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x480ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x600ToolStripMenuItem;
		private System.Windows.Forms.Timer timerUpdateChannelInfo;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxThreadURL;
		private System.Windows.Forms.Label labelThreadTitle;
	}
}

