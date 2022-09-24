<template>
	<view :class="[reverse&&'mtc-reverse', 'myp-flex-row', 'myp-wrap-nowrap', 'mtc-left']">
		<image :src="avatar" mode="aspectFill"
			style="width: 60rpx;height: 60rpx;border-radius: 12rpx;margin-right: 8rpx;" @click="toUser()"></image>
		<view class="mtc-voice myp-flex-row myp-align-center myp-bg-inverse" style="border-radius: 8rpx;" bubble="true"
			@tap="toPlay">
			<myp-icon :name="current==src?'stop':'play'" type="error" size="ll" @iconClicked="toPlay"></myp-icon>
			<text class="myp-size-ss"
				style="font-weight: 700;margin-right: 40rpx;margin-left: 20rpx;font-size: 28rpx;color: #9092A5;">{{current==src?playTime:'00:00'}}</text>
			<text class="myp-size-ss"
				style="font-weight: 700;margin-left: 30rpx;font-size: 28rpx;color: #C7C7C7;">{{time}}</text>
		</view>
	</view>
</template>

<script>
	export default {
		props: {
			avatar: {
				type: String,
				default: ''
			},
			src: {
				type: String,
				default: ''
			},
			time: {
				type: String,
				default: " 00:00"
			},
			playTime: {
				type: String,
				default: "00:00"
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
			toPlay() {
				this.$emit("play", this.src)
			},
			toUser() {
				this.$emit("userDetail");
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
			padding-left: 40rpx;
			padding-right: 24rpx;
			padding-top: 16rpx;
			padding-bottom: 16rpx;
		}
	}
</style>
