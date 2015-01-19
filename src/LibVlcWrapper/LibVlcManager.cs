using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using LibVlcWrapper.Signatures;

namespace LibVlcWrapper
{
	internal class LibVlcManager : IDisposable
	{
		private IntPtr libGccHandle = IntPtr.Zero;
		private IntPtr libVlcCoreHandle = IntPtr.Zero;
		private IntPtr libVlcHandle = IntPtr.Zero;

		internal libvlc_new libvlc_newDelegate;
		internal libvlc_release libvlc_releaseDelegate;
		internal libvlc_free libvlc_freeDelegate;
		internal libvlc_get_version libvlc_get_versionDelegate;
		internal libvlc_clearerr libvlc_clearerrDelegate;
		internal libvlc_errmsg libvlc_errmsgDelegate;
		internal libvlc_event_attach libvlc_event_attachDelegate;
		internal libvlc_event_detach libvlc_event_detachDelegate;

		internal libvlc_media_new_location libvlc_media_new_locationDelegate;
		internal libvlc_media_new_path libvlc_media_new_pathDelegate;
		internal libvlc_media_add_option libvlc_media_add_optionDelegate;
		internal libvlc_media_get_mrl libvlc_media_get_mrlDelegate;
		internal libvlc_media_get_state libvlc_media_get_stateDelegate;
		internal libvlc_media_get_stats libvlc_media_get_statsDelegate;
		internal libvlc_media_event_manager libvlc_media_event_managerDelegate;
		internal libvlc_media_get_duration libvlc_media_get_durationDelegate;
		internal libvlc_media_parse libvlc_media_parseDelegate;
		internal libvlc_media_is_parsed libvlc_media_is_parsedDelegate;
		internal libvlc_media_tracks_get libvlc_media_tracks_getDelegate;
		internal libvlc_media_tracks_release libvlc_media_tracks_releaseDelegate;
		internal libvlc_media_release libvlc_media_releaseDelegate;

		internal libvlc_media_player_new libvlc_media_player_newDelegate;
		internal libvlc_media_player_new_from_media libvlc_media_player_new_from_mediaDelegate;
		internal libvlc_media_player_release libvlc_media_player_releaseDelegate;
		internal libvlc_media_player_set_media libvlc_media_player_set_mediaDelegate;
		internal libvlc_media_player_get_media libvlc_media_player_get_mediaDelegate;
		internal libvlc_media_player_event_manager libvlc_media_player_event_managerDelegate;
		internal libvlc_media_player_is_playing libvlc_media_player_is_playingDelegate;
		internal libvlc_media_player_play libvlc_media_player_playDelegate;
		internal libvlc_media_player_pause libvlc_media_player_pauseDelegate;
		internal libvlc_media_player_stop libvlc_media_player_stopDelegate;
		internal libvlc_media_player_set_hwnd libvlc_media_player_set_hwndDelegate;
		internal libvlc_media_player_get_hwnd libvlc_media_player_get_hwndDelegate;
		internal libvlc_media_player_get_length libvlc_media_player_get_lengthDelegate;
		internal libvlc_media_player_get_time libvlc_media_player_get_timeDelegate;
		internal libvlc_media_player_set_time libvlc_media_player_set_timeDelegate;
		internal libvlc_media_player_get_position libvlc_media_player_get_positionDelegate;
		internal libvlc_media_player_set_position libvlc_media_player_set_positionDelegate;
		internal libvlc_media_player_get_rate libvlc_media_player_get_rateDelegate;
		internal libvlc_media_player_set_rate libvlc_media_player_set_rateDelegate;
		internal libvlc_media_player_get_fps libvlc_media_player_get_fpsDelegate;

		internal libvlc_toggle_fullscreen libvlc_toggle_fullscreenDelegate;
		internal libvlc_set_fullscreen libvlc_set_fullscreenDelegate;
		internal libvlc_get_fullscreen libvlc_get_fullscreenDelegate;
		internal libvlc_video_get_height libvlc_video_get_heightDelegate;
		internal libvlc_video_get_width libvlc_video_get_widthDelegate;
		internal libvlc_video_get_aspect_ratio libvlc_video_get_aspect_ratioDelegate;
		internal libvlc_video_set_aspect_ratio libvlc_video_set_aspect_ratioDelegate;
		internal libvlc_video_get_track libvlc_video_get_trackDelegate;
		internal libvlc_video_get_track_description libvlc_video_get_track_descriptionDelegate;

