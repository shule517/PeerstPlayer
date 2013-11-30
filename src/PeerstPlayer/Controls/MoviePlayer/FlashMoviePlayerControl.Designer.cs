namespace PeerstPlayer.Controls.MoviePlayer
{
	partial class FlashMoviePlayerControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlashMoviePlayerControl));
			this.axShockwaveFlash = new AxShockwaveFlashObjects.AxShockwaveFlash();
			((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).BeginInit();
			this.SuspendLayout();
			// 
			// axShockwaveFlash
			// 
			this.axShockwaveFlash.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axShockwaveFlash.Enabled = true;
			this.axShockwaveFlash.Location = new System.Drawing.Point(0, 0);
			this.axShockwaveFlash.Name = "axShockwaveFlash";
			this.axShockwaveFlash.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash.OcxState")));
			this.axShockwaveFlash.Size = new System.Drawing.Size(150, 150);
			this.axShockwaveFlash.TabIndex = 0;
			// 
			// FlashMoviePlayerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.axShockwaveFlash);
			this.Name = "FlashMoviePlayerControl";
			((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash;
	}
}
