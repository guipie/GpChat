<template>
	<view :class="[reverse&&'mtc-reverse', 'myp-flex-row', 'myp-wrap-nowrap', 'mtc-left']">
		<image :src="avatar" mode="aspectFill"
			style="width: 60rpx;height: 60rpx;border-radius: 12rpx;margin-right: 8rpx;" @click="toUser()"></image>
		<!-- <image src="/static/left-angle.png" mode="aspectFill" style="width: 15rpx;height: 25rpx;"></image> -->
		<text :class="['mtc-text', 'myp-size-base', 'myp-lh-s', 'myp-color-text', 'mtc-text-left']"
			style="border-radius: 8rpx;" @longpress="toPressed">{{msg}}</text>
	</view>
</template>

<script>
	export default {
		props: {
			avatar: {
				type: String,
				default: ''
			},
			msg: {
				type: String,
				default: ''
			},
			reverse: {
				type: Boolean,
				default: false
			}
		},
		methods: {
			toPressed(e) {
				this.$emit("more")
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

		&-text {
			max-width: 554rpx;
			padding-left: 24rpx;
			padding-top: 24rpx;
			padding-right: 24rpx;
			padding-bottom: 24rpx;

			&-left {
				background-color: #FFFFFF;
				// box-shadow: 0 0 10rpx 0 rgba(0,0,0,0.03);
			}

			&-right {
				background-color: #7FD81F;
				// box-shadow: 0 0 10rpx 0 rgba(0,0,0,0.03);
			}
		}
	}
</style>
