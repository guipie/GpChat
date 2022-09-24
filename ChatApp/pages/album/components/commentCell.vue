<template>
	<view class="myp-bg-inverse cc">
		<view class="myp-flex-row myp-align-center myp-justify-between">
			<view class="myp-flex-row myp-align-center" @tap="replyComment">
				<image :src="commentAvatar" @tap="toUser(item.commentUser.userId)" mode="aspectFill"
					style="width: 64rpx;height: 64rpx;border-radius: 12rpx;">
				</image>
				<text class="myp-size-s myp-lh-s"
					style="margin-left: 20rpx;color: #717171;font-size: 28rpx;">{{item.commentUser.nickName}}</text>
			</view>
			<view class="myp-flex-row myp-align-center" bubble="true" @tap="toToggle">
				<myp-icon name="yes" :type="item.praised?'error':'second'" size="base" @iconClicked="toToggle">
				</myp-icon>
				<text class="myp-color-text"
					style="margin-left: 8rpx;font-size: 10px;font-weight: 700;">{{item.praiseCount}}</text>
			</view>
		</view>
		<view style="margin-left: 84rpx;" class="myp-flex-row" @tap="replyComment">
			<text class="myp-size-s myp-lh-s myp-color-text" v-if="item.parentId>0">回复</text>
			<text class="myp-size-s myp-lh-s" style="color: #0070F5;font-size: 28rpx;" v-if="item.parentId>0"
				@tap="toUser(item.parentcommentUser.userId)">{{item.parentcommentUser.nickName}}：</text>
			<text class="myp-size-s myp-color-text myp-lh-s" style="width: 606rpx;">{{item.comments}}</text>
		</view>
		<view class="myp-flex-row myp-align-center myp-justify-between">
			<text class="myp-color-third"
				style="margin-left: 84rpx;font-size: 10px;line-height: 38rpx;font-weight: 700;">{{date}}</text>
			<view class="myp-flex-row myp-align-center" bubble="true">
				<view class="myp-border-all myp-flex-row myp-justify-center"
					style="width: 28rpx;height: 22rpx;border-radius: 8rpx;border-color: #CCCFDB;margin-bottom: 18rpx;"
					@touchstart="toPopOptions">
					<myp-icon name="close1" type="third" :stop="false" iconStyle="font-size:8px;color:#8E979C;">
					</myp-icon>
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	import {
		getTouchPoint
	} from '@/mypUI/utils/element.js'
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
			date: {
				type: String,
				default: ''
			}
		},
		computed: {
			commentAvatar() {
				return baseAvatar(this.item.commentUser.avatar);
			}
		},
		methods: {
			toToggle() {
				this.$emit("praise", this.item)
			},
			toUser(userId) {
				this.$emit("userAlbum", userId);
			},
			toPopOptions(e) {
				const p = getTouchPoint(e);
				p.popItem = this.item;
				this.$emit("pop", p);
			},
			replyComment() {
				this.$emit('reply', this.item)
			}
		}
	}
</script>

<style lang="scss" scoped>
	.cc {
		padding-top: 24rpx;
		padding-left: 30rpx;
		padding-right: 30rpx;
	}
</style>
