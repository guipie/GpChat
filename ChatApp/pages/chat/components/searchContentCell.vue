<template>
	<view class="ac myp-bg-inverse">
		<view class="myp-flex-row myp-align-center myp-justify-between" v-if="item.createID>0">
			<view class="myp-flex-row myp-align-center" @tap="toUser()">
				<image :src="avatar" style="width: 64rpx;height: 64rpx;border-radius: 16rpx;" mode="aspectFill">
				</image>
				<text class="myp-size-s myp-color-text"
					style="margin-left: 20rpx;font-weight: 700;">{{item.nickName}}</text>
			</view>
			<view class="myp-flex-row myp-align-center">
				<view>
					<myp-button textSize="ss" radius="ll" :text="item.isFollowing?'已关注':'关注'"
						:textType="item.isFollowing?'':'inverse'" :bgType="item.isFollowing?'':'error'"
						:textStyle="item.isFollowing?'color: #A0A3B8;':'color: #ffffff;'"
						boxStyle="width:100rpx;height:40rpx;border:1rpx;border-color:#F3F3F6;"
						@buttonClicked="toFollow">
					</myp-button>
				</view>
			</view>
		</view>
		<view style="height: 26rpx;" @tap="toDetail"></view>
		<text class="myp-lines-two myp-color-text myp-size-s myp-lh-s" @tap="toDetail">{{item.contents}}</text>
		<view style="height: 16rpx;" @tap="toDetail"></view>
		<view v-if="(item.pics||[]).length>0" class="myp-flex-row myp-wrap-wrap">
			<image v-for="(img,idx) in item.pics" :key="idx" :src="img"
				:style="'width:220rpx;height:220rpx;margin-bottom:15rpx;'+(idx%3==2?'margin-right:0;':'margin-right:15rpx;')"
				mode="aspectFill" @tap="previewImage(item.pics,idx)"></image>
		</view>
		<view class="myp-flex-row myp-align-center myp-justify-end">
			<text class="myp-size-ss myp-color-third myp-lh-ss">{{createDate}}</text>
		</view>
	</view>
</template>

<script>
	import {
		FollowUser
	} from '@/api/friend/follow.js'
	import {
		baseAvatar
	} from '@/common/env.js'
	import {
		formatTime
	} from '@/mypUI/utils/date.js'
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
			},
			createDate() {
				return formatTime(this.item.createDate || '', '{y}-{m}-{d} {h}:{i}')
			}
		},
		methods: {
			toDetail() {
				this.$emit("detail", this.item);
			},
			toFollow() {
				FollowUser(this.item.createID).then(res => {
					this.item.isFollowing = res.data > 0;
				});
			}
		}
	}
</script>

<style lang="scss" scoped>
	.ac {
		width: 750rpx;
		padding-top: 36rpx;
		padding-left: 30rpx;
		padding-right: 30rpx;
		padding-bottom: 16rpx;
		position: relative;
	}
</style>
