'use strict'
import {
	isNullOrEmpty
} from '@/mypUI/utils/validate.js'
import {
	baseFileUrl
} from '@/common/env.js'
import {
	GetInteractNotices,
	GetFollowNotices,
	GetSysNotices,
	GetFriendRequestNotices,
	GetAcceptRequestNotices
} from '@/api/notices.js'
import {
	GetInteractSettings,
	GetFollowSettings,
	GetSysNoticeSettings
} from '@/api/notices.js'
/*
#region 内容互动通知操作
        Comment = 24,
        Praise = 25,
        XinPraise = 26
#endregion
#region 好友通知操作
        FriendRequest = 21,
		FriendAccept = 22
#endregion
#region 关注动态
        Follow = 27 
#endregion
*/
const Audio = uni.createInnerAudioContext();
// Audio.autoplay = true;
Audio.src = "/static/resource/msg.wav"; //音频地址  
const state = {
	FriendRequest: {
		UnReadCount: 0
	}, //好友请求 
	noticeSettings: [{ //互动通知
		Avatar: '/static/icon/interaction.png',
		Type: 'interact',
		Name: '互动提醒',
		Content: '暂无互动内容',
		SendTime: new Date(),
		NotDisturb: false,
		IsSys: true,
		UnReadCount: 0
	}, { //关注通知 
		Avatar: '/static/icon/follow.png',
		Type: 'follow',
		Name: '关注的动态',
		Content: '暂无新的动态',
		SendTime: new Date(),
		NotDisturb: false,
		IsSys: true,
		UnReadCount: 0
	}, {
		Avatar: '/static/icon/notice.png',
		Type: 'notice',
		Name: '系统通知',
		Content: '暂无新的通知',
		SendTime: new Date(),
		IsSys: true,
		UnReadCount: 0
	}],
	currentNotice: {}
}
const mutations = {
	SetCurrentNotice(state, detail) {
		state.currentNotice = detail;
	},
	SetNotice(state, notice) {
		if (notice.Type == 21) //好友请求
			this.dispatch("notice/GetFriendRequestNoticesAsync");
		else if (notice.Type == 22) //好友接受请求
			this.dispatch("notice/GetFriendAcceptNoticesAsync");
		else if ([24, 25, 26].indexOf(notice.Type) > -1) //互动动态
			this.dispatch("notice/GetInteractNoticesAsync");
		else if (notice.Type == 27) //关注内容
			this.dispatch("notice/GetFollowNoticesAsync");
		else if (notice.Type == 100) //系统通知
			this.dispatch("notice/GetSysNoticesAsync");
		else
			console.log("无效通知");
	},
	SetNoticeSettings(state) {
		uni.setStorage({
			key: "noticeSettings",
			data: state.noticeSettings
		})
	},
	ClearUnReadNotice(state, type) {
		state.noticeSettings.find(function(m) {
			if (m.Type == type)
				m.UnReadCount = 0;
		});
	},
	ClearUnReadFriendRequest(state) {
		state.FriendRequest.UnReadCount = 0;
	}
}

