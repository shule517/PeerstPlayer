using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace PeerstPlayer
{
	class HTTP
	{
		/// <summary>
		/// 指定したアドレスのHTMLを取得（エンコード指定）
		/// </summary>
		public static string GetHtml(string url, string encode)
		{
			try
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				WebResponse res = req.GetResponse();
				Stream resStream = res.GetResponseStream();
				StreamReader sr = new StreamReader(resStream, Encoding.GetEncoding(encode));

				string html = sr.ReadToEnd();
				sr.Close();
				resStream.Close();

				return html;
			}
			catch
			{
			}

			return "";
		}

		/// <summary>
		/// HTMLを取得（サーバを指定）
		/// </summary>
		public static string GetHtml(string host, string port_no, string url)
		{
			try
			{
				//文字コードを指定する
				System.Text.Encoding enc = System.Text.Encoding.UTF8;

				//TcpClientを作成し、サーバーと接続する
				System.Net.Sockets.TcpClient tcp = new System.Net.Sockets.TcpClient(host, int.Parse(port_no));

				//NetworkStreamを取得する
				System.Net.Sockets.NetworkStream ns = tcp.GetStream();

				//サーバーにデータを送信する
				//送信するデータを入力
				string sendMsg = "GET " + url + " HTTP/1.0\r\n\r\n";

				//文字列をByte型配列に変換
				byte[] sendBytes = enc.GetBytes(sendMsg);
				//データを送信する
				ns.Write(sendBytes, 0, sendBytes.Length);

				//サーバーから送られたデータを受信する
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				byte[] resBytes = new byte[256];
				int resSize;
				do
				{
					//データの一部を受信する
					resSize = ns.Read(resBytes, 0, resBytes.Length);
					//Readが0を返した時はサーバーが切断したと判断
					if (resSize == 0)
					{
						return "";
					}
					//受信したデータを蓄積する
					ms.Write(resBytes, 0, resSize);
				} while (ns.DataAvailable);
				//受信したデータを文字列に変換
				string resMsg = enc.GetString(ms.ToArray());
				ms.Close();

				//閉じる
				tcp.Close();

				resMsg = resMsg.Replace("\r\n", "\n");
				int s = resMsg.IndexOf("\n\n");
				return resMsg.Substring(s + 2);
			}
			catch
			{
			}

			return "";
		}


		/// <summary>
		/// HTMLを取得（サーバー指定 + エンコード指定）
		/// </summary>
		public static string GetHtml(string host, string port_no, string url, string encode)
		{
			try
			{
				//文字コードを指定する
				System.Text.Encoding enc = System.Text.Encoding.GetEncoding(encode);

				//TcpClientを作成し、サーバーと接続する
				System.Net.Sockets.TcpClient tcp = new System.Net.Sockets.TcpClient(host, int.Parse(port_no));

				//NetworkStreamを取得する
				System.Net.Sockets.NetworkStream ns = tcp.GetStream();

				//サーバーにデータを送信する
				//送信するデータを入力
				string sendMsg = "GET " + url + " HTTP/1.0\r\n\r\n";

				//文字列をByte型配列に変換
				byte[] sendBytes = enc.GetBytes(sendMsg);
				//データを送信する
				ns.Write(sendBytes, 0, sendBytes.Length);

				//サーバーから送られたデータを受信する
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				byte[] resBytes = new byte[256];
				int resSize;
				do
				{
					//データの一部を受信する
					resSize = ns.Read(resBytes, 0, resBytes.Length);
					//Readが0を返した時はサーバーが切断したと判断
					if (resSize == 0)
					{
						return "";
					}
					//受信したデータを蓄積する
					ms.Write(resBytes, 0, resSize);
				} while (ns.DataAvailable);
				//受信したデータを文字列に変換
				string resMsg = enc.GetString(ms.ToArray());
				ms.Close();

				//閉じる
				tcp.Close();

				resMsg = resMsg.Replace("\r\n", "\n");
				int s = resMsg.IndexOf("\n\n");
				return resMsg.Substring(s + 2);
			}
			catch
			{
			}

			return "";
		}


		public static void SendCommand(string host, string port_no, string url, string encode)
		{
			try
			{
				//文字コードを指定する
				System.Text.Encoding enc = System.Text.Encoding.GetEncoding(encode);

				//TcpClientを作成し、サーバーと接続する
				System.Net.Sockets.TcpClient tcp = new System.Net.Sockets.TcpClient(host, int.Parse(port_no));

				//NetworkStreamを取得する
				System.Net.Sockets.NetworkStream ns = tcp.GetStream();

				//サーバーにデータを送信する
				//送信するデータを入力
				string sendMsg = "GET " + url + " HTTP/1.0\r\n\r\n";

				//文字列をByte型配列に変換
				byte[] sendBytes = enc.GetBytes(sendMsg);
				//データを送信する
				ns.Write(sendBytes, 0, sendBytes.Length);
			}
			catch
			{
			}
		}
	}
}
