<template>
	<view style="margin-bottom: 8rpx;">
		<!-- 第一行 -->
		<view class="myp-flex-row myp-align-center myp-wrap-nowrap" style="width: 598rpx;" @tap="toReply">
			<text class="myp-size-s myp-lh-s" style="color: #0070F5;font-size: 28rpx;"
				@tap="toFriend(item.userId)">{{item.nickName}}:</text>
			<text class="myp-size-s myp-lh-s myp-color-text" v-if="item.parentId>0">回复</text>
			<text class="myp-size-s myp-lh-s" style="color: #0070F5;font-size: 28rpx;"
				v-if="item.parentId>0&&item.parent" @tap="toFriend(item.parent.userId)">{{item.parent.nickName}}:</text>
			<text class="myp-size-s myp-lh-s myp-color-text">{{item.contents}}</text>
		</view>
		<!-- 其它行 -->
		<!-- <text class="myp-size-s myp-lh-s myp-color-text" style="width: 598rpx;">{{item.contents}}</text> -->
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
			}
		},
		data() {
			return {

			}
		},
		computed: {
			name1() {
				// 一个名字最多显示8哥字
				return '风刀霜剑分'
			},
			name2() {
				console.log(this.item);
				return '金葵花卡'
			},
			oneText() {
				// 名字和文字共18个字
				return '阿娇开放后可恢复的'
			},
			otherText() {
				return '饭卡绝代风华肯定会发快递费那开发计划的开发哈佛女孩哭似懂非懂发的啥饭'
			}
		},
		methods: {
			toReply() {
				this.$emit("reply", this.item);
			},
			toFriend(userId) {
				this.$emit("friend", userId);
			}
		}
	}
</script>

<style>
</style>
