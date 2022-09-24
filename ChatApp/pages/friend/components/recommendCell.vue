<template>
	<view class="myp-flex-row myp-align-center myp-wrap-nowrap myp-bg-inverse myp-position-relative"
		style="padding-left: 30rpx;padding-right: 30rpx;height: 104rpx;">
		<image :src="avatar" mode="aspectFill" style="width: 72rpx;height: 72rpx;border-radius: 16rpx;"></image>
		<text class="myp-flex-one myp-lines-one myp-color-text myp-size-base"
			style="margin-left: 24rpx;">{{item.nickName}}</text>
		<myp-button bgType="error" text="认识" textType="inverse" textSize="s" border="none" radius="ll"
			boxStyle="width: 110rpx;height: 44rpx;" @buttonClicked="toAdd"></myp-button>
		<view v-if="!isLast" class="myp-position-absolute"
			style="height: 0.5px;background-color: #EFEFF4;width: 624rpx;right: 0;bottom: 0;"></view>
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
			isLast: {
				type: Boolean,
				default: false
			}
		},
		computed: {
			avatar() {
				return baseAvatar(this.item.avatar);
			}
		},
		methods: {
			toAdd() {
				this.$emit('chat', this.item)
			}
		}
	}
</script>

<style>
</style>
