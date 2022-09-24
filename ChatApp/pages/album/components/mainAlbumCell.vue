<template>
	<view class="mac myp-bg-page">
		<view class="myp-flex-row myp-align-center myp-justify-between">
			<view class="myp-flex-row myp-align-center">
				<text class="myp-color-second myp-size-s myp-lh-s" style="width: 87rpx;" v-if="item.location">在</text>
				<text class="myp-color-text myp-size-s myp-lh-s">{{item.location}}</text>
			</view>
			<view class="myp-flex-row myp-align-center" style="margin-right: 24rpx;">
				<text class="myp-color-third myp-size-ss">{{item.beginDate}} - {{item.endDate}}</text>
				<view class="myp-border-all myp-flex-row myp-align-center myp-justify-center"
					style="margin-left: 12rpx;width: 30rpx;height: 22rpx;border-radius: 8rpx;border-color: #CCCFDB;"
					@touchstart="toPopOptions">
					<myp-icon name="close1" :stop="false" type="third" iconStyle="font-size:8px;color:#8E979C;">
					</myp-icon>
				</view>
			</view>
		</view>
		<view class="myp-flex-row myp-wrap-nowrap">
			<text class="myp-color-second myp-size-s myp-lh-s" style="width: 87rpx;" v-if="item.introduction">经历</text>
			<text class="myp-color-text myp-size-s myp-lh-s" style="width: 592rpx;">{{item.introduction}}</text>
		</view>
		<view style="height: 16rpx;"></view>
		<view v-if="item.pics.length>0" class="myp-flex-row myp-wrap-wrap">
			<image v-for="(img,idx) in item.pics" :key="idx" :src="img.uploadPath"
				:style="'width:220rpx;height:220rpx;margin-bottom:15rpx;'+(idx%3==2?'margin-right:0;':'margin-right:15rpx;')"
				mode="aspectFill" @tap="previewImage(item.pics.map(m=>m.uploadPath),idx)"></image>
		</view>
		<view v-if="item.pics.length>0" style="height: 16rpx;"></view>
		<view class="myp-flex-row myp-align-center myp-justify-end" style="margin-right: 24rpx;">
			<myp-icon name="yes" type="error" size="s" :iconStyle="item.isPraised?'color:#F7412D':'color:#AAAAAA'"
				@iconClicked="praised">
			</myp-icon>
			<text class="myp-color-second myp-size-s myp-lh-s" style="margin-left: 12rpx;">{{item.praiseCount}}</text>
		</view>
	</view>
</template>

<script>
	import {
		getTouchPoint
	} from '@/mypUI/utils/element.js'
	import {
		IntroPraised
	} from '@/api/album/intro.js'
	export default {
		props: {
			item: {
				type: Object,
				default: () => {
					return {}
				}
			}
		},
		computed: {},
		methods: {
			toPopOptions(e) {
				const p = getTouchPoint(e);
				p.type = 'intro';
				p.item = this.item;
				this.$emit("introPop", p);
			},
			praised() {
				IntroPraised(this.item.introGuid).then(res => {
					if (res.status) {
						this.item.praiseCount += res.data;
						this.item.isPraised = res.data > 0;
					}
				});
			}
		}
	}
</script>

<style lang="scss" scoped>
	.mac {
		margin-left: 10rpx;
		margin-bottom: 30rpx;
		background-color: #F2F2F3;
		width: 730rpx;
		border-radius: 32rpx;
		padding-top: 30rpx;
		padding-left: 24rpx;
		padding-bottom: 16rpx;
	}
</style>
