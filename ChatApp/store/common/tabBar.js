'use strict'
import {
	isNullOrEmpty
} from '@/mypUI/utils/validate.js'


const state = {
	currentBarIndex: 0,
	chatCount: 0,
	newsCount: 0
}
const mutations = {
	RemoveTabBar(state, index) {
		state.currentBarIndex = index;
		uni.removeTabBarBadge({
			index: index
		});
		if (index == 0) state.chatCount = 0
		else if (index == 1) state.newsCount = 0
	},
	SetChatTabBar(state, count) {
		if (state.currentBarIndex != 0) {
			state.chatCount += count;
			uni.setTabBarBadge({
				index: 0,
				text: (state.chatCount).toString()
			})
		}
	},
	SetNewsTabBar(state, count) {
		if (state.currentBarIndex != 1) {
			state.newsCount += count;
			uni.setTabBarBadge({
				index: 1,
				text: (state.newsCount).toString()
			})
		}
	}
}

const actions = {}
export default {
	namespaced: true,
	state,
	mutations,
	actions
}
