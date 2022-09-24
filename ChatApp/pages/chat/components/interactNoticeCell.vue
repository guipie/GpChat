<template>
	<view class="inc">
		<view class="myp-flex-row myp-align-center myp-wrap-nowrap myp-justify-between">
			<view class="myp-flex-row myp-align-center" @tap="toUser">
				<image :src="avatar" mode="aspectFill" style="width: 64rpx;height: 64rpx;border-radius: 12rpx;"></image>
				<text class="myp-size-s myp-color-text" style="margin-left: 20rpx;">{{item.nickName}}</text>
				<text class="myp-size-ss myp-color-second" style="margin-left: 24rpx;">{{item.type}}</text>
				<myp-icon :name="interactIcon" :iconStyle="interactIconColor" type="error" size="ss"></myp-icon>
			</view>
			<text class="myp-color-second" style="font-size: 20rpx;">{{interactDate}}</text>
		</view>
		<view style="height: 24rpx;"></view>
		<view class="myp-flex-row myp-wrap-wrap" style="height: 120rpx;" @tap="toAlbum">
			<image :src="item.pics[0]" v-if="(item.pics||[]).length>0" mode="aspectFill"
				style="width: 120rpx;height: 120rpx;"></image>
			<view class="myp-flex-one" style="background-color: #F7F8FA;">
				<text class="myp-size-s myp-color-text myp-lh-s myp-lines-two"
					style="width: 520rpx;margin-left: 24rpx;margin-top: 12rpx;">{{item.contents}}</text>
			</view>
		</view>
		<view v-if="isPraise" class="myp-flex-row myp-align-center myp-justify-end" style="height: 72rpx;">
			<text class="myp-color-second" style="font-size: 20rpx;">还有</text>
			<text class="myp-color-error" style="font-size: 20rpx;">{{item.praiseCount}}</text>
			<text class="myp-color-second" style="font-size: 20rpx;">人点赞</text>
		</view>
	</view>
</template>

<script>
	import {
		formatTime
	} from '@/mypUI/utils/date.js'
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
			isPraise() {
				return this.item.type == '点赞了';
			},
			interactIcon() {
				if (this.item.type == '点赞了') return 'yes';
				else if (this.item.type == '评论了') return 'chat';
				else if (this.item.type == '新赞了') return 'hand-solid';
			},
			interactIconColor() {
				if (this.item.type == '评论了') return 'color:#38E0E0;';
				else if (this.item.type == '新赞了') return 'color:#FFAF49;';
			},
			interactDate() {
				return formatTime(this.item.createDate || '', '{y}-{m}-{d} {h}:{i}')
			},
			avatar() {
				return baseAvatar(this.item.avatar);
			}
		},
		methods: {
			toUser() {
				uni.navigateTo({
					url: '/pages/album/userAlbums?userId=' + this.item.createID
				})
			},
			toAlbum() {
				uni.navigateTo({
					url: '/pages/album/albumDetail?guid=' + this.item.contentGuid
				})
			}
		}
	}
</script>

<style lang="scss" scoped>
	.inc {
		width: 750rpx;
		padding-left: 30rpx;
		padding-top: 36rpx;
		padding-right: 30rpx;
		background-color: #FFFFFF;
	}
</style>
