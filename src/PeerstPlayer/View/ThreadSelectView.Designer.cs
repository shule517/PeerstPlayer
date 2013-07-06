namespace PeerstPlayer.View
{
	partial class ThreadSelectView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThreadSelectView));
			this.checkBox = new System.Windows.Forms.CheckBox();
			this.urlTextBox = new System.Windows.Forms.TextBox();
			this.updateButton = new System.Windows.Forms.Button();
			this.threadListView = new PeerstLib.Control.BufferedListView();
			this.columnHeaderNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.threadTitleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.resNumColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.resSpeedColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.sinecColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// checkBox
			// 
			this.checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBox.AutoSize = true;
			this.checkBox.Location = new System.Drawing.Point(5, 268);
			this.checkBox.Name = "checkBox";
			this.checkBox.Size = new System.Drawing.Size(142, 16);
			this.checkBox.TabIndex = 1;
			this.checkBox.Text = "ストップしたスレッドを表示";
			this.checkBox.UseVisualStyleBackColor = true;
			this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
			// 
			// urlTextBox
			// 
			this.urlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.urlTextBox.Location = new System.Drawing.Point(0, 2);
			this.urlTextBox.Name = "urlTextBox";
			this.urlTextBox.Size = new System.Drawing.Size(441, 19);
			this.urlTextBox.TabIndex = 2;
			this.urlTextBox.Text = "http://";
			this.urlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.urlTextBox_KeyDown);
			// 
			// updateButton
			// 
			this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.updateButton.Location = new System.Drawing.Point(440, 0);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(75, 23);
			this.updateButton.TabIndex = 3;
			this.updateButton.Text = "更新";
			this.updateButton.UseVisualStyleBackColor = true;
			this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// threadListView
			// 
			this.threadListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.threadListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderNo,
            this.threadTitleColumnHeader,
            this.resNumColumnHeader,
            this.resSpeedColumnHeader,
            this.sinecColumnHeader});
			this.threadListView.FullRowSelect = true;
			this.threadListView.GridLines = true;
			this.threadListView.Location = new System.Drawing.Point(0, 21);
			this.threadListView.MultiSelect = false;
			this.threadListView.Name = "threadListView";
			this.threadListView.Size = new System.Drawing.Size(515, 242);
			this.threadListView.TabIndex = 0;
			this.threadListView.UseCompatibleStateImageBehavior = false;
			this.threadListView.View = System.Windows.Forms.View.Details;
			this.threadListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.threadListView_KeyDown);
			this.threadListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.threadListView_MouseClick);
			// 
			// columnHeaderNo
			// 
			this.columnHeaderNo.Text = "No";
			this.columnHeaderNo.Width = 34;
			// 
			// threadTitleColumnHeader
			// 
			this.threadTitleColumnHeader.Text = "スレッドタイトル";
			this.threadTitleColumnHeader.Width = 292;
			// 
			// resNumColumnHeader
			// 
			this.resNumColumnHeader.Text = "レス数";
			this.resNumColumnHeader.Width = 43;
			// 
			// resSpeedColumnHeader
			// 
			this.resSpeedColumnHeader.Text = "勢い";
			this.resSpeedColumnHeader.Width = 44;
			// 
			// sinecColumnHeader
			// 
			this.sinecColumnHeader.Text = "Since";
			this.sinecColumnHeader.Width = 78;
			// 
			// ThreadSelectView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(514, 286);
			this.Controls.Add(this.updateButton);
			this.Controls.Add(this.urlTextBox);
			this.Controls.Add(this.checkBox);
			this.Controls.Add(this.threadListView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ThreadSelectView";
			this.Text = "スレッド選択画面";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private PeerstLib.Control.BufferedListView threadListView;
		private System.Windows.Forms.CheckBox checkBox;
		private System.Windows.Forms.TextBox urlTextBox;
		private System.Windows.Forms.Button updateButton;
		private System.Windows.Forms.ColumnHeader columnHeaderNo;
		private System.Windows.Forms.ColumnHeader threadTitleColumnHeader;
		private System.Windows.Forms.ColumnHeader resNumColumnHeader;
		private System.Windows.Forms.ColumnHeader resSpeedColumnHeader;
		private System.Windows.Forms.ColumnHeader sinecColumnHeader;

	}
}