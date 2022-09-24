import request from '../../api/request.js'
import {
	isNullOrEmpty,
	isNullOrEmptyToStr
} from '@/mypUI/utils/validate.js'
import {
	baseFileUrl
} from '@/common/env.js'
import {
	executeSQL,
	selectSQL
} from '@/api/db.js'
import {
	SearchAllFriendsToOther,
	GetFriend,
	SetFriendRemark,
	SetFriendTop,
	SetFriendNotDisturb,
	SetFriendBlackGroundImg,
	SetFriendBlack,
	DelFriend
} from '@/api/friend/index.js'
const state = {
	allFriends: [], //所有好友列表
	friends: [], //通讯录
	addressBook: [], //通讯录
	followUsers: [] //关注好友  
}
const mutations = {
	// SetFriends(state, friends) {
	// 	state.treeFriends = friends;
	// } 
	SetFriend(state, friend) {
		var insertSql =
			`insert into [friend]([FriendUserId], [FriendRemarkName], [Avatar], [Status], [Top], [NotDisturb], [BackgroundImage], [CreateDate], [ModifyDate]) select ${friend.FriendUserId},'${friend.FriendRemarkName}','${friend.Avatar}',${friend.Status},${(friend.Top?1:0)},${(friend.NotDisturb?1:0)},'${friend.BackgroundImage}','${friend.CreateDate}','${(friend.ModifyDate?friend.ModifyDate:"")}' where not exists (select * from [friend] where [FriendUserId]=${friend.FriendUserId})`;
		executeSQL(insertSql);
		if (state.allFriends.findIndex(m => m.FriendUserId == friend.FriendUserId) == -1)
			state.allFriends.push(friend);
	},
	DelFrinend(state, userId) {
		executeSQL(`delete from friend where FriendUserId=${userId}`);
		// executeSQL(`delete from chat where SendUserId=${userId} or ReceiveUserId=${userId}`);
		this.commit("chat/ClearChats", userId);
		state.addressBook.forEach(m => {
			let delIndex = m.value.findIndex(f => f.FriendUserId == userId);
			if (delIndex > -1)
				m.value.splice(delIndex, 1);
		});
	}
}

const actions = {
	async GetFriendsAsync({
		state,
		commit
	}) {
		executeSQL("delete from [friend]");
		return SearchAllFriendsToOther().then(res => {
			state.allFriends = res.data;
			state.friends = res.data.filter(m => m.Status == 1);
			state.addressBook = [];
			state.friends.forEach(m => {
				let firstPy = m.PinYin.substr(0, 1).toUpperCase();
				var currentIndex = state.addressBook.findIndex(x => x.key === firstPy);
				if (currentIndex > -1)
					state.addressBook[currentIndex].value.push(m);
				else
					state.addressBook.push({
						key: firstPy,
						value: [m]
					});
				commit("SetFriend", m);
			});
			return new Promise(function(resolve, reject) {
				if (res.status) {
					resolve(res.data);
				} else
					reject("获取好友列表失败.")
			});
		});
	},
	async GetFriendDetailAsync({
		state,
		commit,
		dispatch
	}, userId) {
		return new Promise(function(resolve, reject) {
			let friend = state.allFriends.find(m => m.FriendUserId == userId);
			if ((friend || {}).FriendUserId > 0)
				resolve(friend)
			else
				GetFriend(userId).then(res => {
					let data = res.data;
					// if (data.Status == 1)
					// 	dispatch("GetFriendsAsync"); //是好友，就重置好友列表 
					commit("SetFriend", data);
					resolve(data);
				}).catch(err => {
					console.log(err);
					reject({});
				});
		});
	},
	async DelFriendAsync({ //删除好友
		rootState,
		commit
	}, userId) {
		return new Promise(function(resolve, reject) {
			DelFriend(userId).then(res => {
				commit("DelFrinend", userId);
				resolve(res);
			}).catch(err => {
				reject(err)
			});
		});
	},
	async SetFriendTopAsync({
		rootState,
		commit,
		dispatch
	}, {
		userId,
		top
	}) {
		SetFriendTop(userId, top).then(res => {
			executeSQL([
				"update last_chat set Top=" + (top ? 1 : 0) + " where FriendUserId=" +
				userId,
				"update friend set Top=" + (top ? 1 : 0) + " where FriendUserId=" + userId
			]);
			let chatIndex = rootState.chat.lastChat.findIndex(m => m.FriendUserId == userId);
			if (chatIndex > -1) {
				rootState.chat.lastChat[chatIndex].Top = top;
				rootState.chat.lastChat.splice(chatIndex, 1, rootState.chat.lastChat[chatIndex]);
			}
			let friend = rootState.friend.allFriends.find(m => m.FriendUserId == userId);
			friend.Top = top;
			// dispatch("GetFriendsAsync"); //重置好友列表 
		});
	},
	async SetFriendNotDisturbAsync({
		rootState,
		commit,
		dispatch
	}, {
		userId,
		notDisturb
	}) {
		SetFriendNotDisturb(userId, notDisturb).then(res => {
			executeSQL([
				"update last_chat set NotDisturb=" + (notDisturb ? 1 : 0) +
				" where FriendUserId=" +
				userId,
				"update friend set NotDisturb=" + (notDisturb ? 1 : 0) +
				" where FriendUserId=" + userId
			]);
			let lastChat = rootState.chat.lastChat.find(m => m.FriendUserId == userId);
			lastChat && (lastChat.NotDisturb = notDisturb);
			let friend = rootState.friend.allFriends.find(m => m.FriendUserId == userId);
			friend && (friend.NotDisturb = notDisturb);
			// dispatch("GetFriendsAsync"); //重置好友列表 
		});
	},
	async SetFriendBlackAsync({
		rootState,
		commit,
		dispatch
	}, {
		userId,
		black
	}) {
		SetFriendBlack(userId, black).then(res => {
			dispatch("GetFriendsAsync"); //重置好友列表  
		});
	},
	async SetFriendRemarkAsync({
		rootState,
		commit,
		dispatch
	}, {
		userId,
		remarkName
	}) {
		SetFriendRemark(userId, remarkName).then(res => {
			dispatch("GetFriendsAsync"); //重置好友列表 
			executeSQL([
				"update last_chat set Name='" + remarkName + "' where FriendUserId=" +
				userId
			]);
			let lastChat = rootState.chat.lastChat.find(m => m.FriendUserId == userId);
			lastChat && (lastChat.Name = remarkName);
		});
	},
}
export default {
	namespaced: true,
	state,
	mutations,
	actions
}
