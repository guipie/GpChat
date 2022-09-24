<template>
	<view class="myp-bg-inverse myp-position-relative">
		<image src="/static/color-bg.jpeg" mode="aspectFill" style="width: 750rpx;height: 320rpx;"></image>
		<view class="myp-bg-inverse myp-flex-row myp-justify-end myp-align-end" style="height: 72rpx;width: 750rpx;">
			<text class="myp-size-base myp-lh-s myp-color-text myp-lines-two"
				style="line-height: 28rpx;width: 420rpx;text-align: right;margin-right: 30rpx;">{{userInfo.remark}}</text>
		</view>
		<view class="myp-position-absolute myp-flex-row myp-align-center myp-wrap-nowrap myp-justify-end"
			style="right: 30rpx;bottom: 52rpx;">
			<text class="myp-size-ll myp-color-inverse"
				style="margin-right: 32rpx;font-weight: 900;">{{userInfo.nickName}}</text>
			<image :src="avatar" mode="aspectFill" style="width: 140rpx;height: 140rpx;border-radius: 30rpx;"></image>
		</view>
	</view>
</template>

<script>
	export default {
		props: {
			userInfo: {
				type: Object,
				default: {}
			},
			avatar: ""
		}
	}
</script>

<style lang="scss" scoped>
</style>
