<template>
	<myp-popup :show="show" :hasOverlay="false" :height="(height||120)+'px'" width="750rpx" :duration="250">
		<view class="myp-bg-page myp-flex-row myp-wrap-wrap"
			:style="'width: 750rpx;height:'+(height||120)+'px;padding-top:10px;'">
			<view class="myp-flex-column myp-align-center myp-justify-start" style="margin-left: 62rpx;width: 112rpx;"
				bubble="true" @tap="toCollect">
				<myp-icon name="fav-fill" type="second" iconStyle="font-size:72rpx;opacity: 0.8;"
					@iconClicked="toCollect"></myp-icon>
				<text class="myp-size-s myp-lh-s myp-color-second" style="font-weight: 700;">收藏</text>
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
			height: {
				type: Number,
				default: 0
			}
		},
		methods: {
			toCollect() {
				this.$emit("collect")
			}
		}
	}
</script>

<style>
</style>
