<template>
	<myp-popup :show="show" pos="center" width="524rpx" height="218rpx">
		<view class="myp-flex-column myp-align-center myp-bg-inverse" style="width: 524rpx;height: 218rpx;border-radius: 32rpx;">
			<text class="myp-color-primary myp-size-l" style="line-height: 60rpx;margin-top: 8rpx;">{{title}}</text>
			<text class="myp-color-text myp-size-l myp-lh-l" style="margin-top: 40rpx;margin-top: 46rpx;">{{desc}}</text>
		</view>
	</myp-popup>
</template>

<script>
	export default {
		props: {
			show: {
				type: Boolean,
				default: false
			},
			title: {
				type: String,
				default: '提示'
			},
			desc: {
				type: String,
				default: '密码错误！'
			}
		}
	}
</script>

<style>
</style>
