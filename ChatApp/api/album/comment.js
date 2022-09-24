'use strict'

import http from '@/api/request.js';


export const GetComments = async (contentGuid, date) => http.get("/contents/Comment/" + contentGuid + '?searchDate=' +
	date); //发布内容/朋友圈

export const PostComments = async (contentGuid, comments, parentId) => http.post("/contents/Comment/" + contentGuid +
	'?comments=' + comments + '&parentId=' + parentId); //发布内容评论
export const PutComments = async (commentsId, comments) => http.put("/contents/Comment/" + commentsId +
	'?comments=' + comments); //修改内容评论
export const DelComments = async (commentsId) => http.delete("/contents/Comment/" + commentsId); //删除内容评论
export const PraiseComments = async (commentsId) => http.put("/contents/Comment/Praise/" + commentsId); //点赞内容评论
export const PostFriendComments = async (contentGuid, comments, parentId) => http.post("/contents/Comment/Friend/" +
	contentGuid +
	'?comments=' + comments + '&parentId=' + parentId); //发布朋友圈评论
