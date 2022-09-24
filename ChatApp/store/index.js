import Vue from 'vue'
import Vuex from 'vuex'
import getters from './getters.js'
// modules
import user from '@/store/user/index.js'
import friend from './user/friend.js'
import socket from '@/store/socket/index.js'
import chat from '@/store/socket/chat.js'
import group from '@/store/user/group.js'
import album from '@/store/album/index.js'
import notice from '@/store/common/notice.js'
import settings from '@/store/common/settings.js'
import tabBar from '@/store/common/tabBar.js'
Vue.use(Vuex)

const store = new Vuex.Store({
	modules: {
		user,
		friend,
		socket,
		chat,
		group,
		album,
		notice,
		settings,
		tabBar
	},
	getters
})

export default store
