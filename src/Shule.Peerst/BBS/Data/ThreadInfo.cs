using System;
namespace Shule.Peerst.BBS
{
	/// <summary>
	/// スレッド情報クラス
	/// </summary>
	public class ThreadInfo
	{
		public ThreadInfo(string title, string threadNo, string resCount)
		{
			Title = title;
			ThreadNo = threadNo;
			ResCount = resCount;
		}

		public string Title { get; set; }		// スレタイ
		public string ThreadNo { get; set; }	// スレッド番号
		public string ResCount { get; set; }	// レス数

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
					int count = int.Parse(ResCount);
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
