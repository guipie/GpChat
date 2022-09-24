<template>
	<myp-list ref="myp-list" @inited="toInit" @down="toLoadData" @up="toLoadData">
		<myp-list-cell v-for="(item,idx) in items" :key="idx">
			<album-cell :item="item" :hasChat="false" :forCared="true" @detail="toDetail"></album-cell>
		</myp-list-cell>
	</myp-list>
</template>

<script>
	import albumCell from '../../tabs/components/albumCell.vue'
	import {
		GetInteractAlbums
	} from '@/api/album/index.js'
	export default {
		components: {
			albumCell
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
		data() {
			return {
				inited: false,
				searchDate: '',
				items: []
			}
		},
		methods: {
			toDetail(val) {
				uni.navigateTo({
					url: '/pages/album/albumDetail'
				})
			},
			toInit() {
				if (this.current == this.index && !this.inited) {
					this.inited = true
					const ins = this.$refs['myp-list']
					ins.mypRefresh()
					this.inited = true
				}
			},
			toLoadData() {
				const ins = this.$refs['myp-list'];
				const cp = ins.mypCurrentPage;
				if (cp === 1) {
					this.searchDate = "";
					this.items = [];
				}
				GetInteractAlbums(this.searchDate, this.index).then(res => {
					this.items.push.apply(this.items, res.data || []);
					this.searchDate = res.data.map(m => m.createDate)[res.data.length - 1];
					ins.mypEndSuccess(res.data.length >= 5)
				}).catch(err => {
					ins.mypEndError()
				});
			}
		},
		watch: {
			current(newV) {
				if (newV == this.index && !this.inited) {
					const ins = this.$refs['myp-list']
					ins && ins.mypRefresh()
					this.inited = true
				}
			}
		}
	}
</script>

<style lang="scss" scoped>
</style>
