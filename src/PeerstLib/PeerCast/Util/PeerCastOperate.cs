using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeerstLib.Util;

namespace PeerstLib.PeerCast.Util
{
	public class PeerCastOperate
	{
		/// <summary>
		/// Bump
		/// </summary>
		static public void Bump()
		{
			/*
			string url = "/admin?cmd=bump&id=" + channelId;
			WebUtil.SendCommand(host, portNo, url, "Shift_JIS");

			WebUtil.SendCommand("", 7144, "", Encoding.Unicode);
			 */
		}

		/// <summary>
		/// リレー切断
		/// </summary>
		static public void DisconnectRelay()
		{
			/*
			TODO
			・現在見ている配信情報を共有したい
			 ChannelInfo{ channelId }

			・PeerCastの接続先情報を共有したい
			 StreamUrlInfo {
			  host
			  port
			 }
			 */

			string channelId = "90E13182A11873DF1B8ADD5F4E7C0A38";
			string url = string.Format("/admin?cmd=stop&id={0}", channelId);
			WebUtil.SendCommand("localhost", 7145, url, Encoding.GetEncoding("Shift_JIS"));

			/*
			// TODO 配信中でない場合は切断
			//if (form.channelInfo.IsInfo && form.channelInfo.Status != "BROADCAST")
			{
				string url = "/admin?cmd=stop&id=" + channelId;
				WebUtil.SendCommand(host, portNo, url, "Shift_JIS");
			}
			 */
		}

		/// <summary>
		/// リレーキープ
		/// </summary>
		static public void RelayKeep()
		{
			/*
			string url = "/admin?cmd=keep&id=" + channelId;
			WebUtil.SendCommand(host, portNo, url, "Shift_JIS");
			 */
		}
	}
}
