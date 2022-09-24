import {
	isNullOrEmpty,
	isNum
} from '@/mypUI/utils/validate.js'
import {
	parseTime
} from '@/mypUI/utils/date.js'

import {
	GetChatFriendInfo
} from '@/api/friend/index.js'
import {
	executeSQL,
	executeSQLTran,
	selectSQL
} from '@/api/db.js'
import {
	baseFileUrl
} from '@/common/env.js'
import {
	SendChat,
	SendChatToMe,
	SendGroupChat,
	SendGroupChatToMe
} from '@/api/chat/index.js'

import {
	UserInfo
} from '@/api/user/index.js'
import {
	encrypt,
	decrypt
} from '@/js_sdk/crypto/index.js'
const Audio = uni.createInnerAudioContext();
// Audio.autoplay = true;
Audio.src = "/static/resource/msg.wav"; //音频地址  
const state = {
	isGroup: false,
	hasMore: false,
	searchDate: "",
	chatPaging: 1,
	chattingGroup: {}, //当前聊天群
	lastChat: [], //所有聊天记录最后一条
	currentChat: [], //当前窗口聊天记录 
	chattingUser: {} //当前正在聊天的用户{userId,nickName,avatar}
}
const mutations = {
	UpdateFriend(state, {
		nickName,
		avatar,
		userId
	}) {
		var updateSql = [];
		updateSql[0] =
			`update friend set Avatar='${avatar}',FriendRemarkName='${nickName}' where FriendUserId=${userId};`;
		updateSql[1] =
			`update chat_group_member set Avatar='${avatar}',NickName='${nickName}' where UserId=${userId};`;
		updateSql[2] =
			`update last_chat set Avatar='${avatar}',Name='${nickName}' where FriendUserId=${userId};`;
		executeSQL(updateSql);
		let index = state.lastChat.findIndex(m => m.FriendUserId == userId && m.IsGroup == 0);
		if (index > -1) {
			state.lastChat[index].Avatar = avatar;
			state.lastChat[index].Name = nickName;
		}
		if (state.chattingUser.FriendUserId == userId) {
			state.chattingUser.FriendRemarkName = nickName;
			state.chattingUser.Avatar = avatar;
		}
	},
	SetChattingUserLocal(state, user) {
		state.isGroup = false;
		state.chattingUser = {
			...state.chattingUser,
			...user
		};
	},
	SetChattingGroupLocal(state, group) {
		state.isGroup = true;
		state.chattingGroup = {
			...state.chattingGroup,
			...group
		};
	},
	SetChattingUser(state, user) {
		state.isGroup = false;
		state.chattingUser = user;
		if (user.searchDate) {
			state.searchDate = user.searchDate;
			selectSQL(
				`select * from chat where IsGroup=0 and SendTime>='${user.searchDate}' and (SendUserId=${user.FriendUserId} or ReceiveUserId=${user.FriendUserId}) order by SendTime desc`,
				data => {
					state.hasMore = data.length >= 20;
					state.chatPaging++;
					if (state.hasMore)
						state.currentChat = data;
					else
						state.currentChat = data.sort((a, b) => {
							return (new Date(a.SendTime).getTime()) - (new Date(b.SendTime)
								.getTime());
						});
					uni.navigateTo({
						url: "/pages/chat/chat?page=" + user.page //如果从现有已经聊的相关进来的需要不重置
					});
				});
		} else {
			this.dispatch("chat/GetCurrentChatsAsync");
			uni.navigateTo({
				url: "/pages/chat/chat?page=" + (user.page || '')
			});
		}

	},
	SetChattingGroup(state, {
		groupId,
		isClose,
		page,
		searchDate
	}) {
		state.isGroup = true;
		this.dispatch("group/GetGroupDetailAsync", groupId).then(group => {
			state.chattingGroup = group;
			state.chattingGroup.FriendUserId = group.GroupId;
			if (searchDate) {
				state.searchDate = searchDate;
				selectSQL(
					`select * from chat where IsGroup=1 and SendTime>='${searchDate}' and  ReceiveUserId=${groupId} order by SendTime desc`,
					data => {
						state.hasMore = data.length >= 20;
						state.chatPaging++;
						if (state.hasMore)
							state.currentChat = data;
						else
							state.currentChat = data.sort((a, b) => {
								return (new Date(a.SendTime).getTime()) - (new Date(b.SendTime)
									.getTime());
							});
						uni.navigateTo({
							url: "/pages/chat/chat?page=" + page //如果从搜索进来的需要不重置
						});
					});
			} else {
				this.dispatch("chat/GetCurrentChatsAsync");
				if (isClose)
					uni.redirectTo({
						url: "/pages/chat/chat"
					})
				else
					uni.navigateTo({
						url: "/pages/chat/chat?page=" + page
					});
			}
		}).catch(err => {
			console.log(err);
			uni.showToast({
				title: '此群已不存在'
			})
		});
	},
	ResetCurrent(state) { //重置当前聊天 
		state.currentChat = [];
		state.chattingUser = {};
		state.chattingGroup = {};
		state.chatPaging = 1;
		state.searchDate = "";
		state.isGroup = false;
	},
	// SendMsg(state, sendObj) { //  
	// 	if (state.isGroup) sendObj.SenderAvatar == state.chattingGroup.Avatar;
	// 	else sendObj.SenderAvatar = state.chattingUser.Avatar;
	// 	if (state.hasMore)
	// 		state.currentChat.unshift(sendObj);
	// 	else
	// 		state.currentChat.push(sendObj);
	// },
	ClearChats(state, userId) { //清空删除当前好友聊天记录
		executeSQL(
			`delete from chat where (SendUserId=${userId} or ReceiveUserId=${userId}) and IsGroup=0`
		);
		executeSQL(`delete from last_chat where FriendUserId=${userId} and IsGroup=0`);
		let chatIndex = state.lastChat.findIndex(m => m.FriendUserId == userId && m.IsGroup == 0);
		(chatIndex > -1) && (state.lastChat.splice(chatIndex, 1));
		(state.chattingUser.FriendUserId == userId) && (state.currentChat = []);
	},
	ClearGroupChats(state, groupId) { //清空删除当前群聊天记录
		executeSQL(`delete from chat where  ReceiveUserId=${groupId} and IsGroup=1`);
		executeSQL(`delete from last_chat where FriendUserId=${groupId} and IsGroup=1`);
		let chatIndex = state.lastChat.findIndex(m => m.FriendUserId == groupId && m.IsGroup == 1);
		(chatIndex > -1) && (state.lastChat.splice(chatIndex, 1));
		(state.chattingGroup.GroupId == groupId) && (state.currentChat = []);
	},
	ReceiveChat(state, receiveObj) { //在线接收消息
		receiveObj.Content = decrypt(receiveObj.Content);
		let _this = this;
		receiveObj.FileInfo = JSON.stringify(receiveObj.FileInfo || "");
		const insertMsgSql =
			`INSERT INTO chat(SendUserId,ReceiveUserId,Content,Type,SendTime,IsGroup,FileInfo) VALUES 
	        (${receiveObj.SendUserId}, ${receiveObj.ReceiveUserId},'${receiveObj.Content}',${receiveObj.Type},'${receiveObj.SendTime}',${receiveObj.IsGroup},'${receiveObj.FileInfo}')`;
		executeSQL(insertMsgSql);
		if (state.chattingUser.FriendUserId == receiveObj.SendUserId && !state.isGroup) {
			if (state.hasMore)
				state.currentChat.unshift(receiveObj);
			else
				state.currentChat.push(receiveObj);
		} else if (state.chattingGroup.GroupId == receiveObj.ReceiveUserId && state.isGroup) {
			if (state.hasMore)
				state.currentChat.unshift(receiveObj);
			else
				state.currentChat.push(receiveObj);
		} else if (receiveObj.Online) {
			receiveObj.FriendUserId = receiveObj.IsGroup == 1 ? receiveObj.ReceiveUserId :
				receiveObj
				.SendUserId;
			_this.commit("chat/SetLastChat", receiveObj);
			_this.commit("tabBar/SetChatTabBar", 1);
		}
	},
	SetLastChat(state, chatObj) { //设置最后一条聊天  
		let lastChatIndex = lastChatIndex = state.lastChat.findIndex(m => m.FriendUserId == chatObj
			.FriendUserId &&
			m.IsGroup == chatObj.IsGroup);
		if (lastChatIndex > -1) {
			let lastObj = state.lastChat[lastChatIndex];
			lastObj.SendTime = chatObj.SendTime;
			lastObj.Content = chatObj.Content;
			lastObj.Type = chatObj.Type;
			// lastObj.NotDisturb = state.isGroup ? state.chattingGroup.NotDisturb : state.chattingUser.NotDisturb;
			// lastObj.Top = state.isGroup ? state.chattingGroup.Top : state.chattingUser.Top;
			if (chatObj.UnReadCount === -1)
				lastObj.UnReadCount = 0;
			else {
				lastObj.UnReadCount = (lastObj.UnReadCount || 0) + 1;
				lastObj.NotDisturb != 1 && Audio.play();
			}
			state.lastChat.splice(lastChatIndex, 1, lastObj);
		} else {
			if (chatObj.IsGroup === 1)
				this.dispatch("group/GetGroupDetailAsync", chatObj.FriendUserId).then(group => {
					chatObj.FriendRemarkName = group.Name;
					chatObj.Avatar = (group.Members || []).slice(0, 9).map(m => m.Avatar)
						.toString();
					chatObj.Name = group.Name;
					chatObj.Top = group.Top;
					chatObj.NotDisturb = group.NotDisturb;
					if (chatObj.UnReadCount === -1)
						chatObj.UnReadCount = 0;
					else {
						group.NotDisturb != 1 && Audio.play();
						chatObj.UnReadCount = chatObj.UnReadCount || 1;
					}
					state.lastChat.unshift(chatObj);
				}).catch(err => console.log(err));
			else
				this.dispatch("friend/GetFriendDetailAsync", chatObj.FriendUserId).then(friend => {
					chatObj.Avatar = friend.Avatar;
					chatObj.FriendRemarkName = friend.FriendRemarkName;
					chatObj.Avatar = friend.Avatar;
					chatObj.Name = friend.FriendRemarkName;
					chatObj.Top = friend.Top;
					chatObj.NotDisturb = friend.NotDisturb;
					if (chatObj.UnReadCount === -1)
						chatObj.UnReadCount = 0;
					else {
						friend.NotDisturb != 1 && Audio.play();
						chatObj.UnReadCount = chatObj.UnReadCount || 1;
					}
					state.lastChat.unshift(chatObj);
				}).catch(err => console.log(err));
		}
	},
	SetLastSendChat(state, lastObj) {
		if (state.isGroup && state.chattingGroup.FriendUserId)
			lastObj.FriendUserId = state.chattingGroup.FriendUserId
		else if (!state.isGroup && state.chattingUser.FriendUserId)
			lastObj.FriendUserId = state.chattingUser.FriendUserId;
		lastObj.UnReadCount = -1;
		this.commit("chat/SetLastChat", lastObj);
	},
	SetLastChatToDb(state) { // 设置最后一条聊天到数据库  
		let lastChats = state.lastChat.filter(m => parseTime(m.SendTime) > parseTime(getApp().globalData
					.lastLoginTime) || m
				.IsOpened)
			.forEach(m => {
				console.log(m);
				let lastSql = new Array();
				lastSql.push(
					`delete FROM last_chat where IsGroup=${m.IsGroup?1:0} and FriendUserId=${m.FriendUserId}`
				)
				lastSql.push(
					`INSERT INTO last_chat(FriendUserId,Avatar,Name,Content,UnReadCount,Type,IsGroup,SendTime,Top,NotDisturb) VALUES 
					(${m.FriendUserId},'${m.Avatar}','${m.Name}','${m.Content}',${m.UnReadCount},${m.Type},${m.IsGroup},'${m.SendTime}',${(m.Top?1:0)},${(m.NotDisturb?1:0)})`
				);
				executeSQL(lastSql);
			});
		console.log('插入最后记录...');
	},
	GetLastChats(state) { //获取最后一条聊天 
		const lastChatSql = 'select * from last_chat order by Top desc,SendTime desc';
		selectSQL(lastChatSql, (data) => {
			state.lastChat = data;
		}, (err) => {
			console.log(err);
		});
	}
}

