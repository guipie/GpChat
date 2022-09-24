export default {
	methods: {
		mypShowLoading(info) {
			const loadingIns = this.$refs['myp-loading'];
			if (!info)
				info = {
					needMask: true,
					delay: 0
				};
			if (loadingIns) {
				loadingIns.show(info)
			} else {
				const that = this
				setTimeout(() => {
					that.$refs['myp-loading'].show(info)
				}, 0)
			}
		},
		mypHideLoading() {
			const loadingIns = this.$refs['myp-loading']
			loadingIns && loadingIns.hide()
		},
		uniShowLoading(txt) {
			uni.showToast({
				title: (txt || "正在请求中.."),
				icon: "loading",
				duration: 1000 * 10
			});
		},
		uniHideLoading() {
			uni.hideToast();
		}
	}
}
