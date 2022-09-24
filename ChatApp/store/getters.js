import {
	baseFileUrl,
	baseAvatar
} from '@/common/env.js'
export default {
	hasLogedIn: state => state.user.hasLogedIn,
	userId: state => state.user.userInfo.userId,
	userName: state => state.user.userInfo.userName,
	avatar: state => baseAvatar(state.user.userInfo.avatar),
	nickName: state => state.user.userInfo.nickName,
	phone: state => state.user.userInfo.phone,
	//socket连接状态
	isOpen: state => state.socket.isOpen,
	//是否正在连接中...
	connecting: state => state.socket.connecting,
	// 心跳检测，多少秒执行检测 
	timeout: state => state.socket.timeout,
	reconnectCount: state => state.socket.reconnectCount,
	// //检测服务器端是否还活着
	// heartbeatInterval: state => state.socket.heartbeatInterval,
	// //重连之后多久再次重连 
	// reconnectTimeOut: state => state.socket.reconnectTimeOut,
}
