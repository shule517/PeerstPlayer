using PeerstLib.Controls;
using PeerstPlayer.Controls.PecaPlayer;
using PeerstPlayer.Controls.StatusBar;
namespace PeerstPlayer.Forms.Setting
{
	partial class PlayerView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerView));
			this.toolStrip = new PeerstLib.Controls.ToolStripEx();
			this.openViewerToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.minToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.maxToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.closeToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.statusBar = new PeerstPlayer.Controls.StatusBar.StatusBarControl();
			this.pecaPlayer = new PeerstPlayer.Controls.PecaPlayer.PecaPlayerControl();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scale50PerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scale75PerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scale100PerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scale150PerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scale200PerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sizeToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.size160x120ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.size320x240ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.size480x360ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.size640x480ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.size800x600ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fitMovieSizeToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.fitMovieSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.画面分割ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.screenSplitWidthx5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.screenSplitWidthx4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.screenSplitWidthx3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.screenSplitWidthx2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.screenSplitToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.screenSplitHeightx5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.screenSplitHeightx4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.screenSplitHeightx3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.screenSplitHeightx2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.functionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.topMostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updateChannelInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.volumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.volumeUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.volumeDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.muteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.volumeToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.volumeBalanceLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.volumeBalanceMiddleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.volumeBalanceRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.wmpMenuToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.wmpMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip.BackColor = System.Drawing.SystemColors.Control;
			this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.GripMargin = new System.Windows.Forms.Padding(0);
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openViewerToolStripButton,
            this.minToolStripButton,
            this.maxToolStripButton,
            this.closeToolStripButton});
			this.toolStrip.Location = new System.Drawing.Point(381, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
			this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.toolStrip.Size = new System.Drawing.Size(99, 25);
			this.toolStrip.TabIndex = 2;
			// 
			// openViewerToolStripButton
			// 
			this.openViewerToolStripButton.BackColor = System.Drawing.SystemColors.Control;
			this.openViewerToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.openViewerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openViewerToolStripButton.Font = new System.Drawing.Font("Marlett", 9F);
			this.openViewerToolStripButton.ForeColor = System.Drawing.Color.Black;
			this.openViewerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openViewerToolStripButton.Image")));
			this.openViewerToolStripButton.Name = "openViewerToolStripButton";
			this.openViewerToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.openViewerToolStripButton.ToolTipText = "Viewerを開く";
			// 
			// minToolStripButton
			// 
			this.minToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.minToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.minToolStripButton.Font = new System.Drawing.Font("Marlett", 9F);
			this.minToolStripButton.ForeColor = System.Drawing.Color.Black;
			this.minToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("minToolStripButton.Image")));
			this.minToolStripButton.Name = "minToolStripButton";
			this.minToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.minToolStripButton.ToolTipText = "最小化";
			// 
			// maxToolStripButton
			// 
			this.maxToolStripButton.BackColor = System.Drawing.SystemColors.Control;
			this.maxToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.maxToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.maxToolStripButton.Font = new System.Drawing.Font("Marlett", 9F);
			this.maxToolStripButton.ForeColor = System.Drawing.Color.Black;
			this.maxToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("maxToolStripButton.Image")));
			this.maxToolStripButton.Name = "maxToolStripButton";
			this.maxToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.maxToolStripButton.ToolTipText = "最大化";
			// 
			// closeToolStripButton
			// 
			this.closeToolStripButton.BackColor = System.Drawing.SystemColors.Control;
			this.closeToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.closeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.closeToolStripButton.Font = new System.Drawing.Font("Marlett", 9F);
			this.closeToolStripButton.ForeColor = System.Drawing.Color.Black;
			this.closeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("closeToolStripButton.Image")));
			this.closeToolStripButton.Name = "closeToolStripButton";
			this.closeToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.closeToolStripButton.ToolTipText = "閉じる";
			// 
			// statusBar
			// 
			this.statusBar.ChannelDetail = "チャンネル情報";
			this.statusBar.Location = new System.Drawing.Point(0, 360);
			this.statusBar.Margin = new System.Windows.Forms.Padding(0);
			this.statusBar.MovieStatus = "";
			this.statusBar.Name = "statusBar";
			this.statusBar.SelectThreadUrl = null;
			this.statusBar.Size = new System.Drawing.Size(480, 18);
			this.statusBar.TabIndex = 1;
			this.statusBar.Volume = "50";
			this.statusBar.WriteFieldVisible = false;
			// 
			// pecaPlayer
			// 
			this.pecaPlayer.BackColor = System.Drawing.Color.Black;
			this.pecaPlayer.ChannelInfo = null;
			this.pecaPlayer.ClickPoint = new System.Drawing.Point(0, 0);
			this.pecaPlayer.EnableContextMenu = false;
			this.pecaPlayer.Location = new System.Drawing.Point(0, 0);
			this.pecaPlayer.Margin = new System.Windows.Forms.Padding(0);
			this.pecaPlayer.Mute = false;
			this.pecaPlayer.Name = "pecaPlayer";
			this.pecaPlayer.Size = new System.Drawing.Size(480, 360);
			this.pecaPlayer.TabIndex = 0;
			this.pecaPlayer.Volume = 50;
			this.pecaPlayer.VolumeBalance = 0;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SizeToolStripMenuItem,
            this.画面分割ToolStripMenuItem,
            this.functionToolStripMenuItem,
            this.volumeToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.wmpMenuToolStripSeparator,
            this.wmpMenuToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(154, 142);
			// 
			// SizeToolStripMenuItem
			// 
			this.SizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scale50PerToolStripMenuItem,
            this.scale75PerToolStripMenuItem,
            this.scale100PerToolStripMenuItem,
            this.scale150PerToolStripMenuItem,
            this.scale200PerToolStripMenuItem,
            this.sizeToolStripSeparator,
            this.size160x120ToolStripMenuItem,
            this.size320x240ToolStripMenuItem,
            this.size480x360ToolStripMenuItem,
            this.size640x480ToolStripMenuItem,
            this.size800x600ToolStripMenuItem,
            this.fitMovieSizeToolStripSeparator,
            this.fitMovieSizeToolStripMenuItem});
			this.SizeToolStripMenuItem.Name = "SizeToolStripMenuItem";
			this.SizeToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.SizeToolStripMenuItem.Text = "サイズ";
			// 
			// scale50PerToolStripMenuItem
			// 
			this.scale50PerToolStripMenuItem.Name = "scale50PerToolStripMenuItem";
			this.scale50PerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.scale50PerToolStripMenuItem.Text = "50%";
			// 
			// scale75PerToolStripMenuItem
			// 
			this.scale75PerToolStripMenuItem.Name = "scale75PerToolStripMenuItem";
			this.scale75PerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.scale75PerToolStripMenuItem.Text = "75%";
			// 
			// scale100PerToolStripMenuItem
			// 
			this.scale100PerToolStripMenuItem.Name = "scale100PerToolStripMenuItem";
			this.scale100PerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.scale100PerToolStripMenuItem.Text = "100%";
			// 
			// scale150PerToolStripMenuItem
			// 
			this.scale150PerToolStripMenuItem.Name = "scale150PerToolStripMenuItem";
			this.scale150PerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.scale150PerToolStripMenuItem.Text = "150%";
			// 
			// scale200PerToolStripMenuItem
			// 
			this.scale200PerToolStripMenuItem.Name = "scale200PerToolStripMenuItem";
			this.scale200PerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.scale200PerToolStripMenuItem.Text = "200%";
			// 
			// sizeToolStripSeparator
			// 
			this.sizeToolStripSeparator.Name = "sizeToolStripSeparator";
			this.sizeToolStripSeparator.Size = new System.Drawing.Size(149, 6);
			// 
			// size160x120ToolStripMenuItem
			// 
			this.size160x120ToolStripMenuItem.Name = "size160x120ToolStripMenuItem";
			this.size160x120ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.size160x120ToolStripMenuItem.Text = "160 x 120";
			// 
			// size320x240ToolStripMenuItem
			// 
			this.size320x240ToolStripMenuItem.Name = "size320x240ToolStripMenuItem";
			this.size320x240ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.size320x240ToolStripMenuItem.Text = "320 x 240";
			// 
			// size480x360ToolStripMenuItem
			// 
			this.size480x360ToolStripMenuItem.Name = "size480x360ToolStripMenuItem";
			this.size480x360ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.size480x360ToolStripMenuItem.Text = "480 x 360";
			// 
			// size640x480ToolStripMenuItem
			// 
			this.size640x480ToolStripMenuItem.Name = "size640x480ToolStripMenuItem";
			this.size640x480ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.size640x480ToolStripMenuItem.Text = "640 x 480";
			// 
			// size800x600ToolStripMenuItem
			// 
			this.size800x600ToolStripMenuItem.Name = "size800x600ToolStripMenuItem";
			this.size800x600ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.size800x600ToolStripMenuItem.Text = "800 x 600";
			// 
			// fitMovieSizeToolStripSeparator
			// 
			this.fitMovieSizeToolStripSeparator.Name = "fitMovieSizeToolStripSeparator";
			this.fitMovieSizeToolStripSeparator.Size = new System.Drawing.Size(149, 6);
			// 
			// fitMovieSizeToolStripMenuItem
			// 
			this.fitMovieSizeToolStripMenuItem.Name = "fitMovieSizeToolStripMenuItem";
			this.fitMovieSizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.fitMovieSizeToolStripMenuItem.Text = "黒枠を消す";
			// 
			// 画面分割ToolStripMenuItem
			// 
			this.画面分割ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.screenSplitWidthx5ToolStripMenuItem,
            this.screenSplitWidthx4ToolStripMenuItem,
            this.screenSplitWidthx3ToolStripMenuItem,
            this.screenSplitWidthx2ToolStripMenuItem,
            this.screenSplitToolStripSeparator,
            this.screenSplitHeightx5ToolStripMenuItem,
            this.screenSplitHeightx4ToolStripMenuItem,
            this.screenSplitHeightx3ToolStripMenuItem,
            this.screenSplitHeightx2ToolStripMenuItem});
			this.画面分割ToolStripMenuItem.Name = "画面分割ToolStripMenuItem";
			this.画面分割ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.画面分割ToolStripMenuItem.Text = "画面分割";
			// 
			// screenSplitWidthx5ToolStripMenuItem
			// 
			this.screenSplitWidthx5ToolStripMenuItem.Name = "screenSplitWidthx5ToolStripMenuItem";
			this.screenSplitWidthx5ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.screenSplitWidthx5ToolStripMenuItem.Text = "幅：5分の1";
			// 
			// screenSplitWidthx4ToolStripMenuItem
			// 
			this.screenSplitWidthx4ToolStripMenuItem.Name = "screenSplitWidthx4ToolStripMenuItem";
			this.screenSplitWidthx4ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.screenSplitWidthx4ToolStripMenuItem.Text = "幅：4分の1";
			// 
			// screenSplitWidthx3ToolStripMenuItem
			// 
			this.screenSplitWidthx3ToolStripMenuItem.Name = "screenSplitWidthx3ToolStripMenuItem";
			this.screenSplitWidthx3ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.screenSplitWidthx3ToolStripMenuItem.Text = "幅：3分の1";
			// 
			// screenSplitWidthx2ToolStripMenuItem
			// 
			this.screenSplitWidthx2ToolStripMenuItem.Name = "screenSplitWidthx2ToolStripMenuItem";
			this.screenSplitWidthx2ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.screenSplitWidthx2ToolStripMenuItem.Text = "幅：2分の1";
			// 
			// screenSplitToolStripSeparator
			// 
			this.screenSplitToolStripSeparator.Name = "screenSplitToolStripSeparator";
			this.screenSplitToolStripSeparator.Size = new System.Drawing.Size(147, 6);
			// 
			// screenSplitHeightx5ToolStripMenuItem
			// 
			this.screenSplitHeightx5ToolStripMenuItem.Name = "screenSplitHeightx5ToolStripMenuItem";
			this.screenSplitHeightx5ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.screenSplitHeightx5ToolStripMenuItem.Text = "高さ：5分の1";
			// 
			// screenSplitHeightx4ToolStripMenuItem
			// 
			this.screenSplitHeightx4ToolStripMenuItem.Name = "screenSplitHeightx4ToolStripMenuItem";
			this.screenSplitHeightx4ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.screenSplitHeightx4ToolStripMenuItem.Text = "高さ：4分の1";
			// 
			// screenSplitHeightx3ToolStripMenuItem
			// 
			this.screenSplitHeightx3ToolStripMenuItem.Name = "screenSplitHeightx3ToolStripMenuItem";
			this.screenSplitHeightx3ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.screenSplitHeightx3ToolStripMenuItem.Text = "高さ：3分の1";
			// 
			// screenSplitHeightx2ToolStripMenuItem
			// 
			this.screenSplitHeightx2ToolStripMenuItem.Name = "screenSplitHeightx2ToolStripMenuItem";
			this.screenSplitHeightx2ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.screenSplitHeightx2ToolStripMenuItem.Text = "高さ：2分の1";
			// 
			// functionToolStripMenuItem
			// 
			this.functionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.topMostToolStripMenuItem,
            this.updateChannelInfoToolStripMenuItem,
            this.bumpToolStripMenuItem});
			this.functionToolStripMenuItem.Name = "functionToolStripMenuItem";
			this.functionToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.functionToolStripMenuItem.Text = "機能";
			// 
			// topMostToolStripMenuItem
			// 
			this.topMostToolStripMenuItem.Name = "topMostToolStripMenuItem";
			this.topMostToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.topMostToolStripMenuItem.Text = "最前列表示";
			// 
			// updateChannelInfoToolStripMenuItem
			// 
			this.updateChannelInfoToolStripMenuItem.Name = "updateChannelInfoToolStripMenuItem";
			this.updateChannelInfoToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.updateChannelInfoToolStripMenuItem.Text = "チャンネル情報更新";
			// 
			// bumpToolStripMenuItem
			// 
			this.bumpToolStripMenuItem.Name = "bumpToolStripMenuItem";
			this.bumpToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.bumpToolStripMenuItem.Text = "Bump(再接続)";
			// 
			// volumeToolStripMenuItem
			// 
			this.volumeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.volumeUpToolStripMenuItem,
            this.volumeDownToolStripMenuItem,
            this.muteToolStripMenuItem,
            this.volumeToolStripSeparator,
            this.volumeBalanceLeftToolStripMenuItem,
            this.volumeBalanceMiddleToolStripMenuItem,
            this.volumeBalanceRightToolStripMenuItem});
			this.volumeToolStripMenuItem.Name = "volumeToolStripMenuItem";
			this.volumeToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.volumeToolStripMenuItem.Text = "音量";
			// 
			// volumeUpToolStripMenuItem
			// 
			this.volumeUpToolStripMenuItem.Name = "volumeUpToolStripMenuItem";
			this.volumeUpToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.volumeUpToolStripMenuItem.Text = "上げる";
			// 
			// volumeDownToolStripMenuItem
			// 
			this.volumeDownToolStripMenuItem.Name = "volumeDownToolStripMenuItem";
			this.volumeDownToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.volumeDownToolStripMenuItem.Text = "下げる";
			// 
			// muteToolStripMenuItem
			// 
			this.muteToolStripMenuItem.Name = "muteToolStripMenuItem";
			this.muteToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.muteToolStripMenuItem.Text = "ミュート";
			// 
			// volumeToolStripSeparator
			// 
			this.volumeToolStripSeparator.Name = "volumeToolStripSeparator";
			this.volumeToolStripSeparator.Size = new System.Drawing.Size(157, 6);
			// 
			// volumeBalanceLeftToolStripMenuItem
			// 
			this.volumeBalanceLeftToolStripMenuItem.Name = "volumeBalanceLeftToolStripMenuItem";
			this.volumeBalanceLeftToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.volumeBalanceLeftToolStripMenuItem.Text = "バランス：左";
			// 
			// volumeBalanceMiddleToolStripMenuItem
			// 
			this.volumeBalanceMiddleToolStripMenuItem.Name = "volumeBalanceMiddleToolStripMenuItem";
			this.volumeBalanceMiddleToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.volumeBalanceMiddleToolStripMenuItem.Text = "バランス：中央";
			// 
			// volumeBalanceRightToolStripMenuItem
			// 
			this.volumeBalanceRightToolStripMenuItem.Name = "volumeBalanceRightToolStripMenuItem";
			this.volumeBalanceRightToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.volumeBalanceRightToolStripMenuItem.Text = "バランス：右";
			// 
			// settingToolStripMenuItem
			// 
			this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
			this.settingToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.settingToolStripMenuItem.Text = "設定";
			// 
			// wmpMenuToolStripSeparator
			// 
			this.wmpMenuToolStripSeparator.Name = "wmpMenuToolStripSeparator";
			this.wmpMenuToolStripSeparator.Size = new System.Drawing.Size(150, 6);
			// 
			// wmpMenuToolStripMenuItem
			// 
			this.wmpMenuToolStripMenuItem.Name = "wmpMenuToolStripMenuItem";
			this.wmpMenuToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.wmpMenuToolStripMenuItem.Text = "WMPメニュー";
			// 
			// PlayerView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(480, 379);
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.pecaPlayer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PlayerView";
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private PecaPlayerControl pecaPlayer;
		private StatusBarControl statusBar;
		private ToolStripEx toolStrip;
		private System.Windows.Forms.ToolStripButton minToolStripButton;
		private System.Windows.Forms.ToolStripButton maxToolStripButton;
		private System.Windows.Forms.ToolStripButton closeToolStripButton;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem wmpMenuToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scale50PerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scale75PerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scale100PerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scale150PerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scale200PerToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator sizeToolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem size160x120ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem size320x240ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem size480x360ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem size640x480ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem size800x600ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator wmpMenuToolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem functionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem updateChannelInfoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem topMostToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator fitMovieSizeToolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem fitMovieSizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bumpToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton openViewerToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem 画面分割ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem screenSplitWidthx5ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem screenSplitWidthx4ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem screenSplitWidthx3ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem screenSplitWidthx2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator screenSplitToolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem screenSplitHeightx5ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem screenSplitHeightx4ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem screenSplitHeightx3ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem screenSplitHeightx2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem volumeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem volumeUpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem volumeDownToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem muteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator volumeToolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem volumeBalanceLeftToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem volumeBalanceMiddleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem volumeBalanceRightToolStripMenuItem;
	}
}

