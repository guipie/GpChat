<template>
	<myp-list ref="albumList" :refreshingText='refreshingText' :useLoading="useLoading" @down='toRefresh'
		@up="getAlbums()" :autoUpdate="true">
		<myp-list-cell v-for="(item,idx) in items" :key="idx">
			<top-album-cell v-if="index===2" :item="item" :top="(idx+1)" @detail="toDetail">
			</top-album-cell>
			<album-cell v-else :hasChat="false" :showFollow="index===1" :item="item" @detail="toDetail" @pop="toPop"
				@follow="toFollow">
			</album-cell>
		</myp-list-cell>
	</myp-list>
</template>

<script>
	import albumCell from './albumCell.vue'
	import topAlbumCell from './topAlbumCell.vue'
	import {
		GetFollowAlbums,
		GetRecommendAlbums,
		GetTopAlbums
	} from '@/api/album/index.js'
	import {
		FollowUser
	} from '@/api/friend/follow.js'
	export default {
		components: {
			albumCell,
			topAlbumCell
		},
		props: {
			current: {
				type: Number,
				default: 0
			},
			index: {
				type: Number,
				default: 0
			}
		},
		computed: {
			refreshingText() {
				if (this.index === 0)
					return '正在获取关注用户内容';
				else if (this.index === 1)
					return '正在获取推荐内容';
				else
					return '正在获取榜单内容';
			}
		},
		data() {
			return {
				items: []
			}
		},
		methods: {
			toPop(e) {
				this.$emit("pop", e);
			},
			toDetail(item) {
				this.$emit("detail", item);
			},
			toRefresh() {
				this.getAlbums();
			},
			getAlbums() {
				const ins = this.$refs['albumList'];
				const cp = ins.mypCurrentPage;
				if (cp === 1) this.searchDate = "";
				if (this.index === 0)
					GetFollowAlbums(this.searchDate).then(res => {
						if (cp === 1) {
							this.items = res.data || []
						} else {
							this.items.push.apply(this.items, res.data || []);
						}
						this.searchDate = res.data.map(m => m.createDate)[res.data.length - 1];
						ins.mypEndSuccess(res.data.length >= 5)
					}).catch(err => {
						ins.mypEndError()
					})
				else if (this.index === 1)
					GetRecommendAlbums(this.searchDate).then(res => {
						if (cp === 1) {
							this.items = res.data || []
						} else {
							this.items.push.apply(this.items, res.data || []);
						}
						this.searchDate = res.data.map(m => m.createDate)[res.data.length - 1];
						ins.mypEndSuccess(res.data.length >= 5)
					}).catch(err => {
						ins.mypEndError()
					})
				else if (this.index === 2)
					GetTopAlbums(this.searchDate).then(res => {
						if (cp === 1) {
							this.items = res.data || []
						} else {
							this.items.push.apply(this.items, res.data || []);
						}
						this.searchDate = res.data.map(m => m.createDate)[res.data.length - 1];
						ins.mypEndSuccess(res.data.length == 5 && cp < 4)
					}).catch(err => {
						ins.mypEndError()
					})
			},
			toFollow(item) {
				FollowUser(item.createID).then(res => {
					item.isFollowing = res.data > 0;
				});
			}
		}
	}
</script>

<style lang="scss" scoped>
</style>
