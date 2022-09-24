<template>
	<view class="itl myp-flex-row myp-wrap-nowrap myp-align-end">
		<myp-icon name="add-pic" type="second" iconStyle="font-size:42rpx;" boxStyle="height:60rpx;"
			@iconClicked="toSelectImage">
		</myp-icon>
		<view style="width: 32rpx;"></view>
		<myp-icon name="mic" type="second" iconStyle="font-size:42rpx;" boxStyle="height:60rpx;" @iconClicked="toVoice">
		</myp-icon>
		<view style="width: 32rpx;"></view>
		<textarea v-model="msg" class="itl-area myp-flex-one" placeholder="联系建立友谊" :style="'height:'+areaHeight+'px;'"
			:maxlength="1000" :adjust-position="false" confirm-type="send" @linechange="toChangeLine"
			@keyboardheightchange="toChangeKb" @confirm="toConfirm"></textarea>
		<view style="width: 32rpx;"></view>
		<!--
		<myp-icon name="smile" type="second" size="l" boxStyle="height:60rpx;" @iconClicked="toEmotion"></myp-icon>
		<view style="width: 32rpx;"></view>
		-->
		<myp-button v-if="sendVisible" text="发送" textType="inverse" textSize="s" bgType="success" border="none"
			radius="base" boxStyle="width:90rpx;height:60rpx;" @buttonClicked="toConfirm"></myp-button>
		<view v-else class="myp-flex-row myp-align-center myp-justify-center myp-position-relative myp-overflow-hidden"
			style="width: 46rpx;height: 60rpx;" bubble="true" @click="toPlus">
			<view class="myp-position-absolute"
				style="border-radius: 40rpx;left: 2rpx;top: 9rpx;width: 42rpx;height: 42rpx;"></view>
			<myp-icon name="plus-circle" type="second" iconStyle="font-size:44rpx;" @iconClicked="toPlus"></myp-icon>
		</view>
	</view>
</template>

<script>
	import emotions from '@/common/emotions.js'

	export default {
		data() {
			return {
				areaHeight: uni.upx2px(60),
				voiceHeight: uni.upx2px(60),
				msg: ''
			}
		},
		computed: {
			sendVisible() {
				if (!this.msg) {
					return false
				}
				return this.msg.trim().length > 0
			}
		},
		methods: {
			toSelectImage() {
				uni.hideKeyboard();
				uni.chooseImage({
					count: 9,
					success: (res) => {
						this.$emit("image", res.tempFiles);
					},
					fail: (chooseError) => {
						uni.showToast({
							title: '选择图片出错了',
							icon: 'none'
						})
						console.log(chooseError);
					}
				});
			},
			toVoice() {
				uni.hideKeyboard()
				this.$emit("record")
			},
			toEmotion() {
				uni.hideKeyboard()
				this.$emit("emotion")
			},
			toPlus() {
				uni.hideKeyboard()
				this.$emit("plus")
			},
			toConfirm() {
				if (!this.msg || this.msg.trim().length === 0) {
					return
				}
				this.$emit("confirm", this.msg.trim())
			},
			toChangeLine(e) {
				// 最大显示四行
				if (e.detail.lineCount <= 4) {
					let h = e.detail.height
					if (h <= uni.upx2px(60)) {
						h = uni.upx2px(60)
					}
					if (e.detail.lineCount <= 1) {
						this.voiceHeight = h
					}
					this.areaHeight = h
				} else {
					this.areaHeight = 4 * e.detail.height / e.detail.lineCount
				}
			},
			toChangeKb(e) {
				this.$emit("kb", e.detail);
			},
			clear() {
				this.msg = ''
			}
		},
		created() {
			const that = this
			uni.$on("chatEmj", val => {
				that.msg = that.msg + val
			})
			uni.$on("chatDelEmj", () => {
				if (!that.msg || that.msg.length === 0) return;
				if (that.msg.length === 1) {
					that.msg = ''
					return
				}
				const a = that.msg.substr(that.msg.length - 2, that.msg.length)
				if (emotions.includes(a)) {
					that.msg = that.msg.substr(0, that.msg.length - 2)
				} else if (that.msg.substr(that.msg.length - 2, that.msg.length).search(
						/(\ud83c[\udf00-\udfff])|(\ud83d[\udc00-\ude4f])|(\ud83d[\ude80-\udeff])/i) >= 0) {
					// 进一步搜索是否是表情
					that.msg = that.msg.substr(0, that.msg.length - 2)
				} else {
					that.msg = that.msg.substr(0, that.msg.length - 1)
				}
				// 表情需要删除2位，其他删除1位
				// const number = that.msg.substr(that.msg.length - 2, that.msg.length).search(/(\ud83c[\udf00-\udfff])|(\ud83d[\udc00-\ude4f])|(\ud83d[\ude80-\udeff])/i)==-1?1:2
				// that.msg = that.msg.substr(0, that.msg.length - number)
			})
		},
		beforeDestroy() {
			uni.$off("chatEmj")
			uni.$off("chatDelEmj")
		}
	}
</script>

<style lang="scss" scoped>
	.itl {
		// box-shadow: 0 -5rpx 5rpx 0 rgba(0,0,0,0.03);
		padding-top: 20rpx;
		padding-bottom: 20rpx;
		padding-left: 30rpx;
		padding-right: 30rpx;
		background-color: #FFFFFF;
		width: 750rpx;

		&-token {
			border-radius: 12rpx;
			background-color: #EFEFF4;
		}

		&-voice {
			border-radius: 12rpx;
			background-color: #EFEFF4;
		}

		&-area {
			padding-left: 12rpx;
			padding-right: 12rpx;
			padding-top: 12rpx;
			padding-bottom: 12rpx;
			// border-radius: 12rpx;
			background-color: #EFEFF4;
			font-size: 32rpx;
			color: $myp-text-color;
			border-radius: 40rpx;
			height: 66rpx;
		}
	} 
</style>
