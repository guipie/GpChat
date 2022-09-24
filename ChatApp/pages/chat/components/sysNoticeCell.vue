<template>
	<view style="margin-left: 30rpx;width: 690rpx;" bubble="true" @click="toDetail">
		<view class="myp-flex-row myp-align-center myp-justify-end" style="height: 68rpx;">
			<text class="myp-color-second" style="font-size: 20rpx;">{{createDate}}</text>
		</view>
		<view class="myp-bg-inverse" style="border-radius: 4rpx;">
			<view class="myp-flex-row myp-align-center myp-justify-between myp-border-bottom"
				style="height: 88rpx;padding-left: 24rpx;padding-right: 24rpx;">
				<text class="myp-color-second myp-size-ss">{{item.Title}}</text>
				<myp-icon name="right" type="third" size="s" @iconClicked="toDetail"></myp-icon>
			</view>
			<view style="padding-top: 16rpx;padding-bottom: 54rpx;padding-left: 20rpx;padding-right: 20rpx;">
				<text class="myp-color-text myp-size-s myp-lh-s">{{item.Summary}}</text>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		props: {
			item: {
				type: Object,
				default: () => {
					return {}
				}
			},
			createDate: {
				type: Date,
				default: () => new Date()
			}
		},
		methods: {
			toDetail() {
				this.$emit("detail", this.item)
			}
		}
	}
</script>

<style>
</style>
