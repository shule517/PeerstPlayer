using System;

using System.Runtime.InteropServices;

namespace LibVlcWrapper
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void LibVlcCallback(IntPtr args);
}
	