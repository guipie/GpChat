<template>
	<myp-popup :show="show" pos="top-center" width="600rpx" height="820rpx" @overlayClicked="toCancel">
		<view class="pp">
			<view class="pp-top">
				<text class="pp-title">用户隐私政策说明</text>
				<text class="pp-desc">本《用户协议》及《隐私政策》将向你说明：</text>
				<text class="pp-desc">1.为帮助你注册帐号，浏览推荐发布内容、交流沟通，我们会收集你的部分必要信息；</text>
				<text class="pp-desc">2.为提供上述服务，我们可能会收集联络方式、位置、音视频等敏感信息，你有权拒绝或撤回授权；</text>
				<text class="pp-desc">3.未经你同意，我们不会从第三方获取、共享或对外提供你的信息；</text>
				<text class="pp-desc">4.你可以访问、更正、删除你的个人信息，我们也将提供注销、投诉方式。</text>
			</view>
			<view class="pp-hint">
				<text class="pp-hint-label">阅读查看</text>
				<text class="pp-hint-link" @tap="toPrivacy('service')">《用户协议》</text>
				<text class="pp-hint-link" @tap="toPrivacy('secret')">《隐私政策》</text>
			</view>
			<view class="pp-actions">
				<myp-button text="不同意" textType="second" radius="ll" boxStyle="width:236rpx;height:78rpx;" @buttonClicked="toReject"></myp-button>
				<myp-button text="同意" bgType="primary" textType="inverse" border="none" radius="ll" boxStyle="width:236rpx;height:78rpx;" @buttonClicked="toAgree"></myp-button>
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
			}
		},
		methods: {
			toCancel() {
				// this.$emit("cancel")
			},
			toReject() {
				this.$emit("reject")
			},
			toAgree() {
				this.$emit("agree")
			},
			toPrivacy(val) {
				this.$emit("privacy", val)
			}
		}
	}
</script>

<style lang="scss" scoped>
	
	.pp {
		width: 600rpx;
		height: 820rpx;
		border-radius: 20rpx;
		background-color: #FFFFFF;
		overflow: hidden;
		position: relative;
		
		&-top {
			width: 600rpx;
		}
		&-title {
			font-size: 36rpx;
			line-height: 45rpx;
			color: $myp-text-color;
			text-align: center;
			margin-top: 32rpx;
			font-weight: 600;
			margin-bottom: 20rpx;
		}
		&-desc {
			font-size: 30rpx;
			line-height: 42rpx;
			color: $myp-text-color-second;
			padding-left: 32rpx;
			padding-right: 32rpx;
			width: 600rpx;
		}
		&-hint {
			flex-direction: row;
			align-items: center;
			margin-top: 12rpx;
			margin-left: 32rpx;
			
			&-label {
				font-size: 30rpx;
				line-height: 42rpx;
				color: $myp-text-color-third;
				font-weight: 600;
			}
			&-link {
				font-size: 30rpx;
				line-height: 42rpx;
				color: $myp-color-link;
				font-weight: 600;
			}
		}
		
		&-actions {
			position: absolute;
			left: 48rpx;
			right: 48rpx;
			bottom: 38rpx;
			flex-direction: row;
			flex-wrap: nowrap;
			justify-content: space-between;
		}
	}
</style>
