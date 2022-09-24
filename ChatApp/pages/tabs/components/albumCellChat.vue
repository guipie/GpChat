<template>
	<view class="myp-flex-row myp-align-center myp-justify-between">
		<view class="myp-flex-row myp-align-center">
			<view class="myp-flex-row myp-align-center" style="width: 140rpx;" @tap="toCollected">
				<myp-icon :name="item.isCollected?'fav-fill':'fav'" type="second" size="l"
					:iconStyle="item.isCollected?'color:#3F7600':'color:#AAAAAA'" @iconClicked="toCollected">
				</myp-icon>
				<text class="myp-size-ss myp-lh-ss myp-color-second"
					:style="item.isCollected?'color:#000000':'color:#AAAAAA'" style="margin-left: 12rpx;">收藏</text>
			</view>
			<view class="myp-flex-row myp-align-center" style="width: 140rpx;" @touchstart="toSharePop">
				<myp-icon name="share" type="second" size="l" :stop="false"
					:iconStyle="item.isShared?'color:#f78063':'color:#AAAAAA'">
				</myp-icon>
				<text class="myp-size-ss myp-lh-ss myp-color-second"
					:style="item.isShared?'color:#f78063':'color:#AAAAAA'"
					style="margin-left: 12rpx;">{{item.shareCount>0?item.shareCount:'转发'}}</text>
			</view>
			<view class="myp-flex-row myp-align-center" style="width: 140rpx;" @tap="toXinPraised">
				<myp-icon :name="item.isXinPraised?'hand-solid':'hand'" type="second" size="l"
					:iconStyle="item.isXinPraised?'color:#FFAF49':'color:#AAAAAA'" @iconClicked="toXinPraised">
				</myp-icon>
				<text class="myp-size-ss myp-lh-ss myp-color-second"
					:style="item.isXinPraised?'color:#FFAF49':'color:#AAAAAA'"
					style="margin-left: 12rpx;">{{item.xinPraiseCount>0?item.xinPraiseCount:'新赞'}}</text>
			</view>
			<view class="myp-flex-row myp-align-center" style="width: 140rpx;" @tap="toPraised">
				<myp-icon name="yes" type="second" size="l" :iconStyle="item.isPraised?'color:#F7412D':'color:#AAAAAA'"
					@iconClicked="toPraised">
				</myp-icon>
				<text class="myp-size-ss myp-lh-ss myp-color-second"
					:style="item.isPraised?'color:#F7412D':'color:#AAAAAA'"
					style="margin-left: 12rpx;">{{item.praiseCount>0?item.praiseCount:'点赞'}}</text>
			</view>
		</view>
		<myp-button bgType="success" text="聊天" textType="inverse" textSize="ss" radius="s"
			boxStyle="width:100rpx;height:44rpx;margin-right:20rpx;" @buttonClicked="toChat"></myp-button>
	</view>
</template>

<script>
	export default {
		props: {
			item: {
				type: Object,
				default: {}
			}
		},
		methods: {
			toCollected() {
				this.$emit('collected', this.item)
			},
			toPraised() {
				this.$emit('praised', this.item)
			},
			toXinPraised() {
				this.$emit('xinPraised', this.item);
			},
			toSharePop(e) {
				this.$emit("sharePop", e);
			},
			toChat() {
				this.$emit("chat");
			}
		}
	}
</script>

<style>
</style>
