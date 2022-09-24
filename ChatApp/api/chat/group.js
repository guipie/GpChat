'use strict'

import http from '@/api/request.js';
/*群相关接口*/
export const GetGroups = async (sendObject) => http.get("/group"); //我的群聊
export const GetGroupInfo = async (groupId) => http.get("/group/" + groupId); //群聊详情
export const AddGroup = async (userIds) => http.post("/group", userIds, {
	custom: {
		loading: true,
		loadingTxt: "正在创建.."
	}
}); //创建群聊

export const Quit = async (groupId) => http.delete("/group/Quit/" + groupId, {
	custom: {
		loading: true,
		loadingTxt: "正在退出.."
	}
});

export const AddMembers = async (groupId, userIds) => http.put("/group/" + groupId, userIds, {
	custom: {
		loading: true,
		loadingTxt: "正在添加中.."
	}
}); //添加群聊成员
export const DelMembers = async (groupId, userIds) => http.delete("/group/" + groupId, userIds, {
	custom: {
		loading: true,
		loadingTxt: "正在移除中.."
	}
}); //删除群聊成员

export const SetTop = async (groupId, top) => http.post(`/group/settings/${groupId}?top=${top}`); //是否置顶
export const SetNotDisturb = async (groupId, notDisturb) => http.post(
	`/group/settings/${groupId}?notDisturb=${notDisturb}`); //是否免打扰
export const SetSaveMy = async (groupId, saveMy) => http.post(
	`/group/settings/${groupId}?saveMy=${saveMy}`); //是否保存我的群聊
export const SetAccept = async (groupId, accept) => http.post(
	`/group/settings/${groupId}?accept=${accept}`); //是否免打扰
export const SetGroupNickName = async (groupId, nickName) => http.post(
	`/group/settings/NickName/${groupId}?nickName=${nickName}`); //群昵称设置


export const SetGroupNotice = async (groupId, discription) => http.post(
	`/group/${groupId}?discription=${discription}`); //群名称设置
export const SetGroupName = async (groupId, name) => http.post(
	`/group/${groupId}?name=${name}`); //群描述设置
