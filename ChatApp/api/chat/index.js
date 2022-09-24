'use strict'

import http from '@/api/request.js';

export const SendChat = async (sendObject) => http.post("/ws/send-msg", sendObject); //发送消息
export const LoadChat = async () => http.get("/ws/Chat/Reload"); //拉取消息
export const SendChatToMe = async (sendObject) => http.post("/ws/send-msg?IsReceive=true", sendObject); //发送消息给自己 
export const SendGroupChat = async (sendObject) => http.post(
	`/ws/send-channelmsg/${getApp().globalData.currentSocketGuid}/` + sendObject
	.ReceiveUserId, sendObject); //发送群消息

export const SendGroupChatToMe = async (sendObject) => http.post(
	`/ws/send-channelmsg/${getApp().globalData.currentSocketGuid}/` + sendObject
	.ReceiveUserId + '?receive=true', sendObject); //发送群消息，同时发送自己
