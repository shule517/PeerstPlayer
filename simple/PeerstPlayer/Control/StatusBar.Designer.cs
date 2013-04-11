namespace PeerstPlayer.Control
{
	partial class StatusBar
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

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.threadTitleLabel = new System.Windows.Forms.Label();
			this.channelDetailLabel = new System.Windows.Forms.Label();
			this.writeFieldPanel = new System.Windows.Forms.Panel();
			this.channelDetailPanel = new System.Windows.Forms.Panel();
			this.volumeLabel = new System.Windows.Forms.Label();
			this.writeField = new PeerstPlayer.Control.WriteField();
			this.writeFieldPanel.SuspendLayout();
			this.channelDetailPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// threadTitleLabel
			// 
			this.threadTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.threadTitleLabel.BackColor = System.Drawing.Color.Black;
			this.threadTitleLabel.ForeColor = System.Drawing.Color.SpringGreen;
			this.threadTitleLabel.Location = new System.Drawing.Point(0, 0);
			this.threadTitleLabel.Margin = new System.Windows.Forms.Padding(0);
			this.threadTitleLabel.Name = "threadTitleLabel";
			this.threadTitleLabel.Size = new System.Drawing.Size(480, 12);
			this.threadTitleLabel.TabIndex = 1;
			this.threadTitleLabel.Text = "読み込み中...";
			this.threadTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// channelDetailLabel
			// 
			this.channelDetailLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.channelDetailLabel.BackColor = System.Drawing.Color.Black;
			this.channelDetailLabel.ForeColor = System.Drawing.Color.SpringGreen;
			this.channelDetailLabel.Location = new System.Drawing.Point(0, 0);
			this.channelDetailLabel.Margin = new System.Windows.Forms.Padding(0);
			this.channelDetailLabel.Name = "channelDetailLabel";
			this.channelDetailLabel.Padding = new System.Windows.Forms.Padding(3);
			this.channelDetailLabel.Size = new System.Drawing.Size(465, 18);
			this.channelDetailLabel.TabIndex = 1;
			this.channelDetailLabel.Text = "チャンネル情報";
			this.channelDetailLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.channelDetailLabel_MouseDown);
			// 
			// writeFieldPanel
			// 
			this.writeFieldPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.writeFieldPanel.Controls.Add(this.threadTitleLabel);
			this.writeFieldPanel.Controls.Add(this.writeField);
			this.writeFieldPanel.Location = new System.Drawing.Point(0, 0);
			this.writeFieldPanel.Margin = new System.Windows.Forms.Padding(0);
			this.writeFieldPanel.Name = "writeFieldPanel";
			this.writeFieldPanel.Size = new System.Drawing.Size(480, 31);
			this.writeFieldPanel.TabIndex = 3;
			// 
			// channelDetailPanel
			// 
			this.channelDetailPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.channelDetailPanel.Controls.Add(this.volumeLabel);
			this.channelDetailPanel.Controls.Add(this.channelDetailLabel);
			this.channelDetailPanel.Location = new System.Drawing.Point(0, 31);
			this.channelDetailPanel.Margin = new System.Windows.Forms.Padding(0);
			this.channelDetailPanel.Name = "channelDetailPanel";
			this.channelDetailPanel.Size = new System.Drawing.Size(480, 18);
			this.channelDetailPanel.TabIndex = 4;
			// 
			// volumeLabel
			// 
			this.volumeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.volumeLabel.BackColor = System.Drawing.Color.Black;
			this.volumeLabel.ForeColor = System.Drawing.Color.SpringGreen;
			this.volumeLabel.Location = new System.Drawing.Point(453, 0);
			this.volumeLabel.Margin = new System.Windows.Forms.Padding(0);
			this.volumeLabel.Name = "volumeLabel";
			this.volumeLabel.Padding = new System.Windows.Forms.Padding(3);
			this.volumeLabel.Size = new System.Drawing.Size(30, 18);
			this.volumeLabel.TabIndex = 2;
			this.volumeLabel.Text = "100";
			this.volumeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// writeField
			// 
			this.writeField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.writeField.AutoSize = true;
			this.writeField.Location = new System.Drawing.Point(0, 12);
			this.writeField.Margin = new System.Windows.Forms.Padding(0);
			this.writeField.Name = "writeField";
			this.writeField.Size = new System.Drawing.Size(480, 19);
			this.writeField.TabIndex = 0;
			// 
			// StatusBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.channelDetailPanel);
			this.Controls.Add(this.writeFieldPanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "StatusBar";
			this.Size = new System.Drawing.Size(480, 49);
			this.writeFieldPanel.ResumeLayout(false);
			this.writeFieldPanel.PerformLayout();
			this.channelDetailPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private WriteField writeField;
		private System.Windows.Forms.Label threadTitleLabel;
		private System.Windows.Forms.Label channelDetailLabel;
		private System.Windows.Forms.Panel writeFieldPanel;
		private System.Windows.Forms.Panel channelDetailPanel;
		private System.Windows.Forms.Label volumeLabel;
	}
}
