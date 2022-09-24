<template>
	<view :class="[reverse&&'mtc-reverse', 'myp-flex-row', 'myp-wrap-nowrap', 'mtc-right']">
		<image :src="src" mode="aspectFill" style="width: 240rpx;height: 240rpx;border-radius: 8rpx;"
			@click="toPreview">
		</image>
		<image :src="avatar" mode="aspectFill"
			style="width: 60rpx;height: 60rpx;border-radius: 12rpx;margin-left: 8rpx;" @click="toUser()"></image>
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
			reverse: {
				type: Boolean,
				default: false
			}
		},
		methods: {
			toPreview() {
				this.$emit('preview', this.src);
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

		&-right {
			margin-right: 30rpx;
			justify-content: flex-end;
		}
	}
</style>
