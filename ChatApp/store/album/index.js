'use strict'
import {
	Post
} from '@/api/album/index.js'
const state = {
	contents: [],
	friendContents: []
}
const actions = {
	async PostAsync({
		state,
		commit
	}, album) {
		console.log(album);
		Post(album).then();
	}
}


export default {
	namespaced: true,
	state,
	actions
}
