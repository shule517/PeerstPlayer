using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace PeerstViewer.Controls
{
	public class WebBrowserEx : WebBrowser
	{
		private AxHost.ConnectionPointCookie cookie;
		private WebBrowser2EventHelper helper;

		[DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[DispId(200)]
		public object Application
		{
			get
			{
				if (this.ActiveXInstance == null)
				{
					throw new AxHost.InvalidActiveXStateException("Application", AxHost.ActiveXInvokeKind.PropertyGet);
				}
				return (this.ActiveXInstance as IWebBrowser2).Application;
			}
		}

		[DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[DispIdAttribute(552)]
		public bool RegisterAsBrowser
		{
			get
			{
				if (this.ActiveXInstance == null)
				{
					throw new AxHost.InvalidActiveXStateException("RegisterAsBrowser", AxHost.ActiveXInvokeKind.PropertyGet);
				}
				return (this.ActiveXInstance as IWebBrowser2).RegisterAsBrowser;
			}
			set
			{
				if (this.ActiveXInstance == null)
				{
					throw new AxHost.InvalidActiveXStateException("RegisterAsBrowser", AxHost.ActiveXInvokeKind.PropertyGet);
				}
				(this.ActiveXInstance as IWebBrowser2).RegisterAsBrowser = value;
			}
		}

		[PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]
		protected override void CreateSink()
		{
			base.CreateSink();
			helper = new WebBrowser2EventHelper(this);
			cookie = new AxHost.ConnectionPointCookie(this.ActiveXInstance, helper, typeof(DWebBrowserEvents2));
		}

		[PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]
		protected override void DetachSink()
		{
			if (cookie != null)
			{
				cookie.Disconnect();
				cookie = null;
			}
			base.DetachSink();
		}

		[Category("アクション")]
		[Description("")]
		[Browsable(true)]
		public event WebBrowserNewWindow2EventHandler NewWindow2;
		[Category("アクション")]
		[Description("")]
		[Browsable(true)]
		public event WebBRowserNewWindow3EventHandler NewWindow3;

		protected virtual void OnNewWindow2(WebBrowserNewWindow2EventArgs e)
		{
			NewWindow2(this, e);
		}

		protected virtual void OnNewWindow3(WebBrowserNewWindow3EventArgs e)
		{
			NewWindow3(this, e);
		}

		private class WebBrowser2EventHelper : StandardOleMarshalObject, DWebBrowserEvents2
		{
			private WebBrowserEx parent;

			public WebBrowser2EventHelper(WebBrowserEx parent)
			{
				this.parent = parent;
			}

			public void NewWindow2(ref object ppDisp, ref bool cancel)
			{
				var e = new WebBrowserNewWindow2EventArgs(ppDisp);
				this.parent.OnNewWindow2(e);
				ppDisp = e.ppDisp;
				cancel = e.Cancel;
			}

			public void NewWindow3(ref object ppDisp, ref bool cancel, UInt32 dwFlags, string bstrUrlContext, string bstrUrl)
			{
				var e = new WebBrowserNewWindow3EventArgs(ref ppDisp, bstrUrlContext, bstrUrl);
				parent.OnNewWindow3(e);
				ppDisp = e.ppDisp;
				cancel = e.Cancel;
			}
		}

		[ComImport, Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		public interface DWebBrowserEvents2
		{
			[DispId(251)]
			void NewWindow2(
				[InAttribute(), OutAttribute(), MarshalAs(UnmanagedType.IDispatch)] ref object ppDisp,
				[InAttribute(), OutAttribute()] ref bool cancel);

			[DispId(273)]
			void NewWindow3(
				[In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object pDisp,
				[In, Out] ref bool cancel,
				[In] UInt32 dwFlags,
				[In] string bstrUrlContext,
				[In] string bstrUrl);
		}

		[ComImport, Guid("D30C1661-CDAF-11D0-8A3E-00C04FC9E26E")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		public interface IWebBrowser2
		{
			object Application { get; }
			bool RegisterAsBrowser { get; set; }
		}
	}

	public delegate void WebBrowserNewWindow2EventHandler(object sender, WebBrowserNewWindow2EventArgs e);
	public delegate void WebBRowserNewWindow3EventHandler(object sender, WebBrowserNewWindow3EventArgs e);

	public class WebBrowserNewWindow2EventArgs : CancelEventArgs
	{
		public object ppDisp { get; set; }

		public WebBrowserNewWindow2EventArgs(object ppDisp)
		{
			this.ppDisp = ppDisp;
		}
	}

	public class WebBrowserNewWindow3EventArgs : CancelEventArgs
	{
		public WebBrowserNewWindow3EventArgs(ref object ppDisp, string bstrUrlContext, string bstrUrl)
		{
			this.ppDisp = ppDisp;
			this.bstrUrlContext = bstrUrlContext;
			this.bstrUrl = bstrUrl;
		}

		public object ppDisp { get; set; }

		public string bstrUrlContext { get; set; }

		public string bstrUrl { get; set; }
	}
}
