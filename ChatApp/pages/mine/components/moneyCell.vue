<template>
	<view class="myp-flex-row myp-align-center myp-justify-between myp-bg-inverse myp-position-relative" style="padding-left: 30rpx;padding-right: 30rpx;height: 120rpx;">
		<view class="myp-flex-row myp-align-center">
			<image src="/static/logo-dark.png" mode="aspectFill" style="width: 72rpx;height: 72rpx;border-radius: 12rpx;"></image>
			<view style="margin-left: 24rpx;">
				<text class="myp-size-s myp-lh-s myp-color-text" style="font-weight: 700;">退款</text>
				<view style="height: 6rpx;"></view>
				<text class="myp-size-ss myp-lh-ss myp-color-second" style="font-weight: 700;">2019-02-07 12:46:01</text>
			</view>
		</view>
		<text class="myp-size-base myp-color-text" style="font-weight: 900;">+10086.12</text>
		<view class="myp-position-absolute" style="left: 126rpx;right: 30rpx;bottom: 0;height: 0.5px;background-color: #ECEEF0;"></view>
	</view>
</template>

<script>
	export default {
		props: {
			item: {
				type: Object,
				default: ()=>{
					return {}
				}
			},
			last: {
				type: Boolean,
				default: false
			}
		}
	}
</script>

<style lang="scss" scoped>
</style>
