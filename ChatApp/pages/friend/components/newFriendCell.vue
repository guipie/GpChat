<template>
	<view>
		<view class="myp-flex-row myp-align-center myp-justify-between" style="height: 56rpx;padding-right: 30rpx;">
			<text class="myp-color-second myp-lh-ss myp-size-ss" style="margin-left: 30rpx;">好友申请</text>
			<text class="myp-color-second"
				style="font-size: 20rpx;font-weight: 700;color: #9092A5;">{{item.createDate}}</text>
		</view>
		<view class="myp-bg-inverse" style="width: 750rpx;">
			<view class="myp-border-bottom" style="height: 180rpx;margin-left: 30rpx;width: 690rpx;padding-top: 32rpx;">
				<view class="myp-flex-row myp-align-center">
					<image :src="avatar" mode="aspectFill" style="width: 72rpx;height: 72rpx;border-radius: 16rpx;">
					</image>
					<text class="myp-color-second myp-size-s"
						style="font-weight: 700;margin-left: 12rpx;">{{item.friendRemarkName}}</text>
				</view>
				<view style="height: 18rpx;"></view>
				<text class="myp-size-s myp-lh-s myp-color-text"
					style="font-weight: 700;margin-left: 84rpx;">{{item.description || '请求添加你为好友'}}</text>
			</view>
		</view>
		<view class="myp-bg-inverse myp-flex-row myp-align-center myp-justify-center"
			style="width: 750rpx;height: 128rpx;">
			<myp-button text="同意" border="none" radius="ll" textType="inverse" textSize="ss" bgType="error"
				boxStyle="width:140rpx;height:46rpx;" @buttonClicked="toAgree"></myp-button>
		</view>
	</view>
</template>

<script>
	import {
		baseAvatar
	} from '@/common/env.js'
	export default {
		props: {
			item: {
				type: Object,
				default: () => {
					return {}
				}
			}
		},
		computed: {
			avatar() {
				return baseAvatar(this.item.avatar);
			}
		},
		methods: {
			toAgree() {
				this.$emit("agree", this.item)
			}
		}
	}
</script>

<style>
</style>
