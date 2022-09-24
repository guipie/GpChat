<template>
	<view class="ac myp-bg-inverse">
		<view class="myp-flex-row myp-align-center myp-justify-between">
			<view class="myp-flex-row myp-align-center">
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
		<myp-scroll-h width="750rpx" height="200rpx" v-if="item.contentList.length>0">
			<view class="vitc" bubble="true" v-for="(c,index) in item.contentList" @tap="toDetail(c)">
				<view class="vitc-top" v-if="c.pics.length>0">
					<image :src="c.pics[0]" class="vitc-img" mode="aspectFill"></image>
					<view class="vitc-tag">
						<text class="vitc-tag-text">{{c.pics.length}}张图</text>
					</view>
				</view>
				<text class="vitc-text">{{c.contents}}</text>
			</view>
		</myp-scroll-h>
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
			toDetail(c) {
				this.$emit("detail", c)
			},
			toFollow() {
				FollowUser(this.item.userId).then(res => {
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

	.vitc {
		width: 200rpx;
		margin-right: 30rpx;
		margin-top: 20rpx;

		&-top {
			width: 200rpx;
			height: 200rpx;
			border-radius: 12rpx;
			position: relative;
		}

		&-img {
			width: 200rpx;
			height: 200rpx;
			border-radius: 12rpx;
		}

		&-tag {
			/* #ifndef APP-NVUE */
			display: flex;
			box-sizing: border-box;
			/* #endif */
			flex-direction: row;
			align-items: center;
			position: absolute;
			right: 12rpx;
			top: 8rpx;

			&-text {
				margin-left: 6rpx;
				color: #FFFFFF;
				font-size: 24rpx;
				line-height: 30rpx;
			}
		}

		&-text {
			margin-top: 8rpx;
			width: 200rpx;
			font-size: 28rpx;
			line-height: 40rpx;
			color: #333333;
			lines: 2;
			overflow: hidden;
			text-overflow: ellipsis;
			/* #ifndef APP-NVUE */
			display: -webkit-box;
			-webkit-box-orient: vertical;
			-webkit-line-clamp: 2;
			/* #endif */
		}
	}
</style>
