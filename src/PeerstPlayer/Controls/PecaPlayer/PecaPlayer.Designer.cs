namespace PeerstPlayer.Controls.PecaPlayer
{
    partial class PecaPlayerControl
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PecaPlayerControl));
			this.wmp = new AxWMPLib.AxWindowsMediaPlayer();
			((System.ComponentModel.ISupportInitialize)(this.wmp)).BeginInit();
			this.SuspendLayout();
			// 
			// wmp
			// 
			this.wmp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wmp.Enabled = true;
			this.wmp.Location = new System.Drawing.Point(0, 0);
			this.wmp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.wmp.Name = "wmp";
			this.wmp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmp.OcxState")));
			this.wmp.Size = new System.Drawing.Size(200, 188);
			this.wmp.TabIndex = 0;
			// 
			// PecaPlayerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.wmp);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "PecaPlayerControl";
			this.Size = new System.Drawing.Size(200, 188);
			((System.ComponentModel.ISupportInitialize)(this.wmp)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private AxWMPLib.AxWindowsMediaPlayer wmp;
    }
}
