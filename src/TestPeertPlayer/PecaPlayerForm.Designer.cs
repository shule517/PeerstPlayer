using PeerstPlayer.Controls.PecaPlayer;
namespace TestPeertPlayer
{
	partial class PecaPlayerForm
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
			this.pecaPlayer = new PecaPlayerControl();
			this.SuspendLayout();
			// 
			// pecaPlayer
			// 
			this.pecaPlayer.Location = new System.Drawing.Point(0, 0);
			this.pecaPlayer.Name = "pecaPlayer";
			this.pecaPlayer.Size = new System.Drawing.Size(150, 150);
			this.pecaPlayer.TabIndex = 1;
			// 
			// PecaPlayerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.pecaPlayer);
			this.Name = "PecaPlayerForm";
			this.Text = "PecaPlayerForm";
			this.ResumeLayout(false);

		}

		#endregion

		public PecaPlayerControl pecaPlayer;
	}
}