'use strict'

import http from '@/api/request.js';

export const GetInteractNotices = async (date) => http.get("/Interact/Notice?searchDate=" + date, {
	custom: {
		loading: true
	}
}); //获取互动我的通知内容
