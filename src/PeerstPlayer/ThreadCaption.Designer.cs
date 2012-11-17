using PeerstPlayer;
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
			this.timerResUpdate = new System.Windows.Forms.Timer(this.components);
			this.toolStrip = new PeerstPlayer.ToolStripEx();
			this.toolStripButtonUpdate = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonVisible = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
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
			// timerResUpdate
			// 
			this.timerResUpdate.Enabled = true;
			this.timerResUpdate.Interval = 10000;
			this.timerResUpdate.Tick += new System.EventHandler(this.timerResUpdate_Tick);
			// 
			// toolStrip
			// 
			this.toolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.EnableClickThrough = true;
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonUpdate,
            this.toolStripButtonVisible,
            this.toolStripButtonClose});
			this.toolStrip.Location = new System.Drawing.Point(203, 9);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(72, 25);
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
			// toolStripButtonVisible
			// 
			this.toolStripButtonVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonVisible.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonVisible.Image")));
			this.toolStripButtonVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonVisible.Name = "toolStripButtonVisible";
			this.toolStripButtonVisible.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonVisible.Text = "表示切り替え";
			this.toolStripButtonVisible.Click += new System.EventHandler(this.toolStripButtonVisible_Click);
			// 
			// toolStripButtonClose
			// 
			this.toolStripButtonClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonClose.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClose.Image")));
			this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonClose.Name = "toolStripButtonClose";
			this.toolStripButtonClose.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonClose.Text = "表示切り替え";
			this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
			// 
			// ThreadCaption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.ContextMenuStrip = this.contextMenuStrip;
			this.Controls.Add(this.toolStrip);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ThreadCaption";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "PeerstCaption";
			this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PeerstCaption_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PeerstCaption_MouseDown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ThreadCaption_FormClosing);
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
		private ToolStripEx toolStrip;
		private System.Windows.Forms.ToolStripButton toolStripButtonUpdate;
		private System.Windows.Forms.ToolStripButton toolStripButtonClose;
		private System.Windows.Forms.ToolStripButton toolStripButtonVisible;
		private System.Windows.Forms.Timer timerResUpdate;
	}
}

