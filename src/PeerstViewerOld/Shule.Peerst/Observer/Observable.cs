using System;
using System.Collections.Generic;
using System.Text;

namespace Shule.Peerst.Observer
{
	/// <summary>
	/// オブザーバの通知先：観測対象
	/// </summary>
	public class Observable
	{
		/// <summary>
		/// オブザーバリスト
		/// </summary>
		private List<Observer> observers = new List<Observer>();

		/// <summary>
		/// オブザーバの追加
		/// </summary>
		/// <param name="observer">追加対象のオブザーバ</param>
		public void AddObserver(Observer observer)
		{
			observers.Add(observer);
		}

		/// <summary>
		/// オブザーバの削除
		/// </summary>
		/// <param name="observer">削除対象のオブザーバ</param>
		public void DeleteObserver(Observer observer)
		{
			observers.Remove(observer);
		}

		/// <summary>
		/// オブザーバへ通知
		/// </summary>
		/// <param name="param">渡すデータ</param>
		public void NotifyObservers(Object param)
		{
			foreach(Observer observer in observers)
			{
				observer.Update(param);
			}
		}

		/// <summary>
		/// オブザーバへ通知
		/// </summary>
		protected void NotifyObservers()
		{
			NotifyObservers(new Object());
		}
	}
}