const actions = {
	async GetInteractSettingAsync({ //获取互动通知配置信息
		commit,
		state,
		dispatch
	}, sendObj) {
		let interactIndex = state.noticeSettings.findIndex(m => m.Type == 'interact');
		if (state.noticeSettings[interactIndex].Id > 0)
			return;
		uni.getStorage({
			key: "noticeSettings",
			success: (res) => {
				state.noticeSettings = res.data;
			},
			complete: () => {
				if (!(state.noticeSettings[interactIndex].Id > 0))
					GetInteractSettings().then(res => {
						state.noticeSettings.splice(interactIndex, 1, {
							...state.noticeSettings[interactIndex],
							...res.data
						});
						commit("SetNoticeSettings");
					});
				dispatch("GetInteractNoticesAsync");
			}
		});
	},
	async GetFollowSettingAsync({ //获取关注动态配置信息
		commit,
		state,
		dispatch
	}, sendObj) {
		let followIndex = state.noticeSettings.findIndex(m => m.Type == 'follow');
		if (state.noticeSettings[followIndex].Id > 0)
			return;
		uni.getStorage({
			key: "noticeSettings",
			success: (res) => {
				state.noticeSettings = res.data;
			},
			complete: () => {
				if (!(state.noticeSettings[followIndex].Id > 0))
					GetFollowSettings().then(res => {
						state.noticeSettings.splice(followIndex, 1, {
							...state.noticeSettings[followIndex],
							...res.data
						});
						commit("SetNoticeSettings");
					});
				dispatch("GetFollowNoticesAsync");
			}
		});
	},
	async GetSysNoticeSettingAsync({ //获取系统通知配置信息
		commit,
		state,
		dispatch
	}, sendObj) {
		let sysIndex = state.noticeSettings.findIndex(m => m.Type == 'notice');
		if (state.noticeSettings[sysIndex].Id > 0)
			return;
		uni.getStorage({
			key: "noticeSettings",
			success: (res) => {
				state.noticeSettings = res.data;
			},
			complete: () => {
				if (!(state.noticeSettings[sysIndex].Id > 0))
					GetSysNoticeSettings().then(res => {
						state.noticeSettings.splice(sysIndex, 1, {
							...state.noticeSettings[sysIndex],
							...res.data
						});
						commit("SetNoticeSettings");
					});
				dispatch("GetSysNoticesAsync");
			}
		});
	},
	async GetInteractNoticesAsync({
		state
	}) {
		GetInteractNotices().then(res => {
			let {
				Count,
				Content,
				SendTime
			} = res.data;
			if (Count > 0) {
				let interactIndex = state.noticeSettings.findIndex(m => m.Type == 'interact');
				state.noticeSettings[interactIndex].UnReadCount += Count;
				state.noticeSettings[interactIndex].Content = Content;
				state.noticeSettings[interactIndex].SendTime = SendTime;
				Audio.play();
			}
		});
	},
	async GetFollowNoticesAsync({
		state,
		commit
	}) {
		GetFollowNotices().then(res => {
			let {
				Count,
				Content,
				SendTime
			} = res.data;
			if (Count > 0) {
				let followIndex = state.noticeSettings.findIndex(m => m.Type == 'follow');
				state.noticeSettings[followIndex].UnReadCount += Count;
				state.noticeSettings[followIndex].Content = Content;
				state.noticeSettings[followIndex].SendTime = SendTime;
				Audio.play();
			}
		});
	},
	async GetFriendRequestNoticesAsync({
		state,
		commit
	}) {
		GetFriendRequestNotices().then(res => {
			let {
				Count,
				Content,
				SendTime
			} = res.data;
			if (Count > 0) {
				state.FriendRequest.UnReadCount += Count;
				state.FriendRequest.Content = Content;
				state.FriendRequest.SendTime = SendTime;
				this.commit("tabBar/SetNewsTabBar", state.FriendRequest.UnReadCount);
			}
		});
	},
	async GetSysNoticesAsync({
		state,
		commit
	}) {
		GetSysNotices().then(res => {
			let {
				Count,
				Content,
				SendTime
			} = res.data;
			if (Count > 0) {
				let sysIndex = state.noticeSettings.findIndex(m => m.Type == 'notice');
				state.noticeSettings[sysIndex].UnReadCount += Count;
				state.noticeSettings[sysIndex].Content = Content;
				state.noticeSettings[sysIndex].SendTime = SendTime;
				Audio.play();
			}
		});
	},
	async GetFriendAcceptNoticesAsync() //如果接受了好友，判断当前是否在聊天，如果有设置好友状态 
	{
		GetAcceptRequestNotices().then(res => {
			let {
				Count,
				SendUserId
			} = res.data;
			if (Count > 0) {
				this.dispatch("friend/GetFriendsAsync");
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
