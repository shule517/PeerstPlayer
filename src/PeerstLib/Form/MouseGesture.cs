
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PeerstPlayer
{
	/// <summary>
	/// マウスジェスチャを取得するクラス
	/// </summary>
	public class MouseGesture
	{
		// 基準位置
		Point? basePoint;
		// 方向のリスト
		List<Direction> value = new List<Direction>();
		// 矢印
		string[] arrows = { "", "↑", "↓", "←", "→", "[←↑]", "[→↑]", "[←↓]", "[→↓]" };
		// 感度
		int interval = 30;
		// 斜め認識
		bool slant = false;
		// 厳密さ
		int strict = 3;
		// 厳密チェック  以前の方向
		Direction strict_before;
		// 厳密チェック カウント
		int strict_count = 0;
		// 重複を許可
		bool duplicate = false;

		/// <summary>
		/// ジェスチャの内容
		/// </summary>
		public List<Direction> Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		/// <summary>
		/// ジェスチャ判定する間隔。値が少ないほど、細かい動きに反応するようになる。
		/// </summary>
		public int Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				if (value < 1) { value = 1; }
				this.interval = value;
			}
		}

		/// <summary>
		/// 斜めも感知するかどうか
		/// </summary>
		public bool Slant
		{
			get { return this.slant; }
			set { this.slant = value; }
		}

		/// <summary>
		/// 厳密さ。値が高いほど、厳しく判定するようになる。
		/// </summary>
		public int Strict
		{
			get { return this.strict; }
			set
			{
				if (value < 1) { value = 1; }
				this.strict = value;
			}
		}

		/// <summary>
		/// 重複を許可。同じ方向に長く動いた場合に重複して軌跡を記録する。
		/// </summary>
		public bool Duplicate
		{
			get { return this.duplicate; }
			set { this.duplicate = value; }
		}

		/// <summary>
		/// ジェスチャの記録開始
		/// </summary>
		public void Start()
		{
			this.basePoint = null;
			this.value.Clear();
		}

		/// <summary>
		/// ジェスチャ中に現在のポインタの位置を記録
		/// </summary>
		/// <param name="point">現在のマウスポインタの位置</param>
		public void Moving(Point point)
		{
			if (this.basePoint == null) { this.basePoint = (Point?)point; }
			Direction dir = this.JudgeDirection(point, (Point)this.basePoint);
			if (dir == Direction.None) { return; }
			if (this.strict_before == dir) { this.strict_count++; }
			else { this.strict_before = dir; this.strict_count = 0; }
			if (this.strict_count >= this.strict
				&& (this.duplicate || (this.value.Count == 0 || this.value[this.value.Count - 1] != dir)))
			{
				this.value.Add(dir);
				this.strict_count = 0;
			}
			this.basePoint = null;
		}

		/// <summary>
		/// ジェスチャの文字列表現
		/// </summary>
		/// <returns>矢印記号で表した現在のジェスチャ</returns>
		public override string ToString()
		{
			StringBuilder strb = new StringBuilder();
			foreach (Direction d in this.value)
			{
				strb.Append(this.arrows[(int)d]);
			}
			return strb.ToString();
		}

		/// <summary>
		/// 方向を判断
		/// </summary>
		/// <param name="a">現在のポインタの位置</param>
		/// <param name="b">基準となるポインタの位置</param>
		/// <returns>方向</returns>
		protected Direction JudgeDirection(Point a, Point b)
		{
			if ((int)(Math.Sqrt(Math.Pow(Math.Abs(a.X - b.X), 2) + Math.Pow(Math.Abs(a.Y - b.Y), 2))) < this.Interval / this.Strict)
			{
				return Direction.None;
			}
			double dir = Math.Abs(a.X - b.X) / (Math.Abs(a.Y - b.Y) + 0.0001);

			if (this.Slant && dir > 0.4 && dir < 1.6)
			{
				if (a.X > b.X)
				{
					if (a.Y > b.Y) { return Direction.BottomRight; }
					else { return Direction.TopRight; }
				}
				else
				{
					if (a.Y > b.Y) { return Direction.BottomLeft; }
					else { return Direction.TopLeft; }
				}
			}
			if (dir > 1)
			{
				if (a.X > b.X) { return Direction.Right; }
				else { return Direction.Left; }
			}
			else
			{
				if (a.Y > b.Y) { return Direction.Bottom; }
				else { return Direction.Top; }
			}
		}
	}

	/// <summary>
	/// 方向を表す列挙型
	/// </summary>
	public enum Direction
	{
		/// <summary>
		/// 無指向
		/// </summary>
		None = 0,
		/// <summary>
		/// 上方向
		/// </summary>
		Top = 1,
		/// <summary>
		/// 下方向
		/// </summary>
		Bottom = 2,
		/// <summary>
		/// 左方向
		/// </summary>
		Left = 3,
		/// <summary>
		/// 右方向
		/// </summary>
		Right = 4,
		/// <summary>
		/// 左上
		/// </summary>
		TopLeft = 5,
		/// <summary>
		/// 右上
		/// </summary>
		TopRight = 6,
		/// <summary>
		/// 左下
		/// </summary>
		BottomLeft = 7,
		/// <summary>
		/// 右下
		/// </summary>
		BottomRight = 8
	}
}
