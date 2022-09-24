<template>
	<view class="aic">
		<image class="aic-img" :src="src" mode="aspectFill" @tap="toImage"></image>
		<view class="aic-close">
			<myp-icon name="close1" type="inverse" size="ss" iconStyle="opacity:0.5;" @iconClicked="toDelete">
			</myp-icon>
		</view>
	</view>
</template>

<script>
	export default {
		props: {
			src: {
				type: String,
				default: ''
			}
		},
		methods: {
			toImage() {
				this.$emit("preview", this.src)
			},
			toDelete() {
				this.$emit("delete", this.src)
			}
		}
	}
</script>

<style lang="scss" scoped>
	.aic {
		width: 220rpx;
		height: 220rpx;
		overflow: hidden;
		position: relative;

		&-img {
			width: 220rpx;
			height: 220rpx;
			border-radius: 2rpx;
		}

		&-close {
			position: absolute;
			top: 2rpx;
			right: 2rpx;
		}
	}
</style>
