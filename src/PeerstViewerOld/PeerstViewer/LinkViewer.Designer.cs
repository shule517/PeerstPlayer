namespace PeerstViewer
{
	partial class LinkViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkViewer));
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			this.textBoxURL = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// webBrowser
			// 
			this.webBrowser.Location = new System.Drawing.Point(0, 25);
			this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.ScriptErrorsSuppressed = true;
			this.webBrowser.Size = new System.Drawing.Size(584, 543);
			this.webBrowser.TabIndex = 0;
			this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
			// 
			// textBoxURL
			// 
			this.textBoxURL.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBoxURL.Location = new System.Drawing.Point(0, 0);
			this.textBoxURL.Name = "textBoxURL";
			this.textBoxURL.Size = new System.Drawing.Size(584, 19);
			this.textBoxURL.TabIndex = 1;
			this.textBoxURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxURL_KeyDown);
			this.textBoxURL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBoxURL_MouseDown);
			// 
			// LinkViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 562);
			this.Controls.Add(this.textBoxURL);
			this.Controls.Add(this.webBrowser);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LinkViewer";
			this.Text = "LinkViewer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LinkViewer_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.WebBrowser webBrowser;
		private System.Windows.Forms.TextBox textBoxURL;
	}
}