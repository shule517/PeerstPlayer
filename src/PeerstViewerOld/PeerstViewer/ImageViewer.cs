using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PeerstViewer
{
	public partial class ImageViewer : Form
	{
		/// <summary>
		/// アドレス
		/// </summary>
		string url = "";

		public ImageViewer()
		{
			InitializeComponent();
		}

		public void Show(string url)
		{
			if (Visible)
				return;

			Show();
			Visible = true;
			IsMax = false;
			Focus();
			pictureBox.Image = pictureBox.InitialImage;
			Application.DoEvents();
			this.url = url;

			pictureBox.WaitOnLoad = false;
			pictureBox.LoadAsync(url);
		}

		/// <summary>
		/// マウスリリース
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseLeave(object sender, EventArgs e)
		{
			// マウスリリース時に画像ビューワを非表示にする
			if (!IsMax && !contextMenuStrip.Visible)
			{
				Visible = false;
			}
		}

		bool isMax = false;
		bool IsMax
		{
			get
			{
				return isMax;
			}
			set
			{
				isMax = value;
				if (value)
				{
					// サイズ変更
					ClientSize = pictureBox.Image.Size;
					if (Width > Screen.PrimaryScreen.Bounds.Width - 100)
					{
						Width = Screen.PrimaryScreen.Bounds.Width - 100;
					}
					if (Height > Screen.PrimaryScreen.Bounds.Height - 100)
					{
						Height = Screen.PrimaryScreen.Bounds.Height - 100;
					}

					//WindowState = FormWindowState.Maximized;
					FormBorderStyle = FormBorderStyle.Sizable;
					ShowIcon = true;
					Location = new Point(MousePosition.X - Width / 2, MousePosition.Y - Height / 2);
				}
				else
				{
					Size = new Size(100, 100);
					WindowState = FormWindowState.Normal;
					FormBorderStyle = FormBorderStyle.None;
					Location = new Point(Control.MousePosition.X - Width / 2, Control.MousePosition.Y - Height / 2);
					//pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				}
			}
		}

		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (pictureBox.Image != pictureBox.ErrorImage)
				{
					IsMax = !IsMax;
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				contextMenuStrip.Show(MousePosition);
			}
		}

		/// <summary>
		/// フォームを終了させない
		/// </summary>
		private void ImageViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Visible = false;
		}

		/// <summary>
		/// 画像の読み込みが完了
		/// </summary>
		private void pictureBox_LoadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			// Error
			if (e.Error != null)
			{
				labelProgress.Visible = false;
				pictureBox.Image = pictureBox.ErrorImage;
			}
			// 成功
			else
			{
				Focus();
				// ラベルを表示しない
				labelProgress.Visible = false;
				IsMax = IsMax;
			}
		}

		/// <summary>
		/// 読み込み中
		/// </summary>
		private void pictureBox_LoadProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			// ラベルを表示する
			if (!labelProgress.Visible)
			{
				labelProgress.Visible = true;
			}

			labelProgress.Text = string.Format("{0}% 読み込みました", e.ProgressPercentage);
		}

		#region コンテキストメニュー

		private void アドレスをコピーToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.SetDataObject(url, true);

			if (!(Left < MousePosition.X && MousePosition.X < Right &&
				Top < MousePosition.Y && MousePosition.Y < Bottom))
			{
				Visible = false;
			}
		}

		private void 画像を保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			string Ext = Path.GetExtension(url);
			sfd.Filter = "画像ファイル(" + Ext + ")|*" + Ext + "|すべてのファイル(*.*)|*.*";
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			sfd.DefaultExt = Ext;
			sfd.AddExtension = true;
			sfd.OverwritePrompt = true;
			sfd.FileName = Path.GetFileNameWithoutExtension(url);
			sfd.ShowDialog();

			if (sfd.FileName != "")
			{
				pictureBox.Image.Save(sfd.FileName);
			}

			if (!(Left < MousePosition.X && MousePosition.X < Right &&
				Top < MousePosition.Y && MousePosition.Y < Bottom))
			{
				Visible = false;
			}
		}

		#endregion
	}
}
