<template>
	<view class="myp-bg-inverse myp-flex-row myp-align-start myp-wrap-nowrap" style="padding-bottom: 12rpx;">
		<view class="myp-flex-row myp-align-center" style="width: 138rpx;height: 72rpx;">
			<text class="myp-size-base myp-color-text" style="margin-left: 30rpx;font-weight: 900;">{{item.key}}</text>
		</view>
		<view class="myp-flex-one myp-flex-row myp-wrap-wrap">
			<image v-for="(path,idx) in item.pics" :key="idx" :src="path" mode="aspectFill" @tap="toPreview(idx)"
				style="width: 186rpx;height: 186rpx;border-radius: 4rpx;margin-right: 12rpx;margin-bottom: 12rpx;">
			</image>
		</view>
	</view>
</template>

<script>
	export default {
		props: {
			item: {
				type: Object,
				default: () => {
					return {}
				}
			}
		},
		computed: {
			images() {
				const a = Math.floor(Math.random() * 7)
				const arr = []
				for (let i = 0; i <= a; i++) {
					arr.push(`/static/pic${i+1}.jpg`)
				}
				return arr
			}
		},
		methods: {
			toPreview(index) {
				let item = {
					pics: this.item.pics,
					index: index
				};
				this.$emit('preview', item);
			}
		}
	}
</script>

<style>
</style>
