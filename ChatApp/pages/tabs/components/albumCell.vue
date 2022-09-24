<template>
	<view class="ac myp-bg-inverse">
		<view class="myp-flex-row myp-align-center myp-justify-between">
			<view class="myp-flex-row myp-align-center" @tap="toUser()">
				<image :src="avatar" style="width: 64rpx;height: 64rpx;border-radius: 16rpx;" mode="aspectFill">
				</image>
				<text class="myp-size-s myp-color-text"
					style="margin-left: 20rpx;">{{(item.contentUser||{}).nickName}}</text>
			</view>
			<text v-if="forCared" class="myp-color-second"
				style="font-size: 10px;font-weight: 700;">{{createDateByCollect }}</text>
			<view v-else class="myp-flex-row myp-align-center">
				<view v-if="showFollow&&item.createID!=$store.getters.userId">
					<myp-button textSize="ss" radius="ll" :text="item.isFollowing?'已关注':'关注'"
						:textType="item.isFollowing?'':'inverse'" :bgType="item.isFollowing?'':'error'"
						:textStyle="item.isFollowing?'color: #A0A3B8;':'color: #ffffff;'"
						boxStyle="width:100rpx;height:40rpx;border:1rpx;border-color:#F3F3F6;"
						@buttonClicked="toFollow">
					</myp-button>
				</view>
				<view class="myp-border-all myp-flex-row myp-align-center myp-justify-center"
					style="margin-left: 12rpx;width: 30rpx;height: 22rpx;border-radius: 8rpx;border-color: #CCCFDB;"
					@touchstart="toClosePop">
					<myp-icon name="close1" :stop="false" type="third" iconStyle="font-size:8px;color:#8E979C;">
					</myp-icon>
				</view>
			</view>
		</view>
		<view style="height: 26rpx;" @click="toDetail"></view>
		<text class="myp-lines-two myp-color-text myp-size-s myp-lh-s" @click="toDetail"
			style="line-height: 50rpx;font-weight: 100;">{{item.contents}}</text>
		<view style="height: 16rpx;" @click="toDetail"></view>
		<view v-if="hasImages" class="myp-flex-row myp-wrap-wrap">
			<image v-for="(img,idx) in item.pics" :key="idx" :src="img.path"
				:style="'width:220rpx;height:220rpx;margin-bottom:15rpx;'+(idx%3==2?'margin-right:0;':'margin-right:15rpx;')"
				mode="aspectFill" @click="previewImage(item.pics.map(m=>m.path),idx)"></image>
		</view>
		<view v-if="hasImages" style="height: 16rpx;"></view>
		<!-- //转发内容 -->
		<albumCellParent v-if="item.parentGuid&&item.parent" :parentItem="item.parent" @detail="toParentDetail">
		</albumCellParent>
		<view v-if="!forCared" class="myp-flex-row myp-align-center myp-justify-end">
			<text class="myp-size-ss myp-color-third myp-lh-ss">{{createDate}}</text>
		</view>
		<view v-if="!forCared||(item.parentGuid&&item.parent)" style="height: 20rpx;"></view>
		<albumCellChat v-if="hasChat" :item='item' @sharePop="toSharePop" @collected="toCollected" @praised="toPraised"
			@xinPraised="toXinPraised" @chat="toChat">
		</albumCellChat>
		<albumCellComment v-else :item='item' @sharePop="toSharePop" @collected="toCollected" @praised="toPraised"
			@xinPraised="toXinPraised">
		</albumCellComment>
		<!-- 横线 -->
		<view class="myp-position-absolute"
			style="width: 690rpx;left: 30rpx;bottom: 0;height: 0.5px;background-color: #EFEFF4;"></view>
		<!-- 优质内容标识 -->
		<myp-icon name="success" v-if="(item.user||{}).super" iconStyle="color:#213093;" size="ss"
			boxStyle="position:absolute;top:76rpx;left:76rpx;">
		</myp-icon>
	</view>
</template>

<script>
	import albumCellChat from '@/pages/tabs/components/albumCellChat.vue';
	import albumCellComment from '@/pages/tabs/components/albumCellComment.vue';
	import albumCellParent from '@/pages/tabs/components/albumCellParent.vue';
	import {
		getTouchPoint
	} from '@/mypUI/utils/element.js'

	import imageHelper from '@/service/imageHelper.js'
	import {
		baseAvatar
	} from '@/common/env.js'

	import 'dayjs/locale/zh-cn'
	import dayjs from 'dayjs'; // ES 2015 
	dayjs.locale('zh-cn');
	import relativeTime from 'dayjs/plugin/relativeTime'
	import isBetween from 'dayjs/plugin/isBetween'
	dayjs.extend(relativeTime);
	dayjs.extend(isBetween);
	import {
		AlbumCollect,
		AlbumPraise,
		AlbumXinPraise
	} from '@/api/album/index.js'
	export default {
		components: {
			albumCellChat,
			albumCellComment,
			albumCellParent
		},
		mixins: [imageHelper],
		props: {
			hasChat: {
				type: Boolean,
				default: true
			},
			showFollow: { //关注按钮显示与否
				type: Boolean,
				default: false
			},
			forCared: { //收藏过来的
				type: Boolean,
				default: false
			},
			item: {
				type: Object,
				default: () => {
					return {}
				}
			},
			last: {
				type: Boolean,
				default: false
			},
			currentIndex: {
				type: Number,
				default: 0
			}
		},
		computed: {
			hasImages() {
				return (this.item.pics || []).length > 0
			},
			avatar() {
				return baseAvatar(this.item.contentUser?.avatar);
			},
			createDate() {
				if (dayjs(Date.now()).add(-4, 'day').isBefore(this.item.createDate))
					return dayjs(this.item.createDate).fromNow();
				return dayjs(this.item.createDate).format('YYYY-MM-DD HH:mm');
			},
			createDateByCollect() {
				return dayjs(this.item.createDate).format('YYYY-MM-DD');
			}
		},
		methods: {
			toDetail() {
				this.$emit("detail", this.item)
			},
			toParentDetail(parent) {
				this.$emit("detail", parent)
			},
			toUser() {
				uni.navigateTo({
					url: '/pages/album/userAlbums?userId=' + this.item.createID
				})
			},
			toCollected(item) {
				AlbumCollect(item.guid).then(res => {
					if (res.status) {
						item.isCollected = res.data > -1;
					}
				});
			},
			toPraised(item) {
				AlbumPraise(item.guid).then(res => {
					if (res.status) {
						item.praiseCount += new Number(res.data);
						item.isPraised = res.data > 0;
					}
				});
			},
			toXinPraised(item) {
				AlbumXinPraise(item.guid).then(res => {
					if (res.status) {
						item.xinPraiseCount += new Number(res.data);
						item.isXinPraised = res.data > 0;
					}
				});
			},
			toClosePop(e) {
				const p = getTouchPoint(e);
				p.type = 'album';
				p.item = this.item;
				this.$emit("pop", p);
			},
			toSharePop(e) {
				const p = getTouchPoint(e);
				p.type = 'share';
				p.item = this.item;
				this.$emit("pop", p);
			},
			toFollow() {
				this.$emit("follow", this.item);
			},
			toChat() {
				this.$emit("chat", this.item)
			}
		}
	}
</script>

<style lang="scss" scoped>
	.ac {
		width: 750rpx;
		padding-top: 36rpx;
		padding-left: 30rpx;
		padding-right: 30rpx;
		padding-bottom: 16rpx;
		position: relative;
	}
</style>
