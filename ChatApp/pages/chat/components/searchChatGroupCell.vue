<template>
	<view class="src myp-position-relative myp-bg-inverse myp-flex-row myp-wrap-nowrap" @tap="toDetail">
		<view class="myp-flex-row myp-wrap-wrap myp-bg-inverse myp-align-center myp-justify-center"
			style="width: 80rpx;height: 80rpx;background-color: #f0f0f0;border-radius: 20rpx;">
			<image v-for="(u,index) in avatars" lazy-load='true' :key="index" :src="u" mode="aspectFill"
				style="width: 23rpx;height:23rpx;border-radius: 10rpx;margin: 2rpx;">
			</image>
		</view>
		<view class="myp-flex-one" style="margin-left: 18rpx;" @click="toDetail">
			<view class="myp-flex-row myp-align-center myp-justify-between" style="height: 72rpx;">
				<text class="myp-color-second myp-size-ss">{{group.Name}}</text>
				<text class="myp-color-second myp-size-ss">{{sendTime}}</text>
			</view>
			<view style="height: 10rpx;"></view>
			<view class="myp-flex-row myp-align-center myp-wrap-nowrap" style="width: 600rpx;">
				<rich-text class="myp-size-s myp-color-text myp-lh-s" :nodes="contents"></rich-text>
				<!--<text class="myp-size-s myp-color-error myp-lh-s">文章</text>
				<text class="myp-size-s myp-color-text myp-lh-s">发的恢复</text> -->
			</view>
		</view>
		<view v-if="!isLast" class="myp-position-absolute"
			style="right: 30rpx;bottom: 0;width: 606rpx;height: 0.5px;background-color: #ECEEF0;"></view>
	</view>
</template>

<script>
	import {
		baseAvatar
	} from '@/common/env.js'

	import {
		formatTime
	} from '@/mypUI/utils/date.js'
	import parseHtml from '@/common/nodesParser.js'
	export default {
		props: {
			item: {
				type: Object,
				default: () => {
					return {}
				}
			},
			group: {
				type: Object,
				default: () => {
					return {}
				}
			},
			isLast: {
				type: Boolean,
				default: false
			},
			search: {
				type: String,
				default: ""
			}
		},
		computed: {
			avatars() {
				return (this.group.Members || []).map(m => baseAvatar(m.Avatar));
			},
			sendTime() {
				return formatTime(this.item.SendTime || '', '{y}-{m}-{d} {h}:{i}')
			},
			contents() {
				return parseHtml(this.item.Content.replace(new RegExp(this.search, 'g'), '<span style="color:#E02020;">' +
					this.search + '</span>'));
			}
		},
		methods: {
			toDetail() {
				this.$emit("detail", this.item)
			}
		}
	}
</script>

<style lang="scss" scoped>
	.src {
		width: 750rpx;
		height: 158rpx;
		padding-left: 30rpx;
		padding-right: 30rpx;
		padding-top: 24rpx;
	}
</style>
