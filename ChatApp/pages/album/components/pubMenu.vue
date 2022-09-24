<template>
	<myp-popup :show="show" pos="top" :hasOverlay="true" overlayStyle="background-color:rgba(0,0,0,0);" top="status-nav"
		width="204rpx" height="156rpx" rightOffset="30rpx" @overlayClicked="toHidePop">
		<view class="myp-flex-column myp-align-center"
			style="width: 204rpx;height: 156rpx;background-color: rgba(58,68,74,0.95);border-radius: 2rpx;">
			<view class="myp-flex-row myp-align-center" style="height: 78rpx;" bubble="true" @tap="toPub(1)">
				<myp-icon name="/static/icon/11fawenzi.png" type="inverse" size="base" @iconClicked="toPub(1)">
				</myp-icon>
				<text class="myp-color-inverse myp-size-ss" style="margin-left: 18rpx;">发图文</text>
			</view>
			<view class="myp-flex-row myp-align-center" style="height: 78rpx;" bubble="true" @tap="toPub(0)">
				<myp-icon name="text" type="inverse" size="base" @iconClicked="toPub(0)"></myp-icon>
				<text class="myp-color-inverse myp-size-ss" style="margin-left: 18rpx;">发文字</text>
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
			toPub(i) {
				this.$emit("pub", i)
			},
			toHidePop() {
				this.show = false;
			}
		}
	}
</script>

<style>
</style>
