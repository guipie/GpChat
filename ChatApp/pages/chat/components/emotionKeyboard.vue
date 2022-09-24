<template>
	<myp-popup :show="show" :hasOverlay="false" height="160px" width="750rpx" :duration="250">
		<scroll-view :scroll-y="true" class="myp-bg-page" style="width: 750rpx;height:160px;">
			<view class="myp-flex-row myp-wrap-wrap">
				<text v-for="(emj,idx) in emotions" :key="idx" style="width: 60rpx;text-align: center;font-size: 30rpx;line-height: 42rpx;margin-left: 30rpx;margin-top: 30rpx;" @tap="toSelect(emj)">{{emj}}</text>
			</view>
			<view style="width: 750rpx;height: 90rpx;"></view>
		</scroll-view>
		<myp-button bgType="inverse" radius="s" icon="close" boxStyle="position:absolute;right:0;bottom:30rpx;width:100rpx;height:60rpx;" @buttonClicked="toDelete"></myp-button>
	</myp-popup>
</template>

<script>
	import emotions from '@/common/emotions.js'
	
	export default {
		props: {
			show: {
				type: Boolean,
				default: false
			}
		},
		data() {
			return {
				emotions: emotions
			}
		},
		methods: {
			toDelete() {
				this.$emit("delete")
			},
			toSelect(val) {
				this.$emit("select", val)
			}
		}
	}
</script>

<style>
</style>
