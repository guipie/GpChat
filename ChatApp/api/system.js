'use strict'

import http from '@/api/request.js';


export const PostComplaint = (complaint) => http.post(`/System/complaint`, complaint); //投诉
export const Upgrade = (platform, version) => http.get(`/System/upgrade/${platform}/${version}`); //App升级
// user = 1, 用户协议
// help = 2, 帮助协议
// secrecy = 3 隐私协议
export const Agreement = (key) => http.get(`/System/Agreement/${key}`);
