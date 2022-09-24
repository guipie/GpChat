const devBase = "http://192.168.0.104:9992/AppApi/V1/"
const devFileBase = "http://192.168.0.104:9992/"
const prodFileBase = "http://inshine.xyz:9001/"
const prodBase = "http://inshine.xyz:9001/AppApi/V1/" // product server

import {
	isNullOrEmpty
} from '@/mypUI/utils/validate.js'
export const isDev = process.env.NODE_ENV === 'development'
// devBase是开发环境，prodBase是正式环境
export const baseUrl = isDev ? prodBase : prodBase;
//占位图
export function baseFileUrl(path) {
	if (isNullOrEmpty(path))
		return '/static/ui/404pic.png';
	else {
		if (path.startsWith("http"))
			return path;
		else
			return (isDev ? devFileBase : prodFileBase) + path
	}
}
//头像
export function baseAvatar(path) {
	if (isNullOrEmpty(path))
		return '/static/ui/avatar.jpg';
	else {
		if (path.startsWith("http"))
			return path;
		else
			return (isDev ? devFileBase : prodFileBase) + path
	}
}
