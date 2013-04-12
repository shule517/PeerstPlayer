namespace PeerstPlayer
{
	partial class PlayerView
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.statusBar = new PeerstPlayer.Control.StatusBar();
			this.pecaPlayer = new PeerstPlayer.PecaPlayer();
			this.SuspendLayout();
			// 
			// statusBar
			// 
			this.statusBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.statusBar.BackColor = System.Drawing.Color.Black;
			this.statusBar.ChannelDetail = "チャンネル情報";
			this.statusBar.Location = new System.Drawing.Point(0, 360);
			this.statusBar.Margin = new System.Windows.Forms.Padding(0);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(480, 49);
			this.statusBar.TabIndex = 1;
			this.statusBar.ThreadTitle = "読み込み中...";
			this.statusBar.Volume = "100";
			this.statusBar.SizeChanged += new System.EventHandler(this.statusBar_SizeChanged);
			// 
			// pecaPlayer
			// 
			this.pecaPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pecaPlayer.Location = new System.Drawing.Point(0, 0);
			this.pecaPlayer.Margin = new System.Windows.Forms.Padding(0);
			this.pecaPlayer.Name = "pecaPlayer";
			this.pecaPlayer.Size = new System.Drawing.Size(480, 360);
			this.pecaPlayer.TabIndex = 0;
			this.pecaPlayer.Volume = 50;
			// 
			// PlayerView2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(480, 436);
			this.ControlBox = false;
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.pecaPlayer);
			this.MinimumSize = new System.Drawing.Size(100, 100);
			this.Name = "PlayerView2";
			this.ResumeLayout(false);

		}

		#endregion

		private PecaPlayer pecaPlayer;
		private Control.StatusBar statusBar;
	}
}