<template>
	<myp-popup :show="show" pos="top" topOffset="status-nav-132rpx" width="524rpx" height="310rpx">
		<view class="myp-flex-column myp-align-center myp-bg-inverse" style="width: 524rpx;height: 310rpx;border-radius: 32rpx;">
			<view style="height: 49rpx;"></view>
			<text class="myp-color-text myp-size-l" style="line-height: 66rpx;">确认清空记录</text>
			<view style="height: 36rpx;"></view>
			<view style="height: 1px;background-color: #808080;width: 432rpx;"></view>
			<view style="height: 24rpx;"></view>
			<text class="myp-color-error myp-size-l" style="line-height: 89rpx;width: 436rpx;text-align: center;" @click="toCancel">取消</text>
		</view>
	</myp-popup>
</template>

<script>
	export default {
		props: {
			show: {
				type: Boolean,
				default: false
			}
		},
		methods: {
			toCancel() {
				this.$emit("cancel")
			}
		}
	}
</script>

<style>
</style>
