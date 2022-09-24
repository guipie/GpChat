<template>
	<view :class="[reverse&&'mtc-reverse', 'myp-flex-row', 'myp-wrap-nowrap', 'mtc-left']">
		<image :src="avatar" mode="aspectFill"
			style="width: 60rpx;height: 60rpx;border-radius: 12rpx;margin-right: 8rpx;" @click="toUser()"></image>
		<view class="mtc-text myp-flex-row myp-align-center myp-bg-inverse" style="border-radius: 8rpx;"
			@click="toDetail">
			<image :src="contentItem.Pic" v-if="contentItem.Pic" mode="aspectFill"
				style="width: 120rpx;height: 120rpx;"></image>
			<view class="myp-flex-one">
				<text class="myp-size-s myp-color-text myp-lh-s myp-lines-two"
					style="padding-left: 20rpx;">{{contentItem.Content}}</text>
			</view>
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
			contentItem: {
				type: Object,
				default: {}
			},
			contentId: {
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
			toDetail() {
				this.$emit("detail", this.contentId);
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
