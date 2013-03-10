namespace PeerstPlayer
{
	partial class PlayerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.channelInfoLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.writeField = new System.Windows.Forms.TextBox();
			this.threadTitleLabel = new System.Windows.Forms.Label();
			this.wmp = new AxWMPLib.AxWindowsMediaPlayer();
			this.statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.wmp)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.BackColor = System.Drawing.Color.Black;
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelInfoLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 191);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(284, 23);
			this.statusStrip.TabIndex = 1;
			// 
			// channelInfoLabel
			// 
			this.channelInfoLabel.ForeColor = System.Drawing.Color.SpringGreen;
			this.channelInfoLabel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.channelInfoLabel.Name = "channelInfoLabel";
			this.channelInfoLabel.Size = new System.Drawing.Size(269, 18);
			this.channelInfoLabel.Spring = true;
			this.channelInfoLabel.Text = "チャンネル情報";
			this.channelInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// writeField
			// 
			this.writeField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.writeField.Location = new System.Drawing.Point(0, 169);
			this.writeField.Name = "writeField";
			this.writeField.Size = new System.Drawing.Size(284, 19);
			this.writeField.TabIndex = 2;
			this.writeField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.writeField_KeyDown);
			// 
			// threadTitleLabel
			// 
			this.threadTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.threadTitleLabel.ForeColor = System.Drawing.Color.SpringGreen;
			this.threadTitleLabel.Location = new System.Drawing.Point(-2, 154);
			this.threadTitleLabel.Name = "threadTitleLabel";
			this.threadTitleLabel.Size = new System.Drawing.Size(286, 12);
			this.threadTitleLabel.TabIndex = 3;
			this.threadTitleLabel.Text = "更新中...";
			this.threadTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.threadTitleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.threadTitleLabel_MouseDown);
			// 
			// wmp
			// 
			this.wmp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wmp.Enabled = true;
			this.wmp.Location = new System.Drawing.Point(0, 0);
			this.wmp.Name = "wmp";
			this.wmp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmp.OcxState")));
			this.wmp.Size = new System.Drawing.Size(284, 151);
			this.wmp.TabIndex = 0;
			// 
			// PlayerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(284, 214);
			this.Controls.Add(this.threadTitleLabel);
			this.Controls.Add(this.writeField);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.wmp);
			this.Name = "PlayerForm";
			this.Text = "PeerstPlayer";
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.wmp)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private AxWMPLib.AxWindowsMediaPlayer wmp;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel channelInfoLabel;
		private System.Windows.Forms.TextBox writeField;
		private System.Windows.Forms.Label threadTitleLabel;
	}
}

