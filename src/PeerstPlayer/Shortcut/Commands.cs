﻿
namespace PeerstPlayer.Shortcut
{
	// コマンド実行
	public enum Commands
	{
		None,				// なし
		VolumeUp,			// 音量UP
		VolumeDown,			// 音量DOWN
		VolumeBalanceLeft,	// 音量バランス：左
		VolumeBalanceMiddle,// 音量バランス：中央
		VolumeBalanceRight,	// 音量バランス：右
		VolumeBalanceByWindowPos,// ウィンドウ位置に応じて音量バランスを変更
		Mute,				// ミュート切り替え
		WindowMaximize,		// ウィンドウ最大化
		WindowMinimize,		// ウィンドウ最小化
		MiniMute,			// 最小化ミュート
		Close,				// 閉じる
		OpenPeerstViewer,	// ビューワを開く
		VisibleStatusBar,	// ステータスバーの表示切り替え
		UpdateChannelInfo,	// チャンネル情報更新
		ShowNewRes,			// 新着レス表示
		TopMost,			// 最前列表示切り替え
		WindowSizeUp,		// ウィンドウサイズUP
		WindowSizeDown,		// ウィンドウサイズDOWN
		DisconnectRelay,	// リレー切断
		Bump,				// Bump
		WmpMenu,			// WMPメニュー表示
		FitMovieSize,		// 動画サイズに合わせる
		Screenshot,			// スクリーンショット
		OpenScreenshotFolder,// スクリーンショットフォルダを開く

		// ウィンドウ拡大率指定
		WindowScale25Per,
		WindowScale50Per,
		WindowScale75Per,
		WindowScale100Per,
		WindowScale150Per,
		WindowScale200Per,

		// ウィンドウサイズ指定
		WindowSize160x120,
		WindowSize320x240,
		WindowSize480x360,
		WindowSize640x480,
		WindowSize800x600,

		// 画面分割
		ScreenSplitWidthx5,
		ScreenSplitWidthx4,
		ScreenSplitWidthx3,
		ScreenSplitWidthx2,
		ScreenSplitWidthx1,
		ScreenSplitHeightx5,
		ScreenSplitHeightx4,
		ScreenSplitHeightx3,
		ScreenSplitHeightx2,
		ScreenSplitHeightx1,
	};
}
