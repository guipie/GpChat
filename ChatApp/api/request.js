// 200 OK - [GET]：服务器成功返回用户请求的数据，该操作是幂等的（Idempotent）。
// 201 CREATED - [POST/PUT/PATCH]：用户新建或修改数据成功。
// 202 Accepted - [*]：表示一个请求已经进入后台排队（异步任务）
// 204 NO CONTENT - [DELETE]：用户删除数据成功。
// 400 INVALID REQUEST - [POST/PUT/PATCH]：用户发出的请求有错误，服务器没有进行新建或修改数据的操作，该操作是幂等的。
// 401 Unauthorized - [*]：表示用户没有权限（令牌、用户名、密码错误）。
// 403 Forbidden - [*] 表示用户得到授权（与401错误相对），但是访问是被禁止的。
// 404 NOT FOUND - [*]：用户发出的请求针对的是不存在的记录，服务器没有进行操作，该操作是幂等的。
// 406 Not Acceptable - [GET]：用户请求的格式不可得（比如用户请求JSON格式，但是只有XML格式）。
// 410 Gone -[GET]：用户请求的资源被永久删除，且不会再得到的。
// 422 Unprocesable entity - [POST/PUT/PATCH] 当创建一个对象时，发生一个验证错误。
// 500 INTERNAL SERVER ERROR - [*]：服务器发生错误，用户将无法判断发出的请求是否成功。
import Request from '@/js_sdk/luch-request/luch-request/index.js' // 
import {
	baseUrl
} from '@/common/env.js'

const config = {
	baseURL: baseUrl,
	/* 会与全局header合并，如有同名属性，局部覆盖全局 */
	header: {
		"content-type": "application/json-patch+json"
	},
	dataType: 'json',
	// 注：如果局部custom与全局custom有同名属性，则后面的属性会覆盖前面的属性，相当于Object.assign(全局，局部)
	custom: {
		auth: true
	}, // 可以加一些自定义参数，在拦截器等地方使用。比如这里我加了一个auth，可在拦截器里拿到，如果true就传token
	// #ifndef MP-ALIPAY
	responseType: 'text',
	// #endif
	// #ifdef H5 || APP-PLUS || MP-ALIPAY || MP-WEIXIN
	timeout: 20000, // H5(HBuilderX 2.9.9+)、APP(HBuilderX 2.9.9+)、微信小程序（2.10.0）、支付宝小程序
	// #endif
	// #ifdef APP-PLUS
	sslVerify: false, // 验证 ssl 证书 仅5+App安卓端支持（HBuilderX 2.3.3+）
	// #endif
	// #ifdef H5
	withCredentials: false, // 跨域请求时是否携带凭证（cookies）仅H5支持（HBuilderX 2.6.15+）
	// #endif
	// 返回当前请求的task, options。请勿在此处修改options。非必填
	getTask: (task, options) => {
		// console.log(task);
		// console.log(options);
		// setTimeout(() => {
		// 	task.abort()
		// }, 500)
	},
	// 自定义验证器。statusCode必存在。非必填
	validateStatus: function validateStatus(statusCode) {
		if (statusCode === 401)
			uni.reLaunch({
				url: '/pages/auth/login'
			})
		return statusCode >= 200 && statusCode < 300
	}
}
const http = new Request(config);
let loading = null;
//请求拦截器
http.interceptors.request.use((request) => { // 可使用async await 做异步操作 
	// here we add auth interceptor  
	const app = getApp({
		allowDefault: true
	});
	if (request.custom.loading)
		uni.showToast({
			title: (request.custom.loadingTxt || "正在请求中.."),
			icon: "loading",
			mask: true,
			duration: 1000 * 20
		});
	if (request.custom.auth) {
		if (app.globalData.userId > 0)
			request.header["Authorization"] = 'Bearer ' + app.globalData.access;
		else
			return Promise.reject(request);
	}
	console.log("请求开始:" + request.url, request);
	return request;
}, error => {
	console.log(error);
	return Promise.reject(error)
})
// 响应拦截器
http.interceptors.response.use((response) => {
	if (response?.config?.custom?.loading)
		uni.hideToast();
	console.log("请求结束:" + response.config.url, response);
	// checkResponse(response);
	let {
		data
	} = response;
	if (data.status) {
		if (data.successMessage)
			uni.showToast({
				title: data.successMessage,
				icon: 'success',
				duration: 2000
			});
		return data;
	} else {
		uni.showToast({
			icon: 'none',
			duration: 2000,
			title: data.message || "请求无效"
		});
		return Promise.resolve(data);
	}
}, (error) => {
	if (error?.config?.custom?.loading)
		uni.hideToast();
	console.log("请求出错:", error);
	if (error?.data?.code === 401) {
		uni.showToast({
			title: '您没有权限访问',
			icon: 'none'
		})
	} else {
		// uni.showToast({
		// 	image: "/static/ui/warning.png",
		// 	duration: 2000,
		// 	title: "出错了."
		// });
	}
	return Promise.reject(error)
})

function checkResponse(res) {
	//刷新token
	if (res.header.GuXin_exp == "1") {
		replaceToken();
	}
}
//动态刷新token
function replaceToken() {
	http.post("/User/Token/Replace").then(x => {
		console.log(x);
		if (x.status) {
			this.commit('user/SetLoginInfo', userInfo);
		} else {
			console.log(x.message);
			uni.reLaunch({
				url: '/pages/auth/login.nvue'
			})
		}
	}).catch(ex => {
		uni.reLaunch({
			url: '/pages/auth/login.nvue'
		})
	});
}
export default http;
