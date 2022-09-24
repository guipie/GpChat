'use strict'

import http from '@/api/request.js';


export const UserInfo = (userId) => http.get("/User/" + userId); //用户信息

/******用户操作配置******/

export const GetLCode = (phone) => http.post("/user/vcode/" + phone + '?code=' + 1, {}, {
	custom: {
		auth: false,
		loading: true,
		loadingTxt: "正在发送.."
	}
}); //登录验证码 
export const GetRCode = (phone) => http.post("/user/vcode/" + phone + '?code=' + 2, {}, {
	custom: {
		auth: false,
		loading: true,
		loadingTxt: "正在发送.."
	}
}); //注册验证码
export const GetUCode = (phone) => http.post("/user/vcode/" + phone + '?code=' + 3, {}, {
	custom: {
		auth: false,
		loading: true,
		loadingTxt: "正在发送.."
	}
}); //修改密码验证码
export const GetCCode = (phone) => http.post("/user/vcode/" + phone + '?code=' + 4, {}, {
	custom: {
		auth: false,
		loading: true,
		loadingTxt: "正在发送.."
	}
}); //改换手机号验证码

export const Login = (userInfo) => http.post("/user/login", userInfo, {
	custom: {
		auth: false,
		loading: true,
		loadingTxt: "正在登录.."
	}
}); //用户登录 
export const Register = (userInfo) => http.post("/user/register", userInfo, {
	custom: {
		auth: false,
		loading: true,
		loadingTxt: "正在注册.."
	}
}); //用户注册
export const UpdatePassWord = async (user) => http.put("/user/Password/", user); //修改密码
export const UpdateSex = async (sex) => http.put("/user/sex/" + sex); //修改性别
export const UpdateLocation = (obj) => http.put("/user/location/" + obj.province + "/" + obj.city); //修改位置
export const UpdateNickName = (nickName) => http.put("/user/nickName/" + nickName); //修改昵称
export const UpdateRemark = (remark) => http.put("/user/remark/" + remark); //修改签名
export const UpdatePhone = (phone, loginObj) => http.put("/user/Phone/" + phone, loginObj); //修改手机号

export function UploadAvatar(filePath) {
	return http.upload('User/Avatar/upload', {
		filePath: filePath, // 要上传文件资源的路径。 
		name: 'avatar', // 文件对应的 key , 开发者在服务器端通过这个 key 可以获取到文件二进制内容
		// #ifdef H5 || APP-PLUS
		timeout: 60000, // H5(HBuilderX 2.9.9+)、APP(HBuilderX 2.9.9+)
		// #endif 
		custom: {
			loading: true,
			loadingTxt: "头像上传中.."
		},
		// 返回当前请求的task, options。请勿在此处修改options。非必填
		getTask: (task, options) => {
			// task.Update((res) => {
			// 	console.log('上传进度' + res.progress);
			// 	console.log('已经上传的数据长度' + res.totalBytesSent);
			// 	console.log('预期需要上传的数据总长度' + res.totalBytesExpectedToSend);
			// 	// // 测试条件，取消上传任务。
			// 	// if (res.progress > 50) {
			// 	// 	uploadTask.abort();
			// 	// }
			// });
		}
	})
}
export const TokenReplace = () => http.post("/user/Token/Replace"); //每次启动替换token

/******用户数据******/
export const StatisticQuantity = (phone) => http.get("/Contents/Statistic"); //动态/新赞/互动数

export const UserQuantity = (userId) => http.get("/user/albumInfo/" + userId); //用户关注粉丝，信息。

export const GetXinPraiseInfo = async () => http.get("/user/xinzan"); //个人新赞统计
export const UpdateXinPraise = async () => http.put("/user/xinzan"); //个人新赞更新
