<template>
	<myp-popup :show="show" pos="top-center" width="600rpx" height="460rpx" @overlayClicked="toCancel">
		<view class="pp">
			<view class="pp-top">
				<text class="pp-title">您需要同意本隐私政策才能继续使用推朋应用程序</text>
				<text class="pp-desc">若您不同意，很遗憾我们将无法为您提供账号信息服务。</text>
			</view>
			<view class="pp-actions">
				<myp-button text="仍不同意" textType="second" radius="ll" boxStyle="width:236rpx;height:78rpx;" @buttonClicked="toReject"></myp-button>
				<myp-button text="查看协议" bgType="primary" textType="inverse" border="none" radius="ll" boxStyle="width:236rpx;height:78rpx;" @buttonClicked="toAgree"></myp-button>
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
			toCancel() {
				
			},
			toReject() {
				this.$emit("reject")
			},
			toAgree() {
				this.$emit("agree")
			}
		}
	}
</script>

<style lang="scss" scoped>
	
	.pp {
		width: 600rpx;
		height: 460rpx;
		border-radius: 20rpx;
		background-color: #FFFFFF;
		overflow: hidden;
		position: relative;
		
		&-top {
			width: 600rpx;
		}
		&-title {
			font-size: 36rpx;
			line-height: 45rpx;
			color: $myp-text-color;
			padding-left: 48rpx;
			padding-right: 48rpx;
			width: 600rpx;
			margin-top: 62rpx;
			font-weight: 600;
			margin-bottom: 30rpx;
		}
		&-desc {
			font-size: 30rpx;
			line-height: 42rpx;
			color: $myp-text-color-second;
			padding-left: 48rpx;
			padding-right: 48rpx;
			width: 600rpx;
		}
		&-actions {
			position: absolute;
			left: 48rpx;
			right: 48rpx;
			bottom: 38rpx;
			flex-direction: row;
			flex-wrap: nowrap;
			justify-content: space-between;
		}
	}
</style>
