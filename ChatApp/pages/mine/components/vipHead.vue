<template>
	<view class="vh">
		<image src="/static/color-bg.jpeg" mode="aspectFill" style="width: 750rpx;height: 320rpx;"></image>
		<view class="myp-position-absolute myp-flex-row myp-align-center myp-justify-between" style="left: 30rpx;bottom: 48rpx;width: 690rpx;">
			<view class="myp-flex-row myp-align-center">
				<image :src="avatar" mode="aspectFill" style="width: 120rpx;height: 120rpx;border-radius: 20rpx;"></image>
				<view style="margin-left: 24rpx;">
					<view class="myp-flex-row myp-align-center">
						<text class="myp-size-ll myp-lh-ll myp-color-inverse" style="font-weight: 700;">{{nickName}}</text>
						<view class="myp-flex-row myp-bg-inverse myp-radius-l" style="margin-left: 12rpx;height: 28rpx;padding-left: 8rpx;padding-right: 8rpx;">
							<myp-icon name="decoration" iconStyle="font-size:10px;" type="text"></myp-icon>
							<text class="myp-color-text" style="font-size: 10px;">VIP.1</text>
						</view>
					</view>
					<view style="height: 4rpx;"></view>
					<text class="myp-size-ss myp-lh-ss myp-color-inverse" style="font-weight: 700;">距特权结束还有5天</text>
				</view>
			</view>
			<text class="myp-size-s myp-color-inverse" style="font-weight: 700;">20元/月</text>
		</view>
	</view>
</template>

<script>
	import {mapGetters} from 'vuex'
	
	export default {
		computed: {
			...mapGetters(['avatar', 'nickName'])
		}
	}
</script>

<style lang="scss" scoped>
	.vh {
		width: 750rpx;
		height: 320rpx;
		position: relative;
	}
</style>
