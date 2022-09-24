import {
	isNullOrEmpty,
	isNum
} from '@/mypUI/utils/validate.js'
import {
	baseFileUrl
} from '@/common/env.js'
import {
	GetGroups,
	GetGroupInfo,
	SetGroupNotice, //群名称
	SetGroupNickName,
	SetTop,
	SetNotDisturb,
	SetSaveMy,
	SetAccept,
	DelMembers,
	Quit
} from '@/api/chat/group.js'
import {
	executeSQL,
	selectSQL
} from '@/api/db.js'
const state = {
	groups: [], //群列表
}
const mutations = {
	SetGroup(state, group) {
		let myUserId = getApp().globalData.userId;
		var insertSqls = [
			`insert or ignore into [chat_group]([GroupId], [Name], [Avatar], [SaveGroup],[AcceptAdd], [Top], [NotDisturb], [BackgroundImage],Description, [CreateDate],CreateID, [ModifyDate]) values(${group.GroupId},'${group.Name}','${group.Avatar}',${group.SaveGroup?1:0},${group.AcceptAdd?1:0},${(group.Top?1:0)},${(group.NotDisturb?1:0)},'${group.BackgroundImage}','${group.Description}','${group.CreateDate}',${group.CreateID},'${(group.ModifyDate?group.ModifyDate:"")}')`
		];
		group.Members.forEach(m => {
			insertSqls.push(
				`insert or ignore into [chat_group_member]([GroupId],[UserId],[NickName],[Avatar],[CreateDate]) values(${group.GroupId},${m.UserId},'${m.NickName}','${m.Avatar}','${m.CreateDate}')`
			)
		});
		executeSQL(insertSqls);
	},
	SetGroupMembers(state, {
		members,
		groupId
	}) {
		let insertSqls = [];
		members.forEach(m => {
			insertSqls.push(
				`insert or ignore into [chat_group_member]([GroupId],[UserId],[NickName],[Avatar],[CreateDate]) values(${groupId},${m.UserId},'${m.NickName}','${m.Avatar}','${new Date()}')`
			)
		});
		executeSQL(insertSqls);
		let groupIndex = state.groups.findIndex(m => m.GroupId == groupId);
		if (groupIndex > -1) {
			state.groups[groupIndex].Members.push(...members);
		}
	}
}

const actions = {
	async GetGroupsAsync({ //获取群  
		commit,
		state
	}) {
		return new Promise((resolve, reject) => {
			return GetGroups().then(res => {
				state.groups = res.data;
				res.data.forEach(m => {
					commit("SetGroup", m)
				});
				resolve(res.data);
			});
		});
	},
	async GetGroupDetailAsync({
		state,
		commit
	}, groupId) {
		return new Promise(function(resolve, reject) {
			let groupIndex = state.groups.findIndex(m => m.GroupId == groupId);
			if (groupIndex > -1) {
				if ((state.groups[groupIndex].Members || []).length > 0)
					return resolve(state.groups[groupIndex]);
				else
					state.groups.splice(groupIndex, 1);
			}
			return GetGroupInfo(groupId).then(data => {
				if (data.status) {
					executeSQL(["delete from [chat_group] where GroupId=" + groupId,
						"delete from [chat_group_member] where GroupId=" +
						groupId
					]);
					commit("SetGroup", data.data);
					state.groups.push(data.data);
					resolve(data.data);
				} else
					reject(data)
			});
		});
	},
	async SetGroupDescAsync({
		state
	}, {
		groupId,
		desc
	}) {
		SetGroupNotice(groupId, desc).then(res => {
			executeSQL("update [chat_group] set Description='" + desc + "' where GroupId=" +
				groupId);
			let chatIndex = rootState.group.groups.findIndex(m => m.GroupId == groupId);
			if (chatIndex > -1)
				rootState.group.groups[chatIndex].Description = desc;
		})
	},
	async SetGroupTopAsync({
		rootState
	}, {
		groupId,
		top
	}) {
		SetTop(groupId, top).then(res => {
			executeSQL([
				"update [chat_group] set Top=" + (top ? 1 : 0) + " where GroupId=" +
				groupId,
				"update [last_chat] set Top=" + (top ? 1 : 0) +
				" where IsGroup=1 and FriendUserId=" + groupId
			], res => {
				let chatIndex = rootState.group.groups.findIndex(m => m.GroupId ==
					groupId);
				if (chatIndex > -1)
					rootState.group.groups[chatIndex].NotDisturb = notDisturb;
			});
		});
	},
	async SetGroupNotDisturbAsync({
		rootState
	}, {
		groupId,
		notDisturb
	}) {
		SetNotDisturb(groupId, notDisturb).then(res => {
			executeSQL([
				"update [chat_group] set NotDisturb=" + (notDisturb ? 1 : 0) +
				" where GroupId=" +
				groupId,
				"update [last_chat] set NotDisturb=" + (notDisturb ? 1 : 0) +
				" where IsGroup=1 and FriendUserId=" + groupId
			]);
			let chatIndex = rootState.group.groups.findIndex(m => m.GroupId == groupId);
			if (chatIndex > -1)
				rootState.group.groups[chatIndex].NotDisturb = notDisturb;
		})
	},
	async SetGroupSaveMyAsync({ //保存到我的群聊
		rootState
	}, {
		groupId,
		saveMy
	}) {
		SetSaveMy(groupId, saveMy).then(res => {
			executeSQL("update [chat_group] set SaveGroup=" + (saveMy ? 1 : 0) +
				" where GroupId=" + groupId);
			let chatIndex = rootState.group.groups.findIndex(m => m.GroupId == groupId);
			if (chatIndex > -1)
				rootState.group.groups[chatIndex].SaveGroup = saveMy;
		})
	},
	async SetGroupAcceptAsync({ //保存到我的群聊
		rootState
	}, {
		groupId,
		accept
	}) {
		SetAccept(groupId, accept).then(res => {
			executeSQL("update [chat_group] set AcceptAdd=" + (accept ? 1 : 0) +
				" where GroupId=" + groupId);
			let chatIndex = rootState.group.groups.findIndex(m => m.GroupId == groupId);
			if (chatIndex > -1)
				rootState.group.groups[chatIndex].AcceptAdd = accept;
		})
	},
	async SetGroupMyNickAsync({ //保存到我的群聊
		rootState
	}, {
		groupId,
		nickName
	}) {
		SetGroupNickName(groupId, nickName).then(res => {
			let myUserId = getApp().globalData.userId;
			executeSQL("update [chat_group_member] set NickName='" + nickName +
				"' where GroupId=" + groupId + ' and UserId=' + myUserId);
			let chatIndex = rootState.group.groups.findIndex(m => m.GroupId == groupId);
			if (chatIndex > -1) {
				let memIndex = rootState.group.groups[chatIndex].Memebers.findIndex(m => m
					.GroupId ==
					groupId && m.UserId == myUserId);
				rootState.group.groups[chatIndex].Memebers[memIndex].NickName = nickName;
			}
		})
	}
}
export default {
	namespaced: true,
	state,
	mutations,
	actions
}
