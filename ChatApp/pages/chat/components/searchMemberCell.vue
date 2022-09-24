<template>
	<view class="smc myp-bg-inverse myp-flex-row myp-align-center myp-position-relative" bubble="true"
		@click="toFriend">
		<image :src="avatar" mode="aspectFill" style="width: 72rpx;height: 72rpx;border-radius: 12rpx;"></image>
		<view class="myp-flex-row myp-align-center" style="margin-left: 18rpx;">
			<text class="myp-color-error myp-size-s">{{item.FriendRemarkName}}</text>
		</view>
		<view v-if="!isLast" class="myp-position-absolute"
			style="right: 30rpx;bottom: 0;width: 606rpx;height: 0.5px;background-color: #ECEEF0;"></view>
	</view>
</template>

<script>
	import {
		mapActions,
		mapState
	} from 'vuex'
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
			...mapState({
				allFriends: state => state.friend.allFriends
			}),
			avatar() {
				return baseAvatar(this.item.Avatar);
			}
		},
		methods: {
			toFriend() {
				if (this.allFriends.findIndex(m => m.FriendUserId === this.item.FriendUserId) > -1)
					uni.navigateTo({
						url: '/pages/chat/userMiddle?userId=' + this.item.FriendUserId
					})
				else
					uni.navigateTo({
						url: '/pages/friend/addFriend?userId=' + this.item.FriendUserId
					});
			}
		}
	}
</script>

<style lang="scss" scoped>
	.smc {
		width: 750rpx;
		padding-left: 30rpx;
		padding-right: 30rpx;
		height: 110rpx;
	}
</style>
