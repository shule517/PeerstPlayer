namespace PeerstPlayer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerView));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.minToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.maxToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.closeToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.statusBar = new PeerstPlayer.Control.StatusBar();
			this.pecaPlayer = new PeerstPlayer.Control.PecaPlayer();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.GripMargin = new System.Windows.Forms.Padding(0);
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minToolStripButton,
            this.maxToolStripButton,
            this.closeToolStripButton});
			this.toolStrip.Location = new System.Drawing.Point(444, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
			this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.toolStrip.Size = new System.Drawing.Size(71, 25);
			this.toolStrip.TabIndex = 3;
			// 
			// minToolStripButton
			// 
			this.minToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.minToolStripButton.Font = new System.Drawing.Font("Marlett", 9F);
			this.minToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("minToolStripButton.Image")));
			this.minToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.minToolStripButton.Name = "minToolStripButton";
			this.minToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.minToolStripButton.Text = "0";
			this.minToolStripButton.ToolTipText = "最小化";
			// 
			// maxToolStripButton
			// 
			this.maxToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.maxToolStripButton.Font = new System.Drawing.Font("Marlett", 9F);
			this.maxToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("maxToolStripButton.Image")));
			this.maxToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.maxToolStripButton.Name = "maxToolStripButton";
			this.maxToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.maxToolStripButton.Text = "1";
			this.maxToolStripButton.ToolTipText = "最大化";
			// 
			// closeToolStripButton
			// 
			this.closeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.closeToolStripButton.Font = new System.Drawing.Font("Marlett", 9F);
			this.closeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("closeToolStripButton.Image")));
			this.closeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.closeToolStripButton.Name = "closeToolStripButton";
			this.closeToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.closeToolStripButton.Text = "r";
			this.closeToolStripButton.ToolTipText = "閉じる";
			// 
			// statusBar
			// 
			this.statusBar.ChannelDetail = "チャンネル情報";
			this.statusBar.Location = new System.Drawing.Point(0, 360);
			this.statusBar.Margin = new System.Windows.Forms.Padding(0);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(480, 43);
			this.statusBar.TabIndex = 1;
			// 
			// pecaPlayer
			// 
			this.pecaPlayer.Location = new System.Drawing.Point(0, 0);
			this.pecaPlayer.Margin = new System.Windows.Forms.Padding(0);
			this.pecaPlayer.Name = "pecaPlayer";
			this.pecaPlayer.Size = new System.Drawing.Size(480, 360);
			this.pecaPlayer.TabIndex = 0;
			// 
			// PlayerView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(514, 449);
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.pecaPlayer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PlayerView";
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Control.PecaPlayer pecaPlayer;
		private Control.StatusBar statusBar;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton minToolStripButton;
		private System.Windows.Forms.ToolStripButton maxToolStripButton;
		private System.Windows.Forms.ToolStripButton closeToolStripButton;
	}
}

