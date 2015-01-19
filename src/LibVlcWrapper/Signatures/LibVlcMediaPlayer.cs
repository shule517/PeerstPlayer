using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using LibVlcWrapper.Structures;

namespace LibVlcWrapper.Signatures
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate LibVlcMediaPlayer libvlc_media_player_new(LibVlcInstance instance);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate LibVlcMediaPlayer libvlc_media_player_new_from_media(LibVlcMedia media);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_player_release(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_player_set_media(LibVlcMediaPlayer mediaPlayer, LibVlcMedia media);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate LibVlcMedia libvlc_media_player_get_media(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate LibVlcEventManager libvlc_media_player_event_manager(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate bool libvlc_media_player_is_playing(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int libvlc_media_player_play(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_player_pause(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_player_stop(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_player_set_hwnd(LibVlcMediaPlayer mediaPlayer, IntPtr handle);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate IntPtr libvlc_media_player_get_hwnd(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate long libvlc_media_player_get_length(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate long libvlc_media_player_get_time(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_player_set_time(LibVlcMediaPlayer mediaPlayer, long time);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate float libvlc_media_player_get_position(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_player_set_position(LibVlcMediaPlayer mediaPlayer, float position);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate float libvlc_media_player_get_rate(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_media_player_set_rate(LibVlcMediaPlayer mediaPlayer, float rate);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate float libvlc_media_player_get_fps(LibVlcMediaPlayer mediaPlayer);
}
