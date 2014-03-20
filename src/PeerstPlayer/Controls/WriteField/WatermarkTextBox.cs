using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Extentions
{
	public class WatermarkTextBox : System.Windows.Forms.TextBox
	{
		public WatermarkTextBox()
		{
			this.empty = true;
			this._WatermarkText = string.Empty;
			this._WatermarkColor = Color.DarkGray;
			this._ForegroundColor = this.ForeColor;
		}

		/// <summary>本来のテキストが空かどうか</summary>
		private bool empty;
		/// <summary>透かし文字の表示・非表示の設定中に ModifiedChanged プロパティを変更しているかどうか</summary>
		private bool modifiedChanging;

		private void SetBaseText(string text)
		{
			bool m = this.Modified;
			base.Text = text;
			this.modifiedChanging = true;
			this.Modified = m;
			this.modifiedChanging = false;
		}

		protected override void OnModifiedChanged(EventArgs e)
		{
			if (this.modifiedChanging == true)
			{
				return;
			}

			base.OnModifiedChanged(e);
		}

		protected override void OnEnter(EventArgs e)
		{
			if (this.empty == true)
			{
				this.empty = false;
				base.PasswordChar = this._PasswordChar;
				base.ForeColor = this.ForegroundColor;
				this.SetBaseText(string.Empty);
			}

			base.OnEnter(e);
		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);

			if (base.Text == string.Empty && this.WatermarkText != string.Empty)
			{
				this.empty = true;
				base.PasswordChar = '\0';
				base.ForeColor = this.WatermarkColor;
				this.SetBaseText(this.WatermarkText);
			}
			else
			{
				this.empty = false;
			}
		}

		[RefreshProperties(RefreshProperties.Repaint)]
		public override string Text
		{
			get
			{
				if (this.empty == true)
				{
					return string.Empty;
				}

				return base.Text;
			}
			set
			{
				if (value == string.Empty && this.WatermarkText != string.Empty)
				{
					this.empty = true;
					base.PasswordChar = '\0';
					base.ForeColor = this.WatermarkColor;
					base.Text = this.WatermarkText;
				}
				else
				{
					this.empty = false;
					base.PasswordChar = this._PasswordChar;
					if (base.ForeColor != this._ForegroundColor)
					{
						base.ForeColor = this._ForegroundColor;
					}
					base.Text = value;
				}
			}
		}

		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set
			{
				if (this.empty == true)
				{
					this._WatermarkColor = value;
				}
				else
				{
					this._ForegroundColor = value;
				}

				base.ForeColor = value;
			}
		}

		private Color _ForegroundColor;
		[Category("表示")]
		[DefaultValue(typeof(Color), "WindowText")]
		[Description("ForeColor プロパティに設定したコントロールの前景色を取得または設定します。")]
		public Color ForegroundColor
		{
			get { return this._ForegroundColor; }
			set
			{
				this._ForegroundColor = value;

				if (this.empty == false && base.ForeColor != this._ForegroundColor)
				{
					base.ForeColor = value;
				}
			}
		}

		private Char _PasswordChar;
		public new Char PasswordChar
		{
			get
			{
				if (this.empty == true)
				{
					return '\0';
				}

				return base.PasswordChar;
			}
			set
			{
				this._PasswordChar = value;

				if (this.empty == false)
				{
					base.PasswordChar = value;
				}
			}
		}

		private string _WatermarkText;
		[Category("表示")]
		[DefaultValue("")]
		[Description("テキストが空の場合に表示する文字列を設定または取得します。")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public string WatermarkText
		{
			get { return this._WatermarkText; }
			set
			{
				this._WatermarkText = value;

				if (this.Text == string.Empty && value != string.Empty)
				{
					this.empty = true;
					base.PasswordChar = '\0';
					base.ForeColor = this.WatermarkColor;
					this.SetBaseText(value);
				}
				else if (this.Text == string.Empty && value == string.Empty)
				{
					this.Text = string.Empty;
				}
			}
		}

		private Color _WatermarkColor;
		[Category("表示")]
		[DefaultValue(typeof(Color), "DarkGray")]
		[Description("テキストが空の場合に表示する文字列の色を設定または取得します。")]
		public Color WatermarkColor
		{
			get { return this._WatermarkColor; }
			set
			{
				this._WatermarkColor = value;

				if (this.empty == true)
				{
					base.ForeColor = value;
				}
			}
		}
	}
}