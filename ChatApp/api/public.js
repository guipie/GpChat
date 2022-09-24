'use strict'

import http from '@/api/request.js';

export const GetProvinces = (phone) => http.get("/common/Provinces", {}, {
	custom: {
		loading: true,
		loadingTxt: "正在加载.."
	}
});
export const GetCities = (pCode) => http.get("/Common/Cities/" + pCode, {}, {
	custom: {
		loading: true,
		loadingTxt: "正在加载.."
	}
});
