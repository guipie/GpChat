<template>
	<myp-list ref="normalList" loadingText="正在获取最新内容..." @down='getNormalAlbums' @up="getNormalAlbums">
		<myp-list-cell v-for="(item,idx) in items" :key="idx">
			<album-cell :item="item" :showFollow="true" :currentIndex="idx" @detail="toDetail" @pop="toPop"
				:last="items.length==idx+1" ref="albumCell" @follow="toFollow" @chat="toChat">
			</album-cell>
		</myp-list-cell>
	</myp-list>
</template>

<script>
	import albumCell from './albumCell.vue'
	import {
		FollowUser
	} from '@/api/friend/follow.js'
	import {
		GetNewestAlbums
	} from '@/api/album/index.js';
	import {
		mapMutations
	} from 'vuex'
	export default {
		components: {
			albumCell
		},
		data() {
			return {
				items: [],
				useLoading: true,
				searchDate: ""
			}
		},
		methods: {
			...mapMutations({
				SetChattingUser: "chat/SetChattingUser"
			}),
			toDetail(item) {
				this.$emit("detail", item);
			},
			toPop(e) {
				this.$emit("pop", e);
			},
			getNormalAlbums() {
				const ins = this.$refs['normalList'];
				const cp = ins.mypCurrentPage;
				if (cp === 1) {
					this.searchDate = "";
					this.items = [];
				}
				GetNewestAlbums(this.searchDate).then(res => {
					this.items.push.apply(this.items, res.data || []);
					this.searchDate = res.data.map(m => m.createDate)[res.data.length - 1];
					ins.mypEndSuccess(res.data.length >= 5)
				}).catch(err => {
					ins.mypEndError()
				})
			},
			toFollow(item) {
				FollowUser(item.createID).then(res => {
					item.isFollowing = res.data > 0;
				});
			},
			toChat(item) {
				this.SetChattingUser({
					FriendRemarkName: item.contentUser.nickName,
					FriendUserId: item.contentUser.userId,
					Avatar: item.contentUser.avatar
				});
			}
		},
		mounted() {
			const ins = this.$refs['normalList'];
			ins.mypLoad();
		}
	}
</script>

<style lang="scss" scoped>
</style>
