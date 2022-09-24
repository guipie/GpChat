<template>
	<view class="it myp-flex-row myp-justify-center myp-align-center" bubble="true" @tap="toAdd">
		<myp-icon name="plus" type="third" iconStyle="font-size: 64rpx;" @iconClicked="toAdd"></myp-icon>
	</view>
</template>

<script>
	export default {
		methods: {
			toAdd() {
				this.$emit("add")
			}
		}
	}
</script>

<style lang="scss" scoped>
	.it {
		background-color: #EFEFF4;
		width: 220rpx;
		height: 220rpx;
		border-radius: 2rpx;
	}
</style>