		internal libvlc_audio_toggle_mute libvlc_audio_toggle_muteDelegate;
		internal libvlc_audio_get_mute libvlc_audio_get_muteDelegate;
		internal libvlc_audio_set_mute libvlc_audio_set_muteDelegate;
		internal libvlc_audio_get_volume libvlc_audio_get_volumeDelegate;
		internal libvlc_audio_set_volume libvlc_audio_set_volumeDelegate;
		internal libvlc_audio_get_track_count libvlc_audio_get_track_countDelegate;
		internal libvlc_audio_get_track_description libvlc_audio_get_track_descriptionDelegate;

		internal LibVlcManager(DirectoryInfo vlcDirectoryInfo)
		{
			if (!vlcDirectoryInfo.Exists)
			{
				throw new DirectoryNotFoundException(vlcDirectoryInfo.FullName);
			}

			if (IntPtr.Size == 8)
			{
				var libGccPath = Path.Combine(vlcDirectoryInfo.FullName, "libgcc_s_seh-1.dll");
				if (!File.Exists(libGccPath))
				{
					throw new FileNotFoundException(libGccPath);
				}
				libGccHandle = Win32API.LoadLibrary(libGccPath);
				if (libGccHandle == IntPtr.Zero)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}

			var libVlcCorePath = Path.Combine(vlcDirectoryInfo.FullName, "libvlccore.dll");
			if (!File.Exists(libVlcCorePath))
			{
				throw new FileNotFoundException(libVlcCorePath);
			}
			// プロセスとLibVLCのアーキテクチャが一致しない
			if (!(IntPtr.Size == 8 && Utility.Is64BitDll(libVlcCorePath)) &&
				!(IntPtr.Size == 4 && !Utility.Is64BitDll(libVlcCorePath)))
			{
				throw new BadImageFormatException("", libVlcCorePath);
			}
			libVlcCoreHandle = Win32API.LoadLibrary(libVlcCorePath);
			if (libVlcCoreHandle == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}

			var libVlcPath = Path.Combine(vlcDirectoryInfo.FullName, "libvlc.dll");
			if (!File.Exists(libVlcPath))
			{
				throw new FileNotFoundException(libVlcPath);
			}
			libVlcHandle = Win32API.LoadLibrary(libVlcPath);
			if (libVlcHandle == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}

			GetAddresses();
		}

