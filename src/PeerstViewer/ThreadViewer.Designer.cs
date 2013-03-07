namespace PeerstViewer
{
	partial class ThreadViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThreadViewer));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonReload = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAutoReload = new System.Windows.Forms.ToolStripButton();
			this.toolStripComboBoxReloadTime = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripButtonScroolBottom = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonScrollTop = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonWriteView = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonTopMost = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonOpenClipBoad = new System.Windows.Forms.ToolStripButton();
			this.URLをコピー = new System.Windows.Forms.ToolStripButton();
			this.toolStripDropDownButtonSetting = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripMenuItemデフォルト = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxX = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxY = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripMenuItemWidth = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxWidth = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripMenuItemHeight = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxHeight = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.最前列表示toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.書き込み欄の表示toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.最下位へスクロールtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.自動更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.状態保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.位置を保存するToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.サイズを保存するToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.終了時ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.位置を保存するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.サイズを保存するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.フォントToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.フォントToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.折り返し表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.comboBox = new System.Windows.Forms.ComboBox();
			this.buttonThreadListUpdate = new System.Windows.Forms.Button();
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			this.textBoxMail = new System.Windows.Forms.TextBox();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.labelMail = new System.Windows.Forms.Label();
			this.labelName = new System.Windows.Forms.Label();
			this.textBoxMessage = new System.Windows.Forms.TextBox();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.アドレスをコピーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.リンクを開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundWorkerReload = new System.ComponentModel.BackgroundWorker();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.toolStrip.SuspendLayout();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.panel1.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonReload,
            this.toolStripButtonAutoReload,
            this.toolStripComboBoxReloadTime,
            this.toolStripButtonScroolBottom,
            this.toolStripButtonScrollTop,
            this.toolStripButtonWriteView,
            this.toolStripButtonTopMost,
            this.toolStripButtonOpenClipBoad,
            this.URLをコピー,
            this.toolStripDropDownButtonSetting});
			this.toolStrip.Location = new System.Drawing.Point(3, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(527, 26);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "設定";
			// 
			// toolStripButtonReload
			// 
			this.toolStripButtonReload.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReload.Image")));
			this.toolStripButtonReload.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonReload.Name = "toolStripButtonReload";
			this.toolStripButtonReload.Size = new System.Drawing.Size(52, 23);
			this.toolStripButtonReload.Text = "更新";
			this.toolStripButtonReload.ToolTipText = "更新";
			this.toolStripButtonReload.Click += new System.EventHandler(this.toolStripButtonReload_Click);
			// 
			// toolStripButtonAutoReload
			// 
			this.toolStripButtonAutoReload.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAutoReload.Image")));
			this.toolStripButtonAutoReload.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAutoReload.Name = "toolStripButtonAutoReload";
			this.toolStripButtonAutoReload.Size = new System.Drawing.Size(23, 23);
			this.toolStripButtonAutoReload.ToolTipText = "自動更新";
			this.toolStripButtonAutoReload.Click += new System.EventHandler(this.toolStripButtonAutoReload_Click);
			// 
			// toolStripComboBoxReloadTime
			// 
			this.toolStripComboBoxReloadTime.AutoSize = false;
			this.toolStripComboBoxReloadTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolStripComboBoxReloadTime.DropDownWidth = 30;
			this.toolStripComboBoxReloadTime.Items.AddRange(new object[] {
            "7",
            "10",
            "15",
            "20",
            "25",
            "30"});
			this.toolStripComboBoxReloadTime.Name = "toolStripComboBoxReloadTime";
			this.toolStripComboBoxReloadTime.Size = new System.Drawing.Size(38, 26);
			this.toolStripComboBoxReloadTime.ToolTipText = "自動更新間隔(秒)";
			// 
			// toolStripButtonScroolBottom
			// 
			this.toolStripButtonScroolBottom.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonScroolBottom.Image")));
			this.toolStripButtonScroolBottom.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonScroolBottom.Name = "toolStripButtonScroolBottom";
			this.toolStripButtonScroolBottom.Size = new System.Drawing.Size(40, 23);
			this.toolStripButtonScroolBottom.Text = "↓";
			this.toolStripButtonScroolBottom.ToolTipText = "最下位へスクロール";
			this.toolStripButtonScroolBottom.Click += new System.EventHandler(this.toolStripButtonScroolBottom_Click);
			// 
			// toolStripButtonScrollTop
			// 
			this.toolStripButtonScrollTop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonScrollTop.Image")));
			this.toolStripButtonScrollTop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonScrollTop.Name = "toolStripButtonScrollTop";
			this.toolStripButtonScrollTop.Size = new System.Drawing.Size(40, 23);
			this.toolStripButtonScrollTop.Text = "↑";
			this.toolStripButtonScrollTop.ToolTipText = "最上位へスクロール";
			this.toolStripButtonScrollTop.Click += new System.EventHandler(this.toolStripButtonScrollTop_Click);
			// 
			// toolStripButtonWriteView
			// 
			this.toolStripButtonWriteView.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonWriteView.Image")));
			this.toolStripButtonWriteView.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonWriteView.Name = "toolStripButtonWriteView";
			this.toolStripButtonWriteView.Size = new System.Drawing.Size(88, 23);
			this.toolStripButtonWriteView.Text = "書き込み欄";
			this.toolStripButtonWriteView.Click += new System.EventHandler(this.toolStripButtonWriteView_Click);
			// 
			// toolStripButtonTopMost
			// 
			this.toolStripButtonTopMost.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTopMost.Image")));
			this.toolStripButtonTopMost.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonTopMost.Name = "toolStripButtonTopMost";
			this.toolStripButtonTopMost.Size = new System.Drawing.Size(64, 23);
			this.toolStripButtonTopMost.Text = "最前列";
			this.toolStripButtonTopMost.ToolTipText = "toolStripButtonTopMost";
			this.toolStripButtonTopMost.Click += new System.EventHandler(this.toolStripButtonTopMost_Click);
			// 
			// toolStripButtonOpenClipBoad
			// 
			this.toolStripButtonOpenClipBoad.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenClipBoad.Image")));
			this.toolStripButtonOpenClipBoad.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonOpenClipBoad.Name = "toolStripButtonOpenClipBoad";
			this.toolStripButtonOpenClipBoad.Size = new System.Drawing.Size(52, 23);
			this.toolStripButtonOpenClipBoad.Text = "展開";
			this.toolStripButtonOpenClipBoad.ToolTipText = "クリップボードからURLを開く";
			this.toolStripButtonOpenClipBoad.Click += new System.EventHandler(this.toolStripButtonOpenClipBoad_Click);
			// 
			// URLをコピー
			// 
			this.URLをコピー.Image = ((System.Drawing.Image)(resources.GetObject("URLをコピー.Image")));
			this.URLをコピー.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.URLをコピー.Name = "URLをコピー";
			this.URLをコピー.Size = new System.Drawing.Size(64, 23);
			this.URLをコピー.Text = "コピー";
			this.URLをコピー.ToolTipText = "スレッドURLをコピー";
			this.URLをコピー.Click += new System.EventHandler(this.URLをコピー_Click);
			// 
			// toolStripDropDownButtonSetting
			// 
			this.toolStripDropDownButtonSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemデフォルト,
            this.状態保存ToolStripMenuItem,
            this.終了時ToolStripMenuItem,
            this.フォントToolStripMenuItem,
            this.toolStripSeparator1,
            this.折り返し表示ToolStripMenuItem});
			this.toolStripDropDownButtonSetting.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonSetting.Image")));
			this.toolStripDropDownButtonSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonSetting.Name = "toolStripDropDownButtonSetting";
			this.toolStripDropDownButtonSetting.Size = new System.Drawing.Size(61, 23);
			this.toolStripDropDownButtonSetting.Text = "設定";
			this.toolStripDropDownButtonSetting.DropDownOpened += new System.EventHandler(this.toolStripDropDownButtonSetting_DropDownOpened);
			// 
			// toolStripMenuItemデフォルト
			// 
			this.toolStripMenuItemデフォルト.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemX,
            this.toolStripMenuItemY,
            this.toolStripMenuItemWidth,
            this.toolStripMenuItemHeight,
            this.toolStripSeparator2,
            this.最前列表示toolStripMenuItem,
            this.書き込み欄の表示toolStripMenuItem,
            this.最下位へスクロールtoolStripMenuItem,
            this.自動更新ToolStripMenuItem});
			this.toolStripMenuItemデフォルト.Name = "toolStripMenuItemデフォルト";
			this.toolStripMenuItemデフォルト.Size = new System.Drawing.Size(148, 22);
			this.toolStripMenuItemデフォルト.Text = "デフォルト";
			this.toolStripMenuItemデフォルト.DropDownOpened += new System.EventHandler(this.デフォルトToolStripMenuItem_DropDownOpened);
			// 
			// toolStripMenuItemX
			// 
			this.toolStripMenuItemX.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxX});
			this.toolStripMenuItemX.Name = "toolStripMenuItemX";
			this.toolStripMenuItemX.Size = new System.Drawing.Size(184, 22);
			this.toolStripMenuItemX.Text = "X";
			this.toolStripMenuItemX.DropDownOpened += new System.EventHandler(this.xToolStripMenuItem_DropDownOpened);
			// 
			// toolStripTextBoxX
			// 
			this.toolStripTextBoxX.Name = "toolStripTextBoxX";
			this.toolStripTextBoxX.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBoxX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxX_KeyDown);
			// 
			// toolStripMenuItemY
			// 
			this.toolStripMenuItemY.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxY});
			this.toolStripMenuItemY.Name = "toolStripMenuItemY";
			this.toolStripMenuItemY.Size = new System.Drawing.Size(184, 22);
			this.toolStripMenuItemY.Text = "Y";
			this.toolStripMenuItemY.DropDownOpened += new System.EventHandler(this.yToolStripMenuItem_DropDownOpened);
			// 
			// toolStripTextBoxY
			// 
			this.toolStripTextBoxY.Name = "toolStripTextBoxY";
			this.toolStripTextBoxY.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBoxY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxY_KeyDown);
			// 
			// toolStripMenuItemWidth
			// 
			this.toolStripMenuItemWidth.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxWidth});
			this.toolStripMenuItemWidth.Name = "toolStripMenuItemWidth";
			this.toolStripMenuItemWidth.Size = new System.Drawing.Size(184, 22);
			this.toolStripMenuItemWidth.Text = "幅";
			this.toolStripMenuItemWidth.DropDownOpened += new System.EventHandler(this.幅ToolStripMenuItem_DropDownOpened);
			// 
			// toolStripTextBoxWidth
			// 
			this.toolStripTextBoxWidth.Name = "toolStripTextBoxWidth";
			this.toolStripTextBoxWidth.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBoxWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxWidth_KeyDown);
			// 
			// toolStripMenuItemHeight
			// 
			this.toolStripMenuItemHeight.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxHeight});
			this.toolStripMenuItemHeight.Name = "toolStripMenuItemHeight";
			this.toolStripMenuItemHeight.Size = new System.Drawing.Size(184, 22);
			this.toolStripMenuItemHeight.Text = "高さ";
			this.toolStripMenuItemHeight.DropDownOpened += new System.EventHandler(this.高さToolStripMenuItem_DropDownOpened);
			// 
			// toolStripTextBoxHeight
			// 
			this.toolStripTextBoxHeight.Name = "toolStripTextBoxHeight";
			this.toolStripTextBoxHeight.Size = new System.Drawing.Size(100, 25);
			this.toolStripTextBoxHeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxHeight_KeyDown);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
			// 
			// 最前列表示toolStripMenuItem
			// 
			this.最前列表示toolStripMenuItem.Name = "最前列表示toolStripMenuItem";
			this.最前列表示toolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.最前列表示toolStripMenuItem.Text = "最前列表示";
			this.最前列表示toolStripMenuItem.Click += new System.EventHandler(this.最前列表示ToolStripMenuItem_Click);
			// 
			// 書き込み欄の表示toolStripMenuItem
			// 
			this.書き込み欄の表示toolStripMenuItem.Name = "書き込み欄の表示toolStripMenuItem";
			this.書き込み欄の表示toolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.書き込み欄の表示toolStripMenuItem.Text = "書き込み欄の表示";
			this.書き込み欄の表示toolStripMenuItem.Click += new System.EventHandler(this.書き込み欄の表示ToolStripMenuItem_Click);
			// 
			// 最下位へスクロールtoolStripMenuItem
			// 
			this.最下位へスクロールtoolStripMenuItem.Name = "最下位へスクロールtoolStripMenuItem";
			this.最下位へスクロールtoolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.最下位へスクロールtoolStripMenuItem.Text = "最下位へスクロール";
			this.最下位へスクロールtoolStripMenuItem.Click += new System.EventHandler(this.最下位へスクロールToolStripMenuItem_Click);
			// 
			// 自動更新ToolStripMenuItem
			// 
			this.自動更新ToolStripMenuItem.Name = "自動更新ToolStripMenuItem";
			this.自動更新ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.自動更新ToolStripMenuItem.Text = "自動更新";
			this.自動更新ToolStripMenuItem.Click += new System.EventHandler(this.自動更新ToolStripMenuItem_Click);
			// 
			// 状態保存ToolStripMenuItem
			// 
			this.状態保存ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.位置を保存するToolStripMenuItem1,
            this.サイズを保存するToolStripMenuItem1});
			this.状態保存ToolStripMenuItem.Name = "状態保存ToolStripMenuItem";
			this.状態保存ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.状態保存ToolStripMenuItem.Text = "状態保存";
			// 
			// 位置を保存するToolStripMenuItem1
			// 
			this.位置を保存するToolStripMenuItem1.Name = "位置を保存するToolStripMenuItem1";
			this.位置を保存するToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.位置を保存するToolStripMenuItem1.Text = "位置を保存する";
			this.位置を保存するToolStripMenuItem1.Click += new System.EventHandler(this.位置を保存するToolStripMenuItem1_Click);
			// 
			// サイズを保存するToolStripMenuItem1
			// 
			this.サイズを保存するToolStripMenuItem1.Name = "サイズを保存するToolStripMenuItem1";
			this.サイズを保存するToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.サイズを保存するToolStripMenuItem1.Text = "サイズを保存する";
			this.サイズを保存するToolStripMenuItem1.Click += new System.EventHandler(this.サイズを保存するToolStripMenuItem1_Click);
			// 
			// 終了時ToolStripMenuItem
			// 
			this.終了時ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.位置を保存するToolStripMenuItem,
            this.サイズを保存するToolStripMenuItem});
			this.終了時ToolStripMenuItem.Name = "終了時ToolStripMenuItem";
			this.終了時ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.終了時ToolStripMenuItem.Text = "終了時";
			this.終了時ToolStripMenuItem.DropDownOpened += new System.EventHandler(this.終了時ToolStripMenuItem_DropDownOpened);
			// 
			// 位置を保存するToolStripMenuItem
			// 
			this.位置を保存するToolStripMenuItem.Name = "位置を保存するToolStripMenuItem";
			this.位置を保存するToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.位置を保存するToolStripMenuItem.Text = "位置を保存する";
			this.位置を保存するToolStripMenuItem.Click += new System.EventHandler(this.位置を保存するToolStripMenuItem_Click);
			// 
			// サイズを保存するToolStripMenuItem
			// 
			this.サイズを保存するToolStripMenuItem.Name = "サイズを保存するToolStripMenuItem";
			this.サイズを保存するToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.サイズを保存するToolStripMenuItem.Text = "サイズを保存する";
			this.サイズを保存するToolStripMenuItem.Click += new System.EventHandler(this.サイズを保存するToolStripMenuItem_Click);
			// 
			// フォントToolStripMenuItem
			// 
			this.フォントToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.色ToolStripMenuItem,
            this.フォントToolStripMenuItem1});
			this.フォントToolStripMenuItem.Enabled = false;
			this.フォントToolStripMenuItem.Name = "フォントToolStripMenuItem";
			this.フォントToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.フォントToolStripMenuItem.Text = "フォント";
			// 
			// 色ToolStripMenuItem
			// 
			this.色ToolStripMenuItem.Enabled = false;
			this.色ToolStripMenuItem.Name = "色ToolStripMenuItem";
			this.色ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.色ToolStripMenuItem.Text = "色";
			// 
			// フォントToolStripMenuItem1
			// 
			this.フォントToolStripMenuItem1.Enabled = false;
			this.フォントToolStripMenuItem1.Name = "フォントToolStripMenuItem1";
			this.フォントToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
			this.フォントToolStripMenuItem1.Text = "フォント";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
			// 
			// 折り返し表示ToolStripMenuItem
			// 
			this.折り返し表示ToolStripMenuItem.Name = "折り返し表示ToolStripMenuItem";
			this.折り返し表示ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.折り返し表示ToolStripMenuItem.Text = "折り返し表示";
			this.折り返し表示ToolStripMenuItem.Click += new System.EventHandler(this.折り返し表示ToolStripMenuItem_Click);
			// 
			// splitContainer
			// 
			this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.panel1);
			this.splitContainer.Panel1.Controls.Add(this.webBrowser);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.textBoxMail);
			this.splitContainer.Panel2.Controls.Add(this.textBoxName);
			this.splitContainer.Panel2.Controls.Add(this.buttonWrite);
			this.splitContainer.Panel2.Controls.Add(this.labelMail);
			this.splitContainer.Panel2.Controls.Add(this.labelName);
			this.splitContainer.Panel2.Controls.Add(this.textBoxMessage);
			this.splitContainer.Panel2.SizeChanged += new System.EventHandler(this.splitContainer_Panel2_SizeChanged);
			this.splitContainer.Size = new System.Drawing.Size(537, 320);
			this.splitContainer.SplitterDistance = 228;
			this.splitContainer.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.comboBox);
			this.panel1.Controls.Add(this.buttonThreadListUpdate);
			this.panel1.Controls.Add(this.toolStrip);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(537, 50);
			this.panel1.TabIndex = 1;
			// 
			// comboBox
			// 
			this.comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox.FormattingEnabled = true;
			this.comboBox.Items.AddRange(new object[] {
            "http://",
            "本スレ"});
			this.comboBox.Location = new System.Drawing.Point(3, 27);
			this.comboBox.Name = "comboBox";
			this.comboBox.Size = new System.Drawing.Size(490, 20);
			this.comboBox.TabIndex = 2;
			this.comboBox.Text = "本スレ";
			this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
			this.comboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
			// 
			// buttonThreadListUpdate
			// 
			this.buttonThreadListUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonThreadListUpdate.Location = new System.Drawing.Point(496, 25);
			this.buttonThreadListUpdate.Name = "buttonThreadListUpdate";
			this.buttonThreadListUpdate.Size = new System.Drawing.Size(38, 23);
			this.buttonThreadListUpdate.TabIndex = 3;
			this.buttonThreadListUpdate.Text = "更新";
			this.buttonThreadListUpdate.UseVisualStyleBackColor = true;
			this.buttonThreadListUpdate.Click += new System.EventHandler(this.buttonThreadListUpdate_Click);
			// 
			// webBrowser
			// 
			this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowser.Location = new System.Drawing.Point(0, 53);
			this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.ScriptErrorsSuppressed = true;
			this.webBrowser.Size = new System.Drawing.Size(534, 173);
			this.webBrowser.TabIndex = 0;
			this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
			// 
			// textBoxMail
			// 
			this.textBoxMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxMail.Location = new System.Drawing.Point(192, 66);
			this.textBoxMail.Name = "textBoxMail";
			this.textBoxMail.Size = new System.Drawing.Size(100, 19);
			this.textBoxMail.TabIndex = 5;
			this.textBoxMail.Text = "sage";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxName.Location = new System.Drawing.Point(47, 66);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(100, 19);
			this.textBoxName.TabIndex = 4;
			// 
			// buttonWrite
			// 
			this.buttonWrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonWrite.Location = new System.Drawing.Point(298, 64);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new System.Drawing.Size(239, 23);
			this.buttonWrite.TabIndex = 3;
			this.buttonWrite.Text = "書き込む";
			this.buttonWrite.UseVisualStyleBackColor = true;
			this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
			// 
			// labelMail
			// 
			this.labelMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelMail.AutoSize = true;
			this.labelMail.Location = new System.Drawing.Point(153, 69);
			this.labelMail.Name = "labelMail";
			this.labelMail.Size = new System.Drawing.Size(33, 12);
			this.labelMail.TabIndex = 2;
			this.labelMail.Text = "メール";
			// 
			// labelName
			// 
			this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(12, 69);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(29, 12);
			this.labelName.TabIndex = 1;
			this.labelName.Text = "名前";
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMessage.Location = new System.Drawing.Point(0, 0);
			this.textBoxMessage.Multiline = true;
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxMessage.Size = new System.Drawing.Size(537, 64);
			this.textBoxMessage.TabIndex = 0;
			this.textBoxMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxMessage_KeyDown);
			this.textBoxMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxMessage_KeyUp);
			this.textBoxMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBoxMessage_MouseMove);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アドレスをコピーToolStripMenuItem,
            this.リンクを開くToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(173, 48);
			// 
			// アドレスをコピーToolStripMenuItem
			// 
			this.アドレスをコピーToolStripMenuItem.Name = "アドレスをコピーToolStripMenuItem";
			this.アドレスをコピーToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.アドレスをコピーToolStripMenuItem.Text = "アドレスをコピー";
			this.アドレスをコピーToolStripMenuItem.Click += new System.EventHandler(this.アドレスをコピーToolStripMenuItem_Click);
			// 
			// リンクを開くToolStripMenuItem
			// 
			this.リンクを開くToolStripMenuItem.Name = "リンクを開くToolStripMenuItem";
			this.リンクを開くToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.リンクを開くToolStripMenuItem.Text = "リンクを開く";
			this.リンクを開くToolStripMenuItem.Click += new System.EventHandler(this.リンクを開くToolStripMenuItem_Click);
			// 
			// backgroundWorkerReload
			// 
			this.backgroundWorkerReload.WorkerSupportsCancellation = true;
			this.backgroundWorkerReload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerReload_DoWork);
			this.backgroundWorkerReload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerReload_RunWorkerCompleted);
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 5000;
			this.toolTip1.InitialDelay = 500;
			this.toolTip1.ReshowDelay = 0;
			this.toolTip1.ShowAlways = true;
			// 
			// ThreadViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(540, 320);
			this.Controls.Add(this.splitContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ThreadViewer";
			this.Text = "PeerstViewer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ThreadViewer_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ThreadViewer_FormClosed);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.Panel2.PerformLayout();
			this.splitContainer.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton toolStripButtonReload;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxReloadTime;
		private System.Windows.Forms.ToolStripButton toolStripButtonAutoReload;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.WebBrowser webBrowser;
		private System.Windows.Forms.Button buttonWrite;
		private System.Windows.Forms.Label labelMail;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.TextBox textBoxMessage;
		private System.Windows.Forms.TextBox textBoxMail;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.ToolStripButton toolStripButtonWriteView;
		private System.Windows.Forms.ToolStripButton toolStripButtonScroolBottom;
		private System.Windows.Forms.ToolStripButton toolStripButtonScrollTop;
		private System.Windows.Forms.ComboBox comboBox;
		private System.Windows.Forms.Button buttonThreadListUpdate;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem アドレスをコピーToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem リンクを開くToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButtonTopMost;
		private System.ComponentModel.BackgroundWorker backgroundWorkerReload;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSetting;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemデフォルト;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemX;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxX;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemY;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxY;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemWidth;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxWidth;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHeight;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxHeight;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem 最前列表示toolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 書き込み欄の表示toolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 最下位へスクロールtoolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 状態保存ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了時ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem フォントToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem 折り返し表示ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 位置を保存するToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem サイズを保存するToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 位置を保存するToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem サイズを保存するToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 色ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem フォントToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 自動更新ToolStripMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolStripButton toolStripButtonOpenClipBoad;
		private System.Windows.Forms.ToolStripButton URLをコピー;
	}
}

