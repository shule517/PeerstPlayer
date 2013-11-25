using PeerstPlayer.Controls.MovieDetail;
using PeerstPlayer.Controls.WriteField;
namespace PeerstPlayer.Controls.StatusBar
{
	partial class StatusBarControl
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
			this.movieDetail = new PeerstPlayer.Controls.MovieDetail.MovieDetailControl();
			this.writeField = new PeerstPlayer.Controls.WriteField.WriteFieldControl();
			this.SuspendLayout();
			// 
			// movieDetail
			// 
			this.movieDetail.BackColor = System.Drawing.Color.Black;
			this.movieDetail.ChannelDetail = "チャンネル情報";
			this.movieDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.movieDetail.Location = new System.Drawing.Point(0, 37);
			this.movieDetail.Margin = new System.Windows.Forms.Padding(5);
			this.movieDetail.MovieStatus = "";
			this.movieDetail.Name = "movieDetail";
			this.movieDetail.Size = new System.Drawing.Size(640, 22);
			this.movieDetail.TabIndex = 1;
			this.movieDetail.Volume = "50";
			// 
			// writeField
			// 
			this.writeField.Dock = System.Windows.Forms.DockStyle.Top;
			this.writeField.Location = new System.Drawing.Point(0, 0);
			this.writeField.Margin = new System.Windows.Forms.Padding(0);
			this.writeField.Name = "writeField";
			this.writeField.SelectThreadUrl = null;
			this.writeField.Size = new System.Drawing.Size(640, 39);
			this.writeField.TabIndex = 0;
			// 
			// StatusBarControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.movieDetail);
			this.Controls.Add(this.writeField);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "StatusBarControl";
			this.Size = new System.Drawing.Size(640, 59);
			this.ResumeLayout(false);

		}

		#endregion

		private MovieDetailControl movieDetail;
		private WriteFieldControl writeField;
	}
}
