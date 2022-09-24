
/**
 * 设置粘贴板数据
 * @param {String} text 要设置的字符串
 * 如果未设置参数，则清空数据
 */
function setClipboardText(text){
	try{
	var os = plus.os.name;
	text = text||'';
	if('iOS' == os){
		// var UIPasteboard  = plus.ios.importClass('UIPasteboard');  
		// var pasteboard = UIPasteboard.generalPasteboard();  
		// pasteboard.setValueforPasteboardType(text, 'public.utf8-plain-text');
		var pasteboard = plus.ios.invoke('UIPasteboard', 'generalPasteboard');
		plus.ios.invoke(pasteboard, 'setValue:forPasteboardType:', text, 'public.utf8-plain-text');
	}else{
		var main = plus.android.runtimeMainActivity();
		// var Context = plus.android.importClass('android.content.Context');
		// var clip = main.getSystemService(Context.CLIPBOARD_SERVICE);
		var clip = main.getSystemService('clipboard');
		plus.android.invoke(clip, 'setText', text);
	}
	}catch(e){
		console.error('error @setClipboardText!!');
	}
}

function getClipboardText(){
	try{
	var os = plus.os.name;
	if('iOS' == os){
		var pasteboard = plus.ios.invoke('UIPasteboard', 'generalPasteboard');
		return plus.ios.invoke(pasteboard, 'valueForPasteboardType:', 'public.utf8-plain-text')
	}else{
		var main = plus.android.runtimeMainActivity();
		var clip = main.getSystemService('clipboard');
		return plus.android.invoke(clip, 'getText');
	}
	}catch(e){
		console.error('error @getClipboardText!!');
	}
}


module.exports = {
	setText: setClipboardText,
	getText: getClipboardText
}
