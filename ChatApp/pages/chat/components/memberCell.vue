<template>
	<view class="mc myp-flex-column myp-align-center" :style="last?'margin-right:0;':'margin-right:32rpx;'"
		bubble="true">
		<image :src="avatar" mode="aspectFill" style="width: 88rpx;height: 88rpx;border-radius: 16rpx;" @click="toUser">
		</image>
		<text :class="['myp-size-ss', 'myp-lh-ss','myp-color-second', 'myp-lines-one']"
			style="font-weight: 700;margin-top: 10rpx;width: 88rpx;text-align: center;">{{item.NickName||item.FriendRemarkName}}</text>
		<view class="mc-close" v-if="isDel" @tap="toDelMember">
			<myp-icon name="close1" type="inverse" size="s" iconStyle="color:#E02020;" @iconClicked="toDelMember">
			</myp-icon>
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
			},
			last: {
				type: Boolean,
				default: false
			},
			isDel: {
				type: Boolean,
				default: false
			}
		},
		computed: {
			avatar() {
				return baseAvatar(this.item.Avatar);
			}
		},
		methods: {
			toUser() {
				this.$emit("detail", this.item.FriendUserId || this.item.UserId)
			},
			toDelMember() {
				this.$emit("del", this.item.FriendUserId);
			}
		}
	}
</script>

<style lang="scss" scoped>
	.mc {
		width: 88rpx;
		height: 172rpx;
		padding-top: 8rpx;
		overflow: hidden;
		position: relative;

		&-img {
			width: 220rpx;
			height: 220rpx;
			border-radius: 2rpx;
		}

		&-close {
			position: absolute;
			top: 9rpx;
			right: 3rpx;
			font-size: 40rpx;
		}
	}
</style>
