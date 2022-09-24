export default {
	data() {
		return {
			leftIcons: [{
				icon: 'left',
				style: "width:44px;height:44px;"
			}]
		}
	},
	methods: {
		navLeftAction(i) {
			uni.navigateBack({
				delta: 1
			})
		}
	}
}
