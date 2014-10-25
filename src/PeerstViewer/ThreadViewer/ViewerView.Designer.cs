namespace PeerstViewer.ThreadViewer
{
	partial class ThreadViewerView
	{
		/// <summary>
		/// 必要なデザイナー変数です。
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

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThreadViewerView));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonUpdate = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAutoUpdate = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonBottom = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonTop = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonThreadList = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonWriteField = new System.Windows.Forms.ToolStripButton();
			this.toolStripDropDownButtonFavorite = new System.Windows.Forms.ToolStripDropDownButton();
			this.お気に入りに追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.peerstPlayer総合スレ３ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ものすごい勢いで下位配信を実況するスレ134620ゴミ目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.peerCast用ツール開発者バグ報告専用スレ41ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainerThreadList = new System.Windows.Forms.SplitContainer();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.threadListView = new PeerstLib.Controls.BufferedListView();
			this.columnHeaderNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.threadTitleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.resNumColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.resSpeedColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.sinecColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.splitContainerWriteField = new System.Windows.Forms.SplitContainer();
			this.threadViewer = new PeerstViewer.Controls.ThreadViewer.ThreadViewerControl();
			this.textBoxMail = new System.Windows.Forms.TextBox();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.labelMail = new System.Windows.Forms.Label();
			this.textBoxMessage = new System.Windows.Forms.TextBox();
			this.labelName = new System.Windows.Forms.Label();
			this.autoUpdateTimer = new System.Windows.Forms.Timer(this.components);
			this.textBoxUrl = new System.Windows.Forms.TextBox();
			this.toolStripButtonSetting = new System.Windows.Forms.ToolStripButton();
			this.toolStrip.SuspendLayout();
			this.splitContainerThreadList.Panel1.SuspendLayout();
			this.splitContainerThreadList.Panel2.SuspendLayout();
			this.splitContainerThreadList.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.splitContainerWriteField.Panel1.SuspendLayout();
			this.splitContainerWriteField.Panel2.SuspendLayout();
			this.splitContainerWriteField.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonUpdate,
            this.toolStripButtonAutoUpdate,
            this.toolStripButtonBottom,
            this.toolStripButtonTop,
            this.toolStripButtonThreadList,
            this.toolStripButtonWriteField,
            this.toolStripDropDownButtonFavorite,
            this.toolStripButtonSetting});
			this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(776, 23);
			this.toolStrip.Stretch = true;
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "toolStrip1";
			// 
			// toolStripButtonUpdate
			// 
			this.toolStripButtonUpdate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUpdate.Image")));
			this.toolStripButtonUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonUpdate.Name = "toolStripButtonUpdate";
			this.toolStripButtonUpdate.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripButtonUpdate.Size = new System.Drawing.Size(49, 20);
			this.toolStripButtonUpdate.Text = "更新";
			// 
			// toolStripButtonAutoUpdate
			// 
			this.toolStripButtonAutoUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAutoUpdate.Enabled = false;
			this.toolStripButtonAutoUpdate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAutoUpdate.Image")));
			this.toolStripButtonAutoUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAutoUpdate.Name = "toolStripButtonAutoUpdate";
			this.toolStripButtonAutoUpdate.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripButtonAutoUpdate.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonAutoUpdate.Text = "自動更新";
			// 
			// toolStripButtonBottom
			// 
			this.toolStripButtonBottom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonBottom.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBottom.Image")));
			this.toolStripButtonBottom.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonBottom.Name = "toolStripButtonBottom";
			this.toolStripButtonBottom.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripButtonBottom.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonBottom.Text = "下へスクロール";
			// 
			// toolStripButtonTop
			// 
			this.toolStripButtonTop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTop.Image")));
			this.toolStripButtonTop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonTop.Name = "toolStripButtonTop";
			this.toolStripButtonTop.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripButtonTop.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonTop.ToolTipText = "上へスクロール";
			// 
			// toolStripButtonThreadList
			// 
			this.toolStripButtonThreadList.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonThreadList.Image")));
			this.toolStripButtonThreadList.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonThreadList.Name = "toolStripButtonThreadList";
			this.toolStripButtonThreadList.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripButtonThreadList.Size = new System.Drawing.Size(83, 20);
			this.toolStripButtonThreadList.Text = "スレッド一覧";
			// 
			// toolStripButtonWriteField
			// 
			this.toolStripButtonWriteField.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonWriteField.Image")));
			this.toolStripButtonWriteField.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonWriteField.Name = "toolStripButtonWriteField";
			this.toolStripButtonWriteField.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripButtonWriteField.Size = new System.Drawing.Size(69, 20);
			this.toolStripButtonWriteField.Text = "書き込み";
			// 
			// toolStripDropDownButtonFavorite
			// 
			this.toolStripDropDownButtonFavorite.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.お気に入りに追加ToolStripMenuItem,
            this.toolStripSeparator1,
            this.peerstPlayer総合スレ３ToolStripMenuItem,
            this.ものすごい勢いで下位配信を実況するスレ134620ゴミ目ToolStripMenuItem,
            this.peerCast用ツール開発者バグ報告専用スレ41ToolStripMenuItem});
			this.toolStripDropDownButtonFavorite.Enabled = false;
			this.toolStripDropDownButtonFavorite.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonFavorite.Image")));
			this.toolStripDropDownButtonFavorite.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonFavorite.Name = "toolStripDropDownButtonFavorite";
			this.toolStripDropDownButtonFavorite.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripDropDownButtonFavorite.Size = new System.Drawing.Size(85, 20);
			this.toolStripDropDownButtonFavorite.Text = "お気に入り";
			// 
			// お気に入りに追加ToolStripMenuItem
			// 
			this.お気に入りに追加ToolStripMenuItem.Name = "お気に入りに追加ToolStripMenuItem";
			this.お気に入りに追加ToolStripMenuItem.Size = new System.Drawing.Size(336, 22);
			this.お気に入りに追加ToolStripMenuItem.Text = "追加";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(333, 6);
			// 
			// peerstPlayer総合スレ３ToolStripMenuItem
			// 
			this.peerstPlayer総合スレ３ToolStripMenuItem.Name = "peerstPlayer総合スレ３ToolStripMenuItem";
			this.peerstPlayer総合スレ３ToolStripMenuItem.Size = new System.Drawing.Size(336, 22);
			this.peerstPlayer総合スレ３ToolStripMenuItem.Text = "PeerstPlayer総合スレ３";
			// 
			// ものすごい勢いで下位配信を実況するスレ134620ゴミ目ToolStripMenuItem
			// 
			this.ものすごい勢いで下位配信を実況するスレ134620ゴミ目ToolStripMenuItem.Name = "ものすごい勢いで下位配信を実況するスレ134620ゴミ目ToolStripMenuItem";
			this.ものすごい勢いで下位配信を実況するスレ134620ゴミ目ToolStripMenuItem.Size = new System.Drawing.Size(336, 22);
			this.ものすごい勢いで下位配信を実況するスレ134620ゴミ目ToolStripMenuItem.Text = "ものすごい勢いで下位配信を実況するスレ 134620ゴミ目";
			// 
			// peerCast用ツール開発者バグ報告専用スレ41ToolStripMenuItem
			// 
			this.peerCast用ツール開発者バグ報告専用スレ41ToolStripMenuItem.Name = "peerCast用ツール開発者バグ報告専用スレ41ToolStripMenuItem";
			this.peerCast用ツール開発者バグ報告専用スレ41ToolStripMenuItem.Size = new System.Drawing.Size(336, 22);
			this.peerCast用ツール開発者バグ報告専用スレ41ToolStripMenuItem.Text = "PeerCast用ツール開発者、バグ報告専用スレ 41";
			// 
			// splitContainerThreadList
			// 
			this.splitContainerThreadList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerThreadList.Location = new System.Drawing.Point(0, 42);
			this.splitContainerThreadList.Name = "splitContainerThreadList";
			// 
			// splitContainerThreadList.Panel1
			// 
			this.splitContainerThreadList.Panel1.Controls.Add(this.toolStrip1);
			this.splitContainerThreadList.Panel1.Controls.Add(this.threadListView);
			// 
			// splitContainerThreadList.Panel2
			// 
			this.splitContainerThreadList.Panel2.Controls.Add(this.splitContainerWriteField);
			this.splitContainerThreadList.Size = new System.Drawing.Size(776, 403);
			this.splitContainerThreadList.SplitterDistance = 315;
			this.splitContainerThreadList.SplitterWidth = 6;
			this.splitContainerThreadList.TabIndex = 3;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(315, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(63, 22);
			this.toolStripLabel1.Text = "スレッド一覧";
			// 
			// threadListView
			// 
			this.threadListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.threadListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderNo,
            this.threadTitleColumnHeader,
            this.resNumColumnHeader,
            this.resSpeedColumnHeader,
            this.sinecColumnHeader});
			this.threadListView.FullRowSelect = true;
			this.threadListView.GridLines = true;
			this.threadListView.Location = new System.Drawing.Point(0, 28);
			this.threadListView.MultiSelect = false;
			this.threadListView.Name = "threadListView";
			this.threadListView.Size = new System.Drawing.Size(312, 381);
			this.threadListView.TabIndex = 1;
			this.threadListView.UseCompatibleStateImageBehavior = false;
			this.threadListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderNo
			// 
			this.columnHeaderNo.Text = "No";
			this.columnHeaderNo.Width = 34;
			// 
			// threadTitleColumnHeader
			// 
			this.threadTitleColumnHeader.Text = "スレッドタイトル";
			this.threadTitleColumnHeader.Width = 120;
			// 
			// resNumColumnHeader
			// 
			this.resNumColumnHeader.Text = "レス数";
			this.resNumColumnHeader.Width = 43;
			// 
			// resSpeedColumnHeader
			// 
			this.resSpeedColumnHeader.Text = "勢い";
			this.resSpeedColumnHeader.Width = 44;
			// 
			// sinecColumnHeader
			// 
			this.sinecColumnHeader.Text = "Since";
			// 
			// splitContainerWriteField
			// 
			this.splitContainerWriteField.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerWriteField.Location = new System.Drawing.Point(0, 0);
			this.splitContainerWriteField.Name = "splitContainerWriteField";
			this.splitContainerWriteField.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerWriteField.Panel1
			// 
			this.splitContainerWriteField.Panel1.Controls.Add(this.threadViewer);
			// 
			// splitContainerWriteField.Panel2
			// 
			this.splitContainerWriteField.Panel2.Controls.Add(this.textBoxMail);
			this.splitContainerWriteField.Panel2.Controls.Add(this.textBoxName);
			this.splitContainerWriteField.Panel2.Controls.Add(this.buttonWrite);
			this.splitContainerWriteField.Panel2.Controls.Add(this.labelMail);
			this.splitContainerWriteField.Panel2.Controls.Add(this.textBoxMessage);
			this.splitContainerWriteField.Panel2.Controls.Add(this.labelName);
			this.splitContainerWriteField.Size = new System.Drawing.Size(455, 403);
			this.splitContainerWriteField.SplitterDistance = 271;
			this.splitContainerWriteField.SplitterWidth = 6;
			this.splitContainerWriteField.TabIndex = 1;
			// 
			// threadViewer
			// 
			this.threadViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.threadViewer.DocumentText = "";
			this.threadViewer.Location = new System.Drawing.Point(0, 0);
			this.threadViewer.Name = "threadViewer";
			this.threadViewer.Size = new System.Drawing.Size(455, 271);
			this.threadViewer.TabIndex = 0;
			// 
			// textBoxMail
			// 
			this.textBoxMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxMail.Location = new System.Drawing.Point(178, 99);
			this.textBoxMail.Name = "textBoxMail";
			this.textBoxMail.Size = new System.Drawing.Size(100, 19);
			this.textBoxMail.TabIndex = 11;
			this.textBoxMail.Text = "sage";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxName.Location = new System.Drawing.Point(33, 99);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(100, 19);
			this.textBoxName.TabIndex = 10;
			// 
			// buttonWrite
			// 
			this.buttonWrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonWrite.Location = new System.Drawing.Point(284, 97);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new System.Drawing.Size(168, 23);
			this.buttonWrite.TabIndex = 9;
			this.buttonWrite.Text = "書き込む";
			this.buttonWrite.UseVisualStyleBackColor = true;
			// 
			// labelMail
			// 
			this.labelMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelMail.AutoSize = true;
			this.labelMail.Location = new System.Drawing.Point(139, 102);
			this.labelMail.Name = "labelMail";
			this.labelMail.Size = new System.Drawing.Size(33, 12);
			this.labelMail.TabIndex = 8;
			this.labelMail.Text = "メール";
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMessage.Location = new System.Drawing.Point(3, 3);
			this.textBoxMessage.Multiline = true;
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxMessage.Size = new System.Drawing.Size(449, 94);
			this.textBoxMessage.TabIndex = 6;
			// 
			// labelName
			// 
			this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(3, 102);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(29, 12);
			this.labelName.TabIndex = 7;
			this.labelName.Text = "名前";
			// 
			// autoUpdateTimer
			// 
			this.autoUpdateTimer.Enabled = true;
			this.autoUpdateTimer.Interval = 7000;
			// 
			// textBoxUrl
			// 
			this.textBoxUrl.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBoxUrl.Location = new System.Drawing.Point(0, 23);
			this.textBoxUrl.Name = "textBoxUrl";
			this.textBoxUrl.Size = new System.Drawing.Size(776, 19);
			this.textBoxUrl.TabIndex = 2;
			// 
			// toolStripButtonSetting
			// 
			this.toolStripButtonSetting.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSetting.Image")));
			this.toolStripButtonSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSetting.Name = "toolStripButtonSetting";
			this.toolStripButtonSetting.Size = new System.Drawing.Size(49, 20);
			this.toolStripButtonSetting.Text = "設定";
			// 
			// ThreadViewerView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(776, 445);
			this.Controls.Add(this.splitContainerThreadList);
			this.Controls.Add(this.textBoxUrl);
			this.Controls.Add(this.toolStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ThreadViewerView";
			this.Text = "PeerstViewer";
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.splitContainerThreadList.Panel1.ResumeLayout(false);
			this.splitContainerThreadList.Panel1.PerformLayout();
			this.splitContainerThreadList.Panel2.ResumeLayout(false);
			this.splitContainerThreadList.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainerWriteField.Panel1.ResumeLayout(false);
			this.splitContainerWriteField.Panel2.ResumeLayout(false);
			this.splitContainerWriteField.Panel2.PerformLayout();
			this.splitContainerWriteField.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton toolStripButtonUpdate;
		private System.Windows.Forms.ToolStripButton toolStripButtonAutoUpdate;
		private System.Windows.Forms.ToolStripButton toolStripButtonBottom;
		private System.Windows.Forms.ToolStripButton toolStripButtonTop;
		private System.Windows.Forms.ToolStripButton toolStripButtonWriteField;
		private System.Windows.Forms.TextBox textBoxUrl;
		private System.Windows.Forms.SplitContainer splitContainerThreadList;
		private System.Windows.Forms.SplitContainer splitContainerWriteField;
		private System.Windows.Forms.TextBox textBoxMail;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Button buttonWrite;
		private System.Windows.Forms.Label labelMail;
		private System.Windows.Forms.TextBox textBoxMessage;
		private System.Windows.Forms.Label labelName;
		private PeerstLib.Controls.BufferedListView threadListView;
		private System.Windows.Forms.ColumnHeader columnHeaderNo;
		private System.Windows.Forms.ColumnHeader threadTitleColumnHeader;
		private System.Windows.Forms.ColumnHeader resNumColumnHeader;
		private System.Windows.Forms.ColumnHeader resSpeedColumnHeader;
		private System.Windows.Forms.ColumnHeader sinecColumnHeader;
		private System.Windows.Forms.ToolStripButton toolStripButtonThreadList;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonFavorite;
		private System.Windows.Forms.ToolStripMenuItem peerstPlayer総合スレ３ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ものすごい勢いで下位配信を実況するスレ134620ゴミ目ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem peerCast用ツール開発者バグ報告専用スレ41ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem お気に入りに追加ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.Timer autoUpdateTimer;
		private Controls.ThreadViewer.ThreadViewerControl threadViewer;
		private System.Windows.Forms.ToolStripButton toolStripButtonSetting;
	}
}

