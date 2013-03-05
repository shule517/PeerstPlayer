using System;
using System.Collections.Generic;
using System.Text;

namespace PeerstPlayer
{
	enum Commands
	{
		// ウィンドウサイズ
		SizeScale,			// 75%,50%,200%,150%,100%,10%
		Width,				// 800,640,480,320,160
		Height,				// 600,480,360,240,120
		SizePlusScale,		// -10%,+10%
		SizePlus,			// +10,-10
		ScreenSplitWidth,	// 5,4,3,2
		ScreenSplitHeight,	// 5,4,3,2
		ScreenSplit,		// 5,4,3,2
		FitSizeMovie,
		WMPFullScreen,
		FullScreen,

		// 音量
		VolumeBalanceRight,
		VolumeBalanceMiddle,
		VolumeBalanceLeft,
		VolumeBalance,		// -10,+10
		Volume,				// -5,-10,-1,+5,+10,+1
		Mute,
		MiniAndMute,

		// ウィンドウ設定
		TopMost,
		Frame,
		AspectRate,

		SelectResBox,
		StatusLabel,
		ResBox,

		// 開く
		OpenFile,
		OpenClipBoard,

		// 機能
		ScreenShot,
		OpenThreadViewer,
		OpenScreenShotFolder,
		OpenContextMenu,
		ChannelInfoUpdate,

		// 動画
		Retry,
		Bump,

		// 終了
		CloseAndRelayCut,
		Close,
	}
}
