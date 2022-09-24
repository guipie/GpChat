<template>
	<view class="myp-bg-inverse myp-position-relative" style="height: 428rpx;">
		<image src="/static/color-bg.jpeg" mode="aspectFill"
			style="width: 750rpx;height: 368rpx;position: absolute;left: 0;top: 0;"></image>
		<view class="myp-bg-inverse myp-flex-column"
			style="height: 100rpx;width: 750rpx;border-top-left-radius: 20rpx;border-top-right-radius: 20rpx;margin-top: 328rpx;">
			<view class="myp-flex-row myp-align-center myp-justify-between" style="width: 540rpx;margin-left: 30rpx;">
				<view class="myp-flex-row myp-align-center">
					<text class="myp-size-ss myp-color-text">关注</text>
					<text class="myp-size-l myp-color-primary"
						style="margin-left: 6rpx;">{{userInfo.followCount}}</text>
					<text class="myp-size-ss myp-color-text" style="margin-left: 24rpx;">粉丝</text>
					<text class="myp-size-l myp-color-primary"
						style="margin-left: 6rpx;">{{userInfo.followedCount}}</text>
				</view>
				<view class="myp-flex-row myp-align-center">
					<image src="/static/vip.png" style="width: 56rpx;height: 24rpx;"></image>
					<text class="myp-color-text myp-size-ll myp-lh-ll"
						style="margin-left: 12rpx;">{{userInfo.nickName}}</text>
				</view>
			</view>
			<view class="myp-flex-row myp-align-center" style="margin-left: 20rpx;">
				<myp-button v-if="userInfo.isMine" border="none" radius="ll" bgType="primary" text="编辑简介"
					textType="inverse" textSize="ss" boxStyle="width:187rpx;height:44rpx;margin-right:23rpx;"
					@buttonClicked="toEdit"></myp-button>
				<view v-else class="myp-flex-row">
					<myp-button textSize="ss" radius="ll" :text="userInfo.isFollowing?'已关注':'关注'"
						:textType="userInfo.isFollowing?'':'inverse'" :bgType="userInfo.isFollowing?'':'error'"
						:textStyle="userInfo.isFollowing?'color: #A0A3B8;':'color: #ffffff;'"
						:boxStyle="(userInfo.isFollowing?'width:100rpx;':'width:180rpx;')+'height:40rpx;border:1rpx;border-color:#f7eeec;'"
						@buttonClicked="toFollow">
					</myp-button>
					<myp-button textSize="ss" radius="ll" text="私信" :textStyle="'color: #A0A3B8;'"
						boxStyle="width:100rpx;height:40rpx;border:1rpx;border-color:#F3F3F6;" @buttonClicked="toChat"
						v-if="userInfo.isFollowing">
					</myp-button>
				</view>
				<text class="myp-size-ss myp-color-text myp-lines-one"
					style="line-height: 28rpx;width: 470rpx;text-align: right;margin-right: 30rpx;">{{userInfo.remark}}</text>
			</view>
		</view>
		<view class="myp-position-absolute myp-flex-row myp-wrap-nowrap myp-justify-end"
			style="right: 30rpx;bottom: 52rpx;">
			<image :src="avatar" mode="aspectFill" style="width: 140rpx;height: 140rpx;border-radius: 30rpx;"></image>
		</view>
	</view>
</template>

<script>
	import {
		mapMutations
	} from 'vuex'
	import {
		baseAvatar
	} from '@/common/env.js'
	import {
		FollowUser
	} from '@/api/friend/follow.js'
	export default {
		computed: {
			// ...mapGetters(['avatar', 'sign', 'nickName']) 
			avatar() {
				return baseAvatar(this.userInfo.avatar);
			}
		},
		data() {
			return {
				userInfo: {}
			}
		},
		props: {
			userInfo: {
				default: {},
				type: Object
			}
		},
		methods: {
			...mapMutations({
				SetChattingUser: "chat/SetChattingUser"
			}),
			toEdit() {
				uni.navigateTo({
					url: '/pages/album/editIntro'
				});
			},
			toFollow() {
				FollowUser(this.userInfo.userId).then(res => {
					if (res.data === 1)
						this.userInfo.isFollowing = true;
					else
						this.userInfo.isFollowing = false;
				})
			},
			toChat() {
				this.SetChattingUser({
					FriendRemarkName: this.userInfo.nickName,
					FriendUserId: this.userInfo.userId,
					Avatar: this.userInfo.avatar
				});
			}
		}
	}
</script>

<style lang="scss" scoped>
</style>
