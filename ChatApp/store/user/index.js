import {
	Login,
	UpdateNickName,
	UpdateLocation,
	UpdateSex,
	UpdateRemark,
	UploadAvatar,
	UpdatePhone,
	StatisticQuantity,
	TokenReplace,
	UpdateXinPraise
} from '@/api/user/index.js'

import {
	dbInit
} from '@/api/db.js';
import {
	socketInit,
	socketClose
} from "@/api/request_socket.js"
import {
	isNotNullOrEmpty
} from '@/mypUI/utils/validate.js'
import u from '@/js_sdk/wonyes-checkappupdate/wonyes/checkappupdate.js'
const state = {
	hasLogedIn: false,
	userInfo: {
		userName: "",
		nickName: "",
		avatar: ""
	},
	xinPraiseCount: 0,
	interactionCount: 0,
	contentCount: 0
}
const mutations = {
	SetLoginInfo(state, userInfo) { //填充用户数据   
		state.userInfo = userInfo;
		state.hasLogedIn = true;
		getApp().globalData.access = state.userInfo.token;
		getApp().globalData.userId = state.userInfo.userId;
		getApp().globalData.nickName = state.userInfo.nickName;
		uni.setStorageSync("user", state.userInfo);
	},
	LoginExist(state) {
		let _this = this;
		if (!state.hasLogedIn) {
			var userInfo = uni.getStorageSync('user');
			if (userInfo?.userId > 0)
				_this.commit('user/SetLoginInfo', userInfo);
			else
				return uni.reLaunch({
					url: '/pages/auth/login'
				})
		};
		getApp().globalData.lastLoginTime = new Date();
		if (state.hasLogedIn) {
			this.commit('chat/GetLastChats');
			u.check();
			dbInit();
			socketInit(); //socket初始化 
			UpdateXinPraise();
		}
	},
	SetLoginOut(state, userInfo) { //退出登录   
		state.userInfo = {};
		state.hasLogedIn = false;
		uni.setStorageSync("user", "");
		socketClose();
	}
}
const actions = {
	async LoginAsync({
		state,
		commit
	}, loginUser) {
		return Login(loginUser).then(data => {
			return new Promise(function(resolve, reject) {
				if (data.status) {
					commit('SetLoginInfo', data.data);
					commit('LoginExist');
					resolve(data.data);
				} else
					reject("登陆失败.")
			});
		});
	},
	// async TokenReplaceAsync({
	// 	commit
	// }) {
	// 	TokenReplace().then(res => {
	// 		commit("SetLoginInfo", res.data);
	// 	}).catch(ex => uni.reLaunch({
	// 		url: "/pages/auth/login"
	// 	}))
	// },
	async SetNickNameAsync({
		state,
		commit
	}, nickName) {
		UpdateNickName(nickName).then(data => {
			if (data.status) {
				state.userInfo.nickName = nickName;
				commit('SetLoginInfo', state.userInfo);
			}
		});
	},
	async SetSexAsync({
		state,
		commit
	}, sex) {
		UpdateSex(sex).then(data => {
			if (data.status) {
				state.userInfo.sex = sex;
				commit('SetLoginInfo', state.userInfo);
			}
		});
	},
	async SetLocationAsync({
		state,
		commit
	}, obj) {
		UpdateLocation(obj).then(data => {
			if (data.status) {
				state.userInfo.provine = obj.provine;
				state.userInfo.city = obj.city;
				commit('SetLoginInfo', state.userInfo);
			}
		});
	},
	async SetRemarkAsync({
		state,
		commit
	}, remark) {
		UpdateRemark(remark).then(data => {
			if (data.status) {
				state.userInfo.remark = remark;
				commit('SetLoginInfo', state.userInfo);
			}
		});
	},
	async SetAvatarAsync({
		state,
		commit
	}, localPath) {
		UploadAvatar(localPath).then(data => {
			if (data.status) {
				state.userInfo.avatar = data.data;
				commit('SetLoginInfo', state.userInfo);
			}
		});
	},
	async SetPhoneAsync({
		state,
		commit
	}, userObj) {
		UpdatePhone(userObj.oldPhone, userObj).then(data => {
			if (data.status) {
				state.userInfo.phone = phone;
				commit('SetLoginInfo', state.userInfo);
			}
		});
	},
	async Statistic({
		state,
		commit
	}, localPath) {
		StatisticQuantity().then(data => {
			if (data.status) {
				state.contentCount = data.data.item1;
				state.xinPraiseCount = data.data.item2;
				state.interactionCount = data.data.item3;
			}
		});
	}
}

export default {
	namespaced: true,
	state,
	mutations,
	actions
}
