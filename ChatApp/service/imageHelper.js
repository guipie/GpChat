export default {
	data() {
		return {
			img404: '/static/404.png'
		}
	},
	methods: {
		previewImage(urls, currentIndex) {
			currentIndex = currentIndex || 0;
			uni.previewImage({
				urls: urls,
				current: currentIndex,
				loop: true,
				longPressActions: {
					itemList: ['发送给朋友', '保存图片'],
					success: function(data) {
						console.log('选中了第' + (data.tapIndex + 1) + '个按钮,第' + (data.index + 1) + '张图片');
					},
					fail: function(err) {
						console.log(err.errMsg);
					}
				}
			});
		}
	}
}
