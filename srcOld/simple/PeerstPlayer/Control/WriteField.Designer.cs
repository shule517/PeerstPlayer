namespace PeerstPlayer.Control
{
	partial class WriteField
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
			this.writeFieldTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// writeFieldTextBox
			// 
			this.writeFieldTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.writeFieldTextBox.Location = new System.Drawing.Point(0, 0);
			this.writeFieldTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.writeFieldTextBox.Multiline = true;
			this.writeFieldTextBox.Name = "writeFieldTextBox";
			this.writeFieldTextBox.Size = new System.Drawing.Size(480, 19);
			this.writeFieldTextBox.TabIndex = 0;
			this.writeFieldTextBox.WordWrap = false;
			this.writeFieldTextBox.TextChanged += new System.EventHandler(this.writeFieldTextBox_TextChanged);
			// 
			// WriteField
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.writeFieldTextBox);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "WriteField";
			this.Size = new System.Drawing.Size(480, 19);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox writeFieldTextBox;
	}
}
