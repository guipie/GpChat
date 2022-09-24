export default {
	methods: {
		mypShowToast(info, interval = null, finish = null, errorOptions = null) {
			const toastIns = this.$refs['myp-toast']
			if (toastIns) {
				toastIns.show(info, interval, finish, errorOptions)
			} else {
				const that = this
				setTimeout(() => {
					that.$refs['myp-toast'].show(info, interval, finish, errorOptions)
				}, 0)
			}
		},
		mypShowToastSuccess(msg, interval = null, finish = null, errorOptions = null) {
			let info = {
				text: msg,
				type: 'success',
				mode: 'big'
			}
			this.mypShowToast(info, interval, finish, errorOptions);
		},
		mypShowToastWarning(msg, interval = null, finish = null, errorOptions = null) {
			let info = {
				text: msg,
				type: 'warning',
				mode: 'big'
			}
			this.mypShowToast(info, interval, finish, errorOptions);
		},
		showToastSuccess(msg) {
			uni.showToast({
				title: msg,
				icon: 'success'
			});
		},
		showToastWarning(msg) {
			uni.showToast({
				title: msg,
				icon: 'none'
			});
		}
	}
}
