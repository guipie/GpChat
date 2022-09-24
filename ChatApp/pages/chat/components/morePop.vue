<template>
	<myp-popup :show="show" :hasOverlay="false" height="200rpx" @overlayClicked="toCancel">
		<view class="myp-bg-inverse myp-flex-row myp-wrap-wrap" style="width: 750rpx;height: 200rpx;padding-top: 26rpx;padding-left: 30rpx;">
			<view class="myp-flex-column myp-align-center" style="width: 120rpx;" bubble="true" @click="toCollect">
				<myp-icon name="fav-fill" type="second" size="ll" iconStyle="font-size: 46rpx;" @iconClicked="toCollect"></myp-icon>
				<text class="myp-size-ss myp-lh-ss myp-color-second" style="margin-top: 3rpx;">收藏</text>
			</view>
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
			toCollect() {
				uni.$emit("collect")
			}
		}
	}
</script>

<style>
</style>
