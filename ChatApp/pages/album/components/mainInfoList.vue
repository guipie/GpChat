<template>
	<list class="myp-bg-inverse" ref="myp-list" :bounce="true" isSwiperList="true"
		style="width:750rpx;flex:1; padding-top: 25rpx;" :loadmoreoffset="60" @loadmore="getIntros()">
		<cell v-for="(item,idx) in items" :key="idx">
			<album-cell :item="item" @detail="toDetail" @introPop="toIntroPop"></album-cell>
		</cell>
		<cell>
			<myp-loader :isLoading="mypIsUpLoading" :hasMore="mypHasMore"></myp-loader>
		</cell>
	</list>
</template>

<script>
	import albumCell from './mainAlbumCell.vue'

	import childMixin from '@/mypUI/myp-list/header/headerChild.js'

	import {
		Get
	} from '@/api/album/intro.js'
	export default {
		components: {
			albumCell
		},
		mixins: [childMixin],
		data() {
			return {
				searchDate: ''
			}
		},
		props: {
			items: {
				type: Array,
				default: []
			},
			userId: {
				type: Number,
				default: 0
			}
		},
		methods: {
			toRefresh(ref, sucH, failH) {
				this.getIntros('refresh', ref, sucH, failH)
			},
			toIntroPop(e) {
				this.$emit('pop', e);
			},
			getIntros(val, ref, sucH, failH) {
				if (this.current !== this.index) return;
				this.mypInited = true
				const cp = this.mypStart(val);
				if (!cp) return;
				if (cp === 1) this.searchDate = "";
				if (this.index === 2)
					Get(this.userId, this.searchDate).then(res => {
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

		}
	}
</script>

<style>
</style>
