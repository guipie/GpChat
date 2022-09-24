'use strict'

import http from '@/api/request.js';


export const Post = async (content) => http.post("/contents", content); //发布内容/朋友圈
export const Put = async (content) => http.put("/contents", content); //修改内容 
/*
公共内容相关
*/
export const GetNewestAlbums = async (date) => http.get("/contents/Init?searchDate=" + date); //获取最新内容 
export const GetFollowAlbums = async (date) => http.get("/contents/Follow?searchDate=" + date); //获取关注内容
export const GetRecommendAlbums = async (date) => http.get("/contents/Recommend?searchDate=" + date); //获取推荐内容
export const GetTopAlbums = async (date) => http.get("/contents/Top?searchDate=" + date); //获取榜单内容 
export const GetAlbumDetail = async (contentGuid) => http.get("/contents/" + contentGuid); //获取内容详情
export const DelAlbumDetail = async (contentGuid) => http.delete("/contents/" + contentGuid); //删除内容详情
export const GetEditAlbumDetail = async (contentGuid) => http.get("/contents/edit/" + contentGuid); //获取编辑内容详情
/*
用户公共内容相关
*/
export const GetUserAlbums = async (userId, date) => http.get("/contents/user/" + userId + '?dateTime=' +
	date, {
		custom: {
			loading: true
		}
	}); //获取用户全部故新内容 
export const GetUserTopAlbums = async (userId, date) => http.get("/contents/user/top/" + userId + '?dateTime=' +
	date, {
		custom: {
			loading: true
		}
	}); //获取用户优质故新内容


export const GetAlbumCollect = async (date) => http.get("/contents/Collect?searchDate=" + date); //获取用户收藏内容  

export const AlbumCollect = async (guid) => http.post("/contents/Collect/" + guid); //用户收藏内容  
export const AlbumPraise = async (guid) => http.post("/contents/Praise/" + guid); //用户点赞内容
export const AlbumXinPraise = async (guid) => http.post("/contents/XinPraise/" + guid); //用户新赞内容
export const AlbumComment = async (guid, comments, parentId) => http.post("/contents/Comment/" + guid + '?comments=' +
	comments + '&parentId' + parentId); //用户评论内容

export const GetInteractAlbums = async (date, type) => http.get("/contents/Interact?searchDate=" + date + '&type=' +
	type, {
		custom: {
			loading: true
		}
	}); //获取我的互动内容  

export const SearchAlbums = async (key, date) => http.get(`/contents/Search/${key}?searchDate=${date}`); //搜索故新内容
export const SearchUserAlbums = async (key, date, userId) => http.get(
	`/contents/User/Search/${key}?searchDate=${date}&userId=${userId}`); //搜索用户和故新内容
