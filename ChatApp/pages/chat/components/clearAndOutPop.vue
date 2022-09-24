<template>
	<myp-popup :show="show" height="280rpx" @overlayClicked="toCancel">
		<view class="cp myp-flex-column myp-align-center" style="width: 750rpx;height: 280rpx;">
			<view style="height: 10rpx;"></view>
			<view class="myp-flex-row myp-justify-center myp-align-center myp-border-bottom" style="width: 600rpx;height: 96rpx;" @click="toClear">
				<text class="myp-size-l myp-color-error">{{desc}}</text>
			</view>
			<view class="myp-flex-row myp-justify-center myp-align-center" style="width: 600rpx;height: 96rpx;" @click.stop="toCancel">
				<text class="myp-size-l myp-color-primary">取消</text>
			</view>
		</view>
	</myp-popup>
</template>

<script>
	export default {
		props: {
			show: {
				type: Boolean,
				default: false
			},
			desc: {
				type: String,
				default: '删除并清空聊天记录'
			}
		},
		methods: {
			toCancel() {
				this.$emit("cancel")
			},
			toClear() {
				this.$emit("clear")
			}
		}
	}
</script>

<style lang="scss" scoped>
	.cp {
		border-top-left-radius: 35rpx;
		border-top-right-radius: 35rpx;
		background-color: #FFFFFF;
	}
</style>