const actions = {
	async SendMsgAsync({ //异步发送消息
		commit,
		state,
		rootState
	}, sendObj) {
		sendObj.SendTime = parseTime(new Date());
		// if (sendObj.ReceiveUserId != state.chattingUser.FriendUserId && !state.isGroup) return;
		// else if (sendObj.ReceiveUserId != state.chattingGroup.GroupId && state.isGroup) return;
		// else {
		console.log(sendObj);
		const insertMsgSql =
			`INSERT INTO chat(SendUserId,ReceiveUserId,Content,Type,SendTime,IsGroup,FileInfo) VALUES 
					 (${sendObj.SendUserId}, ${sendObj.ReceiveUserId},'${sendObj.Content}',${sendObj.Type},'${sendObj.SendTime}',${sendObj.IsGroup},'${JSON.stringify(sendObj.FileInfo||'')}')`;
		executeSQL(insertMsgSql, (res) => {
			sendObj.Content = encrypt(sendObj.Content);
			if (sendObj.IsGroup == 1)
				SendGroupChat(sendObj); //服务器推送群消息 
			else
				SendChat(sendObj); //服务器推送消息
		}).catch(err => console.log(err));
		// }
	},
	async SendMsgReceiveAsync({ //异步发送消息,无回执
		commit,
		state
	}, sendObj) {
		sendObj.Content = encrypt(sendObj.Content);
		sendObj.SendTime = parseTime(new Date());
		SendChatToMe(sendObj); //服务器推送消息
	},
	async SendGroupChatToMeAsync({ //异步发送消息,无回执
		commit,
		state
	}, sendObj) {
		sendObj.Content = encrypt(sendObj.Content);
		sendObj.SendTime = parseTime(new Date());
		SendGroupChatToMe(sendObj); //服务器推送消息
	},
	async GetCurrentChatsAsync({
		state
	}) { //获取当前聊天记录及分页 
		let filter = ` SendTime<'${(state.searchDate||new Date())}'`;

		if (state.isGroup) {
			filter += ` and IsGroup=1 and ReceiveUserId=${state.chattingGroup.GroupId}`;
		} else {
			if (getApp().globalData.userId === state.chattingUser.FriendUserId)
				filter +=
				`and (SendUserId=${state.chattingUser.FriendUserId} and ReceiveUserId=${state.chattingUser.FriendUserId}) and IsGroup=0`;
			else
				filter +=
				`and (SendUserId=${state.chattingUser.FriendUserId} or ReceiveUserId=${state.chattingUser.FriendUserId}) and IsGroup=0`;
		}
		let chatSql =
			`select * from chat where ${filter} order by SendTime desc limit 0,20`;
		return new Promise((resolve, reject) => {
			selectSQL(chatSql, (data) => {
				if (data.length == 0)
					return resolve([]);
				state.searchDate = data[data.length - 1].SendTime;
				if (state.chatPaging == 1) {
					state.hasMore = data.length >= 20;
					if (state.hasMore)
						state.currentChat = data;
					else
						state.currentChat = data.sort((a, b) => {
							return (new Date(a.SendTime).getTime()) - (new Date(b.SendTime)
								.getTime());
						});
				} else {
					state.currentChat.push(...data);
				}
				state.chatPaging++;
				return resolve(data);
			}, error => {
				console.log(error);
				return resolve([]);
			})
		});
	}
}
export default {
	namespaced: true,
	state,
	mutations,
	actions
}
