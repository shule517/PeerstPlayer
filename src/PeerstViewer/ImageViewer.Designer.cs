namespace PeerstViewer
{
	partial class ImageViewer
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewer));
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.アドレスをコピーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.画像を保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.labelProgress = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.pictureBox.ContextMenuStrip = this.contextMenuStrip;
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.InitialImage = global::PeerstViewer.Properties.Resources.loading;
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(116, 100);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
			this.pictureBox.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureBox_LoadCompleted);
			this.pictureBox.LoadProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.pictureBox_LoadProgressChanged);
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.アドレスをコピーToolStripMenuItem,
			this.画像を保存ToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(173, 48);
			// 
			// アドレスをコピーToolStripMenuItem
			// 
			this.アドレスをコピーToolStripMenuItem.Name = "アドレスをコピーToolStripMenuItem";
			this.アドレスをコピーToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.アドレスをコピーToolStripMenuItem.Text = "アドレスをコピー";
			this.アドレスをコピーToolStripMenuItem.Click += new System.EventHandler(this.アドレスをコピーToolStripMenuItem_Click);
			// 
			// 画像を保存ToolStripMenuItem
			// 
			this.画像を保存ToolStripMenuItem.Name = "画像を保存ToolStripMenuItem";
			this.画像を保存ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.画像を保存ToolStripMenuItem.Text = "画像を保存";
			this.画像を保存ToolStripMenuItem.Click += new System.EventHandler(this.画像を保存ToolStripMenuItem_Click);
			// 
			// labelProgress
			// 
			this.labelProgress.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.labelProgress.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelProgress.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelProgress.ForeColor = System.Drawing.Color.Yellow;
			this.labelProgress.Location = new System.Drawing.Point(0, 0);
			this.labelProgress.Name = "labelProgress";
			this.labelProgress.Size = new System.Drawing.Size(116, 12);
			this.labelProgress.TabIndex = 1;
			this.labelProgress.Text = "0%読み込みました";
			this.labelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ImageViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(116, 100);
			this.Controls.Add(this.labelProgress);
			this.Controls.Add(this.pictureBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ImageViewer";
			this.Text = "ImageViewer";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageViewer_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Label labelProgress;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem アドレスをコピーToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 画像を保存ToolStripMenuItem;
	}
}