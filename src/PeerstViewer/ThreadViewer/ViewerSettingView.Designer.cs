namespace PeerstViewer.ThreadViewer
{
	partial class ViewerSettingView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewerSettingView));
			this.initGroupBox = new System.Windows.Forms.GroupBox();
			this.closeGroupBox = new System.Windows.Forms.GroupBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.returnSizeOnStartCheckBox = new System.Windows.Forms.CheckBox();
			this.returnPositionOnStartCheckBox = new System.Windows.Forms.CheckBox();
			this.saveReturnSizeCheckBox = new System.Windows.Forms.CheckBox();
			this.saveReturnPositionCheckBox = new System.Windows.Forms.CheckBox();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.initGroupBox.SuspendLayout();
			this.closeGroupBox.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// initGroupBox
			// 
			this.initGroupBox.Controls.Add(this.returnSizeOnStartCheckBox);
			this.initGroupBox.Controls.Add(this.returnPositionOnStartCheckBox);
			this.initGroupBox.Location = new System.Drawing.Point(8, 6);
			this.initGroupBox.Name = "initGroupBox";
			this.initGroupBox.Size = new System.Drawing.Size(210, 70);
			this.initGroupBox.TabIndex = 0;
			this.initGroupBox.TabStop = false;
			this.initGroupBox.Text = "初期表示";
			// 
			// closeGroupBox
			// 
			this.closeGroupBox.Controls.Add(this.saveReturnSizeCheckBox);
			this.closeGroupBox.Controls.Add(this.saveReturnPositionCheckBox);
			this.closeGroupBox.Location = new System.Drawing.Point(8, 82);
			this.closeGroupBox.Name = "closeGroupBox";
			this.closeGroupBox.Size = new System.Drawing.Size(210, 70);
			this.closeGroupBox.TabIndex = 1;
			this.closeGroupBox.TabStop = false;
			this.closeGroupBox.Text = "終了時";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(461, 362);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.closeGroupBox);
			this.tabPage1.Controls.Add(this.initGroupBox);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(453, 336);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// returnSizeOnStartCheckBox
			// 
			this.returnSizeOnStartCheckBox.AutoSize = true;
			this.returnSizeOnStartCheckBox.Location = new System.Drawing.Point(18, 40);
			this.returnSizeOnStartCheckBox.Name = "returnSizeOnStartCheckBox";
			this.returnSizeOnStartCheckBox.Size = new System.Drawing.Size(148, 16);
			this.returnSizeOnStartCheckBox.TabIndex = 2;
			this.returnSizeOnStartCheckBox.Text = "ウィンドウサイズを復帰する";
			this.returnSizeOnStartCheckBox.UseVisualStyleBackColor = true;
			// 
			// returnPositionOnStartCheckBox
			// 
			this.returnPositionOnStartCheckBox.AutoSize = true;
			this.returnPositionOnStartCheckBox.Location = new System.Drawing.Point(18, 18);
			this.returnPositionOnStartCheckBox.Name = "returnPositionOnStartCheckBox";
			this.returnPositionOnStartCheckBox.Size = new System.Drawing.Size(143, 16);
			this.returnPositionOnStartCheckBox.TabIndex = 3;
			this.returnPositionOnStartCheckBox.Text = "ウィンドウ位置を復帰する";
			this.returnPositionOnStartCheckBox.UseVisualStyleBackColor = true;
			// 
			// saveReturnSizeCheckBox
			// 
			this.saveReturnSizeCheckBox.AutoSize = true;
			this.saveReturnSizeCheckBox.Location = new System.Drawing.Point(18, 40);
			this.saveReturnSizeCheckBox.Name = "saveReturnSizeCheckBox";
			this.saveReturnSizeCheckBox.Size = new System.Drawing.Size(148, 16);
			this.saveReturnSizeCheckBox.TabIndex = 2;
			this.saveReturnSizeCheckBox.Text = "ウィンドウサイズを保存する";
			this.saveReturnSizeCheckBox.UseVisualStyleBackColor = true;
			// 
			// saveReturnPositionCheckBox
			// 
			this.saveReturnPositionCheckBox.AutoSize = true;
			this.saveReturnPositionCheckBox.Location = new System.Drawing.Point(18, 18);
			this.saveReturnPositionCheckBox.Name = "saveReturnPositionCheckBox";
			this.saveReturnPositionCheckBox.Size = new System.Drawing.Size(143, 16);
			this.saveReturnPositionCheckBox.TabIndex = 3;
			this.saveReturnPositionCheckBox.Text = "ウィンドウ位置を保存する";
			this.saveReturnPositionCheckBox.UseVisualStyleBackColor = true;
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(290, 368);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 3;
			this.saveButton.Text = "保存";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(371, 368);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// ViewerSettingView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(461, 397);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ViewerSettingView";
			this.Text = "設定";
			this.initGroupBox.ResumeLayout(false);
			this.initGroupBox.PerformLayout();
			this.closeGroupBox.ResumeLayout(false);
			this.closeGroupBox.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox initGroupBox;
		private System.Windows.Forms.GroupBox closeGroupBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.CheckBox returnSizeOnStartCheckBox;
		private System.Windows.Forms.CheckBox returnPositionOnStartCheckBox;
		private System.Windows.Forms.CheckBox saveReturnSizeCheckBox;
		private System.Windows.Forms.CheckBox saveReturnPositionCheckBox;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
	}
}