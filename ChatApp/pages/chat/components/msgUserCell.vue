<template>
	<view :class="[reverse&&'mtc-reverse', 'myp-flex-row', 'myp-wrap-nowrap', isLeft?'mtc-left':'mtc-right']">
		<image v-if="isLeft" :src="avatar" mode="aspectFill" style="width: 60rpx;height: 60rpx;border-radius: 12rpx;margin-right: 8rpx;"></image>
		<view class="mtc-voice" bubble="true" @tap="toUser">
			<view class="myp-flex-row myp-wrap-nowrap myp-align-center">
				<image src="/static/logo-dark.png" mode="aspectFill" style="width: 88rpx;height: 88rpx;border-radius: 16rpx;"></image>
				<text class="myp-size-l myp-color-text myp-lines-one" style="margin-left: 24rpx;width: 160rpx;"></text>
			</view>
			<view class="myp-bg-inverse" style="width: 250rpx;height: 1px;margin-top: 6rpx;"></view>
			<view class="myp-flex-row myp-align-center" style="height: 60rpx;">
				<text class="myp-color-second myp-size-ss">与ta联系</text>
			</view>
		</view>
		<image v-if="!isLeft" :src="avatar" mode="aspectFill" style="width: 60rpx;height: 60rpx;border-radius: 12rpx;margin-left: 8rpx;"></image>
	</view>
</template>

<script>
	export default {
		props: {
			avatar: {
				type: String,
				default: ''
			},
			user: {
				type: Object,
				default: ()=>{
					return {}
				}
			},
			isLeft: {
				type: Boolean,
				default: true
			},
			reverse: {
				type: Boolean,
				default: false
			},
			current: {
				type: String,
				default: ''
			}
		},
		methods: {
			toUser() {
				
			}
		}
	}
</script>

<style lang="scss" scoped>
	.mtc {
		&-reverse {
			transform: rotateX(180deg);
		}
		&-left {
			margin-left: 30rpx;
			justify-content: flex-start;
		}
		&-right {
			margin-right: 30rpx;
			justify-content: flex-end;
		}
		&-voice {
			width: 300rpx;
			padding-left: 30rpx;
			padding-right: 24rpx;
			padding-top: 16rpx;
			background-color: $myp-color-success;
		}
	}
</style>
