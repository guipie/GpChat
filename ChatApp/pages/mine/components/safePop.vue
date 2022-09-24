<template>
	<myp-popup :show="show" pos="top" topOffset="status-nav-216rpx" width="520rpx" height="502rpx">
		<view class="myp-flex-column myp-align-center myp-bg-inverse"
			style="width: 520rpx;height: 502rpx;border-radius: 32rpx;">
			<view style="height: 66rpx;"></view>
			<text class="myp-color-text myp-size-l myp-lh-l"
				style="width: 340rpx;">您的申请已经受理，系统将于7个工作日完成审核，如果您放弃此次申请，可以点击取消。</text>
			<view style="height: 40rpx;"></view>
			<view style="height: 1px;background-color: #808080;width: 430rpx;"></view>
			<view style="height: 24rpx;"></view>
			<view class="myp-flex-row myp-align-center myp-wrap-nowrap myp-justify-center">
				<text class="myp-color-third myp-size-l" style="line-height: 70rpx;width: 166rpx;text-align: center;"
					@click="toCancel">取消</text>
				<view
					style="width: 1px;height: 48rpx;background-color: #808080;margin-left: 62rpx;margin-right: 62rpx;">
				</view>
				<text class="myp-color-error myp-size-l" style="line-height: 70rpx;width: 166rpx;text-align: center;"
					@click="toConfirm">确认</text>
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
				this.$emit("cancel")
			},
			toConfirm() {
				this.$emit("confirm")
			}
		}
	}
</script>

<style>
</style>
