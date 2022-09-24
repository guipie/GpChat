<template>
	<myp-popup :show="show" :hasOverlay="false" height="276rpx" width="750rpx" :duration="250">
		<view class="myp-bg-page myp-flex-column myp-align-center" style="width: 750rpx;height:276rpx;"
			@touchstart="onTouchStart" @touchmove="onTouchMove" @touchend="onTouchEnd" @touchcancel="onTouchCancel">
			<text class="myp-size-s myp-lh-ss myp-color-second" style="margin-top: 20rpx;">按住说话，上推取消</text>
			<view class="myp-flex-row myp-align-center myp-justify-center" style="margin-top: 20rpx;">
				<text class="myp-color-second"
					style="width: 250rpx;text-align: center;font-size: 60rpx;">{{hintText}}</text>
				<view class="myp-radius-ll myp-bg-error myp-flex-row myp-align-center myp-justify-center"
					style="margin-left: 42rpx;margin-right: 42rpx;width: 105rpx;height: 105rpx;">
					<!--
					<view class="myp-bg-inverse" style="width: 35rpx;height: 35rpx;"></view>
					-->
					<image :src="icon" mode="aspectFill" style="width: 40rpx;height: 40rpx;"></image>
				</view>
				<text class="myp-color-second"
					style="width: 250rpx;text-align: center;font-size: 60rpx;">{{hintText}}</text>
			</view>
		</view>
	</myp-popup>
</template>

<script>
	import {
		getTouchPoint
	} from '@/mypUI/utils/element.js'
	const recorderManager = uni.getRecorderManager()
	let timer = null
	const leastTime = 1000 // ms
	const leftX = uni.upx2px(280)
	const rightX = uni.upx2px(750 - 280)
	const cancelY = 20 // 向上滑动20px算取消

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
				canceled: false,
				startPoint: null,
				icon: '/static/triangle.png'
			}
		},
		computed: {
			hintText() {
				if (this.second <= 9) {
					return '00:0' + parseInt(this.second)
				}
				if (this.second >= 60) {
					return '01:00'
				}
				return '00:' + parseInt(this.second)
			}
		},
		methods: {
			onTouchStart(e) {
				this.icon = '/static/box.png'
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
				this.startPoint = point
				this._no = false
				this.canceled = false
				this.toTimer()
				this.startTime = Date.now()
				recorderManager.start({
					duration: 1000 * 60 * 5,
					// sampleRate: 16000,
					format: 'mp3'
					// encodeBitRate: 48000
				});
				this.$emit("start");
			},
			onTouchMove(e) {
				// console.log(e)
				if (this._no) return;
				// 检测是否在取消位置
				/*
				const point = getTouchPoint(e)
				if (point.sX < leftX) {
					this.inCancelArea = true
				} else {
					this.inCancelArea = false
				}
				*/
			},
			onTouchEnd(e) {
				this.icon = '/static/box.png'
				if (this._no) return;
				const point = getTouchPoint(e)
				/*
				if (point.sX < leftX) {
					// 直接取消
					this.toCancelRecord()
					recorderManager.stop()
					this.toCancel()
					return
				}*/
				if (!this.startPoint) {
					// 直接取消
					this.toCancelRecord();
					recorderManager.stop();
					this.toCancel();
					return;
				}
				const offsetY = point.y - this.startPoint.y
				if (offsetY <= -1 * cancelY) {
					// 直接取消
					this.toCancelRecord()
					recorderManager.stop()
					this.toCancel()
					return
				}
				this.endTime = Date.now();
				if (this.endTime - this.startTime <= leastTime) {
					// 录音时间过短
					this.toCancelRecord();
					this.$emit("short");
					return;
				}
				recorderManager.stop();
				timer && clearInterval(timer);
				this.second = 0;
			},
			onTouchCancel(e) {
				this.icon = '/static/box.png'
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
				timer = setInterval(() => {
					that.second += 0.05
					if (that.second >= 59.95) {
						timer && clearInterval(timer)
						that.second = 0
					}
				}, 50)
			}
		},
		created() {
			const that = this;
			recorderManager.onStop((res) => {
				if (that.canceled) return;
				res.tempFilePath;
				res.time = parseInt(Date.now() - that.startTime) / 1000;
				this.$emit("voice", res);
			});
		}
	}
</script>

<style>
</style>
