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
			this.ChannelDetailLabel = new System.Windows.Forms.Label();
			this.writeField = new PeerstPlayer.Control.WriteField();
			this.SuspendLayout();
			// 
			// ChannelDetailLabel
			// 
			this.ChannelDetailLabel.BackColor = System.Drawing.Color.Black;
			this.ChannelDetailLabel.ForeColor = System.Drawing.Color.SpringGreen;
			this.ChannelDetailLabel.Location = new System.Drawing.Point(0, 31);
			this.ChannelDetailLabel.Name = "ChannelDetailLabel";
			this.ChannelDetailLabel.Size = new System.Drawing.Size(480, 12);
			this.ChannelDetailLabel.TabIndex = 1;
			this.ChannelDetailLabel.Text = "チャンネル情報";
			// 
			// writeField
			// 
			this.writeField.Location = new System.Drawing.Point(0, 0);
			this.writeField.Margin = new System.Windows.Forms.Padding(0);
			this.writeField.Name = "writeField";
			this.writeField.Size = new System.Drawing.Size(480, 31);
			this.writeField.TabIndex = 0;
			// 
			// StatusBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ChannelDetailLabel);
			this.Controls.Add(this.writeField);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "StatusBar";
			this.Size = new System.Drawing.Size(480, 43);
			this.ResumeLayout(false);

		}

		#endregion

		private WriteField writeField;
		private System.Windows.Forms.Label ChannelDetailLabel;
	}
}
