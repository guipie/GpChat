'use strict'

import http from '@/api/request.js';


export const MyFriendAlbums = async (date) => http.get("/contents/friend?searchDate=" + date); //我的朋友圈
export const MyFriendPhotos = async (date) => http.get("/contents/friend/photos?searchDate=" + date); //我的朋友圈
export const UserFriendAlbums = async (userId) => http.get("/contents/friend/" + userId); //好友的朋友圈

export const FriendComments = async (contentGuid, searchDate) => http.get("/contents/Comment/Friend/" + contentGuid, {
	searchDate: selectDate
}); //朋友圈好友评论列表
export const FriendPraises = async (userId) => http.get("/contents/Praise/Friend/" + contentGuid, {
	searchDate: selectDate
}); //朋友圈点赞列表

export const FriendCommentsPost = async (contentGuid, comments, parentId) => http.post("/contents/Comment/Friend/" +
	ontentGuid, {
		comments: selectDate,
		parentId: parentId
	}); //朋友圈评论

export const FriendPraisesPost = async (contentGuid) => http.post("/contents/Praise/Friend/" +
	contentGuid); ///朋友圈点赞
