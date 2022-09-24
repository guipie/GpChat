<template>
	<view class="mtc myp-flex-column myp-align-center" :style="isMinus?'margin-right:32rpx;':'margin-right:0rpx;'"
		bubble="true" @tap="toAdd">
		<view class="myp-justify-center myp-align-center"
			style="width: 88rpx;height: 88rpx;border-radius: 16rpx;border-color: #D8D9DF;border-style-left: dashed;border-width: 2rpx;">
			<myp-icon :name="isMinus?'minus':'plus'" type="second" size="ll" @iconClicked="toAdd"></myp-icon>
		</view>
		<text :class="['myp-size-ss', 'myp-lh-ss', isMinus?'myp-color-second':'myp-color-success', 'myp-lines-one']"
			style="font-weight: 700;margin-top: 10rpx;width: 88rpx;text-align: center;">{{isMinus?'减少':'新增'}}</text>
	</view>
</template>

<script>
	export default {
		props: {
			isMinus: {
				type: Boolean,
				default: false
			}
		},
		methods: {
			toAdd() {
				this.$emit("add")
			}
		}
	}
</script>

<style lang="scss" scoped>
	.mtc {
		width: 88rpx;
		height: 172rpx;
		padding-top: 8rpx;
	}
</style>
