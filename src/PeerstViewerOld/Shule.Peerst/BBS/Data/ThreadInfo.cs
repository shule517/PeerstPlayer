using System;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// スレッド情報クラス
	/// </summary>
	public class ThreadInfo
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="threadTitle">スレッドタイトル</param>
		/// <param name="threadNo">スレッド番号</param>
		/// <param name="resCount">レス数</param>
		public ThreadInfo(string threadTitle, string threadNo, int resCount)
		{
			ThreadTitle = threadTitle;
			ThreadNo = threadNo;
			ResCount = resCount;
		}

		/// <summary>
		/// スレッドタイトル
		/// </summary>
		public string ThreadTitle { get; private set; }

		/// <summary>
		/// スレッド番号
		/// </summary>
		public string ThreadNo { get; private set; }

		/// <summary>
		/// レス数
		/// </summary>
		public int ResCount { get; private set; }

		/// <summary>
		/// スレッド作成日時
		/// </summary>
		public DateTime DateCreated
		{
			get
			{
				DateTime time = new DateTime(1970, 1, 1, 0, 0, 0);
				try
				{
					int num = int.Parse(ThreadNo);
					time = time.AddSeconds(num);

					// ローカルのタイムゾーン変更  
					time = System.TimeZone.CurrentTimeZone.ToLocalTime(time);
				}
				catch
				{
				}

				return time;
			}
		}

		/// <summary>
		/// スレッド速度取得
		/// </summary>
		public double ThreadSpeed
		{
			get
			{
				double speed = 0;
				try
				{
					int count = ResCount;
					double days = (DateTime.Now - DateCreated).TotalDays;
					speed = (double)count / days;
				}
				catch
				{
				}

				return speed;
			}
		}
	}
}
