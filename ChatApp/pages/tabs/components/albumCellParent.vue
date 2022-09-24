<template>
	<view class="ac">
		<view class="myp-flex-row myp-align-center myp-justify-between">
			<view class="myp-flex-row myp-align-center" @tap="toUser()">
				<image :src="avatar" style="width: 64rpx;height: 64rpx;border-radius: 16rpx;" mode="aspectFill">
				</image>
				<text class="myp-size-s myp-color-text"
					style="margin-left: 20rpx;font-weight: 700;">{{(parentItem.contentUser||{}).nickName}}</text>
			</view>
		</view>
		<view style="height: 26rpx;" @click="toDetail"></view>
		<text class="myp-lines-two myp-color-text myp-size-s myp-lh-s" @click="toDetail">{{parentItem.contents}}</text>
		<view style="height: 16rpx;" @click="toDetail"></view>
		<view v-if="hasImages" class="myp-flex-row myp-wrap-wrap">
			<image v-for="(img,idx) in parentItem.pics" :key="idx" :src="img.path"
				:style="'width:200rpx;height:200rpx;margin-bottom:15rpx;'+(idx%3==2?'margin-right:0;':'margin-right:15rpx;')"
				mode="aspectFill" @tap="previewImage(parentItem.pics.map(m=>m.path),idx)"></image>
		</view>
	</view>
</template>

<script>
	import imageHelper from '@/service/imageHelper.js'
	import {
		baseAvatar
	} from '@/common/env.js'

	export default {
		components: {},
		mixins: [imageHelper],
		props: {
			parentItem: {
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
		computed: {
			hasImages() {
				return (this.parentItem.pics || []).length > 0
			},
			avatar() {
				return baseAvatar(this.parentItem.contentUser?.avatar);
			},
			createDate() {
				return dayjs(this.parentItem.createDate).fromNow();
			}
		},
		methods: {
			toDetail() {
				this.$emit("detail", this.parentItem);
			},
			toUser() {
				uni.navigateTo({
					url: '/pages/album/userAlbums?userId=' + this.parentItem.createID
				})
			}
		}
	}
</script>

<style lang="scss" scoped>
	.ac {
		width: 690rpx;
		padding: 20rpx;
		position: relative;
		border-color: #efefef;
		border-width: 1rpx;
		background-color: #f7f7f7;
		border-radius: 15rpx;
	}
</style>
