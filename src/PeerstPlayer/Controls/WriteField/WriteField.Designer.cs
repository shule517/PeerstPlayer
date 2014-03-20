namespace PeerstPlayer.Controls.WriteField
{
	partial class WriteFieldControl
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WriteFieldControl));
			this.selectThreadLabel = new System.Windows.Forms.Label();
			this.writeButton = new System.Windows.Forms.Button();
			this.newResToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.writeFieldTextBox = new Extentions.WatermarkTextBox();
			this.SuspendLayout();
			// 
			// selectThreadLabel
			// 
			this.selectThreadLabel.BackColor = System.Drawing.Color.Black;
			this.selectThreadLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.selectThreadLabel.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.selectThreadLabel.ForeColor = System.Drawing.Color.SpringGreen;
			this.selectThreadLabel.Location = new System.Drawing.Point(0, 0);
			this.selectThreadLabel.Margin = new System.Windows.Forms.Padding(0);
			this.selectThreadLabel.Name = "selectThreadLabel";
			this.selectThreadLabel.Size = new System.Drawing.Size(480, 16);
			this.selectThreadLabel.TabIndex = 2;
			this.selectThreadLabel.Text = "読み込み中...";
			this.selectThreadLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// writeButton
			// 
			this.writeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.writeButton.AutoSize = true;
			this.writeButton.Image = ((System.Drawing.Image)(resources.GetObject("writeButton.Image")));
			this.writeButton.Location = new System.Drawing.Point(450, 14);
			this.writeButton.Margin = new System.Windows.Forms.Padding(0);
			this.writeButton.Name = "writeButton";
			this.writeButton.Size = new System.Drawing.Size(30, 23);
			this.writeButton.TabIndex = 3;
			this.writeButton.UseVisualStyleBackColor = true;
			// 
			// newResToolTip
			// 
			this.newResToolTip.AutoPopDelay = 50000;
			this.newResToolTip.InitialDelay = 500;
			this.newResToolTip.IsBalloon = true;
			this.newResToolTip.ReshowDelay = 100;
			this.newResToolTip.ShowAlways = true;
			this.newResToolTip.ToolTipTitle = "新着レス";
			// 
			// writeFieldTextBox
			// 
			this.writeFieldTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.writeFieldTextBox.ForeColor = System.Drawing.Color.DarkGray;
			this.writeFieldTextBox.Location = new System.Drawing.Point(0, 16);
			this.writeFieldTextBox.Name = "writeFieldTextBox";
			this.writeFieldTextBox.Size = new System.Drawing.Size(450, 19);
			this.writeFieldTextBox.TabIndex = 4;
			this.writeFieldTextBox.WatermarkText = "書き込み内容を入力してください";
			// 
			// WriteFieldControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.writeFieldTextBox);
			this.Controls.Add(this.writeButton);
			this.Controls.Add(this.selectThreadLabel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "WriteFieldControl";
			this.Size = new System.Drawing.Size(480, 35);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label selectThreadLabel;
		private System.Windows.Forms.Button writeButton;
		private System.Windows.Forms.ToolTip newResToolTip;
		private Extentions.WatermarkTextBox writeFieldTextBox;
	}
}
