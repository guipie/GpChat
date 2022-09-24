<template>
	<view class="smc myp-bg-inverse myp-flex-row myp-align-center myp-position-relative" bubble="true"
		@click="toSelect">
		<myp-icon v-if="disable" name="success" iconStyle="font-size: 40rpx;color:#bebebe;"></myp-icon>
		<myp-icon v-else :name="selected?'success':'check'" :type="selected?'success':'third'"
			iconStyle="font-size: 40rpx;" @iconClicked="toSelect"></myp-icon>
		<image :src="avatar" mode="aspectFill"
			style="width: 72rpx;height: 72rpx;border-radius: 12rpx;margin-left: 20rpx;"></image>
		<view class="myp-flex-row myp-align-center myp-flex-one" style="margin-left: 24rpx;">
			<text class="myp-color-text myp-size-base">{{item.FriendRemarkName}}</text>
		</view>
		<view v-if="!isLast" class="myp-position-absolute"
			style="right: 46rpx;bottom: 0;width: 560rpx;height: 0.5px;background-color: #ECEEF0;"></view>
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
			},
			selected: {
				type: Array,
				default: () => {
					return []
				}
			},
			disable: false
		},
		computed: {
			avatar() {
				return baseAvatar(this.item.Avatar);
			}
		},
		methods: {
			toSelect() {
				this.$emit("select", this.item.FriendUserId)
			}
		}
	}
</script>

<style lang="scss" scoped>
	.smc {
		width: 750rpx;
		padding-left: 30rpx;
		padding-right: 30rpx;
		height: 100rpx;
	}
</style>
