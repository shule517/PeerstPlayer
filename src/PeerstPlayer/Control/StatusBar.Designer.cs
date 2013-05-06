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
			this.writeField = new PeerstPlayer.Control.WriteField();
			this.movieDetail = new PeerstPlayer.Control.MovieDetail();
			this.SuspendLayout();
			// 
			// writeField
			// 
			this.writeField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.writeField.Location = new System.Drawing.Point(0, 0);
			this.writeField.Margin = new System.Windows.Forms.Padding(0);
			this.writeField.Name = "writeField";
			this.writeField.SelectThreadUrl = "読み込み中...";
			this.writeField.Size = new System.Drawing.Size(480, 31);
			this.writeField.TabIndex = 0;
			// 
			// movieDetail
			// 
			this.movieDetail.ChannelDetail = "チャンネル情報";
			this.movieDetail.Location = new System.Drawing.Point(0, 29);
			this.movieDetail.MovieStatus = "00:00:00";
			this.movieDetail.Name = "movieDetail";
			this.movieDetail.Size = new System.Drawing.Size(480, 18);
			this.movieDetail.TabIndex = 1;
			this.movieDetail.Volume = "100";
			// 
			// StatusBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.movieDetail);
			this.Controls.Add(this.writeField);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "StatusBar";
			this.Size = new System.Drawing.Size(480, 47);
			this.ResumeLayout(false);

		}

		#endregion

		private WriteField writeField;
		private MovieDetail movieDetail;
	}
}
