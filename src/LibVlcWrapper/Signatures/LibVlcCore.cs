using System;
using System.Runtime.InteropServices;
using LibVlcWrapper.Structures;

namespace LibVlcWrapper.Signatures
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate LibVlcInstance libvlc_new(int argc, string[] argv);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_release(LibVlcInstance instance);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_free(IntPtr ptr);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate string libvlc_get_version();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_clearerr();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate string libvlc_errmsg();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int libvlc_event_attach(LibVlcEventManager eventManager, EventTypes eventType, LibVlcCallback callback, IntPtr userData);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int libvlc_event_detach(LibVlcEventManager eventManager, EventTypes eventType, LibVlcCallback callback, IntPtr userData);

}
