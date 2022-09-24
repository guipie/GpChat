<template>
	<view class="ac">
		<view class="myp-flex-row myp-align-center" v-if="userId<=0" @tap="toFriend(item.userId)">
			<image :src="avatar" mode="aspectFill" style="width: 64rpx;height: 64rpx;border-radius: 12rpx;"></image>
			<text class="myp-size-s myp-color-text"
				style="font-weight: 700;margin-left: 20rpx;">{{item.nickName}}</text>
		</view>
		<text class="myp-size-base myp-lh-s myp-color-text myp-lines-two" style="margin-top: 18rpx;"
			@tap="toDetail">{{item.contents}}</text>
		<view class="myp-flex-row myp-wrap-wrap" style="margin-top: 18rpx;">
			<image v-for="(img,idx) in item.pics" :key="idx" :src="img" mode="aspectFill" @tap="toPreview(idx)"
				:style="'width: 220rpx;height: 200rpx;margin-bottom:15rpx;'+((idx+1)%3===0?'margin-right:0;':'margin-right:15rpx;')">
			</image>
		</view>
		<!-- 转发内容 -->
		<view v-if='item.parentGuid&&item.parent' class="myp-flex-row myp-align-center" @click="toContentDetail">
			<view class="myp-flex-row myp-wrap-wrap">
				<image :src="item.parent.pic" v-if="item.parent.pic" mode="aspectFill"
					style="width: 120rpx;height: 120rpx;">
				</image>
				<view class="myp-flex-one" style="background-color: #F7F8FA;">
					<text class="myp-size-base myp-color-text myp-lh-s myp-lines-two"
						style="width: 520rpx;margin-left: 24rpx;margin-top: 12rpx;">{{item.parent.contents}}</text>
				</view>
			</view>
		</view>
		<view class="myp-flex-row myp-align-center myp-justify-between">
			<text class="myp-color-second" style="font-size: 20rpx;font-weight: 700;line-height: 28rpx;">
				<!-- 北京·海淀 -->
			</text>
			<text class="myp-color-second"
				style="font-size: 20rpx;font-weight: 700;line-height: 28rpx;">{{fmtDate}}</text>
		</view>
		<view style="height: 16rpx;"></view>
		<view class="myp-flex-row myp-align-center myp-justify-end">
			<!-- 正常应该受外部item控制 -->
			<!-- <myp-icon name="yes" type="error" size="ll"></myp-icon> -->
			<myp-icon name="yes" size="l" :iconStyle="isPraised?'color:#F7412D':'color:#9092A5'"
				@iconClicked="toPraised">
			</myp-icon>
			<view style="width: 52rpx;"></view>
			<myp-icon name="chat" type="second" size="l" :iconStyle="isCommented?'color:#0086FF':''"
				@iconClicked="toComment"></myp-icon>
		</view>
		<view class="ac-tc" v-if="comments.length>0||praises.length>0">
			<view v-if="praises.length>0" class="myp-flex-row myp-wrap-nowrap myp-align-start"
				:class="comments.length>0?'myp-border-bottom':''" style="padding-bottom: 12rpx;">
				<myp-icon name="yes" type="second" size="s" :iconStyle="'color:#AAAAAA'">
				</myp-icon>
				<view class="myp-flex-row myp-wrap-wrap" style="margin-left: 16rpx;width: 596rpx;">
					<text v-for="(i,idx) in praises" :key="idx" class="myp-size-s myp-lh-s"
						style="margin-right: 24rpx;margin-bottom: 4rpx; color: #0070F5;font-size: 28rpx;"
						@tap="toFriend(i.userId)">{{i.nickName}}</text>
				</view>
			</view>
			<view style="height: 20rpx;" v-if="comments.length>0"></view>
			<view v-if="comments.length>0" class="myp-flex-row myp-wrap-nowrap myp-align-start">
				<myp-icon name="chat" size="s" type="second"></myp-icon>
				<view style="margin-left: 16rpx;width: 596rpx;">
					<comment-text v-for="(i,idx) in comments" :key="idx" :item="i" @reply="toReply" @friend="toFriend">
					</comment-text>
				</view>
			</view>
		</view>
		<view style="height: 16rpx;"></view>
		<view style="width: 690rpx;height: 0.5px;background-color: #EFEFF4;"></view>
	</view>
</template>

<script>
	import commentText from './commentText.vue'
	import {
		FriendCommentsPost,
		FriendPraisesPost
	} from '@/api/album/index.js'
	export default {
		components: {
			commentText
		},
		props: {
			item: {
				type: Object,
				default: () => {
					return {}
				}
			},
			userId: { //朋友圈内容的创建人
				type: Number,
				default: 0
			},
			myUser: { //我自己
				type: Number,
				default: 0
			},
			fmtDate: {
				type: String,
				default: '日期'
			},
			avatar: ""
		},
		data() {
			return {
				// 仅仅是示范点击切换状态
				thumbed: Math.floor(Math.random() * 2) > 0
			}
		},
		computed: {
			comments() {
				return (this.item.comments || []).map(m => {
					if (m.parentId > 0)
						m.parent = this.item.comments.find(mm => mm.id == m.parentId);
					return m;
				});
			},
			praises() {
				return this.item.praiseUsers || [];
			},
			isPraised() {
				return this.praises.findIndex(m => m.userId == this.myUser.userId) > -1;
			},
			isCommented() {
				return this.comments.findIndex(m => m.userId == this.myUser.userId) > -1;
			}
		},
		methods: {
			toPraised() {
				if (this.isPraised)
					this.item.praiseUsers.splice(this.praises.findIndex(m => m.userId == this.myUser.userId), 1);
				else {
					this.item.praiseUsers = this.item.praiseUsers || [];
					this.item.praiseUsers.push({
						"userId": this.myUser.userId,
						"nickName": this.myUser.nickName
					});
				}
				this.$emit('praised', this.item)
			},
			toDetail() {
				this.$emit("detail", this.item)
			},
			toContentDetail() {
				this.$emit("contentDetail", this.item.parentGuid)
			},
			toComment() {
				this.$emit("comment", this.item);
			},
			toReply(commentItem) {
				this.item.replyItem = commentItem;
				this.$emit("comment", this.item);
			},
			toFriend(userId) {
				this.$emit("friend", userId);
			},
			toPreview(index) {
				this.$emit("preview", this.item.pics, index);
			}
		}
	}
</script>

<style lang="scss" scoped>
	.ac {
		width: 750rpx;
		background-color: #FFFFFF;
		padding-left: 30rpx;
		padding-right: 30rpx;
		padding-top: 32rpx;

		&-tc {
			background-color: #FAFBFC;
			padding-left: 24rpx;
			padding-right: 24rpx;
			padding-top: 36rpx;
			padding-bottom: 26rpx;
		}
	}
</style>
