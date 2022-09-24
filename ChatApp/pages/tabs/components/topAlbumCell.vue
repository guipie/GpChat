<template>
	<view class="ac myp-bg-inverse">
		<!-- &nbsp;代表空格，用来做距离控制的，不要删除 -->
		<text class="myp-lines-two myp-color-text myp-size-s myp-lh-s"
			@tap="toDetail">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{{item.contents}}</text>
		<view style="height: 16rpx;" @tap="toDetail"></view>
		<view v-if="item.pics.length>0" class="myp-flex-row myp-wrap-wrap">
			<image v-for="(img,idx) in item.pics" :key="idx" :src="img.path"
				:style="'width:220rpx;height:220rpx;margin-bottom:15rpx;'+(idx%3==2?'margin-right:0;':'margin-right:15rpx;')"
				mode="aspectFill" @tap="previewImage(item.pics.map(m=>m.path),idx)"></image>
		</view>
		<view v-if="item.pics.length>0" style="height: 16rpx;"></view>
		<view class="myp-flex-row myp-align-center myp-justify-end" style="height: 40rpx;">
			<text class="myp-size-ss myp-color-third myp-lh-ss">{{createDate}}</text>
		</view>
		<view v-if="!last" class="myp-position-absolute"
			style="width: 690rpx;left: 30rpx;bottom: 0;height: 0.5px;background-color: #EFEFF4;"></view>
		<!-- 它的高度应该与文字行高一致 -->
		<image v-if="top<=3" :src="'/static/icon/hot'+top+'.png'" mode="aspectFill"
			style="position: absolute;left: 30rpx;top: 36rpx;width: 39rpx;height: 39rpx;"></image>
		<text v-else style="position: absolute;left:40rpx;top: 32rpx;width: 80rpx;height: 38rpx;">{{top}}</text>
	</view>
</template>

<script>
	import imageHelper from '@/service/imageHelper.js'

	import dayjs from 'dayjs';
	import relativeTime from 'dayjs/plugin/relativeTime'
	import 'dayjs/locale/zh-cn'
	dayjs.locale('zh-cn');
	dayjs.extend(relativeTime);
	export default {
		components: {},
		mixins: [imageHelper],
		props: {
			item: {
				type: Object,
				default: () => {
					return {}
				}
			},
			top: {
				type: Number,
				default: 0
			},
			last: {
				type: Boolean,
				default: false
			}
		},
		computed: {
			createDate() {
				return dayjs(this.item.createDate).fromNow();
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
	.ac {
		width: 750rpx;
		padding-top: 36rpx;
		padding-left: 30rpx;
		padding-right: 30rpx;
		padding-bottom: 16rpx;
		position: relative;
	}
</style>