		~LibVlcManager()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (libVlcHandle != IntPtr.Zero)
			{
				Win32API.FreeLibrary(libVlcHandle);
				libVlcHandle = IntPtr.Zero;
			}
			if (libVlcCoreHandle != IntPtr.Zero)
			{
				Win32API.FreeLibrary(libVlcCoreHandle);
				libVlcCoreHandle = IntPtr.Zero;
			}
			if (libGccHandle != IntPtr.Zero)
			{
				Win32API.FreeLibrary(libGccHandle);
				libGccHandle = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		void GetAddresses()
		{
			libvlc_newDelegate = GetDelegate<libvlc_new>();
			libvlc_releaseDelegate = GetDelegate<libvlc_release>();
			libvlc_freeDelegate = GetDelegate<libvlc_free>();
			libvlc_get_versionDelegate = GetDelegate<libvlc_get_version>();
			libvlc_clearerrDelegate = GetDelegate<libvlc_clearerr>();
			libvlc_errmsgDelegate = GetDelegate<libvlc_errmsg>();
			libvlc_event_attachDelegate = GetDelegate<libvlc_event_attach>();
			libvlc_event_detachDelegate = GetDelegate<libvlc_event_detach>();

			libvlc_media_new_locationDelegate = GetDelegate<libvlc_media_new_location>();
			libvlc_media_new_pathDelegate = GetDelegate<libvlc_media_new_path>();
			libvlc_media_add_optionDelegate = GetDelegate<libvlc_media_add_option>();
			libvlc_media_get_mrlDelegate = GetDelegate<libvlc_media_get_mrl>();
			libvlc_media_get_stateDelegate = GetDelegate<libvlc_media_get_state>();
			libvlc_media_get_statsDelegate = GetDelegate<libvlc_media_get_stats>();
			libvlc_media_event_managerDelegate = GetDelegate<libvlc_media_event_manager>();
			libvlc_media_get_durationDelegate = GetDelegate<libvlc_media_get_duration>();
			libvlc_media_parseDelegate = GetDelegate<libvlc_media_parse>();
			libvlc_media_is_parsedDelegate = GetDelegate<libvlc_media_is_parsed>();
			libvlc_media_tracks_getDelegate = GetDelegate<libvlc_media_tracks_get>();
			libvlc_media_tracks_releaseDelegate = GetDelegate<libvlc_media_tracks_release>();
			libvlc_media_releaseDelegate = GetDelegate<libvlc_media_release>();

			libvlc_media_player_newDelegate = GetDelegate<libvlc_media_player_new>();
			libvlc_media_player_new_from_mediaDelegate = GetDelegate<libvlc_media_player_new_from_media>();
			libvlc_media_player_releaseDelegate = GetDelegate<libvlc_media_player_release>();
			libvlc_media_player_set_mediaDelegate = GetDelegate<libvlc_media_player_set_media>();
			libvlc_media_player_get_mediaDelegate = GetDelegate<libvlc_media_player_get_media>();
			libvlc_media_player_event_managerDelegate = GetDelegate<libvlc_media_player_event_manager>();
			libvlc_media_player_is_playingDelegate = GetDelegate<libvlc_media_player_is_playing>();
			libvlc_media_player_playDelegate = GetDelegate<libvlc_media_player_play>();
			libvlc_media_player_pauseDelegate = GetDelegate<libvlc_media_player_pause>();
			libvlc_media_player_stopDelegate = GetDelegate<libvlc_media_player_stop>();
			libvlc_media_player_set_hwndDelegate = GetDelegate<libvlc_media_player_set_hwnd>();
			libvlc_media_player_get_hwndDelegate = GetDelegate<libvlc_media_player_get_hwnd>();
			libvlc_media_player_get_lengthDelegate = GetDelegate<libvlc_media_player_get_length>();
			libvlc_media_player_get_timeDelegate = GetDelegate<libvlc_media_player_get_time>();
			libvlc_media_player_set_timeDelegate = GetDelegate<libvlc_media_player_set_time>();
			libvlc_media_player_get_positionDelegate = GetDelegate<libvlc_media_player_get_position>();
			libvlc_media_player_set_positionDelegate = GetDelegate<libvlc_media_player_set_position>();
			libvlc_media_player_get_rateDelegate = GetDelegate<libvlc_media_player_get_rate>();
			libvlc_media_player_set_rateDelegate = GetDelegate<libvlc_media_player_set_rate>();
			libvlc_media_player_get_fpsDelegate = GetDelegate<libvlc_media_player_get_fps>();

			libvlc_toggle_fullscreenDelegate = GetDelegate<libvlc_toggle_fullscreen>();
			libvlc_set_fullscreenDelegate = GetDelegate<libvlc_set_fullscreen>();
			libvlc_get_fullscreenDelegate = GetDelegate<libvlc_get_fullscreen>();
			libvlc_video_get_heightDelegate = GetDelegate<libvlc_video_get_height>();
			libvlc_video_get_widthDelegate = GetDelegate<libvlc_video_get_width>();
			libvlc_video_get_aspect_ratioDelegate = GetDelegate<libvlc_video_get_aspect_ratio>();
			libvlc_video_set_aspect_ratioDelegate = GetDelegate<libvlc_video_set_aspect_ratio>();
			libvlc_video_get_trackDelegate = GetDelegate<libvlc_video_get_track>();
			libvlc_video_get_track_descriptionDelegate = GetDelegate<libvlc_video_get_track_description>();

			libvlc_audio_toggle_muteDelegate = GetDelegate<libvlc_audio_toggle_mute>();
			libvlc_audio_get_muteDelegate = GetDelegate<libvlc_audio_get_mute>();
			libvlc_audio_set_muteDelegate = GetDelegate<libvlc_audio_set_mute>();
			libvlc_audio_get_volumeDelegate = GetDelegate<libvlc_audio_get_volume>();
			libvlc_audio_set_volumeDelegate = GetDelegate<libvlc_audio_set_volume>();
			libvlc_audio_get_track_countDelegate = GetDelegate<libvlc_audio_get_track_count>();
			libvlc_audio_get_track_descriptionDelegate = GetDelegate<libvlc_audio_get_track_description>();
		}

		private T GetDelegate<T>()
		{
			var address = Win32API.GetProcAddress(libVlcHandle, typeof(T).Name);
			if (address == IntPtr.Zero) throw new Win32Exception();
			var delegatePointer = Marshal.GetDelegateForFunctionPointer(address, typeof(T));
			return (T)Convert.ChangeType(delegatePointer, typeof(T));
		}
	}
}
