using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using LibVlcWrapper.Signatures;
using LibVlcWrapper.Structures;

namespace LibVlcWrapper
{
	public class LibVlc : IDisposable
	{
		internal LibVlcManager Manager { get; private set; }

		internal LibVlcInstance Handle { get; private set; }

		public LibVlc(DirectoryInfo directoryInfo)
		{
			Manager = new LibVlcManager(directoryInfo);
		}

		~LibVlc()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			//Release();
			GC.SuppressFinalize(this);
		}

		public void CreateInstance(string[] args)
		{
			if (args == null) args = new string[0];
			Handle = Manager.libvlc_newDelegate(args.Length, args);
		}

		public void Release()
		{
			if (Handle.Pointer == IntPtr.Zero) return;
			try
			{
				Manager.libvlc_releaseDelegate(Handle);
			}
			catch (Exception)
			{
			}
		}

		public string GetVersion()
		{
			return Manager.libvlc_get_versionDelegate();
		}

		public void ClearError()
		{
			Manager.libvlc_clearerrDelegate();
		}

		public string GetErrorMessage()
		{
			return Manager.libvlc_errmsgDelegate();
		}

		internal int AttachEvent(LibVlcEventManager eventManager, EventTypes eventType, LibVlcCallback callback,
			IntPtr userData = default(IntPtr))
		{
			return Manager.libvlc_event_attachDelegate(eventManager, eventType, callback, userData);
		}

		internal int DetachEvent(LibVlcEventManager eventManager, EventTypes eventType, LibVlcCallback callback,
			IntPtr userData = default(IntPtr))
		{
			return Manager.libvlc_event_detachDelegate(eventManager, eventType, callback, userData);
		}

		public Media CreateMedia(FileInfo fileInfo)
		{
			if (Handle.Pointer == IntPtr.Zero) throw new InvalidOperationException();
			return new Media(this, fileInfo);
		}

		public Media CreateMedia(Uri uri)
		{
			if (Handle.Pointer == IntPtr.Zero) throw new InvalidOperationException();
			return new Media(this, uri);
		}

		public MediaPlayer CreateMediaPlayer()
		{
			if (Handle.Pointer == IntPtr.Zero) throw new InvalidOperationException();
			return new MediaPlayer(this);
		}
	}
}
