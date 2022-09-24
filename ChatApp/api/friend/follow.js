'use strict'

import http from '@/api/request.js';

export const GetFollowUsers = async (date) => http.get("/friend/follow?searchDate=" + date); //获取关注好友
export const FollowUser = async (userId) => http.post("/friend/follow/" + userId); //关注、取消关注好友
