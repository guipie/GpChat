<template>
	<view class="myp-bg-inverse myp-flex-row myp-align-center"
		style="width: 750rpx;padding-left: 30rpx;padding-right: 30rpx;" bubble="true" @tap="toDetail">
		<view :class="[!last&&'myp-border-bottom', 'myp-flex-one', 'myp-flex-column', 'myp-justify-center']"
			style="height: 148rpx;">
			<text class="myp-size-base myp-color-text myp-lh-base"
				style="font-weight: 700;">{{item.name}}({{item.memberCount}})</text>
			<view style="height: 12rpx;"></view>
			<text class="myp-lines-one myp-color-second myp-size-s myp-lh-s">{{nameStr}}</text>
		</view>
	</view>
</template>

<script>
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
			}
		},
		computed: {
			nameStr() {
				return (this.item.members || []).join('   ')
			}
		},
		methods: {
			toDetail() {
				this.$emit("detail", this.item)
			}
		}
	}
</script>

<style>
</style>
