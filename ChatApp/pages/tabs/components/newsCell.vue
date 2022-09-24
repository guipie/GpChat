<template>
	<view class="myp-flex-row myp-align-center myp-bg-inverse myp-wrap-nowrap myp-position-relative"
		:style="'width:886rpx;'+aniStyle">
		<view class="myp-flex-row myp-align-center myp-bg-inverse myp-wrap-nowrap myp-position-relative"
			style="width: 750rpx;padding-left: 30rpx;" :style="item.Top?'background-color: #f8f8f8;':''" bubble="true"
			@touchstart="onTouchStart" @touchend="onTouchEnd">
			<view style="width: 80rpx;height: 80rpx;border-radius: 20rpx;">
				<view v-if='item.IsGroup===1'
					class="myp-flex-row myp-wrap-wrap myp-bg-inverse myp-align-center myp-justify-center"
					style="width: 80rpx;height: 80rpx;background-color: #f0f0f0;border-radius: 10rpx;">
					<image v-for="(u,index) in avatars" lazy-load='true' :key="index" :src="u" mode="aspectFill"
						style="width: 23rpx;height:23rpx;border-radius: 10rpx;margin: 2rpx;">
					</image>
				</view>
				<image v-else :src="avatars[0]" style="width: 80rpx;height: 80rpx;border-radius: 20rpx;"
					mode="aspectFill">
				</image>
			</view>
			<view class="myp-flex-column myp-justify-between myp-flex-one"
				style="margin-left: 24rpx;padding-right: 30rpx; height: 136rpx;">
				<view style="height: 0.5px;"></view>
				<view>
					<view class="myp-flex-row myp-align-center myp-justify-between">
						<view class="myp-flex-row myp-align-center">
							<text class="myp-color-text myp-size-l myp-lh-l"
								style="font-size:32rpx;color: #3A444A;">{{item.Name}}</text>
							<view v-if="isTemp" class="nc-temp myp-flex-row myp-align-center myp-justify-center"
								style="margin-left: 20rpx;">
								<text class="myp-color-inverse" style="font-size: 20rpx;line-height: 28rpx;">临时</text>
							</view>
						</view>
						<text class="myp-color-third myp-size-ss myp-lh-ss">{{SendTimeText}}</text>
					</view>
					<view style="height: 4rpx;"></view>
					<view class="myp-flex-row myp-align-center myp-justify-between">
						<text
							class="myp-color-second myp-size-s myp-lh-s">{{(item.UnReadCount>0?'['+item.UnReadCount+'条]':'')+content}}</text>
						<myp-icon v-if="item.NotDisturb" name="bell-off" type="third" size="ss"></myp-icon>
					</view>
				</view>
				<view :style="'height: 0.5px;'+('background-color:#EFEFF4;')"></view>
			</view>
			<!-- 	<view v-if="item.UnReadCount>0"
				class="myp-position-absolute myp-bg-error myp-flex-row myp-justify-center myp-align-center"
				style="width: 21rpx;height: 21rpx;border-radius: 12rpx;border-width:1px;border-color: #FFFFFF;left: 100rpx;top: 22rpx;">
				 <text class="myp-color-inverse myp-size-ss">{{item.unread}}</text>  
			</view> -->
			<view v-if="item.UnReadCount>0"
				class="myp-position-absolute myp-bg-error myp-flex-row myp-justify-center myp-align-center"
				style="width: 30rpx;height:30rpx;border-radius: 20rpx;border-width:0.5px;border-color: #FFFFFF;left: 96rpx;top: 18rpx;">
				<text class="myp-color-inverse myp-size-ss" style="font-size: 22rpx;"
					v-if="item.UnReadCount>0">{{item.NotDisturb?'':item.UnReadCount}}</text>
			</view>
		</view>
		<view class="myp-bg-error myp-flex-row myp-align-center myp-justify-center"
			style="width: 136rpx;height: 136rpx;" @click="toDelete">
			<text class="myp-color-inverse myp-size-l">删除</text>
		</view>
	</view>
</template>

<script>
	import {
		getTouchPoint
	} from '@/mypUI/utils/element.js'
	import {
		formatTime
	} from '@/mypUI/utils/date.js'
	const xDistance = 30
	export default {
		props: {
			item: {
				type: Object,
				default: () => {
					return {}
				}
			},
			last: {
				type: Boolean,
				default: false
			},
			isTemp: {
				type: Boolean,
				default: false
			},
			avatars: {
				type: Array,
				default: []
			}
		},
		data() {
			return {
				aniStyle: '',
				w: uni.upx2px(136)
			}
		},
		computed: {
			SendTimeText() {
				return formatTime(this.item.SendTime || '', '{y}-{m}-{d} {h}:{i}')
			},
			content() {
				if (this.item.IsSys)
					return this.item.Disable ? '您已经停用了此通知' : this.item.Content;
				if (this.item.Type === 2)
					return "[图片]"
				else if (this.item.Type === 3)
					return "[语音]"
				else if (this.item.Type === 4)
					return "[分享内容]"
				else
					return this.item.Content;
			}
		},
		methods: {
			toDetail() {
				this.$emit("detail", this.item)
			},
			toDelete() {
				this.$emit("delete", this.item)
				this.aniStyle = `transform:translateX(0px);`
			},
			onTouchStart(e) {
				this.startPoint = getTouchPoint(e)
			},
			onTouchEnd(e) {
				const now = getTouchPoint(e)
				const offsetX = now.x - this.startPoint.x
				if (offsetX <= -1 * xDistance) {
					// 打开
					this.aniStyle = `transform:translateX(-${this.w}px);`
				} else if (offsetX >= xDistance) {
					// 关闭
					this.aniStyle = `transform:translateX(0px);`
				} else {
					this.aniStyle = `transform:translateX(0px);`
					this.toDetail()
				}
			}
		}
	}
</script>

<style lang="scss" scoped>
	.nc {
		&-temp {
			width: 90rpx;
			height: 32rpx;
			border-radius: 32rpx;
			background-image: linear-gradient(to right, #00C3F7, #4074E3);
		}
	}
</style>
