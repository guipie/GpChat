<template>
	<view v-if="show" class="vkb myp-flex-column myp-align-center myp-justify-end" @tap="toCancel">
		<view class="vkb-box myp-flex-column myp-align-center myp-justify-center" @touchstart="onTouchStart" @touchmove="onTouchMove" @touchend="onTouchEnd" @touchcancel="onTouchCancel">
			<text class="myp-size-s myp-color-second">{{hintText}}</text>
			<view style="height: 60rpx;"></view>
			<view class="myp-bg-primary myp-flex-row myp-align-center myp-justify-center" style="width: 120rpx;height: 120rpx;border-radius: 100rpx;">
				<image src="/static/speak.png" style="width: 60rpx;height: 60rpx;"></image>
			</view>
		</view>
		<view style="height: 200rpx;"></view>
		<myp-xbar bgType="none"></myp-xbar>
	</view>
</template>

<script>
	import {getTouchPoint} from '@/mypUI/utils/element.js'
	
	const recorderManager = uni.getRecorderManager()
	let timer = null
	const leastTime = 100  // ms
	const leftX = uni.upx2px(280)
	const rightX = uni.upx2px(750-280)
	
	export default {
		props: {
			show: {
				type: Boolean,
				default: false
			}
		},
		data() {
			return {
				second: 0,
				startTime: 0,
				endTime: 0,
				inCancelArea: false,
				canceled: false
			}
		},
		computed: {
			hintText() {
				if (this.second <= 0) {
					return '长按开始录音'
				}
				if (this.inCancelArea) {
					return '松开取消'
				}
				if (this.second <= 50) {
					return '松开发送，左滑取消'
				}
				let extra = 60 - this.second
				if (extra < 0) {
					extra = 0
				}
				extra = parseInt(extra)
				return `倒计时${extra}S`
			}
		},
		methods: {
			onTouchStart(e) {
				console.log(e)
				const point = getTouchPoint(e)
				// 需要按在合适的位置上才开始录音
				if (point.sX <= leftX) {
					this._no = true
					return
				}
				if (point.sX >= rightX) {
					this._no = true
					return
				}
				this._no = false
				this.canceled = false
				this.toTimer()
				this.startTime = Date.now()
				recorderManager.start({
					duration: 60000,
					format: 'mp3'
				})
			},
			onTouchMove(e) {
				console.log(e)
				if (this._no) return;
				// 检测是否在取消位置
				const point = getTouchPoint(e)
				if (point.sX < leftX) {
					this.inCancelArea = true
				} else {
					this.inCancelArea = false
				}
			},
			onTouchEnd(e) {
				console.log(e)
				if (this._no) return;
				const point = getTouchPoint(e)
				if (point.sX < leftX) {
					// 直接取消
					this.toCancelRecord()
					recorderManager.stop()
					this.toCancel()
					return
				}
				this.endTime = Date.now()
				if (this.endTime - this.startTime < leastTime) {
					// 录音时间过短
					this.toCancelRecord()
					recorderManager.stop()
					this.$emit("short")
					return
				}
				recorderManager.stop()
				timer && clearInterval(timer)
				this.second = 0
			},
			onTouchCancel(e) {
				console.log(e)
				this.toCancelRecord()
				recorderManager.stop()
				this.toCancel()
			},
			toCancel() {
				this.$emit("cancel")
			},
			toCancelRecord() {
				this.canceled = true
				timer && clearInterval(timer)
				this.second = 0
				this.startTime = 0
				this.endTime = 0
			},
			toTimer() {
				timer && clearInterval(timer)
				this.second = 0
				const that = this
				// 每隔50ms记录一下时间
				timer = setInterval(()=>{
					that.second += 0.05
					if (that.second >= 59.95) {
						timer && clearInterval(timer)
						that.second = 0
					}
				}, 50)
			}
		},
		created() {
			const that = this
			recorderManager.onStop(res => {
				if (that.canceled) {
					return
				}
				const path = res.tempFilePath
				that.$emit("voice", path)
			})
		}
	}
</script>

<style lang="scss" scoped>
	.vkb {
		position: fixed;
		left: 0;
		width: 750rpx;
		top: 0;
		bottom: 0;
		background-color: transparent;
		
		&-box {
			// box-shadow: 0 0 10rpx 0 rgba(0,0,0,0.03);
			background-color: #FFFFFF;
			border-radius: 12rpx;
			width: 500rpx;
			height: 400rpx;
		}
	}
</style>
