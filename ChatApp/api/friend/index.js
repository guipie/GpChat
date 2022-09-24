'use strict'

import http from '@/api/request.js';

export const AddFriend = async (friendUserId, userNickName, addResean) => http.post(
	`/friend/add/${friendUserId}?nickName=${userNickName}&reasean=${addResean}`); //添加好友
// export const TreeFriends = async (key) => http.get("/friend/Tree"); //获取好友列表树
export const SearchUser = async (key) => http.get("/friend/Search/" + key); //搜索用户
export const SearchUserByUserId = async (userId) => http.get("/friend/" + userId); //搜索单个用户

export const GetFriend = async (userId) => http.get("/friend/detail/" + userId); //获取好友设置
export const SetFriendNotDisturb = async (friendUserId, notDisturb) => http.put(
	`/friend/setting/notDisturb/${friendUserId}?notDisturb=${notDisturb}`
); //好友设置免打扰
export const SetFriendTop = async (friendUserId, top) => http.put(
	`/friend/setting/top/${friendUserId}?top=${top}`
); //好友设置置顶
export const SetFriendBlack = async (friendUserId, black) => http.put(
	`/friend/setting/black/${friendUserId}?black=${black}`
); //好友设置拉黑
export const SetFriendBlackGroundImg = async (friendUserId, backGroundImg) => http.put(
	`/friend/setting/backGroundImg/${friendUserId}?black=${backGroundImg}`
); //好友设置聊天背景
export const SetFriendRemark = async (friendUserId, remarkName) => http.put(
	`/friend/setting/remarkName/${friendUserId}?remarkName=${remarkName}`
); //好友设置备注 


export const SearchAllFriendsToMe = async () => http.get("/friend/all/toMe"); //获取所有添加我的好友
export const SearchAllFriendsToOther = async () => http.get("/friend/all/toOther"); //获取所有我添加过的、包括临时好友

export const GetBlackList = async () => http.get("/friend/BlackList"); //获取黑名单balcklist

export const TobeAgreeFriends = async () => http.get("/friend/TobeAgree"); //获取待我同意添加的好友  
export const GetNewestUsers = async (registrationDate) => http.get("/friend/newest?registrationDate=" +
	registrationDate); //最新注册用户 
export const AcceptFriend = async (friendUserId) => http.put("/friend/Accept/" + friendUserId); //同意好友

export const DelFriend = async (userId) => http.delete("/friend/" + userId); //删除好友
