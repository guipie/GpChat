<template>
  <div>
    <div id="iframe-container">
      <iframe src="http://guxin.link:8888/" scrolling="auto" frameborder="0" class="trend-container2"
        id="iframe"></iframe>
    </div>
  </div>
</template>

<script>
// 进度条的使用需要安装NProgress,并在main.js注册一哈
import NProgress from 'nprogress'

export default {
  name: "LinkHome",
  mounted () {
    let iframe = document.getElementById('iframe')
    NProgress.start()

    iframe.onload = function () {
      NProgress.done()
    }
  },
}
</script>

<style scoped>
.trend-container2 {
  position: relative;
  width: 100%;
  min-height: 700px;
  padding-bottom: 16px;
}
</style>

