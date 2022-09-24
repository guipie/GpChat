<template>
	<view class="myp-flex-row myp-wrap-nowrap myp-bg-inverse"
		style="height: 150rpx;width: 750rpx;padding-left: 30rpx;padding-right: 30rpx;background: #EDEDED;" bubble="true"
		@tap="toDetail">
		<image :src="avatar" mode="aspectFill" style="width: 120rpx;height: 120rpx;border-radius: 24rpx;"
			@error="errorAvatar"></image>
		<view class="myp-flex-one myp-flex-column" style="margin-left: 24rpx;">
			<text class="myp-size-ll myp-lh-ll myp-color-text" style="font-weight: 900;">{{nickName}}</text>
			<view style="height: 15rpx;"></view>
			<view class="myp-flex-row myp-wrap-nowrap myp-align-center myp-justify-between">
				<text class="myp-size-ss myp-lh-ss myp-color-second">{{'故新号：'+userName}}</text>
				<view class="myp-flex-row myp-align-center">
					<myp-icon name="qr" type="third" size="ss" @iconClicked="toQr"></myp-icon>
					<view style="width: 32rpx;"></view>
					<myp-icon name="right" type="third" size="ss" @iconClicked="toDetail"></myp-icon>
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	import {
		mapGetters
	} from 'vuex'

	export default {
		computed: {
			...mapGetters(["userName", 'nickName', 'avatar'])
		},
		methods: {
			toDetail() {
				this.$emit("detail")
			},
			toQr() {
				this.$emit("qr")
			},
			errorAvatar() {

			}
		},
		onShow() {
			console.log('aaa', this.$store);
		}

	}
</script>

<style>
</style>
