<template>
	<view class="myp-flex-row myp-align-center">
		<!-- 收藏 -->
		<view class="myp-flex-row myp-align-center myp-flex-one" @touchstart="toCollected">
			<myp-icon :name="item.isCollected?'fav-fill':'fav'" type="second" size="l"
				:iconStyle="item.isCollected?'color:#3F7600':'color:#AAAAAA'">
			</myp-icon>
			<text class="myp-size-ss myp-lh-ss myp-color-second"
				:style="item.isCollected?'color:#000000':'color:#AAAAAA'" style="margin-left: 12rpx;">收藏</text>
		</view>
		<!-- 分享 -->
		<view class="myp-flex-row myp-align-center myp-flex-one" @touchstart="toSharePop">
			<myp-icon name="share" type="second" size="l" :stop="false"
				:iconStyle="item.isShared?'color:#f78063':'color:#AAAAAA'">
			</myp-icon>
			<text class="myp-size-ss myp-lh-ss myp-color-second" :style="item.isShared?'color:#f78063':'color:#AAAAAA'"
				style="margin-left: 12rpx;">{{item.shareCount>0?item.shareCount:'转发'}}</text>
		</view>
		<!-- 新赞 -->
		<view class="myp-flex-row myp-align-center myp-flex-one" @tap="toXinPraised">
			<myp-icon :name="item.isXinPraised?'hand-solid':'hand'" type="second" size="l"
				:iconStyle="item.isXinPraised?'color:#FFAF49':'color:#AAAAAA'" @iconClicked="toXinPraised">
			</myp-icon>
			<text class="myp-size-ss myp-lh-ss myp-color-second"
				:style="item.isXinPraised?'color:#FFAF49':'color:#AAAAAA'"
				style="margin-left: 12rpx;">{{item.xinPraiseCount>0?item.xinPraiseCount:'新赞'}}</text>
		</view>
		<!-- 点赞 -->
		<view class="myp-flex-row myp-align-center myp-flex-one" @tap="toPraised()">
			<myp-icon name="yes" type="second" size="l" @iconClicked="toPraised()"
				:iconStyle="item.isPraised?'color:#F7412D':'color:#AAAAAA'">
			</myp-icon>
			<text class="myp-size-ss myp-lh-ss myp-color-second" :style="item.isPraised?'color:#F7412D':'color:#AAAAAA'"
				style="margin-left: 12rpx;">{{item.praiseCount>0?item.praiseCount:'点赞'}}</text>
		</view>
		<!-- 评论 -->
		<view class="myp-flex-row myp-align-center myp-flex-one" @tap="toComment()">
			<myp-icon name="chat" type="second" size="l" :iconStyle="item.isCommented?'color:#0086FF':'color:#AAAAAA'"
				@iconClicked="toComment()">
			</myp-icon>
			<text class="myp-size-ss myp-lh-ss myp-color-second"
				:style="item.isCommented?'color:#000000':'color:#AAAAAA'"
				style="margin-left: 12rpx;">{{item.commentCount>0?item.commentCount:'评论'}}</text>
		</view>
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
			toSharePop(e) {
				this.$emit("sharePop", e);
			},
			toCollected() {
				this.$emit('collected', this.item)
			},
			toPraised() {
				this.$emit('praised', this.item)
			},
			toXinPraised() {
				this.$emit('xinPraised', this.item);
			},
			toComment() {
				uni.navigateTo({
					url: '/pages/album/albumDetail?comment=true&guid=' + this.item.guid
				});
			}
		}
	}
</script>

<style>
</style>
