namespace PeerstPlayer.Control
{
	partial class MovieDetail
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
			this.ChannelDetailLabel = new System.Windows.Forms.Label();
			this.volumeLabel = new System.Windows.Forms.Label();
			this.movieStatusLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ChannelDetailLabel
			// 
			this.ChannelDetailLabel.AutoSize = true;
			this.ChannelDetailLabel.BackColor = System.Drawing.Color.Black;
			this.ChannelDetailLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this.ChannelDetailLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F);
			this.ChannelDetailLabel.ForeColor = System.Drawing.Color.SpringGreen;
			this.ChannelDetailLabel.Location = new System.Drawing.Point(0, 0);
			this.ChannelDetailLabel.Margin = new System.Windows.Forms.Padding(3);
			this.ChannelDetailLabel.Name = "ChannelDetailLabel";
			this.ChannelDetailLabel.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.ChannelDetailLabel.Size = new System.Drawing.Size(78, 15);
			this.ChannelDetailLabel.TabIndex = 2;
			this.ChannelDetailLabel.Text = "チャンネル情報";
			this.ChannelDetailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// volumeLabel
			// 
			this.volumeLabel.BackColor = System.Drawing.Color.Black;
			this.volumeLabel.Dock = System.Windows.Forms.DockStyle.Right;
			this.volumeLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F);
			this.volumeLabel.ForeColor = System.Drawing.Color.SpringGreen;
			this.volumeLabel.Location = new System.Drawing.Point(450, 0);
			this.volumeLabel.Margin = new System.Windows.Forms.Padding(3);
			this.volumeLabel.Name = "volumeLabel";
			this.volumeLabel.Size = new System.Drawing.Size(30, 18);
			this.volumeLabel.TabIndex = 3;
			this.volumeLabel.Text = "50";
			this.volumeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// movieStatusLabel
			// 
			this.movieStatusLabel.AutoSize = true;
			this.movieStatusLabel.BackColor = System.Drawing.Color.Black;
			this.movieStatusLabel.Dock = System.Windows.Forms.DockStyle.Right;
			this.movieStatusLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F);
			this.movieStatusLabel.ForeColor = System.Drawing.Color.SpringGreen;
			this.movieStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.movieStatusLabel.Location = new System.Drawing.Point(402, 0);
			this.movieStatusLabel.Margin = new System.Windows.Forms.Padding(3);
			this.movieStatusLabel.Name = "movieStatusLabel";
			this.movieStatusLabel.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.movieStatusLabel.Size = new System.Drawing.Size(48, 15);
			this.movieStatusLabel.TabIndex = 4;
			this.movieStatusLabel.Text = "00:00:00";
			this.movieStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// MovieDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.movieStatusLabel);
			this.Controls.Add(this.volumeLabel);
			this.Controls.Add(this.ChannelDetailLabel);
			this.Name = "MovieDetail";
			this.Size = new System.Drawing.Size(480, 18);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label volumeLabel;
		private System.Windows.Forms.Label movieStatusLabel;
		private System.Windows.Forms.Label ChannelDetailLabel;
	}
}
