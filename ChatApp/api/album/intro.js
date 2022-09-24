'use strict'

import http from '@/api/request.js';

export const Post = async (intros) => http.post("/ContentIntro", intros, {
	custom: {
		loading: true,
		loadingTxt: '正在发布.'
	}
}); //发布内容简介
export const Put = async (intros) => http.put("/ContentIntro", intros, {
	custom: {
		loading: true,
		loadingTxt: '正在编辑.'
	}
}); //编辑内容简介

export const Get = async (userId, date) => http.get("/ContentIntro/" + userId + '?searchDate=' + date, {
	custom: {
		loading: true,
		loadingTxt: '正在获取简介.'
	}
}); //简介列表 

export const IntroPraised = async (interoId) => http.put("/ContentIntro/Praise/" + interoId); //点赞简介
export const IntroDel = async (interoId) => http.delete("/ContentIntro/" + interoId, {
	custom: {
		loading: true,
		loadingTxt: '正在删除简介.'
	}
}); //删除简介
