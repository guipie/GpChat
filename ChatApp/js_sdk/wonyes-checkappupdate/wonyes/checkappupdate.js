/**
  * 检测版本升级	js sdk
  * 版本号:是指 manifest.json->基础配置->应用版本名称的值，以2个点号分隔的3组数字，如 1.0.0  2.1.9 等
  * post请求将app版本号发往后端服务器，与服务器上的apk wgt 版本号进行比对后，返回最新版本的url下载地址。
  * 需自行完成后端比对代码，只要返回符合下列各式的json即可（附带一个php版后端示例代码)
  * POST 请求数据为 
  * 	{
  * 		version:'数字.数字.数字 形式版本号',
  * 		platform:'android或ios'
  * 	}
  * 存在新wgt 或 apk，返回
  * {
	    code:0
		msg:"ok"
		version:1.2.1//版本号
		url:'http...apk|wgt'//url下载地址，如果存在新wgt，则为wgt下载地址，否则再判断如果是ios，则处为 appstore地址
		log:''//更新文字说明，支持 \n 换行
		force:1// 是否强制升级，force=1则是强制升级，用户无法关闭升级提示框
	} 
 * 
 */
import {
	Upgrade
} from '@/api/system.js'

function check(param = {}) {
	// 合并默认参数
	param = Object.assign({
		title: "检测到有新版本！",
		content: "请升级app到最新版本！",
		canceltext: "暂不升级",
		oktext: "立即升级",
		barbackground: "rgba(50,50,50,0.8)",
		barbackgroundactive: "rgba(32,165,58,1)"
	}, param)
	plus.runtime.getProperty(plus.runtime.appid, (widgetInfo) => {
		let platform = plus.os.name.toLocaleLowerCase();
		Upgrade(platform, widgetInfo.versionCode).then(res => {
			if (res.data && res.data.url) {
				if (/\.wgt$/i.test(res.data.url.trim()) || (platform == 'android' && /\.apk$/i.test(res
						.data
						.url.trim()))) {
					// 如果是热更新  wgt 或 android平台下apk
					startdownload(param, res.data);
					return;
				}
				if (platform == 'ios') {
					// 如果是ios,则跳转到appstore
					plus.runtime.openURL(res.data.url)
				}
			}
		})
	});
}

function startdownload(param, data) {
	uni.showModal({
		title: "版本升级",
		content: data.desc ? data.desc : param.content,
		showCancel: data.force ? false : true,
		confirmText: param.oktext,
		cancelText: param.canceltext,
		success: res => {
			if (!res.confirm) {
				console.log('取消了升级');
				return
			}
			if (data.shichang === 1 && /\.apk$/i.test(data.url)) {
				//去应用市场更新
				plus.runtime.openURL(data.shichangurl);
				plus.runtime.restart();
			} else {
				// 开始下载
				// 创建下载任务
				var dtask = plus.downloader.createDownload(data.url, {
						filename: "_downloads/"
					},
					function(d, status) {
						console.log('d', d);
						// 下载完成
						if (status == 200) {
							plus.runtime.install(d.filename, {
								force: false
							}, function() {
								//进行重新启动;
								plus.runtime.restart();
							}, (e) => {
								uni.showToast({
									title: '安装升级包失败:' + JSON.stringify(e),
									icon: 'none'
								})
							});
						} else {
							plus.nativeUI.alert("下载升级包失败，请手动去站点下载安装，错误码: " + status);
						}
					});

				let wrapwidth = parseInt(plus.screen.resolutionWidth / 2);
				let view = new plus.nativeObj.View("maskView", {
					backgroundColor: param.barbackground,
					left: (wrapwidth / 2) + "px",
					bottom: "80px",
					width: wrapwidth + "px",
					height: "10px"
				});

				view.show()
				let viewinner = new plus.nativeObj.View("maskViewinner", {
					backgroundColor: param.barbackgroundactive,
					left: (wrapwidth / 2) + "px",
					bottom: "80px",
					width: "1px",
					height: "10px"
				});
				viewinner.show()
				dtask.addEventListener("statechanged", (e) => {
					if (e && e.downloadedSize > 0) {
						let jindu = parseInt((e.downloadedSize / e.totalSize) * wrapwidth)
						viewinner.setStyle({
							width: jindu + 'px'
						});
					}
				}, false);
				dtask.start();
			}
		}
	});
}

export default {
	check,
	startdownload
}
