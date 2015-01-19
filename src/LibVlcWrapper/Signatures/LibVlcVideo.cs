using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using LibVlcWrapper.Structures;

namespace LibVlcWrapper.Signatures
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_toggle_fullscreen(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_set_fullscreen(LibVlcMediaPlayer mediaPlayer, bool fullscreen);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate bool libvlc_get_fullscreen(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int libvlc_video_get_height(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int libvlc_video_get_width(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate float libvlc_video_get_aspect_ratio(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_video_set_aspect_ratio(LibVlcMediaPlayer mediaPlayer, float ratio);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int libvlc_video_get_track(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate IntPtr libvlc_video_get_track_description(LibVlcMediaPlayer mediaPlayer);
}
