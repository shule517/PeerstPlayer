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
			this.statusBar = new PeerstPlayer.Control.StatusBar();
			this.pecaPlayer = new PeerstPlayer.Control.PecaPlayer();
			this.SuspendLayout();
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 360);
			this.statusBar.Margin = new System.Windows.Forms.Padding(0);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(480, 43);
			this.statusBar.TabIndex = 1;
			// 
			// pecaPlayer
			// 
			this.pecaPlayer.Location = new System.Drawing.Point(0, 0);
			this.pecaPlayer.Margin = new System.Windows.Forms.Padding(0);
			this.pecaPlayer.Name = "pecaPlayer";
			this.pecaPlayer.Size = new System.Drawing.Size(480, 360);
			this.pecaPlayer.TabIndex = 0;
			// 
			// PlayerView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(514, 449);
			this.ControlBox = false;
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.pecaPlayer);
			this.Name = "PlayerView";
			this.ResumeLayout(false);

		}

		#endregion

		private Control.PecaPlayer pecaPlayer;
		private Control.StatusBar statusBar;
	}
}

