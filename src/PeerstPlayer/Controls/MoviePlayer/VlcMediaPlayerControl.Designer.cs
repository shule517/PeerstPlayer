namespace PeerstPlayer.Controls.MoviePlayer
{
	partial class VlcMediaPlayerControl
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
			this.vlcControl = new LibVlcWrapper.VlcControl();
			this.SuspendLayout();
			// 
			// vlcControl
			// 
			this.vlcControl.BackColor = System.Drawing.Color.Black;
			this.vlcControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.vlcControl.Location = new System.Drawing.Point(0, 0);
			this.vlcControl.Mute = false;
			this.vlcControl.Name = "vlcControl";
			this.vlcControl.Size = new System.Drawing.Size(150, 150);
			this.vlcControl.TabIndex = 0;
			this.vlcControl.Text = "vlcControl";
			this.vlcControl.Time = System.TimeSpan.Parse("00:00:00");
			this.vlcControl.Volume = -1;
// TODO: 例外 '無効な Primitive 型 System.IntPtr です。CodeObjectCreateExpression を使ってください。' によって '			this.vlcControl.WindowHandle' のコード生成が失敗しました。
			// 
			// VlcMediaPlayerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.vlcControl);
			this.Name = "VlcMediaPlayerControl";
			this.Load += new System.EventHandler(this.VlcMediaPlayerControl_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private LibVlcWrapper.VlcControl vlcControl;
	}
}
