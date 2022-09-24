/*需要客户端推送的数据调用此接口地址*/
import request from '@/api/request.js'
import {
	isNullOrEmpty,
	isNum
} from '@/mypUI/utils/validate.js'
import {
	baseUrl
} from '@/common/env.js'
const config = {
	baseUrl: baseUrl,
	header: {
		"content-type": "application/json-patch+json"
	}
}
import store from '@/store/index.js'
let handleClose = false; //手动关闭
let reconnectTimeOut = null; //重连之后多久再次重连 
let currentSocketUrl = "";
export function postSocket(url, params = {}) {
	return request.request({
		toggle: false,
		method: 'POST',
		baseUrl: baseUrl,
		url: url,
		data: params
	});
}

export function handleChat(chats) { //处理推送的消息列表，通知列表
	if ((chats || []).length > 0) { //获取离线消息
		setTimeout(() => {
			let lastChats = [];
			for (var i = 0; i < chats.length; i++) {
				let receiveObj = JSON.parse(chats[i]);
				store.commit('chat/ReceiveChat', receiveObj);
				if (receiveObj.IsGroup === 1)
					receiveObj.FriendUserId = receiveObj.GroupId = receiveObj.ReceiveUserId
				else
					receiveObj.FriendUserId = receiveObj.SendUserId;
				let lastChatIndex = lastChats.findIndex(m => m.FriendUserId == receiveObj.FriendUserId);
				if (lastChatIndex > -1) {
					receiveObj.UnReadCount = (lastChats[lastChatIndex].UnReadCount || 1) + 1;
					lastChats.splice(lastChatIndex, 1);
				}
				lastChats.unshift(receiveObj);
			}
			for (let i = 0; i < lastChats.length; i++) {
				store.commit("chat/SetLastChat", lastChats[i]);
				store.commit("tabBar/SetChatTabBar", lastChats[i].UnReadCount);
			}
		}, 2000);
	}
}
export function socketInit() {
	if (!store.getters.isOpen && !store.getters.connecting)
		postSocket('/ws/init').then(res => {
			//初始化socket服务器传回连接server
			if (res.status) {
				store.commit("socket/setSocketOpen");
				connectSocketInit(res.data.wsserver);
				currentSocketUrl = res.data.wsserver;
				getApp().globalData.currentSocketGuid = res.data.currentSocketGuid;
				handleChat(res.data.chat);
				// store.dispatch("notice/GetInteractNoticesAsync"); //拉取互动通知
				// store.dispatch("notice/GetFollowNoticesAsync"); //拉取关注内容通知
				// store.dispatch("notice/GetSysNoticesAsync"); //拉取系统通知
				store.dispatch("notice/GetFriendRequestNoticesAsync"); //拉取好友请求通知
			}
		}).catch(error => {
			if (error.data?.code != 401) {
				console.log('socket获取server-token失败.', error);
				store.commit('socket/setSocketClose');
				store.commit('socket/setSocketCloseConnecting');
				reconnect();
			}
		});
}

export function socketClose() {
	uni.closeSocket({
		code: 1000,
		reason: 'loginOut'
	});
	handleClose = true;
	store.commit('socket/setSocketClose');
	store.commit('socket/setSocketCloseConnecting');
	clearTimeout(reconnectTimeOut);
}

function connectSocketInit(socketUrl) {
	if (isNullOrEmpty(socketUrl)) reconnect();
	uni.connectSocket({
		url: socketUrl,
		header: {
			'content-type': 'application/json',
			"socketGuid": getApp().globalData.currentSocketGuid
		},
		method: "GET",
		success: () => {
			store.commit('socket/setSocketConnecting');
			console.log(socketUrl + "正准备建立websocket中...");
		},
		fail: (err) => {
			console.log("连接失败..", err);
		}
	});
	uni.onSocketOpen(function(res) {
		console.log("WebSocket连接正常！");
		store.commit('socket/setSocketConnecting');
		clearTimeout(reconnectTimeOut);
	});
	// 监听连接失败，重复连接
	uni.onSocketError(function(err) {
		console.log('WebSocket连接打开失败，请检查！', err);
		store.commit('socket/setSocketClose');
		store.commit('socket/setSocketCloseConnecting');
		uni.closeSocket({
			code: 1000,
			reason: 'network close'
		}); //告知服务器下线
		reconnect();
	});
	// 这里仅是事件监听【如果socket关闭了会执行】
	uni.onSocketClose(function(res) {
		store.commit('socket/setSocketCloseConnecting');
		store.commit("chat/SetLastChatToDb");
		uni.closeSocket({
			code: 1000,
			reason: 'network close'
		}); //告知服务器下线
		console.log("已经被关闭了");
		reconnect();
	});
	// 注：只有连接正常打开中 ，才能正常收到消息
	uni.onSocketMessage(function(res) {
		console.log('收到服务器推送内容：' + res.data);
		let result = JSON.parse(res.data);
		if (parseInt(result.Type) < 20) //聊天消息 
			request.get("/ws/get-msg").then(msg => {
				let receiveObj = JSON.parse(msg.data);
				receiveObj.Online = true;
				store.commit('chat/ReceiveChat', receiveObj);
			});
		else
			store.commit("notice/SetNotice", result);
	});
}

function reconnect() {
	//如果不是人为关闭的话，进行重连
	if (!store.getters.isOpen && store.getters.reconnectCount < 100 && !handleClose)
		reconnectTimeOut = setTimeout(() => {
			store.commit('socket/setReconnectCount'); //更新重连次数
			postSocket('/ws/init').then(res => {
				//初始化socket服务器传回连接server
				store.commit("socket/setSocketOpen");
				console.log('socket第' + store.getters.reconnectCount + '次重连成功.');
				connectSocketInit(res.data.wsserver);
				handleChat(res.data.chat);
			}).catch(() => {
				connectSocketInit();
				console.log('socket第' + store.getters.reconnectCount + '次重连失败.');
			});
		}, (store.getters.timeout < 10 ? 10 : store.getters.timeout) * 1000);
	else
		console.log("断开重连...");
}
