コマンドライン引数の設定について

【PeCaRecorder】
引数："$x" "$9" "$0" ⇒　"ストリームURL" "タイプ" "チャンネル名"

【pcypLite】
引数： "<stream/>" "<type/>" "<channelname/>"　⇒　　"ストリームURL" "タイプ" "チャンネル名"

【pcmp+】
タイプ：WMVの場合　引数："<stream />" "WMV" "<channelname />"
タイプ：FLVの場合　引数："<stream />" "FLV" "<channelname />"
タイプ：MKVの場合　引数："<stream />" "MKV" "<channelname />"
タイプ：WEBMの場合　引数："<stream />" "WEBM" "<channelname />"
※pcmpの場合はtypeの指定ができないため、WMV/FLV/MKV/WEBMのそれぞれを登録してください。

利用しているライブラリ
	log4net(http://logging.apache.org/log4net/) Apache License, Version 2.0
	VLC media player(http://www.videolan.org/vlc/GNU) GENERAL PUBLIC LICENSE Version 2
