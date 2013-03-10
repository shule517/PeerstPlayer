using System;
using System.Collections.Generic;
using System.Text;

namespace Shule.Peerst.Observer
{
	/// <summary>
	/// オブザーバ：観測者
	/// </summary>
	public interface Observer
	{
		/// <summary>
		/// 監視対象に更新あり
		/// </summary>
		/// <param name="param">パラメータ</param>
		void OnUpdate(Object param);
	}
}
