import {
	isNullOrEmpty,
	isNum
} from '@/mypUI/utils/validate.js'
import {
	baseFileUrl
} from '@/common/env.js'

const state = {
	// Socket连接状态
	isOpen: false,
	connecting: false,
	// 心跳检测，多少秒执行检测 
	timeout: 20,
	reconnectCount: 1,
	// //检测服务器端是否还活着
	// heartbeatInterval: null,
	// //重连之后多久再次重连 
	// reconnectTimeOut: null
}
const mutations = {
	//设置心跳，默认30秒
	setTimeOut(state, seconds) {
		if (seconds == 0 || isNullOrEmpty(seconds) || !isNum(seconds))
			state.timeout = 30
		else
			state.timeout = seconds;
	},
	setReconnectCount(state) {
		state.reconnectCount++;
	},
	setSocketOpen(state) {
		state.isOpen = true;
	},
	setSocketClose(state) {
		state.isOpen = false;
	},
	setSocketConnecting(state) {
		state.connecting = true;
	},
	setSocketCloseConnecting(state) {
		state.connecting = false;
	}
}
export default {
	namespaced: true,
	state,
	mutations
}
