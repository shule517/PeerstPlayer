using System;
using System.Runtime.InteropServices;
using LibVlcWrapper.Structures;

namespace LibVlcWrapper.Signatures
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate LibVlcMedia libvlc_media_new_location(LibVlcInstance instance, string mrl);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate LibVlcMedia libvlc_media_new_path(LibVlcInstance instance, string path);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_add_option(LibVlcMedia media, string options);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate string libvlc_media_get_mrl(LibVlcMedia media);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate MediaStates libvlc_media_get_state(LibVlcMedia media);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate bool libvlc_media_get_stats(LibVlcMedia media, IntPtr stats);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate LibVlcEventManager libvlc_media_event_manager(LibVlcMedia media);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate long libvlc_media_get_duration(LibVlcMedia media);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_parse(LibVlcMedia media);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate bool libvlc_media_is_parsed(LibVlcMedia media);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate uint libvlc_media_tracks_get(LibVlcMedia media, out IntPtr tracks);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_tracks_release(IntPtr tracks, uint count);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_release(LibVlcMedia media);
}
