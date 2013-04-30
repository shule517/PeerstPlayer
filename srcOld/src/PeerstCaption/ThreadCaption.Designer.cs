namespace PeerstCaption
{
	partial class ThreadCaption
	{
		/// <summary>
		/// 必要なデザイナ変数です。
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

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThreadCaption));
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timerMouseCheck = new System.Windows.Forms.Timer(this.components);
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonUpdate = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
			this.timerResUpdate = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.更新ToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(101, 26);
			// 
			// 更新ToolStripMenuItem
			// 
			this.更新ToolStripMenuItem.Name = "更新ToolStripMenuItem";
			this.更新ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.更新ToolStripMenuItem.Text = "更新";
			this.更新ToolStripMenuItem.Click += new System.EventHandler(this.更新ToolStripMenuItem_Click);
			// 
			// timerMouseCheck
			// 
			this.timerMouseCheck.Enabled = true;
			this.timerMouseCheck.Tick += new System.EventHandler(this.timerMouseCheck_Tick);
			// 
			// toolStrip
			// 
			this.toolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonUpdate,
            this.toolStripButtonClose});
			this.toolStrip.Location = new System.Drawing.Point(242, 9);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(49, 25);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Visible = false;
			// 
			// toolStripButtonUpdate
			// 
			this.toolStripButtonUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonUpdate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUpdate.Image")));
			this.toolStripButtonUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonUpdate.Name = "toolStripButtonUpdate";
			this.toolStripButtonUpdate.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonUpdate.Text = "更新";
			this.toolStripButtonUpdate.Click += new System.EventHandler(this.toolStripButtonUpdate_Click);
			// 
			// toolStripButtonClose
			// 
			this.toolStripButtonClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonClose.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClose.Image")));
			this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonClose.Name = "toolStripButtonClose";
			this.toolStripButtonClose.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonClose.Text = "終了";
			this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
			// 
			// timerResUpdate
			// 
			this.timerResUpdate.Enabled = true;
			this.timerResUpdate.Interval = 10000;
			this.timerResUpdate.Tick += new System.EventHandler(this.timerResUpdate_Tick);
			// 
			// ThreadCaption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(300, 210);
			this.ContextMenuStrip = this.contextMenuStrip;
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ThreadCaption";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "PeerstCaption";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PeerstCaption_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PeerstCaption_MouseDown);
			this.Resize += new System.EventHandler(this.PeerstCaption_Resize);
			this.contextMenuStrip.ResumeLayout(false);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem 更新ToolStripMenuItem;
		private System.Windows.Forms.Timer timerMouseCheck;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton toolStripButtonUpdate;
		private System.Windows.Forms.ToolStripButton toolStripButtonClose;
		private System.Windows.Forms.Timer timerResUpdate;
	}
}

