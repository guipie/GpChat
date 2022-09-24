<template>
	<view class="myp-bg-inverse myp-position-relative myp-flex-row myp-align-center myp-wrap-nowrap myp-justify-between"
		style="height: 104rpx;padding-left: 30rpx;padding-right: 30rpx;">
		<view class="myp-flex-row myp-align-center">
			<image :src="avatar()" mode="aspectFill" style="width: 72rpx;height: 72rpx;border-radius: 16rpx;"></image>
			<text class="myp-size-base myp-color-text"
				style="margin-left: 24rpx;font-weight: 700;">{{item.friendRemarkName}}</text>
		</view>
		<myp-button bgType="success" text="取消" textType="inverse" textSize="ss" textStyle="font-weight:700;"
			border="none" radius="ll" boxStyle="width:88rpx;height:40rpx;" @buttonClicked="toCancel"></myp-button>
		<view v-if="!last" class="myp-position-absolute"
			style="left: 126rpx;right: 0;bottom: 0;height: 0.5px;background-color: #EFEFF4;"></view>
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
			},
			last: {
				type: Boolean,
				default: false
			}
		},
		methods: {
			toCancel() {
				this.$emit("cancel", this.item.friendUserId)
			},
			avatar() {
				return baseAvatar(this.item.avatar);
			}
		}
	}
</script>

<style>
</style>
