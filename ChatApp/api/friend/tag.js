'use strict'

import http from '@/api/request.js';

export const GetTags = async () => http.get('/tag'); //标签列表
export const GetMembers = async (tagId) => http.get('/tag/Members/' + tagId); //标签列表
export const PostTagMember = async (tagInfo) => http.post('/tag', tagInfo, {
	custom: {
		loading: true,
		loadingTxt: "正在创建.."
	}
}); //添加标签
