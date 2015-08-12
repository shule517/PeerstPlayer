コマンドライン引数の設定について

【PeCaRecorder】
引数："$x" "$9" "$0" "$3" ⇒　"ストリームURL" "タイプ" "チャンネル名" "コンタクトURL"

【pcypLite】
引数： "<stream/>" "<type/>" "<channelname/>" "<contact/>"　⇒　　"ストリームURL" "タイプ" "チャンネル名" "コンタクトURL"

【pcmp+】
タイプ：WMVの場合　引数：<stream /> "WMV" <channelname />
タイプ：FLVの場合　引数：<stream /> "FLV" <channelname />
※pcmpの場合はtypeの指定ができないため、WMV/FLVのそれぞれを登録してください。
※pcmpの場合はコンタクトURLの指定もできないため、特定の配信で掲示板URLが取得されない場合外部ツールに、以下の引数を設定して外部ツールから再生してください。
引数：<stream /> <type /> <channelname /> <contact />