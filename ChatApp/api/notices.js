'use strict'

import http from '@/api/request.js';
//获取互动通知
export const GetInteractNotices = () => http.get(`/Notice/Interact`);
export const GetInteractSettings = () => http.get(`/Interact/Settings`);
export const SetInteractNotDisturb = (notDisturb) => http.put(`/Interact/Settings/NotDisturb/${notDisturb}`);
export const SetInteractTop = (top) => http.put(`/Interact/Settings/Top/${top}`);
export const SetInteractDisable = (disable) => http.put(`/Interact/Settings/Disable/${disable}`);
//关注动态通知
export const GetFollowNotices = () => http.get(`/Notice/Follow`);
export const GetFollowSettings = () => http.get(`/Friend/Follow/Settings`);
export const SetFollowNotDisturb = (notDisturb) => http.put(`/Friend/Follow/Settings/NotDisturb/` + notDisturb);
export const SetFollowTop = (top) => http.put(`/Friend/Follow/Settings/Top/` + top);
export const SetFollowDisable = (disable) => http.put(`/Friend/Follow/Settings/Disable/` + disable);

//获取系统通知
export const GetNotices = (searchDate) => http.get(`/Notice?searchDate=` + searchDate);
export const GetSysNotices = () => http.get(`/Notice/Sys`);
export const GetSysNoticeSettings = () => http.get(`/Notice/Settings`);
export const SetSysNotDisturb = (notDisturb) => http.put(`/Notice/Settings/NotDisturb/${notDisturb}`);
export const SetSysTop = (top) => http.put(`/Notice/Settings/Top/${top}`);

//好友通知
export const GetFriendRequestNotices = () => http.get(`/Notice/FriendRequest`);
export const GetAcceptRequestNotices = () => http.get(`/Notice/FriendAccept`);
