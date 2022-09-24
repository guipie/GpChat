<template>
	<list class="myp-bg-inverse" ref="myp-list" :bounce="true" isSwiperList="true" style="width:750rpx;flex:1;"
		:loadmoreoffset="60" @loadmore="toGetAlbums">
		<cell v-for="(item,idx) in items" :key="idx">
			<album-cell :item="item" :hasChat="false" :forCared="false" @detail="toDetail" @sharePop="toSharePopOptions"
				@pop="toPop">
			</album-cell>
			<view style="height: 20rpx;"></view>
		</cell>
		<cell>
			<myp-loader :isLoading="mypIsUpLoading" :hasMore="mypHasMore"></myp-loader>
		</cell>
	</list>
</template>

<script>
	import albumCell from '@/pages/tabs/components/albumCell.vue'

	import childMixin from '@/mypUI/myp-list/header/headerChild.js'
	import {
		GetUserAlbums,
		GetUserTopAlbums
	} from '@/api/album/index.js'
	export default {
		components: {
			albumCell
		},
		mixins: [childMixin],
		data() {
			return {
				items: [],
				searchDate: ''
			}
		},
		props: {
			userId: {
				default: 0,
				type: Number
			}
		},
		methods: {
			toPop(e) {
				this.$emit("pop", e);
			},
			toRefresh(ref, sucH, failH) {
				this.toGetAlbums('refresh', ref, sucH, failH)
			},
			toGetAlbums(val, ref, sucH, failH) {
				if (this.current !== this.index) return;
				this.mypInited = true
				const cp = this.mypStart(val)
				if (!cp) return;
				// api demo
				const mode = this.index === 1 ? 'all' : 'hot'
				if (this.index === 1)
					this.getAllAlbums(cp, ref, sucH, failH);
				if (this.index === 0)
					this.getTopAlbums(cp, ref, sucH, failH);
			},
			getAllAlbums(cp, ref, sucH, failH) {
				if (cp === 1) this.searchDate = "";
				GetUserAlbums(this.userId, this.searchDate).then(res => {
					if (res.status) {
						if (cp === 1)
							this.items = res.data;
						else
							this.items.push.apply(this.items, res.data);
						this.searchDate = res.data.map(m => m.createDate)[res.data.length - 1];
						this.mypEndSuccess(cp, res.data.length >= 5, ref, sucH);
					}
				}).catch(err => {
					this.$emit("error", err)
					this.mypEndError(cp, ref, failH)
				})
			},
			getTopAlbums(cp, ref, sucH, failH) {
				if (cp === 1) this.searchDate = "";
				GetUserTopAlbums(this.userId, this.searchDate).then(res => {
					if (res.status) {
						if (cp === 1)
							this.items = res.data;
						else
							this.items.push.apply(this.items, res.data);
						this.searchDate = res.data.map(m => m.createDate)[res.data.length - 1];
						this.mypEndSuccess(cp, res.data.length >= 5, ref, sucH);
					}
				}).catch(err => {
					this.$emit("error", err)
					this.mypEndError(cp, ref, failH)
				})
			}
		}
	}
</script>

<style>
</style>
