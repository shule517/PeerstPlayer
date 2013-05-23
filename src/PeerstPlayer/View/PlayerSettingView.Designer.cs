namespace PeerstPlayer.View
{
	partial class PlayerSettingView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerSettingView));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.settingPage = new System.Windows.Forms.TabPage();
			this.windowGroupBox = new System.Windows.Forms.GroupBox();
			this.volumeGroupBox = new System.Windows.Forms.GroupBox();
			this.initGroupBox = new System.Windows.Forms.GroupBox();
			this.closeGroupBox = new System.Windows.Forms.GroupBox();
			this.statusBarGroupBox = new System.Windows.Forms.GroupBox();
			this.shortcutPage = new System.Windows.Forms.TabPage();
			this.mouseGesturePage = new System.Windows.Forms.TabPage();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.settingPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.settingPage);
			this.tabControl.Controls.Add(this.shortcutPage);
			this.tabControl.Controls.Add(this.mouseGesturePage);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(519, 333);
			this.tabControl.TabIndex = 0;
			// 
			// settingPage
			// 
			this.settingPage.Controls.Add(this.windowGroupBox);
			this.settingPage.Controls.Add(this.volumeGroupBox);
			this.settingPage.Controls.Add(this.initGroupBox);
			this.settingPage.Controls.Add(this.closeGroupBox);
			this.settingPage.Controls.Add(this.statusBarGroupBox);
			this.settingPage.Location = new System.Drawing.Point(4, 22);
			this.settingPage.Name = "settingPage";
			this.settingPage.Padding = new System.Windows.Forms.Padding(3);
			this.settingPage.Size = new System.Drawing.Size(511, 307);
			this.settingPage.TabIndex = 0;
			this.settingPage.Text = "各種設定";
			this.settingPage.UseVisualStyleBackColor = true;
			// 
			// windowGroupBox
			// 
			this.windowGroupBox.Location = new System.Drawing.Point(256, 228);
			this.windowGroupBox.Name = "windowGroupBox";
			this.windowGroupBox.Size = new System.Drawing.Size(242, 68);
			this.windowGroupBox.TabIndex = 4;
			this.windowGroupBox.TabStop = false;
			this.windowGroupBox.Text = "ウィンドウ";
			// 
			// volumeGroupBox
			// 
			this.volumeGroupBox.Location = new System.Drawing.Point(256, 154);
			this.volumeGroupBox.Name = "volumeGroupBox";
			this.volumeGroupBox.Size = new System.Drawing.Size(242, 68);
			this.volumeGroupBox.TabIndex = 3;
			this.volumeGroupBox.TabStop = false;
			this.volumeGroupBox.Text = "音量";
			// 
			// initGroupBox
			// 
			this.initGroupBox.Location = new System.Drawing.Point(256, 80);
			this.initGroupBox.Name = "initGroupBox";
			this.initGroupBox.Size = new System.Drawing.Size(242, 68);
			this.initGroupBox.TabIndex = 2;
			this.initGroupBox.TabStop = false;
			this.initGroupBox.Text = "初期表示";
			// 
			// closeGroupBox
			// 
			this.closeGroupBox.Location = new System.Drawing.Point(256, 6);
			this.closeGroupBox.Name = "closeGroupBox";
			this.closeGroupBox.Size = new System.Drawing.Size(242, 68);
			this.closeGroupBox.TabIndex = 1;
			this.closeGroupBox.TabStop = false;
			this.closeGroupBox.Text = "終了時";
			// 
			// statusBarGroupBox
			// 
			this.statusBarGroupBox.Location = new System.Drawing.Point(8, 6);
			this.statusBarGroupBox.Name = "statusBarGroupBox";
			this.statusBarGroupBox.Size = new System.Drawing.Size(242, 290);
			this.statusBarGroupBox.TabIndex = 0;
			this.statusBarGroupBox.TabStop = false;
			this.statusBarGroupBox.Text = "ステータスバー";
			// 
			// shortcutPage
			// 
			this.shortcutPage.Location = new System.Drawing.Point(4, 22);
			this.shortcutPage.Name = "shortcutPage";
			this.shortcutPage.Padding = new System.Windows.Forms.Padding(3);
			this.shortcutPage.Size = new System.Drawing.Size(511, 307);
			this.shortcutPage.TabIndex = 1;
			this.shortcutPage.Text = "ショートカット";
			this.shortcutPage.UseVisualStyleBackColor = true;
			// 
			// mouseGesturePage
			// 
			this.mouseGesturePage.Location = new System.Drawing.Point(4, 22);
			this.mouseGesturePage.Name = "mouseGesturePage";
			this.mouseGesturePage.Size = new System.Drawing.Size(511, 307);
			this.mouseGesturePage.TabIndex = 2;
			this.mouseGesturePage.Text = "マウスジェスチャ";
			this.mouseGesturePage.UseVisualStyleBackColor = true;
			// 
			// saveButton
			// 
			this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.saveButton.Location = new System.Drawing.Point(351, 339);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 1;
			this.saveButton.Text = "保存";
			this.saveButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(432, 339);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// PlayerSettingView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(519, 374);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.tabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PlayerSettingView";
			this.Text = "設定";
			this.tabControl.ResumeLayout(false);
			this.settingPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage settingPage;
		private System.Windows.Forms.TabPage shortcutPage;
		private System.Windows.Forms.TabPage mouseGesturePage;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.GroupBox windowGroupBox;
		private System.Windows.Forms.GroupBox volumeGroupBox;
		private System.Windows.Forms.GroupBox initGroupBox;
		private System.Windows.Forms.GroupBox closeGroupBox;
		private System.Windows.Forms.GroupBox statusBarGroupBox;

	}
}