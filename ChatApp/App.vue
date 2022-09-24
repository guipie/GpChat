<script>
	import request from '@/api/user/index.js'

	import {
		baseWebsocketUrl
	} from '@/common/env.js'
	import store from '@/store/index.js'
	export default {
		globalData: {
			access: '',
			accessTime: new Date(),
			userId: "",
			nickName: ""
		},
		computed: {},
		methods: {},
		onLaunch: function() {
			console.log('App Launch');
			if (!store.getters.hasLogedIn) {
				store.commit('user/LoginExist');
			}
			let main = plus.android.runtimeMainActivity();
			//为了防止快速点按返回键导致程序退出重写quit方法改为隐藏至后台  
			plus.runtime.quit = function() {
				main.moveTaskToBack(false);
			};
			//重写toast方法如果内容为 ‘再按一次退出应用’ 就隐藏应用，其他正常toast  
			plus.nativeUI.toast = (function(str) {
				if (str == '再按一次退出应用') {
					main.moveTaskToBack(false);
					return false;
				} else {
					uni.showToast({
						title: str,
						icon: 'none',
					})
				}
			});
			// let _this = this;
			// uni.onNetworkStatusChange(function(res) {
			// 	if (!res.isConnected)
			// 		store.commit("socket/setSocketConnecting");
			// }); 
		},
		onShow: function() {
			console.log('App Show');
		},
		onHide: function() {
			store.commit("chat/SetLastChatToDb");
			store.commit("notice/SetNoticeSettings");
			console.log('App Hide');
		},
		onError(err) {
			console.log("app出错了", err);
		},
		onUniNViewMessage(err) { //对 nvue 页面发送的数据进行监听，可参考 nvue 向 vue 通讯
			console.log("对 nvue 页面发送的数据进行监听，可参考 nvue 向 vue 通讯", err);
		},
		onUnhandledRejection(err) { //对未处理的 Promise 拒绝事件监听函数
			console.log("对未处理的 Promise 拒绝事件监听函数", err);
		},
		onPageNotFound(err) { //页面不存在监听函数
			console.log("页面不存在监听函数", err);
		},
		onThemeChange(err) { //监听系统主题变化
			console.log("监听系统主题变化", err);
		}
	}
</script>

<style lang="scss">
	/*每个页面公共css */
	@import '@/mypUI/base.scss';
</style>
