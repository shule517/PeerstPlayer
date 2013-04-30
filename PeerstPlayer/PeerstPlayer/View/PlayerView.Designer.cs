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
			this.pecaPlayer = new PeerstPlayer.Control.PecaPlayer();
			this.SuspendLayout();
			// 
			// pecaPlayer
			// 
			this.pecaPlayer.Location = new System.Drawing.Point(3, 2);
			this.pecaPlayer.Name = "pecaPlayer";
			this.pecaPlayer.Size = new System.Drawing.Size(150, 150);
			this.pecaPlayer.TabIndex = 0;
			// 
			// PlayerView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.pecaPlayer);
			this.Name = "PlayerView";
			this.Text = "PeestPlayer";
			this.ResumeLayout(false);

		}

		#endregion

		private Control.PecaPlayer pecaPlayer;
	}
}

