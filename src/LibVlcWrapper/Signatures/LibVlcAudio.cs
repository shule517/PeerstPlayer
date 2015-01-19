using System;
using System.Runtime.InteropServices;
using LibVlcWrapper.Structures;

namespace LibVlcWrapper.Signatures
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_audio_toggle_mute(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate bool libvlc_audio_get_mute(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_audio_set_mute(LibVlcMediaPlayer mediaPlayer, bool status);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int libvlc_audio_get_volume(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void libvlc_audio_set_volume(LibVlcMediaPlayer mediaPlayer, int volume);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int libvlc_audio_get_track_count(LibVlcMediaPlayer mediaPlayer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate IntPtr libvlc_audio_get_track_description(LibVlcMediaPlayer mediaPlayer);
}
