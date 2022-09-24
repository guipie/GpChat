<template>
	<view class="alc myp-flex-row myp-wrap-nowrap myp-justify-between myp-align-center myp-position-relative">
		<view class="myp-flex-row myp-align-center">
			<image src="/static/logo-dark.png" style="width: 100rpx;height: 100rpx;border-radius: 24rpx;" mode="aspectFill"></image>
			<view class="myp-flex-column myp-align-start" style="margin-left: 30rpx;width: 442rpx;">
				<text class="myp-color-text myp-size-s myp-lh-s" style="margin-bottom: 6rpx;">我来自山里</text>
				<myp-icon v-if="type" name="yes" type="error" size="ss"></myp-icon>
				<text v-else class="myp-color-second myp-size-ss myp-lh-ss">你是开飞机的吗？</text>
				<text class="myp-color-third" style="font-size: 20rpx;line-height: 28rpx;margin-top: 12rpx;">下午16:56</text>
			</view>
		</view>
		<image src="/static/logo-dark.png" style="width: 100rpx;height: 100rpx;border-radius: 4rpx;" mode="aspectFill"></image>
		<view v-if="!isLast" class="myp-position-absolute" style="left: 40rpx;right: 30rpx;height: 0.5px;bottom: 0;background-color: #EFEFF4;"></view>
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
			isLast: {
				type: Boolean,
				default: false
			}
		},
		data() {
			return {
				type: Math.floor(Math.random()*2) > 0
			}
		}
	}
</script>

<style lang="scss" scoped>
	.alc {
		width: 750rpx;
		background-color: #FFFFFF;
		padding-left: 40rpx;
		padding-right: 30rpx;
		height: 146rpx;
	}
</style>
